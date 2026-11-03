using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;

namespace FinalTerm
{
    public static class Database
    {
        private static readonly string dbPath =
           @"C:\FinalTerm\sampledb.accdb";

        private static readonly string connectionString =
            "Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source=" + dbPath + ";" +
            "Persist Security Info=False;";
        public static OleDbConnection GetConnection()
        {
            return new OleDbConnection(connectionString);
        }
    }
}
 