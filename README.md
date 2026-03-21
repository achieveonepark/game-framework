# Game Framework

Unity 게임 개발을 빠르게 시작할 수 있도록 설계된 미리 구성된 시스템과 확장 기능 모음입니다.

모든 매니저는 정적 클래스 `GameFramework.Core`를 통해 등록 및 접근합니다.

## UPM 설치

1. Unity Package Manager를 엽니다 (`Window > Package Manager`).
2. `+` 버튼을 클릭하고 `Add package from git URL...`을 선택합니다.
3. 아래 URL을 입력합니다: `https://github.com/achieveonepark/game-framework.git`

## 의존성

### 필수
- **[UniTask](https://github.com/Cysharp/UniTask):** 프레임워크 전반의 비동기 처리에 필요합니다. Game Framework 설치 **전에** 먼저 설치해 주세요.

### 선택
추가 기능을 활성화하려면 아래 패키지를 설치하세요.

- **[UniTaskPubSub](https://github.com/hadashiA/UniTaskPubSub):** `UIBindingManager`의 반응형 이벤트 기반 UI 기능을 활성화합니다. 사용 시 프로젝트 Scripting Define Symbols에 `USE_PUBSUB`를 추가하세요.
- **[QuickSave](https://github.com/achieveonepark/quicksave):** `PlayerManager`의 데이터 영속성 기능을 활성화합니다. 사용 시 `USE_QUICK_SAVE`를 Scripting Define Symbols에 추가하세요.

## 초기화

모든 매니저는 `[RuntimeInitializeOnLoadMethod]` 어트리뷰트를 통해 씬 로드 전에 자동으로 초기화됩니다.

```csharp
// 자동 실행 (별도 호출 불필요)
await Core.InitializeAllManagers();
```

커스텀 매니저를 직접 등록할 수도 있습니다.

```csharp
await Core.Register(new MyCustomManager());
```

## 기능 및 API

### 접근 패턴

모든 매니저는 `Core.Get<T>()`를 통해 접근합니다.

```csharp
var config = Core.Get<ConfigManager>();
var time   = Core.Get<TimeManager>();
var sound  = Core.Get<SoundManager>();
```

`PopupManager`는 `PersistentMonoSingleton`으로, `Instance` 프로퍼티로 접근합니다.

```csharp
PopupManager.Instance.Open<MyPopup>();
```

### 시스템 모듈

| 클래스 | 접근 방식 | 설명 |
| :--- | :--- | :--- |
| `ConfigManager` | `Core.Get<ConfigManager>()` | PlayerPrefs 기반 키-값 설정 관리 |
| `PlayerManager` | `Core.Get<PlayerManager>()` | 컨테이너 기반 런타임 플레이어 데이터 관리 |
| `TimeManager` | `Core.Get<TimeManager>()` | 타임스케일 제어 및 NTP 서버 시간 제공 |
| `InputManager` | `Core.Get<InputManager>()` | `UnityEngine.Input` 래퍼 |
| `SoundManager` | `Core.Get<SoundManager>()` | BGM / SFX 재생 관리 |
| `SceneManager` | `Core.Get<SceneManager>()` | 비동기 씬 로드 및 언로드 |
| `IAPManager` | `Core.Get<IAPManager>()` | 인앱 결제 처리 훅 |
| `PopupManager` | `PopupManager.Instance` | UI 팝업 인스턴스 생성 및 수명 주기 관리 |

---

## 빠른 시작 예제

### `ConfigManager`
PlayerPrefs에 저장되는 간단한 설정 관리.

```csharp
var config = Core.Get<ConfigManager>();

// 키가 없을 때 초기값 설정
config.AddKey("BGMVolume", 0.8f);

// 값 읽기 / 쓰기
config.SetConfig("BGMVolume", 0.7f);
float volume = (float)config.GetConfig("BGMVolume");
```

---

### `PlayerManager` (데이터 관리)
컨테이너 클래스를 통해 런타임 데이터를 관리합니다.

**1. 데이터와 컨테이너를 정의합니다.**
```csharp
// 원시 데이터 구조
public class CharacterData : PlayerDataBase
{
    public string Name;
    public int Level;
}

// 데이터를 담는 컨테이너
public class CharacterDataContainer : PlayerDataContainerBase<int, CharacterData>
{
    public CharacterDataContainer()
    {
        // IMPORTANT: DataKey는 반드시 클래스 이름과 일치해야 합니다.
        DataKey = typeof(CharacterDataContainer).Name;
    }
}
```

**2. 컨테이너를 등록하고 사용합니다.**
```csharp
// 게임 시작 시 컨테이너 생성 및 등록
var container = new CharacterDataContainer();
container.Add(1, new CharacterData { Id = 1, Name = "Hero", Level = 1 });
Core.Get<PlayerManager>().AddContainer(container);

// 다른 곳에서 데이터 조회 및 사용
var myChars = Core.Get<PlayerManager>().GetContainer<CharacterDataContainer>();
var mainChar = myChars.GetInfo(1);
mainChar.Level++;

// 전체 데이터 저장/불러오기 (USE_QUICK_SAVE 정의 필요)
Core.Get<PlayerManager>().Save();
Core.Get<PlayerManager>().Load();
```

---

### `TimeManager`
NTP 서버에서 현재 시간을 가져오고, 타임스케일을 제어합니다.

```csharp
var time = Core.Get<TimeManager>();

// 게임 속도 2배로 설정
time.TimeScale = 2.0f;

// NTP 서버 기반 현재 시간 가져오기
DateTime now = time.Now;

// 1초마다 호출되는 이벤트 구독
time.OnEvent1Sec += () => Debug.Log("1초 경과");
```

---

### `PopupManager`
씬에 `PopupManager` GameObject가 있어야 합니다.

```csharp
// 특정 타입의 팝업 열기
var popup = PopupManager.Instance.Open<MyAwesomePopup>();

// 데이터를 전달하며 팝업 열기
var data = new MyPopupData { Message = "안녕하세요!" };
PopupManager.Instance.Open<MyAwesomePopup>(data);

// 팝업 닫기
popup.Close();
```

---

### `HttpLink`
빌더 패턴 기반 HTTP 요청 래퍼 (async/await 지원).

```csharp
// GET 요청 후 JSON 역직렬화
var result = await new HttpLink.Builder()
    .SetUrl("https://api.example.com/data")
    .GetAsync<MyResponseData>();

// POST 요청
var response = await new HttpLink.Builder()
    .SetUrl("https://api.example.com/submit")
    .SetBody(new MyRequestData { Value = 42 })
    .PostAsync<MyResponseData>();
```

---

## UI 컴포넌트

| 컴포넌트 | 설명 |
| :--- | :--- |
| `SafeArea` | 기기 SafeArea에 맞게 RectTransform을 자동 조정 |
| `Draggable` | Physics2D 기반 드래그 앤 드롭 기능 제공 |
| `BaseUI` | 이름 접두사 기반 컴포넌트 자동 캐싱 (`Img_`, `Txt_`, `Btn_` 등) |
| `PopupBase` | Open / Close / Refresh 수명 주기를 가진 팝업 베이스 클래스 |
| `ItemListPopup<T>` | 리스트 기반 팝업 제네릭 템플릿 |
| `ObjectTouchManager` | 2D 오브젝트 터치/클릭 이벤트 중앙 관리 |
| `UIBindingManager` | 반응형 이벤트 기반 UI (USE_PUBSUB 정의 필요) |

---

## 유틸리티

### 싱글톤 베이스

| 클래스 | 설명 |
| :--- | :--- |
| `Singleton<T>` | 스레드 안전 C# 클래스 기반 싱글톤 |
| `MonoSingleton<T>` | 씬 전환 시 파괴되는 MonoBehaviour 싱글톤 |
| `PersistentMonoSingleton<T>` | 씬 전환 후에도 유지되는 MonoBehaviour 싱글톤 |

### 공통 유틸리티

| 클래스 | 설명 |
| :--- | :--- |
| `CachableMonoBehaviour` | Transform / RectTransform 캐싱을 제공하는 MonoBehaviour |
| `MultiTask` | 순차 UniTask 실행 헬퍼 |
| `Selectable<T>` | 값 변경 콜백을 가진 Observable 값 컨테이너 |
| `MultiDictionary<TKey, TValue>` | 하나의 키에 여러 값을 매핑하는 딕셔너리 |

### 확장 메서드

`Runtime/Extensions` 폴더에 50개 이상의 Unity / C# 타입 확장 메서드가 포함되어 있습니다.

- **컬렉션**: `List`, `Array`, `IEnumerable`, `Dictionary` 등
- **Unity 타입**: `GameObject`, `Component`, `Transform`, `Vector2/3`, `Color`, `Rect` 등
- **UI**: `RectTransform`, `Text`, `Image`, `Button` 등
- **기타**: `String`, `DateTime`, `Enum`, `PlayerPrefs`, `Camera` 등
