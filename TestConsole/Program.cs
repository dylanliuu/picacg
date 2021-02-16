using System.Threading.Tasks;
using PicACG.Models.Request;
using PicACG.Services;

namespace TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service = new Request();
            
            var input = new SignIn
            {
                email = "abc",
                password = "abc"
            };
            
            await service.SignIn(input);
        }
    }
}