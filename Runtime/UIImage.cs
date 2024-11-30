using UnityEngine;

namespace GameFrameX.UI.UGUI.Runtime
{
    [DisallowMultipleComponent]
    public class UIImage : UnityEngine.UI.Image
    {
        private string m_icon { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string icon
        {
            get { return m_icon; }
            set
            {
                if (m_icon != value)
                {
                    m_icon = value;
                    this.SetIcon(m_icon);
                }
            }
        }
    }
}