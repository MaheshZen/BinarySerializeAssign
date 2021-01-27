using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BinarySerializeAssign
{
    [Serializable]
    class Age : IDeserializationCallback
    {
        int dob;
        static string dt =  DateTime.Now.ToString("yyyy");
        int cDate = Convert.ToInt32(dt);

        [NonSerialized] public int age;
        
        public Age(int db)
        {
            dob = db;
        }

        public void OnDeserialization(object sender)
        {
            age = cDate - dob;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Year of Birth:");
            int age = Convert.ToInt32(Console.ReadLine());

            Age a = new Age(age);
            FileStream fs = new FileStream(@"Non Serial.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs,a);

            fs.Seek(0, SeekOrigin.Begin);
            Age res = (Age)bf.Deserialize(fs);
            Console.WriteLine(res.age);


        }
    }
}
