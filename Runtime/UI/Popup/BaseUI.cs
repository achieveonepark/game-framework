using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if USE_TMP
using TMPro;
#endif

namespace GameFramework
{
    /// <summary>
    /// 모든 UI 클래스의 기반이 되는 기본 클래스입니다.
    /// 자식 오브젝트의 UI 컴포넌트(Image, Text, Button 등)를 이름 접두사(prefix)로 자동 캐싱하여
    /// Get&lt;T&gt;("이름") 형태로 쉽게 접근할 수 있게 합니다.
    /// 예: Btn_StartGame 이라는 이름의 Button을 Get&lt;Button&gt;("_StartGame")으로 가져올 수 있습니다.
    /// </summary>
    public class BaseUI : MonoBehaviour
    {
        // 컴포넌트 이름을 키로 하여 컴포넌트를 저장하는 캐시 딕셔너리
        // 매번 GetComponentInChildren을 호출하는 것보다 훨씬 빠릅니다
        private Dictionary<string, Component> _componentCache = new Dictionary<string, Component>();

        /// <summary>
        /// Unity의 Awake는 오브젝트가 씬에 로드될 때 한 번 호출됩니다.
        /// 자식 오브젝트의 컴포넌트를 캐싱합니다.
        /// </summary>
        private void Awake()
        {
            SetCacheComponent();
        }

        /// <summary>
        /// 자식 오브젝트의 UI 컴포넌트들을 이름 기반으로 캐싱합니다.
        /// 오버라이드하여 추가 컴포넌트 타입을 등록할 수 있습니다.
        /// 컴포넌트 이름이 지정된 접두사(예: "Btn", "Img")로 시작하는 것들만 캐싱됩니다.
        /// </summary>
        public virtual void SetCacheComponent()
        {
            // 이름이 "Img"로 시작하는 Image 컴포넌트를 모두 캐싱합니다
            CacheComponentsInChildren<Image>("Img");
#if USE_TMP
            // TextMeshPro를 사용하는 경우 TMP_Text를 캐싱합니다
            CacheComponentsInChildren<TMP_Text>("Txt");
#else
            // USE_TMP 심볼이 없으면 기본 Unity UI Text를 캐싱합니다
            CacheComponentsInChildren<Text>("Txt");
#endif
            CacheComponentsInChildren<InputField>("If");
            CacheComponentsInChildren<Button>("Btn");
            CacheComponentsInChildren<Slider>("sli");
        }

        /// <summary>
        /// 캐싱된 컴포넌트를 이름으로 가져옵니다.
        /// Text만 세팅 가능하며, value 매개변수를 사용하면 텍스트 내용도 설정합니다.
        /// </summary>
        /// <typeparam name="T">가져올 컴포넌트 타입</typeparam>
        /// <param name="name">컴포넌트 이름에서 접두사를 제외한 부분 (예: Button "Btn_Start"는 "_Start")</param>
        /// <param name="isActive">컴포넌트를 활성화할지 여부</param>
        /// <param name="value">Text 컴포넌트의 경우 설정할 텍스트 내용</param>
        /// <returns>찾은 컴포넌트. 없으면 null</returns>
        public T Get<T>(string name, bool isActive = true, string value = "") where T : Component
        {
            // 타입에 해당하는 접두사와 이름을 조합하여 캐시에서 검색합니다
            if (_componentCache.TryGetValue($"{GetPrefix(typeof(T))}_{name}", out Component component) is false)
            {
                Debug.LogWarning($"{typeof(T)} with name {name} not found.");
                return null;
            }

            component.gameObject.SetActive(true);

            // Text 컴포넌트라면 value 값이 있을 때 텍스트를 설정합니다
            if (component is Text textComponent)
            {
                if (!string.IsNullOrEmpty(value))
                    textComponent.text = value;
            }

            return component as T;
        }

        /// <summary>
        /// 특정 타입과 접두사에 맞는 자식 컴포넌트를 모두 찾아 캐시에 등록합니다.
        /// 이름이 지정한 접두사로 시작하는 컴포넌트만 등록됩니다.
        /// </summary>
        /// <typeparam name="T">캐싱할 컴포넌트 타입</typeparam>
        /// <param name="prefix">컴포넌트 이름의 접두사 (예: "Btn", "Img", "Txt")</param>
        public void CacheComponentsInChildren<T>(string prefix) where T : Component
        {
            // true를 전달하면 비활성화된 자식 오브젝트도 검색합니다
            foreach (var component in GetComponentsInChildren<T>(true))
            {
                // 이름이 접두사로 시작하는 컴포넌트만 캐싱합니다
                if (component.name.StartsWith(prefix))
                {
                    _componentCache[component.name] = component;
                }
            }
        }

        /// <summary>
        /// 컴포넌트 타입에 해당하는 이름 접두사를 반환합니다.
        /// 이 접두사를 사용하여 오브젝트 이름으로 컴포넌트를 찾을 수 있습니다.
        /// </summary>
        /// <param name="type">접두사를 알고 싶은 컴포넌트 타입</param>
        /// <returns>해당 타입의 이름 접두사 문자열</returns>
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
