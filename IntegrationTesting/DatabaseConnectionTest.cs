using Xunit;
using IIG.CoSFE.DatabaseUtils;
using IIG.PasswordHashingUtils;


namespace IntegrationTesting
{
    public class DatabaseConnectionTest
    {
        private const string Server = @"WIN-J6V8NPHEMEJ\LAB4";
        private const string Database = @"IIG.CoSWE.AuthDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"1969";
        private const int ConnectionTimeout = 75;
        private AuthDatabaseUtils authDatabaseUtils = new AuthDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTimeout);

        [Fact]
        public void Add_Check_Delete_Credentials()
        {
            string password = "password";
            string login = "user";
            string hash = PasswordHasher.GetHash(password);

            bool add = authDatabaseUtils.AddCredentials(login, hash);
            Assert.True(add);
            bool check = authDatabaseUtils.CheckCredentials(login, hash);
            Assert.True(check);
            bool delete = authDatabaseUtils.DeleteCredentials(login, hash);
            Assert.True(delete);
        }

        [Fact]
        public void Unique_Login_Condition()
        {
            string password = "password";
            string login = "user1";
            string hash = PasswordHasher.GetHash(password);

            bool add = authDatabaseUtils.AddCredentials(login, hash);
            Assert.True(add);
            bool add2 = authDatabaseUtils.AddCredentials(login, hash);
            Assert.False(add2);
        }

        [Fact]
        public void UpdateCredentials()
        {
            string password2 = "password2";
            string login2 = "user2";
            string hash2 = PasswordHasher.GetHash(password2);

            string password3 = "password3";
            string login3 = "user3";
            string hash3 = PasswordHasher.GetHash(password3);

            authDatabaseUtils.AddCredentials(login2, hash2);
            bool update = authDatabaseUtils.UpdateCredentials(login2, hash2, login3, hash3);
            Assert.True(update);
            bool check1 = authDatabaseUtils.CheckCredentials(login2, hash2);
            Assert.False(check1);
            bool check2 = authDatabaseUtils.CheckCredentials(login3, hash3);
            Assert.True(check2);
        }

        
        [Fact]
        public void FailedToAdd_PasswordIsNull()
        {
            string password = null;
            string login = "user4";
            string hash = PasswordHasher.GetHash(password);

            authDatabaseUtils.AddCredentials(login, hash);
            bool check = authDatabaseUtils.CheckCredentials(login, hash);
            Assert.False(check);
        }

        [Fact]
        public void FailedToAdd_LoginIsNull()
        {
            string password = "password";
            string login = null;
            string hash = PasswordHasher.GetHash(password);

            authDatabaseUtils.AddCredentials(login, hash);
            bool check = authDatabaseUtils.CheckCredentials(login, hash);
            Assert.False(check);
        }

        [Fact]
        public void FailedToAdd_LoginIsEmpty()
        {
            string password = "password";
            string login = "";
            string hash = PasswordHasher.GetHash(password);

            authDatabaseUtils.AddCredentials(login, hash);
            bool check = authDatabaseUtils.CheckCredentials(login, hash);
            Assert.False(check);
        }

        [Fact]
        public void Add_LoginOverflowException()
        {
            string password = "password";
            string login = "пользователь5";
            string hash = PasswordHasher.GetHash(password);

            authDatabaseUtils.AddCredentials(login, hash);
            bool check = authDatabaseUtils.CheckCredentials(login, hash);
            Assert.True(check);
        }
    }
}
