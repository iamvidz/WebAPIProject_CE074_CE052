using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Online_Examination_Client.Models;

namespace Online_Examination_Client
{
    public partial class AddStudent : System.Web.UI.Page
    {
        //UserService.UserServiceClient userServiceClient;
        //StudentService.StudentServiceClient studentServiceClient;
        private readonly string baseAddress = ConfigurationManager.AppSettings["WebAPIServiceURL"];

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            tbTeacher.Text = (string)Session["User"];
            //userServiceClient = new UserService.UserServiceClient();
            //studentServiceClient = new StudentService.StudentServiceClient();
        }
        protected async void btnAddStudent(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                bool res = true;
                string teacher = (string)Session["User"];
                string name = tbName.Text;
                string email = tbusername.Text;
                string passwd = tbpasswd.Text;
                string role = "Student";
                User u = new User
                {
                    Email = email,
                    Name = name,
                    Password = passwd,
                    Role = role
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Student student = new Student()
                    {
                        Name = name,
                        Email = email,
                        Password = passwd,
                        Role = role,
                        Teacher = teacher
                    };
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/student/add", student);
                    if (response.IsSuccessStatusCode)
                    {
                        if(bool.Parse(response.Content.ReadAsStringAsync().Result))
                            Response.Redirect("TeacherHome", false);
                        else
                            lblRegister.Text = "Something Went Wrong";
                    }
                    else
                    {
                        lblRegister.Text = "Something Went Wrong";
                    }
                }

                //res = res && userServiceClient.Register(u);
                //Student s = new Student
                //{
                //    Email = email,
                //    Name = name,
                //    Password = passwd,
                //    Role = role,
                //    Teacher = teacher
                //};
                //res = res && studentServiceClient.AddStudent(s);
                //if (res)
                //{
                //    Response.Redirect("TeacherHome");
                //}
                //else
                //{
                //    lblRegister.Text = "Something Went Wrong";
                //}
            }
        }
    }
}