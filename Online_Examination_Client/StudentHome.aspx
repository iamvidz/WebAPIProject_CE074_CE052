<%@ Page Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentHome.aspx.cs" Inherits="Online_Examination_Client.StudentHome" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="my-5">
        <asp:Table ID="ListOfExams" runat="server" CssClass="my-5 table">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Test ID</asp:TableCell>
                <asp:TableCell runat="server">Teacher</asp:TableCell>
                <asp:TableCell runat="server">Due Time</asp:TableCell>
                <asp:TableCell runat="server">Take Test</asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
    </div>
</asp:Content>