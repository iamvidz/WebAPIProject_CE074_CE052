<%@ Page Title="Contact" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Online_Examination_Client.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="well">
        <h2 class="display-2">Developers</h2>
    </div>

    <div class="row">
        <div class="col-sm-4">
            <div class="card">
                <div class="card-body">
                    <ul class="list-group list-group-flush">
                        <li class="card-title"><i class="fas fa-user fa-3x"></i>&nbsp;&nbsp; Vidhi Maheriya</li>
                        <li class="list-group-item"><b>Email: </b>vim7127@gmail.com</li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="card">
                <div class="card-body">
                    <ul class="list-group list-group-flush">
                        <li class="card-title"><i class="fas fa-user fa-3x"></i>&nbsp;&nbsp; Harsh Joshi</li>
                        <li class="list-group-item"><b>Email: </b>harshvkjoshi@gmail.com</li>
                    </ul>
                </div>
            </div>
        </div>
        
    </div>
    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
    </address>
</asp:Content>

<%--c#
type="Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"--%>