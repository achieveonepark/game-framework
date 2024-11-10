using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if USE_TMP
using TMPro;
#endif

namespace GameFramework
{
    public class BaseUI : MonoBehaviour
    {
        private Dictionary<string, Component> _componentCache = new Dictionary<string, Component>();

        private void Awake()
        {
            SetCacheComponent();
        }

        public virtual void SetCacheComponent()
        {
            CacheComponentsInChildren<Image>("Img");
#if USE_TMP
            CacheComponentsInChildren<TMP_Text>("Txt");
#else
            CacheComponentsInChildren<Text>("Txt");
#endif
            CacheComponentsInChildren<InputField>("If");
            CacheComponentsInChildren<Button>("Btn");
            CacheComponentsInChildren<Slider>("sli");
        }

        /// <summary>
        /// Text만 세팅 가능
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isActive"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(string name, bool isActive = true, string value = "") where T : Component
        {
            if (_componentCache.TryGetValue($"{GetPrefix(typeof(T))}_{name}", out Component component) is false)
            {
                Debug.LogWarning($"{typeof(T)} with name {name} not found.");
                return null;
            }

            component.gameObject.SetActive(true);

            if (component is Text textComponent)
            {
                if (!string.IsNullOrEmpty(value))
                    textComponent.text = value;
            }

            return component as T;
        }

        public void CacheComponentsInChildren<T>(string prefix) where T : Component
        {
            foreach (var component in GetComponentsInChildren<T>(true))
            {
                if (component.name.StartsWith(prefix))
                {
                    _componentCache[component.name] = component;
                }
            }
        }

        public virtual string GetPrefix(Type type)
        {
#if USE_TMP
            if (type == typeof(TMP_Text)) return "Txt";
#else
            if (type == typeof(Text)) return "Txt";
#endif
            if (type == typeof(Button)) return "Btn";
            if (type == typeof(Image)) return "Img";
            if (type == typeof(GameObject)) return "Obj";
            return string.Empty;
        }
    }
}