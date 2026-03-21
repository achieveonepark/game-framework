using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
using UnityEngine;

namespace GameFramework
{
    public class TimeManager : IManager
    {
        private const string TimeApiUrl = "https://timeapi.io/api/Time/current/zone?timeZone=UTC";

        public float TimeScale
        {
            set => UnityEngine.Time.timeScale = value;
        }

        private DateTime time;

        private float _timer;
        private float _startTimer;
        private CancellationTokenSource _cts;

        public DateTime Now => time.AddSeconds(_timer - _startTimer);

        public event Action OnEvent1Sec;

        public async UniTask Initialize()
        {
            await GetNetworkTimeAsync();
            _startTimer = UnityEngine.Time.unscaledTime;
            _cts = new CancellationTokenSource();
            OnCheck_1Sec(_cts.Token).Forget();
            Debug.Log("[TimeManager] Initialized");
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTask GetNetworkTimeAsync()
        {
            var response = await new HttpLink.Builder()
                .SetUrl(TimeApiUrl)
                .GetAsync<NTPResponse>();

            time = DateTime.Parse(response.dateTime);
        }

        private async UniTask OnCheck_1Sec(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                await UniTask.Delay(1000, cancellationToken: ct);
                _timer = UnityEngine.Time.unscaledTime;
                OnEvent1Sec?.Invoke();
            }
        }
    }

    [Serializable]
    public class NTPResponse
    {
        public string dateTime;
    }
}