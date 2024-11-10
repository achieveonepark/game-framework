using System;

namespace GameFramework
{

	public sealed class SelectableBool : Selectable<bool>
	{

		public Action mChangedTrue;


		public Action mChangedFalse;


		public SelectableBool() : base()
		{
		}


		public SelectableBool(bool value) : base()
		{
			SetValueWithoutCallback(value);
		}


		public void Not()
		{
			Value = !Value;
		}


		public void True()
		{
			Value = true;
		}


		public void False()
		{
			Value = false;
		}


		protected override void DoOnChanged()
		{
			if (Value)
			{
				mChangedTrue.Call();
			}
			else
			{
				mChangedFalse.Call();
			}
		}
	}
}