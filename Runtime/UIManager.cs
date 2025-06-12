// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using GameFrameX.UI.Runtime;

namespace GameFrameX.UI.UGUI.Runtime
{
    /// <summary>
    /// 界面管理器。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    internal sealed partial class UIManager : BaseUIManager
    {
        /// <summary>
        /// 初始化界面管理器的新实例。
        /// </summary>
        public UIManager()
        {
            m_AssetManager = null;
            m_InstancePool = null;
            m_UIFormHelper = null;
            m_Serial = 0;
            m_RecycleTime = 0;
            if (m_RecycleInterval < 10)
            {
                m_RecycleInterval = 10;
            }

            m_IsShutdown = false;
            m_OpenUIFormSuccessEventHandler = null;
            m_OpenUIFormFailureEventHandler = null;
            // m_OpenUIFormUpdateEventHandler = null;
            // m_OpenUIFormDependencyAssetEventHandler = null;
            m_CloseUIFormCompleteEventHandler = null;
        }

        /*/// <summary>
        /// 获取或设置界面实例对象池的优先级。
        /// </summary>
        public int InstancePriority
        {
            get { return m_InstancePool.Priority; }
            set { m_InstancePool.Priority = value; }
        }*/
    }
}