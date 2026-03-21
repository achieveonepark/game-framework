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
    /// IPlayerDataContainerBase를 구현한 컨테이너들을 타입 별로 관리합니다.
    /// USE_QUICK_SAVE 심볼이 정의된 경우 MemoryPack을 통한 빠른 직렬화/역직렬화를 지원합니다.
    /// </summary>
    [Serializable]
#if USE_QUICK_SAVE
    [MemoryPackable]
    public partial class PlayerManager : IManager
#else
    public class PlayerManager : IManager
#endif
    {
        // 플레이어 데이터 컨테이너를 타입명(키)으로 관리하는 저장소
        private readonly Dictionary<string, IPlayerDataContainerBase> _dataStorage = new();

        /// <summary>
        /// PlayerManager를 초기화합니다.
        /// 필요에 따라 저장된 데이터를 즉시 불러오거나, 나중에 수동으로 Load()를 호출할 수 있습니다.
        /// </summary>
        public UniTask Initialize()
        {
            // Load on initialize if needed, or handle it manually.
            // Load();
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// 새 플레이어 데이터 컨테이너를 추가합니다.
        /// 같은 키의 컨테이너가 이미 있으면 예외를 던집니다.
        /// </summary>
        /// <typeparam name="T">추가할 컨테이너 타입. IPlayerDataContainerBase를 구현해야 합니다.</typeparam>
        /// <param name="data">추가할 컨테이너 인스턴스</param>
        public void AddContainer<T>(T data) where T : IPlayerDataContainerBase
        {
            var key = data.DataKey;
            // 동일한 키의 컨테이너가 이미 등록되어 있으면 중복 추가를 막습니다
            if (_dataStorage.ContainsKey(key))
            {
                throw new InvalidOperationException($"Data of type {key} already exists.");
            }

            _dataStorage.Add(key, data);
        }

        /// <summary>
        /// 타입으로 플레이어 데이터 컨테이너를 가져옵니다.
        /// 예: playerManager.GetContainer&lt;InventoryContainer&gt;()
        /// 해당 타입의 컨테이너가 없으면 예외를 던집니다.
        /// </summary>
        /// <typeparam name="T">가져올 컨테이너 타입</typeparam>
        /// <returns>등록된 컨테이너 인스턴스</returns>
        public T GetContainer<T>() where T : class, IPlayerDataContainerBase
        {
            // 타입 이름을 키로 사용하여 컨테이너를 검색합니다
            var key = typeof(T).Name;
            if (_dataStorage.TryGetValue(key, out var data))
            {
                return data as T;
            }

            throw new KeyNotFoundException($"Data of type {key} not found.");
        }

        /// <summary>
        /// 등록된 플레이어 데이터 컨테이너를 제거합니다.
        /// 해당 타입의 컨테이너가 없으면 예외를 던집니다.
        /// </summary>
        /// <typeparam name="T">제거할 컨테이너 타입</typeparam>
        public void RemoveContainer<T>() where T : IPlayerDataContainerBase
        {
            var key = typeof(T).Name;
            // Remove 메서드는 제거 성공 시 true, 키가 없으면 false를 반환합니다
            if (!_dataStorage.Remove(key))
            {
                throw new KeyNotFoundException($"Data of type {key} not found for removal.");
            }
        }

#if USE_QUICK_SAVE
        // USE_QUICK_SAVE 심볼이 정의된 경우에만 사용하는 빠른 저장/불러오기 기능
        private readonly QuickSave _quickSave = new ();

        /// <summary>
        /// 현재 PlayerManager 상태를 QuickSave를 통해 빠르게 저장합니다.
        /// MemoryPack을 사용하여 직렬화하므로 매우 빠른 저장 속도를 제공합니다.
        /// </summary>
        public void Save()
        {
            _quickSave.SaveData(this);
        }

        /// <summary>
        /// QuickSave에서 저장된 PlayerManager 데이터를 불러옵니다.
        /// </summary>
        /// <returns>저장된 PlayerManager 인스턴스</returns>
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
