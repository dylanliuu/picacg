using System.Collections.Generic;
using System.Threading.Tasks;
using PicACG.Models.Request;
using PicACG.Models.Response;

namespace PicACG.Services
{
    public interface IRequest
    {
        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        Task<InitializationInfo?> Init();

        /// <summary>
        /// 获取Image的Key
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult?> InitAndroid();

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SignIn(SignIn input);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task Register(Register user);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        Task<Profile?> GetUserInfo();

        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        Task<bool> PunchIn();

        /// <summary>
        /// 获取目录分类
        /// </summary>
        /// <returns></returns>
        Task<Category?> GetCategories();

        /// <summary>
        /// 获取个人收藏
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<ICollection<Favorite>?> GetFavorites(int page);

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="comicId"></param>
        /// <returns></returns>
        Task<bool> AddFavorite(string comicId);

        /// <summary>
        /// 分类搜索
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        Task CategoriesSearch(int page, string sort, IEnumerable<Category> categories);

        /// <summary>
        /// 高级分类搜索
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="categories"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task AdvancedSearch(int page, string sort, IEnumerable<Category> categories, string keyword);

        /// <summary>
        /// 排行榜
        /// </summary>
        /// <param name="tt"></param>
        /// <returns></returns>
        Task GetRank(string tt);

        /// <summary>
        /// 获取漫画
        /// </summary>
        /// <param name="comicId"></param>
        /// <returns></returns>
        Task<Comic?> GetComic(string comicId);

        /// <summary>
        /// 获取漫画章节列表
        /// </summary>
        /// <param name="comicId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<ComicEps?> GetComicEps(string comicId, int page);

        /// <summary>
        /// 获取某章节的图片信息
        /// </summary>
        /// <param name="comicId"></param>
        /// <param name="epsId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task GetComicOrder(string comicId, string epsId, int page);

        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="comicId"></param>
        /// <param name="pag"></param>
        /// <returns></returns>
        Task GetComments(string comicId, int pag);

        /// <summary>
        /// 下载漫画(本软件）
        /// </summary>
        /// <returns></returns>
        Task DownloadComic();

        /// <summary>
        /// 获取更新(本软件）
        /// </summary>
        /// <returns></returns>
        Task CheckUpdate();
    }
}