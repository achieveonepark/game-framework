using System;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{
	/// <summary>
	/// 여러 개의 비동기 작업을 병렬로 실행하고, 모두 완료되면 콜백을 호출하는 유틸리티 클래스입니다.
	/// 각 작업은 Action&lt;Action&gt; 형태로, 작업이 완료되면 전달받은 Action(완료 콜백)을 호출해야 합니다.
	/// 예: AddAsync 이미지 로딩, 애니메이션 등 여러 작업이 모두 끝난 후 다음 단계를 진행할 때 사용합니다.
	/// sealed 클래스이므로 상속할 수 없습니다.
	/// </summary>
	public sealed class MultiTask : IEnumerable
	{
		// 추가된 작업들의 목록. 각 작업은 완료 시 호출할 Action을 인자로 받습니다.
		private readonly List<Action<Action>> _list = new List<Action<Action>>();

		// 현재 작업이 실행 중인지 여부. 실행 중에는 새 작업을 추가할 수 없습니다.
		private bool _isPlaying;


		/// <summary>
		/// 실행할 작업을 추가합니다.
		/// 작업은 완료되면 전달받은 Action(done callback)을 반드시 호출해야 합니다.
		/// 실행 중이거나 null 작업은 무시됩니다.
		/// </summary>
		/// <param name="task">추가할 작업. done 콜백을 인자로 받고, 작업 완료 시 호출해야 합니다.</param>
		public void Add(Action<Action> task)
		{
			if (task == null || _isPlaying) return;

			_list.Add(task);
		}


		/// <summary>
		/// 추가된 모든 작업을 병렬로 실행합니다.
		/// 모든 작업이 완료되면 onCompleted 콜백이 호출됩니다.
		/// 작업이 없으면 즉시 onCompleted를 호출합니다.
		/// </summary>
		/// <param name="onCompleted">모든 작업 완료 후 호출될 콜백. null이면 빈 콜백이 사용됩니다.</param>
		public void Play(Action onCompleted = null)
		{
			if (onCompleted == null)
			{
				onCompleted = delegate { };
			}

			// 작업이 없으면 즉시 완료 콜백을 호출합니다
			if (_list.Count <= 0)
			{
				onCompleted();
				return;
			}

			_isPlaying = true;

			// 모든 작업이 완료될 때까지 카운트를 세는 콜백을 생성합니다
			// _list.Count번 호출되면 비로소 onCompleted가 실행됩니다
			var task = CallOfCountsFromDelegate(_list.Count, () =>
			{
				_isPlaying = false;
				_list.Clear();
				onCompleted();
			});

			// 등록된 모든 작업을 시작합니다
			for (int i = 0; i < _list.Count; i++)
			{
				var n = _list[i];
				Action nextTask = task;
				// 각 작업에 완료 콜백을 전달합니다. 작업은 완료 시 이 콜백을 호출해야 합니다.
				n(() =>
				{
					if (nextTask == null) return;
					nextTask();
					// 중복 호출 방지를 위해 참조를 null로 설정합니다
					nextTask = null;
				});
			}
		}


		/// <summary>
		/// count번 호출되면 onCompleted를 실행하는 IEnumerator를 생성합니다.
		/// 내부적으로 카운트 다운 방식으로 동작합니다.
		/// </summary>
		/// <param name="count">완료까지 필요한 호출 횟수</param>
		/// <param name="onCompleted">카운트가 0이 되었을 때 호출할 콜백</param>
		/// <param name="onUpdated">카운트가 감소할 때마다 호출할 선택적 콜백</param>
		private static IEnumerator CallOfCounts(int count, Action onCompleted, Action onUpdated = null)
		{
			if (onUpdated == null)
			{
				onUpdated = delegate { };
			}

			onUpdated();

			// count가 0이 될 때까지 yield return으로 실행을 중단합니다
			while (0 < --count)
			{
				yield return count;
				onUpdated();
			}

			onCompleted();

			// 메모리 해제를 위해 콜백 참조를 null로 설정합니다
			onCompleted = null;
			onUpdated = null;
		}


		/// <summary>
		/// IEnumerator 기반의 카운터를 Action으로 래핑합니다.
		/// 반환된 Action을 호출할 때마다 IEnumerator가 한 단계씩 진행됩니다.
		/// </summary>
		/// <param name="count">완료까지 필요한 호출 횟수</param>
		/// <param name="onCompleted">모든 카운트 완료 후 실행할 콜백</param>
		/// <param name="onUpdated">각 카운트 감소 시 실행할 선택적 콜백</param>
		/// <returns>호출할 때마다 카운터를 하나씩 감소시키는 Action</returns>
		private static Action CallOfCountsFromDelegate(int count, Action onCompleted, Action onUpdated = null)
		{
			var coroutine = CallOfCounts(count, onCompleted, onUpdated);
			// MoveNext()를 호출하면 IEnumerator가 yield return까지 실행됩니다
			return () => coroutine.MoveNext();
		}


		/// <summary>
		/// IEnumerable 인터페이스 구현입니다. 현재는 지원되지 않습니다.
		/// </summary>
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
