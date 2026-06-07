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

## 安裝

### 依賴項

```json
{
  "com.gameframex.unity": "1.1.1",
  "com.gameframex.unity.ui": "1.0.0",
  "com.gameframex.unity.asset": "1.0.6",
  "com.gameframex.unity.event": "1.0.0"
}
```

### 安裝方式（任選其一）

1. **Package Manager (推薦)**
   - 打開 Unity 編輯器
   - 打開 Package Manager 窗口
   - 點擊"+"按鈕，選擇"Add package from git URL"
   - 輸入：`https://github.com/gameframex/com.gameframex.unity.ui.ugui.git`

2. **manifest.json**
   - 直接在 `manifest.json` 文件中的 `dependencies` 節點下添加以下內容
   ```json
   {"com.gameframex.unity.ui.ugui": "https://github.com/gameframex/com.gameframex.unity.ui.ugui.git"}
   ```

3. **本地安裝**
   - 直接下載倉庫放置到 Unity 項目的 `Packages` 目錄下，會自動加載識別

## 快速開始

### 1. 基本 UI 類創建

```csharp
using GameFrameX.UI.UGUI.Runtime;
using UnityEngine;

public class MainMenuUI : UGUI
{
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        // 初始化 UI 邏輯
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        // UI 打開時的邏輯
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        // UI 關閉時的邏輯
    }
}
```

## 開源協議

詳見 [LICENSE.md](LICENSE.md) 檔案。
