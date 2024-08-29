//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFrameX.UI.Runtime;
using UnityEngine;

namespace GameFrameX.UI.UGUI.Runtime
{
    /// <summary>
    /// UGUI界面组辅助器。
    /// </summary>
    public sealed class UGUIUIGroupHelper : UIGroupHelperBase
    {
        /// <summary>
        /// 设置界面组深度。
        /// </summary>
        /// <param name="depth">界面组深度。</param>
        public override void SetDepth(int depth)
        {
            transform.localPosition = new Vector3(0, 0, depth * 1000);
        }

        public override IUIGroupHelper Handler(Transform root, string groupName, string uiGroupHelperTypeName, IUIGroupHelper customUIGroupHelper)
        {
            GameObject component = new GameObject();
            var comName = groupName;
            component.name = comName;
            component.transform.SetParent(root, false);
            component.SetLayerRecursively(LayerMask.NameToLayer("UI"));
            RectTransform rectTransform = component.GetOrAddComponent<RectTransform>();
            rectTransform.MakeFullScreen();
            return GameFrameX.Runtime.Helper.CreateHelper(component, uiGroupHelperTypeName, (UIGroupHelperBase)customUIGroupHelper, 0);
        }
    }
}