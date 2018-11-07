using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            List<Person> people = person.GetPeople();
            person.CreateXMLDoc(people);

            //foreach(var p in people)
            //{
            //    Console.WriteLine(p.Classes);
            //}

            Console.WriteLine("People.xml created.");
            Console.ReadLine();



        }
    }
}
