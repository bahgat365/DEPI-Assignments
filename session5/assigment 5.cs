using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace assigment4
{
    internal class Assigment4
    {
        enum Days
        {
            Saturday,
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday
        }
        struct Persone
        {
            public string name;
            public int age;
        }
        enum seasons{
        Spring,
            Summer,
            Autumn,
            Winter
        }
        [Flags]
        enum Permissions
        {
           none=0,
           read=1,
            write=2,
            delete=4,
            execute=16
        }
        enum Pcolors
        {
            Red=0, Green=1, Blue=2
        }
        struct Point
        {
            public int x;
            public int y;
        }
        static void Main()
        {

            // Explain the difference between passing(Value type parameters) by value and by reference then write a suitable c# example.
            /*the difrent is when we pass by value we are not change in the basic var or we cant we just use it by by refrence we can modify or update the basic value 
             becouse we already point of its ref so when we edit we edit in tha main var*/

            /*
            void edite_by_val(int num)
            {
                num = 200;
            }
            int num = 20;
            edite_by_val(num);
            Console.WriteLine("Value Refrances : "+num);
            //===================================================
            void edite_by_ref(ref int num_ref)
            {
                num_ref = 200;
            }
            int num_ref = 20;
            edite_by_ref(ref num_ref);
            Console.WriteLine("By Refrances Function : " + num_ref);
            //===================================================
            */

            /*Write a c# Function that accept 4 parameters from user and return result of summation and subtracting of two numbers*/
            /*
             int Sum(int x, int y, int z, int p)
             {    
                 return  x + y + z + p;
             }

             Console.WriteLine("Sum : " + Sum(1, 2, 5, 7));

             int sub(int x, int y, int z, int p)
             {
                 return x - y - z - p;
             }

             Console.WriteLine("sub : " + sub(1, 2, 5, 7));
             */

            /*Write a program in C# Sharp to create a function to calculate the sum of the individual digits of a given number.
        Output should be like 
        Enter a number: 25                                                                                            
        The sum of the digits of the number 25 is: 7
                 */
            /*
            Console.WriteLine("Enter A Number: ");
            int.TryParse(Console.ReadLine(), out int num);
            int Sum_Entered_Num(int num)
            {
                int resutl = 0;
                while (num > 0) { 
                int  digit  = num % 10;
                    resutl += digit;
                    num = num / 10;
                }
                return resutl; 

            }
            Console.WriteLine(Sum_Entered_Num(num));
            */

            //Create a function named "IsPrime", which receives an integer number and retuns true if it is prime, or false if it is not:
            /*
            bool isPrime(int num)
            {
                if (num > 0)
                {
                    for (int i = 2; i < num; i++)
                    {
                        if (num % i == 0)
                            return false;
                    }
                   
                }
               return true;

            }
            isPrime (1);
            */

            /*
             Create a function named MinMaxArray, to return the minimum and maximum values stored in an array, using reference parameters
             */
            /*static void MinMaxArray(int[] arr, ref int min, ref int max)
{
    min = arr[0];
    max = arr[0];

    for (int i = 1; i < arr.Length; i++)
    {
        if (arr[i] < min)
            min = arr[i];

        if (arr[i] > max)
            max = arr[i];
    }
}

static void Main()
{
    int[] arr = { 10, 5, 20, 3, 15 };

    int min = 0;
    int max = 0;

    MinMaxArray(arr, ref min, ref max);

    Console.WriteLine("Minimum = " + min);
    Console.WriteLine("Maximum = " + max);
}*/

            /*Create an iterative (non-recursive) function to calculate the factorial of the number specified as parameter*/

            /*
            Console.WriteLine("Enter num : ");
            int factorial(int num) {
                int result = 1;
            for (int i = 1; i <=num; i++)
                {
                    result *= i;
                }
            return result;
            }

            int.TryParse(Console.ReadLine(), out int num);
            Console.WriteLine("the factioral for your num: "+factorial(num));
            */

            //reate a function named "ChangeChar" to modify a letter in a certain position(0 based) of a string, replacing it with a different letter
            /*
            string ChangeChar (string Word,char ch,int position)
            {
                char[] charactars = Word.ToCharArray();
                charactars[position] = ch;
                return new string(charactars);

            }
            ;
            Console.WriteLine(ChangeChar("possa", 'k',0));
            */

            // Create an enum called "WeekDays" with the days of the week(Monday to Sunday) as its members.Then, write a C# program that prints out all the days of the week using this enum.


            /*

                         foreach (Days day in Enum.GetValues(typeof(Days)))
                    {
                        Console.WriteLine(day);
                    }
            */

            //  Define a struct "Person" with properties "Name" and "Age". Create an array of three "Person" objects and populate it with data.Then, write a C# program to display the details of all the persons in the array.

            /*
                Console.Write("Enter The Num Of Persones:");
            int.TryParse(Console.ReadLine(), out int num_p);
            Persone[] P = new Persone[num_p];
            for (int i = 0; i < P.Length; i++) { 
                 Console.Write($"Enter {i+1} name : ");
                P[i].name=Console.ReadLine();

                 Console.Write($"Enter {i} Age : ");
                P[i].age=int.Parse(Console.ReadLine());
            }
            //show
            Console.WriteLine("=================================");

            for (int i = 0; i < P.Length; i++) {
                Console.WriteLine($"name {i + 1}= {P[i].name}");
                Console.WriteLine($"age {i + 1}= {P[i].age}");
            }

            */
            //Create an enum called "Season" with the four seasons (Spring, Summer, Autumn, Winter) as its members. Write a C# program that takes a season name as input from the user and displays the corresponding month range for that season. Note range for seasons ( spring march to may , summer june to august , autumn September to November , winter December to February)
            /*
             Console.WriteLine("Enter Season Name : ");
             string seasonsName = Console.ReadLine();
             seasons season = Enum.Parse<seasons>(seasonsName, true);

             string result = season switch
             {
                 seasons.Spring => "March to May",
                 seasons.Summer => "June to August",
                 seasons.Autumn => "September to November",
                 seasons.Winter => "December to February",
                 _ => "Invalid Season"
             };

             Console.WriteLine(result);*/

            // Assign the following Permissions (Read, write, Delete, Execute) in a form of Enum.
            /*
             Permissions p = Permissions.delete | Permissions.write;
             if ((p & Permissions.read) == Permissions.read)
             {
                 Console.WriteLine( "READ EXIEST");
             }
             else
                 Console.WriteLine("READ IS NOT EXIST");
            */
            //Create an enum called "Colors" with the basic colors (Red, Green, Blue) as its members. Write a C# program that takes a color name as input from the user and displays a message indicating whether the input color is a primary color or not.
            /*
             Console.WriteLine("Enter color name: ");
             string Color_name = Console.ReadLine();

             if (Enum.TryParse<Pcolors>(Color_name, true, out Pcolors color))
             {
                 Console.WriteLine("Exist");
             }
             else
             {
                 Console.WriteLine("Doesn't Exist");
             }
             */
            //Create a struct called "Point" to represent a 2D point with properties "X" and "Y". Write a C# program that takes two points as input from the user and calculates the distance between them.
            /*
             Point p1=new Point();
             Console.Write("Enter x1 : ");
             int.TryParse(Console.ReadLine(),out  p1.x);

             Console.Write("Enter y1 : ");
             int.TryParse(Console.ReadLine(), out p1.y);

             Point p2=new Point();
             Console.Write("Enter x2 : ");
             int.TryParse(Console.ReadLine(), out p2.x);

             Console.Write("Enter y1 : ");
             int.TryParse(Console.ReadLine(), out p2.y);

             double d = Math.Sqrt(Math.Pow(p2.x-p1.x,2)+Math.Pow(p2.y-p1.y,2));
             */
            //. Create a struct called "Person" with properties "Name" and "Age". Write a C# program that takes details of 3 persons as input from the user and displays the name and age of the oldest person.
            Console.WriteLine("ENTER THREE PERSONS DETAILS : ");
            Persone[] p = new Persone[3];
            for (int i = 0; i < p.Length; i++)
            {
                Console.WriteLine($"Enter Person{i} name : ");
                p[i].name = Console.ReadLine();
                Console.WriteLine($"Enter Person{i} age : ");
               int.TryParse(Console.ReadLine(), out p[i].age);
            }
            Console.WriteLine("===================================================");
            //get the oldest
            Persone oldest= new Persone();
            oldest = p[0];
            for (int i = 1; i < p.Length; i++)
            {
                if (p[i].age > oldest.age)
                {
                    oldest = p[i];
                }
            }
            Console.WriteLine("the oldest name : " + oldest.name);
            Console.WriteLine("the oldest age : " + oldest.age);




        }
    }
}