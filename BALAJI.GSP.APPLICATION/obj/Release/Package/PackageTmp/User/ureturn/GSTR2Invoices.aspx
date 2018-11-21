<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="GSTR2Invoices.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.GSTR2Invoices" %>

<%@ MasterType VirtualPath="~/User/User.master" %>
<%@ PreviousPageType VirtualPath="~/User/ureturn/GSTR2.aspx" %>
<%@ Register Src="~/UC/UC_Invoice/uc_InvoiceView.ascx" TagPrefix="uc1" TagName="uc_InvoiceView" %>
<%@ Register Src="~/UC/UC_Invoice/uc_InvoiceEdit.ascx" TagPrefix="uc1" TagName="uc_InvoiceEdit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>GSTR2</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">GSTR2</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">View Details</h3>
            </div>
            <div class="box-body">
                <asp:ListView ID="lvRegularInvoice" runat="server" DataKeyNames="InvoiceID,AuditTrailID">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkSelect" Visible='<%#DataBinder.Eval(Container.DataItem,"ReceiverInvoiceAction")==null?true: DataBinder.Eval(Container.DataItem,"ReceiverInvoiceAction").ToString()=="0"?true:false %>' runat="server" /></td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceNo")%>
                            </td>
                            <td><%#DateTimeAgo.GetFormatDate(DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.Invoicedate").ToString())%></td>
                            <%-- <td><%#Common.InvoiceTypeColor(Eval("InvoiceSpecialCondition").ToString(),Eval("InvoiceType").ToString())%></td>--%>
                            <td>
                                <asp:LinkButton ID="lkbView" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
                                &nbsp;
                      <%--  <asp:LinkButton ID="lkbEdit" runat="server" OnClick="lkbEditInvoice_Click" Visible='<%#((DataBinder.Eval(Container.DataItem,"AuditTrailStatus").ToString()!="1"))?true:false %>' CommandArgument='<%# Eval("InvoiceID") %>' ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>--%>
                                <asp:LinkButton ID="lkbEdit" runat="server" OnClick="lkbEditInvoice_Click" Visible='<%#DataBinder.Eval(Container.DataItem,"ReceiverInvoiceAction")==null?true: DataBinder.Eval(Container.DataItem,"ReceiverInvoiceAction").ToString()=="0"?true:false %>' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceID")  %>' ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                                <div class="modal fade" id='<%# Container.DataItemIndex +"_myModalINV"%>' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                    <div class="modal-dialog" style="width: 90%;">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                    &times;
                                                </button>
                                                <h4 class="modal-title"><b><i class="fa fa-globe"></i>
                                                    <asp:Label ID="lblModalTitle" runat="server" Text="Invoice Product Details"></asp:Label>
                                                </b></h4>
                                            </div>
                                            <div class="modal-body">
                                                <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>' />
                                            </div>
                                            <div class="modal-footer">
                                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">
                                                    Close
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive">
                            <thead>
                                <tr>
                                    <th style="width: 10px">#</th>
                                    <th>Invoice</th>
                                    <th>Invoice Date</th>
                                    <%--   <th>Inv. Sub-Type</th>--%>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
            <div class="box-footer">
                <%--<asp:LinkButton ID="lkbImport" Visible="false" CommandName="Import" CssClass=" btn btn-primary" OnClick="lkvGSTR2A_Click" runat="server"><i class="fa fa-cloud-download"></i> Import - 2A</asp:LinkButton>
                --%>
                <asp:LinkButton ID="lkbAccept" CommandName="Accept" CssClass=" btn btn-success" runat="server" OnClick="lkb_Click"><i class="fa fa-thumbs-up"></i> Accept</asp:LinkButton>
                <asp:LinkButton ID="lkbDelete" Visible="false" CommandName="Delete" CssClass=" btn btn-danger" runat="server" OnClick="lkb_Click"><i class="fa fa-trash"></i>  Delete</asp:LinkButton>
                <asp:LinkButton ID="lkbReject" CommandName="Reject" CssClass=" btn btn-warning" runat="server" OnClick="lkb_Click"><i class="fa fa-ban"></i> Reject</asp:LinkButton>
                <asp:LinkButton ID="lkbPending" CommandName="Pending" CssClass=" btn btn-info" runat="server" OnClick="lkb_Click"><i class="fa fa-exclamation-circle"></i> Pending</asp:LinkButton>
                <uc1:uc_InvoiceEdit runat="server" ID="uc_InvoiceEdit" />
            </div>
        </div>
    </div>
</asp:Content>
