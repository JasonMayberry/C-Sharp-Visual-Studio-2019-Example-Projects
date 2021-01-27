using System;
using System.Configuration;
using System.Collections.Specialized;
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
using System.Xml;

namespace Wpf_app_config_XML_estimates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // HOW TO USE THE CONFIG FILE
            string sAttr;
            // Read a particular key from the config file 
            sAttr = ConfigurationManager.AppSettings.Get("Key0");
            outBox.Text += "\n\n The value of Key0: " + sAttr + "\n\n";
            // Read all the keys from the config file
            NameValueCollection sAll;
            sAll = ConfigurationManager.AppSettings;
            foreach (string s in sAll.AllKeys)
                outBox.Text += "Key: " + s + " Value: " + sAll.Get(s) + "\n";



            // NEW XMLDOCUMENT()
            string xmlDocPath;
            string xmlDocPath2;
            XmlDocument Xdoc = new XmlDocument();
            var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            xmlDocPath = System.IO.Path.Combine(homeDir, "Desktop", "Est1.xml");
            xmlDocPath2 = System.IO.Path.Combine(homeDir, "Desktop", "Est2.xml");
            Xdoc.Load(xmlDocPath);
            Xdoc.Save(xmlDocPath2);
            foreach(XmlNode node in Xdoc.DocumentElement)
            {
                string name = node.Attributes[0].Value; // The XML file had to have a <root> element to use this.
                outBox.Text += "\n";
                outBox.Text += "**** Attribute Name = " + name + "\n\n";
            }


            // NEW XMLTEXTREADER()
            XmlTextReader xtr = new XmlTextReader(xmlDocPath);
            while (xtr.Read())
            {
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "name")
                {
                    string s1 = xtr.ReadElementString();
                    outBox.Text += "Name = " + s1 + "\n";
                }
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "class")
                {
                    string s2 = xtr.ReadElementString();
                    outBox.Text += "Class = " + s2 + "\n";
                }
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "result")
                {
                    string s3 = xtr.ReadElementString();
                    outBox.Text += "Result = " + s3 + "\n";
                }
            }

            // HOW TO WRITE XML FILE USING C#
            string filename = System.IO.Path.Combine(homeDir, "Desktop", "Estimate.xml");
            XmlTextWriter xmlWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteComment("Creating an XML file using C#");
            xmlWriter.WriteStartElement("root");
            xmlWriter.WriteStartElement("Customer");
            xmlWriter.WriteElementString("name", "John");
            xmlWriter.WriteElementString("phone", "(417) 543-1824");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("vehicle");
            xmlWriter.WriteElementString("vin", "1FTSE34L17DB39279");
            xmlWriter.WriteElementString("color", "Gold");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("damages");
            xmlWriter.WriteElementString("Fender", "Dented");
            xmlWriter.WriteElementString("Hood", "Dented");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();


        }

    }
}
