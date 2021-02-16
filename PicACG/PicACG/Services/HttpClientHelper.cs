using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PicACG.Services
{
    public class HttpClientHelper
    {
        private static readonly object LockObj = new();
        private static HttpClient? _client;

        private static readonly JsonSerializerSettings Json = new()
        {
            // 默认值
            DefaultValueHandling = DefaultValueHandling.Include,

            // 驼峰命名
            ContractResolver = new CamelCasePropertyNamesContractResolver(),

            // 不要循环序列化
            ReferenceLoopHandling = ReferenceLoopHandling.Error
        };

        public HttpClientHelper()
        {
            GetInstance();
        }

        private static void GetInstance()
        {
            if (_client == null)
            {
                lock (LockObj)
                {
                    if (_client == null)
                    {
                        var handler = new HttpClientHandler {Proxy = new WebProxy(Config.HttpProxy)};
                        _client = new HttpClient(handler) {BaseAddress = new Uri(Config.Url)};

                        // _client.DefaultRequestHeaders.Host = Config.Host;
                        _client.DefaultRequestHeaders.Add("accept", Config.Accept);
                        _client.DefaultRequestHeaders.Add("user-agent", Config.Agent);
                        _client.DefaultRequestHeaders.Add("api-key", Config.ApiKey);
                        _client.DefaultRequestHeaders.Add("app-channel", Config.AppChannel);
                        _client.DefaultRequestHeaders.Add("time", Config.Now);
                        _client.DefaultRequestHeaders.Add("app-uuid", Config.Uuid);
                        _client.DefaultRequestHeaders.Add("nonce", Config.Nonce);
                        // nonce = "c74f6b365c8411eb97cf3c7c3f156854"
                        _client.DefaultRequestHeaders.Add("app-version", Config.Version);
                        _client.DefaultRequestHeaders.Add("image-quality", Config.ImageQuality.ToString());
                        _client.DefaultRequestHeaders.Add("app-platform", Config.Platform);
                        _client.DefaultRequestHeaders.Add("app-build-version", Config.BuildVersion);
                    }
                }
            }
        }

        /// <summary>
        /// 异步Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<string?> PostAsync(string url, object? obj) //post异步请求方法
        {
            if (_client is null)
            {
                throw new NullReferenceException();
            }

            StringContent? content = null;
            if (obj is not null)
            {
                var strJon = JsonConvert.SerializeObject(obj, Json);
                content = new StringContent(strJon, Encoding.UTF8, "application/json");
            }

            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(url, Method.POST));

            var res = await _client.PostAsync(url, content);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return await res.Content.ReadAsStringAsync();
            }

            return null;
        }

        /// <summary>
        /// 同步Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public string? Post(string url, object? obj) //post同步请求方法
        {
            if (_client is null)
            {
                throw new NullReferenceException();
            }

            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(url, Method.POST));

            StringContent? content = null;
            if (obj is not null)
            {
                var strJon = JsonConvert.SerializeObject(obj, Json);
                content = new StringContent(strJon, Encoding.UTF8, "application/json");
            }

            Task<HttpResponseMessage> res = _client.PostAsync(url, content);
            if (res.Result.StatusCode == HttpStatusCode.OK)
            {
                return res.Result.Content.ReadAsStringAsync().Result;
            }

            return null;
        }

        /// <summary>
        /// 异步Get
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<string?> GetAsync(string url)
        {
            if (_client is null)
            {
                throw new NullReferenceException();
            }

            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(url, Method.GET));
            _client.DefaultRequestHeaders.Connection.Add("keep-alive");

            var res = await _client.GetAsync(url);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return await res.Content.ReadAsStringAsync();
            }

            return null;
        }

        /// <summary>
        /// 异步GetString
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public string? GetString(string url)
        {
            if (_client is null)
            {
                throw new NullReferenceException();
            }

            var responseString = _client.GetStringAsync(url);
            return responseString.Result;
        }
    }
}