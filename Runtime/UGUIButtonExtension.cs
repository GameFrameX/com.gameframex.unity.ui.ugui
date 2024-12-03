using GameFrameX.Runtime;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameFrameX.UI.UGUI.Runtime
{
    /// <summary>
    /// UGUI按钮扩展
    /// </summary>
    public static class UGUIButtonExtension
    {
        /// <summary>
        /// 添加按钮点击事件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="action">点击回调</param>
        public static void Add(this Button.ButtonClickedEvent self, UnityAction action)
        {
            GameFrameworkGuard.NotNull(action, nameof(action));
            self.AddListener(action);
        }

        /// <summary>
        /// 移除按钮点击事件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="action">点击回调</param>
        public static void Remove(this Button.ButtonClickedEvent self, UnityAction action)
        {
            GameFrameworkGuard.NotNull(action, nameof(action));
            self.RemoveListener(action);
        }

        /// <summary>
        /// 清除按钮点击事件
        /// </summary>
        /// <param name="self"></param>
        public static void Clear(this Button.ButtonClickedEvent self)
        {
            self.RemoveAllListeners();
        }

        /// <summary>
        /// 设置按钮点击事件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="action">点击回调</param>
        public static void Set(this Button.ButtonClickedEvent self, UnityAction action)
        {
            GameFrameworkGuard.NotNull(action, nameof(action));
            self.Clear();
            self.Add(action);
        }

        /// <summary>
        /// 设置按钮点击事件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="action">点击回调</param>
        /// <param name="userData">用户数据</param>
        public static void Set(this Button.ButtonClickedEvent self, UnityAction<object> action, object userData)
        {
            GameFrameworkGuard.NotNull(action, nameof(action));
            self.Clear();

            void Call()
            {
                action.Invoke(userData);
            }

            self.AddListener(Call);
        }
    }
}