# ホロホッピング!!!/HoloHopping!!!
<img src="https://img.shields.io/badge/-Unity2019.4.12f1-000000.svg?logo=unity&style=plastic">
ホロライブの非公式ファンゲーム

ジャンルアクションゲーム


# デモ/DEMO

[<img src="https://img.shields.io/badge/-%E9%85%8D%E4%BF%A1%E5%85%88(UnityRoom)-4285F4.svg?logo=google-chrome&style=plastic">](https://unityroom.com/games/holohopping)

![プレイ画像](https://drive.google.com/uc?export=view&id=1aebieHpnXxMAGdXLMfmjFcsVR-vOLQVj)

# 使用アセット/Used Assets

- [UniRx](https://assetstore.unity.com/packages/tools/integration/unirx-reactive-extensions-for-unity-17276)
- [DoTweenPro](https://assetstore.unity.com/packages/tools/visual-scripting/dotween-pro-32416)
- [Arbor3](https://assetstore.unity.com/packages/tools/visual-scripting/arbor-3-fsm-bt-graph-editor-112239)
- [DoozyUI](https://assetstore.unity.com/packages/tools/gui/doozyui-complete-ui-management-system-138361)
- [Modern UI](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-150824)

# デザイン(プログラミング的な)/Design

本ゲームのアウトゲームの開発には「MV(R)Pパターン」を参考にしています。
インゲームは例外的に、新規作成したComponentをアタッチしてゲームオブジェクトの管理をしています。
正直なことを言いますと、完成を優先して作っているため、定則通りに作れていない部分は多々あります。

- Component
```bash
インゲームで扱うゲームオブジェクトに対してアタッチしています。
ゲームオブジェクトにアタッチされたTransform,RigidBodyなどはこのComponentを介して操作されます
```

- Data
```bash
ScriptableObjectを継承したPrefabやアイテムのスコアなどを管理するクラスです。
```

- Entity
```bash
Projectに保存されたDataをComponentへ渡すためにEntityとして加工します。
プレハブなども利用するときはDataとEntityを介します。
```

- Presenter
```bash
Model,Viewの両者のイベントを管理するクラスです。今回はMonoBehaviorを継承し、Scene内のゲームオブジェクトにアタッチしています。
アウトゲームではDoozyUIと組み合わせて実装しています。
また、このクラスには計算等の処理を行うことは許されてません。
```

- View
```bash
プレイヤーのUI入力・システム側のUI操作を反映します。
インゲームではスコア等のUIに使用しています。
```

- Model
```bash
数値やオブジェクト等のデータを管理するクラスです。
数値変更がされたときにイベントを発行します。
```

# 外部ソフト/OtherTools
- [MagicaVoxel](https://ephtracy.github.io/)
- [<img src="https://img.shields.io/badge/-Blender-F5792A.svg?logo=blender&style=plastic">](https://www.blender.org/)

# 原作(ホロライブ)/Source(Hololive)
ホロライブの著作権表示


© 2017-2020 cover corp. [ホロライブプロダクション](https://www.hololive.tv/)
