<%@ Page Title="Home Page" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Default.aspx.cs" Inherits="Online_Examination_Client._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ValidationSummary ID="ValidationSummaryForLogin" runat="server" ForeColor="Red" HeaderText="Following errors occured while logging in." ValidationGroup="Login" />
    <br />

    <main class="d-flex align-items-center min-vh-100 py-3 py-md-0">
        <div class="container">

            <div class="card login-card">
                <div class="row no-gutters">
                    <div class="col-md-5">
                        <img src="Content/login.jpg" alt="login" class="login-card-img" />
                    </div>
                    <div class="col-md-7">
                        <div class="card-body">
                            <div class="brand-wrapper">
                                <img src="Content/logo.svg" alt="logo" class="logo" />
                            </div>
                            <p class="login-card-description">Existing User Log in </p>

                            <div class="form-group">
                                <label for="exampleInputEmail1" class="sr-only">Email address</label>
                                <asp:TextBox ID="tbusername" runat="server" CssClass="form-control" placeholder="Email address" aria-describedby="emailHelp"></asp:TextBox>

                            </div>
                            <div class="from-group mb-4">
                                <label for="exampleInputPassword1" class="sr-only">Password</label>
                                <asp:TextBox ID="tbpasswd" CssClass="form-control" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-block login-btn mb-4" OnClick="btnLogin_Click" />
                                <a href="TeacherRegistration.aspx" class="login-card-footer-text" runat="server">New User? Create Account</a>
                                <br />
                                <asp:Label ID="lblLogin" runat="server"></asp:Label>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorForUsername" ControlToValidate="tbusername" Text="Username is Required" runat="server" ErrorMessage="Username is required" ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorForPasswd" ControlToValidate="tbpasswd" Text="Password is Required" runat="server" ErrorMessage="Password is Required." ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>
                            </div>
                            <nav class="login-card-footer-nav">
                                <a href="#!">Terms of use.</a>
                                <a href="#!">Privacy policy</a>
                            </nav>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>



</asp:Content>
