using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLSample
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con;
            SqlDataReader reader;

            Person person = new Person();
            List<Person> people = person.GetPeople();
            person.CreateXMLDoc(people);

            //foreach(var p in people)
            //{
            //    Console.WriteLine(p.Classes);
            //}

            Console.WriteLine("People.xml created.");
            string answer = "y";

            while (answer == "y")
            {


                Console.WriteLine("Do you want to continue? (y/n)");
                answer = Console.ReadLine();
                if (answer == "y")
                {
                    try
                    {
                        int id;
                        con = new SqlConnection(Properties.Settings.Default.connectionString);
                        con.Open();
                        Console.WriteLine("Enter Person Id");
                        id = int.Parse(Console.ReadLine());
                        reader = new SqlCommand("select distinct ID, Name, Age from Person where ID=" + id, con).ExecuteReader();



                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("ID | Name | Age \n {0}  |   {1}  |   {2}", reader.GetInt32(0),
                                reader.GetString(1), reader.GetInt32(2));
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                            Console.WriteLine(person.SavePeople(people));
                            Console.ReadLine();
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }


        }
    }
}
