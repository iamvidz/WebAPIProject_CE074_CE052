<%@ Page Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exam.aspx.cs" Inherits="Online_Examination_Client.Exam" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
        <div class="p-3  my-lg-5 bg-body rounded">
            <h3 class="my-5">Exam<%=Request.QueryString["id"]%></h3>
            <div id="ques" class="text-left" style="margin-left:-5500px !important" runat="server"></div>
        </div>
    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Submit_Exam"/>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</asp:Content>