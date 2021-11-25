using RegistrationSystem.DAL.Entities.Enums;

namespace RegistrationSystem.BLL.Dto.User
{
    public record NewUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
