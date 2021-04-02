using Online_Examination_Client.Models;
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
    public partial class TeacherHome : System.Web.UI.Page
    {
        //UserServiceClient UserServiceClient;
        //TeacherServiceClient TeacherServiceClient;
        //ExamServiceClient ExamServiceClient;
        //StudentServiceClient StudentServiceClient;

        HttpClient userClient;
        HttpClient examClient;
        HttpClient teacherClient;
        HttpClient studentClient;
        private readonly string baseAddress = ConfigurationManager.AppSettings["WebAPIServiceURL"];

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        public TeacherHome()
        {
            //UserServiceClient = new UserServiceClient();
            //TeacherServiceClient = new TeacherServiceClient();
            //ExamServiceClient = new ExamServiceClient();
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

        ~TeacherHome()
        {
            userClient.Dispose();
            examClient.Dispose();
            teacherClient.Dispose();
            studentClient.Dispose();
        }

        HttpClient initializeClient(HttpClient client)
        {
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            User loggedInUser = Session["loggedInUser"] as User;
            string mail = Session["email"] as string;
            if (loggedInUser != null)
            {
                // generate list of exams and ask to create a new exam
                HttpResponseMessage response = await teacherClient.PostAsJsonAsync("api/teacher/find", mail);
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<Teacher>();
                    readTask.Wait();

                    //Teacher teacher = TeacherServiceClient.FindTeacher(mail);
                    Teacher teacher = readTask.Result;
                    string teacherName = teacher.Name;
                    await generateListOfExams(teacherName);
                    await generateListOfStudents(teacherName);
                }
                else
                    Response.Redirect("Default.aspx", false);
            }
            else
            {
                // either student or user not logged in
                Response.Redirect("Default.aspx", false);
            }
        }
        private async Task generateListOfStudents(string teacherName)
        {
            string url = "api/student/find-student-by-teacher/";
            
            HttpResponseMessage response = await studentClient.GetAsync(url + teacherName);

            if (response.IsSuccessStatusCode)
            {
                Task<List<Student>> readTask = response.Content.ReadAsAsync<List<Student>>();
                readTask.Wait();

                List<Student> students = readTask.Result;
                foreach (Student s in students)
                {
                    TableRow row = new TableRow();
                    TableCell cellOfName = new TableCell();
                    cellOfName.Text = s.Name;

                    TableCell cellOfPasswd = new TableCell();
                    cellOfPasswd.Text = s.Password;

                    TableCell cellOfEmail = new TableCell();
                    cellOfEmail.Text = s.Email;
                    row.Cells.Add(cellOfName);
                    row.Cells.Add(cellOfPasswd);
                    row.Cells.Add(cellOfEmail);
                    ListOfStudents.Rows.Add(row);
                }
                if (students.Count == 0)
                {
                    Label2.Text = "No Students Added";
                    ListOfStudents.Visible = false;
                    Label2.ForeColor = System.Drawing.Color.Green;
                }
            }

            //Student[] students = StudentServiceClient.FindStudentByTeacher(teacherName);
        }
        private async Task generateListOfExams(string teacherName)
        {
            string url = "api/exam/get-all-exams/";
            HttpResponseMessage responseMessage1 = await examClient.GetAsync(url + teacherName);
            string url2 = "null str";
            if (responseMessage1.IsSuccessStatusCode)
            {
                Task<List<Models.Exam>> readTask = responseMessage1.Content.ReadAsAsync<List<Models.Exam>>();
                readTask.Wait();

                List<Models.Exam> exams = readTask.Result;
                
                url = "api/exam/get-all-posted-exams/";
                
                HttpResponseMessage responseMessage = await examClient.GetAsync(url+teacherName);
                if (responseMessage.IsSuccessStatusCode)
                {
                    Task<List<Models.Exam>> readTask1 = responseMessage.Content.ReadAsAsync<List<Models.Exam>>();
                    readTask1.Wait();

                    List<Models.Exam> postedExams = readTask1.Result;

                    List<int> eid = new List<int>();
                    foreach (Models.Exam e in postedExams)
                    {
                        eid.Add(e.Id);
                    }
                    // now add these exams to the page
                    foreach (Models.Exam exam in exams)
                    {

                        TableRow row = new TableRow();
                        TableCell cellOfId = new TableCell();
                        cellOfId.Text = exam.Id.ToString();

                        TableCell cellOfTeacherName = new TableCell();
                        cellOfTeacherName.Text = exam.Teacher;

                        TableCell cellOfDueTime = new TableCell();
                        cellOfDueTime.Text = exam.DueTime.ToString();

                        TableCell cellForPostExam = new TableCell();
                        Button buttonToPostExam = new Button();

                        row.ID = ListOfExams.Rows.Count.ToString();
                        row.Cells.Add(cellOfId);
                        row.Cells.Add(cellOfTeacherName);
                        row.Cells.Add(cellOfDueTime);
                        if (eid.Contains(exam.Id))
                            cellForPostExam.Text = "Exam Already Posted";
                        else
                        {
                            buttonToPostExam.Text = "Post Exam";
                            buttonToPostExam.CssClass = "btn btn-secondary";
                            buttonToPostExam.CommandName = row.ID;
                            buttonToPostExam.CommandArgument = exam.Id.ToString();
                            buttonToPostExam.Command += async (Object sender, CommandEventArgs e) =>
                            {
                                bool addExamRes = await postExam(Convert.ToInt32((string)e.CommandArgument));
                                if (addExamRes)
                                {
                                    string rowID = e.CommandName;
                                    string examID = e.CommandArgument.ToString();
                                    TableRow currentRow = ListOfExams.Rows[Convert.ToInt32(rowID)];
                                    
                                    Label label = new Label();
                                    label.Text = "Exam posted successfully.";
                                    label.ForeColor = System.Drawing.Color.Green;
                                    TableCell cell = new TableCell();
                                    cell.Controls.Add(label);
                                    currentRow.Cells.Add(cell);
                                    Response.Redirect("TeacherHome", false);
                                }
                                else
                                {
                                    TableCell cell = new TableCell();
                                    Label label = new Label();
                                    label.Text = "Some error occured while posting exam!";
                                    label.ForeColor = System.Drawing.Color.Red;
                                    cell.Controls.Add(label);
                                }
                            };
                            cellForPostExam.Controls.Add(buttonToPostExam);
                        }
                        row.Cells.Add(cellForPostExam);
                        ListOfExams.Rows.Add(row);
                    }
                    if (exams.Count == 0)
                    {
                        ListOfExams.Enabled = false;
                        ListOfExams.Visible = false;
                        Label1.Text += "No Exams created.";
                        Label1.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        Label1.Enabled = false;
                    }
                }
                //ExamService.Exam[] postedExams = ExamServiceClient.GetAllPostedExam(teacherName);

            }
            //Models.Exam[] exams = ExamServiceClient.GetAllExam(teacherName);


        }

        async Task<bool> postExam(int examid)
        {
            string url = "api/exam/postexam/" + examid;
            HttpResponseMessage responseMessage = await examClient.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                bool res = bool.Parse(responseMessage.Content.ReadAsStringAsync().Result);
                return res;
            }
            return false;
        }

        protected void btn_AddExam(object sender, EventArgs e)
        {
            Response.Redirect("AddExams", false);
        }

        protected void btn_AddStudent(object sender, EventArgs e)
        {
            Response.Redirect("AddStudent", false);
        }
    }
}