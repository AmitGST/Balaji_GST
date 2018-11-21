<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="Auditrail.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Offline.Auditrail" %>

<%@ Register Src="~/UC/UC_Invoice/uc_invoiceMonth.ascx" TagPrefix="uc1" TagName="uc_invoiceMonth" %>
<%@ Register Src="~/UC/uc_GSTNUsers.ascx" TagPrefix="uc1" TagName="uc_GSTNUsers" %>




<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>Auditrail 
            <small></small></h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>AuditTrail</a></li>
            <li class="active">Offline</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <div class="row">
                    <div class="col-sm-2">
                        <uc1:uc_invoiceMonth runat="server" ID="uc_invoiceMonth" />
                    </div>
                    <div class="col-sm-6">
                        <asp:LinkButton ID="lkbUploadExcel" Style="margin-left: 3px;" CssClass="btn btn-primary" OnClick="lkbUploadExcel_Click" runat="server"><i class="fa fa-upload">&nbsp;</i>Upload Excel</asp:LinkButton>
                        <asp:LinkButton ID="lkbDownloadJSON" runat="server" CssClass="btn btn-primary" OnClick="lkbDownloadJSON_Click"><i class="fa fa-download">&nbsp;</i>Download JSON</asp:LinkButton>
                        <asp:LinkButton ID="lkbDownload3B" CssClass="btn btn-primary pull-left" OnClick="lkbDownload3B_Click" CommandArgument='<%# Eval("UserID") %>' runat="server"><i class="fa fa-download">&nbsp;</i>Download 3B</asp:LinkButton>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lkbDownloadJSON" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <%--                    </div>
                    <div class="col-sm-2">--%>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lkbDownload3B" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="box-body table-responsive no-padding">
                <asp:ListView ID="lv_AuditTrail" OnItemUpdating="lv_AuditTrail_ItemUpdating"  runat="server" >
                    <EmptyDataTemplate>
                        <table class="table table-responsive">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%>.</td>
                            <td><%#DataBinder.Eval(Container.DataItem,"AspNetUser.GSTNNo")%></td>
                            <td><%# Eval("JobName") %></td>
                            <td><%# Eval("JobType")==null?"-":Common.GetJobType(Eval("JobType").ToString())%></td>
                            <td><%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %></td>
                        
                            <td>
                                <asp:LinkButton CommandArgument='<%# Eval("AuditTrailId") %>' OnClick="lkbEdit_Click" ID="lkbEdit" runat="server"  CssClass="btn btn-primary btn-xs" data-placement="right"><i class="fa fa-pencil-square"></i></asp:LinkButton>

                                <asp:LinkButton ID="lkbDelete" CssClass="btn btn-danger btn-xs" OnClientClick="return confirm('Are You sure want to Delete')"   CommandArgument='<%# Eval("AuditTrailId") %>' CommandName="Update" runat="server"><i class="fa fa-trash-o text-white"></i></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive">
                            <thead>
                                <tr>
                                    <th>S.No</th>
                                    <th>GSTIN No.</th>
                                    <th>Job Name</th>
                                    <th>Job Type</th>
                                    <th>Invoice Date</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server" />
                            </tbody>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
            <div class="box-footer">
                <asp:LinkButton ID="lkbBack" CssClass="btn btn-danger" OnClick="lkbBack_Click" runat="server" ><i class="fa fa-backward"></i>&nbsp;Back</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
