using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Xml.Linq;
using static Practise._ِAssigment6;

namespace Practise
{
    internal class _ِAssigment6
    {
        //interfacses
        public interface IMoveable
        {
            void MoveForward();
            void MoveBackward();
        }
       public interface IFlayable
        {
            void MoveUp();
            void MoveDown();
        }

        //funcitons , classes and enum 
        public class Shap
        {
            public double Width { get; set; }
            public double Height { get; set; }

            //constractor
            //Q1 to get Area
            public Shap(double width, double height)
            {
                Width = width;
                Height = height;
            }

            //method
            public double Area()
            {
                return (Width * Height);
            }
            public override string ToString()
            {
                return (
                    string.Format("Width={0} , Hieght={1} ", Width, Height)

                    );
            }

        }
        //Class cube 
        class Cube : Shap
        {
             double Depth;

            //consrtactor
            public Cube(double depth, double width, double height) : base(width, height)
            {
                Depth = depth;
            }
            //method
            public new double Area()
            {
                return (base.Area() * Depth);
            }

            //print method 
            public void Print()
            {
                Console.WriteLine(string.Format("Width = {0} , Height = {1} , Depth ={2} ", base.Width, base.Height, Depth));
            }

}
        // person bais class
       public class person
        {
            //prop
            int Id { get; set; }
            int Age { get; set; }
            string Name { get; set; }
            //constraactorr
            public person(int id, int age, string name) { Id = id; Age = age; Name = name; }
            //methods
            public void Greet()
            {
                Console.WriteLine("I am a person's basic dataPerson.");    
            }
            public virtual void Display()
            {
                Console.WriteLine("Id : "+Id);
                Console.WriteLine("Age : "+Age);
                Console.WriteLine("Name : "+Name);

            }
        }
        public class doctor : person
            {
                string Hospetal_add { get; set; }
                string Field { get; set; }
            int Yeas_Of_Ex { get; set; }
            public doctor(string hospetal_add,string field, int yeas_Of_Ex , int id,int age,string name):base(id, age,  name) 
                {
                    Hospetal_add = hospetal_add;
                    Field = field;
                    Yeas_Of_Ex = yeas_Of_Ex;
                }
                //method
                public new void Greet()
                {
                    Console.WriteLine("Hi im doctor ");
                       
                }
                public override void Display() {
                    base .Display();
                    Console.WriteLine("Hospetal_add : "+Hospetal_add);
                    Console.WriteLine("Field : " + Field);
                    Console.WriteLine("years of ex : " + Yeas_Of_Ex);
                }
              }

        // eng class 
        public class Engineer : person 
        { public string Field { get; set; }
            public int YearsOfExperience { get; set; } 
            public Engineer(int id, string name, int age, string field, int yearsOfExperience) : base(id, age, name)
            { Field = field; YearsOfExperience = yearsOfExperience; }
            public new void Greet()
            { Console.WriteLine("Hi, I am an engineer."); 
            } 
            public override void Display() 
            { base.Display(); Console.WriteLine("Field : " + Field); 
                Console.WriteLine("Years of Experience : " + YearsOfExperience);
            }
        }
        class Car
        {
            public void MoveForward()
            {
                Console.WriteLine("Moving Forward");
            }
            public void MoveBack()
            {
                Console.WriteLine("Moving Back");
            }

        }

        //the Problem is 1/redunduncy 2/easy to change or modify class without change all of them
        class car : IMoveable{
            public void MoveForward()
            {
                Console.WriteLine("car is moving forward");
            }
            public void MoveBackward()
            {
                Console.WriteLine("car is moving Backward");
            }

        }
        class ship:IMoveable {
           public void MoveForward()
            {
                Console.WriteLine("ship is moving forward in sea");
            }
           public void MoveBackward()
            {
                Console.WriteLine("ship is moving Backward in sea");
            }

        }
        class AirPlane : IMoveable, IFlayable
        {

            public void MoveForward()
            {
                Console.WriteLine("plane is moving forward in sea");
            }
            public void MoveBackward()
            {
                Console.WriteLine("plane is moving Backward in sea");
            }
            public void MoveUp()
            {
                Console.WriteLine("Plane Is Moving Up");
            }
            public void MoveDown()
            {
                Console.WriteLine("plane is Moving Down");
            }

        }

        //Method Process Person
        public static void ProcessPerson(person p)
        {
            p.Greet();
            p.Display();
        }




        //main func
        static void Main(string[] args)
            {
            /*
                Shap a = new Shap(10, 15);
                Console.WriteLine(a);
                Console.WriteLine("the Area Is: " + a.Area());
            */

            //======================================
            /*
            Cube C = new Cube(15,10,15);
            Console.WriteLine("the cube area is :  "+ C.Area());
            Console.WriteLine("The dimintians are \n");
            C.Print();
            */
            //===========================================
            /*Shap shapRef = new Cube(2,3,4); 
            Console.WriteLine( shapRef.Area());*/

            //object obj =new Cube(1,2,3);

            // Console.WriteLine(obj.ToString());
            // It will call Shape's ToString() because ToString() is virtual,
            // so the runtime uses the overridden version in Shape.

            //==============================================
           /* 
           person eng = new Engineer(10,"mohammed",20,"CS",0);
            ProcessPerson(eng);
           */
           //==============================================
           car car1 = new car();
            car1.MoveBackward();
            car1.MoveForward();
            Console.WriteLine("===========================");
            ship ship1 = new ship();
            ship1.MoveBackward();
            ship1.MoveForward();
            Console.WriteLine("===========================");

            AirPlane plane1 = new AirPlane();
            plane1 .MoveForward();
            plane1.MoveBackward();
            plane1.MoveDown();
            plane1.MoveUp();
            Console.WriteLine("===========================");
            IMoveable carRef = new car();
            carRef.MoveForward();
            IMoveable planeRef = new AirPlane();
            planeRef .MoveBackward();
        }
    }
}
