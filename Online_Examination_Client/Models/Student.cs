using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Examination_Client.Models
{
    public class Student : User
    {
        string teacher = string.Empty;

        public string Teacher
        {
            get { return teacher; }
            set { teacher = value; }
        }
    }
}