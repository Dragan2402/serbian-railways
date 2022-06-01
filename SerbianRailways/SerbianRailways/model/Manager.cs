using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model
{
    public class Manager : User
    {
        public Manager() : base()
        {
            Role = Role.MANAGER;
        }

        public Manager(string user_name, string password, string name, string surname, Address address) : base(user_name, password, name, surname, address, Role.MANAGER)
        {

        }


    }
}
