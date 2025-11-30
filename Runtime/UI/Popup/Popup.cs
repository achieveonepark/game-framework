using System;
using UnityEngine;

namespace GameFramework
{
    public class Popup : BaseUI
    {
        public bool Active => gameObject.activeSelf;

        protected void Awake()
        {
            Caching();
        }

        /// <summary>
        /// 코드로 인스펙터의 컴포넌트 등록이 필요한 경우
        /// </summary>
        public virtual void Caching()
        {
            
        }
        
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Open(object data)
        {
            Open();
        }

        public virtual void Refresh()
        {
            
        }

        public virtual void Refresh(object data)
        {
            
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public Popup SetPosition(Vector2 position)
        {
            transform.position = position;
            return this;
        }

        public Popup SetParent(Transform transform)
        {
            transform.parent = transform;
            return this;
        }

        public Popup SetName(string name)
        {
            gameObject.name = name;
            return this;
        }
    }
}