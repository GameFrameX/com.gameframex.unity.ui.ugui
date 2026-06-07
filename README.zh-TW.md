<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X UI UGUI

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.ui.ugui)](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.ui.ugui)](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

<br />

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#quick-start) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 功能特性

### 核心功能

- **UGUI 組件封裝**: 提供對 Unity UGUI 組件的高級封裝
- **UI 管理器**: 完整的 UI 界面管理系統
- **代碼生成器**: 自動生成 UI 代碼，提高開發效率
- **擴展方法**: 豐富的 UGUI 組件擴展方法
- **表單輔助**: UI 表單創建和管理輔助工具

### 主要組件

#### UI 管理系統
- `UIManager`: 界面管理器，負責 UI 的打開、關閉和生命週期管理
- `UGUI`: 抽象 UI 基類，提供 UI 顯示狀態控制
- `UGUIFormHelper`: UI 表單輔助器，處理 UI 實例化和創建

#### 擴展功能
- `UGUIButtonExtension`: Button 組件擴展方法
- `UGUIImageExtension`: Image 組件擴展方法
- `RectTransformExtension`: RectTransform 擴展方法
- `UIImage`: 增強的 Image 組件，支持異步圖片加載

#### 編輯器工具
- `UGUICodeGenerator`: UGUI 代碼生成器
- `UGUIComponentInspector`: UGUI 組件檢查器
- `UIImageReplaceHandler`: UI 圖片替換處理器

## 快速開始

### 安裝

編輯 Unity 專案的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：

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

`scopes` 控制哪些套件透過此註冊表解析。只有以 `com.gameframex` 開頭的套件才會從這個註冊表取得。

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.ui.ugui": "2.5.1"
  }
}
```


## 依賴

| 套件 | 說明 |
|------|------|
| `com.gameframex.unity` | 1.1.1 |
| `com.gameframex.unity.asset` | 1.0.6 |
| `com.gameframex.unity.event` | 1.0.0 |
| `com.gameframex.unity.ui` | 1.0.0 |

## 文檔與資源

- [官方文檔](https://gameframex.doc.alianblank.com)

## 社區與支援

- QQ群: 467608841 / 233840761

## 更新日誌

查看 [Releases](https://github.com/GameFrameX/gameframex/com.gameframex.unity.ui.ugui/releases) 了解更新日誌。
## 開源協議

詳見 [LICENSE.md](LICENSE.md) 檔案。
