// ==========================================================================================
//   GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//   GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//   均受中华人民共和国及相关国际法律法规保护。
//   are protected by the laws of the People's Republic of China and relevant international regulations.
//   使用本项目须严格遵守相应法律法规及开源许可证之规定。
//   Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
//   本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//   This project is dual-licensed under the MIT License and Apache License 2.0,
//   完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//   please refer to the LICENSE file in the root directory of the source code for the full license text.
//   禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//   It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//   侵犯他人合法权益等法律法规所禁止的行为！
//   or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//   因基于本项目二次开发所产生的一切法律纠纷与责任，
//   Any legal disputes and liabilities arising from secondary development based on this project
//   本项目组织与贡献者概不承担。
//   shall be borne solely by the developer; the project organization and contributors assume no responsibility.
//   GitHub 仓库：https://github.com/GameFrameX
//   GitHub Repository: https://github.com/GameFrameX
//   Gitee  仓库：https://gitee.com/GameFrameX
//   Gitee Repository:  https://gitee.com/GameFrameX
//   CNB  仓库：https://cnb.cool/GameFrameX
//   CNB Repository:  https://cnb.cool/GameFrameX
//   官方文档：https://gameframex.doc.alianblank.com/
//   Official Documentation: https://gameframex.doc.alianblank.com/
//  ==========================================================================================

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
    /// <remarks>
    /// UI manager.
    /// </remarks>
    internal sealed partial class UIManager
    {
        /// <summary>
        /// 正在加载的界面列表。
        /// </summary>
        /// <remarks>
        /// List of UI forms currently being loaded.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        private readonly List<UIFormLoadingObject> m_LoadingUIForms = new List<UIFormLoadingObject>(64);

        /// <summary>
        /// 需要移除的界面加载对象列表。
        /// </summary>
        /// <remarks>
        /// List of UI form loading objects to be removed.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        private readonly List<UIFormLoadingObject> m_UIFormsRemoveList = new List<UIFormLoadingObject>(64);

        /// <summary>
        /// 异步打开界面。
        /// </summary>
        /// <remarks>
        /// Opens a UI form asynchronously.
        /// </remarks>
        /// <param name="uiFormAssetPath">界面资源路径 / UI form asset path</param>
        /// <param name="uiFormType">界面类型 / UI form type</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面 / Whether to pause covered UI forms</param>
        /// <param name="userData">用户自定义数据 / User custom data</param>
        /// <param name="isFullScreen">是否全屏显示 / Whether to display in full screen</param>
        /// <returns>界面实例 / UI form instance</returns>
        [UnityEngine.Scripting.Preserve]
        protected override async Task<IUIForm> InnerOpenUIFormAsync(string uiFormAssetPath, Type uiFormType, bool pauseCoveredUIForm, object userData, bool isFullScreen = false)
        {
            GameFrameworkGuard.NotNull(m_AssetManager, nameof(m_AssetManager));
            GameFrameworkGuard.NotNull(m_UIFormHelper, nameof(m_UIFormHelper));
            GameFrameworkGuard.NotNull(uiFormType, nameof(uiFormType));
            var uiFormAssetName = uiFormType.Name;
            string assetPath = PathHelper.Combine(uiFormAssetPath, uiFormAssetName);
            var uiFormInstanceObject = m_InstancePool.Spawn(assetPath);
            if (uiFormInstanceObject != null)
            {
                // 如果对象池存在
                return InternalOpenUIForm(-1, uiFormAssetPath, uiFormAssetName, uiFormType, uiFormInstanceObject.Target, pauseCoveredUIForm, false, 0f, userData, isFullScreen);
            }

            var uiForm = InnerLoadUIFormAsync(uiFormAssetPath, uiFormType, pauseCoveredUIForm, userData, isFullScreen, uiFormAssetName, assetPath);
            UIFormLoadingObject uiFormLoadingObject = UIFormLoadingObject.Create(uiFormAssetPath, uiFormAssetName, uiFormType, uiForm);
            m_LoadingUIForms.Add(uiFormLoadingObject);
            var result = await uiForm;

            foreach (var value in m_LoadingUIForms)
            {
                if (value.UIFormAssetPath == uiFormAssetPath && value.UIFormAssetName == uiFormAssetName && value.UIFormType == uiFormType)
                {
                    m_UIFormsRemoveList.Add(value);
                }
            }

            foreach (var value in m_UIFormsRemoveList)
            {
                m_LoadingUIForms.Remove(value);
                ReferencePool.Release(value);
            }

            m_UIFormsRemoveList.Clear();

            return result;
        }

        /// <summary>
        /// 异步加载界面。
        /// </summary>
        /// <remarks>
        /// Loads a UI form asynchronously.
        /// </remarks>
        /// <param name="uiFormAssetPath">界面资源路径 / UI form asset path</param>
        /// <param name="uiFormType">界面类型 / UI form type</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面 / Whether to pause covered UI forms</param>
        /// <param name="userData">用户自定义数据 / User custom data</param>
        /// <param name="isFullScreen">是否全屏显示 / Whether to display in full screen</param>
        /// <param name="uiFormAssetName">界面资源名称 / UI form asset name</param>
        /// <param name="assetPath">完整资源路径 / Full asset path</param>
        /// <returns>界面实例 / UI form instance</returns>
        [UnityEngine.Scripting.Preserve]
        private async Task<IUIForm> InnerLoadUIFormAsync(string uiFormAssetPath, Type uiFormType, bool pauseCoveredUIForm, object userData, bool isFullScreen, string uiFormAssetName, string assetPath)
        {
            int serialId = ++m_Serial;
            m_UIFormsBeingLoaded.Add(serialId, uiFormAssetName);
            OpenUIFormInfo openUIFormInfo = OpenUIFormInfo.Create(serialId, assetPath, uiFormAssetName, uiFormType, pauseCoveredUIForm, userData, isFullScreen);
            if (uiFormAssetPath.IndexOf(Utility.Asset.Path.BundlesDirectoryName, StringComparison.OrdinalIgnoreCase) < 0)
            {
                // 从Resources 中加载
                var original = (GameObject)Resources.Load(assetPath);
                if (original == null)
                {
                    return LoadAssetFailureCallback(assetPath, $"Resources.Load failed for path: {assetPath}", openUIFormInfo);
                }
                var gameObject = UnityEngine.Object.Instantiate(original);
                gameObject.name = uiFormAssetName;
                return LoadAssetSuccessCallback(assetPath, gameObject, 0, openUIFormInfo);
            }

            // 从包中加载
            var assetHandle = await m_AssetManager.LoadAssetAsync<UnityEngine.Object>(assetPath);
            if (assetHandle.IsDone && assetHandle.Status == EOperationStatus.Succeed)
            {
                // 加载成功
                var gameObject = assetHandle.InstantiateSync();
                gameObject.name = uiFormAssetName;
                openUIFormInfo.SetAssetHandle(assetHandle);
                return LoadAssetSuccessCallback(assetPath, gameObject, assetHandle.Progress, openUIFormInfo);
            }

            // 加载失败
            return LoadAssetFailureCallback(assetPath, assetHandle.LastError, openUIFormInfo);
        }

        /// <summary>
        /// 内部打开界面。
        /// </summary>
        /// <remarks>
        /// Opens a UI form internally.
        /// </remarks>
        /// <param name="serialId">界面序列号 / UI form serial ID</param>
        /// <param name="uiFormAssetPath">界面资源路径 / UI form asset path</param>
        /// <param name="uiFormAssetName">界面资源名称 / UI form asset name</param>
        /// <param name="uiFormType">界面类型 / UI form type</param>
        /// <param name="uiFormInstance">界面实例 / UI form instance</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面 / Whether to pause covered UI forms</param>
        /// <param name="isNewInstance">是否为新实例 / Whether it is a new instance</param>
        /// <param name="duration">加载耗时 / Loading duration</param>
        /// <param name="userData">用户自定义数据 / User custom data</param>
        /// <param name="isFullScreen">是否全屏显示 / Whether to display in full screen</param>
        /// <returns>界面实例 / UI form instance</returns>
        [UnityEngine.Scripting.Preserve]
        private IUIForm InternalOpenUIForm(int serialId, string uiFormAssetPath, string uiFormAssetName, Type uiFormType, object uiFormInstance, bool pauseCoveredUIForm, bool isNewInstance, float duration, object userData, bool isFullScreen)
        {
            try
            {
                IUIForm uiForm = m_UIFormHelper.CreateUIForm(uiFormInstance, uiFormType, userData);
                if (uiForm == null)
                {
                    throw new GameFrameworkException("Can not create UI form in UI form helper.");
                }

                var uiGroup = uiForm.UIGroup;
                uiForm.Init(serialId, uiFormAssetPath, uiFormAssetName, uiGroup, null, pauseCoveredUIForm, isNewInstance, userData, RecycleInterval, isFullScreen);

                if (!uiGroup.InternalHasInstanceUIForm(uiFormAssetName, uiForm))
                {
                    uiGroup.AddUIForm(uiForm);
                }

                uiForm.OnOpen(userData);
                uiForm.BindEvent();
                uiForm.LoadData();
                uiForm.UpdateLocalization();
                if (uiForm.EnableShowAnimation)
                {
                    uiForm.Show(m_UIFormShowHandler, null);
                }

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

        /// <summary>
        /// 资源加载成功回调。
        /// </summary>
        /// <remarks>
        /// Callback when asset loading succeeds.
        /// </remarks>
        /// <param name="uiFormAssetPath">界面资源路径 / UI form asset path</param>
        /// <param name="uiFormAsset">界面资源对象 / UI form asset object</param>
        /// <param name="duration">加载耗时 / Loading duration</param>
        /// <param name="userData">用户自定义数据 / User custom data</param>
        /// <returns>界面实例 / UI form instance</returns>
        [UnityEngine.Scripting.Preserve]
        private IUIForm LoadAssetSuccessCallback(string uiFormAssetPath, object uiFormAsset, float duration, object userData)
        {
            OpenUIFormInfo openUIFormInfo = (OpenUIFormInfo)userData;
            if (openUIFormInfo == null)
            {
                throw new GameFrameworkException("Open UI form info is invalid.");
            }

            if (m_UIFormsToReleaseOnLoad.Contains(openUIFormInfo.SerialId))
            {
                var form = GetUIForm(openUIFormInfo.SerialId);
                m_UIFormsToReleaseOnLoad.Remove(openUIFormInfo.SerialId);
                m_UIFormHelper.ReleaseUIForm(uiFormAsset, null, openUIFormInfo.AssetHandle, uiFormAssetPath, openUIFormInfo.AssetName);
                ReferencePool.Release(openUIFormInfo);
                return form;
            }

            m_UIFormsBeingLoaded.Remove(openUIFormInfo.SerialId);
            var uiFormInstanceObject = UIFormInstanceObject.Create(uiFormAssetPath, openUIFormInfo.AssetName, uiFormAsset, m_UIFormHelper.InstantiateUIForm(uiFormAsset), m_UIFormHelper, openUIFormInfo.AssetHandle);
            m_InstancePool.Register(uiFormInstanceObject, true);

            var uiForm = InternalOpenUIForm(openUIFormInfo.SerialId, uiFormAssetPath, openUIFormInfo.AssetName, openUIFormInfo.FormType, uiFormInstanceObject.Target, openUIFormInfo.PauseCoveredUIForm, true, duration, openUIFormInfo.UserData, openUIFormInfo.IsFullScreen);
            ReferencePool.Release(openUIFormInfo);
            return uiForm;
        }

        /// <summary>
        /// 资源加载失败回调。
        /// </summary>
        /// <remarks>
        /// Callback when asset loading fails.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称 / UI form asset name</param>
        /// <param name="errorMessage">错误信息 / Error message</param>
        /// <param name="userData">用户自定义数据 / User custom data</param>
        /// <returns>界面实例 / UI form instance</returns>
        [UnityEngine.Scripting.Preserve]
        private IUIForm LoadAssetFailureCallback(string uiFormAssetName, string errorMessage, object userData)
        {
            OpenUIFormInfo openUIFormInfo = (OpenUIFormInfo)userData;
            if (openUIFormInfo == null)
            {
                throw new GameFrameworkException("Open UI form info is invalid.");
            }

            if (m_UIFormsToReleaseOnLoad.Contains(openUIFormInfo.SerialId))
            {
                var uiForm = GetUIForm(openUIFormInfo.SerialId);
                m_UIFormsToReleaseOnLoad.Remove(openUIFormInfo.SerialId);
                ReferencePool.Release(openUIFormInfo);
                return uiForm;
            }

            m_UIFormsBeingLoaded.Remove(openUIFormInfo.SerialId);
            string appendErrorMessage = Utility.Text.Format("Load UI form failure, asset name '{0}', error message '{1}'.", uiFormAssetName, errorMessage);
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
