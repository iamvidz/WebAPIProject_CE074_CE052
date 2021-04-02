using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Online_Examination_Client.Models;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Online_Examination_Client
{
    public partial class Exam : System.Web.UI.Page
    {   
        int examid = 0;
        
        //QuestionService.QuestionServiceClient questionServiceClient;
        //ExamService.ExamServiceClient examServiceClient;
        
        List<Question> questions = new List<Question>();
        string baseAddress = ConfigurationManager.AppSettings["WebAPIServiceURL"];
        
        
        Label GetLabel(string val)
        {
            Label l = new Label();
            l.CssClass = "form-label";
            l.Text = val;
            return l;
        }
        HtmlGenericControl getCustomDiv(string cssclass)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("class", cssclass);
            return div;
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

        protected async void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            examid = Convert.ToInt32(Request.QueryString["id"]);

            //questionServiceClient = new QuestionService.QuestionServiceClient();
            //examServiceClient = new ExamService.ExamServiceClient();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "api/exam/" + examid.ToString();
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<Online_Examination_Client.Models.Exam>();
                    readTask.Wait();

                    Online_Examination_Client.Models.Exam exam = readTask.Result;
                    
                    int noq = exam.Questions.Count;
                    for (int i = 0; i < noq; i++)
                    {
                        string question = exam.Questions[i];
                        HtmlGenericControl d = getCustomDiv("row my-3");
                        HtmlGenericControl d1 = getCustomDiv("offset-4 col-6");
                        d.Controls.Add(d1);
                        HtmlGenericControl q = new HtmlGenericControl("h4");
                        q.Attributes.Add("class", "font-weight-bold form-label");
                        q.InnerText = (i + 1).ToString() + " " + question;

                        /*client.BaseAddress = new Uri(baseAddress);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
*/
                        response = await client.PostAsJsonAsync("api/question/find-question/", question);
                        if (response.IsSuccessStatusCode)
                        {
                            var readTask1 = response.Content.ReadAsAsync<Question>();
                            readTask1.Wait();
                            Question ques = readTask1.Result;

                            questions.Add(ques);
                            RadioButtonList rdr = new RadioButtonList();
                            rdr.Items.Add(new ListItem(ques.Option1, ques.Option1));
                            rdr.Items.Add(new ListItem(ques.Option2, ques.Option2));
                            rdr.Items.Add(new ListItem(ques.Option3, ques.Option3));
                            rdr.Items.Add(new ListItem(ques.Option4, ques.Option4));
                            rdr.ID = "que" + i.ToString();
                            rdr.CssClass = "mx-3 p-3";
                            d.Controls.Add(q);
                            d.Controls.Add(rdr);
                            this.Master.FindControl("MainContent").FindControl("ques").Controls.Add(d);
                        }
                        //QuestionService.Question ques = questionServiceClient.FindQuestion(question);
                    }
                }

            }

            //ExamService.Exam exam = examServiceClient.GetExam(examid);
        }

        protected async void Submit_Exam(object sender, EventArgs e)
        {
            int score = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                RadioButtonList ans = (RadioButtonList)this.Master.FindControl("MainContent").FindControl("que" + i.ToString());
                Question q = questions[i];

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    PostQuestionRequest postQuestionRequest = new PostQuestionRequest
                    {
                        question = q.QuestionVal,
                        answer = ans.SelectedValue
                    };
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/question/is-correct", postQuestionRequest);
                    if (response.IsSuccessStatusCode)
                    {
                        bool iscrt = bool.Parse(response.Content.ReadAsStringAsync().Result);
                        if (iscrt) score++;
                    }

                }

                //bool iscrt = questionServiceClient.IsCorrect(q.QuestionVal, ans.SelectedValue);
                //if (iscrt) score++;
            }
            string script = "alert('Score: " + score.ToString() + "');";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "script", script, true);

            script = "alert('You will be redirected, your score will be sent to your teacher.');window.location='StudentHome'";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "script2", script, true);

        }
    }
}