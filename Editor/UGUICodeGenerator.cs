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

using System;
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
    /// UGUI代码生成器,用于自动生成UI代码文件
    /// </summary>
    internal static class UGUICodeGenerator
    {
        /// <summary>
        /// 生成UI代码的菜单项
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
                        Generate(selectedObject);
                        return;
                    }
                }
            }

            Debug.LogError("请选择一个有效的UGUI预制体进行操作");
        }

        /// <summary>
        /// UI控件类型转换处理器列表
        /// </summary>
        private static List<IUGUIGeneratorCodeConvertTypeHandler> _handler;

        /// <summary>
        /// 生成UI代码的主要方法
        /// </summary>
        /// <param name="selectedObject">选中的游戏对象</param>
        internal static void Generate(GameObject selectedObject)
        {
            var assetPath = AssetDatabase.GetAssetPath(selectedObject);

            if (assetPath.IsNullOrWhiteSpace())
            {
                assetPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(selectedObject);
            }

            // 获取所有实现了IUGUICodeConvertType接口的类型
            var types = Utility.Assembly.GetTypes();
            _handler = new List<IUGUIGeneratorCodeConvertTypeHandler>();
            foreach (var type in types)
            {
                if (type.IsImplWithInterface(typeof(IUGUIGeneratorCodeConvertTypeHandler)))
                {
                    var handler = (IUGUIGeneratorCodeConvertTypeHandler)Activator.CreateInstance(type);
                    _handler.Add(handler);
                }
            }

            // 按优先级排序处理器
            if (_handler.Count > 0)
            {
                _handler.Sort((x, y) => x.Priority.CompareTo(y.Priority));
            }

            // 设置代码生成路径
            string className = selectedObject.name;
            string savePath;
            if (assetPath.Contains(nameof(Resources)))
            {
                savePath = System.IO.Path.Combine(Application.dataPath, "Scripts", "Game", "UGUI", className);
            }
            else
            {
                savePath = System.IO.Path.Combine(Application.dataPath, "Hotfix", "UI", "UGUI", className);
            }

            CreateFoldersIfNotExist(savePath);

            // 生成代码并保存
            var codeString = GenerateCode(selectedObject, assetPath);
            string filePath = Path.Combine(savePath, className + ".UI.cs");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, codeString, Encoding.UTF8);
            AssetDatabase.Refresh();

            Debug.Log("生成UI代码完成 文件路径:" + filePath);
            Debug.Log("现在请将生成的代码挂载到当前的UGUI预制体上,然后点击重新绑定列表");
            return;
        }

        /// <summary>
        /// 生成具体的代码内容
        /// </summary>
        /// <param name="selectedObject">选中的游戏对象</param>
        /// <returns>生成的代码字符串</returns>
        private static string GenerateCode(GameObject selectedObject, string assetPath)
        {
            StringBuilder codeBuilder = new StringBuilder();
            string className = selectedObject.name;

            // 生成代码头部
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

            // 生成命名空间和类定义
            if (assetPath.Contains(nameof(Resources)))
            {
                codeBuilder.AppendLine("namespace Unity.Startup");
            }
            else
            {
                codeBuilder.AppendLine("namespace Hotfix.UI");
            }

            codeBuilder.AppendLine("{");
            codeBuilder.AppendLine("\t/// <summary>");
            codeBuilder.AppendLine($"\t/// 代码生成的UI代码{className}");
            codeBuilder.AppendLine("\t/// </summary>");
            codeBuilder.AppendLine("\t[DisallowMultipleComponent]");
            if (!assetPath.Contains(nameof(Resources)))
            {
                if (assetPath.IsNotNullOrWhiteSpace())
                {
                    codeBuilder.AppendLine($"\t[OptionUIConfig(null, \"{assetPath.Substring(0, assetPath.LastIndexOf('/'))}\")]");
                }
            }

            codeBuilder.AppendLine($"\tpublic sealed partial class {className} : UGUI");
            codeBuilder.AppendLine("\t{");

            // 生成self属性
            codeBuilder.AppendLine("\t\tpublic GameObject self { get; private set; }");
            codeBuilder.AppendLine();

            // 处理所有UI节点
            List<NodeInfo> nodeInfos = new List<NodeInfo>();
            PropertyHandler(selectedObject, null, nodeInfos, true);
            PropertyCodeHandler(codeBuilder, nodeInfos);

            // 生成初始化方法
            codeBuilder.AppendLine("\t\tprivate bool _isInitView = false;");
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("\t\tprotected override void InitView()");
            codeBuilder.AppendLine("\t\t{");
            codeBuilder.AppendLine("\t\t\tif (_isInitView)");
            codeBuilder.AppendLine("\t\t\t{");
            codeBuilder.AppendLine("\t\t\t\treturn;");
            codeBuilder.AppendLine("\t\t\t}");
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("\t\t\t_isInitView = true;");
            codeBuilder.AppendLine("\t\t\tthis.self = this.gameObject;");
            PropertyInitCodeHandler(codeBuilder, nodeInfos);
            codeBuilder.AppendLine("\t\t}");

            codeBuilder.AppendLine("\t}");
            codeBuilder.AppendLine("}");
            codeBuilder.AppendLine("#endif");
            return codeBuilder.ToString().Replace("\r\n", "\n");
        }

        /// <summary>
        /// 处理节点路径,生成完整的节点路径字符串
        /// </summary>
        /// <param name="nodeInfo">节点信息</param>
        /// <returns>完整的节点路径</returns>
        private static string PathHandler(NodeInfo nodeInfo)
        {
            if (nodeInfo.Parent == null)
            {
                return nodeInfo.Path;
            }

            string path = PathHandler(nodeInfo.Parent) + "/" + nodeInfo.Path;
            return path;
        }

        /// <summary>
        /// 生成属性相关的代码
        /// </summary>
        /// <param name="codeBuilder">代码构建器</param>
        /// <param name="nodeInfos">节点信息列表</param>
        private static void PropertyCodeHandler(StringBuilder codeBuilder, List<NodeInfo> nodeInfos)
        {
            codeBuilder.AppendLine("\t\t#region Properties");
            codeBuilder.AppendLine();

            foreach (var nodeInfo in nodeInfos)
            {
                string path = PathHandler(nodeInfo);
                codeBuilder.AppendLine($"\t\t[SerializeField] [UGUIElementProperty(\"{path}\")]");
                codeBuilder.AppendLine($"\t\tprivate {nodeInfo.Type} {nodeInfo.Name};");
                codeBuilder.AppendLine();

                codeBuilder.AppendLine($"\t\tpublic {nodeInfo.Type} m_{nodeInfo.Name}");
                codeBuilder.AppendLine($"\t\t{{");
                codeBuilder.AppendLine($"\t\t\tget {{ return {nodeInfo.Name};}}");
                codeBuilder.AppendLine($"\t\t}}");
                codeBuilder.AppendLine();
            }

            codeBuilder.AppendLine("\t\t#endregion");
            codeBuilder.AppendLine();
        }

        /// <summary>
        /// 生成属性相关的代码
        /// </summary>
        /// <param name="codeBuilder">代码构建器</param>
        /// <param name="nodeInfos">节点信息列表</param>
        private static void PropertyInitCodeHandler(StringBuilder codeBuilder, List<NodeInfo> nodeInfos)
        {
            foreach (var nodeInfo in nodeInfos)
            {
                string path = PathHandler(nodeInfo);
                codeBuilder.AppendLine($"\t\t\t{nodeInfo.Name} = gameObject.transform.FindChildName(\"{path}\").GetComponent<{nodeInfo.Type}>();");
            }
        }

        /// <summary>
        /// 处理UI预制体的属性,递归遍历所有子节点
        /// </summary>
        /// <param name="selectedObject">选中的游戏对象</param>
        /// <param name="parentInfo">父节点信息</param>
        /// <param name="nodeInfos">节点信息列表</param>
        /// <param name="isRoot">是否为根节点</param>
        /// <returns>是否成功处理</returns>
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
                    name = $"{parentInfo.Name}_{name}";
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

        /// <summary>
        /// UI节点信息类,用于存储节点的各种属性
        /// </summary>
        sealed class NodeInfo
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

            /// <summary>
            /// 节点对应的GameObject
            /// </summary>
            public GameObject GameObject { get; set; }

            /// <summary>
            /// 节点的Transform组件
            /// </summary>
            public Transform Transform { get; set; }

            /// <summary>
            /// 父节点信息
            /// </summary>
            public NodeInfo Parent { get; set; }
        }

        /// <summary>
        /// UI控件类型转换,将Transform转换为对应的UI控件类型
        /// </summary>
        /// <param name="transform">要转换的Transform</param>
        /// <returns>控件类型的完整名称</returns>
        private static string ConvertType(Transform transform)
        {
            // 首先使用自定义处理器进行处理
            if (_handler != null)
            {
                foreach (var handler in _handler)
                {
                    var type = handler.Run(transform);
                    if (type != null)
                    {
                        return type;
                    }
                }
            }

            // 依次检查是否包含各种UI组件
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