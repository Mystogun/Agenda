using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LawAgendaApi.Data;
using LawAgendaApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LawAgendaApi.Repositories.AuthRepo
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;

        public AuthRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);

            user.Username = user.Username.ToLower();
            await _context.Users.AddAsync(user);

            var isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved)
            {
                user.Type = await _context.A1UserTypes.FirstOrDefaultAsync(t => t.Id == user.TypeId);
                return user;

            }
            
            return null;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.Type)
                .Include(u => u.Avatar)
                .Include(u => u.Avatar.Type)
                .FirstOrDefaultAsync(u => u.Username == username.ToLower());

            if (user == null)
            {
                return null;
            }
            
            var passwordHash = Convert.FromBase64String(user.PasswordHash);
            var passwordSalt = Convert.FromBase64String(user.PasswordSalt);
            
            var isVerified = VerifyPasswordHash(password, passwordHash, passwordSalt);

            if (!isVerified)
            {
                user = null;
            }
            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return false;
            }

            return true;
        }
        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (var i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}