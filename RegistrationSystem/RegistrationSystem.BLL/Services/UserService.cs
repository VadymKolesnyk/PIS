using Microsoft.EntityFrameworkCore;
using RegistrationSystem.BLL.Dto.User;
using RegistrationSystem.BLL.Helpers;
using RegistrationSystem.BLL.Interfases;
using RegistrationSystem.DAL;
using RegistrationSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationSystem.BLL.Services
{
    public class UserService : IUserServise
    {
        private readonly RegistrationSystemContext _context;

        public UserService(RegistrationSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CheckLogingPasswordAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == dto.Login);
            return user is not null && PasswordHelper.VerifyHashedPassword(user.HashedPassword, dto.Password);
        }

        public async Task<UserDto> CreateUserAsync(NewUserDto dto)
        {
            await ValidateLogin(dto.Login);
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Login = dto.Login,
                Role = dto.Role,
                HashedPassword = PasswordHelper.HashPassword(dto.Password)
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName, 
                Login = user.Login,
                Role = user.Role,
            };
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var user = (await _context.Users.FirstOrDefaultAsync(x => x.Id == id)) ?? throw new KeyNotFoundException($"User with id {id} doesn't exists");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users.Select(user => new UserDto
            {
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Login = user.Login,
                Role = user.Role,
            }).ToListAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = (await _context.Users.FirstOrDefaultAsync(x => x.Id == id)) ?? throw new KeyNotFoundException($"User with id {id} doesn't exists");
            return new UserDto
            {
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Login = user.Login,
                Role = user.Role,
            };
        }

        public async Task<UserDto> UpdateLoginPasswordAsync(UpdateLoginPasswordDto dto)
        {
            await ValidateLogin(dto.Login);
            var user = (await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.Id)) ?? throw new KeyNotFoundException($"User with id {dto.Id} doesn't exists");
            user.Login = dto.Login;
            user.HashedPassword = PasswordHelper.HashPassword(dto.Password);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Login = user.Login,
                Role = user.Role,
            };
        }

        public async Task<UserDto> UpdateUserAsync(UpdateUserDto dto)
        {
            var user = (await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.Id)) ?? throw new KeyNotFoundException($"User with id {dto.Id} doesn't exists");
            _context.Entry(user).CurrentValues.SetValues(dto);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Login = user.Login,
                Role = user.Role,
            };
        }

        private async Task ValidateLogin(string login)
        {
            if (await _context.Users.AnyAsync(x => x.Login == login))
            {
                throw new ArgumentException($"Login {login} alredy exists");
            }
        }
    }
}
