using Microsoft.AspNetCore.Mvc;
using RegistrationSystem.BLL.Dto.User;
using RegistrationSystem.BLL.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegistrationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServise _userServise;

        public UsersController(IUserServise userServise)
        {
            _userServise = userServise ?? throw new ArgumentNullException(nameof(userServise));
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ICollection<UserDto>> GetAll() => await _userServise.GetAllUsersAsync();

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<UserDto> GetById(int id) => await _userServise.GetUserByIdAsync(id);

        // POST api/<UsersController>
        [HttpPost]
        public async Task<UserDto> CreateUser([FromBody] NewUserDto dto) => await _userServise.CreateUserAsync(dto);

        // PUT api/<UsersController>/5
        [HttpPut]
        public async Task<UserDto> UpdateUser([FromBody] UpdateUserDto dto) => await _userServise.UpdateUserAsync(dto);

        // PUT api/<UsersController>/5
        [HttpPut("login")]
        public async Task<UserDto> UpdateUser([FromBody] UpdateLoginPasswordDto dto) => await _userServise.UpdateLoginPasswordAsync(dto);

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id) => await _userServise.DeleteUserByIdAsync(id);
    }
}
