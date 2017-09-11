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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace StayHydrated
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        public Settings()
        {
            InitializeComponent();

            tbDuration.Text = "testing";
            tbFrequency.Text = "testing2";

            checkForSavedSettings();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //clean up notifyicon (would otherwise stay open until application finishes)
            //CustomCommandNotifyIcon.Dispose();
            //RoutedCommandNotifyIcon.Dispose();

            base.OnClosing(e);
        }

        static void Deserialize()
        {
            // Declare the hashtable reference.
            Hashtable settings = null;

            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream("Settings.dat", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                settings = (Hashtable)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            // To prove that the table deserialized correctly, 
            // display the key/value pairs.
            foreach (DictionaryEntry de in settings)
            {
                Console.WriteLine("{0} lives at {1}.", de.Key, de.Value);
            }
        }

        private void checkForSavedSettings()
        {
            if (File.Exists("Settings.dat")){
                applySavedSettings();
            } else
            {
                createSavedSettings();
            }
        }

        private void createSavedSettings()
        {
            Hashtable settings = new Hashtable();
            settings.Add("Duration", 5);
            settings.Add("Frequency", 15);
            settings.Add("Display", true);

            FileStream fs = new FileStream("Settings.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, settings);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        private void applySavedSettings()
        {
            System.Console.WriteLine("Apply settings");
        }

        private void updateSavedSettings()
        {

        }
    }
}
