using System;

namespace GameFrameX.UI.UGUI.Runtime
{
    /// <summary>
    /// UGUI控件属性
    /// </summary>
    public sealed class UGUIElementPropertyAttribute : Attribute
    {
        /// <summary>
        /// 控件路径
        /// </summary>
        public string Path { get; }

        public UGUIElementPropertyAttribute(string path)
        {
            Path = path;
        }
    }
}