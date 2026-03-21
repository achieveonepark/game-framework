# Game Framework

**[한국어](README.md) | [English](README.en.md) | 日本語**

Unity ゲーム開発を加速するために設計された、事前構築済みのシステムと拡張機能のコレクションです。

このフレームワークは中央静的クラス `GameFramework.Core` の下に整理されており、様々なマネージャーとシステムが含まれています。

## UPM インストール

1.  Unity Package Manager を開きます（`Window > Package Manager`）。
2.  `+` アイコンをクリックし、`Add package from git URL...` を選択します。
3.  以下の URL を入力します: `https://github.com/achieveonepark/game-framework.git`

## 依存関係

このフレームワークは完全な機能のためにいくつかの外部パッケージに依存しています。

### 必須
-   **[UniTask](https://github.com/Cysharp/UniTask):** フレームワーク全体の非同期処理に必要です。Game Framework パッケージをインストールする**前に**インストールしてください。

### オプション
以下のパッケージをインストールすることで追加機能が有効になります。

-   **[UniTaskPubSub](https://github.com/hadashiA/UniTaskPubSub):** リアクティブなイベントベース UI のための `UIBindingManager` を有効にします。
-   **[QuickSave](https://github.com/achieveonepark/quicksave):** `Core.Player` のデータ永続化機能を有効にします。使用するには、プロジェクトの Scripting Define Symbols に `USE_QUICK_SAVE` を追加する必要があります。

## 機能 & API

ほとんどのフレームワークモジュールは、静的クラス `GameFramework.Core` 内のネストされたクラスとして利用できます。

### アクセスパターン
-   **静的クラス**: 直接アクセスします（例: `Core.Time.TimeScale`）。
-   **MonoBehaviour シングルトン**: `Instance` プロパティ経由でアクセスします（例: `Core.Sound.Instance.PlayBGM()`）。シーン内に対応する GameObject が必要です。

### システムモジュール
| クラス | アクセスパターン | 説明 |
| :--- | :--- | :--- |
| `Core.Log` | Static | 様々なレベルのコンソールログを処理します。 |
| `Core.Config` | Static | PlayerPrefs に保存されたキーと値の設定を管理します。 |
| `Core.Player` | Static | 「コンテナ」を通じたランタイムプレイヤーデータの集中管理。 |
| `Core.Time` | Static | グローバルタイムスケールを制御し、現在時刻を提供します。 |
| `Core.Input` | Static | オン/オフスイッチ付きの `UnityEngine.Input` ラッパー。 |
| `Core.Pool` | Static | `UnityEngine.Pool` を使用したプレハブの汎用オブジェクトプーリング。 |
| `Core.IAP` | Static | アプリ内購入処理のシンプルなフック。 |
| `Core.Sound` | Singleton | BGM と SFX の再生を管理します。 |
| `Core.Scene` | Singleton | シーンのロードとアンロードを管理します。 |
| `Core.Popup` | Singleton | UI ポップアップのインスタンス化とライフサイクルを管理します。 |

### その他の機能
-   **ユーティリティ & 拡張メソッド**: Unity と C# の組み込み型に対する拡張メソッドの大規模なコレクション。`Runtime/Extensions` フォルダを参照してください。
-   **UI コンポーネント**: `SafeArea` や `Draggable` などのヘルパーコンポーネント。

## クイックスタート

### `Core.Log`
カテゴリ化されたコンソールログを処理します。
```csharp
Core.Log.Debug("デバッグメッセージです。");
Core.Log.Info("重要な情報に使用します。");
Core.Log.Warning("何か問題があるかもしれません。");
```

### `Core.Config`
`PlayerPrefs` に保存された簡単なデータを管理します。
```csharp
// キーが存在しない場合、初期値を設定する
Core.Config.AddKey("BGMVolume", 0.8f);

// 値の取得と設定
Core.Config.SetConfig("BGMVolume", 0.7f);
float currentVolume = (float)Core.Config.GetConfig("BGMVolume");
```

### `Core.Player`（データ管理）
コンテナクラスを通じてランタイムデータを管理します。

**1. データとコンテナを定義します。**
```csharp
// 生のデータ構造
public class CharacterData : PlayerDataBase
{
    public string Name;
    public int Level;
}

// データを保持するコンテナ
public class CharacterDataContainer : PlayerDataContainerBase<int, CharacterData>
{
    public CharacterDataContainer()
    {
        // 重要: GetContainer<T> が動作するには DataKey がクラス名と一致する必要があります！
        DataKey = typeof(CharacterDataContainer).Name;
    }
}
```

**2. コンテナを登録して使用します。**
```csharp
// ゲーム開始時にコンテナを作成して登録する
var characterContainer = new CharacterDataContainer();
characterContainer.Add(1, new CharacterData { Id = 1, Name = "Hero", Level = 1 });
Core.Player.AddContainer(characterContainer);

// 別の場所でデータを取得して使用する
var myChars = Core.Player.GetContainer<CharacterDataContainer>();
var mainChar = myChars.GetInfo(1);
mainChar.Level++;

// すべてのデータを保存/ロードする（USE_QUICK_SAVE 定義が必要）
Core.Player.Save();
Core.Player.Load();
```


### `Core.Popup`（Singleton）
`Core.Popup` スクリプトとポップアッププレハブのリストを持つ `PopupManager` GameObject が必要です。
```csharp
// マネージャーのリストから特定の型のポップアップを開く
// ポップアップの Open() メソッドが自動的に呼び出される
var myPopup = Core.Popup.Instance.Open<MyAwesomePopup>();

// 開く際にポップアップにデータを渡す
var data = new MyPopupData { Message = "こんにちは！" };
Core.Popup.Instance.Open<MyAwesomePopup>(data);

// ポップアップを閉じる
myPopup.Close();
```

### `Core.Time`
Unity の Time と `DateTime` のラッパーです。
```csharp
// ゲーム速度を2倍に設定する
Core.Time.TimeScale = 2.0f;

// 現実の現在時刻を取得する
DateTime now = Core.Time.Now;
```
