// ==========================================================================================
//   GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//   GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//   均受中华人民共和国及相关国际法律法规保护。
//   are protected by the laws of the People's Republic of China and relevant international regulations.
//   使用本项目须严格遵守相应法律法规及开源许可证之规定。
//   Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
//   本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//   This project is dual-licensed under the MIT License and Apache License 2.0,
//   完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//   please refer to the LICENSE file in the root directory of the source code for the full license text.
//   禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//   It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//   侵犯他人合法权益等法律法规所禁止的行为！
//   or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//   因基于本项目二次开发所产生的一切法律纠纷与责任，
//   Any legal disputes and liabilities arising from secondary development based on this project
//   本项目组织与贡献者概不承担。
//   shall be borne solely by the developer; the project organization and contributors assume no responsibility.
//   GitHub 仓库：https://github.com/GameFrameX
//   GitHub Repository: https://github.com/GameFrameX
//   Gitee  仓库：https://gitee.com/GameFrameX
//   Gitee Repository:  https://gitee.com/GameFrameX
//   CNB  仓库：https://cnb.cool/GameFrameX
//   CNB Repository:  https://cnb.cool/GameFrameX
//   官方文档：https://gameframex.doc.alianblank.com/
//   Official Documentation: https://gameframex.doc.alianblank.com/
//  ==========================================================================================

using UnityEngine;

namespace GameFrameX.UI.UGUI.Runtime
{
    /// <summary>
    /// 扩展的 UGUI 图片组件，支持通过资源路径设置图标。
    /// </summary>
    /// <remarks>
    /// Extended UGUI image component that supports setting icons via asset path.
    /// </remarks>
    [DisallowMultipleComponent]
    public class UIImage : UnityEngine.UI.Image
    {
        /// <summary>
        /// 图标资源路径的内部存储。
        /// </summary>
        /// <remarks>
        /// Internal storage for the icon asset path.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        private string m_icon { get; set; }

        /// <summary>
        /// 获取或设置图标资源路径。
        /// </summary>
        /// <remarks>
        /// Gets or sets the icon asset path. When set, automatically loads the icon from the asset system.
        /// </remarks>
        /// <value>图标资源路径 / Icon asset path</value>
        [UnityEngine.Scripting.Preserve]
        public string icon
        {
            get { return m_icon; }
            set
            {
                if (m_icon != value)
                {
                    m_icon = value;
                    _ = this.SetIconAsync(m_icon);
                }
            }
        }
    }
}