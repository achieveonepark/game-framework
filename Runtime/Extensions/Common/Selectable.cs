using System;

namespace GameFramework
{
	/// <summary>
	/// 값이 변경될 때 자동으로 이벤트를 발생시키는 제네릭 래퍼 클래스입니다.
	/// 옵저버 패턴(Observer Pattern)의 간단한 구현으로, 값이 바뀌면 구독자에게 알려줍니다.
	/// IDisposable을 구현하여 using 블록이나 명시적 Dispose()로 이벤트를 정리할 수 있습니다.
	/// 예: Selectable&lt;int&gt; score = new Selectable&lt;int&gt;(0); score.mChanged += UpdateScoreUI;
	/// </summary>
	/// <typeparam name="T">래핑할 값의 타입</typeparam>
	public class Selectable<T> : IDisposable
	{
		private T _value; // 現在選択中の値 (현재 선택된 값)


		/// <summary>
		/// 현재 저장된 값입니다.
		/// set으로 값을 변경하면 자동으로 mChanged 이벤트와 DoOnChanged()가 호출됩니다.
		/// </summary>
		public T Value
		{
			get { return _value; }
			set
			{
				_value = value;
				OnChanged();
			}
		}


		/// <summary>
		/// 값이 변경될 때 호출될 콜백을 등록하는 이벤트입니다.
		/// 여러 개의 콜백을 += 로 등록할 수 있습니다.
		/// </summary>
		public Action mChanged;


		/// <summary>
		/// 이벤트 구독을 정리합니다.
		/// mChanged에 등록된 모든 콜백을 해제하여 메모리 누수를 방지합니다.
		/// </summary>
		public void Dispose()
		{
			mChanged = null;
		}


		/// <summary>
		/// 기본값(default(T))으로 초기화하는 생성자입니다.
		/// int는 0, bool은 false, 참조 타입은 null이 됩니다.
		/// </summary>
		public Selectable()
		{
			_value = default(T);
		}


		/// <summary>
		/// 지정한 초기값으로 초기화하는 생성자입니다.
		/// 초기화 시에는 mChanged 이벤트가 발생하지 않습니다.
		/// </summary>
		/// <param name="value">초기값</param>
		public Selectable(T value)
		{
			_value = value;
		}


		/// <summary>
		/// 이벤트 발생 없이 값만 변경합니다.
		/// 초기화 또는 UI 갱신 없이 내부 상태만 동기화할 때 사용합니다.
		/// </summary>
		/// <param name="value">설정할 새 값</param>
		public void SetValueWithoutCallback(T value)
		{
			_value = value;
		}


		/// <summary>
		/// 현재 값과 다른 경우에만 값을 변경하고 이벤트를 발생시킵니다.
		/// 같은 값을 반복 설정할 때 불필요한 이벤트 발생을 막을 때 유용합니다.
		/// </summary>
		/// <param name="value">비교하고 설정할 새 값</param>
		public void SetValueIfNotEqual(T value)
		{
			// 현재 값과 동일하면 이벤트를 발생시키지 않습니다
			if (_value.Equals(value))
			{
				return;
			}

			_value = value;
			OnChanged();
		}


		/// <summary>
		/// 값이 변경될 때 내부적으로 호출되는 메서드입니다.
		/// mChanged 이벤트를 발생시키고, 자식 클래스의 DoOnChanged도 호출합니다.
		/// </summary>
		private void OnChanged()
		{
			mChanged.Call();
			DoOnChanged();
		}


		/// <summary>
		/// 값 변경 시 추가 동작이 필요한 경우 자식 클래스에서 오버라이드합니다.
		/// </summary>
		protected virtual void DoOnChanged()
		{
		}
	}
}
