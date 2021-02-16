using System;
using System.Collections.Generic;
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

                // TODO:// 返回类型
                return true;
            }

            return false;
        }

        public async Task Register(Register user)
        {
            var userJsonStr = JsonConvert.SerializeObject(user);
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync("auth/register", new StringContent(userJsonStr));
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                // TODO:// 返回类型
            }
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

                // TODO:// 返回类型
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

        public async Task<ICollection<Favorite>?> GetFavorites(int page)
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.GET));
            var response = await _client.GetAsync($"users/favourite?s=da&page={page}");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                var favorites = JsonConvert.DeserializeObject<ICollection<Favorite>>(jsonStr);
                if (favorites is not null)
                {
                    return favorites;
                }
            }

            return null;
        }

        public async Task<bool> AddFavorite(string comicId)
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response = await _client.PostAsync($"comics/{comicId}/favourite", null);
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                return true;
            }

            return false;
        }
        
        public async Task CategoriesSearch(int page, string sort, IEnumerable<Category> categories)
        {
            var categoriesJsonStr = JsonConvert.SerializeObject(categories);

            if (string.IsNullOrWhiteSpace(categoriesJsonStr))
            {
                throw new NullReferenceException();
            }

            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.GET));

            var response = await _client.GetAsync($"comics?page={page}&c={categories}&s={sort}");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                // TODO:// 返回类型
            }
        }

        public async Task AdvancedSearch(int page, string sort, IEnumerable<Category> categories, string keyword)
        {
            var reqJsonStr = JsonConvert.SerializeObject(new
            {
                Categories = categories,
                Keyword = keyword,
                Sort = sort
            });

            if (string.IsNullOrWhiteSpace(reqJsonStr))
            {
                throw new NullReferenceException();
            }

            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.POST));
            var response =
                await _client.PostAsync($"comics/advanced-search?page={page}", new StringContent(reqJsonStr));
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                // TODO:// 返回类型
            }
        }
        
        public async Task GetRank(string tt)
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.GET));
            var response = await _client.GetAsync($"comics/leaderboard?tt={tt}&ct=VC");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                // TODO:// 返回类型
            }
        }

        public async Task<Comic?> GetComic(string comicId)
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.GET));
            var response = await _client.GetAsync($"comics/{comicId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                var comic = JsonConvert.DeserializeObject<Comic>(jsonStr);
                if (comic is not null)
                {
                    return comic;
                }
            }

            return null;
        }

        public async Task<ComicEps?> GetComicEps(string comicId, int page)
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.GET));
            var response = await _client.GetAsync($"comics/{comicId}/eps?page={page}");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                var comicEps = JsonConvert.DeserializeObject<ComicEps>(jsonStr);
                if (comicEps is not null)
                {
                    return comicEps;
                }
            }

            return null;
        }

        public async Task GetComicOrder(string comicId, string epsId, int page)
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.GET));
            var response = await _client.GetAsync($"comics/{comicId}/order/{epsId}/pages?page={page}");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                // TODO:// 返回类型
            }
        }

        public async Task GetComments(string comicId, int page)
        {
            _client.DefaultRequestHeaders.Add("signature", Config.GetSignature(Method.GET));
            var response = await _client.GetAsync($"comics/{comicId}/comments?page={page}");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                // TODO:// 返回类型
            }
        }

        /// <summary>
        /// TODO:// 下载功能实现
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DownloadComic()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// TODO:// 获取更新
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task CheckUpdate()
        {
            throw new NotImplementedException();
        }
    }
}