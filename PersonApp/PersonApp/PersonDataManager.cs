//using PersonApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PersonApp.Data
{
    public static class PersonDataManager
    {
        private static readonly string filePath = "persons.xml";

        public static void Save(List<Person> persons)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, persons);
            }
        }

        public static List<Person> Load()
        {
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    return (List<Person>)serializer.Deserialize(fs);
                }
            }
            return new List<Person>();
        }
    }
}
