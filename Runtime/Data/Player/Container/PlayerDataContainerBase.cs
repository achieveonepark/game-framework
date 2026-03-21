using System;
using System.Collections.Generic;
#if USE_QUICK_SAVE
using MemoryPack;
#endif

namespace GameFramework
{
    /// <summary>
    /// 인게임에 실사용되는 데이터를 담는 클래스입니다.<br>
    /// 이 클래스를 상속받아서, 가지고 있는 Dictionary로 데이터를 가공하거나 변경사항이 있는 경우 수정합니다.
    /// Dictionary 기반으로 키-값 쌍의 데이터를 관리하며, CRUD(추가/조회/삭제) 기능을 제공합니다.
    /// USE_QUICK_SAVE 심볼이 정의된 경우 MemoryPack으로 직렬화 가능합니다.
    /// </summary>
    /// <typeparam name="TKey">데이터를 구분하는 키 타입 (예: int ID, string 이름)</typeparam>
    /// <typeparam name="TValue">저장할 데이터 타입 (예: PlayerDataBase를 상속한 클래스)</typeparam>
    [Serializable]
    #if USE_QUICK_SAVE
    [MemoryPackable]
    public partial class PlayerDataContainerBase<TKey, TValue> : IPlayerDataContainerBase
#else
    public class PlayerDataContainerBase<TKey, TValue> : IPlayerDataContainerBase
#endif
    {
        /// <summary>
        /// 이 컨테이너의 고유 키입니다. 자식 클래스 생성자에서 설정해야 합니다.
        /// PlayerManager에서 GetContainer&lt;T&gt;()로 찾을 때 사용됩니다.
        /// </summary>
        public string DataKey { get; protected set; }

        // 실제 데이터를 저장하는 딕셔너리. 키로 빠르게 데이터를 조회할 수 있습니다.
        protected Dictionary<TKey, TValue> _dataDic;

        /// <summary>
        /// 새 데이터를 추가하거나 기존 키의 데이터를 덮어씁니다.
        /// </summary>
        /// <param name="key">데이터 키</param>
        /// <param name="value">저장할 데이터 값</param>
        public void Add(TKey key, TValue value)
        {
            _dataDic[key] = value;
        }

        // 데이터 가져오기
        /// <summary>
        /// 키에 해당하는 데이터를 가져옵니다.
        /// 키가 없으면 TValue의 기본값(null 또는 0)을 반환합니다.
        /// </summary>
        /// <param name="key">조회할 데이터 키</param>
        /// <returns>해당 키의 데이터. 없으면 기본값</returns>
        public TValue GetInfo(TKey key)
        {
            return _dataDic.GetValueOrDefault(key);
        }

        // 데이터 삭제
        /// <summary>
        /// 키에 해당하는 데이터를 삭제합니다.
        /// </summary>
        /// <param name="key">삭제할 데이터 키</param>
        /// <returns>삭제 성공이면 true, 키가 없어 삭제 실패면 false</returns>
        public bool RemoveInfo(TKey key)
        {
            return _dataDic.Remove(key);
        }

        // 데이터 전체 가져오기
        /// <summary>
        /// 저장된 모든 데이터를 키-값 쌍으로 반환합니다.
        /// foreach로 순회하거나 LINQ로 필터링할 때 사용합니다.
        /// </summary>
        /// <returns>전체 데이터의 키-값 쌍 열거형</returns>
        public IEnumerable<KeyValuePair<TKey, TValue>> GetAll()
        {
            return _dataDic;
        }
    }
}
