using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XMLSample
{
    class Person
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Job { get; set; }
        public List<Classroom> Classes { get; set; }


        public List<Person> GetPeople()
        {
            

            List<Person> people = new List<Person> {
                        new Person
            {
                ID = 1,
                Name = "Joe",
                Age = 35,
                Job = "Manager",
                Classes = new List<Classroom>() {
                                new Classroom{ID = 1, Name = "Math"},
                                new Classroom{ ID = 2, Name = "English" }
                }
            },
                new Person
                {
                    ID = 2,
                    Name = "Jason",
                    Age = 18,
                    Job = "Software Engineer",
                    Classes = new List<Classroom>() {
                                new Classroom{ID = 3, Name = "History"},
                                new Classroom{ ID = 4, Name = "Science" }
                    }
                },
                new Person
                {
                    ID = 3,
                    Name = "Lisa",
                    Age = 53,
                    Job = "Bakery Owner",
                    Classes = new List<Classroom>() {
                                new Classroom{ID = 6, Name = "Reading"},
                                new Classroom{ ID = 7, Name = "Calculus" }
                    }
                },
                new Person
                {
                    ID = 4,
                    Name = "Mary",
                    Age = 90,
                    Job = "Nurse",
                    Classes = new List<Classroom>() {
                                new Classroom{ID = 8, Name = "Government"},
                                new Classroom{ ID = 9, Name = "Geography" }
                    }
                }
            };

            return people;
        }

            

        

        public void CreateXMLDoc(List<Person> people)
        {
            
            XDocument document = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Izzys xml"),
                new XElement("People",

                from p in people
                select new XElement("Person", new XAttribute("ID", p.ID),
                            new XElement("Name", p.Name),
                            new XElement("Age", p.Age),
                            new XElement("Job", p.Job),
                            new XElement("Classes",
                               from c in p.Classes
                               select new XElement("Class", c.Name)
                            )
                                    //Person Element
                                    )
                //People Element
                            )
                //XDocument
                );

            document.Save(@"C:\Users\Ihernandez\Documents\MWS\MyNewService\People.xml");
        }


    }
}
