using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.IO;

namespace Test_Wpf_App_with_SQLite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // CREATE NEW TABLE
            string query = @"CREATE TABLE IF NOT EXISTS
                             [albums] (
                             [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                             [title] TEXT,
                             [artist] TEXT)";
            using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.MyConnection()))
            {
                Database.OpenConnection();
                myCommand.CommandText = query;
                myCommand.ExecuteNonQuery();
                Database.CloseConnection();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("./database.sqlite3"))
            {
                outBox.Text = "Database Does Not Exist";
            }
            else
            {
                outBox.Text = "Database Exists";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // INSERT INTO DATABASE
            string query = "INSERT INTO albums (`title`, `artist`) VALUES (@title, @artist)";
            using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.MyConnection()))
            {
                Database.OpenConnection();
                myCommand.Parameters.AddWithValue("@title", title.Text);
                myCommand.Parameters.AddWithValue("@artist", artist.Text);
                var returnValue = myCommand.ExecuteNonQuery();
                Database.CloseConnection();
                outBox.Text = "Rows Added : " + returnValue;
            }
            title.Text = "";
            artist.Text = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // SELECT FROM DATABASE
            string query = "SELECT * FROM albums";
            using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.MyConnection()))
            {
                Database.OpenConnection();

                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    int lNum = 0;
                    outBox.Text = "";
                    while (result.Read())
                    {
                        lNum++;
                        outBox.Text += lNum + ".  [ID:" + result["id"] + "] - Album: " + result["title"] + " - Artist: " + result["artist"] + "\n";
                    }
                }
                Database.CloseConnection();
            }
        }

        private void deleteById_Click(object sender, RoutedEventArgs e)
        {
            // DELETE TABLE ROW
            string query = "DELETE FROM albums WHERE id = " + Textbox.Text;
            int nu;
            bool boolResult = int.TryParse(Textbox.Text, out nu);
            if (boolResult)
            {
                // outBox.Text += query + "\n";
                using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.MyConnection()))
                {
                    Database.OpenConnection();
                    myCommand.CommandText = query;
                    var returnValue = myCommand.ExecuteNonQuery();
                    outBox.Text = "Rows Deleted: " + returnValue;
                    Database.CloseConnection();
                }
            }
        }
    }
}
