using System.Collections.Generic;
using System.IO;
using System.Text;
using GameFrameX.Runtime;
using GameFrameX.UI.UGUI.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameFrameX.UI.UGUI.Editor
{
    /// <summary>
    /// UGUI代码生成
    /// </summary>
    internal static class UGUICodeGenerator
    {
        /// <summary>
        /// 生成UI代码
        /// </summary>
        [MenuItem("GameObject/UI/Generate UGUI Code(生成UGUI代码)", false, 1)]
        static void Code()
        {
            if (Selection.activeObject)
            {
                var selectedObject = (GameObject)Selection.activeObject;
                if (selectedObject)
                {
                    if (PrefabUtility.GetPrefabAssetType(selectedObject) != PrefabAssetType.NotAPrefab)
                    {
                        string className = selectedObject.name;
                        string savePath = PathHelper.Combine(Application.dataPath, "Hotfix", "UI", "UGUI", className);
                        CreateFoldersIfNotExist(savePath);
                        var codeString = GenerateCode(selectedObject);
                        string filePath = Path.Combine(savePath, className + ".cs");
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }

                        File.WriteAllText(filePath, codeString, Encoding.UTF8);
                        AssetDatabase.Refresh();

                        Debug.Log("生成UI代码完成");
                        Debug.Log("现在请将生成的代码挂载到当前的UGUI预制体上,然后点击重新绑定列表");
                        return;
                    }
                }
            }

            Debug.LogError("请选择一个有效的UGUI预制体进行操作");
        }

        private static string GenerateCode(GameObject selectedObject)
        {
            StringBuilder codeBuilder = new StringBuilder();
            // 定义类名，可以根据预制体名称等规则来定，这里简单示例
            string className = selectedObject.name;
            codeBuilder.AppendLine("/** This is an automatically generated class by UGUI. Please do not modify it. **/");
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("#if ENABLE_UI_UGUI");
            codeBuilder.AppendLine("using Cysharp.Threading.Tasks;");
            codeBuilder.AppendLine("using GameFrameX.Entity.Runtime;");
            codeBuilder.AppendLine("using GameFrameX.UI.Runtime;");
            codeBuilder.AppendLine("using GameFrameX.Runtime;");
            codeBuilder.AppendLine("using GameFrameX.UI.UGUI.Runtime;");
            codeBuilder.AppendLine("using UnityEngine;");
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("namespace Hotfix.UI");
            codeBuilder.AppendLine("{");
            codeBuilder.AppendLine("\t/// <summary>");
            codeBuilder.AppendLine($"\t/// 代码生成的UI代码{className}");
            codeBuilder.AppendLine("\t/// </summary>");
            codeBuilder.AppendLine("\t[DisallowMultipleComponent]");
            codeBuilder.AppendLine($"\tpublic sealed partial class {className} : UGUI");
            codeBuilder.AppendLine("\t{");


            codeBuilder.AppendLine("\t\tpublic GameObject self { get; private set; }");
            codeBuilder.AppendLine();
            List<NodeInfo> nodeInfos = new List<NodeInfo>();

            // 遍历所有子节点并生成获取子节点的代码
            PropertyHandler(selectedObject, null, nodeInfos, true);
            PropertyCodeHandler(codeBuilder, nodeInfos);

            // 生成预制体实例化代码
            codeBuilder.AppendLine("\t\tprotected override void InitView()");
            codeBuilder.AppendLine("\t\t{");
            codeBuilder.AppendLine("\t\t\tthis.self = this.gameObject;");
            codeBuilder.AppendLine("\t\t}");

            codeBuilder.AppendLine();
            codeBuilder.AppendLine("\t}");
            codeBuilder.AppendLine("}");
            codeBuilder.AppendLine("#endif");
            return codeBuilder.ToString().Replace("\r\n", "\n");
        }

        private static string PathHandler(NodeInfo nodeInfo)
        {
            if (nodeInfo.Parent == null)
            {
                return nodeInfo.Path;
            }

            string path = PathHandler(nodeInfo.Parent) + "/" + nodeInfo.Path;
            return path;
        }

        private static void PropertyCodeHandler(StringBuilder codeBuilder, List<NodeInfo> nodeInfos)
        {
            codeBuilder.AppendLine("\t\t#region Properties");
            foreach (var nodeInfo in nodeInfos)
            {
                string path = PathHandler(nodeInfo);
                codeBuilder.AppendLine($"\t\t[SerializeField] [UGUIElementProperty(\"{path}\")] private {nodeInfo.Type} m{nodeInfo.Name};");
                codeBuilder.AppendLine($"\t\tpublic {nodeInfo.Type} m_{nodeInfo.Name} {{ get {{ return m{nodeInfo.Name};}} }}");
                codeBuilder.AppendLine();
            }

            codeBuilder.AppendLine("\t\t#endregion");
            codeBuilder.AppendLine();
        }

        private static bool PropertyHandler(GameObject selectedObject, NodeInfo parentInfo, List<NodeInfo> nodeInfos, bool isRoot = false)
        {
            var prefabAssetType = PrefabUtility.GetPrefabAssetType(selectedObject);

            if (prefabAssetType == PrefabAssetType.NotAPrefab)
            {
                return false;
            }

            var uguiComponent = selectedObject.GetComponent<Runtime.UGUI>();
            if (uguiComponent != null && !isRoot)
            {
                return false;
            }

            var children = selectedObject.transform.childCount;
            for (var index = 0; index < children; index++)
            {
                var child = selectedObject.transform.GetChild(index);
                var type = ConvertType(child);
                var name = $"{child.name.Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty)}";
                if (parentInfo != null)
                {
                    name = $"{parentInfo.Name}__{name}";
                }

                var nodeInfo = new NodeInfo()
                {
                    Name = name,
                    Path = child.name,
                    Type = type,
                    GameObject = child.gameObject,
                    Transform = child,
                    Parent = parentInfo
                };

                var result = PropertyHandler(child.gameObject, nodeInfo, nodeInfos);
                if (result)
                {
                    nodeInfos.Add(nodeInfo);
                }
            }

            return true;
        }

        class NodeInfo
        {
            /// <summary>
            /// 规范化之后的名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 节点路径
            /// </summary>
            public string Path { get; set; }

            /// <summary>
            /// 节点类型
            /// </summary>
            public string Type { get; set; }

            public GameObject GameObject { get; set; }
            public Transform Transform { get; set; }
            public NodeInfo Parent { get; set; }
        }

        /// <summary>
        /// UI 控件类型转换
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        private static string ConvertType(Transform transform)
        {
            UIBehaviour component = transform.GetComponent<Button>();
            if (component != null)
            {
                return typeof(Button).FullName;
            }

            component = transform.GetComponent<Text>();
            if (component != null)
            {
                return typeof(Text).FullName;
            }

            component = transform.GetComponent<ToggleGroup>();
            if (component != null)
            {
                return typeof(ToggleGroup).FullName;
            }

            component = transform.GetComponent<Toggle>();
            if (component != null)
            {
                return typeof(Toggle).FullName;
            }

            component = transform.GetComponent<InputField>();
            if (component != null)
            {
                return typeof(InputField).FullName;
            }

            component = transform.GetComponent<ScrollRect>();
            if (component != null)
            {
                return typeof(ScrollRect).FullName;
            }

            component = transform.GetComponent<Dropdown>();
            if (component != null)
            {
                return typeof(Dropdown).FullName;
            }

            component = transform.GetComponent<Scrollbar>();
            if (component != null)
            {
                return typeof(Scrollbar).FullName;
            }

            component = transform.GetComponent<Slider>();
            if (component != null)
            {
                return typeof(Slider).FullName;
            }

            component = transform.GetComponent<RawImage>();
            if (component != null)
            {
                return typeof(RawImage).FullName;
            }

            component = transform.GetComponent<UIImage>();
            if (component != null)
            {
                return typeof(UIImage).FullName;
            }

            component = transform.GetComponent<Image>();
            if (component != null)
            {
                return typeof(Image).FullName;
            }

            return typeof(Transform).FullName;
        }

        /// <summary>
        /// 递归判断文件夹是否存在，不存在则创建
        /// </summary>
        /// <param name="path">要检查和创建的文件夹路径</param>
        private static void CreateFoldersIfNotExist(string path)
        {
            // 获取文件夹的父目录路径
            string parentPath = Path.GetDirectoryName(path);

            // 如果父目录路径不为空且不存在，则先递归创建父目录
            if (!string.IsNullOrEmpty(parentPath) && !Directory.Exists(parentPath))
            {
                CreateFoldersIfNotExist(parentPath);
            }

            // 如果当前文件夹路径不存在，则创建当前文件夹
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}