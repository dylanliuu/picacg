#nullable enable
using PicACG.Models;
using System.Threading.Tasks;
using PicACG.Models.Request;
using PicACG.Models.Response;

namespace PicACG.Services
{
    public interface IRequest
    {
        Task<Initialization?> Init();
        Task<Initialization?> InitAndroid();
        Task<bool> SignIn(SignIn input);
        Task Register(Register user);
        Task<Profile?> GetUserInfo();
        Task<bool> PunchIn();
        Task<Category?> GetCategories();
        Task<Favorite?> GetFavorites();
        Task<bool> AddFavorite(Favorite favorite);
        Task AdvancedSearch();
        Task CategoriesSearch();
        Task GetRank();
        Task<Comic?> GetComic(string comicId);
        Task<ComicEps?> GetComicEps(string comicId, string page);
        Task GetComicOrder(string comicId, string epsId, string page);
        Task GetBook();
        Task GetComments();
        Task CheckUpdate();
    }
}
