using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Model
{
    public class User
    {
        public Guid Id { get; }
        public string Username { get; set; }
        public User(string username)
        {
            Id = Guid.NewGuid();
            Username = username;
        }
    }
}
