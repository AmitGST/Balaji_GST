<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Testing.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.Testing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>Vendor Registration
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">Vendor Registration</li>
        </ol>
    </div>
    <div class="content">


        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Add Vendor</h3>
            </div>
            <form class="form-horizontal">
                <div class="box-body">
                    <div class="form-group">
                        <label for="entity" class="col-sm-3">Entity Name</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                            <asp:TextBox ID="txtEntity" CssClass="form-control" placeholder="Entity Name" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEnt" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ControlToValidate="txtEntity"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="address" class="col-sm-3">Address</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtadd" CssClass="form-control" placeholder="Address" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfadd" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ControlToValidate="txtadd"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3"></div>
                    <div class="col-sm-9">
                        <asp:Button ID="btnReg" CssClass="btn btn-primary" runat="server" Text="Submit" />
                    </div>
                </div>
            </form>
        </div>
    </div>


</asp:Content>


