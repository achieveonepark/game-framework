using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameFramework
{
    public class ObjectPool : MonoBehaviour
    {
        public GameObject CreatePrefab;
        [SerializeField] private int _addSize;
        
        private readonly Queue<GameObject> _objects = new ();
        
        public void Initialize(int initializeCount, GameObject createPrefab = null)
        {
            if (createPrefab != null)
            {
                CreatePrefab = createPrefab;
            }
            
            AddSize(initializeCount);
        }
        
        public GameObject Get() => GetInternal(Vector2.zero, null);
        public GameObject Get(Vector2 pos) => GetInternal(pos, null);
        public GameObject Get(Transform parent) => GetInternal(Vector2.zero, parent);
        public GameObject Get(Vector2 pos, Transform parent) => GetInternal(pos, parent);

        public T Get<T>() where T : MonoBehaviour
        {
            var obj = GetInternal(Vector2.zero, null);   
            return obj.TryGetComponent(out T component) ? component : null;
        }

        public T Get<T>(Vector2 pos) where T : MonoBehaviour
        {
            var obj = GetInternal(pos, null);
            return obj.TryGetComponent(out T component) ? component : null;
        }
        
        public T Get<T>(Transform parent) where T : MonoBehaviour
        {
            var obj = GetInternal(Vector2.zero, parent);
            return obj.TryGetComponent(out T component) ? component : null;
        }

        public T Get<T>(Vector2 pos, Transform parent) where T : MonoBehaviour
        {
            var obj = GetInternal(pos, parent);
            return obj.TryGetComponent(out T component) ? component : null;
        }


        /// <summary>
        /// Pool에서 Object를 받아옵니다.
        /// </summary>
        /// <returns></returns>
        private GameObject GetInternal(Vector2 position, Transform parent)
        {
            if (_objects.Count == 0)
            {
                AddSize(_addSize);
            }
            
            var obj = _objects.Dequeue();
            obj.gameObject.SetActive(true);

            if (position == Vector2.zero)
            {
                obj.ResetLocalPosition();
            }
            else
            {
                obj.SetLocalPosition(position);
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
        public void Release(GameObject obj)
        {
            _objects.Enqueue(obj);
            obj.gameObject.transform.position = Vector3.zero;
            obj.gameObject.transform.parent = gameObject.transform;
            obj.gameObject.SetActive(false);
        }

        public void AddSize(int size)
        {
            for (int i = 0; i < size; i++)
            {
                var obj = Instantiate(CreatePrefab, gameObject.transform);
                obj.name = obj.name.Replace("(Clone)", "");
                _objects.Enqueue(obj);
                obj.SetActive(false);
            }
        }
    }
}