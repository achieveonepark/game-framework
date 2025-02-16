using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Unity.Serialization.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace GameFramework
{
    public class HttpLink
    {
        private HttpLink(UnityWebRequest request)
        {
            this._request = request;
        }
        
        private UnityWebRequest _request;

        public UnityWebRequest.Result Result => _request.result;
        public bool Success => Result == UnityWebRequest.Result.Success;
        public byte[] ReceiveData => _request.downloadHandler.data;
        public string ReceiveDataString => _request.downloadHandler.text;
        public ulong DownloadSize => _request.downloadedBytes;
        public float DownloadProgress => _request.downloadProgress;
        
        public async UniTask<HttpLink> SendAsync()
        {
            await _request.SendWebRequest();

            // 오류 처리
            if (_request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {_request.error}");
            }

            return this;
        }
        
        public class Builder
        {
            private string url;
            private string method;
            private Dictionary<string, string> headers = new Dictionary<string, string>();
            private string requestBody;
            private int timeout;

            // URL 설정
            public Builder SetUrl(string url)
            {
                this.url = url;
                return this;
            }

            public async UniTask<T> GetAsync<T>()
            {
                this.method = UnityWebRequest.kHttpVerbGET;

                if (string.IsNullOrEmpty(url))
                {
                    throw GameLog.Fatal(new InvalidUrlException());
                }
                
                var httpLink = Build();
                var request = await httpLink.SendAsync();
                return request.Success is false ? default : JsonSerialization.FromJson<T>(request.ReceiveDataString);
            }

            public async UniTask<bool> PostAsync()
            {
                this.method = UnityWebRequest.kHttpVerbPOST;
                
                if (string.IsNullOrEmpty(url))
                {
                    throw GameLog.Fatal(new InvalidUrlException());
                }
                
                var httpLink = Build();
                var request = await httpLink.SendAsync();
                return request.Success;
            }

            // HTTP 메서드 설정 (Get, Post 등)
            public Builder SetMethod(string method)
            {
                this.method = method;
                return this;
            }

            // 헤더 추가
            public Builder AddHeader(string key, string value)
            {
                headers[key] = value;
                return this;
            }

            // POST 요청 시, JSON 형태의 본문 데이터 추가
            public Builder SetJsonBody(string json)
            {
                this.requestBody = json;
                return this;
            }

            // Timeout 설정
            public Builder SetTimeout(int seconds)
            {
                this.timeout = seconds;
                return this;
            }

            // 요청 보내기 (async/await 패턴 사용)
            public HttpLink Build()
            {
                if (string.IsNullOrEmpty(url))
                {
                    throw GameLog.Fatal(new InvalidUrlException());
                }
                
                UnityWebRequest request;

                // HTTP 메서드에 따른 요청 생성
                if (method == UnityWebRequest.kHttpVerbPOST)
                {
                    request = UnityWebRequest.PostWwwForm(url, requestBody);
                    
                    if (!string.IsNullOrEmpty(requestBody))
                    {
                        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
                        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                        request.SetRequestHeader("Content-Type", "application/json");
                    }
                }
                else if (method == UnityWebRequest.kHttpVerbGET)
                {
                    request = UnityWebRequest.Get(url);
                }
                else
                {
                    throw new System.Exception("지원하지 않는 HTTP 메서드입니다.");
                }
                
                if (timeout != 0)
                {
                    request.timeout = timeout;
                }

                // 헤더 추가
                foreach (var header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }

                return new HttpLink(request);
            }
        }
    }
}
