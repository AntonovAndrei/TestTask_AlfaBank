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
                    string sqlCommand = "INSERT INTO Users (FullName, Login, RegistrationDate) VALUES " +
                        "(@FullName, @Login, @RegistrationDate)";

                    command.CommandText = sqlCommand;
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
                    var sqlCommand = "INSERT INTO Users (FullName, Login, RegistrationDate) VALUES " +
                        "(@FullName, @Login, @RegistrationDate)";

                    command.CommandText = sqlCommand;
                    command.Parameters.Add(new SQLiteParameter("@FullName", user.FullName));
                    command.Parameters.Add(new SQLiteParameter("@Login", user.Login));
                    command.Parameters.Add(new SQLiteParameter("@RegistrationDate", user.RegistrationDate));

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public void AddRange(IEnumerable<User> users)
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

                    var user = new User();
                    while (dataReader.Read())
                    {
                        try
                        {
                            user.Id = dataReader.GetInt32(0);
                            user.FullName = dataReader.GetString(1);
                            user.Login = dataReader.GetString(2);
                            user.RegistrationDate = dataReader.GetDateTime(3);
                            user.IsDeleted = dataReader.GetInt32(4) == 0 ? false : true;

                            users.Add(user);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                }
            }

            return users;                
        }
    }
}
