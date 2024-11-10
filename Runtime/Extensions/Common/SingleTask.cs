using System;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{

	public sealed class SingleTask : IEnumerable
	{
		private readonly List<Action<Action>> _list = new List<Action<Action>>();

		private bool _isPlaying;


		public void Add(Action<Action> task)
		{
			if (task == null || _isPlaying) return;
			_list.Add(task);
		}


		public void Play(Action onCompleted = null)
		{
			if (onCompleted == null)
			{
				onCompleted = delegate { };
			}

			if (_list.Count <= 0)
			{
				onCompleted();
				return;
			}

			int count = 0;

			Action task = null;
			task = () =>
			{
				if (_list.Count <= count)
				{
					_isPlaying = false;
					_list.Clear();
					onCompleted();
					return;
				}

				Action nextTask = task;

				_list[count++](() =>
				{
					if (nextTask == null) return;
					nextTask();
					nextTask = null;
				});
			};

			_isPlaying = true;
			task();
		}


		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}