using System.Data.SQLite;

namespace AlfaBank.DataAccess
{
    public static class DbInitialize
    {
        private const string CreateTableQuery = @"CREATE TABLE IF NOT EXISTS [Users] (
                                               [ID] INTEGER NOTNULL PRIMARY KEY AUTOINCREMENT,
                                               [FullName] TEXT NOT NULL,
                                               [Login] TEXT NOT NULL,
                                               [RegistrationDate] TEXT  NOT NULL,
                                               [IsDeleted] NUMERIC DEFAULT 0
                                               )";
        

        public static void InitializeIfDbNotExist(string DatabaseFile, string DatabaseSource)
        {
            // Create the file which will be hosting our database
            if (!File.Exists(DatabaseFile))
            {
                SQLiteConnection.CreateFile(DatabaseFile);
            }

            // Connect to the database 
            using (var connection = new SQLiteConnection(DatabaseSource))
            {
                // Create a database command
                using (var command = new SQLiteCommand(connection))
                {
                    connection.Open();

                    // Create the table
                    command.CommandText = CreateTableQuery;
                    command.ExecuteNonQuery();

                    // Checking whether the database contains records
                    command.CommandText = "Select COUNT(*) FROM Users";
                    bool isDbHaveData = false;
                    if(Convert.ToInt32(command.ExecuteScalar()) == 0) isDbHaveData = true;

                    if (!isDbHaveData)
                    {
                        // Insert entries in database table
                        command.CommandText = "INSERT INTO Users (FullName, Login, RegistrationDate, IsDeleted) VALUES " +
                            "('Petrov Petr Petrovich', 'petr@google.com', date('now'), 0)," +
                            "('Vasiliev Vasiliy Vasilievich', 'vasya@google.com', '2021-11-29', 0)," +
                            "('Antonov Anton Antonovich', 'anton@google.com', date('now'), 0)," +
                            "('Ivanov Ivan Ivanovich', 'ivan@google.com', '2021-11-29', 0)," +
                            "('Sidovrov Sidr Sidorovich', 'sidr@google.com', date('now'), 0)," +
                            "('Dmitriy Agafonov Vasilievich', 'dmitriy@google.com', '2021-10-15', 0)," +
                            "('Volkiv Volk Volkovich', 'auf@google.com', '2021-01-21', 0)," +
                            "('Trofimov Trofim Trofimovich', 'trofim@google.com', date('now'), 0),";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
