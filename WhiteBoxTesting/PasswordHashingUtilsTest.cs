using System;
using Xunit;
using IIG.PasswordHashingUtils;

namespace WhiteBoxTesting
{
    public class PasswordHashingUtilsTest
    {
        [Fact]
        public void Init_Explicitly()
        {
            string password = "password";
            string salt = "salt";
            uint adlerMod32 = 32;

            PasswordHasher.Init(salt, adlerMod32);
            var res1 = PasswordHasher.GetHash(password);
            var res2 = PasswordHasher.GetHash(password, salt, adlerMod32);

            Assert.Equal(res1, res2);
        }

        [Fact]
        public void GetHash_Separately()
        {
            string password = "password";
            string salt = "salt";
            uint adlerMod32 = 32;

            var res = PasswordHasher.GetHash(password, salt, adlerMod32);

            Assert.IsType<string>(res);
            Assert.True(res.Length != 0);
        }

        [Fact]
        public void GetHash_PasswordIsNull()
        {
            string password = null;
            string salt = "salt";
            uint adlerMod32 = 32;

            var res = PasswordHasher.GetHash(password, salt, adlerMod32);

            Assert.Null(res);
        }

        [Fact]
        public void GetHash_PasswordIsEmpty()
        {
            string password = "";
            string salt = "salt";
            uint adlerMod32 = 32;

            var res = PasswordHasher.GetHash(password, salt, adlerMod32);

            Assert.IsType<string>(res);
            Assert.True(res.Length != 0);
        }

        [Fact]
        public void GetHash_PasswordOverflowException()
        {
            string password = "пароль";
            string salt = "salt";
            uint adlerMod32 = 0;

            var res = PasswordHasher.GetHash(password, salt, adlerMod32);

            Assert.IsType<string>(res);
            Assert.True(res.Length != 0);
        }

        [Fact]
        public void GetHash_SaltIsNull()
        {
            string password = "password";
            string salt = null;
            uint adlerMod32 = 32;

            var res = PasswordHasher.GetHash(password, salt, adlerMod32);

            Assert.IsType<string>(res);
            Assert.True(res.Length != 0);
        }

        [Fact]
        public void GetHash_SaltIsEmpty()
        {
            string password = "password";
            string salt = "";
            uint adlerMod32 = 32;

            var res = PasswordHasher.GetHash(password, salt, adlerMod32);

            Assert.IsType<string>(res);
            Assert.True(res.Length != 0);
        }

        [Fact]
        public void GetHash_SaltOverflowException()
        {
            string password = "password";
            string salt = "соль";
            uint adlerMod32 = 32;

            var res = PasswordHasher.GetHash(password, salt, adlerMod32);

            Assert.IsType<string>(res);
            Assert.True(res.Length != 0);
        }

        [Fact]
        public void GetHash_AdlerModIsNull()
        {
            string password = "password";
            string salt = "salt";
            uint? adlerMod32 = null;

            var res = PasswordHasher.GetHash(password, salt, adlerMod32);

            Assert.IsType<string>(res);
            Assert.True(res.Length != 0);
        }

        [Fact]
        public void GetHash_AdlerModIsZero()
        {
            string password = "password";
            string salt = "salt";
            uint adlerMod32 = 0;

            var res = PasswordHasher.GetHash(password, salt, adlerMod32);

            Assert.IsType<string>(res);
            Assert.True(res.Length != 0);
        }
    }
}
