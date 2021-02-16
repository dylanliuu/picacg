using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PicACG.Models.Request;
using PicACG.Models.Response;

namespace PicACG.Services
{
    public class Request : IRequest
    {
        private static readonly HttpClientHelper Client = new();

        public async Task<InitializationInfo?> Init()
        {
            var handler = new HttpClientHandler {Proxy = new WebProxy(Config.HttpProxy)};
            var client = new HttpClient(handler) {BaseAddress = new Uri(Config.BaseUrl)};
            var response = await client.GetAsync("init");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                var initialization = JsonConvert.DeserializeObject<InitializationInfo>(jsonStr);
                if (initialization is not null)
                {
                    return initialization;
                }
            }

            return null;
        }

        public async Task<ResponseResult?> InitAndroid()
        {
            var uri = "init?platform=android";
            var jsonStr = await Client.GetAsync(uri);
            if (jsonStr is not null)
            {
                var result = JsonConvert.DeserializeObject<ResponseResult>(jsonStr);
                if (result is not null)
                {
                    return result;
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

            var uri = "auth/sign-in";

            var jsonStr1 = await Client.PostAsync(uri, content);

            return true;
        }

        public async Task Register(Register user)
        {
            var uri = "auth/register";
            var userJsonStr = JsonConvert.SerializeObject(user);

            var jsonStr = await Client.PostAsync(uri, userJsonStr);
            if (jsonStr is not null)
            {
                // TODO:// 返回类型
            }
        }

        public async Task<Profile?> GetUserInfo()
        {
            var uri = "users/profile";

            var jsonStr = await Client.GetAsync(uri);
            if (jsonStr is not null)
            {
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
            var uri = "users/punch-in";

            var jsonStr = await Client.PostAsync(uri, null);
            if (jsonStr is not null)
            {
                // TODO:// 返回类型
                return true;
            }

            return false;
        }

        public async Task<Category?> GetCategories()
        {
            var uri = "categories";

            var jsonStr = await Client.GetAsync(uri);
            if (jsonStr is not null)
            {
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
            var uri = $"users/favourite?s=da&page={page}";

            var jsonStr = await Client.GetAsync(uri);
            if (jsonStr is not null)
            {
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
            var uri = $"comics/{comicId}/favourite";

            var jsonStr = await Client.PostAsync(uri, null);

            if (jsonStr is not null)
            {
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

            var uri = $"comics?page={page}&c={categories}&s={sort}";

            var jsonStr = await Client.GetAsync(uri);
            if (jsonStr is not null)
            {
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

            var uri = $"comics/advanced-search?page={page}";

            var jsonStr = await Client.PostAsync(uri, reqJsonStr);
            if (jsonStr is not null)
            {
                // TODO:// 返回类型
            }
        }

        public async Task GetRank(string tt)
        {
            var uri = $"comics/leaderboard?tt={tt}&ct=VC";

            var jsonStr = await Client.GetAsync(uri);
            if (jsonStr is not null)
            {
                // TODO:// 返回类型
            }
        }

        public async Task<Comic?> GetComic(string comicId)
        {
            var uri = $"comics/{comicId}";

            var jsonStr = await Client.GetAsync(uri);
            if (jsonStr is not null)
            {
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
            var uri = $"comics/{comicId}/eps?page={page}";

            var jsonStr = await Client.GetAsync(uri);
            if (jsonStr is not null)
            {
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
            var uri = $"comics/{comicId}/order/{epsId}/pages?page={page}";

            var jsonStr = await Client.GetAsync(uri);
            if (jsonStr is not null)
            {
                // TODO:// 返回类型
            }
        }

        public async Task GetComments(string comicId, int page)
        {
            var uri = $"comics/{comicId}/comments?page={page}";

            var jsonStr = await Client.GetAsync(uri);
            if (jsonStr is not null)
            {
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