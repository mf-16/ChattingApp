using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Model
{
    public class User
    {
        private Guid _id;
        public string Username { get; set; }
        public User(string username)
        {
            _id = Guid.NewGuid();
            Username = username;
        }
    }
}
