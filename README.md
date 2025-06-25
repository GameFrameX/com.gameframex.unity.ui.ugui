# Game Frame X UI UGUI

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Unity](https://img.shields.io/badge/Unity-2019.4+-green.svg)](https://unity3d.com)
[![Version](https://img.shields.io/badge/version-2.0.0-orange.svg)](package.json)

独立游戏前后端一体化解决方案，独立游戏开发者的圆梦大使。

Game Frame X UGUI 组件 - Unity UI功能包，提供UGUI组件的封装，使UGUI组件的使用更加简单高效。

## 📖 文档

- [官方文档](https://gameframex.doc.alianblank.com)
- [GitHub仓库](https://github.com/gameframex/com.gameframex.unity.ui.ugui)

## ✨ 特性

### 🎯 核心功能

- **UGUI组件封装**: 提供对Unity UGUI组件的高级封装
- **UI管理器**: 完整的UI界面管理系统
- **代码生成器**: 自动生成UI代码，提高开发效率
- **扩展方法**: 丰富的UGUI组件扩展方法
- **表单辅助**: UI表单创建和管理辅助工具

### 🛠️ 主要组件

#### UI管理系统
- `UIManager`: 界面管理器，负责UI的打开、关闭和生命周期管理
- `UGUI`: 抽象UI基类，提供UI显示状态控制
- `UGUIFormHelper`: UI表单辅助器，处理UI实例化和创建

#### 扩展功能
- `UGUIButtonExtension`: Button组件扩展方法
- `UGUIImageExtension`: Image组件扩展方法
- `RectTransformExtension`: RectTransform扩展方法
- `UIImage`: 增强的Image组件，支持异步图片加载

#### 编辑器工具
- `UGUICodeGenerator`: UGUI代码生成器
- `UGUIComponentInspector`: UGUI组件检查器
- `UIImageReplaceHandler`: UI图片替换处理器

## 📦 安装

### 依赖项

```json
{
  "com.gameframex.unity": "1.1.1",
  "com.gameframex.unity.ui": "1.0.0",
  "com.gameframex.unity.asset": "1.0.6",
  "com.gameframex.unity.event": "1.0.0"
}
```

### 使用方式(任选其一)

1. **Package Manager (推荐)**
   - 打开Unity编辑器
   - 打开Package Manager窗口
   - 点击"+"按钮，选择"Add package from git URL"
   - 输入：`https://github.com/gameframex/com.gameframex.unity.ui.ugui.git`

2. **manifest.json**
   - 直接在 `manifest.json` 的文件中的 `dependencies` 节点下添加以下内容
   ```json
   {"com.gameframex.unity.ui.ugui": "https://github.com/gameframex/com.gameframex.unity.ui.ugui.git"}
   ```

3. **本地安装**
   - 直接下载仓库放置到Unity项目的`Packages`目录下，会自动加载识别

## 🚀 快速开始

### 1. 基本UI类创建

```csharp
using GameFrameX.UI.UGUI.Runtime;
using UnityEngine;

public class MainMenuUI : UGUI
{
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        // 初始化UI逻辑
    }
    
    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        // UI打开时的逻辑
    }
    
    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        // UI关闭时的逻辑
    }
}
```

### 2. 使用扩展方法

```csharp
using GameFrameX.UI.UGUI.Runtime;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Image iconImage;
    [SerializeField] private RectTransform panel;
    
    void Start()
    {
        // 按钮扩展方法
        startButton.onClick.Add(OnStartButtonClick);
        
        // 图片扩展方法
        iconImage.SetIcon("UI/Icons/StartIcon");
        
        // RectTransform扩展方法
        panel.MakeFullScreen();
    }
    
    private void OnStartButtonClick()
    {
        Debug.Log("Start button clicked!");
    }
}
```

### 3. 使用代码生成器

1. 在Hierarchy中选择一个UGUI预制体
2. 右键选择 `GameObject/UI/Generate UGUI Code(生成UGUI代码)`
3. 代码将自动生成到 `Assets/Hotfix/UI/UGUI/` 目录下

### 4. 使用UIImage组件

```csharp
using GameFrameX.UI.UGUI.Runtime;

public class IconDisplay : MonoBehaviour
{
    [SerializeField] private UIImage iconImage;
    
    void Start()
    {
        // 设置图标，支持异步加载
        iconImage.icon = "UI/Icons/PlayerAvatar";
    }
}
```

## 📋 API参考

### UGUI基类

```csharp
public abstract class UGUI : UIForm
{
    // 设置UI显示状态
    protected override void InternalSetVisible(bool value);
    
    // 获取或设置UI可见性
    public override bool Visible { get; protected set; }
}
```

### 扩展方法

#### Button扩展
```csharp
public static class UGUIButtonExtension
{
    // 添加点击事件
    public static void Add(this Button.ButtonClickedEvent self, UnityAction action);
    
    // 移除点击事件
    public static void Remove(this Button.ButtonClickedEvent self, UnityAction action);
    
    // 清除所有点击事件
    public static void Clear(this Button.ButtonClickedEvent self);
    
    // 设置点击事件（清除后设置）
    public static void Set(this Button.ButtonClickedEvent self, UnityAction action);
}
```

#### Image扩展
```csharp
public static class UGUIImageExtension
{
    // 异步设置图标
    public static async void SetIcon(this UnityEngine.UI.Image self, string icon);
}
```

#### RectTransform扩展
```csharp
public static class RectTransformExtension
{
    // 设置为全屏
    public static void MakeFullScreen(this RectTransform rectTransform);
}
```

## 🔧 配置

### 代码生成器配置

代码生成器会自动扫描实现了 `IUGUIGeneratorCodeConvertTypeHandler` 接口的处理器，并按优先级排序执行。

```csharp
public interface IUGUIGeneratorCodeConvertTypeHandler
{
    int Priority { get; }  // 优先级，数值越小优先级越高
    // 其他接口方法...
}
```

## 🤝 贡献

欢迎提交Issue和Pull Request来帮助改进这个项目。

## 📄 许可证

本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。

## 👥 作者

- **Blank** - [alianblank@outlook.com](mailto:alianblank@outlook.com)
- 项目主页: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)

## 🔗 相关链接

- [Game Frame X 主框架](https://github.com/gameframex/com.gameframex.unity)
- [Game Frame X UI 基础包](https://github.com/gameframex/com.gameframex.unity.ui)
- [Game Frame X 资源管理](https://github.com/gameframex/com.gameframex.unity.asset)
- [Game Frame X 事件系统](https://github.com/gameframex/com.gameframex.unity.event)

---

**注意**: 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！