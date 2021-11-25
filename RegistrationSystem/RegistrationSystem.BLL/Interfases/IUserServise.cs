using RegistrationSystem.BLL.Dto.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegistrationSystem.BLL.Interfases
{
    public interface IUserServise
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<ICollection<UserDto>> GetAllUsersAsync();
        Task<UserDto> CreateUserAsync(NewUserDto dto);
        Task<UserDto> UpdateUserAsync(UpdateUserDto dto);
        Task<UserDto> UpdateLoginPasswordAsync(UpdateLoginPasswordDto dto);
        Task DeleteUserByIdAsync(int id);

        Task<bool> CheckLogingPasswordAsync(LoginDto dto);
    }
}
