using AlfaBank.DataAccess.Models;
using AlfaBank.DataAccess.Repositories;

namespace AlfaBank.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var db = new UserRepository();

            Console.WriteLine("Программа начала свою работу. \n" +
                "Выберите действие:\n" +
                "1 - Добавить пользователя;\n" +
                "2 - Удалить пользователя;\n" +
                "3 - Изменить информацию о пользователе;\n" +
                "4 - Выгрузить информацию о пользователях в excel;\n" +
                "5 - Просмотреть всех пользователей;\n" +
                "6 - Просмотреть пользователя по id;\n" +
                "7 - Выход.");

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                var command = Console.ReadLine().Trim();

                if(command == "1")
                {
                    var setUser = GetUserFromConsole();

                    db.Add(setUser);
                }
                else if(command == "2")
                {
                    var u = db.Delete(GetIdFromConsole());
                    if(u == null) Console.WriteLine("В бд нет пользователя с таким id.");
                    else Console.WriteLine("Удален:" + u.Id + ":" + u.FullName + " " +
                            u.Login + " " + u.RegistrationDate + " " + u.IsDeleted);
                }
                else if (command == "3")
                {
                    var id = GetIdFromConsole();
                    var setUser = GetUserFromConsole();
                    setUser.Id = id;

                    db.Update(setUser);
                }
                else if( command == "4")
                {
                    var users = db.GetAll();

                    if (users == null) Console.WriteLine("В бд нет пользователей.");
                    else SaveToExcel(users);
                }
                else if(command == "5")
                {
                    var users = db.GetAll();

                    if (users == null) 
                    { 
                        Console.WriteLine("В бд нет пользователей."); 
                    }
                    else
                    {
                        foreach (var u in users)
                        {
                            Console.WriteLine(u.Id + ":" + u.FullName + " " +
                                u.Login + " " + u.RegistrationDate + " " + u.IsDeleted);
                        }
                    }
                }
                else if (command == "6")
                {
                    var u = db.Get(GetIdFromConsole());

                    if(u == null)
                    {
                        Console.WriteLine("В бд нет пользователя с таким id.");
                    }
                    else
                    {
                        Console.WriteLine(u.Id + ":" + u.FullName + " " +
                            u.Login + " " + u.RegistrationDate + " " + u.IsDeleted);
                    }
                }
                else if (command == "7")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неправильный ввод");
                }
            }
        }

        private static void SaveToExcel(List<User> users)
        {
            var projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
            var directory = Path.Combine(projectDir, "Excels");


            var fileName = "users.xlsx";

            string path = Path.Combine(directory, fileName);

            ExcelParser.SaveUsersToExcel(users, path);

            Console.WriteLine("Путь к файлу:" + path);
        }

        private static User GetUserFromConsole()
        {
            var user = new User();

            while (true)
            {
                try
                {
                    Console.WriteLine("Введите ФИО:");
                    user.FullName = Console.ReadLine();
                    Console.WriteLine("Введите логин:");
                    user.Login = Console.ReadLine();
                    Console.WriteLine("Введите дату в формате: 05/11/1996:");
                    user.RegistrationDate = Convert.ToDateTime(Console.ReadLine());
                    user.IsDeleted = false;

                    return user;
                }
                catch
                {
                    Console.WriteLine("Неправильный ввод, попробуйте заново.");
                }
            }
            
        }

        private static int GetIdFromConsole()
        {
            Console.WriteLine("Введите id");
            while (true)
            {
                try
                {
                    var id = Convert.ToInt32(Console.ReadLine());
                    return id;
                }
                catch
                {
                    Console.WriteLine("Неправильный ввод, введите число.");
                }
            }
            
        }
    }
}