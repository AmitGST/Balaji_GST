<%@ Page Title="Manage Account" Language="C#" EnableEventValidation="true" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>
<%@ Register Src="~/Account/uc_RoleManager.ascx" TagPrefix="uc" TagName="uc_RoleManager" %>
<%@ Register Src="~/Account/uc_UserAddToRole.ascx" TagPrefix="uc" TagName="uc_UserAddToRole" %>
<%@ Register Src="uc_ChangePassword.ascx" TagName="uc_ChangePassword" TagPrefix="uc1" %>
<%@ Register Src="~/Account/uc_ProfileUpdate.ascx" TagPrefix="uc" TagName="uc_ProfileUpdate" %>
<%@ Register Src="~/Account/uc_CreateUser.ascx" TagPrefix="uc" TagName="uc_CreateUser" %>
<%@ Register Src="~/Account/uc_CreateBuisnessType.ascx" TagPrefix="uc" TagName="uc_CreateBuisnessType" %>


<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">

        <h1>
            <%:Title %>
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">User Manage</li>
        </ol>
    </section>
    <div class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">
                            <asp:LinkButton ID="lkbUserNew" OnClick="lkbOpen_Click" Visible="false"  CommandName="UserNew" CssClass="btn btn-app" runat="server">    
                                <i class="fa fa-user"></i>Create User
                            </asp:LinkButton>
                             
                              <asp:LinkButton ID="lkbAssignRole" OnClick="lkbOpen_Click" Visible="false" CommandName="RoleAssign" CssClass="btn btn-app" runat="server">    
                                <i class="fa fa-play"></i>Assign Role
                            </asp:LinkButton>
                            
                            <asp:LinkButton ID="lkbUsers" OnClick="lkbOpen_Click" Visible="false"  CommandName="Users" CssClass="btn btn-app" runat="server">    
                                <i class="fa fa-users"></i>Users
                            </asp:LinkButton>
                             <asp:LinkButton ID="lkbGroup" OnClick="lkbOpen_Click" Visible="false"  CommandName="GroupNew" CssClass="btn btn-app" runat="server">    
                                <i class="fa fa-object-group"></i>Group
                            </asp:LinkButton>
                            <asp:LinkButton ID="lkbRole" OnClick="lkbOpen_Click" Visible="false"  CommandName="RoleNew" CssClass="btn btn-app" runat="server">    
                                <i class="fa fa-edit"></i>Role
                            </asp:LinkButton>
                             <asp:LinkButton ID="lkbbBuisnessType" Visible="false"  CommandName="BusinessType" OnClick="lkbOpen_Click" CssClass="btn btn-app" runat="server">    
                                <i class="fa fa-briefcase"></i>Create Business Type
                            </asp:LinkButton>
                        </h3>   
                    </div>
                </div>
            </div>
            <!-- /.col -->
        </div>
        <div class="row">
            <div class="col-md-12">
                <!-- general form elements -->
                <asp:PlaceHolder ID="phDynamicControls" runat="server"></asp:PlaceHolder>
                <%-- <uc:uc_RoleManager runat="server" ID="uc_RoleManager" />--%>
            </div>

            <%-- <div class="col-md-6">
                <uc:uc_UserAddToRole runat="server" ID="uc_UserAddToRole" />
            </div>--%>
        </div>
    </div>







</asp:Content>
