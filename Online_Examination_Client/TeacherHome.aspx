<%@ Page Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeacherHome.aspx.cs" Inherits="Online_Examination_Client.TeacherHome" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="my-5">        
        <asp:Button ID="Button1" runat="server" Text="Create Test" CssClass="btn btn-danger btn-lg mb-3" OnClick="btn_AddExam" />
        <asp:Table ID="ListOfExams" runat="server" CssClass="table">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Exam ID</asp:TableCell>
                <asp:TableCell runat="server">Teacher</asp:TableCell>
                <asp:TableCell runat="server">Due Time</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    
        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
    </div>
    <div class="my-5">        
        <asp:Button ID="Button2" runat="server" Text="Register Student" CssClass="btn btn-success btn-lg mb-3" OnClick="btn_AddStudent" />
        <asp:Table ID="ListOfStudents" runat="server" CssClass="table">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Student Name</asp:TableCell>
                <asp:TableCell runat="server">Password</asp:TableCell>
                <asp:TableCell runat="server">Email</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    
        <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
    </div>
</asp:Content>