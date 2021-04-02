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
    //[Route("api/student")]// if error in route occur then try removing last '/'
    public class StudentController : ApiController
    {
        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            return con;
        }

        [HttpPost]
        [Route("api/student/add")]
        public bool AddStudent(Student student)
        {
            try
            {
                SqlConnection con = GetConnection();
                string query = "insert into [student] values(@Name, @Email, @Role, @Teacher, @Password)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Password", student.Password);
                cmd.Parameters.AddWithValue("@Role", student.Role);
                cmd.Parameters.AddWithValue("@Teacher", student.Teacher);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result <= 0) return false;
                else
                {
                    User u = new User
                    {
                        Email = student.Email,
                        Name = student.Name,
                        Password = student.Password,
                        Role = "Student"
                    };
                    UserController userController = new UserController();
                    bool res = userController.Register(u);
                    return res;
                }
            }
            catch (SqlException sqlex)
            {
                return false;
            }
        }

        [Route("api/student")]
        public bool Delete([FromBody]string email)
        {
            SqlConnection con = GetConnection();
            string query = "delete from [student] where email=@Email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result > 0) return true;
            else return false;
        }

        [HttpPost]
        [Route("api/student/find-student")]
        public Student FindStudent([FromBody]string email)
        {
            Student s = new Student();
            SqlConnection con = GetConnection();
            string query = "select * from [student] where email=@Email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                s.Name = reader[0].ToString();
                s.Email = reader[1].ToString();
                s.Role = reader[2].ToString();
                s.Teacher = reader[3].ToString();
                s.Password = reader[4].ToString();
            }
            con.Close();
            return s;
        }

        [HttpGet]
        [Route("api/student/find-student-by-teacher/{teacher}")]
        public List<Student> FindStudentByTeacher(string teacher)
        {
            List<Student> students = new List<Student>();
            SqlConnection con = GetConnection();
            string query = "select * from [student] where Teacher=@t";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@t", teacher);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Student s = new Student
                {
                    Name = reader[0].ToString(),
                    Email = reader[1].ToString(),
                    Role = reader[2].ToString(),
                    Teacher = reader[3].ToString(),
                    Password = reader[4].ToString()
                };
                students.Add(s);
            }
            return students;
        }

        public class PutStudentRequest
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
            public string Teacher { get; set; }
            public string currentEmail { get; set; }
        }

        // PUT: used to update details of the student
        [HttpPost]
        [Route("api/student/")]
        public bool Put([FromBody] PutStudentRequest studentRequest)
        {
            SqlConnection con = GetConnection();
            string query = "update [student] set name=@Name,email=@Email,passwd=@p,role=@r,teacher=@t where email=@E";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", studentRequest.Name);
            cmd.Parameters.AddWithValue("@Email", studentRequest.Email);
            cmd.Parameters.AddWithValue("@p", studentRequest.Password);
            cmd.Parameters.AddWithValue("@r", studentRequest.Role);
            cmd.Parameters.AddWithValue("@t", studentRequest.Teacher);
            cmd.Parameters.AddWithValue("@E", studentRequest.currentEmail);
            con.Open();

            int result = cmd.ExecuteNonQuery();
            if (result > 0) return true;
            else return false;
        }
    }
}
