using System;
using System.Collections.Generic;
#if USE_QUICK_SAVE
using MemoryPack;
#endif

namespace GameFramework
{
    public static partial class Core
    {
        /// <summary>
        /// PlayerData를 담당하는 클래스입니다.<br>
        /// 테이블 데이터를 가공하여 인게임에 실사용되는 데이터들을 적재합니다.<br>
        /// </summary>
#if USE_QUICK_SAVE
    [MemoryPackable]
#endif
        [Serializable]
        public class Player
        {
            private static Player _instance;
            private static Player getInstance => _instance ??= new Player();

            private readonly Dictionary<string, IPlayerDataContainerBase> _dataStorage = new();

            public static void AddContainer<T>(T data) where T : IPlayerDataContainerBase
            {
                var key = data.DataKey;
                if (getInstance._dataStorage.TryGetValue(key, out var playerInfos))
                {
                    throw new InvalidOperationException($"Data of type {key} already exists.");
                }

                getInstance._dataStorage.Add(key, data);
            }

            public static T GetContainer<T>() where T : class, IPlayerDataContainerBase
            {
                var key = typeof(T).Name;
                if (getInstance._dataStorage.TryGetValue(key, out var data))
                {
                    return data as T;
                }

                throw new KeyNotFoundException($"Data of type {key} not found.");
            }

            public static void RemoveContainer<T>() where T : IPlayerDataContainerBase
            {
                var key = typeof(T).Name;
                if (!getInstance._dataStorage.Remove(key))
                {
                    throw new KeyNotFoundException($"Data of type {key} not found for removal.");
                }
            }

#if USE_QUICK_SAVE
        private readonly QuickSave _quickSave = new ();
        
        public static void Save()
        {
            getInstance.SaveInternal();
        }

        public static void Load()
        {
            getInstance.LoadInternal();
        }

        private void SaveInternal()
        {
            _quickSave.Save(this);
        }

        private Player LoadInternal()
        {
            return _quickSave.Load();
        }
#endif
        }
    }
}