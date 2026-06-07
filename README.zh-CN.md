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

## 快速开始

### 安装

选择以下任一方式：

1. 编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
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
     ],
     "dependencies": {
       "com.gameframex.unity.ui.ugui": "2.5.1"
     }
   }
   ```

   `scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

2. 直接在 `manifest.json` 的 `dependencies` 节点下添加以下内容：
   ```json
   {
      "com.gameframex.unity.ui.ugui": "https://github.com/gameframex/com.gameframex.unity.ui.ugui.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式添加库，地址为：`https://github.com/gameframex/com.gameframex.unity.ui.ugui.git`
4. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下，会自动加载识别。
## 依赖

| 包 | 说明 |
|----|------|
| `com.gameframex.unity` | 1.1.1 |
| `com.gameframex.unity.asset` | 1.0.6 |
| `com.gameframex.unity.event` | 1.0.0 |
| `com.gameframex.unity.ui` | 1.0.0 |

## 文档与资源

- [官方文档](https://gameframex.doc.alianblank.com)

## 社区与支持

- QQ群: 467608841 / 233840761

## 更新日志

查看 [Releases](https://github.com/GameFrameX/gameframex/com.gameframex.unity.ui.ugui/releases) 了解更新日志。
## 开源协议

详见 [LICENSE.md](LICENSE.md) 文件。
