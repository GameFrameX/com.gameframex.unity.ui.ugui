// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

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
        private readonly GUIContent _reGenerate = new GUIContent("重新生成代码");

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
                    bool buttonGenerateClicked = EditorGUILayout.DropdownButton(_reGenerate, FocusType.Passive, CustomButtonStyle);
                    if (buttonGenerateClicked && !EditorApplication.isPlaying)
                    {
                        UGUICodeGenerator.Generate(targetGameObject);
                    }

                    bool buttonClicked = EditorGUILayout.DropdownButton(_reBind, FocusType.Passive, CustomButtonStyle);
                    if (buttonClicked && !EditorApplication.isPlaying)
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