using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LawAgendaApi.Data;
using LawAgendaApi.Data.Entities;
using LawAgendaApi.Data.Queries.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace LawAgendaApi.Repositories.UserRepo
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _context;

        public UserRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
                .Include(u => u.Type)
                .Include(u => u.Avatar.Type)
                .ToListAsync();
        }

        public async Task<User> GetUserById(long id)
        {
            var users = await GetAll();

            var user = users.FirstOrDefault(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> Search(SearchQuery<UserSearchQueryType> query, bool singleFilter)
        {
            if (string.IsNullOrEmpty(query.Query))
            {
                return null;
            }

            var users = (await GetAll()).AsQueryable();

            if (singleFilter)
            {
                switch (query.Filter)
                {
                    case UserSearchQueryType.Name:
                        users = users.Where(u => EF.Functions.Like(u.Name, $"%{query.Query}%"));
                        break;
                    case UserSearchQueryType.Username:
                        users = users.Where(u => EF.Functions.Like(u.Username, $"%{query.Query}%"));
                        break;
                    case UserSearchQueryType.PhoneNumber:
                        users = users.Where(u => EF.Functions.Like(u.PhoneNumber, $"%{query.Query}%")) ??
                                users.Where(u => EF.Functions.Like(u.PhoneNumber2, $"%{query.Query}%"));

                        break;
                    default:
                        users = null;
                        break;
                }

                return users;
            }

            users = users.Where(u => EF.Functions.Like(u.Name, $"%{query.Query}%") ||
                                     EF.Functions.Like(u.Username, $"%{query.Query}%") ||
                                     EF.Functions.Like(u.PhoneNumber, $"%{query.Query}%") ||
                                     EF.Functions.Like(u.PhoneNumber2, $"%{query.Query}%"));
            return users;
        }

        public async Task<SaveChangesToDbResult<User>> EditProfile(User user)
        {
            var users = await GetAll();

            var userFromDb = users.FirstOrDefault(u => u.Id == user.Id);

            var result = new SaveChangesToDbResult<User>();

            if (userFromDb == null)
            {
                result.Message = "Not Found";
                return null;
            }

            var updateResult = _context.Update(user);

            var isSaved = await _context.SaveChangesAsync() > 0;

            if (!isSaved)
            {
                result.Message = "Could not Save Changes";
                return null;
            }

            result.Message = "Success";
            result.Entity = updateResult.Entity;
            return result;
        }
    }
}