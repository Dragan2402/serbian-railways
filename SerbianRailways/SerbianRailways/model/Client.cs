using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model
{
    public class Client : User
    {
        public Client() : base()
        {
            Role = Role.CLIENT; 
        }

        public Client(string user_name, string password, string name, string surname, Address address): base(user_name, password, name, surname, address, Role.CLIENT)
        {
            Tickets = new List<Ticket>();
        }

        public List<Ticket> Tickets { get; set; }

    }
}
