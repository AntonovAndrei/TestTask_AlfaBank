using AlfaBank.DataAccess.IRepositories;
using AlfaBank.DataAccess.Models;
using AlfaBank.DataAccess.Repositories;

namespace AlfaBank.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();

            userRepository.Add(new User()
            {
                FullName = "Leskov Pasha Sergeevich",
                Login = "pasha@yandex.ru",
                RegistrationDate = DateTime.Now,
                IsDeleted = false
            });

            var users = userRepository.GetAll();

            foreach (var user in users)
            {
                Console.WriteLine(user.Id + ":" + user.FullName + " " +
                    user.Login + " " + user.RegistrationDate + " " + user.IsDeleted);
            }

            string path = @"D:\myFile.xlsx";

            ExcelParser.SaveUsersToExcel(users, path);

            /*var setUser = new User()
            {
                FullName = "Leskov Gena Yurievich",
                Login = "gena@google.com",
                RegistrationDate = DateTime.Now,
                IsDeleted = false
            };

            userRepository.Add(setUser);

            var user = userRepository.Get(setUser.FullName);

            Console.WriteLine(user.Id + ":" + user.FullName + " " +
                    user.Login + " " + user.RegistrationDate + " " + user.IsDeleted);

            userRepository.Delete(user);

            user = userRepository.Get(setUser.FullName);

            Console.WriteLine(user.Id + ":" + user.FullName + " " +
                    user.Login + " " + user.RegistrationDate + " " + user.IsDeleted);*/

            /*userRepository.AddRange(new List<User>()
            {
                new User()
                {
                FullName = "Leskov Gena Yurievich",
                Login = "gena@google.com",
                RegistrationDate = DateTime.Now,
                IsDeleted = false
                },
                new User()
            {
                FullName = "Leskov Pasha Sergeevich",
                Login = "pasha@yandex.ru",
                RegistrationDate = DateTime.Now,
                IsDeleted = false
            }
            });*/
        }
    }
}