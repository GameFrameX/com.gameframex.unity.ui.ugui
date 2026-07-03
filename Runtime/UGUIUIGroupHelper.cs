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

using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace GameFrameX.UI.UGUI.Runtime
{
    /// <summary>
    /// UGUI界面组辅助器。
    /// </summary>
    /// <remarks>
    /// UGUI UI group helper that manages UI group depth and hierarchy.
    /// </remarks>
    [Preserve]
    public sealed class UGUIUIGroupHelper : UIGroupHelperBase
    {
        /// <summary>
        /// 获取界面组深度。
        /// </summary>
        /// <remarks>
        /// Gets the UI group depth.
        /// </remarks>
        /// <value>界面组深度 / UI group depth</value>
        [UnityEngine.Scripting.Preserve]
        public override int Depth { get; protected set; }

        /// <summary>
        /// 设置界面组深度。
        /// </summary>
        /// <remarks>
        /// Sets the UI group depth and updates the local position accordingly.
        /// </remarks>
        /// <param name="depth">界面组深度 / UI group depth</param>
        [UnityEngine.Scripting.Preserve]
        public override void SetDepth(int depth)
        {
            Depth = depth;
            transform.localPosition = new Vector3(0, 0, depth * 1000);
        }


        /// <summary>
        /// 创建界面组。
        /// </summary>
        /// <remarks>
        /// Creates a UI group with the specified parameters.
        /// </remarks>
        /// <param name="root">根节点 / Root transform</param>
        /// <param name="groupName">界面组名称 / UI group name</param>
        /// <param name="uiGroupHelperTypeName">界面组辅助器类型名 / UI group helper type name</param>
        /// <param name="customUIGroupHelper">自定义的界面组辅助器 / Custom UI group helper</param>
        /// <param name="depth">界面组深度 / UI group depth</param>
        /// <returns>界面组辅助器实例 / UI group helper instance</returns>
        [UnityEngine.Scripting.Preserve]
        public override IUIGroupHelper Handler(Transform root, string groupName, string uiGroupHelperTypeName, IUIGroupHelper customUIGroupHelper, int depth = 0)
        {
            SetDepth(depth);
            root = EnsureRuntimeRoot(root);
            if (!root)
            {
                Log.Error("UGUI runtime root is invalid.");
                return null;
            }

            GameObject component = new GameObject(groupName);
            var comName = groupName;
            component.name = comName;
            component.transform.SetParent(root, false);
            var uiLayer = LayerMask.NameToLayer("UI");
            component.SetLayerRecursively(uiLayer);

            RectTransform rectTransform = component.GetOrAddComponent<RectTransform>();
            rectTransform.MakeFullScreen();
            // var canvas = component.AddComponent<Canvas>();
            // // canvas.pixelPerfect = true;
            // // canvas.overridePixelPerfect = true;
            // canvas.sortingLayerID = uiLayer;
            // canvas.sortingLayerName = "UI";
            // canvas.overrideSorting = true;
            // canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.Normal | AdditionalCanvasShaderChannels.Tangent | AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.TexCoord2 | AdditionalCanvasShaderChannels.TexCoord3;
            var uiGroupHelper = Helper.CreateHelper(component, uiGroupHelperTypeName, (UIGroupHelperBase)customUIGroupHelper, 0);
            return uiGroupHelper;
        }

        private static Transform EnsureRuntimeRoot(Transform root)
        {
            if (!TryGetGameObject(root, out var rootObject))
            {
                Log.Error("UGUI root transform is invalid.");
                return null;
            }

            var designResolution = root.GetComponentInParent<UIComponent>(true)?.DesignResolution;
            var uiLayer = LayerMask.NameToLayer("UI");

            rootObject.SetLayerRecursively(uiLayer);

            var uiCamera = EnsureCamera(root, uiLayer);
            if (uiCamera == null)
            {
                Log.Error("UGUI camera is invalid.");
                return null;
            }

            var canvasRoot = EnsureCanvas(root, uiCamera, designResolution, uiLayer);
            if (!canvasRoot)
            {
                Log.Error("UGUI canvas is invalid.");
                return null;
            }

            EnsureEventSystem(root);

            return canvasRoot;
        }

        private static Camera EnsureCamera(Transform root, int uiLayer)
        {
            var cameraTransform = EnsureChild(root, "UGUICamera");
            if (!TryGetGameObject(cameraTransform, out var cameraObject))
            {
                return null;
            }

            cameraObject.SetLayerRecursively(uiLayer);

            var uiCamera = cameraObject.GetOrAddComponent<Camera>();
            uiCamera.clearFlags = CameraClearFlags.Depth;
            uiCamera.orthographic = true;
            uiCamera.orthographicSize = 5f;
            uiCamera.nearClipPlane = -500f;
            uiCamera.farClipPlane = 500f;
            uiCamera.depth = 10f;
            uiCamera.cullingMask = uiLayer >= 0 ? 1 << uiLayer : uiCamera.cullingMask;
#if UNITY_5_4_OR_NEWER
            uiCamera.stereoTargetEye = StereoTargetEyeMask.None;
#endif
#if UNITY_5_6_OR_NEWER
            uiCamera.allowHDR = false;
            uiCamera.allowMSAA = false;
#endif
            return uiCamera;
        }

        private static Transform EnsureCanvas(Transform root, Camera uiCamera, UIDesignResolutionComponent designResolution, int uiLayer)
        {
            if (uiCamera == null)
            {
                return null;
            }

            var canvasTransform = EnsureChild(root, "UGUICanvas", true);
            if (!TryGetGameObject(canvasTransform, out var canvasObject))
            {
                return null;
            }

            canvasObject.SetLayerRecursively(uiLayer);

            var rectTransform = canvasObject.GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                return null;
            }

            rectTransform.MakeFullScreen();

            var canvas = canvasObject.GetOrAddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = uiCamera;
            canvas.planeDistance = 100f;
            canvas.overrideSorting = true;
            canvas.sortingOrder = 0;
            canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.Normal |
                                              AdditionalCanvasShaderChannels.Tangent |
                                              AdditionalCanvasShaderChannels.TexCoord1 |
                                              AdditionalCanvasShaderChannels.TexCoord2 |
                                              AdditionalCanvasShaderChannels.TexCoord3;

            ApplyDesignResolution(canvasObject.GetOrAddComponent<CanvasScaler>(), designResolution);
            canvasObject.GetOrAddComponent<GraphicRaycaster>();

            return canvasTransform;
        }

        private static void ApplyDesignResolution(CanvasScaler scaler, UIDesignResolutionComponent designResolution)
        {
            if (designResolution == null)
            {
                return;
            }

            scaler.referencePixelsPerUnit = designResolution.ReferencePixelsPerUnit;
            switch (designResolution.ScaleMode)
            {
                case UIDesignResolutionComponent.UIScaleMode.ConstantPixelSize:
                    scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
                    scaler.scaleFactor = designResolution.ConstantScaleFactor;
                    break;
                case UIDesignResolutionComponent.UIScaleMode.ConstantPhysicalSize:
                    scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPhysicalSize;
                    scaler.fallbackScreenDPI = designResolution.FallbackScreenDPI;
                    scaler.defaultSpriteDPI = designResolution.DefaultSpriteDPI;
                    break;
                default:
                    scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                    scaler.referenceResolution = new Vector2(designResolution.DesignWidth, designResolution.DesignHeight);
                    scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                    scaler.matchWidthOrHeight = GetMatchWidthOrHeight(designResolution);
                    break;
            }
        }

        private static float GetMatchWidthOrHeight(UIDesignResolutionComponent designResolution)
        {
            switch (designResolution.ScreenMatchMode)
            {
                case UIDesignResolutionComponent.UIScreenMatchMode.MatchWidth:
                    return 0f;
                case UIDesignResolutionComponent.UIScreenMatchMode.MatchHeight:
                    return 1f;
                default:
                    return designResolution.MatchWidthOrHeight;
            }
        }

        private static void EnsureEventSystem(Transform root)
        {
            if (EventSystem.current != null)
            {
                return;
            }

            if (!TryGetGameObject(root, out _))
            {
                return;
            }

            var eventSystemObject = new GameObject("EventSystem");
            eventSystemObject.transform.SetParent(root, false);
            eventSystemObject.GetOrAddComponent<EventSystem>();
            eventSystemObject.GetOrAddComponent<StandaloneInputModule>();
        }


        private static Transform EnsureChild(Transform root, string name)
        {
            return EnsureChild(root, name, false);
        }

        private static Transform EnsureChild(Transform root, string name, bool requireRectTransform)
        {
            if (!TryGetGameObject(root, out _))
            {
                return null;
            }

            var child = root.Find(name);
            if (TryGetGameObject(child, out _))
            {
                if (!requireRectTransform || child.GetComponent<RectTransform>() != null)
                {
                    return child;
                }

                child.name = Utility.Text.Format("{0} (Invalid)", name);
                UnityEngine.Object.Destroy(child.gameObject);
            }

            GameObject childObject;
            if (requireRectTransform)
            {
                childObject = new GameObject(name);
                childObject.GetOrAddComponent<RectTransform>();
            }
            else
            {
                childObject = new GameObject(name);
            }

            child = childObject.transform;
            child.SetParent(root, false);
            if (TryGetGameObject(child, out _))
            {
                return child;
            }

            return null;
        }

        private static bool TryGetGameObject(Transform transform, out GameObject gameObject)
        {
            gameObject = null;
            if (!transform)
            {
                return false;
            }

            try
            {
                gameObject = transform.gameObject;
                return gameObject != null;
            }
            catch (MissingReferenceException)
            {
                return false;
            }
        }
    }
}