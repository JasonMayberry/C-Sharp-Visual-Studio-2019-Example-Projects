using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Test_Wpf_App_with_SQLite
{
    class Database
    {
        private static SQLiteConnection myConnection;
        public static SQLiteConnection MyConnection()
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appDataPath = Path.Combine(directory, "Estimatrix", "Estimatrix.sqlite3");
            if (!File.Exists(appDataPath))
            {
                // SQLiteConnection.CreateFile("database.sqlite3");
                // Console.WriteLine("Database file created");
                // var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                // using (FileStream fs = File.Create(Path.Combine(directory, "myAppDirectory", "myFile.txt"))) { }
                Directory.CreateDirectory(Path.Combine(directory, "Estimatrix"));
                SQLiteConnection.CreateFile(Path.Combine(directory, "Estimatrix", "Estimatrix.sqlite3"));
            }
            myConnection = new SQLiteConnection("Data Source="+appDataPath);
            return myConnection;
        }
        public static void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        public static void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }
    }
}

