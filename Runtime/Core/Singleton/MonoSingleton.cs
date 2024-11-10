using UnityEngine;

namespace GameFramework
{

    /// <summary>
    /// The basic MonoBehaviour singleton implementation, this singleton is destroyed after scene changes, use <see cref="PersistentMonoSingleton{T}"/> if you want a persistent and global singleton instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static T instance;

        /// <summary>
        /// The initialization status of the singleton's instance.
        /// </summary>
        private SingletonInitializationStatus initializationStatus = SingletonInitializationStatus.None;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
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
        /// Gets whether the singleton's instance is initialized.
        /// </summary>
        public virtual bool IsInitialized => this.initializationStatus == SingletonInitializationStatus.Initialized;

        /// <summary>
        /// Use this for initialization.
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

                // Destory duplicates
                if (Application.isPlaying)
                {
                    Destroy(gameObject);
                }
                else
                {
                    DestroyImmediate(gameObject);
                }
            }
        }

        /// <summary>
        /// This gets called once the singleton's instance is created.
        /// </summary>
        protected virtual void OnMonoSingletonCreated()
        {

        }

        protected virtual void OnInitializing()
        {

        }

        protected virtual void OnInitialized()
        {

        }

        public virtual void InitializeSingleton()
        {
            if (this.initializationStatus != SingletonInitializationStatus.None)
            {
                return;
            }

            this.initializationStatus = SingletonInitializationStatus.Initialized;
            OnInitialized();
        }

        public virtual void ClearSingleton() { }

        public static void CreateInstance()
        {
            DestroyInstance();
            instance = Instance;
        }

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