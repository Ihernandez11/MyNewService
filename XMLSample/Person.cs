using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            
        public string SavePeople(List<Person> people)
        {
             
            SqlConnection con;
            //SqlDataReader reader;

            con = new SqlConnection(Properties.Settings.Default.connectionString);
            con.Open();

            try
            {
                foreach (var p in people)
                {
                    foreach (var c in p.Classes)
                    {
                        SqlCommand cmd = new SqlCommand("Insert into Person (ID, Name, Age, Job, Class_ID) Values (@ID, @Name, @Age, @Job, @Class_ID)", con);
                        cmd.Parameters.AddWithValue("@ID", p.ID);
                        cmd.Parameters.AddWithValue("@Name", p.Name);
                        cmd.Parameters.AddWithValue("@Age", p.Age);
                        cmd.Parameters.AddWithValue("@Job", p.Job);
                        cmd.Parameters.AddWithValue("@Class_ID", c.ID);

                        cmd.ExecuteNonQuery();

                        SqlCommand cmd2 = new SqlCommand("Insert into Classroom (ID, Name) Values (@ID, @Name)", con);
                        cmd2.Parameters.AddWithValue("@ID", c.ID);
                        cmd2.Parameters.AddWithValue("@Name", c.Name);
                    }
                }

               
                

                con.Close();
                return "Successfully added People and classrooms";
            }

            catch(Exception ex)
            {
                return ex.ToString();
            }



            
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


        public void LoadXMLDoc()
        {
            var xmlFile = XDocument.Load(@"C: \Users\Ihernandez\Documents\MWS\MyNewService\People.xml");
        }


    }
}
