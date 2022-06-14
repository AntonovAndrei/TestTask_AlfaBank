using AlfaBank.DataAccess.IRepositories;
using AlfaBank.DataAccess.Repositories;

namespace AlfaBank.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();

            var users = userRepository.GetAll();

            foreach(var user in users)
            {
                Console.WriteLine(user.Id + ":" + user.FullName + " " + 
                    user.Login + " " + user.RegistrationDate + " " + user.IsDeleted);
            }
        }
    }
}