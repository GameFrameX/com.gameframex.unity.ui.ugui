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

using GameFrameX.Runtime;
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