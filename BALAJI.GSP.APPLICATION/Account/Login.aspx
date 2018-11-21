<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Maintheme.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.Login" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main1Content" runat="server">
    <div class="content">
        <div id="content" class="site-content">
            <div id="main" class="clearfix no-sidebar">
                <div class="tg-container">
                    <div id="primary" style="margin-left:32%">
                        <div class="login-box">
                            <div class="login-logo" style="margin-left:14%">
                                <h1><b>GST </b><small>c-Panel</small></h1>
                            </div>
                            <!-- /.login-logo -->
                       <div class="row">
                                <div class="col-md-6">
                                    <div class="login-box-body">
                                        <p class="login-box-msg" style="margin-left:25%">Sign in to start your session</p>
                                        <p class="text-red">
                                            <asp:Literal runat="server" ID="FailureText" />
                                        </p>
                                        <div class="form-group">
                                            <%--<span class="group-addon"><i class="fa fa-envelope icon"></i></span>--%>
                                            <asp:TextBox class="form-control" runat="server" placeholder="User name" ID="UserName" ></asp:TextBox>
                                            <%--<span class="input-group-addon"><i class="fa fa-envelope-o"></i></span>--%>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" CssClass="help-block" Display="Dynamic" ErrorMessage="The user name field is required." />
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox class="form-control" runat="server" placeholder="Password" ID="Password" TextMode="Password" />
                                            <%--<span class="glyphicon glyphicon-lock form-control-feedback"></span>--%>
                                            <%--CssClass="field-validation-error"--%>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="help-block" ErrorMessage="The password field is required." Display="Dynamic" />
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-8">
                                                <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                    CaptchaHeight="60" CaptchaWidth="200" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                                    FontColor="#D20B0C" NoiseColor="#B1B1B1" />
                                            </div>
                                            <div class="col-md-4">
                                                <asp:ImageButton ImageUrl="../dist/img/refresh.png" Width="40px" Height="40px" runat="server" CausesValidation="false" />
                                            </div>
                                            <asp:TextBox ID="txtcaptcha" placeholder="Enter Captcha" Style="text-transform: uppercase;" onpaste="return false;" autocomplete="off" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:CustomValidator ID="ValidateCaptcha" ErrorMessage="Invalid. Please try again." CssClass="help-block" runat="server" OnServerValidate="ValidateCaptcha_ServerValidate"></asp:CustomValidator>
                                     <div class="form-group">
                                        <div class="row" >
                                            <div class="col-sm-6">
                                                <asp:CheckBox ID="RememberMe" runat="server" text=" Remember Me" Font-Bold="true"/>
                                            </div>
                                            <div class="col-sm-6">
                                                <a href="ForgotPassword.aspx" class="pull-right" style="color:black"> <b>Forget Password</b> </a>
                                            </div>
                                        </div>
                                         </div>
                                            <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-offset-8 col-md-4">
                             <asp:Button class="btn btn-primary btn-block btn-success pull-right" runat="server" OnClick="Login_Click" CommandName="Login" Text="Log in" />
                                                </div>
                                          </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
