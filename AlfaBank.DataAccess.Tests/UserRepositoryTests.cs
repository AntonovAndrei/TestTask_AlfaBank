using AlfaBank.DataAccess.Models;
using AlfaBank.DataAccess.Repositories;

namespace AlfaBank.DataAccess.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public void Add_CheckAddingWithGet()
        {
            var repository = new UserRepository();
            var addingUser = new User()
            {
                FullName = "Test user",
                Login = "testLogin",
                RegistrationDate = DateTime.Now.AddYears(10),
                IsDeleted = false
            };

            repository.Add(addingUser);
            var actualUser = repository.Get(addingUser.FullName);

            Assert.Equal(addingUser.Login, actualUser.Login);
        }
    }
}
