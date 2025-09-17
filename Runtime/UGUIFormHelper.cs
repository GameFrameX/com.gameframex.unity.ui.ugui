//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Reflection;
using GameFrameX.Asset.Runtime;
using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;
using UnityEngine;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;

namespace GameFrameX.UI.UGUI.Runtime
{
    /// <summary>
    /// 默认界面辅助器。
    /// </summary>
    [Preserve]
    public sealed class UGUIFormHelper : UIFormHelperBase
    {
        private UIComponent m_UIComponent = null;
        private AssetComponent m_AssetComponent = null;

        /// <summary>
        /// 实例化界面。
        /// </summary>
        /// <param name="uiFormAsset">要实例化的界面资源。</param>
        /// <returns>实例化后的界面。</returns>
        public override object InstantiateUIForm(object uiFormAsset)
        {
            return (Object)uiFormAsset;
        }

        /// <summary>
        /// 创建界面。
        /// </summary>
        /// <param name="uiFormInstance">界面实例。</param>
        /// <param name="uiFormType">界面逻辑类</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面。</returns>
        public override IUIForm CreateUIForm(object uiFormInstance, Type uiFormType, object userData)
        {
            var uiGameObject = uiFormInstance as GameObject;
            if (uiGameObject == null)
            {
                Log.Error("UI form instance is invalid.");
                return null;
            }

            var componentType = uiGameObject.GetOrAddComponent(uiFormType);
            if (!(componentType is IUIForm uiForm))
            {
                Log.Error("UI form instance is invalid.");
                return null;
            }

            if (uiForm.IsAwake == false)
            {
                uiForm.OnAwake();
            }

            var uiGroup = uiForm.UIGroup;
            if (uiGroup == null)
            {
                var attribute = uiFormType.GetCustomAttribute(typeof(OptionUIGroup));
                if (attribute is OptionUIGroup optionUIGroup)
                {
                    uiGroup = m_UIComponent.GetUIGroup(optionUIGroup.GroupName);
                }
            }

            if (uiGroup == null)
            {
                Log.Error("UI group is invalid.");
                return null;
            }

            var uiTransform = uiGameObject.transform;
            uiTransform.SetParent(((MonoBehaviour)uiGroup.Helper).transform);
            uiTransform.localScale = Vector3.one;
            return uiForm;
        }

        /// <summary>
        /// 释放界面。
        /// </summary>
        /// <param name="uiFormAsset">要释放的界面资源。</param>
        /// <param name="uiFormInstance">要释放的界面实例。</param>
        public override void ReleaseUIForm(object uiFormAsset, object uiFormInstance)
        {
            // m_AssetComponent.UnloadAsset(uiFormAsset);
            Destroy((Object)uiFormInstance);
        }

        private void Awake()
        {
            m_AssetComponent = GameEntry.GetComponent<AssetComponent>();
            if (m_AssetComponent == null)
            {
                Log.Fatal("Asset component is invalid.");
                return;
            }

            m_UIComponent = GameEntry.GetComponent<UIComponent>();
            if (m_UIComponent == null)
            {
                Log.Fatal("UI component is invalid.");
                return;
            }
        }
    }
}