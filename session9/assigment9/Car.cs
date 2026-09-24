using System;
using System.Collections.Generic;
using System.Text;

namespace Session_09
{
    internal class Car
    {
        public int ID { get; set; }
        public string Model { get; set; }
        private static Car myCar = null;
        private Car()
        {
        }

        public static Car GetCar()
        {
            if (myCar is null)
                myCar = new Car();

            return myCar;
        }
    }
}
