using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Wpf_Read_Write_JSON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string homeDir;

            homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // Write a class object to file in formated JSON
            Est_data est_data = new Est_data();
            string est_data2json = JsonConvert.SerializeObject(est_data, Formatting.Indented);

            // Combine path to write file in
            string est_data_DocPath = System.IO.Path.Combine(homeDir, "Desktop", "Est1.json");
            File.WriteAllText(est_data_DocPath, est_data2json);

            //serialize JSON to a string and then write string to a file
            File.WriteAllText(est_data_DocPath, JsonConvert.SerializeObject(est_data, Formatting.Indented));

            // ****** serialize JSON directly to a file ******
            using (StreamWriter file = File.CreateText(est_data_DocPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, est_data);
            }



            // read file into a string and deserialize JSON to a type
            Est_data read_in_est_data = JsonConvert.DeserializeObject<Est_data>(File.ReadAllText(est_data_DocPath));
            box1.Text = "A " + read_in_est_data.color + ", " + read_in_est_data.door + " Door, " + read_in_est_data.price + " Dollor, " + read_in_est_data.type + ", " + read_in_est_data.style + ".";


            // ****** deserialize JSON directly from a file ******
            using (StreamReader file = File.OpenText(est_data_DocPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                Est_data read_in_est_data2 = (Est_data)serializer.Deserialize(file, typeof(Est_data));
                box1.Text += "\n\n";
                box1.Text += "A " + read_in_est_data2.color + ", " + read_in_est_data2.door + " Door, " + read_in_est_data2.price + " Dollor, " + read_in_est_data2.type + ", " + read_in_est_data2.style + ".";
            }

        }

    }
}
