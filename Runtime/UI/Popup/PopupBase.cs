using System;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 팝업 UI의 기본 클래스입니다. BaseUI를 상속합니다.
    /// Open/Close/Refresh 메서드를 통해 팝업의 표시/숨김/갱신을 처리합니다.
    /// 실제 팝업은 이 클래스를 상속하여 구체적인 동작을 구현합니다.
    /// 메서드 체이닝(Method Chaining) 패턴을 지원하여 SetPosition().SetParent() 형태로 연속 설정이 가능합니다.
    /// </summary>
    public class PopupBase : BaseUI
    {
        /// <summary>현재 팝업이 화면에 표시되고 있는지 여부를 반환합니다.</summary>
        public bool Active => gameObject.activeSelf;

        /// <summary>
        /// Unity의 Awake는 오브젝트가 씬에 로드될 때 한 번 호출됩니다.
        /// 컴포넌트 캐싱(Caching)을 수행합니다.
        /// </summary>
        protected void Awake()
        {
            Caching();
        }

        /// <summary>
        /// 코드로 인스펙터의 컴포넌트 등록이 필요한 경우 오버라이드하여 사용합니다.
        /// 예: 코드로 특정 컴포넌트를 찾아서 변수에 할당하는 작업을 여기서 합니다.
        /// </summary>
        public virtual void Caching()
        {
        }

        /// <summary>
        /// 팝업을 화면에 표시합니다. gameObject.SetActive(true)를 호출합니다.
        /// </summary>
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 데이터를 받아 팝업을 표시합니다.
        /// 기본 구현은 단순히 Open()을 호출합니다.
        /// 자식 클래스에서 오버라이드하여 data를 활용해 내용을 채울 수 있습니다.
        /// </summary>
        /// <param name="data">팝업에 표시할 데이터 객체</param>
        public virtual void Open(object data)
        {
            Open();
        }

        /// <summary>
        /// 팝업의 내용을 현재 데이터로 갱신합니다.
        /// 자식 클래스에서 오버라이드하여 UI 요소를 최신 데이터로 업데이트합니다.
        /// </summary>
        public virtual void Refresh()
        {
        }

        /// <summary>
        /// 새 데이터로 팝업 내용을 갱신합니다.
        /// 자식 클래스에서 오버라이드하여 전달된 data를 사용해 UI를 업데이트합니다.
        /// </summary>
        /// <param name="data">갱신에 사용할 새 데이터</param>
        public virtual void Refresh(object data)
        {
        }

        /// <summary>
        /// 팝업을 닫습니다. gameObject.SetActive(false)를 호출합니다.
        /// </summary>
        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 팝업의 위치를 설정합니다.
        /// 메서드 체이닝을 위해 자기 자신을 반환합니다.
        /// </summary>
        /// <param name="position">설정할 위치 (월드 좌표)</param>
        /// <returns>this (메서드 체이닝용)</returns>
        public PopupBase SetPosition(Vector2 position)
        {
            transform.position = position;
            return this;
        }

        /// <summary>
        /// 팝업의 부모 Transform을 설정합니다.
        /// 메서드 체이닝을 위해 자기 자신을 반환합니다.
        /// </summary>
        /// <param name="transform">설정할 부모 Transform</param>
        /// <returns>this (메서드 체이닝용)</returns>
        public PopupBase SetParent(Transform transform)
        {
            transform.parent = transform;
            return this;
        }

        /// <summary>
        /// 팝업의 게임 오브젝트 이름을 설정합니다.
        /// 메서드 체이닝을 위해 자기 자신을 반환합니다.
        /// </summary>
        /// <param name="name">설정할 이름</param>
        /// <returns>this (메서드 체이닝용)</returns>
        public PopupBase SetName(string name)
        {
            gameObject.name = name;
            return this;
        }
    }
}
