using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Session_09
{
    // Primary Constructor
    //internal class User(int Id, string Name, string Email, string Password)
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }



        public override string ToString()
        {
            return $"Id: {Id} :: Name: {Name} :: Email: {Email} :: Password: {Password}";
        }
    }

    public record UserDto(int Id, string Name, string Email);
}
