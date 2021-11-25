using Microsoft.EntityFrameworkCore;
using RegistrationSystem.DAL.Entities;

namespace RegistrationSystem.DAL
{
    public class RegistrationSystemContext : DbContext
    {
        public DbSet<User> Users { get; private set; }

        public RegistrationSystemContext(DbContextOptions<RegistrationSystemContext> options) : base(options)
        {
        }

    }
}
