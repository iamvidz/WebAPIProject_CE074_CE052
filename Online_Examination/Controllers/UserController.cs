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
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/user/login")]
        public List<User> Login([FromBody]User user)
        {
            List<User> users = new List<User>();
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from [user] where username=@UserName and passwd=@Password", con);
            cmd.Parameters.AddWithValue("@UserName", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                User u = new User
                {
                    Email = dr[1].ToString(),
                    Name = dr[0].ToString(),
                    Password = dr[2].ToString(),
                    Role = dr[3].ToString()
                };
                users.Add(u);
            }
            con.Close();
            return users;
        }

        [HttpPost]
        [Route("api/user/register")]
        public bool Register([FromBody] User user)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=classroomSoc;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO [User] (name, username, passwd, role) VALUES (@Name, @Username, @Password, @Role)", con);
                cmd.Parameters.AddWithValue("@Username", user.Email);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Role", user.Role);

                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException sqlex)
            {
                return false;
            }
        }
    }
}
