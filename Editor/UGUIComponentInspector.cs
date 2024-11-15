//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameFrameX.Editor;
using GameFrameX.UI.UGUI.Runtime;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFrameX.UI.UGUI.Editor
{
    [CustomEditor(typeof(Runtime.UGUI), true)]
    internal sealed class UGUIComponentInspector : GameFrameworkInspector
    {
        private GUIStyle _customButtonStyle;

        private GUIStyle CustomButtonStyle
        {
            get
            {
                if (_customButtonStyle == null)
                {
                    _customButtonStyle = new GUIStyle(GUI.skin.button)
                    {
                        fontSize = 32,
                        normal =
                        {
                            textColor = Color.green,
                        },
                        hover =
                        {
                            textColor = Color.yellow,
                        },
                        active =
                        {
                            textColor = Color.green,
                        },
                        alignment = TextAnchor.MiddleCenter,
                    };
                }

                return _customButtonStyle;
            }
        }

        private readonly GUIContent _reBind = new GUIContent("重新绑定列表");

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            if (target is Runtime.UGUI targetUGUI)
            {
                GameObject targetGameObject = targetUGUI.gameObject;
                var fieldInfos = targetUGUI.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
                {
                    bool buttonClicked = EditorGUILayout.DropdownButton(_reBind, FocusType.Passive, CustomButtonStyle);
                    if (buttonClicked)
                    {
                        // 重新绑定
                        Dictionary<string, string> fieldMap = new Dictionary<string, string>();
                        foreach (var fieldInfo in fieldInfos)
                        {
                            var serializeFieldAttributes = fieldInfo.GetCustomAttribute(typeof(SerializeField));
                            if (serializeFieldAttributes == null)
                            {
                                continue;
                            }

                            var uguiElementPropertyAttribute = fieldInfo.GetCustomAttribute(typeof(UGUIElementPropertyAttribute));
                            if (uguiElementPropertyAttribute == null)
                            {
                                continue;
                            }

                            var elementPropertyAttribute = (UGUIElementPropertyAttribute)uguiElementPropertyAttribute;

                            fieldMap[fieldInfo.Name] = elementPropertyAttribute.Path;
                        }

                        foreach (var kv in fieldMap)
                        {
                            var property = serializedObject.FindProperty(kv.Key);
                            if (property != null)
                            {
                                var targetFind = targetGameObject.transform.Find(kv.Value);
                                if (targetFind != null)
                                {
                                    property.objectReferenceValue = targetFind.gameObject;
                                }
                            }
                        }
                    }

                    // 编辑器下不允许修改
                    GUI.enabled = false;
                    foreach (var fieldInfo in fieldInfos)
                    {
                        var attributes = fieldInfo.GetCustomAttributes(typeof(SerializeField));
                        if (!attributes.Any())
                        {
                            continue;
                        }

                        EditorGUILayout.ObjectField(fieldInfo.Name, fieldInfo.GetValue(targetUGUI) as Object, fieldInfo.FieldType, true);
                    }

                    GUI.enabled = true;
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}