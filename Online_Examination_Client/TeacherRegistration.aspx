<%@ Page Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="TeacherRegistration.aspx.cs" Inherits="Online_Examination_Client.TeacherRegistration" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ValidationSummary ID="ValidationSummaryForRegister" runat="server" ForeColor="Red" HeaderText="Following errors occured while logging in." ValidationGroup="Register" />
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

                            <p class="login-card-description">Registration for Teachers </p>

                            <div class="form-group">
                                <label for="exampleInputEmail1" class="sr-only">Email address:</label>
                                <asp:TextBox ID="tbusername" placeholder="Email address" runat="server" CssClass="form-control" aria-describedby="emailHelp" ValidationGroup="Register"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label for="tbName" class="sr-only">Name: </label>
                                <asp:TextBox ID="tbName" runat="server" CssClass="form-control" placeholder="Name" aria-describedby="Name" ValidationGroup="Register"></asp:TextBox>
                            </div>
                            <div class="from-group">
                                <label for="exampleInputPassword1" class="sr-only">Password:</label>
                                <asp:TextBox ID="tbpasswd" CssClass="form-control" placeholder="Password" runat="server" TextMode="Password" aria-describedby="password"></asp:TextBox>
                            </div>
                            <div class="from-group">
                                <label for="exampleInputPassword2" class="sr-only">Confirm Password:</label>
                                <asp:TextBox ID="tbConfirmpasswd" CssClass="form-control" placeholder="Confirm Password" runat="server" TextMode="Password" ValidationGroup="Register"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="Role" class="form-label">Role:</label>
                                <asp:TextBox ID="tbRole" CssClass="form-control" runat="server" Enabled="false" Text="Teacher"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-block login-btn mb-4" OnClick="btnRegister_Click" />&nbsp;&nbsp;<a href="Default.aspx" class="login-card-footer-text" runat="server">Log In</a>
                                <br />
                                <asp:Label ID="lblRegister" runat="server"></asp:Label>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorForUsername" ControlToValidate="tbusername" runat="server" ErrorMessage="Username is required" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorForName" ControlToValidate="tbName" runat="server" ErrorMessage="Name is required" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorForPasswd" ControlToValidate="tbpasswd" runat="server" ErrorMessage="Password is Required." ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorForConfirmPasswd" runat="server" ErrorMessage="Confirm Password is required." ControlToValidate="tbConfirmpasswd" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:CompareValidator ID="CompareValidatorForPasswdAndConfirmPasswd" runat="server" ErrorMessage="Password and Confirm password should match." ControlToCompare="tbpasswd" ControlToValidate="tbConfirmpasswd" ForeColor="Red"></asp:CompareValidator><br />

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
