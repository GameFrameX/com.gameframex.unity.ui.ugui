using GameFrameX.UI.Runtime;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.UI.UGUI.Runtime
{
    [Preserve]
    [DisallowMultipleComponent]
    public abstract class UGUI : UIForm
    {
        /// <summary>
        /// 设置UI的显示状态，不发出事件
        /// </summary>
        /// <param name="value"></param>
        protected override void InternalSetVisible(bool value)
        {
            if (gameObject.activeSelf == value)
            {
                return;
            }

            gameObject.SetActive(value);
        }

        public override bool Visible
        {
            get
            {
                if (gameObject == null)
                {
                    return false;
                }

                return gameObject.activeSelf;
            }
            set
            {
                if (gameObject == null)
                {
                    return;
                }

                if (gameObject.activeSelf == value)
                {
                    return;
                }

                if (value == false)
                {
                    // OnHideBeforeAction?.Invoke(this);
                    // foreach (var child in Children)
                    // {
                    //     ((FUI)child.Value).Visible = value;
                    // }

                    // OnHide();
                    // OnHideAfterAction?.Invoke(this);
                }

                gameObject.SetActive(value);
                if (value)
                {
                    // OnShowBeforeAction?.Invoke(this);
                    // foreach (var child in Children)
                    // {
                    //     ((FUI)child.Value).Visible = value;
                    // }
                    //
                    // OnShow();
                    // OnShowAfterAction?.Invoke(this);
                    // Refresh();
                }
            }
        }

        /// <summary>
        /// 设置当前UI对象为全屏
        /// </summary>
        protected override void MakeFullScreen()
        {
            gameObject?.GetOrAddComponent<RectTransform>()?.MakeFullScreen();
        }
    }
}