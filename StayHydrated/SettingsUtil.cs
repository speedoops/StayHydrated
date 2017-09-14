using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace StayHydrated
{
    class SettingsUtil
    {
        private Hashtable settings;
        private int defaultDuration = 5;
        private int defaultFrequency = 15;
        private Boolean defaultDisplay = true;

        public SettingsUtil(){}

        public Boolean checkForSavedSettings()
        {
            if (File.Exists("Settings.dat"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void deserializeSavedSettings()
        {
            // Declare the hashtable reference.
            settings = null;

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

        public int getDuration()
        {
            if (checkForSavedSettings())
            {
                return (int)settings["Duration"];
            } else
            {
                return defaultDuration;
            }
        }

        public int getFrequency()
        {
            if (checkForSavedSettings())
            {
                return (int)settings["Frequency"];
            } else
            {
                return defaultFrequency;
            }
        }

        public Boolean getDisplay()
        {
            if (checkForSavedSettings())
            {
                return (Boolean)settings["Display"];
            } else
            {
                return defaultDisplay;
            }
        }

        private void createSavedSettings()
        {
            settings = new Hashtable();
            settings.Add("Duration", defaultDuration);
            settings.Add("Frequency", defaultFrequency);
            settings.Add("Display", defaultDisplay);

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

        private void updateSavedSettings()
        {

        }
    }
}
