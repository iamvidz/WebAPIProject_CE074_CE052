using Online_Examination_Client.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Examination_Client
{
    public partial class _Default : Page
    {
        private readonly string baseAddress = ConfigurationManager.AppSettings["WebAPIServiceURL"];

        //UserService.UserServiceClient UserServiceClient;
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                User user = new User();
                user.Email = tbusername.Text;
                user.Password = tbpasswd.Text;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/user/login", user);

                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<User[]>();
                        readTask.Wait();

                        User[] loggedInUser = readTask.Result;
                        if (loggedInUser.Length != 1)
                        {
                            string errmsg = "Username or Password incorrect.";
                            lblLogin.Text = errmsg;
                            lblLogin.CssClass = "label label-danger";
                            return;
                        }
                        else
                        {
                            Session["loggedInUser"] = loggedInUser[0];
                            Session["User"] = loggedInUser[0].Name;
                            Session["email"] = tbusername.Text;
                            if (loggedInUser[0].Role.Contains("Teacher"))
                            {
                                Response.Redirect("TeacherHome.aspx", false);
                            }
                            else
                            {
                                Response.Redirect("StudentHome.aspx", false);
                            }
                        }
                    }
                    else
                    {
                        string errmsg = "Username or Password incorrect.";
                        lblLogin.Text = errmsg;
                        lblLogin.CssClass = "label label-danger";
                        return;
                    }

                }

                //User[] loggedInUser = UserServiceClient.Login(user);
                //if (loggedInUser.Length != 1)
                //{
                //    string errmsg = "Username or Password incorrect.";
                //    lblLogin.Text = errmsg;
                //    lblLogin.CssClass = "label label-danger";
                //    return;
                //}
                //else
                //{
                //    Session["loggedInUser"] = loggedInUser[0];
                //    Session["User"] = loggedInUser[0].Name;
                //    Session["email"] = tbusername.Text;
                //    if (loggedInUser[0].Role.Contains("Teacher"))
                //    {
                //        Response.Redirect("TeacherHome.aspx");
                //    }
                //    else
                //    {
                //        Response.Redirect("StudentHome.aspx");
                //    }
                //}
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}