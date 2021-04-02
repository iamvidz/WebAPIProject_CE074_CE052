using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Examination_Client
{
    public partial class AddExams : System.Web.UI.Page
    {
        List<System.Web.UI.HtmlControls.HtmlGenericControl> divs = new List<System.Web.UI.HtmlControls.HtmlGenericControl>();
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void btn_AddQuestion(object sender, EventArgs e)
        {
            if (TextBox1.Text == String.Empty || TextBox2.Text == String.Empty)
            {
                Label1.Visible = true;
                Label1.Text = "All Fields are Mandatory";
                Label1.CssClass = "alert alert-danger";
                return;
            }
            int i = Convert.ToInt32(TextBox2.Text);
            if (i <= 0)
            {
                Label1.Text = "Please Enter A Valid Number";
            }
            Session["noq"] = i;
            Session["date"] = DateTime.Parse(TextBox1.Text);
            Response.Redirect("AddQuestions", false);
        }

    }
}