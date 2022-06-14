using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AlfaBank.DataAccess
{
    public class Database
    {
        public string DatabaseFile { get; private set; }
        public string DatabaseSource { get; private set; }

        public Database()
        {
            /*string filename = "databaseFile.db";
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            DatabaseFile = Path.Combine(directory, filename);*/
            DatabaseFile = "databaseFile.db";
            DatabaseSource = "data source=" + DatabaseFile;

            DbInitialize.InitializeIfDbNotExist(DatabaseFile, DatabaseSource);
        }
    }
}
