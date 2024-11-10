using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class ObjectPool<T> : MonoSingleton<ObjectPool<T>> where T : MonoBehaviour
    {
        [SerializeField] private T createPrefab;
        [SerializeField] private int _initializeSize;
        [SerializeField] private int _addSize;
        private Queue<T> _objects = new Queue<T>();
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            AddSize(_initializeSize);
        }
        
        public T Get() => GetInternal(Vector2.zero, null);
        public T Get(Vector2 pos) => GetInternal(pos, null);
        public T Get(Transform parent) => GetInternal(Vector2.zero, parent);
        public T Get(Vector2 pos, Transform parent) => GetInternal(pos, parent);

        /// <summary>
        /// Pool에서 Object를 받아옵니다.
        /// </summary>
        /// <returns></returns>
        private T GetInternal(Vector2 position, Transform parent)
        {
            if (_objects.Count == 0)
            {
                AddSize(_addSize);
            }
            
            var obj = _objects.Dequeue();
            obj.gameObject.SetActive(true);

            if (position == Vector2.zero)
            {
                obj.ResetPosition();
            }
            else
            {
                obj.SetPosition(position);
            }
            
            if (parent != null)
            {
                obj.SafeSetParent(parent);
            }
            
            return obj;
        }

        /// <summary>
        /// 사용한 Object를 다시 반환합니다.
        /// </summary>
        /// <param name="obj"></param>
        public void Release(T obj)
        {
            _objects.Enqueue(obj);
            obj.gameObject.transform.position = Vector3.zero;
            obj.gameObject.SetActive(false);
        }

        private void AddSize(int size)
        {
            for (int i = 0; i < size; i++)
            {
                _objects.Enqueue(Instantiate(createPrefab));
            }
        }
    }
}