using System;

namespace GameFramework
{

	public class Selectable<T> : IDisposable
	{
		private T _value; // 選択中の値


		public T Value
		{
			get { return _value; }
			set
			{
				_value = value;
				OnChanged();
			}
		}


		public Action mChanged;


		public void Dispose()
		{
			mChanged = null;
		}


		public Selectable()
		{
			_value = default(T);
		}


		public Selectable(T value)
		{
			_value = value;
		}


		public void SetValueWithoutCallback(T value)
		{
			_value = value;
		}


		public void SetValueIfNotEqual(T value)
		{
			if (_value.Equals(value))
			{
				return;
			}

			_value = value;
			OnChanged();
		}


		private void OnChanged()
		{
			mChanged.Call();
			DoOnChanged();
		}


		protected virtual void DoOnChanged()
		{
		}
	}
}