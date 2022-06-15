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
                    command.CommandText = "Select * FROM Users WHERE ID = @ID";
                    command.Parameters.Add(new SQLiteParameter("@ID", id));

                    connection.Open();
                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
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
            }

            return user;
        }

        // ne rabotaet
        public User Get(string fullName)
        {
            var user = new User();

            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "Select * FROM Users WHERE FullName = @FullName";
                    command.Parameters.Add(new SQLiteParameter("@FullName", AddQuotesToParameter(fullName)));

                    connection.Open();
                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
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
            }

            return user;
        }

        // ne rabotaet
        public void Delete(User user)
        {
            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                { 
                    command.CommandText = "UPDATE Users SET IsDeleted = 1 WHERE FullName = @FullName";
                    command.Parameters.Add(new SQLiteParameter("@FullName", AddQuotesToParameter(user.FullName)));

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

        //System.InvalidOperationException: "Operation is not valid due to the current state of the object."
        /*public void AddRange(IEnumerable<User> users)
        {
            using (var connection = new SQLiteConnection(_db.DatabaseSource))
            {
                using (var command = new SQLiteCommand(connection))
                {
                    string sqlCommand = "INSERT INTO Users (FullName, Login, RegistrationDate) VALUES " +
                        "(@FullName, @Login, @RegistrationDate)";

                    using (var transaction = connection.BeginTransaction())
                    {
                        connection.Open();
                        foreach (var user in users)
                        {
                            command.CommandText = sqlCommand;
                            command.Parameters.Add(new SQLiteParameter("@FullName", user.FullName));
                            command.Parameters.Add(new SQLiteParameter("@Login", user.Login));
                            command.Parameters.Add(new SQLiteParameter("@RegistrationDate", user.RegistrationDate));

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }
            }
        }*/

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

        private string AddQuotesToParameter(string parameter)
        {
            return "\"" + parameter + "\"";
        }
    }
}
