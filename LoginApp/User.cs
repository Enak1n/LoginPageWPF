using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp
{
    public class User
    {
        public int id { get; set; }
        public string _username { get; set; }
        public string _password { get; set; }
        public string _email { get; set; }

        public User() { }

        public User(string username, string password, string email) 
        {
            _username = username;
            _password = password;
            _email = email;
        }
    }
}
