using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LawAgendaApi.Data;
using LawAgendaApi.Data.Entities;
using LawAgendaApi.Data.Queries.Search;

namespace LawAgendaApi.Repositories.UserRepo
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUserById(long id);

        Task<IEnumerable<User>> Search(SearchQuery<UserSearchQueryType> query, bool singleFilter);
        Task<SaveChangesToDbResult<User>> EditProfile(User user);
        
        

    }
}