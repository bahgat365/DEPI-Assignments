using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Practise
{
    internal class assigment5
    {
        class Hiring_Date
        {
            public int day;
            public int month ;
            public int year;

        }
        //enums 
        enum security_level
        {
            guest, Developer, secretary, DBA
        }
        class employees
        {
            int id;
            string name;
            security_level emp_sec;
            double salary;

            char gender;
            public Hiring_Date hire_date;

            //constractor
            public employees(int Id, string Name, security_level Security_level, double Salary,Hiring_Date Hire_date)
            {
                id=Id;
                name=Name;
                emp_sec = Security_level;
                salary= Salary;    
                hire_date = Hire_date;
            }

            //prop
            public char Gender
            {
                get
                {
                    return gender;
                }
                set
                {
                    string x=value.ToString().ToLower();
                    if (x == "m" || x== "f")
                    {
                        gender = value;
                    }
                    else
                        gender = 'M';
                }
            }
            public bool FullPermissions
            {
                get
                {
                    return emp_sec == security_level.DBA ||
                           emp_sec == security_level.guest;
                }

            }
            //represent emp data
            public override string ToString()
            {
                return string.Format("Name: {0} , Salary {1:C} : ",
                    name,salary);
            }
            
        }

        static void Main(string[] args)
        {
            // Design and implement a Class for the employees in a company:
            //Employee is identified by an ID, Name, security level, salary, hire date and Gender.
            employees[] EmpArr = new employees[3];



        }

    }
}
