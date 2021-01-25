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
                             [Estimate_Data] (
                             [est_id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                             [Inspection_Date] TEXT,
                             [Inspection_Type] TEXT)";
            using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.MyConnection()))
            {
                Database.OpenConnection();
                myCommand.CommandText = query;
                myCommand.ExecuteNonQuery();
                Database.CloseConnection();
            }

            // POPULATE FORM FIELDS FROM DATABASE
            string estID = "2";
            string allEstimateDataQuery = "SELECT Inspection_Date, Inspection_Type FROM Estimate_Data Where est_id="+estID;
            using (SQLiteCommand myCommand = new SQLiteCommand(allEstimateDataQuery, Database.MyConnection()))
            {
                Database.OpenConnection();

                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        if (Inspection_Date.Text == "") { Inspection_Date.Text = (string)result["Inspection_Date"]; }
                        if (Inspection_Type.Text == "") { Inspection_Type.Text = (string)result["Inspection_Type"]; }
                    }
                    
                }
                Database.CloseConnection();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appDataPath = System.IO.Path.Combine(directory, "Estimatrix", "Estimatrix.sqlite3");
            if (!File.Exists(appDataPath))
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
            string query = "INSERT INTO Estimate_Data (`Inspection_Date`, `Inspection_Type`) VALUES (@Inspection_Date, @Inspection_Type)";
            using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.MyConnection()))
            {
                Database.OpenConnection();
                myCommand.Parameters.AddWithValue("@Inspection_Date", Inspection_Date.Text);
                myCommand.Parameters.AddWithValue("@Inspection_Type", Inspection_Type.Text);
                var returnValue = myCommand.ExecuteNonQuery();
                Database.CloseConnection();
                outBox.Text = "Rows Added : " + returnValue;
            }
            //Inspection_Date.Text = "";
            //Inspection_Type.Text = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // SELECT FROM DATABASE
            string query = "SELECT * FROM Estimate_Data";
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
                        outBox.Text += lNum + ".  [Estimate ID:" + result["est_id"] + "] - Inspection Date: " + result["Inspection_Date"] + " - Inspection Type: " + result["Inspection_Type"] + "\n";
                    }
                }
                Database.CloseConnection();
            }
        }

        private void deleteById_Click(object sender, RoutedEventArgs e)
        {
            // DELETE TABLE ROW
            string query = "DELETE FROM Estimate_Data WHERE est_id = " + Textbox.Text;
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
