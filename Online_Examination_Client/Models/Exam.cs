using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Examination_Client.Models
{
    public class Exam
    {
        int examid;
        List<string> questions;
        DateTime dueTime;
        string teacher;

        public int Id
        {
            get { return examid; }
            set { examid = value; }
        }

        public List<string> Questions
        {
            get { return questions; }
            set { questions = value; }
        }

        public DateTime DueTime
        {
            get { return dueTime; }
            set { dueTime = value; }
        }

        public string Teacher
        {
            get { return teacher; }
            set { teacher = value; }
        }
    }
}