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
    public class TeacherController : ApiController
    {
        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            return con;
        }

        [HttpPost]
        [Route("api/teacher/add")]
        public bool AddTeacher([FromBody]Teacher teacher)
        {
            try
            {
                SqlConnection con = GetConnection();
                string query = "insert into [teacher] values(@Name, @Email)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", teacher.Name);
                cmd.Parameters.AddWithValue("@Email", teacher.Email);
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

        [HttpDelete]
        [Route("api/teacher")]
        public bool Delete([FromBody]string email)
        {
            SqlConnection con = GetConnection();
            string query = "delete from [teacher] where email=@Email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result > 0) return true;
            else return false;
        }

        [HttpPost]
        [Route("api/teacher/find")]
        public IHttpActionResult FindTeacher([FromBody]string email)
        {
            Teacher t = new Teacher();
            SqlConnection con = GetConnection();
            string query = "select * from [teacher] where email=@Email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if(!reader.HasRows)
            {
                return NotFound();
            }
            if (reader.Read() == true)
            {
                t.Email = reader[1].ToString();
                t.Name = reader[0].ToString();
            }
            con.Close();
            return Ok(t);
        }
    }
}
