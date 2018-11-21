<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="Dsc.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.Dsc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <h1>GSTN
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>GSTN</a></li>
            <li class="active">DSC</li>
        </ol>
    </section>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Digital Signature Certificate(DSC) Registration</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-sm-3">
                        <div class="input-group">
                            <asp:TextBox ID="txtpan" placeholder="PAN No." CssClass="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" />

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 pull-left"></div>
                </div>
            </div>
        </div>
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Search TaxPayer</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-sm-3">
                        <div class="input-group">
                            <asp:TextBox ID="txtSearch" placeholder="Taxpayer" CssClass="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" />
                            </div>
                        </div>
                        <div class="col-sm-3 pull-left"></div>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <asp:ListView ID="lv" runat="server">
                <EmptyDataTemplate>
                    <table class="table">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Container.DataItemIndex + 1%>.</td>
                        <td><%# Eval("") %></td>
                        <td><%# Eval("") %></td>
                        <td><%# Eval("") %></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table class="table table-responsive dataTable">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th></th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr id="itemPlaceholder" runat="server" />
                        </tbody>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
