using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Examination.Models
{
    public class Question
    {
        string question = string.Empty;
        string op1 = string.Empty;
        string op2 = string.Empty;
        string op3 = string.Empty;
        string op4 = string.Empty;
        string ans = string.Empty;

        
        public string QuestionVal
        {
            get { return question; }
            set { question = value; }
        }
        
        public string Option1
        {
            get { return op1; }
            set { op1 = value; }
        }
        
        public string Option2
        {
            get { return op2; }
            set { op2 = value; }
        }
        
        public string Option3
        {
            get { return op3; }
            set { op3 = value; }
        }
        
        public string Option4
        {
            get { return op4; }
            set { op4 = value; }
        }
        
        public string Answer
        {
            get { return ans; }
            set { ans = value; }
        }
    }
}