using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PicACG.Models.Request;
using PicACG.Models.Response;

namespace PicACG.Services
{
    public class Request : IRequest
    {
        private readonly HttpClient _client;

        public Request()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(Config.Url),
                DefaultRequestHeaders =
                {
                    Accept = {new MediaTypeWithQualityHeaderValue(Config.Accept)},
                    UserAgent = {new ProductInfoHeaderValue(Config.Agent)}
                }
            };

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

        public async Task<Initialization?> Init()
        {
            _client.BaseAddress = new Uri(Config.BaseUrl);
            var response = await _client.GetAsync("init");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                var initialization = JsonConvert.DeserializeObject<Initialization>(jsonStr);
                if (initialization is not null)
                {
                    return initialization;
                }
            }

            return null;
        }

        public async Task<Initialization?> InitAndroid()
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.GET));
            var response = await _client.GetAsync("init?platform=android");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                var initialization = JsonConvert.DeserializeObject<Initialization>(jsonStr);
                if (initialization is not null)
                {
                    return initialization;
                }
            }

            return null;
        }

        public async Task<bool> SignIn(SignIn input)
        {
            var content = JsonConvert.SerializeObject(input);
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new NullReferenceException();
            }

            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("auth/sign-in", new StringContent(content));
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                return true;
            }

            return false;
        }

        public async Task<Profile?> GetUserInfo()
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.GET));
            var response = await _client.GetAsync("users/profile");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                var profile = JsonConvert.DeserializeObject<Profile>(jsonStr);
                if (profile is not null)
                {
                    return profile;
                }
            }

            return null;
        }

        public async Task<bool> PunchIn()
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("users/punch-in", null);
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                return true;
            }

            return false;
        }
        
        public async Task<Category?> GetCategories()
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.GET));
            var response = await _client.GetAsync("categories");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                var category = JsonConvert.DeserializeObject<Category>(jsonStr);
                if (category is not null)
                {
                    return category;
                }
            }

            return null;
        }
        
        public Task<Favorite> GetFavorites()
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("auth/sign-in", new StringContent(content));
            throw new NotImplementedException();
        }
        
        public Task<bool> AddFavorite(Favorite favorite)
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("auth/sign-in", new StringContent(content));
            throw new NotImplementedException();
        }
        
        public Task Register(Register user)
        {
            throw new NotImplementedException();
        }

        public Task CategoriesSearch()
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("auth/sign-in", new StringContent(content));
            throw new NotImplementedException();
        }

        public Task CheckUpdate()
        {
            throw new NotImplementedException();
        }

        public Task GetBook()
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("auth/sign-in", new StringContent(content));
            throw new NotImplementedException();
        }

        public Task<Comic> GetComic(string comicId)
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("auth/sign-in", new StringContent(content));
            throw new NotImplementedException();
        }

        public Task<ComicEps> GetComicEps(string comicId, string page)
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("auth/sign-in", new StringContent(content));
            throw new NotImplementedException();
        }

        public Task GetComicOrder(string comicId, string epsId, string page)
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("auth/sign-in", new StringContent(content));
            throw new NotImplementedException();
        }

        public Task GetComments()
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("auth/sign-in", new StringContent(content));
            throw new NotImplementedException();
        }

        public Task GetRank()
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("auth/sign-in", new StringContent(content));
            throw new NotImplementedException();
        }

        public Task AdvancedSearch()
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("auth/sign-in", new StringContent(content));
            throw new NotImplementedException();
        }
    }
}