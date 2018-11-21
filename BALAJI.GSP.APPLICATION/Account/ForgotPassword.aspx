<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Maintheme.master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main1Content" runat="server">
    <div class="content">
        <div id="content" class="site-content">
            <div id="main" class="clearfix no-sidebar">
                <div class="tg-container">
                    <div id="primary" style="margin-left:32%">
                        <div class="login-box">
                            <div class="login-logo" style="margin-left:5%">
                                <h1>Forget Password</h1>
                            </div>
                            <!-- /.login-logo -->
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="login-box-body">
                                        <p class="login-box-msg" style="margin-left:5%">To reset your password, please enter the Email you used to signd in GST-C panel</p>
                                        <div class="form-group">
                                             <asp:Label runat="server" ID="Label1" Text="Enter your Email Address" />
                                             <asp:TextBox class="form-control" runat="server" placeholder="Enter Email" ID="TextBox1" ></asp:TextBox>
                                            
                                             </div>
                                        <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-offset-8 col-md-4">
                             <asp:Button class="btn btn-primary btn-block btn-success pull-right" runat="server"   Text="Submit" />
                                                </div>
                                            </div>
                                            </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="OTP1" Text="Enter OTP" />
                                            <asp:TextBox class="form-control" runat="server" placeholder="Enter OTP" ID="OTP" ></asp:TextBox>
                                        </div>
                                           <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-offset-8 col-md-4">
                             <asp:Button class="btn btn-primary btn-block btn-success pull-right" runat="server"   Text="Submit" />
                                                </div>
                                            </div>
                                            </div>
                                    
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="FailureText" Text="Enter New Password" />
                                            <asp:TextBox class="form-control" runat="server" placeholder="New Password" ID="TextBox3" ></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="Label2" Text="Re-Enter Password" />
                                            <asp:TextBox class="form-control" runat="server" placeholder="Confirm Password" ID="TextBox2" ></asp:TextBox>
                                             </div>
                                           <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-offset-8 col-md-4">
                             <asp:Button class="btn btn-primary btn-block btn-success pull-right" runat="server"   Text="Submit" />
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
