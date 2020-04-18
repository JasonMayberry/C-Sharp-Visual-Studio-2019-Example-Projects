using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace SQLiteDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Database databaseObject = new Database();

            //// INSERT INTO DATABASE
            //string query = "INSERT INTO albums (`title`, `artist`) VALUES (@title, @artist)";
            //SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            //databaseObject.OpenConnection();
            //myCommand.Parameters.AddWithValue("@title", "Trapsoul");
            //myCommand.Parameters.AddWithValue("@artist", "Bryson Tiller");
            //var result = myCommand.ExecuteNonQuery();
            //databaseObject.CloseConnection();

            //Console.WriteLine("Rows Added : {0}", result);

            //// SELECT FROM DATABASE
            string query = "SELECT * FROM albums";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Console.WriteLine("Album: {0} - Artist: {1}", result["title"], result["artist"]);
                }
            }
            databaseObject.CloseConnection();

            Console.ReadKey();
        }
    }
}
