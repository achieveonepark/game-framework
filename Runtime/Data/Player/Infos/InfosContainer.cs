using System;
using System.Collections.Generic;
#if USE_QUICK_SAVE
using MemoryPack;
#endif

namespace GameFramework.Data.Player
{
    /// <summary>
    /// 인게임에 실사용되는 데이터를 담는 클래스입니다.<br>
    /// 이 클래스를 상속받아서, 가지고 있는 Dictionary로 데이터를 가공하거나 변경사항이 있는 경우 수정합니다.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    #if USE_QUICK_SAVE
    [MemoryPackable]
    #endif
    public partial class InfosContainer<TKey, TValue> : PlayerInfosBase
    {
        protected Dictionary<TKey, TValue> _dataDic;

        public void Add(TKey key, TValue value)
        {
            _dataDic[key] = value;
        }

        // 데이터 가져오기
        public TValue GetInfo(TKey key)
        {
            return _dataDic.GetValueOrDefault(key);
        }

        // 데이터 삭제
        public bool RemoveInfo(TKey key)
        {
            return _dataDic.Remove(key);
        }

        // 데이터 전체 가져오기
        public IEnumerable<KeyValuePair<TKey, TValue>> GetAll()
        {
            return _dataDic;
        }
    }
}