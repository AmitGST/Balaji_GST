<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="Gstr3B-Preview.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.Gstr3B_Preview" %>

<%@ Register Src="~/UC/UC_Invoice/uc_invoiceMonth.ascx" TagPrefix="uc1" TagName="uc_invoiceMonth" %>
<%@ Register Src="~/UC/Report/uc_ReportViewer.ascx" TagPrefix="uc1" TagName="uc_ReportViewer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content-header">
        <h1>GSTR3B
       <%-- <small>Outward supplies made by the taxpayer</small> --%></h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">GSTR3B</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-default">
            <div class="row">
                <div class="col-md-3">
                    <label>
                        Select Month:</label>
                    <div class="form-group">
                        <uc1:uc_invoiceMonth runat="server" ID="uc_invoiceMonth" />
                    </div>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lkbDownload3B" CssClass="btn btn-primary pull-left" OnClick="lkbDownload3B_Click" CommandArgument='<%# Eval("UserID") %>' runat="server"><i class="fa fa-download">&nbsp;</i>Download 3B</asp:LinkButton>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lkbDownload3B" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="lkbView3B" CssClass="btn btn-warning" OnClick="lkbView3B_Click1" runat="server"><i class="fa fa-eye">&nbsp;</i>View</asp:LinkButton>
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <uc1:uc_ReportViewer runat="server" id="uc_ReportViewer" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
