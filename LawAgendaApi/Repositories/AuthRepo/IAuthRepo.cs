using System.Threading.Tasks;
using LawAgendaApi.Data.Entities;

namespace LawAgendaApi.Repositories.AuthRepo
{
    public interface IAuthRepo
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}