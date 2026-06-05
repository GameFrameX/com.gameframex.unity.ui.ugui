<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="GameFrameX Logo" width="160" height="160" />

# Game Frame X UI UGUI

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.ui.ugui)](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.ui.ugui)](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/releases)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

[Documentation](https://gameframex.doc.alianblank.com) | [Quick Start](https://gameframex.doc.alianblank.com) | [QQ Group](https://qm.qq.com/q/urKenB9AU)

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

Game Frame X UGUI Component - Unity UI package providing UGUI component wrappers for simpler and more efficient usage.

## Features

### Core Functionality

- **UGUI Component Wrappers**: High-level wrappers for Unity UGUI components
- **UI Manager**: Complete UI form management system
- **Code Generator**: Automatic UI code generation for improved development efficiency
- **Extension Methods**: Rich UGUI component extension methods
- **Form Helpers**: UI form creation and management helper tools

### Main Components

#### UI Management System
- `UIManager`: Form manager handling UI open/close and lifecycle management
- `UGUI`: Abstract UI base class providing UI visibility state control
- `UGUIFormHelper`: UI form helper handling UI instantiation and creation

#### Extension Features
- `UGUIButtonExtension`: Button component extension methods
- `UGUIImageExtension`: Image component extension methods
- `RectTransformExtension`: RectTransform extension methods
- `UIImage`: Enhanced Image component with async image loading support

#### Editor Tools
- `UGUICodeGenerator`: UGUI code generator
- `UGUIComponentInspector`: UGUI component inspector
- `UIImageReplaceHandler`: UI image replacement handler

## Installation

### Dependencies

```json
{
  "com.gameframex.unity": "1.1.1",
  "com.gameframex.unity.ui": "1.0.0",
  "com.gameframex.unity.asset": "1.0.6",
  "com.gameframex.unity.event": "1.0.0"
}
```

### Installation Methods (choose one)

1. **Package Manager (Recommended)**
   - Open Unity Editor
   - Open Package Manager window
   - Click the "+" button and select "Add package from git URL"
   - Enter: `https://github.com/gameframex/com.gameframex.unity.ui.ugui.git`

2. **manifest.json**
   - Add the following to the `dependencies` section of your `manifest.json` file
   ```json
   {"com.gameframex.unity.ui.ugui": "https://github.com/gameframex/com.gameframex.unity.ui.ugui.git"}
   ```

3. **Local Installation**
   - Download the repository and place it in your Unity project's `Packages` directory. It will be auto-detected.

## Quick Start

### 1. Basic UI Class Creation

```csharp
using GameFrameX.UI.UGUI.Runtime;
using UnityEngine;

public class MainMenuUI : UGUI
{
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        // Initialize UI logic
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        // Logic when UI opens
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        // Logic when UI closes
    }
}
```

### 2. Using Extension Methods

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
        // Button extension method
        startButton.onClick.Add(OnStartButtonClick);

        // Image extension method
        iconImage.SetIcon("UI/Icons/StartIcon");

        // RectTransform extension method
        panel.MakeFullScreen();
    }

    private void OnStartButtonClick()
    {
        Debug.Log("Start button clicked!");
    }
}
```

### 3. Using the Code Generator

1. Select a UGUI prefab in the Hierarchy
2. Right-click and select `GameObject/UI/Generate UGUI Code`
3. Code will be auto-generated in the `Assets/Hotfix/UI/UGUI/` directory

### 4. Using UIImage Component

```csharp
using GameFrameX.UI.UGUI.Runtime;

public class IconDisplay : MonoBehaviour
{
    [SerializeField] private UIImage iconImage;

    void Start()
    {
        // Set icon with async loading support
        iconImage.icon = "UI/Icons/PlayerAvatar";
    }
}
```

## License

See [LICENSE.md](LICENSE.md) for license information.
