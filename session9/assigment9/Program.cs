namespace Session_09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Primary Constrctor & Records
            //User user01 = new User(1, "Ahmed", "ahmed@gmail.com", "123456");
            //User user02 = new User(1, "Ahmed", "ahmed@gmail.com", "123456");

            //Console.WriteLine(user01.GetHashCode());
            //Console.WriteLine(user02.GetHashCode());
            //Console.WriteLine(user02.Equals(user01));
            //user01 = user02;

            //Console.WriteLine(user02.Equals(user01));
            //Console.WriteLine(user01.ToString());


            #region Records
            //UserDto userDto01 = new UserDto(1, "Ahmed", "ahmed@gmail.com");
            //UserDto userDto02 = new UserDto(1, "Ahmed", "ahmed@gmail.com");
            //Console.WriteLine(userDto01.ToString());
            //Console.WriteLine(userDto02.ToString()); 
            //Console.WriteLine(userDto01.GetHashCode());
            //Console.WriteLine(userDto02.GetHashCode());
            //Console.WriteLine(userDto02.Equals(userDto01));

            //UserDto userDto03 = Mapper.MapFromModelToDto(user01);
            //Console.WriteLine(userDto03);
            #endregion

            #endregion

            #region Singelton
            //Car car01 = Car.GetCar();
            //Console.WriteLine(car01.GetHashCode());

            //Car car02 = Car.GetCar();
            //Console.WriteLine(car02.GetHashCode());
            //Car car03 = Car.GetCar();
            //Console.WriteLine(car03.GetHashCode());
            //Car car04 = Car.GetCar();
            //Console.WriteLine(car04.GetHashCode());

            #endregion

            #region Var & Dynamic
            //User user01 = new(1, "Ahmed", "ahmed@gmail.com", "123456");
            //int x = 5;

            //var user02 = new User(1, "Ahmed", "ahmed@gmail.com", "123456");
            //var user03= new User(1, "Ahmed", "ahmed@gmail.com", "123456");

            //dynamic user04;

            //user04 = new User(1, "Ahmed", "ahmed@gmail.com", "123456"); ;
            //Console.WriteLine(user04);

            #endregion

            #region Ananyomus Type
            //var Person1 = new
            //{
            //    Name = "Ahmed",
            //    Age = 23,
            //    Phone = 0111111111,
            //    Salary = 7_000
            //};
            //var Person3 = new
            //{
            //    Name = "Ahmed",
            //    Age = 23,
            //    Phone = 0111111111,
            //    Salary = 7_000
            //};

            //var Person2 = new
            //{
            //    Name = "Ahmed",
            //    Age = 23
            //};

            //Console.WriteLine(Person1.Age);
            //Console.WriteLine(Person1.Name);
            //Console.WriteLine(Person1.GetHashCode());
            //Console.WriteLine(Person3.GetHashCode());
            //Console.WriteLine(Person3.GetType());
            //Console.WriteLine(Person1.Equals(Person3));
            //Console.WriteLine(Person1.ToString());
            #endregion

            #region Extension Methods
            //bool isGreaterThan = StringHelper.IsLongerThan("Ahmed", 13);

            //Console.WriteLine("Ahmed".IsLongerThan(3)); // Extension Method => Extend string class
            #endregion
        }
    }
}
