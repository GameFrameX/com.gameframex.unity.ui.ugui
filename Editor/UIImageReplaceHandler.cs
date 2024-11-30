using GameFrameX.UI.UGUI.Runtime;
using UnityEditor;
using UnityEngine;

namespace GameFrameX.UI.UGUI.Editor
{
    [CustomEditor(typeof(UnityEngine.UI.Image))]
    public class UIImageReplaceHandler : UnityEditor.Editor
    {
        [MenuItem("CONTEXT/Image/Replace To UIImage(替换为UIImage)", false, 10)]
        static void Run()
        {
            var image = Selection.activeGameObject.GetComponent<UnityEngine.UI.Image>();
            var imageType = image.type;
            var material = image.material;
            var sprite = image.sprite;
            var type = image.type;
            var fillCenter = image.fillCenter;
            var fillMethod = image.fillMethod;
            var fillAmount = image.fillAmount;
            var alphaHitTestMinimumThreshold = image.alphaHitTestMinimumThreshold;
            var useSpriteMesh = image.useSpriteMesh;
            var overrideSprite = image.overrideSprite;
            var color = image.color;
            var raycastTarget = image.raycastTarget;
            var maskable = image.maskable;

            DestroyImmediate(image);

            var uiImage = Selection.activeGameObject.GetOrAddComponent<UIImage>();
            uiImage.type = imageType;
            uiImage.material = material;
            uiImage.sprite = sprite;
            uiImage.type = type;
            uiImage.fillCenter = fillCenter;
            uiImage.fillMethod = fillMethod;
            uiImage.fillAmount = fillAmount;
            uiImage.alphaHitTestMinimumThreshold = alphaHitTestMinimumThreshold;
            uiImage.useSpriteMesh = useSpriteMesh;
            uiImage.overrideSprite = overrideSprite;
            uiImage.color = color;
            uiImage.raycastTarget = raycastTarget;
            uiImage.maskable = maskable;
        }
    }
}