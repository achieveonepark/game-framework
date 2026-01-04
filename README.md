# Game Framework

A collection of pre-built systems and extensions designed to accelerate game development in Unity.

This framework is organized under a central static class `GameFramework.Core`, which contains various managers and systems.

## UPM Installation

1.  Open the Unity Package Manager (`Window > Package Manager`).
2.  Click the `+` icon and select `Add package from git URL...`.
3.  Enter the following URL: `https://github.com/achieveonepark/game-framework.git`

## Dependencies

This framework relies on some external packages for full functionality.

### Required
-   **[UniTask](https://github.com/Cysharp/UniTask):** Required for asynchronous operations throughout the framework. Please install this **before** installing the Game Framework package.

### Optional
These packages can be installed to enable additional features.

-   **[UniTaskPubSub](https://github.com/hadashiA/UniTaskPubSub):** Enables the `UIBindingManager` for a reactive, event-based UI.
-   **[QuickSave](https://github.com/achieveonepark/quicksave):** Enables the data persistence feature in `Core.Player`. To use it, you must also add `USE_QUICK_SAVE` to your project's Scripting Define Symbols.

## Features & API

Most framework modules are available as nested classes within the static `GameFramework.Core` class.

### Access Patterns
-   **Static Classes**: Accessed directly (e.g., `Core.Time.TimeScale`).
-   **MonoBehaviour Singletons**: Accessed via an `Instance` property (e.g., `Core.Sound.Instance.PlayBGM()`). These require a corresponding GameObject in the scene.

### System Modules
| Class | Access Pattern | Description |
| :--- | :--- | :--- |
| `Core.Log` | Static | Handles console logging with various levels. |
| `Core.Config` | Static | Manages key-value configuration saved to PlayerPrefs. |
| `Core.Player` | Static | Centralized manager for runtime player data via "Containers". |
| `Core.Time` | Static | Controls global time scale and provides current time. |
| `Core.Input` | Static | A wrapper for `UnityEngine.Input` with an on/off switch. |
| `Core.Pool` | Static | Generic object pooling for prefabs using `UnityEngine.Pool`. |
| `Core.IAP` | Static | Simple hooks for processing in-app purchases. |
| `Core.Sound` | Singleton | Manages BGM and SFX playback. |
| `Core.Scene` | Singleton | Manages scene loading and unloading. |
| `Core.Popup` | Singleton | Manages instantiation and lifecycle of UI popups. |

### Other Features
-   **Utility & Extension Methods**: A large collection of extension methods for built-in Unity and C# types. See the `Runtime/Extensions` folder.
-   **UI Components**: Helper components like `SafeArea` and `Draggable`.

## Quick Start Examples

### `Core.Log`
Handles categorized console logging.
```csharp
Core.Log.Debug("This is a debug message.");
Core.Log.Info("This is for important information.");
Core.Log.Warning("Something might be wrong.");
```

### `Core.Config`
Manages simple data saved to `PlayerPrefs`.
```csharp
// Set an initial value if the key doesn't exist
Core.Config.AddKey("BGMVolume", 0.8f);

// Get and set values
Core.Config.SetConfig("BGMVolume", 0.7f);
float currentVolume = (float)Core.Config.GetConfig("BGMVolume");
```

### `Core.Player` (Data Management)
Manages runtime data via container classes.

**1. Define your data and container.**
```csharp
// The raw data structure
public class CharacterData : PlayerDataBase
{
    public string Name;
    public int Level;
}

// The container that holds the data
public class CharacterDataContainer : PlayerDataContainerBase<int, CharacterData>
{
    public CharacterDataContainer()
    {
        // IMPORTANT: DataKey MUST match the class name for GetContainer<T> to work!
        DataKey = typeof(CharacterDataContainer).Name;
    }
}
```

**2. Register and use the container.**
```csharp
// At game start, create and register the container
var characterContainer = new CharacterDataContainer();
characterContainer.Add(1, new CharacterData { Id = 1, Name = "Hero", Level = 1 });
Core.Player.AddContainer(characterContainer);

// Elsewhere, retrieve and use the data
var myChars = Core.Player.GetContainer<CharacterDataContainer>();
var mainChar = myChars.GetInfo(1);
mainChar.Level++;

// To save/load all data (requires USE_QUICK_SAVE define)
Core.Player.Save();
Core.Player.Load();
```


### `Core.Popup` (Singleton)
Requires a `PopupManager` GameObject with the `Core.Popup` script and a list of popup prefabs.
```csharp
// Open a popup of a specific type from the manager's list
// The popup's Open() method is called automatically
var myPopup = Core.Popup.Instance.Open<MyAwesomePopup>();

// Pass data to the popup when opening
var data = new MyPopupData { Message = "Hello!" };
Core.Popup.Instance.Open<MyAwesomePopup>(data);

// Close the popup
myPopup.Close();
```

### `Core.Time`
A wrapper for Unity's Time and `DateTime`.
```csharp
// Set the game speed to 2x
Core.Time.TimeScale = 2.0f;

// Get the current real-world time
DateTime now = Core.Time.Now;
```