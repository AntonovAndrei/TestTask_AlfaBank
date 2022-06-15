using AlfaBank.DataAccess.IRepositories;
using AlfaBank.DataAccess.Models;
using System.Data.SQLite;

namespace AlfaBank.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Database _db;

        public UserRepository()
        {
            _db = new Database();
        }

        public void Add(User user)
        {
            //if(IsUserExist(user)) { throw new Exception("Such a user already exists"); }
            if (IsUserExist(user))
            {
                Console.WriteLine("Error! Such a user already exists");
                return;
            }
            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Users (FullName, Login, RegistrationDate) VALUES " +
                        "(@FullName, @Login, @RegistrationDate)";
                    command.Parameters.Add(new SQLiteParameter("@FullName", user.FullName));
                    command.Parameters.Add(new SQLiteParameter("@Login", user.Login));
                    command.Parameters.Add(new SQLiteParameter("@RegistrationDate", user.RegistrationDate));

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task AddAsync(User user)
        {
            //if (IsUserExist(user)) { throw new Exception("Such a user already exists"); }
            if (IsUserExist(user)) 
            { 
                Console.WriteLine("Error! Such a user already exists");
                return;
            }
            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Users (FullName, Login, RegistrationDate) VALUES " +
                        "(@FullName, @Login, @RegistrationDate)";
                    command.Parameters.Add(new SQLiteParameter("@FullName", user.FullName));
                    command.Parameters.Add(new SQLiteParameter("@Login", user.Login));
                    command.Parameters.Add(new SQLiteParameter("@RegistrationDate", user.RegistrationDate));

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
                
        public User Get(int id)
        {
            var user = new User();

            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM Users WHERE ID = @ID";
                    command.Parameters.Add(new SQLiteParameter("@ID", id));

                    connection.Open();
                    var dataReader = command.ExecuteReader();

                    dataReader.Read();
                    try
                    {
                        user.Id = dataReader.GetInt32(0);
                        user.FullName = dataReader.GetString(1);
                        user.Login = dataReader.GetString(2);
                        user.RegistrationDate = dataReader.GetDateTime(3);
                        user.IsDeleted = dataReader.GetInt32(4) == 0 ? false : true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return user;
        }
                
        public User Get(string fullName)
        {
            var user = new User();

            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM Users WHERE FullName = @FullName";
                    command.Parameters.Add(new SQLiteParameter("@FullName", fullName));

                    connection.Open();
                    var dataReader = command.ExecuteReader();

                    dataReader.Read();
                    try
                    {
                        user.Id = dataReader.GetInt32(0);
                        user.FullName = dataReader.GetString(1);
                        user.Login = dataReader.GetString(2);
                        user.RegistrationDate = dataReader.GetDateTime(3);
                        user.IsDeleted = dataReader.GetInt32(4) == 0 ? false : true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return user;
        }
                
        public void Delete(User user)
        {
            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                { 
                    command.CommandText = "UPDATE Users SET IsDeleted = 1 WHERE FullName = @FullName";
                    command.Parameters.Add(new SQLiteParameter("@FullName", user.FullName));

                    connection.Open();
                    command.ExecuteNonQuery();
                } 
            }
        }
                
        public User Delete(int id)
        {
            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "UPDATE Users SET IsDeleted = 1 WHERE ID = @ID;";
                    command.Parameters.Add(new SQLiteParameter("@ID", id));

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return Get(id);
        }

        public List<User> GetAll()
        {
            var users = new List<User>();

            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "Select * FROM Users";

                    connection.Open();
                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        try
                        {
                            var user = new User();

                            user.Id = dataReader.GetInt32(0);
                            user.FullName = dataReader.GetString(1);
                            user.Login = dataReader.GetString(2);
                            user.RegistrationDate = dataReader.GetDateTime(3);
                            user.IsDeleted = dataReader.GetInt32(4) == 0 ? false : true;

                            users.Add(user);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            return users;                
        }

        public void Update(User user)
        {
            if (!IsUserExist(user.Id))
            {
                Console.WriteLine($"Error! There is no user with id: {user.Id}");
                return;
            }

            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"UPDATE Users 
                                            SET FullName = @FullName, Login = @Login WHERE ID = @ID";
                    command.Parameters.Add(new SQLiteParameter("@FullName", user.FullName));
                    command.Parameters.Add(new SQLiteParameter("@Login", user.Login));
                    command.Parameters.Add(new SQLiteParameter("@ID", user.Id));

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private bool IsUserExist(User user)
        {
            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"SELECT COUNT(*) FROM Users WHERE 
                                                FullName = @FullName AND Login = @Login";
                    command.Parameters.Add(new SQLiteParameter("@FullName", user.FullName));
                    command.Parameters.Add(new SQLiteParameter("@Login", user.Login));

                    connection.Open();
                    if (Convert.ToInt32(command.ExecuteScalar()) >= 1) return true;
                    else return false;
                }
            }
        }

        private bool IsUserExist(int id)
        {
            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"SELECT COUNT(*) FROM Users WHERE ID = @ID";
                    command.Parameters.Add(new SQLiteParameter("@ID", id));

                    connection.Open();
                    if (Convert.ToInt32(command.ExecuteScalar()) >= 1) return true;
                    else return false;
                }
            }
        }
    }
}
