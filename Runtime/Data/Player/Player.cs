using System;
using System.Collections.Generic;
#if USE_QUICK_SAVE
using MemoryPack;
#endif

namespace GameFramework
{
    /// <summary>
    /// PlayerData를 담당하는 클래스입니다.<br>
    /// 테이블 데이터를 가공하여 인게임에 실사용되는 데이터들을 적재합니다.<br>
    /// 이 패키지에서는 인게임에서 실사용되는 데이터를 Info라 칭합니다.
    /// </summary>
    #if USE_QUICK_SAVE
    [MemoryPackable]
    #endif
    [Serializable]
    public partial class Player
    {
        private static Player _instance; 
        private static Player getInstance => _instance ??= new Player();
        
        private readonly Dictionary<string, PlayerDataContainerBase> _dataStorage = new ();

        public static void AddInfos<T>(T data) where T : PlayerDataContainerBase
        {
            var key = data.DataKey;
            if (getInstance._dataStorage.TryGetValue(key, out var playerInfos))
            {
                throw new InvalidOperationException($"Data of type {key} already exists.");
            }
            
            getInstance._dataStorage.Add(key, data);
        }

        public static T GetInfos<T>() where T : PlayerDataContainerBase
        {
            var key = typeof(T).Name;
            if (getInstance._dataStorage.TryGetValue(key, out var data))
            {
                return data as T;
            }

            throw new KeyNotFoundException($"Data of type {key} not found.");
        }

        public static void RemoveInfos<T>() where T : PlayerDataContainerBase
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