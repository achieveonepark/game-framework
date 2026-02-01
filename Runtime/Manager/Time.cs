using System;
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

        public DateTime Now => time.AddSeconds(_timer - _startTimer);

        public event Action OnEvent1Sec;

        public async UniTask Initialize()
        {
            await GetNetworkTimeAsync();
            _startTimer = UnityEngine.Time.unscaledTime;
            OnCheck_1Sec().Forget(); // Start the continuous check
            // Assuming LogManager exists and can be retrieved from Core or is static
            // Core.Get<LogManager>()?.Debug("[TimeManager] Initialized");
            Debug.Log("[TimeManager] Initialized"); // Using Debug.Log for now
        }

        private async UniTask GetNetworkTimeAsync()
        {
            var response = await new HttpLink.Builder()
                .SetUrl(TimeApiUrl)
                .GetAsync<NTPResponse>();

            time = DateTime.Parse(response.dateTime);
        }

        private async UniTask OnCheck_1Sec()
        {
            while (true)
            {
                await UniTask.Delay(1000);
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