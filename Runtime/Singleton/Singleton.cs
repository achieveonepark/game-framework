namespace GameFramework
{
    /// <summary>
    /// 싱글톤의 초기화 상태를 나타내는 열거형입니다.
    /// </summary>
    public enum SingletonInitializationStatus
    {
        /// <summary>아직 초기화되지 않은 상태입니다.</summary>
        None,
        /// <summary>초기화가 완료된 상태입니다.</summary>
        Initialized
    }

    /// <summary>
    /// MonoBehaviour를 사용하지 않는 일반 C# 클래스용 싱글톤 구현입니다.
    /// 이 클래스를 상속하면 해당 클래스는 자동으로 싱글톤이 됩니다.
    /// 스레드 안전(Thread-safe)하게 구현되어 있어 멀티스레드 환경에서도 안전합니다.
    /// </summary>
    /// <typeparam name="T">싱글톤으로 만들 클래스 타입. 반드시 이 클래스를 상속하고 기본 생성자(new())가 있어야 합니다.</typeparam>
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>, new()
    {
        // 싱글톤 인스턴스를 저장하는 정적 변수
        private static T instance;

        // 현재 초기화 상태를 추적합니다
        private SingletonInitializationStatus initializationStatus = SingletonInitializationStatus.None;

        /// <summary>
        /// 싱글톤 인스턴스에 접근하는 프로퍼티입니다.
        /// 처음 접근 시 인스턴스를 생성하고 초기화합니다(Lazy Initialization).
        /// lock을 사용해 멀티스레드 환경에서 중복 생성을 방지합니다.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // 여러 스레드가 동시에 접근해도 하나의 인스턴스만 생성되도록 잠금 처리
                    //ensure that only one thread can execute
                    lock (typeof(T))
                    {
                        // lock 이후 다시 null 체크 (double-checked locking 패턴)
                        if (instance == null)
                        {
                            instance = new T();
                            instance.InitializeSingleton();
                        }
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
        /// 초기화가 완료되었을 때 호출되는 가상 메서드입니다.
        /// 자식 클래스에서 오버라이드하여 초기화 시 필요한 작업을 수행할 수 있습니다.
        /// </summary>
        protected virtual void OnInitialized()
        {
        }

        /// <summary>
        /// 싱글톤을 초기화합니다.
        /// 이미 초기화된 경우에는 다시 초기화하지 않습니다.
        /// </summary>
        public virtual void InitializeSingleton()
        {
            // 이미 초기화된 상태라면 중복 초기화를 건너뜁니다
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
        /// 싱글톤을 강제로 재초기화해야 할 때 사용합니다.
        /// </summary>
        public static void CreateInstance()
        {
            DestroyInstance();
            instance = Instance;
        }

        /// <summary>
        /// 싱글톤 인스턴스를 파괴합니다.
        /// ClearSingleton()을 호출하여 리소스를 정리한 후 인스턴스를 null로 만듭니다.
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
