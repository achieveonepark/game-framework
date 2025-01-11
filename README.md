# Game Framework

## Install

### To use this package, [UniTask](https://github.com/cysharp/unitask) installation is required.

- Installing [UniTaskPubSub](https://github.com/hadashiA/UniTaskPubSub) allows for easier UI management, streamlining your workflow with an async/await-based messaging system.
- Installing [QuickSave](https://github.com/achieveonepark/quicksave) provides a simplified and efficient data saving system, making it easier to manage persistent data.

## Description
I’ve added classes and plugins to streamline development in Unity.

### Included Features:

1. [Config](https://github.com/achieveonepark/GameFramework?tab=readme-ov-file#config)
2. [Popup / Scene Base Classes](https://github.com/achieveonepark/GameFramework?tab=readme-ov-file#popup)
3. [Time Management](https://github.com/achieveonepark/GameFramework?tab=readme-ov-file#time-management)
4. [UIBinding using UniTaskPubSub](https://github.com/achieveonepark/GameFramework?tab=readme-ov-file#uibinding-using-unitaskpubsub)
5. [Singleton Pattern](https://github.com/achieveonepark/GameFramework?tab=readme-ov-file#singleton)
6. [Logging](https://github.com/achieveonepark/GameFramework?tab=readme-ov-file#logging)
7. [User-Friendly UnityWebRequest Wrapper](https://github.com/achieveonepark/GameFramework?tab=readme-ov-file#user-friendly-unitywebrequest-wrapper)


### Recommended Assets to Use Together:
1. [Infinity Value](https://github.com/achieveonepark/infinityValue): Struct for infinite numbers with thousand-unit grouping
2. [Smart Addressables](https://github.com/achieveonepark/SmartAddressables): Enhance the convenience and efficiency of using Unity's Addressables.
3. [Lite DB](https://github.com/achieveonepark/LiteDB): Efficiently manages data using SQLite.

## Quick Start

### Config
All data added to the `ConfigManager` is saved in `PlayerPrefs`.

```csharp
ConfigManager.AddKey("Sound", 0);
ConfigManager.AddKey("BGM", 0);
ConfigManager.AddKey("DataKey", "DataValue");

var sound = ConfigManager.GetConfig("Sound");
ConfigManager.SetConfig("Sound", 100);
```

### Popup
Editor Setup:

1. Add a `PopupManager` to the Hierarchy. (It will run with DontDestroyOnLoad enabled.)
2. Cache your `popups` in the Popups field of the `PopupManager`.

```csharp
public class SettingPopup : Popup
{
    // ...
}
public class CommonPopup : Popup
{
    // ...
}
public class SettingData
{
    public int Volume;
    // ...
}

// Calling GetPopup will also trigger the popup's Open method.
var commonPopup = PopupManager.GetPopup<CommonPopup>();

// objName : Txt_Level, TMP Support
var lvTxt = commonPopup.Get<Text>("Level");
var lvTxt = commonPopup.Get<TMP_Text>("Level"); 

SettingData data = new SettingData { Volume = 200 };
var settingPopup = PopupManager.GetPopup<SettingPopup>(data);
```

### Time Management
`TimeManager` only returns the current time.

```csharp
DateTime now = TimeManager.Now;
TimeManager.TimeScale = 2;
```

### UIBinding using UniTaskPubSub

We used the [hadashiA/UniTaskPubSub](https://github.com/hadashiA/UniTaskPubSub) repository. For more details, check out the link.<br> This setup simplifies event management.

```csharp
// Register
UIBindingManager.Subscribe<SettingData>(data =>
{
    SetVolume(data.Volume);
});

// Call
UIBindingManager.Publish(new SettingData { Volume = 5 });
```

### Singleton

Access and use each singleton via `Class.Instance`, just like any other singleton pattern.

- **MonoSingleton**: A singleton that inherits from MonoBehaviour.
- **PersistentMonoSingleton**: Like MonoSingleton but uses `DontDestroyOnLoad` to persist between scenes.
- **Singleton**: An object used as a singleton without MonoBehaviour.

### Logging

The package includes a basic `Log` class.<br> If you need customization, you can implement a custom logger by inheriting from `IGameLog`.<br> For reference, see `GameLog.cs` in the package.

```csharp
GameLog.Debug("Debug");
GameLog.Info("Info");
GameLog.Warning("Warning");
GameLog.Error("Error");
throw GameLog.Fatal("Fatal");
```

### User-Friendly UnityWebRequest Wrapper

This class is designed to wrap UnityWebRequest for a cleaner and more user-friendly experience.

```csharp
var result = new HttpLink.Builder()
    .SetUrl("https://jsonplaceholder.typicode.com/posts/1")
    .SetMethod("GET")
    .Build();
await result.SendAsync();
if (result.Success)
{
    var resultBytes = result.ReceiveData;
    var resultStr = result.ReceiveDataString;
}
```