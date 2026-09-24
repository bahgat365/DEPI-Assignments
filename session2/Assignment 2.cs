using System;
using System.Security.Cryptography.X509Certificates;
namespace First_Project {
    internal class Program
    {
        static void Main(String[] args)
        {
            // Write a program that allows the user to enter a number then print it.
            /*
            Console.Write("enter you number: ");
             int.TryParse(Console.ReadLine(), out int number);
            Console .WriteLine("Your number is "+number);
            */

            //Write C# program that Convert a string to an integer, but the string contains non-numeric characters. And mention what will happen 

            /* string Word = "Mohammed";
            int num = int.Parse(Word);
            Console.WriteLine(Word);//it made an unanable exception thats why we use Try parse

            */

            //Write C# program that Perform a simple arithmetic operation with floating-point numbers And mention what will happen

            /* float M1 = 12.3f;
             float M2 = 11.2f;
             Console .WriteLine(M1+M2);//the output will be also float 
            */


            //Write C# program that Extract a substring from a given string.
            string phrase = "Bahgat Is The C# King";
            string Name = phrase.Substring(0, 6);
            Console .WriteLine(Name);


            //Write C# program that Assigning one value type variable to another and modifying the value of one variable and mention what will happen
            int X = 10;
            int Z = X;
             Z = 10;
            Console.WriteLine(X);//Becous it one of values type so X Wont change 


            //Write C# program that Assigning one reference type variable to another and modifying the object through one variable and mention what will happen
            int[] array1 = { 1, 2, 3, 4, 5 };
            int[] array2 = array1;
            array2[0] = 10;
            Console.WriteLine("array 1[1]= " + array1[1]);//it will change in the both of them becouse they are heep type so they point for the same refrance of heep



            //Write C# program that take two string variables and print them as one variable 
            string St1 = "I love ";
            string St2 = "Bahgat ";
            string Prase1=St1 + St2;//way one to to concatenation 
            string Prase2=$"{St1}{St2}";//way two 

           // Which of the following statements is correct about the C#.NET code snippet given below?

            int d;
            d = Convert.ToInt32(!(30 < 20));
            //A value 1 will be assigned to d.


            //Which of the following is the correct output for the C# code given below?

            Console.WriteLine(13 / 2 + " " + 13 % 2);
            //6 1

           // 10 - What will be the output of the C# code given below?


           int num = 1, z = 5;

            if (!(num <= 0))
                Console.WriteLine(++num + z++ + " " + ++z);
            else
                Console.WriteLine(--num + z-- + " " + --z);
            //7 7

        }

    }
}