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
    public partial class StudentHome : System.Web.UI.Page
    {
        //UserServiceClient UserServiceClient;
        //ExamServiceClient ExamServiceClient;
        //TeacherServiceClient TeacherServiceClient;
        //StudentServiceClient StudentServiceClient;

        HttpClient userClient ;
        HttpClient examClient ;
        HttpClient teacherClient;
        HttpClient studentClient;
        private readonly string baseAddress = ConfigurationManager.AppSettings["WebAPIServiceURL"];

        public StudentHome()
        {
            //UserServiceClient = new UserServiceClient();
            //ExamServiceClient = new ExamServiceClient();
            //TeacherServiceClient = new TeacherServiceClient();
            //StudentServiceClient = new StudentServiceClient();

            userClient = new HttpClient();
            examClient = new HttpClient();
            teacherClient = new HttpClient();
            studentClient = new HttpClient();

            userClient = initializeClient(userClient);
            examClient = initializeClient(examClient);
            teacherClient = initializeClient(teacherClient);
            studentClient = initializeClient(studentClient);
        }

        HttpClient initializeClient(HttpClient client)
        {
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        ~StudentHome()
        {
            userClient.Dispose();
            examClient.Dispose();
            teacherClient.Dispose();
            studentClient.Dispose();
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            User loggedInUser = (User)Session["loggedInUser"];
            string email = (string)Session["email"];
            if (loggedInUser != null)
            {
                HttpResponseMessage response = await studentClient.PostAsJsonAsync("api/student/find-student", email);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<Student>();
                    readTask.Wait();
                    Student student = readTask.Result;

                    if (student == null)
                    {
                        // then some error is there
                        Response.Redirect("Default.aspx", false);
                    }
                    else
                    {
                        string teacherName = student.Teacher;

                        string url = "api/exam/get-all-posted-exams/" + teacherName;
                        HttpResponseMessage response1 = await examClient.GetAsync(url);

                        if (response1.IsSuccessStatusCode)
                        {
                            var readTask1 = response1.Content.ReadAsAsync<Online_Examination_Client.Models.Exam[]>();
                            readTask1.Wait();

                            Online_Examination_Client.Models.Exam[] postedExams = readTask1.Result;
                            foreach (Models.Exam exam in postedExams)
                            {

                                TableRow row = new TableRow();
                                TableCell cellOfId = new TableCell();
                                cellOfId.Text = exam.Id.ToString();

                                TableCell cellOfTeacherName = new TableCell();
                                cellOfTeacherName.Text = exam.Teacher;

                                TableCell cellOfDueTime = new TableCell();
                                cellOfDueTime.Text = exam.DueTime.ToString();

                                TableCell cellForButton = new TableCell();
                                HyperLink link = new HyperLink();
                                link.Text = "Give Exam";
                                link.CssClass = "btn btn-primary";
                                link.NavigateUrl = "Exam?id=" + exam.Id.ToString();
                                cellForButton.Controls.Add(link);

                                row.Cells.Add(cellOfId);
                                row.Cells.Add(cellOfTeacherName);
                                row.Cells.Add(cellOfDueTime);
                                row.Cells.Add(cellForButton);

                                ListOfExams.Rows.Add(row);
                            }
                            if (postedExams.Length == 0)
                            {
                                ListOfExams.Visible = false;
                                Label1.Visible = true;
                                Label1.Text += "Enjoy!! No exams now";
                                Label1.ForeColor = System.Drawing.Color.Green;

                            }
                            else
                            {
                                Label1.Enabled = false;
                            }
                        }
                        //Online_Examination_Client.Models.Exam[] postedExams = ExamServiceClient.GetAllPostedExam(teacherName);
                        // now add these exams to the page
                    }
                }
                // give list of posted exam
                //Student student = StudentServiceClient.FindStudent(email);
            }
            else
            {
                // either teacher or user not logged in
                Response.Redirect("Default.aspx", false);
            }
        }
    }
}