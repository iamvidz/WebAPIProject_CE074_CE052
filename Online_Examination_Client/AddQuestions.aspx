<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddQuestions.aspx.cs" Inherits="Online_Examination_Client.AddQuestions" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="p-3 mb-5 bg-body rounded">
        <h2>New Test</h2>        
        <div class="container-fluid">
            <div class="row">
                <div class="col-8">
                    <div id="quesContainer" runat="server"></div>
                </div>
            </div>
        </div>
        <div>                       
            
            <asp:Button ID="Button1" runat="server" Text="Create" CssClass="btn btn-success btn-block" OnClick="btn_AddExam" /><br/><br/><br/>
        </div>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>