<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="GSTR2.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.GSTR2" %>

<%@ MasterType VirtualPath="~/User/User.master" %>

<%@ Register Src="~/UC/UC_Invoice/uc_InvoiceView.ascx" TagPrefix="uc1" TagName="uc_InvoiceView" %>
<%@ Register Src="~/UC/UC_Invoice/uc_InvoiceEdit.ascx" TagPrefix="uc1" TagName="uc_InvoiceEdit" %>
<%@ Register Src="~/UC/UC_Invoice/uc_invoiceMonth.ascx" TagPrefix="uc1" TagName="uc_invoiceMonth" %>


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
    <div class="content" id="divMain" runat="server" visible="false">
        <div class="box box-solid">
            <div class="box-body">
                <div class="row">
                    <div class="col-md-3">
                        <asp:LinkButton ID="lkcImportConsolidated" OnClick="lkcImportConsolidated_Click" CssClass="btn btn-primary" runat="server"><i class="fa fa-download"></i> Purchase Re-Conciliation</asp:LinkButton>
                    </div>
                    <div class="col-md-offset-7"></div>
                   
                            <div class="col-md-2 pull-right">
                              <%--   <label>MONTH:</label>--%>
                                    <uc1:uc_invoiceMonth runat="server" ID="uc_invoiceMonth" />
                                     <%-- <uc1:uc_invoiceMonth runat="server"  ID="uc_invoiceMonth2" />--%>
                            </div>
                        
                    </div>
                
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#Mismatch" data-toggle="tab">Mis-Matched Invoice</a></li>
                        <li><a href="#Match" data-toggle="tab">Matched Invoice<%--<span style="position:absolute;" class="label label-warning">10</span>--%></a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="Mismatch">
                            <div class="box-body ">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Repeater ID="rptMisMatch" runat="server">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="col-md-4 no-padding" style="padding: 5px !important;">
                                                    <div class='<%#BusinessLogic.Repositories.cls_Invoice.InvoiceColor(Eval("INVSPLCONDITION")) %>'>
                                                        <div class="inner">
                                                            <h4><%#((GST.Utility.EnumConstants.InvoiceSpecialCondition)Convert.ToInt32(Eval("INVSPLCONDITION").ToString())).ToDescription() %>(<%#Eval("TotalInvoice") %>)</h4>
                                                            <p>
                                                                Quantity : <%#Eval("QTY") %>
                                                            </p>
                                                            <p>
                                                                Amount : Rs. <%#Eval("TAXABLEAMOUNT") %>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="fa fa-book"></i>
                                                        </div>
                                                        <asp:LinkButton ID="lbinfo" CssClass="small-box-footer" OnClick="lbinfo_Click" CommandName='<%# Eval("INVSPLCONDITION")%>' runat="server">View Detail <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:Repeater ID="rptMisMatchAdvance" runat="server">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class='<%#BusinessLogic.Repositories.cls_Invoice.InvoiceColor(Eval("INVSPLCONDITION")) %>'>
                                                    <div class="inner">
                                                        <h4><%#((GST.Utility.EnumConstants.InvoiceSpecialCondition)Convert.ToInt32(Eval("INVSPLCONDITION").ToString())).ToDescription() %>( <%#Eval("TotalInvoice") %>)</h4>
                                                        <p>
                                                            Quantity : <%#Eval("QTY") %>
                                                        </p>
                                                        <p>
                                                            Amount : Rs. <%#Eval("TAXABLEAMOUNT") %>
                                                        </p>
                                                    </div>
                                                    <div class="icon">
                                                        <i class="fa fa-book"></i>
                                                    </div>
                                                    <asp:LinkButton ID="lbinfo" CssClass="small-box-footer" OnClick="lbinfo_Click" CommandName='<%# Eval("INVSPLCONDITION")%>' runat="server">View Detail <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="tab-pane" id="Match">
                            <asp:ListView ID="lvMatchInvoice" DataKeyNames="InvoiceID" runat="server">
                                <EmptyDataTemplate>
                                    <table class="table">
                                        <tr>
                                            <td>No data was returned.</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <%--  <td></td>--%>
                                        <td><%# Container.DataItemIndex + 1%>.</td>
                                        <td><%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceNo")%></td>
                                        <%-- <td>
                                              <%#Common.InvoiceAuditTrailSatus(DataBinder.Eval(Container.DataItem,"AuditTrailStatus").ToString()) %></td>
                                      <td>
                                            <%#Common.InvoiceAuditTrailAction(DataBinder.Eval(Container.DataItem,"InvoiceAction").ToString()) %></td>  --%>
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
                                                <%-- <th>Status</th>--%>
                                                <th>Invoice Type</th>
                                                <%--  <th>Invoice Type</th>--%>
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
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-primary" style="display: none">
            <div class="box-header with-border">
                <h3 class="box-title">Details of All Invoice</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lvInvoices" DataKeyNames="InvoiceID,AuditTrailID" OnPagePropertiesChanging="lvItems_PagePropertiesChanging" runat="server">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <%-- <td>
                                
                            </td>--%>
                            <td><%# Container.DataItemIndex + 1%>.</td>
                            <td><%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceNo")%></td>
                            <td><%--<asp:CheckBox ID="chkImport" Visible='<%#DataBinder.Eval(Container.DataItem,"AuditTrailStatus").ToString()=="1"?true:false %>' runat="server" />--%><%#Common.InvoiceAuditTrailSatus(DataBinder.Eval(Container.DataItem, "AuditTrailStatus").ToString())%></td>
                            <td><%-- <asp:CheckBox ID="chkSelect" Visible='<%#DataBinder.Eval(Container.DataItem,"InvoiceAction").ToString()=="0"?true:false %>' runat="server" Enabled='<%#DataBinder.Eval(Container.DataItem,"AuditTrailStatus").ToString()=="1"?false:true %>' />--%><%#Common.InvoiceAuditTrailAction(DataBinder.Eval(Container.DataItem,"InvoiceAction").ToString()) %></td>
                            <td><%#Common.InvoiceTypeColor(DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceSpecialCondition").ToString(),DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceType").ToString())%></td>
                            <td><%#DateTimeAgo.GetFormatDate(DataBinder.Eval(Container.DataItem, "GST_TRN_INVOICE.InvoiceDate").ToString())%></td>
                            <td><%--  <asp:LinkButton ID="lkbView" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>--%>&nbsp; 
                                <%-- <asp:LinkButton ID="lkbEdit" runat="server" Visible='<%#((DataBinder.Eval(Container.DataItem,"InvoiceAction").ToString()=="0" && DataBinder.Eval(Container.DataItem,"AuditTrailStatus").ToString()!="1") || (DataBinder.Eval(Container.DataItem,"InvoiceAction").ToString()=="5"))?true:false %>' CommandArgument='<%# Eval("InvoiceID") %>' ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>--%>
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
                                                <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice1" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>' />
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
                                    <th>Status</th>
                                    <th>Action</th>
                                    <th>Invoice Type</th>
                                    <th>Invoice Date</th>
                                    <th>Inv. Sub-Type</th>
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
                <%--<asp:LinkButton ID="lkbImport" Visible="false" CommandName="Import" CssClass=" btn btn-primary" OnClick="lkvGSTR2A_Click" runat="server"><i class="fa fa-cloud-download"></i> Import - 2A</asp:LinkButton>
                <asp:LinkButton ID="lkbAccept" CommandName="Accept" CssClass=" btn btn-success" runat="server" OnClick="lkb_Click"><i class="fa fa-thumbs-up"></i> Accept</asp:LinkButton>
                <asp:LinkButton ID="lkbDelete" Visible="false" CommandName="Delete" CssClass=" btn btn-danger" runat="server" OnClick="lkb_Click"><i class="fa fa-trash"></i>  Delete</asp:LinkButton>
                <asp:LinkButton ID="lkbReject" CommandName="Reject" CssClass=" btn btn-warning" runat="server" OnClick="lkb_Click"><i class="fa fa-ban"></i> Reject</asp:LinkButton>
                <asp:LinkButton ID="lkbPending" CommandName="Pending" CssClass=" btn btn-info" runat="server" OnClick="lkb_Click"><i class="fa fa-exclamation-circle"></i> Pending</asp:LinkButton>--%>
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
                <%--         <uc1:uc_InvoiceEdit runat="server" ID="uc_InvoiceEdit" />--%>
            </div>
        </div>
    </div>
    <div class="content" id="divViewInvoiceList" runat="server" visible="false">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">View Details</h3>
                <div class="form-inline">
                    <div class="row">
                        <div class="col-md-2">
                            <label>
                            MONTH:</label>
                               <div class="pull-right">
                                   <%=uc_invoiceMonth.GetTextValue%>
                                  <%-- <uc1:uc_invoiceMonth runat="server"  ID="uc_invoiceMonth2" />--%>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:LinkButton ID="lkbBack" CssClass="btn btn-default pull-right" visible="false" OnClick="lkbBack_Click" runat="server">&nbsp;<i class="fa fa-backward"></i> Back</asp:LinkButton>
            </div>
            <div class="box-body">
                <%--  Add listbox--%>

                <asp:ListView ID="lvRegularInvoice" runat="server" OnPagePropertiesChanging="lvRegularInvoice_PagePropertiesChanging" DataKeyNames="InvoiceID,AuditTrailID" >
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%--<asp:CheckBox ID="CheckBox1" Visible='<%#IsActionInvoice(DataBinder.Eval(Container.DataItem,"InvoiceID").ToString()) %>' runat="server" /></td>
                        <%#DataBinder.Eval(Container.DataItem,"ReceiverInvoiceAction").ToString()=="0"?true:false %>--%>
                                <asp:CheckBox ID="chkSelect" Visible='<%#DataBinder.Eval(Container.DataItem,"ReceiverInvoiceAction")!=null?(DataBinder.Eval(Container.DataItem,"ReceiverInvoiceAction").ToString()=="0"?true:false):false %>' runat="server" />
                            </td>
                            <td><%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceNo")%></td>
                            <td><%#Common.InvoiceAuditTrailAction(DataBinder.Eval(Container.DataItem,"ReceiverInvoiceAction"))%></td>
                            <td><%#DateTimeAgo.GetFormatDate(DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.Invoicedate").ToString())%></td>
                            <%-- <td><%#Common.InvoiceTypeColor(Eval("InvoiceSpecialCondition").ToString(),Eval("InvoiceType").ToString())%></td>--%>
                            <td>
                                <asp:LinkButton ID="lkbView" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
                                &nbsp;
                                      <%--  <asp:LinkButton ID="lkbEdit" runat="server" OnClick="lkbEditInvoice_Click" Visible='<%#((DataBinder.Eval(Container.DataItem,"AuditTrailStatus").ToString()!="1"))?true:false %>' CommandArgument='<%# Eval("InvoiceID") %>' ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>--%>
                                <asp:LinkButton ID="lkbEdit" runat="server" OnClick="lkbEditInvoice_Click" Visible='<%#DataBinder.Eval(Container.DataItem,"ReceiverInvoiceAction")!=null?(DataBinder.Eval(Container.DataItem,"ReceiverInvoiceAction").ToString()=="0"?true:false):false %>' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceID")  %>' ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
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
                                    <th>Action By You</th>
                                    <th>Invoice Date</th>
                                    <%--  --%>
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
                <div class="box-footer">
                    <asp:LinkButton ID="lkbImport" Visible="false" CommandName="Import" CssClass=" btn btn-primary" OnClick="lkvGSTR2A_Click" runat="server"><i class="fa fa-cloud-download"></i> Import - 2A</asp:LinkButton>
                    <asp:LinkButton ID="lkbAccept" CommandName="Accept" CssClass=" btn btn-success" runat="server" OnClick="lkb_Click"><i class="fa fa-thumbs-up"></i> Accept</asp:LinkButton>
                    <asp:LinkButton ID="lkbDelete" Visible="false" CommandName="Delete" CssClass=" btn btn-danger" runat="server" OnClick="lkb_Click"><i class="fa fa-trash"></i>  Delete</asp:LinkButton>
                    <asp:LinkButton ID="lkbReject" CommandName="Reject" CssClass=" btn btn-warning" runat="server" OnClick="lkb_Click"><i class="fa fa-ban"></i> Reject</asp:LinkButton>
                    <asp:LinkButton ID="lkbPending" CommandName="Pending" CssClass=" btn btn-info" runat="server" OnClick="lkb_Click"><i class="fa fa-exclamation-circle"></i> Pending</asp:LinkButton>
                    <asp:LinkButton ID="lkbAddInvoice" CommandName="AddInvoice" CssClass=" btn btn-success" runat="server" OnClick="lkbAddInvoice_Click"><i class="fa fa-plus"></i> Add Invoice</asp:LinkButton>
                    <br><br>
                    <asp:LinkButton ID="lkbBottom" CssClass="btn btn-danger" OnClick="lkbBack_Click" runat="server">&nbsp;<i class="fa fa-backward"></i> Back</asp:LinkButton>
                 <div class="pagination pagination-sm no-margin pull-right">
                                <asp:DataPager ID="Dpmissinvoice" runat="server" PagedControlID="lvRegularInvoice" PageSize="10" class="btn-group-sm pager-buttons">
                                    <Fields>
                                        <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                        <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                                        <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                     </div>
                
               
                <uc1:uc_InvoiceEdit runat="server" ID="uc_InvoiceEdit" />
                <%--End--%>
            </div>
        </div>
    </div>
</asp:Content>
