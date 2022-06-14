using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model
{
    public abstract class User { 
        public User() { }
        public User(string user_name, string password, string name, string surname, Address address, Role role)
        {
            UserName = user_name;
            Password = password;
            Name = name;
            Surname = surname;
            Address = address;
            Role = role;

        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Address Address { get; set; }
        public Role Role { get; set; }

        public override string ToString()
        {
            return "User: " + UserName + " " + Password + " " + Name + " " + Surname + " " + Address + " " + Role;
        }

    }

    public enum Role
    {
        MANAGER, CLIENT
    }
}
