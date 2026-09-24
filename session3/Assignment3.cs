using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace First_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {



            // 1 - Write a program that takes a number from the user then print yes if
            //that number can be divided by 3 and 4 otherwise print no.
            /*
            Console.Write("Enter num: ");
            if ( int.TryParse ( Console.ReadLine(),out int num) && num > 0)
            {
                Console.WriteLine(num % 3 == 0 ? "Yes" : "No");
            }
            else
                Console.WriteLine("INVLID NUM");
            */
            //2- Write a program that allows the user to insert an integer then print
            //negative if it is negative number otherwise print positive.

            /*
            Console.Write("Enter num: ");
            if (int.TryParse(Console.ReadLine(), out int num1)  )
            {
                Console.WriteLine(num >= 0 ? "Positive" : "Negative");
            }
            else
                Console.WriteLine("INVLID NUM");

            */

            // 3 - Write a program that takes 3 integers from the user then prints the max
            //   element and the min element.

            /* Console.Write("Enter The Number Of Array elements : ");
             int.TryParse(Console.ReadLine(), out int Array_len);
             int[] array = new int[Array_len];
             for (int i = 0; i < Array_len; i++)
             {
                 Console.Write($"array [{i}] : ");
                 int.TryParse(Console.ReadLine(), out array[i]);
             }
             // To Show Them
             for (int i = 0; i < Array_len; i++)
             {
                 Console.WriteLine("=================================");
                 Console.WriteLine($"Your array Of [{i}] = {array[i]}");
             }
             Console.WriteLine("=================================");


             Console.WriteLine("===============Max==================");

             Console.WriteLine($"The Max Number Is  = {array.Max()}");


             Console.WriteLine("=================================");

             Console.WriteLine("===============Min==================");

             Console.WriteLine($"The Min Number Is  = {array.Min()}");
            */

            //Write a program that allows the user to insert an integer number then
            //check If a number is even or odd.
            /*
            Console.Write("Enter num: ");
            if (int.TryParse(Console.ReadLine(), out int num) && num > 0)
            {
                Console.WriteLine(num % 2 == 0 ? "EVEN" : "OOD");
            }
            else
                Console.WriteLine("INVLID NUM");
            */


            // 5 - Write a program that takes character from the user then if it is a
            //vowel chars(a, e, I, o, u) then print(vowel) otherwise print(consonant).
            /*
             Console.WriteLine("Enter Your Char : ");
             char.TryParse(Console.ReadLine(), out char Ch);
             Ch=char.ToLower(Ch);
             String Result = Ch switch
             {
                 'a' or 'e' or 'I' or 'o' or 'u' => "Vowel",
                 _ => "(consonant)"
             };
             Console.WriteLine(Result);
            */

            //Write a program that allows the user to insert an integer then print
            //      all numbers between 1 to that number.
            /*
            Console.WriteLine("Insert Intger : ");
            int.TryParse(Console.ReadLine(), out int num);
            Console.WriteLine("\n");
            for (int i = 1; i <= num; i++) {
                Console.WriteLine(i);}
            */

            //Write a program that allows the user to insert an integer then
            //print a multiplication table up to 12.

            /*
            Console.WriteLine("Enter our Start Point For Multiplication : ");
            int.TryParse(Console.ReadLine(), out int start_point);

            for (int i = 1; i <= 12; i++)
            {  
                Console.Write(i * start_point + "\t");          
             }
            */
            // Write a program that allows to user to insert number then print all
            // even numbers between 1 to this number
            /*
            Console.WriteLine("write The end num : ");
            int.TryParse(Console.ReadLine(), out int end_num);
            for (int i = 1; i <= end_num; i++) {
                if (i % 2 != 0)
                    Console.Write(i + "\t");
               }
            */
            // Write a program that takes two integers then prints the power.
            /*
            Console.WriteLine("write The bais num : ");
            int.TryParse(Console.ReadLine(), out int num1);
            Console.WriteLine();
            Console.WriteLine("write The end Power : ");
            int.TryParse(Console.ReadLine(), out int num2);

            int result = 1;
            for (int i = 0;i<num2;i++)
            {
                result *= num1 ;
            }
            Console.WriteLine("the pow result is= "+result);
            */
            //Write a program to enter marks of five subjects and calculate total,
            //average and percentage.

            /*
            Console.WriteLine("enter five numbers : ");
            int[] Array_nums = new int[5] ;
            int sum = 0;
            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine($"enter numbers {i} : ");
                int.TryParse(Console.ReadLine(), out Array_nums[i]);
            }
            for (int i = 0; i < 5; i++) { 
            sum+=Array_nums[i];
            }
            Console.WriteLine("Sum = " + sum);
            Console.WriteLine("Avg = " + (double)sum / Array_nums.Length);
            Console.WriteLine("Percentege = " + ((double)sum / 500) * 100);
            */
            //Write a program to input the month number and print the number of days
            // in that month.
            /*
            Console.WriteLine("Enter num The month : ");
            int.TryParse(Console.ReadLine(), out int Num_month);

            int result = Num_month switch
            {
                2 => 28,
                4 or 6 or 9 or 11 => 30,
                1 or 3 or 5 or 7 or 8 or 10 or 12 => 31,
                _ => 0
            };
            Console.WriteLine(result);
            */
            //12- Write a program to create a Simple Calculator.
            /*
            Console.WriteLine("enter number 1 : ");
            int.TryParse(Console.ReadLine(), out int num1);
            Console.WriteLine("===================================");
            Console.WriteLine("enter number 2 : ");
            int.TryParse(Console.ReadLine(), out int num2);
            Console.WriteLine("===================================");
            Console.WriteLine("Choose The number of operator :");
            Console.WriteLine("1: (+)");
            Console.WriteLine("2: (-)");
            Console.WriteLine("3: (*)");
            Console.WriteLine("4: (/)");
            Console.WriteLine("5: (%)");
            int.TryParse(Console.ReadLine(), out int op);

            decimal Result = op switch
            {
                1 => num1 + num2,
                2 => num1 - num2,
                3 => num1 * num2,
                4 => (decimal)num1 / num2,
                5 => num1 % num2
            };
            Console.WriteLine("The Result Of Your Op is = "+Result);
            */
            //Write a program to allow the user to enter a string and print the
            // REVERSE of it.
            /*
            Console.WriteLine("\t \tEnter String and I Will Reverse it to you \t");
            string Str=Console.ReadLine();
            string New_Str =string.Join( " ", Str.Split().Reverse()) ;
            Console.WriteLine(New_Str);
            */
            //14 - Write a program to allow the user to enter int and print the REVERSEt.
            /*
            Console.Write("Enter any num = ");
            int.TryParse(Console.ReadLine(), out int num);

            int reverse = 0;

            while (num != 0)
            {
                int digit = num % 10;
                reverse = reverse * 10 + digit;
                num /= 10;
            }

            Console.WriteLine(reverse);
            */
            //Write a program in C# Sharp to find prime numbers within a range of
            //numbers.
            /*
            Console.WriteLine("Enter The Start : ");
            int.TryParse(Console.ReadLine(),out int St_Num);
            Console.WriteLine("Enter The End : ");
            int.TryParse(Console.ReadLine(), out int En_Num);

            for (int i=St_Num;i<En_Num;i++) {
                bool is_prime = true;
                for (int J = 2; J < i; J++) {
                if (i>1 && i%J==0)
                {
                    is_prime=false;
                }
                 }
                if (is_prime)
                {
                    Console.Write("  " + i);
                }
            }
            */

            // Write a program in C# Sharp to convert a decimal number into binary
            //without using an array.
            /*
            Console.WriteLine("Enter a number to convert : ");
            int.TryParse(Console.ReadLine(), out int num);

            int remain=0;
            int Quo;
            int div=num;
            string binary = "";
            while (div > 0)
            {
              Quo = div / 2;
              remain = div % 2;
              div = Quo;
              binary += remain ;//that is concat not sum 

            }

            Console.WriteLine(binary);
            */

            //17 - Create a program that asks the user to input three points(x1, y1),
            // (x2, y2), and(x3, y3), and determines whether these points lie on a
            // single straight line.
            /*Console.WriteLine("Enter x1:");
            double x1 = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter y1:");
            double y1 = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter x2:");
            double x2 = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter y2:");
            double y2 = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter x3:");
            double x3 = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter y3:");
            double y3 = double.Parse(Console.ReadLine());

            double slope1 = (y2 - y1) / (x2 - x1);
            double slope2 = (y3 - y2) / (x3 - x2);

            if (slope1 == slope2)
            {
            Console.WriteLine("The three points lie on the same straight line.");
            }
            else
            {
            Console.WriteLine("The three points do NOT lie on the same straight line.");
            }

                        */
            /* Within a company, the efficiency of workers is evaluated based on the
 duration required to complete a specific task.A worker's efficiency level
 is determined as follows:
 -If the worker completes the job within 2 to 3 hours, they are considered
 highly efficient.
 - If the worker takes 3 to 4 hours, they are instructed to increase their
 speed.
 - If the worker takes 4 to 5 hours, they are provided with training to
 enhance their speed.
 - If the worker takes more than 5 hours, they are required to leave the
 company.
 To calculate the efficiency of a worker, the time taken for the task is
 obtained via user input from the keyboard.
            */
            /*
                        Console.WriteLine("enter your time taken for task:  ");
                        bool valid_info = true;

                        while (valid_info)
                        {
                           double.TryParse(Console.ReadLine(), out double Time_For_Task);

                            if (Time_For_Task >= 2 && Time_For_Task <= 3)
                            {
                                Console.WriteLine(" YOU ARE highly efficient.");
                                valid_info=false;
                            }
                            else if (Time_For_Task > 3 && Time_For_Task <= 4)
                            {
                                Console.WriteLine("You are instructed to increase your speed.");
                                valid_info = false;
                            }
                            else if (Time_For_Task >= 4 && Time_For_Task <= 5)
                            {
                                Console.WriteLine("training to enhance your speed.");
                                valid_info = false;
                            }
                            else if (Time_For_Task > 5)
                            {
                                Console.WriteLine("you are required to leave the company..");
                                valid_info = false;
                            }
                            else { Console.WriteLine("no valid data try agian"); }
                        }
            */
            /* Write a program that prints an identity matrix using for loop, in
                other words takes a value n from the user and shows the identity table of
             size n * n.*/
            /*
            Console.WriteLine("Insert the size of idintity matrix : ");
            int.TryParse (Console.ReadLine(), out int size_of_matrix);
            
            for (int i = 0; i <= size_of_matrix; i++)
            {
                for(int j = 0; j <= size_of_matrix; j++)
                {
                    if (j == i) Console.Write(1 + " ");
                    else Console.Write(0 + " ");  
                }
                Console.WriteLine();
            }
            */
            // 20 - Write a program in C# Sharp to find the sum of all elements of the
            //array.

            /*
            Console.Write("Enter The size of arary : ");
            int.TryParse(Console.ReadLine(), out int size_of_array);
            int[] arr= new int[size_of_array];
            //full the array with elements
            for (int i=0;i<size_of_array;i++)
            {
                Console.Write($"Enter array of {i} : ");

                int.TryParse(Console.ReadLine(), out arr[i]);
            }
            //sum
            int sum = 0;
            for (int i = 0; i < size_of_array; i++)
            {
                sum+= arr[i];
            }
            Console.WriteLine("===================");
            Console.WriteLine($"Sum Of Your Array = {sum}");
            */

            //Write a program in C# Sharp to find maximum and minimum element in an
            //array

            /*
            int[] arr = { 1, 2, 3, 4, 5, 6 };

            int maxvalue = arr[0];
            int minvalue = arr[0];

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > maxvalue)
                {
                    maxvalue = arr[i];
                }

                if (arr[i] < minvalue)
                {
                    minvalue = arr[i];
                }
            }

            Console.WriteLine("Maximum value = " + maxvalue);
            Console.WriteLine("Minimum value = " + minvalue);
            */

            // Write a program in C# Sharp to find the second largest element in an
            //array.
            /*
          int[] arr = { 10, 25, 7, 40, 30 };

            int largest = arr[0];
            int secondLargest = arr[0];

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > largest)
                {
                    secondLargest = largest;
                    largest = arr[i];
                }
                else if (arr[i] > secondLargest && arr[i] != largest)
                {
                    secondLargest = arr[i];
                }
            }

            Console.WriteLine("Largest = " + largest);
            Console.WriteLine("Second Largest = " + secondLargest);
            */

            /*
             25-. Consider an Array of Integer values with size N, having values as
in this Example
7 0 0 0 5 6 7 5 0 7 5 3
write a program find the longest distance between Two equal cells. In this example. The
distance is measured by the number Of cells- for example, the distance between the first and
the fourth cell is 2 (cell 2 and cell 3).
In the example above, the longest distance is between the first 7 and the
10th 7, with a distance of 8 cells, i.e. the number of cells between the 1st
And the 10th 7s.
Note:
- Array values will be taken from the user
- If you have input like 1111111 then the distance is the number of
Cells between the first and the last cell. 
             */

            /*
            Console.WriteLine("Enter the length of array : ");
            int.TryParse(Console.ReadLine(), out int Arr_length);
            int[] Arr = new int[Arr_length];
            int max_dis = 0;
            for (int i = 0; i < Arr_length; i++)
            {
                Console.Write($"Enter Element {i}: ");
                int.TryParse(Console.ReadLine(), out Arr[i]);
            }

            for (int i = 0; i < Arr_length; i++)
            {
                for (int j = 1; j < Arr_length; j++)
                {
                    if (Arr[i] == Arr[j])
                    {
                        max_dis = j - i - 1;
                    }
                }
            }
            Console.WriteLine($"the max distanse of your array ele = " + max_dis);

            */
            Console.WriteLine("Enter Your Line : ");
            string Str=Console.ReadLine();

            string[] words = Str.Split();
            string[] result = new string[words.Length];
            int j = 0;
            for (int i = words.Length - 1; i >= 0; i--)
            {
                result[j] = words[i];
                j++;
            }
          
           
                Console.Write({String.Join(result)} );
            /* 27 - Write a program to create two multidimensional arrays of same size.
          Accept value from user and store them in first array. Now copy all the
           elements of first array on second array and print second array.*/
            Console.Write("Enter number of rows: ");
            int rows = int.Parse(Console.ReadLine());

            Console.Write("Enter number of columns: ");
            int cols = int.Parse(Console.ReadLine());

            int[,] arr1 = new int[rows, cols];
            int[,] arr2 = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"Enter arr1[{i},{j}]: ");
                    arr1[i, j] = int.Parse(Console.ReadLine());
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    arr2[i, j] = arr1[i, j];
                }
            }

            Console.WriteLine("Second Array:");

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(arr2[i, j] + " ");
                }

                Console.WriteLine();
            }


        }
    }
}