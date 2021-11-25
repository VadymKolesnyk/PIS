using RegistrationSystem.DAL.Entities.Enums;

namespace RegistrationSystem.BLL.Dto.User
{
    public record UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public string Login { get; set; }
    }
}
