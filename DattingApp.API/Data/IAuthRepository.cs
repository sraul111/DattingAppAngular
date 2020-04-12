
namespace DattingApp.API.Data
{
    using System.Threading.Tasks;
    using DattingApp.API.Models;
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username); 
    }
}