<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="GSTR1A.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.GSTR1A" %>

<%@ MasterType VirtualPath="~/User/User.master" %>

<%@ Register Src="~/UC/UC_Invoice/uc_InvoiceView.ascx" TagPrefix="uc1" TagName="uc_InvoiceView" %>
<%@ Register Src="~/UC/UC_Invoice/uc_invoiceMonth.ascx" TagPrefix="uc1" TagName="uc_invoiceMonth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content-header">
        <h1>GSTR1A
        <small></small></h1> 
        <ol class="breadcrumb"> 
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">Gstr1A</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-solid">
            <div class="box-body">
                <div class="row">
                    <div class="col-md-4">
                        <asp:LinkButton ID="lkcImportConsolidated" CssClass="btn btn-primary" OnClick="lkcImportConsolidated_Click" runat="server"><i class="fa fa-cloud-download"></i> Import(s) invoices</asp:LinkButton>
                        Invoice(s) for Import :
                        <asp:Literal ID="litItemToImport" runat="server"></asp:Literal>
                    </div>
                    <div class="form-group col-md-8">
                        <label class="col-sm-2 control-label" style="top:6px;">MONTH:</label>&nbsp;&nbsp;
                        <div class="col-md-3" >
                            <uc1:uc_invoiceMonth runat="server" ID="uc_invoiceMonth" />
                            </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#Change" data-toggle="tab">Changed Invoices</a></li>
                        <li><a href="#Accept" data-toggle="tab">Accepted Invoices</a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-content">
                            <div class="tab-pane active" id="Change">
                                <div class="box-body ">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:ListView ID="lvInvoices" DataKeyNames="InvoiceID,AuditTrailID,InvoiceAction" OnPagePropertiesChanging="lvInvoices_PagePropertiesChanging" runat="server">
                                                    <EmptyDataTemplate>
                                                        <table class="table">
                                                            <tr>
                                                                <td>No data was returned.</td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%--<%# Container.DataItemIndex + 1%>.--%>

                                                                <asp:CheckBox ID="chkSelect" Visible='<%#IsActionInvoice(DataBinder.Eval(Container.DataItem,"InvoiceID").ToString()) %>' ClientIDMode="Static" runat="server"></asp:CheckBox>
                                                            </td>
                                                            <td><%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceNo")%></td>
                                                            <td>
                                                                <%#Common.InvoiceAuditTrailAction(DataBinder.Eval(Container.DataItem,"ReceiverInvoiceAction")) %>
                                                            </td>

                                                            <td><%#Common.InvoiceTypeColor(DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceSpecialCondition").ToString(),DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceType").ToString())%></td>
                                                            <td><%#DateTimeAgo.GetFormatDate(DataBinder.Eval(Container.DataItem, "GST_TRN_INVOICE.InvoiceDate").ToString())%></td>
                                                            <td>
                                                                <asp:LinkButton ID="lkbView" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINVA"%>' ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
                                                                &nbsp; 
                                                     <asp:LinkButton ID="lkbEdit" runat="server" OnClick="lkb_Click" Visible="false" CommandArgument='<%# Eval("InvoiceID") %>' ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                                                                <div class="modal fade" id='<%# Container.DataItemIndex +"_myModalINVA"%>' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
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
                                                                    <th>
                                                                        <asp:CheckBox ID="cbSelectAll" Visible="false" ClientIDMode="Static" runat="server"></asp:CheckBox></th>
                                                                    <th>Invoice</th>
                                                                    <%--  <th>Status</th>--%>
                                                                    <th>Receiver Action</th>
                                                                    <th>Invoice Type</th>
                                                                    <th>Invoice Date</th>
                                                                    <%--   <th>Inv. Sub-Type</th>--%>
                                                                    <th>#</th>
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
                                            <div class="box-footer clearfix">
                                                <asp:LinkButton ID="lkbImport" CommandName="Import" Visible="false" CssClass=" btn btn-primary" runat="server" OnClick="lkbImport_Click"><i class="fa fa-cloud-download"></i> Import - 1A</asp:LinkButton>
                                                <div class="pagination pagination-sm no-margin pull-right">
                                                    <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
                                                    <asp:DataPager ID="dpInvoice" runat="server" PagedControlID="lvInvoices" PageSize="10" class="btn-group-sm pager-buttons ">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                                            <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                                                            <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </div>

                                                <asp:LinkButton ID="lkbAccept" Visible="true" CommandName="Accept" CssClass=" btn btn-success" runat="server" OnClick="lkb_Click"><i class="fa fa-thumbs-up"></i> Accept</asp:LinkButton>
                                                <asp:LinkButton ID="lkbDelete" Visible="false" CommandName="Delete" CssClass=" btn btn-danger" runat="server" OnClick="lkb_Click"><i class="fa fa-trash"></i>  Delete</asp:LinkButton>
                                                <asp:LinkButton ID="lkbReject" Visible="true" CommandName="Reject" CssClass=" btn btn-warning" runat="server" OnClick="lkb_Click"><i class="fa fa-ban"></i> Reject</asp:LinkButton>
                                                <asp:LinkButton ID="lkbPending" Visible="false" CommandName="Pending" CssClass=" btn btn-info" runat="server" OnClick="lkb_Click"><i class="fa fa-exclamation-circle"></i> Pending</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane" id="Accept">
                                <div class="box-body ">
                                    <div class="row">
                                        <div class="col-md-12">

                                            <asp:ListView ID="lvAccept" DataKeyNames="InvoiceID,AuditTrailID,InvoiceAction" runat="server">
                                                <EmptyDataTemplate>
                                                    <table class="table">
                                                        <tr>
                                                            <td>No data was returned.</td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.DataItemIndex + 1%>.

                                                           <%-- <asp:CheckBox ID="chkSelect" ClientIDMode="Static" runat="server"></asp:CheckBox>--%>
                                                        </td>
                                                        <td><%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceNo")%></td>


                                                        <td>
                                                            <%#Common.InvoiceAuditTrailAction(DataBinder.Eval(Container.DataItem,"ReceiverInvoiceAction")) %>

                                                        </td>

                                                        <td><%#Common.InvoiceTypeColor(DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceSpecialCondition").ToString(),DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceType").ToString())%></td>
                                                        <td><%#DateTimeAgo.GetFormatDate(DataBinder.Eval(Container.DataItem, "GST_TRN_INVOICE.InvoiceDate").ToString())%></td>
                                                        <td>
                                                            <asp:LinkButton ID="lkbView" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
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
                                                    <table class="table table-responsive dataTable" id="lvItems">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Invoice</th>
                                                                <th>Receiver Action</th>
                                                                <th>Invoice Type</th>
                                                                <th>Invoice Date</th>
                                                                <th>#</th>
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
                                    </div>
                                </div>
                                <%--     <div class="box-footer clearfix">
                                    <div class="pagination pagination-sm no-margin pull-right">
                                        <asp:DataPager ID="dpAccept" runat="server" PagedControlID="lvAccept" PageSize="10" class="btn-group-sm pager-buttons ">
                                            <Fields>
                                                <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                                <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                                                <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                            </Fields>
                                        </asp:DataPager>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
