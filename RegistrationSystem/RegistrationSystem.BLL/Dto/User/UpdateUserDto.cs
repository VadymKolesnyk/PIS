using RegistrationSystem.DAL.Entities.Enums;

namespace RegistrationSystem.BLL.Dto.User
{
    public record UpdateUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
    }
}
