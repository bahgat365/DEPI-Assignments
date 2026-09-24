using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assigment7
{
    internal class Program
    {
        class point3d : IComparable, ICloneable
        {
            int x;
            int y;
            int z;
            public point3d()
            {
                x = 0;
                y = 0;
                z = 0;
            }
            public point3d(int X) : this()
            {
                x = X;
            }
            public point3d(int X, int Y) : this(X)
            {
                y = Y;
            }
            public point3d(int X, int Y, int Z) : this(X, Y)
            {
                z = Z;
            }


            //methods
            public override string ToString()
            {
                return string.Format("Point Coordinates :({0},{1},{2})", x, y, z);
            }
            public override bool Equals(object? obj)
            {
                if (obj is point3d other)
                {
                    return x == other.x && z == other.z && y == other.y;
                }
                return false;
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(x, y, z);
            }
            public int CompareTo(object? obj)
            {
                if (obj is point3d other)
                {
                    if (other.x != x)
                    {
                        return x.CompareTo(other.x);
                    }
                    if (y != other.y)
                        return y.CompareTo(other.y);

                    return z.CompareTo(other.z);
                }
                return 1;
            }

            public object Clone()
            {
                return new point3d(x, y, z);

            }

        }
        class math
        {

            //mah methods 
            public static int Add(int x, int y)
            {
                return x + y;
            }
            public static int sub(int x, int y)
            {
                return x - y;
            }
            public static int mult(int x, int y)
            {
                return x * y;
            }
            public static int div(int x, int y)
            {
                return x / y;
            }

        }
        class duration
        {
            int Hours;
            int Minutes;
            int Seconds;

            public duration(int seconds)
            {
                Hours = seconds / 3600;
                Minutes = (seconds % 3600) / 60;
                Seconds = seconds % 60;
            }
            public duration(int hours, int minutes, int seconds) : this(seconds)
            {
                Hours = hours; Minutes = minutes;
            }

            //methods
            public override string ToString()
            {
                return string.Format("Hours= {0} , Minutes= {1},Seconds= {2}", Hours, Minutes, Seconds);
            }
            public override bool Equals(object? obj)
            {
                if (obj is duration other)
                {
                    return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
                }
                return false;
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(Hours, Minutes, Seconds);
            }
        }
        class Date
        {
            int Hours;
            int Munits;
            int Seconds;

            public Date(int hours, int munits, int seconds)
            {
                Hours = hours;
                Munits = munits;
                Seconds = seconds;
            }

            // D3 = D1 + D2
            public static Date operator +(Date d1, Date d2)
            {
                int Seconds = d1.Seconds + d2.Seconds;

                int Munits = d1.Munits + d2.Munits + Seconds / 60;
                Seconds = Seconds % 60;

                int Hours = d1.Hours + d2.Hours + Munits / 60;
                Munits = Munits % 60;

                return new Date(Hours, Munits, Seconds);
            }

            // D1 = D1 - D2
            public static Date operator -(Date d1, Date d2)
            {
                int total1 = d1.Hours * 3600 + d1.Munits * 60 + d1.Seconds;
                int total2 = d2.Hours * 3600 + d2.Munits * 60 + d2.Seconds;

                int total = total1 - total2;

                int Hours = total / 3600;
                total = total % 3600;

                int Munits = total / 60;
                int Seconds = total % 60;

                return new Date(Hours, Munits, Seconds);
            }

            // D3 = D1 + 7800
            public static Date operator +(Date d, int seconds)
            {
                int total = d.Hours * 3600
                          + d.Munits * 60
                          + d.Seconds
                          + seconds;

                int Hours = total / 3600;
                total = total % 3600;

                int Munits = total / 60;
                int Seconds = total % 60;

                return new Date(Hours, Munits, Seconds);
            }

            // D3 = D1 - 7800
            public static Date operator -(Date d, int seconds)
            {
                int total = d.Hours * 3600
                          + d.Munits * 60
                          + d.Seconds
                          - seconds;

                int Hours = total / 3600;
                total = total % 3600;

                int Munits = total / 60;
                int Seconds = total % 60;

                return new Date(Hours, Munits, Seconds);
            }

            // D3 = 666 + D3
            public static Date operator +(int seconds, Date d)
            {
                return d + seconds;
            }

            // D3 = ++D1
            public static Date operator ++(Date d)
            {
                d.Munits++;

                if (d.Munits == 60)
                {
                    d.Munits = 0;
                    d.Hours++;
                }

                return d;
            }

            // D3 = --D2
            public static Date operator --(Date d)
            {
                d.Munits--;

                if (d.Munits < 0)
                {
                    d.Munits = 59;
                    d.Hours--;
                }

                return d;
            }

            // D1 > D2
            public static bool operator >(Date d1, Date d2)
            {
                if (d1.Hours != d2.Hours)
                    return d1.Hours > d2.Hours;

                if (d1.Munits != d2.Munits)
                    return d1.Munits > d2.Munits;

                return d1.Seconds > d2.Seconds;
            }

            // D1 < D2
            public static bool operator <(Date d1, Date d2)
            {
                if (d1.Hours != d2.Hours)
                    return d1.Hours < d2.Hours;

                if (d1.Munits != d2.Munits)
                    return d1.Munits < d2.Munits;

                return d1.Seconds < d2.Seconds;
            }

            // D1 >= D2
            public static bool operator >=(Date d1, Date d2)
            {
                return d1 > d2 || d1 == d2;
            }

            // D1 <= D2
            public static bool operator <=(Date d1, Date d2)
            {
                return d1 < d2 || d1 == d2;
            }

            public override string ToString()
            {
                return $"{Hours:D2}:{Munits:D2}:{Seconds:D2}";
            }
        }
        static void Main(string[] args)
            {
                /*
                Console.WriteLine("Enter The Coordinates of 2 points ");

                point3d[] arr_points = new point3d[2];
                for (int i = 0; i < arr_points.Length; i++)
                {
                    Console.Write($"Enter point {i + 1} x : ");
                    int.TryParse(Console.ReadLine(), out int x);

                    Console.Write($"Enter point {i + 1} y : ");
                    int.TryParse(Console.ReadLine(), out int y);

                    Console.Write($"Enter point {i + 1} z : ");
                    int.TryParse(Console.ReadLine(), out int z);

                    arr_points[i] = new point3d(x, y, z);
                }
                Console.WriteLine("\nPoints:");

                for (int i = 0; i < arr_points.Length; i++)
                {
                    Console.WriteLine(arr_points[i]);
                }
                if (arr_points[1] == arr_points[0])
                {
                    Console.WriteLine("they are equal ===");
                }
                else
                    Console.WriteLine("They are not equal ");
                */
                //it will be alway not equal because the donsnt have the same ref and == chek value and ref so thats why we use equals to focuas in vlaues equalitys

                //point3d p1=new point3d(0,0,0);
                // p2=new point3d(0,0,0);
                //Console.WriteLine(p1.Equals(p2));//printed ture
                // Define an array of points and sort this array based on X &
                // Y coordinates.

                /*

                  point3d[] arr_points = {
                  new point3d(0,1,2),
                   new point3d(0,8,2),
                   new point3d(0,1,5),
               new point3d(8,1,2),
                };

                Array.Sort(arr_points);

                for (int i = 0; i < arr_points.Length; i++)
                {
                    Console.WriteLine(arr_points[i].ToString());
                }
                */
                /*
                point3d p1 = new point3d(10, 20, 30);

                point3d p2 = (point3d)p1.Clone();

                Console.WriteLine(p1);
                Console.WriteLine(p2);
                */
                /*
                Console.WriteLine(math.Add(10, 20));
                Console.WriteLine(math.sub(10, 20));
                Console.WriteLine(math.div(10, 20));
                Console.WriteLine(math.mult(10, 20));
                */
                /*
                duration d1 = new duration(1200);
                Console.WriteLine(d1.ToString());
            duration d2 = new duration(55555);
            Console.WriteLine(d2.ToString());
            duration d3 = new duration(10,20,10);
            Console.WriteLine(d3.ToString());
                */

        }

        
    }
}

  

