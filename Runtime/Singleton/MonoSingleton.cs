using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// MonoBehaviour를 기반으로 한 기본 싱글톤 구현입니다.
    /// MonoBehaviour는 Unity의 씬에 존재하는 게임 오브젝트에 붙는 컴포넌트의 기본 클래스입니다.
    /// 이 싱글톤은 씬이 변경되면 함께 파괴됩니다.
    /// 씬 전환 후에도 유지되길 원한다면 <see cref="PersistentMonoSingleton{T}"/>를 사용하세요.
    /// </summary>
    /// <typeparam name="T">싱글톤으로 만들 MonoBehaviour 자식 클래스 타입</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {
        // 단 하나만 존재해야 하는 인스턴스를 저장하는 정적 변수
        private static T instance;

        // 현재 초기화 상태를 추적합니다
        private SingletonInitializationStatus initializationStatus = SingletonInitializationStatus.None;

        /// <summary>
        /// 싱글톤 인스턴스에 접근하는 프로퍼티입니다.
        /// 씬에 이미 있는 오브젝트를 찾거나, 없으면 새 게임 오브젝트를 만들어 컴포넌트를 추가합니다.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // 씬에서 해당 타입의 MonoBehaviour를 검색합니다
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        // 씬에 없으면 새 게임 오브젝트를 만들고 컴포넌트를 추가합니다
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                        instance.OnMonoSingletonCreated();
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 싱글톤 인스턴스가 초기화 완료 상태인지 여부를 반환합니다.
        /// </summary>
        public virtual bool IsInitialized => this.initializationStatus == SingletonInitializationStatus.Initialized;

        /// <summary>
        /// Unity의 Awake는 게임 오브젝트가 활성화될 때 처음 한 번 호출됩니다.
        /// 중복 인스턴스가 있으면 즉시 파괴하여 싱글톤을 유지합니다.
        /// </summary>
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;

                // Initialize existing instance
                InitializeSingleton();
            }
            else
            {
                // 이미 다른 인스턴스가 있으면 이 오브젝트는 중복이므로 파괴합니다
                // Destory duplicates
                if (Application.isPlaying)
                {
                    // 플레이 모드에서는 Destroy를 사용합니다 (다음 프레임에 파괴)
                    Destroy(gameObject);
                }
                else
                {
                    // 에디터 모드에서는 DestroyImmediate를 사용합니다 (즉시 파괴)
                    DestroyImmediate(gameObject);
                }
            }
        }

        /// <summary>
        /// 싱글톤 인스턴스가 코드로 새로 생성되었을 때 호출됩니다.
        /// 자식 클래스에서 오버라이드하여 생성 시 필요한 추가 작업을 수행할 수 있습니다.
        /// </summary>
        protected virtual void OnMonoSingletonCreated()
        {
        }

        /// <summary>
        /// 초기화가 완료되었을 때 호출되는 가상 메서드입니다.
        /// 자식 클래스에서 오버라이드하여 초기화 로직을 추가할 수 있습니다.
        /// </summary>
        protected virtual void OnInitialized()
        {
        }

        /// <summary>
        /// 싱글톤을 초기화합니다. 이미 초기화된 경우에는 다시 초기화하지 않습니다.
        /// </summary>
        public virtual void InitializeSingleton()
        {
            if (this.initializationStatus != SingletonInitializationStatus.None)
            {
                return;
            }

            this.initializationStatus = SingletonInitializationStatus.Initialized;
            OnInitialized();
        }

        /// <summary>
        /// 싱글톤 인스턴스를 정리합니다. 자식 클래스에서 오버라이드하여 리소스 해제 로직을 추가할 수 있습니다.
        /// </summary>
        public virtual void ClearSingleton() { }

        /// <summary>
        /// 기존 인스턴스를 삭제하고 새 인스턴스를 만듭니다.
        /// </summary>
        public static void CreateInstance()
        {
            DestroyInstance();
            instance = Instance;
        }

        /// <summary>
        /// 싱글톤 인스턴스를 파괴하고 null로 초기화합니다.
        /// </summary>
        public static void DestroyInstance()
        {
            if (instance == null)
            {
                return;
            }

            instance.ClearSingleton();
            instance = default(T);
        }
    }
}
