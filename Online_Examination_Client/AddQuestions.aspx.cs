using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Online_Examination_Client.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;

namespace Online_Examination_Client
{
    public partial class AddQuestions : System.Web.UI.Page
    {
        //QuestionService.QuestionServiceClient questionServiceClient;
        //ExamService.ExamServiceClient examServiceClient;
        int noq = 0;
        private readonly string baseAddress = ConfigurationManager.AppSettings["WebAPIServiceURL"];

        Label GetLabel(string val)
        {
            Label l = new Label();
            l.CssClass = "form-label";
            l.Text = val;
            return l;
        }
        HtmlGenericControl getDiv()
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("class", "mb-3");
            return div;
        }
        TextBox GetTextBox(string id)
        {
            TextBox t = new TextBox();
            t.ID = id;
            t.CssClass = "form-control form-control-lg";
            return t;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //questionServiceClient = new QuestionService.QuestionServiceClient();
            //examServiceClient = new ExamService.ExamServiceClient();

            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            noq = Convert.ToInt32(Session["noq"]);
            HtmlGenericControl div = getDiv();
            for (int i = 0; i < noq; i++)
            {
                HtmlGenericControl div1 = getDiv();
                HtmlGenericControl div2 = getDiv();
                HtmlGenericControl div3 = getDiv();
                HtmlGenericControl div4 = getDiv();
                HtmlGenericControl div5 = getDiv();
                HtmlGenericControl div6 = getDiv();

                Label l1 = GetLabel("Question " + (i + 1).ToString());
                Label l2 = GetLabel("Option1");
                Label l3 = GetLabel("Option2");
                Label l4 = GetLabel("Option3");
                Label l5 = GetLabel("Option4");
                Label l6 = GetLabel("Answer");

                /*HtmlTextArea t1 = new HtmlTextArea();
                t1.Attributes.Add("class", "form-control form-control-lg");
                t1.Attributes.Add("id", "ques" + i.ToString());
                t1.Attributes.Add("runat", "server");
                t1.Attributes.Add("rows", "3");*/
                TextBox t1 = GetTextBox("ques" + i.ToString());
                TextBox t2 = GetTextBox("op1" + i.ToString());
                TextBox t3 = GetTextBox("op2" + i.ToString());
                TextBox t4 = GetTextBox("op3" + i.ToString());
                TextBox t5 = GetTextBox("op4" + i.ToString());
                TextBox t6 = GetTextBox("ans" + i.ToString());

                div1.Controls.Add(l1);
                div1.Controls.Add(t1);

                div2.Controls.Add(l2);
                div2.Controls.Add(t2);

                div3.Controls.Add(l3);
                div3.Controls.Add(t3);

                div4.Controls.Add(l4);
                div4.Controls.Add(t4);

                div5.Controls.Add(l5);
                div5.Controls.Add(t5);

                div6.Controls.Add(l6);
                div6.Controls.Add(t6);

                div.Controls.Add(div1);
                div.Controls.Add(div2);
                div.Controls.Add(div3);
                div.Controls.Add(div4);
                div.Controls.Add(div5);
                div.Controls.Add(div6);

                HtmlGenericControl brk = new HtmlGenericControl("hr");
                brk.Attributes.Add("class", "my-3");
                div.Controls.Add(brk);

            }

            Control c = this.Master.FindControl("MainContent").FindControl("quesContainer");
            if (c != null)
                c.Controls.Add(div);
        }
        protected async void btn_AddExam(object sender, EventArgs e)
        {
            bool res = true;
            List<string> questions = new List<string>();
            List<TextBox> listofques = new List<TextBox>();
            List<TextBox> op1s = new List<TextBox>();
            List<TextBox> op2s = new List<TextBox>();
            List<TextBox> op3s = new List<TextBox>();
            List<TextBox> op4s = new List<TextBox>();
            List<TextBox> answers = new List<TextBox>();
            for (int i = 0; i < noq; i++)
            {
                TextBox q = (TextBox)this.Master.FindControl("MainContent").FindControl("ques" + i.ToString());
                TextBox op1 = (TextBox)this.Master.FindControl("MainContent").FindControl("op1" + i.ToString());
                TextBox op2 = (TextBox)this.Master.FindControl("MainContent").FindControl("op2" + i.ToString());
                TextBox op3 = (TextBox)this.Master.FindControl("MainContent").FindControl("op3" + i.ToString());
                TextBox op4 = (TextBox)this.Master.FindControl("MainContent").FindControl("op4" + i.ToString());
                TextBox ans = (TextBox)this.Master.FindControl("MainContent").FindControl("ans" + i.ToString());

                if (q.Text == string.Empty || op1.Text == string.Empty || op2.Text == string.Empty || op3.Text == string.Empty || op4.Text == string.Empty || ans.Text == string.Empty)
                {
                    Label1.Text = "All questions are mandatory";
                    return;
                }
                listofques.Add(q);
                op1s.Add(op1);
                op2s.Add(op2);
                op3s.Add(op3);
                op4s.Add(op4);
                answers.Add(ans);
            }

            // task of adding questions to database begins
            for (int i = 0; i < noq; i++)
            {
                Question ques = new Question
                {
                    QuestionVal = listofques[i].Text,
                    Option1 = op1s[i].Text,
                    Option2 = op2s[i].Text,
                    Option3 = op3s[i].Text,
                    Option4 = op4s[i].Text,
                    Answer = answers[i].Text
                };

                using (var client = new HttpClient())
                {
                    string jsonString = string.Empty;
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/question/", ques);
                    if (response.IsSuccessStatusCode)
                    {
                        jsonString = response.Content.ReadAsStringAsync().Result;
                        bool resques = bool.Parse(jsonString);
                        questions.Add(ques.QuestionVal);
                        Label1.Text += resques.ToString();
                        res = res && resques;
                    }
                }

                //bool resques = questionServiceClient.AddQuestion(ques);
                //questions.Add(ques.QuestionVal);
                //Label1.Text += resques.ToString();
                //res = res && resques;
            }
            
            User loggedInUser = (User)Session["loggedInUser"];
            string name = (string)Session["User"];
            DateTime date = (DateTime)Session["date"];
            Online_Examination_Client.Models.Exam exam = new Online_Examination_Client.Models.Exam
            {
                DueTime = date,
                Questions = new List<string>(questions.ToArray()),
                Teacher = name
            };

            using (var client = new HttpClient())
            {
                string jsonString = string.Empty;
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/exam/add", exam);
                if (response.IsSuccessStatusCode)
                {
                    bool resexam = bool.Parse(response.Content.ReadAsStringAsync().Result);
                    Label1.Text += resexam.ToString();
                    res = res && resexam;
                    if (res)
                        Response.Redirect("TeacherHome", false);
                    else
                        Label1.Text = "Error";
                }
                else
                    Label1.Text = "Error";
            }

/*            bool resexam = examserviceclient.addexam(exam);
            label1.text += resexam.tostring();
            res = res && resexam;
            if (res)
            {
                response.redirect("teacherhome");
            }
            else
            {
                label1.text = "error";
            }*/
        }
    }
}