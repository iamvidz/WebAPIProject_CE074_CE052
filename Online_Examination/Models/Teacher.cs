using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Examination.Models
{
    public class Teacher
    {
        string name = string.Empty;
        string email = string.Empty;
            
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

    }
}