using UnityEngine;

namespace GameFrameX.UI.UGUI.Runtime
{
    public static class RectTransformExtension
    {
        /// <summary>
        /// 设置当前UI对象为全屏
        /// </summary>
        public static void MakeFullScreen(this RectTransform rectTransform)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;
        }
    }
}