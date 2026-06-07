<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X UI UGUI

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.ui.ugui)](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.ui.ugui)](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

<br />

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#quick-start) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>
## 特性

### 核心功能

- **UGUI组件封装**: 提供对Unity UGUI组件的高级封装
- **UI管理器**: 完整的UI界面管理系统
- **代码生成器**: 自动生成UI代码，提高开发效率
- **扩展方法**: 丰富的UGUI组件扩展方法
- **表单辅助**: UI表单创建和管理辅助工具

### 主要组件

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

## 安装

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

## 快速开始

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

## 开源协议

详见 [LICENSE.md](LICENSE.md) 文件。
