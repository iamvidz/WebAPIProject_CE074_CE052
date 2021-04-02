using Online_Examination.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Online_Examination.Controllers
{
    public class QuestionController : ApiController
    {
        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            return con;
        }

        [HttpPost]
        [Route("api/question")]
        // POST: api/question
        public bool Post(Question question)
        {
            try
            {
                SqlConnection con = GetConnection();
                string query = "insert into [question] values(@Question, @op1, @op2, @op3, @op4, @ans)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Question", question.QuestionVal);
                cmd.Parameters.AddWithValue("@op1", question.Option1);
                cmd.Parameters.AddWithValue("@op2", question.Option2);
                cmd.Parameters.AddWithValue("@op3", question.Option3);
                cmd.Parameters.AddWithValue("@op4", question.Option4);
                cmd.Parameters.AddWithValue("@ans", question.Answer);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0) return true;
                else return false;
            }
            catch (SqlException sqlex)
            {
                return false;
            }
        }

        [Route("api/question/")]
        public bool Delete([FromBody]string question)
        {
            SqlConnection con = GetConnection();
            string query = "delete from [question] where question=@q";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@q", question);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result > 0) return true;
            else return false;
        }

        [HttpPost]
        [Route("api/question/find-question/")]
        public Question FindQuestion([FromBody]string question)
        {
            Question q = new Question();
            SqlConnection con = GetConnection();
            string query = "select * from [question] where question=@q";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@q", question);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                q.QuestionVal = reader[0].ToString();
                q.Option1 = reader[1].ToString();
                q.Option2 = reader[2].ToString();
                q.Option3 = reader[3].ToString();
                q.Option4 = reader[4].ToString();
                q.Answer = reader[5].ToString();
            }
            con.Close();
            return q;
        }

        [HttpGet]
        [Route("api/question/get-all-questions")]
        public List<Question> GetAllQuestions()
        {
            List<Question> questions = new List<Question>();
            SqlConnection con = GetConnection();
            string query = "select * from [question]";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Question q = new Question
                {
                    QuestionVal = reader[0].ToString(),
                    Option1 = reader[1].ToString(),
                    Option2 = reader[2].ToString(),
                    Option3 = reader[3].ToString(),
                    Option4 = reader[4].ToString(),
                    Answer = reader[5].ToString(),
                };
                questions.Add(q);
            }
            return questions;
        }

        public class PostQuestionRequest
        {
            public string question { get; set; }
            public string answer { get; set; }
        }

        [HttpPost]
        [Route("api/question/is-correct")]
        public bool IsCorrect([FromBody]PostQuestionRequest request)
        {
            Question q = FindQuestion(request.question);
            if (request.answer == q.Answer) return true;
            else return false;
        }

        public class PutQuestionRequest
        {
            public string question { get; set; }
            public Question updatedVal { get; set; }
        }
        
        [Route("api/question")]
        public bool Put([FromBody]PutQuestionRequest request)
        {
            SqlConnection con = GetConnection();
            string query = "update [question] set question=@q1, op1=@op1, op2=@op2, op3=@op3, op4=@op4, ans=@a where question=@q";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@q1", request.updatedVal.QuestionVal);
            cmd.Parameters.AddWithValue("@op1", request.updatedVal.Option1);
            cmd.Parameters.AddWithValue("@op2", request.updatedVal.Option2);
            cmd.Parameters.AddWithValue("@op3", request.updatedVal.Option3);
            cmd.Parameters.AddWithValue("@op4", request.updatedVal.Option4);
            cmd.Parameters.AddWithValue("@a", request.updatedVal.Answer);
            cmd.Parameters.AddWithValue("@q", request.question);
            con.Open();

            int result = cmd.ExecuteNonQuery();
            if (result > 0) return true;
            else return false;
        }
    }
}
