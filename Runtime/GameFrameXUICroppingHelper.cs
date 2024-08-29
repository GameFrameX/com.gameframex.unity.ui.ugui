using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.UI.UGUI.Runtime
{
    [Preserve]
    public class GameFrameXUIToUGUICroppingHelper : MonoBehaviour
    {
        private Type[] m_Types;

        [Preserve]
        private void Start()
        {
            m_Types = new[]
            {
                typeof(UIManager),
            };
        }
    }
}