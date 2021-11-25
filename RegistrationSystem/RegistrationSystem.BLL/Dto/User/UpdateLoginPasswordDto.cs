namespace RegistrationSystem.BLL.Dto.User
{
    public record UpdateLoginPasswordDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
