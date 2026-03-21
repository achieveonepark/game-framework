# Game Framework

**한국어 | [English](README.en.md) | [日本語](README.ja.md)**

Unity 게임 개발을 가속화하기 위해 설계된 사전 구축된 시스템과 확장 기능 모음입니다.

이 프레임워크는 중앙 정적 클래스 `GameFramework.Core` 아래에 구성되어 있으며, 다양한 매니저와 시스템을 포함하고 있습니다.

## UPM 설치

1.  Unity Package Manager를 엽니다 (`Window > Package Manager`).
2.  `+` 아이콘을 클릭하고 `Add package from git URL...`을 선택합니다.
3.  다음 URL을 입력합니다: `https://github.com/achieveonepark/game-framework.git`

## 의존성

이 프레임워크는 전체 기능을 위해 일부 외부 패키지에 의존합니다.

### 필수
-   **[UniTask](https://github.com/Cysharp/UniTask):** 프레임워크 전반의 비동기 작업에 필요합니다. Game Framework 패키지 설치 **전에** 먼저 설치해주세요.

### 선택사항
아래 패키지를 설치하면 추가 기능을 활성화할 수 있습니다.

-   **[UniTaskPubSub](https://github.com/hadashiA/UniTaskPubSub):** 반응형 이벤트 기반 UI를 위한 `UIBindingManager`를 활성화합니다.
-   **[QuickSave](https://github.com/achieveonepark/quicksave):** `Core.Player`의 데이터 영속성 기능을 활성화합니다. 사용하려면 프로젝트의 Scripting Define Symbols에 `USE_QUICK_SAVE`를 추가해야 합니다.

## 기능 & API

대부분의 프레임워크 모듈은 정적 클래스 `GameFramework.Core` 내의 중첩 클래스로 제공됩니다.

### 접근 패턴
-   **정적 클래스**: 직접 접근합니다 (예: `Core.Time.TimeScale`).
-   **MonoBehaviour 싱글톤**: `Instance` 프로퍼티를 통해 접근합니다 (예: `Core.Sound.Instance.PlayBGM()`). 씬에 해당 GameObject가 있어야 합니다.

### 시스템 모듈
| 클래스 | 접근 패턴 | 설명 |
| :--- | :--- | :--- |
| `Core.Log` | Static | 다양한 레벨의 콘솔 로깅을 처리합니다. |
| `Core.Config` | Static | PlayerPrefs에 저장된 키-값 설정을 관리합니다. |
| `Core.Player` | Static | "컨테이너"를 통한 런타임 플레이어 데이터의 중앙 집중 관리. |
| `Core.Time` | Static | 글로벌 타임 스케일을 제어하고 현재 시각을 제공합니다. |
| `Core.Input` | Static | 켜고 끄는 스위치가 있는 `UnityEngine.Input` 래퍼. |
| `Core.Pool` | Static | `UnityEngine.Pool`을 사용한 프리팹의 범용 오브젝트 풀링. |
| `Core.IAP` | Static | 인앱 구매 처리를 위한 간단한 훅. |
| `Core.Sound` | Singleton | BGM 및 SFX 재생을 관리합니다. |
| `Core.Scene` | Singleton | 씬 로드 및 언로드를 관리합니다. |
| `Core.Popup` | Singleton | UI 팝업의 인스턴스화와 라이프사이클을 관리합니다. |

### 기타 기능
-   **유틸리티 & 확장 메서드**: Unity 및 C# 내장 타입에 대한 방대한 확장 메서드 컬렉션. `Runtime/Extensions` 폴더를 참고하세요.
-   **UI 컴포넌트**: `SafeArea`, `Draggable` 등의 헬퍼 컴포넌트.

## 빠른 시작 예제

### `Core.Log`
카테고리별 콘솔 로깅을 처리합니다.
```csharp
Core.Log.Debug("디버그 메시지입니다.");
Core.Log.Info("중요한 정보에 사용합니다.");
Core.Log.Warning("뭔가 잘못되었을 수 있습니다.");
```

### `Core.Config`
`PlayerPrefs`에 저장된 간단한 데이터를 관리합니다.
```csharp
// 키가 없으면 초기값을 설정
Core.Config.AddKey("BGMVolume", 0.8f);

// 값 가져오기 및 설정
Core.Config.SetConfig("BGMVolume", 0.7f);
float currentVolume = (float)Core.Config.GetConfig("BGMVolume");
```

### `Core.Player` (데이터 관리)
컨테이너 클래스를 통해 런타임 데이터를 관리합니다.

**1. 데이터와 컨테이너를 정의합니다.**
```csharp
// 원시 데이터 구조
public class CharacterData : PlayerDataBase
{
    public string Name;
    public int Level;
}

// 데이터를 보유하는 컨테이너
public class CharacterDataContainer : PlayerDataContainerBase<int, CharacterData>
{
    public CharacterDataContainer()
    {
        // 중요: GetContainer<T>가 동작하려면 DataKey가 클래스 이름과 일치해야 합니다!
        DataKey = typeof(CharacterDataContainer).Name;
    }
}
```

**2. 컨테이너를 등록하고 사용합니다.**
```csharp
// 게임 시작 시 컨테이너를 생성하고 등록
var characterContainer = new CharacterDataContainer();
characterContainer.Add(1, new CharacterData { Id = 1, Name = "Hero", Level = 1 });
Core.Player.AddContainer(characterContainer);

// 다른 곳에서 데이터를 가져와 사용
var myChars = Core.Player.GetContainer<CharacterDataContainer>();
var mainChar = myChars.GetInfo(1);
mainChar.Level++;

// 모든 데이터 저장/로드 (USE_QUICK_SAVE 정의 필요)
Core.Player.Save();
Core.Player.Load();
```


### `Core.Popup` (Singleton)
`Core.Popup` 스크립트와 팝업 프리팹 목록이 있는 `PopupManager` GameObject가 필요합니다.
```csharp
// 매니저의 목록에서 특정 타입의 팝업을 엽니다
// 팝업의 Open() 메서드가 자동으로 호출됩니다
var myPopup = Core.Popup.Instance.Open<MyAwesomePopup>();

// 팝업을 열 때 데이터 전달
var data = new MyPopupData { Message = "안녕하세요!" };
Core.Popup.Instance.Open<MyAwesomePopup>(data);

// 팝업 닫기
myPopup.Close();
```

### `Core.Time`
Unity의 Time 및 `DateTime`을 위한 래퍼입니다.
```csharp
// 게임 속도를 2배로 설정
Core.Time.TimeScale = 2.0f;

// 현재 실제 시간 가져오기
DateTime now = Core.Time.Now;
```
