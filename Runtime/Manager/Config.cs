using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 게임의 설정(Configuration) 데이터를 관리하는 매니저입니다.
    /// PlayerPrefs를 기반으로 key-value 형태의 설정값을 저장하고 불러옵니다.
    /// 게임 내 각종 옵션이나 플래그를 저장할 때 사용합니다.
    /// </summary>
    public class ConfigManager : IManager
    {
        // 저장소 구분을 위한 고유 키. Application.identifier(앱 패키지명)를 포함해 충돌을 방지합니다.
        private readonly string dicKey = $"{Application.identifier}.configs.gameframework";

        // 설정값들을 메모리에 캐싱하는 딕셔너리
        private Dictionary<string, object> _configs;

        /// <summary>
        /// 저장된 설정 데이터를 불러와 초기화합니다.
        /// PlayerPrefs에 저장된 딕셔너리를 역직렬화해서 메모리에 올립니다.
        /// </summary>
        public UniTask Initialize()
        {
            _configs = DictionaryPrefs.LoadDictionary<string, object>(dicKey) ?? new Dictionary<string, object>();
            Debug.Log("[ConfigManager] Initialized");
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// 새 설정 키와 초기값을 추가합니다.
        /// 이미 같은 키가 있으면 추가하지 않습니다.
        /// </summary>
        /// <param name="key">설정 키 이름</param>
        /// <param name="value">초기값</param>
        public void AddKey(string key, object value)
        {
            // 이미 키가 존재하면 중복 추가를 막습니다
            if (_configs.ContainsKey(key))
            {
                Debug.Log($"Already key. key: {key}");
                return;
            }

            _configs.Add(key, value);
        }

        /// <summary>
        /// 저장된 설정값을 키로 가져옵니다.
        /// 존재하지 않는 키를 요청하면 null을 반환합니다.
        /// </summary>
        /// <param name="key">가져올 설정의 키</param>
        /// <returns>설정값 객체. 없으면 null</returns>
        public object GetConfig(string key)
        {
            if (_configs.TryGetValue(key, out var obj) is false)
            {
                Debug.Log($"Invalid key. key: {key}");
                return null;
            }

            return obj;
        }

        /// <summary>
        /// 기존 설정값을 변경하고 PlayerPrefs에 저장합니다.
        /// 존재하지 않는 키에 값을 설정하려 하면 경고를 출력하고 무시합니다.
        /// </summary>
        /// <param name="key">변경할 설정의 키</param>
        /// <param name="value">새로운 값</param>
        public void SetConfig(string key, object value)
        {
            // 등록되지 않은 키에는 값을 설정할 수 없습니다
            if (!_configs.ContainsKey(key))
            {
                Debug.Log($"Invalid key. key: {key}");
                return;
            }

            _configs[key] = value;

            // 변경된 내용을 즉시 PlayerPrefs에 저장합니다 (앱 종료 후에도 유지됨)
            DictionaryPrefs.SaveDictionary(dicKey, _configs);
        }
    }
}
