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
    public class ExamController : ApiController
    {

        [HttpGet]
        [Route("api/exam/count")]
        public int GetLen()
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            con.Open();
            string query = "Select COUNT(id) from [exam]";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
            con.Close();
            return count;
        }

        public int getExLen()
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            con.Open();
            string q = "Select count(examid) from [exam]";
            SqlCommand c = new SqlCommand(q, con);
            int re = Convert.ToInt32(c.ExecuteScalar());
            if (re != 0)
            {
                string query = "Select MAX(examid) from [exam]";
                SqlCommand cmd = new SqlCommand(query, con);
                var res = cmd.ExecuteScalar();
                int cnt = Convert.ToInt32(res);
                return cnt;
            }
            con.Close();
            return 0;
            
        }

        [HttpPost]
        [Route("api/exam/add")]
        public bool AddExam([FromBody] Exam exam)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            con.Open();
            bool result = true;
            int examid = getExLen() + 1;
            foreach (string q in exam.Questions)
            {
                int id = GetLen() + 1;
                string query = "Insert into [exam] values(@id,@question,@time,@teacher,@examid)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@question", q);
                cmd.Parameters.AddWithValue("@time", exam.DueTime);
                cmd.Parameters.AddWithValue("@teacher", exam.Teacher);
                cmd.Parameters.AddWithValue("@examid", examid);
                int res = cmd.ExecuteNonQuery();
                if (res > 0) result = result & true;
                else result = false;
            }
            con.Close();
            return result;
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            con.Open();
            string query = "delete from [exam] where examid=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            int res = cmd.ExecuteNonQuery();

            query = "delete from [postedexam] where examid=@id";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            res += cmd.ExecuteNonQuery();
            con.Close();
            if (res > 0) return true;
            else return false;
        }

        [HttpGet]
        [Route("api/exam/{examid}")]
        public IHttpActionResult Get(int examid)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            con.Open();
            string query = "select * from [exam] where examid=@e";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@e", examid);
            SqlDataReader rd = cmd.ExecuteReader();
            if (! rd.HasRows)
            {
                con.Close();
                return NotFound();
            }
            Exam ex = new Exam();
            List<string> ques = new List<string>();
            int id = 0, exam_id = 0;
            string question, Teacher = string.Empty;
            DateTime d = new DateTime();
            while (rd.Read())
            {
                id = rd.GetInt32(0);
                question = rd[1].ToString();
                Teacher = rd[3].ToString();
                d = rd.GetDateTime(2);
                exam_id = rd.GetInt32(4);
                ques.Add(question);
            }
            ex.Questions = ques;
            ex.Id = exam_id;
            ex.DueTime = d;
            ex.Teacher = Teacher;
            con.Close();
            return Ok(ex);
        }

        [HttpGet]
        [Route("api/exam/postexam/{examid}")]
        public bool PostExam(int examid)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            con.Open();
            Exam exam = getExam(examid);
            bool result = true;
            foreach (string q in exam.Questions)
            {
                string qry = "Select COUNT(id) from [postedexam]";
                SqlCommand c = new SqlCommand(qry, con);
                int rs = Convert.ToInt32(c.ExecuteScalar());
                string query = "Insert into [postedexam] values(@id,@question,@time,@teacher,@examid)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", rs + 1);
                cmd.Parameters.AddWithValue("@question", q);
                cmd.Parameters.AddWithValue("@time", exam.DueTime);
                cmd.Parameters.AddWithValue("@teacher", exam.Teacher);
                cmd.Parameters.AddWithValue("@examid", exam.Id);
                int res = cmd.ExecuteNonQuery();
                if (res > 0) result = result & true;
                else result = false;
            }
            con.Close();
            return result;
        }

        public Exam getExam(int examid)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            con.Open();
            string query = "select * from [exam] where examid=@e";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@e", examid);
            SqlDataReader rd = cmd.ExecuteReader();
            if (!rd.HasRows)
            {
                con.Close();
                return null;

            }
            Exam ex = new Exam();
            List<string> ques = new List<string>();
            int id = 0, exam_id = 0;
            string question, Teacher = string.Empty;
            DateTime d = new DateTime();
            while (rd.Read())
            {
                id = rd.GetInt32(0);
                question = rd[1].ToString();
                Teacher = rd[3].ToString();
                d = rd.GetDateTime(2);
                exam_id = rd.GetInt32(4);
                ques.Add(question);
            }
            ex.Questions = ques;
            ex.Id = exam_id;
            ex.DueTime = d;
            ex.Teacher = Teacher;
            con.Close();
            return ex;
        }

        [HttpGet]
        [Route("api/exam/get-all-posted-exams/{teacher}")]
        public List<Exam> GetAllPostedExam(string teacher)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            con.Open();
            List<Exam> exams = new List<Exam>();
            string query = "select distinct examid from [postedexam] where teacher=@t";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@t", teacher);
            SqlDataReader rd = cmd.ExecuteReader();
            if(!rd.HasRows)
            {
                exams.Clear();
                con.Close();
                return exams;
            }
            while (rd.Read())
            {
                int exid = rd.GetInt32(0);
                Exam e = getExam(exid);
                exams.Add(e);
            }
            con.Close();
            return exams;
        }

        [HttpGet]
        [Route("api/exam/get-all-exams/{teacher}")]
        public List<Exam> GetAllExams(string teacher)
        {
            List<Exam> exams = new List<Exam>();
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            con.Open();
            string query = "select distinct examid from [exam] where teacher=@t";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@t", teacher);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                int exid = rd.GetInt32(0);
                Exam e = getExam(exid);
                exams.Add(e);
            }
            con.Close();
            return exams;
        }
    }
}
