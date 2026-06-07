<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X UI UGUI

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.ui.ugui)](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.ui.ugui)](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

<br />

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · QQ Group: 467608841 / 233840761

<br />

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

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

### Installation

Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:

```json
{
  "scopedRegistries": [
    {
      "name": "GameFrameX",
      "url": "https://gameframex.upm.alianblank.uk",
      "scopes": [
        "com.gameframex"
      ]
    }
  ]
}
```

`scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex` will be fetched from it.

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.ui.ugui": "2.5.1"
  }
}
```


## License

See [LICENSE.md](LICENSE.md) for license information.
