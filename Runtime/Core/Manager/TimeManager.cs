using System;
using Cysharp.Threading.Tasks;
using GameFramework;
using UnityEngine;

public static class TimeManager
{
    [RuntimeInitializeOnLoadMethod]
    private static async void Initialize()
    {
        await GetNetworkTimeAsync();
        _startTimer = Time.unscaledTime;
        OnCheck_1Sec().Forget();
        GameLog.Debug("[TimeManager] Initialized");
    }
    
    private const string TimeApiUrl = "https://timeapi.io/api/Time/current/zone?timeZone=UTC";
    
    public static float TimeScale
    {
        set => Time.timeScale = value;
    }

    private static DateTime time;
    
    private static float _timer;
    private static float _startTimer;

    public static DateTime Now => time.AddSeconds(_timer - _startTimer);
    
    public static event Action OnEvent1Sec;
    
    private static async UniTask GetNetworkTimeAsync()
    {
        var response = await new HttpLink.Builder()
            .SetUrl(TimeApiUrl)
            .GetAsync<NTPResponse>();
       
        time = DateTime.Parse(response.dateTime);
    }

    private static async UniTask OnCheck_1Sec()
    {
        while (true)
        {
            await UniTask.Delay(1000);
            _timer = Time.unscaledTime;
            OnEvent1Sec?.Invoke();
        }
    }
}

[Serializable]
public class NTPResponse
{
    public string dateTime;
}