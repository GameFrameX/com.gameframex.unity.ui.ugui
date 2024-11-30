using GameFrameX.Asset.Runtime;
using GameFrameX.Runtime;
using UnityEngine;

namespace GameFrameX.UI.UGUI.Runtime
{
    /// <summary>
    /// UGUI图片扩展
    /// </summary>
    public static class UGUIImageExtension
    {
        /// <summary>
        /// 设置Icon
        /// </summary>
        /// <param name="self"></param>
        /// <param name="icon">icon地址</param>
        public static async void SetIcon(this UnityEngine.UI.Image self, string icon)
        {
            var assetComponent = GameEntry.GetComponent<AssetComponent>();
            var valueHandle = await assetComponent.LoadAssetAsync<Texture2D>(icon);
            if (valueHandle.IsSucceed)
            {
                var texture2D = valueHandle.GetAssetObject<Texture2D>();
                self.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
            }
        }
    }
}