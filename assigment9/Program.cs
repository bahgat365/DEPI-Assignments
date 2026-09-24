using System;

namespace Assignment9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Q1
            Patient patient01 = new Patient(1, "Ahmed", "01012345678", "No history");
            Patient patient02 = new Patient(1, "Ahmed", "01012345678", "No history");

            Console.WriteLine(patient01);

            // Q2
            Console.WriteLine(patient01.GetHashCode());
            Console.WriteLine(patient02.GetHashCode());

            Console.WriteLine(patient01.Equals(patient02));

            patient01 = patient02;

            Console.WriteLine(patient01.Equals(patient02));

       

            // Q3
            PatientDto dto01 = new PatientDto(1, "Ahmed", "01012345678");
            PatientDto dto02 = new PatientDto(1, "Ahmed", "01012345678");

            Console.WriteLine(dto01.GetHashCode());
            Console.WriteLine(dto02.GetHashCode());

            Console.WriteLine(dto01.Equals(dto02));

           

            // Q4
            PatientDto dto = PatientMapper.MapFromModelToDto(patient02);
            Console.WriteLine(dto);

            // Q5 and Q6
            AppLogger log1 = AppLogger.GetLogger();
            AppLogger log2 = AppLogger.GetLogger();
            AppLogger log3 = AppLogger.GetLogger();
            AppLogger log4 = AppLogger.GetLogger();

            Console.WriteLine(log1.GetHashCode());
            Console.WriteLine(log2.GetHashCode());
            Console.WriteLine(log3.GetHashCode());
            Console.WriteLine(log4.GetHashCode());

       

            // Q7
            Patient p1 = new Patient(2, "Ali", "01111111111", "Good");

            var p2 = new Patient(3, "Omar", "01222222222", "Good");

            dynamic p3 = new Patient(4, "Sara", "01555555555", "Good");

            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.WriteLine(p3);

          

            // Q8
            var doctor01 = new
            {
                Name = "Sara",
                Specialty = "Cardiology",
                ExperienceYears = 8,
                Salary = 25_000
            };

            var doctor02 = new
            {
                Name = "Sara",
                Specialty = "Cardiology",
                ExperienceYears = 8,
                Salary = 25_000
            };

            Console.WriteLine(doctor01.Name);
            Console.WriteLine(doctor01.Specialty);

            Console.WriteLine(doctor01.GetHashCode());
            Console.WriteLine(doctor02.GetHashCode());

            Console.WriteLine(doctor01.GetType());
            Console.WriteLine(doctor01.Equals(doctor02));
            Console.WriteLine(doctor01.ToString());


            // Q10
            Console.WriteLine("Stethoscope".IsShorterThan(5));
            Console.WriteLine("Ab".Repeat(4));
        }
    }
}