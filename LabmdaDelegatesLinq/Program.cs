using System;
using LabmdaDelegatesLinq.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace LabmdaDelegatesLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();
            Console.Write("Enter salary: ");
            double s = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            List<Employee> employees = new List<Employee>();
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while(!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        string name = line[0];
                        string email = line[1];
                        double salary = double.Parse(line[2], CultureInfo.InvariantCulture);
                        employees.Add(new Employee(name, email, salary));
                    }

                    //var bigSalary = employees.Where(e => e.Salary > 2000.0).OrderBy(e => e.Email).Select(e => e.Email);
                    var bigSalary =
                        from emp in employees
                        where emp.Salary > 2000.0
                        orderby emp.Email
                        select emp.Email;
                    //var sumSalary = employees.Where(e => e.Name[0].Equals('M')).Sum(e => e.Salary);
                    var sumSalary =
                        (from emp in employees
                         where emp.Name[0].Equals('M')
                         select emp).Sum(e => e.Salary);

                    Console.WriteLine($"Email of people whose salary is more than {s.ToString("F2", CultureInfo.InvariantCulture)}:");
                    foreach(string email in bigSalary)
                    {
                        Console.WriteLine(email);
                    }

                    Console.WriteLine($"\nSum of salary of people whose name starts with 'M': {sumSalary.ToString("F2", CultureInfo.InvariantCulture)}");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error ocurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}
