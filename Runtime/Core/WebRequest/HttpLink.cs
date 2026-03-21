using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Unity.Properties;
using Unity.Serialization.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace GameFramework
{
    /// <summary>
    /// HTTP 요청을 관리하는 클래스입니다.
    /// UnityWebRequest를 감싸(Wrapping)서 더 편리하게 GET/POST 요청을 보낼 수 있도록 합니다.
    /// 인스턴스 생성은 내부 Builder 클래스를 통해서만 가능합니다 (생성자가 private).
    /// </summary>
    public class HttpLink
    {
        // 생성자를 private으로 설정하여 Builder를 통해서만 인스턴스를 만들 수 있게 합니다
        private HttpLink(UnityWebRequest request)
        {
            this._request = request;
        }

        // 실제 HTTP 요청을 담당하는 Unity의 웹 요청 객체
        private UnityWebRequest _request;

        /// <summary>요청 결과 상태 (성공, 실패, 연결 오류 등)</summary>
        public UnityWebRequest.Result Result => _request.result;

        /// <summary>요청이 성공했으면 true를 반환합니다.</summary>
        public bool Success => Result == UnityWebRequest.Result.Success;

        /// <summary>서버에서 받은 응답 데이터를 바이트 배열로 반환합니다.</summary>
        public byte[] ReceiveData => _request.downloadHandler.data;

        /// <summary>서버에서 받은 응답 데이터를 문자열로 반환합니다. JSON 응답 파싱에 유용합니다.</summary>
        public string ReceiveDataString => _request.downloadHandler.text;

        /// <summary>다운로드된 총 바이트 크기입니다.</summary>
        public ulong DownloadSize => _request.downloadedBytes;

        /// <summary>다운로드 진행률입니다. 0.0 ~ 1.0 범위의 값입니다.</summary>
        public float DownloadProgress => _request.downloadProgress;

        /// <summary>
        /// 비동기로 HTTP 요청을 전송하고, 완료되면 자기 자신(HttpLink)을 반환합니다.
        /// await 키워드와 함께 사용하면 요청이 완료될 때까지 기다립니다.
        /// </summary>
        /// <returns>요청이 완료된 HttpLink 인스턴스 (결과 확인에 사용)</returns>
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

        /// <summary>
        /// HttpLink 인스턴스를 단계적으로 구성하는 빌더 클래스입니다.
        /// 메서드 체이닝 방식으로 URL, 헤더, 본문 등을 설정한 후 Build() 또는 GetAsync/PostAsync로 요청을 생성합니다.
        /// 예: new HttpLink.Builder().SetUrl("https://...").AddHeader("key","val").GetAsync&lt;MyData&gt;()
        /// </summary>
        public class Builder
        {
            // 요청할 URL 주소
            private string url;
            // HTTP 메서드 (GET, POST 등)
            private string method;
            // 요청에 포함할 헤더 딕셔너리
            private Dictionary<string, string> headers = new Dictionary<string, string>();
            // POST 요청 시 전송할 본문(body) 데이터
            private string requestBody;
            // 요청 타임아웃 시간(초). 0이면 기본값 사용
            private int timeout;

            // URL 설정
            /// <summary>
            /// 요청을 보낼 URL을 설정합니다.
            /// </summary>
            /// <param name="url">요청 대상 URL 문자열</param>
            /// <returns>메서드 체이닝을 위해 Builder 자신을 반환합니다.</returns>
            public Builder SetUrl(string url)
            {
                this.url = url;
                return this;
            }

            /// <summary>
            /// GET 방식으로 비동기 요청을 보내고, 응답 JSON을 지정한 타입 T로 역직렬화해서 반환합니다.
            /// </summary>
            /// <typeparam name="T">응답 JSON을 파싱할 대상 타입</typeparam>
            /// <returns>파싱된 데이터 객체. 실패 시 기본값(default) 반환</returns>
            public async UniTask<T> GetAsync<T>()
            {
                this.method = UnityWebRequest.kHttpVerbGET;

                if (string.IsNullOrEmpty(url))
                {
                    throw GameLog.Fatal(new InvalidPathException("Url does not exist."));
                }

                var httpLink = Build();
                var request = await httpLink.SendAsync();
                // 요청 실패 시 기본값 반환, 성공 시 JSON을 T 타입으로 변환
                return request.Success is false ? default : JsonSerialization.FromJson<T>(request.ReceiveDataString);
            }

            /// <summary>
            /// POST 방식으로 비동기 요청을 보내고, 성공 여부를 반환합니다.
            /// </summary>
            /// <returns>요청 성공이면 true, 실패면 false</returns>
            public async UniTask<bool> PostAsync()
            {
                this.method = UnityWebRequest.kHttpVerbPOST;

                if (string.IsNullOrEmpty(url))
                {
                    throw GameLog.Fatal(new InvalidPathException("Url does not exist."));
                }

                var httpLink = Build();
                var request = await httpLink.SendAsync();
                return request.Success;
            }

            // HTTP 메서드 설정 (Get, Post 등)
            /// <summary>
            /// HTTP 메서드를 직접 지정합니다. 보통은 GetAsync/PostAsync를 사용하세요.
            /// </summary>
            /// <param name="method">HTTP 메서드 문자열 (예: "GET", "POST")</param>
            public Builder SetMethod(string method)
            {
                this.method = method;
                return this;
            }

            // 헤더 추가
            /// <summary>
            /// HTTP 요청 헤더를 추가합니다. 인증 토큰이나 Content-Type 등을 설정할 때 사용합니다.
            /// </summary>
            /// <param name="key">헤더 키 (예: "Authorization")</param>
            /// <param name="value">헤더 값 (예: "Bearer token123")</param>
            public Builder AddHeader(string key, string value)
            {
                headers[key] = value;
                return this;
            }

            // POST 요청 시, JSON 형태의 본문 데이터 추가
            /// <summary>
            /// POST 요청에 포함할 JSON 본문 데이터를 설정합니다.
            /// </summary>
            /// <param name="json">JSON 형식의 문자열</param>
            public Builder SetJsonBody(string json)
            {
                this.requestBody = json;
                return this;
            }

            // Timeout 설정
            /// <summary>
            /// 요청 타임아웃 시간을 초 단위로 설정합니다.
            /// 설정한 시간 내에 응답이 없으면 요청이 실패합니다.
            /// </summary>
            /// <param name="seconds">타임아웃 시간(초). 0이면 Unity 기본 타임아웃 사용</param>
            public Builder SetTimeout(int seconds)
            {
                this.timeout = seconds;
                return this;
            }

            // 요청 보내기 (async/await 패턴 사용)
            /// <summary>
            /// 설정된 옵션을 바탕으로 HttpLink 인스턴스를 생성합니다.
            /// URL이 비어 있으면 예외를 던집니다.
            /// </summary>
            /// <returns>구성이 완료된 HttpLink 인스턴스</returns>
            public HttpLink Build()
            {
                if (string.IsNullOrEmpty(url))
                {
                    throw GameLog.Fatal(new InvalidPathException("Url does not exist."));
                }

                UnityWebRequest request;

                // HTTP 메서드에 따른 요청 생성
                if (method == UnityWebRequest.kHttpVerbPOST)
                {
                    request = UnityWebRequest.PostWwwForm(url, requestBody);

                    // POST 요청에 본문 데이터가 있으면 JSON 형식으로 설정
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

                // 타임아웃이 설정된 경우에만 적용 (0이면 기본값 사용)
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
