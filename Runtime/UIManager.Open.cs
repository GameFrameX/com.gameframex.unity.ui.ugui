﻿// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;
using UnityEngine;
using YooAsset;

namespace GameFrameX.UI.UGUI.Runtime
{
    /// <summary>
    /// 界面管理器。
    /// </summary>
    internal sealed partial class UIManager
    {
        protected override async Task<IUIForm> InnerOpenUIFormAsync(string uiFormAssetPath, Type uiFormType, bool pauseCoveredUIForm, object userData, bool isFullScreen = false, bool isMultiple = false)
        {
            GameFrameworkGuard.NotNull(m_AssetManager, nameof(m_AssetManager));
            GameFrameworkGuard.NotNull(m_UIFormHelper, nameof(m_UIFormHelper));
            GameFrameworkGuard.NotNull(uiFormType, nameof(uiFormType));
            var uiFormAssetName = uiFormType.Name;
            string assetPath = PathHelper.Combine(uiFormAssetPath, uiFormAssetName);
            var uiFormInstanceObject = m_InstancePool.Spawn(assetPath);
            if (uiFormInstanceObject != null && isMultiple == false)
            {
                // 如果对象池存在
                return InternalOpenUIForm(-1, uiFormAssetName, uiFormType, uiFormInstanceObject.Target, pauseCoveredUIForm, false, 0f, userData, isFullScreen);
            }

            int serialId = ++m_Serial;
            m_UIFormsBeingLoaded.Add(serialId, uiFormAssetName);
            OpenUIFormInfo openUIFormInfo = OpenUIFormInfo.Create(serialId, uiFormType, pauseCoveredUIForm, userData, isFullScreen);
            if (uiFormAssetPath.IndexOf(Utility.Asset.Path.BundlesDirectoryName, StringComparison.OrdinalIgnoreCase) < 0)
            {
                // 从Resources 中加载
                var original = (GameObject)Resources.Load(assetPath);
                var gameObject = UnityEngine.Object.Instantiate(original);
                gameObject.name = uiFormAssetName;
                return LoadAssetSuccessCallback(assetPath, gameObject, 0, openUIFormInfo);
            }

            // 从包中加载
            var assetHandle = await m_AssetManager.LoadAssetAsync<UnityEngine.Object>(assetPath);
            if (assetHandle.IsSucceed())
            {
                // 加载成功
                var gameObject = assetHandle.InstantiateSync();
                gameObject.name = uiFormAssetName;
                return LoadAssetSuccessCallback(assetPath, gameObject, assetHandle.Progress, openUIFormInfo);
            }

            // 加载失败
            return LoadAssetFailureCallback(assetPath, assetHandle.LastError, openUIFormInfo);
        }

        private IUIForm InternalOpenUIForm(int serialId, string uiFormAssetName, Type uiFormType, object uiFormInstance, bool pauseCoveredUIForm, bool isNewInstance, float duration, object userData, bool isFullScreen)
        {
            try
            {
                IUIForm uiForm = m_UIFormHelper.CreateUIForm(uiFormInstance, uiFormType, userData);
                if (uiForm == null)
                {
                    throw new GameFrameworkException("Can not create UI form in UI form helper.");
                }

                var uiGroup = uiForm.UIGroup;
                uiForm.Init(serialId, uiFormAssetName, uiGroup, null, pauseCoveredUIForm, isNewInstance, userData, isFullScreen);

                if (!uiGroup.InternalHasInstanceUIForm(uiFormAssetName, uiForm))
                {
                    uiGroup.AddUIForm(uiForm);
                }

                uiForm.OnOpen(userData);
                uiForm.BindEvent();
                uiForm.LoadData();
                uiForm.UpdateLocalization();
                uiGroup.Refresh();

                if (m_OpenUIFormSuccessEventHandler != null)
                {
                    OpenUIFormSuccessEventArgs openUIFormSuccessEventArgs = OpenUIFormSuccessEventArgs.Create(uiForm, duration, userData);
                    m_OpenUIFormSuccessEventHandler(this, openUIFormSuccessEventArgs);
                    // ReferencePool.Release(openUIFormSuccessEventArgs);
                }

                return uiForm;
            }
            catch (Exception exception)
            {
                if (m_OpenUIFormFailureEventHandler != null)
                {
                    OpenUIFormFailureEventArgs openUIFormFailureEventArgs = OpenUIFormFailureEventArgs.Create(serialId, uiFormAssetName, pauseCoveredUIForm, exception.ToString(), userData);
                    m_OpenUIFormFailureEventHandler(this, openUIFormFailureEventArgs);
                    return GetUIForm(openUIFormFailureEventArgs.SerialId);
                }

                throw;
            }
        }

        private IUIForm LoadAssetSuccessCallback(string uiFormAssetName, object uiFormAsset, float duration, object userData)
        {
            OpenUIFormInfo openUIFormInfo = (OpenUIFormInfo)userData;
            if (openUIFormInfo == null)
            {
                throw new GameFrameworkException("Open UI form info is invalid.");
            }

            if (m_UIFormsToReleaseOnLoad.Contains(openUIFormInfo.SerialId))
            {
                m_UIFormsToReleaseOnLoad.Remove(openUIFormInfo.SerialId);
                ReferencePool.Release(openUIFormInfo);
                m_UIFormHelper.ReleaseUIForm(uiFormAsset, null);
                return GetUIForm(openUIFormInfo.SerialId);
            }

            m_UIFormsBeingLoaded.Remove(openUIFormInfo.SerialId);
            var uiFormInstanceObject = UIFormInstanceObject.Create(uiFormAssetName, uiFormAsset, m_UIFormHelper.InstantiateUIForm(uiFormAsset), m_UIFormHelper);
            m_InstancePool.Register(uiFormInstanceObject, true);

            var uiForm = InternalOpenUIForm(openUIFormInfo.SerialId, uiFormAssetName, openUIFormInfo.FormType, uiFormInstanceObject.Target, openUIFormInfo.PauseCoveredUIForm, true, duration, openUIFormInfo.UserData, openUIFormInfo.IsFullScreen);
            ReferencePool.Release(openUIFormInfo);
            return uiForm;
        }

        private IUIForm LoadAssetFailureCallback(string uiFormAssetName, string errorMessage, object userData)
        {
            OpenUIFormInfo openUIFormInfo = (OpenUIFormInfo)userData;
            if (openUIFormInfo == null)
            {
                throw new GameFrameworkException("Open UI form info is invalid.");
            }

            if (m_UIFormsToReleaseOnLoad.Contains(openUIFormInfo.SerialId))
            {
                m_UIFormsToReleaseOnLoad.Remove(openUIFormInfo.SerialId);
                return GetUIForm(openUIFormInfo.SerialId);
            }

            m_UIFormsBeingLoaded.Remove(openUIFormInfo.SerialId);
            string appendErrorMessage = Utility.Text.Format("Load UI form failure, asset name '{0}', error message '{2}'.", uiFormAssetName, errorMessage);
            if (m_OpenUIFormFailureEventHandler != null)
            {
                OpenUIFormFailureEventArgs openUIFormFailureEventArgs = OpenUIFormFailureEventArgs.Create(openUIFormInfo.SerialId, uiFormAssetName, openUIFormInfo.PauseCoveredUIForm, appendErrorMessage, openUIFormInfo.UserData);
                m_OpenUIFormFailureEventHandler(this, openUIFormFailureEventArgs);
                return GetUIForm(openUIFormInfo.SerialId);
            }

            throw new GameFrameworkException(appendErrorMessage);
        }
    }
}