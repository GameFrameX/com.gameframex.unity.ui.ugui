## [2.3.6](https://github.com/gameframex/com.gameframex.unity.ui.ugui/compare/2.3.5...2.3.6) (2026-04-01)


### Bug Fixes

* **UGUI:** 禁用自动释放时跳过资源卸载 ([b52f2b5](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/b52f2b583b38d4750e4d953ffdc3a8ae386abb25))
* **UI:** 为Unity IL2CPP代码裁剪添加Preserve属性 ([0ee56cf](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/0ee56cf66e5c52a8b0500202f8f2871b776e04c6))


### Performance Improvements

* 异步加载图标以提升界面响应性能 ([6e15382](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/6e153825bedbb784fa511002687bbcdd6b2d24e8))

## [2.3.5](https://github.com/gameframex/com.gameframex.unity.ui.ugui/compare/2.3.4...2.3.5) (2026-03-27)


### Bug Fixes

* **UI:** 修复UI组助手为空时实例化崩溃并增加资源名参数 ([55c2a73](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/55c2a731c8499ebb4709d092c6ce4cad7d1484b2))
* **UI资源加载:** 增加Resources.Load失败时的错误处理 ([5b46177](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/5b46177daace1385c9ad59309b0aa3b6f473d9a4))
* 修正格式化字符串中的参数索引错误 ([3d5468e](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/3d5468e38757be995c40e21017f5665c80e0c93c))

## [2.3.4](https://github.com/gameframex/com.gameframex.unity.ui.ugui/compare/2.3.3...2.3.4) (2026-03-19)


### Bug Fixes

* **UGUICodeGenerator:** 公开类和方法并添加预制体验证 ([dfd5c03](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/dfd5c0308db28f6f8eb77025fbb749d190445ab2))

## [2.3.3](https://github.com/gameframex/com.gameframex.unity.ui.ugui/compare/2.3.2...2.3.3) (2026-03-19)


### Bug Fixes

* **UI:** 根据资源路径选择正确的资源卸载方式 ([fa0c5ee](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/fa0c5ee58cfdb7426524eedbe430c74de4d88e68))

## [2.3.2](https://github.com/gameframex/com.gameframex.unity.ui.ugui/compare/2.3.1...2.3.2) (2026-03-06)


### Bug Fixes

* **UI:** 修复UI资源释放时未使用正确路径的问题 ([6fb66c5](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/6fb66c577ad333ecc0799dde94aed17febaa078c))

## [2.3.1](https://github.com/gameframex/com.gameframex.unity.ui.ugui/compare/2.3.0...2.3.1) (2026-03-05)


### Bug Fixes

* 修正异步资源加载成功状态检查条件 ([ad6b92f](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/ad6b92f9f965a8cb63a9213b6235cf7a150cb636))

# [2.3.0](https://github.com/gameframex/com.gameframex.unity.ui.ugui/compare/2.2.2...2.3.0) (2026-03-05)


### Features

* **UI编辑器:** 添加对TextMeshPro组件的代码生成支持 ([245edda](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/245edda5dff518e037bbd756dac21fe9441ac659))

## [2.2.2](https://github.com/gameframex/com.gameframex.unity.ui.ugui/compare/2.2.1...2.2.2) (2026-03-05)


### Bug Fixes

* **代码生成器:** 修复资源路径为空时未添加OptionUIConfig特性 ([5c3cd88](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/5c3cd88081bd25121cd8b11743d03b7413234885))

## [2.2.1](https://github.com/gameframex/com.gameframex.unity.ui.ugui/compare/2.2.0...2.2.1) (2026-03-05)


### Bug Fixes

* **UI:** 修复UI组辅助器深度设置问题并优化创建逻辑 ([20e45ce](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/20e45ce925f15ce5d64dfb0817a6c26b27762bba))

# [2.2.0](https://github.com/gameframex/com.gameframex.unity.ui.ugui/compare/2.1.4...2.2.0) (2025-12-23)


### Bug Fixes

