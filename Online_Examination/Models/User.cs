using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Examination.Models
{
    public class User
    {
        string name = string.Empty;
        string username = string.Empty;
        string passwd = string.Empty;
        string role;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Email
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get { return passwd; }
            set { passwd = value; }
        }
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
    }
}