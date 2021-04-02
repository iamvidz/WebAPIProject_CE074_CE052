using Online_Examination_Client.Models;
//using Online_Examination_Client.UserService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Examination_Client
{
    public partial class TeacherRegistration : System.Web.UI.Page
    {
        //UserServiceClient UserServiceClient;
        HttpClient userClient;
        HttpClient teacherClient;
        private readonly string baseAddress = ConfigurationManager.AppSettings["WebAPIServiceURL"];

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            //UserServiceClient = new UserServiceClient();

            userClient = new HttpClient();
            teacherClient = new HttpClient();

            userClient = initializeClient(userClient);
            teacherClient = initializeClient(teacherClient);

        }

        HttpClient initializeClient(HttpClient client)
        {
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        ~TeacherRegistration()
        {
            userClient.Dispose();
        }

        protected async void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                User newUser = new User();
                newUser.Email = tbusername.Text;
                newUser.Name = tbName.Text;
                newUser.Password = tbpasswd.Text;
                newUser.Role = "Teacher";
                bool res1 = await registerUser(newUser);
                if (res1)
                {
                    //TeacherService.TeacherServiceClient t = new TeacherService.TeacherServiceClient();
                    //t.AddTeacher(new TeacherService.Teacher
                    //{
                    //    Name = tbName.Text,
                    //    Email = tbusername.Text
                    //});

                    Teacher teacher = new Teacher()
                    {
                        Name = tbName.Text,
                        Email = tbusername.Text
                    };

                    HttpResponseMessage responseMessage = await teacherClient.PostAsJsonAsync("api/teacher/add", teacher);                    
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        bool res = bool.Parse(responseMessage.Content.ReadAsStringAsync().Result);
                        if (res)
                        {
                            lblRegister.Text = "Registration successfull, Use this credentials to login.";
                            lblRegister.CssClass = "label label-success";
                        }
                        else
                        {
                            lblRegister.Text = "Registration Failed, Please try again.";
                            lblRegister.CssClass = "label label-danger";
                        }
                    }
                    else
                    {
                        lblRegister.Text = "Registration Failed, Please try again.";
                        lblRegister.CssClass = "label label-danger";
                    }

                }
                else
                {
                    lblRegister.Text = "Registration Failed, Please try again.";
                    lblRegister.CssClass = "label label-danger";
                }
            }
            else
            {
                Response.Redirect("TeacherRegister.aspx", false);
            }
        }

        async Task<bool> registerUser(Models.User user)
        {
            HttpResponseMessage responseMessage = await userClient.PostAsJsonAsync("api/user/register", user);
            if (responseMessage.IsSuccessStatusCode)
            {
                bool res1 = bool.Parse(responseMessage.Content.ReadAsStringAsync().Result);
                return res1;
            }
            return false;
        }
    }
}