* **UGUIFormHelper:** 改进无效UI表单实例的错误日志信息 ([d10994e](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/d10994e9643a851ec10b5c835cbffee82307ee32))
* **UGUI:** 修复UI表单组分配逻辑错误 ([046b6af](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/046b6afe4653664eec4f4a54fca64313a0740b36))
* **UGUI:** 修复未设置UIForm的UIGroup问题 ([cc108b1](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/cc108b1ca748bbf45839bfa0e0331cb21cdce3aa))
* **UGUI:** 当未设置动画属性时使用默认动画配置 ([a8f7bc9](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/a8f7bc917089b1b0a1091507366adb6d9ea69877))
* **UIManager:** 使用对象池释放UI对象而非直接销毁 ([36eb439](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/36eb439d5ec4f8f857102464cba894ef0f4a3763))
* **UIManager:** 修复UI回收逻辑并添加回收至实例池方法 ([5a0d772](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/5a0d772bb0223e668c4e155329ea3884d74434b3))
* **UIManager:** 修复UI表单释放时未正确释放资源的问题 ([5f872e3](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/5f872e3949be2878fc977446c056589b37e9423a))
* **UIManager:** 修复UI表单释放时的重复获取问题 ([5364481](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/5364481768f1062061a683c6efc317c3be12240c))
* **UI资源管理:** 修复UI资源释放时未处理资源句柄的问题 ([f19b290](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/f19b2900f4951b93c085f2701c8e112dda5cea21))
* 在UIForm.Init调用中添加RecycleInterval参数 ([d094245](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/d09424506b89d9780b58d3be42d41ed033b4f6df))


### Features

* **UGUI:** 实现界面显示和隐藏的完整逻辑 ([ae30d91](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/ae30d913b4f2684ad5bb97b9554f67d4f27a2b97))
* **UIManager:** 添加异步加载UI表单的队列管理功能 ([68f4912](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/68f4912fc7c4bb7130a09e3dd990b2ee1e654752))
* **UI:** 添加UI表单显示和隐藏动画支持 ([43bf8f9](https://github.com/gameframex/com.gameframex.unity.ui.ugui/commit/43bf8f9eeaa4bac8a0401182189138097597a73e))

# Changelog

## [Unreleased](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/HEAD)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/2.1.3...HEAD)

**Closed issues:**

- 关于UGUI中InitView\(\)逻辑的调用时机问题 [\#2](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/issues/2)

## [2.1.3](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/2.1.3) (2025-10-27)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/2.1.2...2.1.3)

## [2.1.2](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/2.1.2) (2025-10-27)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/2.1.1...2.1.2)

## [2.1.1](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/2.1.1) (2025-10-19)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/2.1.0...2.1.1)

## [2.1.0](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/2.1.0) (2025-09-17)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/2.0.0...2.1.0)

## [2.0.0](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/2.0.0) (2025-06-12)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.2.5...2.0.0)

## [1.2.5](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.2.5) (2025-05-31)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.2.4...1.2.5)

## [1.2.4](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.2.4) (2025-05-30)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.2.3...1.2.4)

## [1.2.3](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.2.3) (2025-04-15)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.2.2...1.2.3)

## [1.2.2](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.2.2) (2025-04-09)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.2.1...1.2.2)

## [1.2.1](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.2.1) (2025-03-14)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.2.0...1.2.1)

## [1.2.0](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.2.0) (2025-03-14)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.0.6...1.2.0)

## [1.0.6](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.0.6) (2025-01-14)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.0.5...1.0.6)

## [1.0.5](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.0.5) (2025-01-13)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.0.4...1.0.5)

## [1.0.4](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.0.4) (2024-12-27)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.0.3...1.0.4)

## [1.0.3](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.0.3) (2024-11-08)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.0.2...1.0.3)

## [1.0.2](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.0.2) (2024-10-12)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.0.1...1.0.2)

## [1.0.1](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.0.1) (2024-09-09)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/1.0.0...1.0.1)

## [1.0.0](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/tree/1.0.0) (2024-09-09)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui.ugui/compare/d916b061d8d4c528cbc85a5755785e5479a818c0...1.0.0)



\* *This Changelog was automatically generated by [github_changelog_generator](https://github.com/github-changelog-generator/github-changelog-generator)*
