<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="MapUserBusiness.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.MapUserBusiness" %>

<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>Map User's Business
            <small></small></h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">User's Business Type</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">User's Business</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>User's List </label>
                            <asp:DropDownList ID="ddlUserList" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    </div>
                <div class="row">
                    <div class="col-md-3">
                    <div class="form-group">
                        <label>Business Type</label>
                        <asp:ListBox ID="lbBuisnessType" Class="form-control" SelectionMode="Multiple" DataTextField="Buisness-type" runat="server" ></asp:ListBox>
                    </div>
                        </div>
                </div>
                </div>
            <div class="box-footer">
              <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click1" />
                <uc1:uc_sucess runat="server" ID="uc_sucess" />
            </div>
            </div>
        </div>
 
</asp:Content>
