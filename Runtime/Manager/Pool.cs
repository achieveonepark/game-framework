using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
using UnityEngine;
using UnityEngine.Pool;

namespace GameFramework
{
    /// <summary>
    /// 오브젝트 풀링(Object Pooling)을 관리하는 매니저입니다.
    /// 오브젝트 풀링이란 자주 생성/삭제되는 게임 오브젝트(총알, 파티클 등)를
    /// 미리 만들어 두고 재활용하는 최적화 기법입니다.
    /// Instantiate/Destroy 대신 Get/Release를 사용하여 GC 부하를 줄입니다.
    /// </summary>
    public class PoolManager : IManager
    {
        // 풀 이름(키)으로 GameObject 풀을 관리하는 딕셔너리
        private Dictionary<string, IObjectPool<GameObject>> _objs = new ();

        /// <summary>
        /// PoolManager를 초기화합니다.
        /// 현재는 별도의 초기화 로직이 없습니다.
        /// </summary>
        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// 새 오브젝트 풀을 등록합니다.
        /// 키와 프리팹을 지정하면 해당 프리팹으로 풀을 생성합니다.
        /// </summary>
        /// <param name="key">풀을 식별하는 고유 이름</param>
        /// <param name="createObject">풀에서 생성할 GameObject 프리팹</param>
        public void AddPool(string key, GameObject createObject)
        {
            // _objs.Add(key, new ObjectPool<GameObject>());
            // TODO: Implement actual pooling logic
        }

        /// <summary>
        /// 등록된 풀에서 오브젝트를 가져옵니다.
        /// 키에 해당하는 풀이 없거나, 오브젝트에 지정한 컴포넌트가 없으면 null을 반환합니다.
        /// </summary>
        /// <typeparam name="T">가져올 컴포넌트 타입</typeparam>
        /// <param name="key">풀 이름</param>
        /// <returns>풀에서 꺼낸 오브젝트의 T 컴포넌트. 실패 시 null</returns>
        public T GetObject<T>(string key) where T : Object
        {
            // 해당 키의 풀이 없으면 null 반환
            if (_objs.TryGetValue(key, out var pool) is false)
            {
                return null;
            }

            // 풀에서 오브젝트를 꺼냅니다 (없으면 새로 생성됨)
            var obj = pool.Get();

            // 꺼낸 오브젝트에 원하는 컴포넌트가 없으면 null 반환
            if (obj.TryGetComponent<T>(out var component) is false)
            {
                return null;
            }

            return component;
        }
    }
}
