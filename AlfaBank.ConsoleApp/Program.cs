﻿using AlfaBank.DataAccess.IRepositories;
using AlfaBank.DataAccess.Models;
using AlfaBank.DataAccess.Repositories;

namespace AlfaBank.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();


            var setUser = new User()
            {
                FullName = "Haritonov Gosha Mironovich",
                Login = "gosha@google.com",
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
                    user.Login + " " + user.RegistrationDate + " " + user.IsDeleted);




            var users = userRepository.GetAll();

            foreach (var u in users)
            {
                Console.WriteLine(u.Id + ":" + u.FullName + " " +
                    u.Login + " " + u.RegistrationDate + " " + u.IsDeleted);
            }

            string path = @"D:\myFile.xlsx";

            ExcelParser.SaveUsersToExcel(users, path);



























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