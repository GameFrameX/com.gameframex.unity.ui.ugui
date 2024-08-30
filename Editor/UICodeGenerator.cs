using UnityEditor;
using UnityEngine;

namespace GameFrameX.UI.UGUI.Editor
{
    [InitializeOnLoad]
    internal static class UICodeGenerator
    {
        /// <summary>
        /// 生成UI代码
        /// </summary>
        [MenuItem("GameObject/UGUI/Code", false, 1)]
        static void Code()
        {
            // Debug.Log("Test");
            if (Selection.activeObject)
            {
                var activeObject = (GameObject)Selection.activeObject;
                if (activeObject)
                {
                    var ugui = activeObject.GetComponent<GameFrameX.UI.UGUI.Runtime.UGUI>();
                    Debug.Log(ugui);
                }
            }
        }
    }
}