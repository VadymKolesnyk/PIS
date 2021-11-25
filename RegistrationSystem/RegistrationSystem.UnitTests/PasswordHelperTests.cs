
using RegistrationSystem.BLL.Helpers;
using System;
using Xunit;

namespace RegistrationSystem.UnitTests
{
    public class PasswordHelperTests
    {
        [Fact]
        public void HashPassword_WhenHashPassword_ThenPasswordIsNotEqualsToHashedPassword()
        {
            var password = "mySuperPassword123";

            var hashedPassword = PasswordHelper.HashPassword(password);

            Assert.NotEqual(password, hashedPassword);
        }

        [Fact]
        public void HashPassword_WhenNull_ThenThrowsArgumentExeption()
        {
            string password = null;

            Assert.Throws<ArgumentNullException>(() => PasswordHelper.HashPassword(password));
        }

        [Fact]
        public void VerifyHashedPassword_WhenRightPassword_ThenTrue()
        {
            string password = "qwerty123";

            var hashedPassword = PasswordHelper.HashPassword(password);

            Assert.True(PasswordHelper.VerifyHashedPassword(hashedPassword, password));
        }

        [Fact]
        public void VerifyHashedPassword_WhenNotRightPassword_ThenFalse()
        {
            string password1 = "admin1234";
            string password2 = "admin1233";

            var hashedPassword = PasswordHelper.HashPassword(password1);

            Assert.False(PasswordHelper.VerifyHashedPassword(hashedPassword, password2));
        }
    }
}
