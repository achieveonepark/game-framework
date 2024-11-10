using System;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{

	public sealed class MultiTask : IEnumerable
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

			_isPlaying = true;

			var task = CallOfCountsFromDelegate(_list.Count, () =>
			{
				_isPlaying = false;
				_list.Clear();
				onCompleted();
			});

			for (int i = 0; i < _list.Count; i++)
			{
				var n = _list[i];
				Action nextTask = task;
				n(() =>
				{
					if (nextTask == null) return;
					nextTask();
					nextTask = null;
				});
			}
		}


		private static IEnumerator CallOfCounts(int count, Action onCompleted, Action onUpdated = null)
		{
			if (onUpdated == null)
			{
				onUpdated = delegate { };
			}

			onUpdated();

			while (0 < --count)
			{
				yield return count;
				onUpdated();
			}

			onCompleted();

			onCompleted = null;
			onUpdated = null;
		}


		private static Action CallOfCountsFromDelegate(int count, Action onCompleted, Action onUpdated = null)
		{
			var coroutine = CallOfCounts(count, onCompleted, onUpdated);
			return () => coroutine.MoveNext();
		}


		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}