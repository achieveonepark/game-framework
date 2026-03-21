using System;
using Cysharp.Threading.Tasks;
using GameFramework.Manager;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 게임 시간을 관리하는 매니저입니다.
    /// 네트워크에서 정확한 현재 시간을 가져와 클라이언트 시간 조작을 방지합니다.
    /// 1초마다 이벤트를 발생시켜 시간에 의존하는 게임 로직을 처리할 수 있습니다.
    /// </summary>
    public class TimeManager : IManager
    {
        // 네트워크 시간을 가져올 외부 API URL (UTC 기준)
        private const string TimeApiUrl = "https://timeapi.io/api/Time/current/zone?timeZone=UTC";

        /// <summary>
        /// Unity 게임 속도를 설정합니다.
        /// 1.0f가 정상 속도, 0.5f는 슬로우모션, 0.0f는 일시 정지입니다.
        /// Time.timeScale에 직접 쓰기만 합니다 (get 없이 set만 존재).
        /// </summary>
        public float TimeScale
        {
            set => UnityEngine.Time.timeScale = value;
        }

        // 네트워크에서 받아온 기준 시간 (UTC)
        private DateTime time;

        // 매 프레임 업데이트되는 Unity의 unscaledTime (게임 시작 후 경과 초)
        private float _timer;
        // 초기화 시점의 unscaledTime. 경과 시간 계산의 기준점이 됩니다.
        private float _startTimer;

        /// <summary>
        /// 현재 시간을 반환합니다.
        /// 네트워크에서 받아온 시간에 초기화 이후 경과 시간을 더하여 계산합니다.
        /// 이 방식으로 서버와 동기화된 시간을 제공하면서도 매 프레임 API를 호출하지 않아도 됩니다.
        /// </summary>
        public DateTime Now => time.AddSeconds(_timer - _startTimer);

        /// <summary>1초마다 발생하는 이벤트입니다. 쿨타임, 타이머 UI 갱신 등에 활용합니다.</summary>
        public event Action OnEvent1Sec;

        /// <summary>
        /// TimeManager를 초기화합니다.
        /// 네트워크 시간을 가져오고, 1초 간격 체크 루프를 시작합니다.
        /// </summary>
        public async UniTask Initialize()
        {
            // 네트워크에서 정확한 현재 시간을 가져옵니다
            await GetNetworkTimeAsync();
            // 초기화 시점의 Unity 경과 시간을 기준점으로 저장합니다
            _startTimer = UnityEngine.Time.unscaledTime;
            // 1초 간격 체크 루프를 백그라운드에서 시작합니다. Forget()은 await 없이 실행을 의미합니다.
            OnCheck_1Sec().Forget(); // Start the continuous check
            // Assuming LogManager exists and can be retrieved from Core or is static
            // Core.Get<LogManager>()?.Debug("[TimeManager] Initialized");
            Debug.Log("[TimeManager] Initialized"); // Using Debug.Log for now
        }

        /// <summary>
        /// 외부 Time API에서 네트워크 시간을 비동기로 가져옵니다.
        /// 응답받은 JSON을 NTPResponse로 파싱하여 시간을 설정합니다.
        /// </summary>
        private async UniTask GetNetworkTimeAsync()
        {
            var response = await new HttpLink.Builder()
                .SetUrl(TimeApiUrl)
                .GetAsync<NTPResponse>();

            time = DateTime.Parse(response.dateTime);
        }

        /// <summary>
        /// 1초마다 반복 실행되는 내부 타이머 루프입니다.
        /// 매 1초마다 현재 Unity 시간을 갱신하고 OnEvent1Sec 이벤트를 발생시킵니다.
        /// while(true) 무한 루프로 동작하지만, UniTask.Delay로 매 반복마다 잠시 기다립니다.
        /// </summary>
        private async UniTask OnCheck_1Sec()
        {
            while (true)
            {
                // 1000밀리초(1초) 대기 후 계속 실행
                await UniTask.Delay(1000);
                // 현재 Unity 경과 시간으로 타이머를 갱신합니다
                _timer = UnityEngine.Time.unscaledTime;
                // 1초 경과 이벤트를 구독한 모든 리스너에게 알립니다
                OnEvent1Sec?.Invoke();
            }
        }
    }

    /// <summary>
    /// Time API의 JSON 응답을 역직렬화하기 위한 데이터 클래스입니다.
    /// API가 반환하는 JSON 필드 이름과 프로퍼티 이름이 일치해야 합니다.
    /// </summary>
    [Serializable]
    public class NTPResponse
    {
        /// <summary>API에서 반환하는 날짜/시간 문자열입니다. DateTime.Parse로 변환할 수 있는 형식이어야 합니다.</summary>
        public string dateTime;
    }
}
