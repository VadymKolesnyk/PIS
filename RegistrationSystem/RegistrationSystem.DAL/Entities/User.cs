using RegistrationSystem.DAL.Entities.Enums;

namespace RegistrationSystem.DAL.Entities
{
    public record User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public string Login { get; set; }
        public string HashedPassword { get; set; }
    }
}
