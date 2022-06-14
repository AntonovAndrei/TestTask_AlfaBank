using System;
using System.Collections.Generic;
using System.Linq;
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
            DatabaseFile = "databaseFile.db";
            DatabaseSource = "data source=" + DatabaseFile;
            DbInitialize.InitializeIfDbNotExist(DatabaseFile, DatabaseSource);
        }
    }
}
