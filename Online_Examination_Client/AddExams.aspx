<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddExams.aspx.cs" Inherits="Online_Examination_Client.AddExams" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="p-3 my-lg-5 bg-body rounded">
        <h1 class="display-3">Create Test</h1><br />
        <h3 > Deadline: <br /><br /><asp:TextBox ID="TextBox1" runat="server" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox></h3><br />
        <h3> Number Of Questions: <br /><br /><asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox> </h3><br /><br />
        <asp:Button ID="Button1" runat="server" CssClass="btn btn-success btn-block" Text="Add Questions" OnClick="btn_AddQuestion" />
        <br /><br /><br /><br />
        <asp:Label ID="Label1" runat="server" CssClass="alert alert-danger" Text="" Visible="false"></asp:Label>
    </div>        
</asp:Content>
