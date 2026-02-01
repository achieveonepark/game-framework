using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
#if USE_QUICK_SAVE
using Achieve.QuickSave;
using MemoryPack;
#endif

namespace GameFramework
{
    /// <summary>
    /// PlayerData를 담당하는 클래스입니다.<br>
    /// 테이블 데이터를 가공하여 인게임에 실사용되는 데이터들을 적재합니다.<br>
    /// </summary>
    [Serializable]
#if USE_QUICK_SAVE
    [MemoryPackable]
    public partial class PlayerManager : IManager
#else
    public class PlayerManager : IManager
#endif
    {
        private readonly Dictionary<string, IPlayerDataContainerBase> _dataStorage = new();

        public UniTask Initialize()
        {
            // Load on initialize if needed, or handle it manually.
            // Load(); 
            return UniTask.CompletedTask;
        }

        public void AddContainer<T>(T data) where T : IPlayerDataContainerBase
        {
            var key = data.DataKey;
            if (_dataStorage.ContainsKey(key))
            {
                throw new InvalidOperationException($"Data of type {key} already exists.");
            }

            _dataStorage.Add(key, data);
        }

        public T GetContainer<T>() where T : class, IPlayerDataContainerBase
        {
            var key = typeof(T).Name;
            if (_dataStorage.TryGetValue(key, out var data))
            {
                return data as T;
            }

            throw new KeyNotFoundException($"Data of type {key} not found.");
        }

        public void RemoveContainer<T>() where T : IPlayerDataContainerBase
        {
            var key = typeof(T).Name;
            if (!_dataStorage.Remove(key))
            {
                throw new KeyNotFoundException($"Data of type {key} not found for removal.");
            }
        }

#if USE_QUICK_SAVE
        private QuickSave<PlayerManager> _quickSave = new ();
        
        public void Save()
        {
            _quickSave.SaveData(this);
        }

        public PlayerManager Load()
        {
            return _quickSave.LoadData();
        }
        
#if USE_ENCRYPT
        public void SetEncrypt(string encryptionKey, int version)
        {
            _quickSave = new QuickSave<PlayerManager>.Builder()
                .UseEncryption(encryptionKey)
                .UseVersion(version)
                .Build();
        }
#endif
#endif
    }
}