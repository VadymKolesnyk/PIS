using Microsoft.EntityFrameworkCore;
using RegistrationSystem.BLL.Services;
using RegistrationSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.UnitTests
{
    public class UserServiceTests
    {
        private RegistrationSystemContext _context;
        private UserService _service;

        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<RegistrationSystemContext>().UseInMemoryDatabase("TestUserDatabase").Options;
            _context = new RegistrationSystemContext(options);
            _context.Database.EnsureCreated();
            _service = new UserService(_context);
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


    }
}
