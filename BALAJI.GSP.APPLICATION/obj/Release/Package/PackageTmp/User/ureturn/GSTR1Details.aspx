<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="GSTR1Details.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.GSTR1Details" %>

<%@ Register Src="~/UC/UC_Gstr/uc_Gstr1_Details_Tileview.ascx" TagPrefix="uc1" TagName="uc_Gstr1_Details_Tileview" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Src="~/UC/UC_Invoice/uc_InvoiceView.ascx" TagPrefix="uc1" TagName="uc_InvoiceView" %>
<%@ Register Src="~/UC/UC_Invoice/uc_invoiceMonth.ascx" TagPrefix="uc1" TagName="uc_invoiceMonth" %>
<%@ Register Src="~/UC/UC_FileReturn/uc_FileReturn_Edit.ascx" TagPrefix="uc1" TagName="uc_FileReturn_Edit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>GSTR-1
        <small>Details of outward supplies of goods or services</small> </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="#">Returns</a></li>
            <li class="active">GSTR1</li>
        </ol>
    </div>
    <uc1:uc_invoiceMonth runat="server" ID="uc_invoiceMonth" Visible="false" />
    <!--Main Content-->
    <div class="content">
        <div id="main" runat="server">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-md-3">
                                <label for="">GSTIN:</label>
                                <asp:Label ID="lblGSTIN" runat="server"> </asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3">
                                    <label for="">Legal Name:</label>
                                    <asp:Label ID="lbllegal" runat="server"> </asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3">
                                    <label for="">Trade Name:</label>
                                    <asp:Label ID="lblTrade" runat="server"> </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <div class="col-md-3">
                                <label>Financial Year:</label>
                                <asp:Label ID="lblFinYear" runat="server"> </asp:Label>
                            </div>

                            <div class="form-group">
                                <div class="col-md-3">
                                    <label for="">Return Period:</label>
                                    <asp:Label ID="lblmonth" runat="server"> </asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3">
                                    <label for="">Status:</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3">
                                    <label>Duedate:</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="box box-primary" id="viewdetails" runat="server" visible="false">
                <div class="box-body">

                    <%--ankita/amit start--%>
                    <asp:ListView ID="lvInvoices"  runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txtReciever" CssClass="form-control" disabled runat="server" Text='<%# Eval("RECIVERGSTN") %>'></asp:TextBox>
                                </td>

                                <td style="width: 15%">
                                    <asp:TextBox ID="txtInvoice" CssClass="form-control" disabled runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:TextBox>
                                </td>

                                <td style="width: 10%">
                                    <asp:TextBox ID="txtDate" CssClass="form-control" disabled runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>'></asp:TextBox>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txtTotalValue" CssClass="form-control" disabled runat="server" Text='<%# Eval("TotalValue") %>'></asp:TextBox></td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txtTaxableValue" CssClass="form-control" disabled runat="server" Text='<%# Eval("TotalTaxableValue") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtIGST" CssClass="form-control" disabled runat="server" Text='<%# Eval("IGSTAmt") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtCGST" CssClass="form-control" disabled runat="server" Text='<%# Eval("CGSTAmt") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtSGST" CssClass="form-control" disabled runat="server" Text='<%# Eval("SGSTAmt") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtCess" CssClass="form-control" disabled runat="server" Text='<%# Eval("CESSAmt") %>'></asp:TextBox></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit"  OnClick="lkbEdit_Click" AutoPostback="true" CommandArgument='<%# Eval("InvoiceID")%>' runat="server" CssClass="btn btn-success btn-xs" CommandName="Edit" data-trigger="hover" data-placement="right" title="Edit" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No New</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>
                    <%--End--%>
                    <asp:ListView ID="lv_gstr1_5" Visible="true"  runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txtReciever" CssClass="form-control" disabled runat="server" Text='<%# Eval("RECIVERGSTN") %>'></asp:TextBox>
                                </td>

                                <td style="width: 15%">
                                    <asp:TextBox ID="txtInvoice" CssClass="form-control" disabled runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:TextBox>
                                </td>

                                <td style="width: 10%">
                                    <asp:TextBox ID="txtDate" CssClass="form-control" disabled runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>'></asp:TextBox>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txtTotalValue" CssClass="form-control" disabled runat="server" Text='<%# Eval("TotalValue") %>'></asp:TextBox></td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txtTaxableValue" CssClass="form-control" disabled runat="server" Text='<%# Eval("TotalTaxableValue") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtIGST" CssClass="form-control" disabled runat="server" Text='<%# Eval("IGSTAmt") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtCGST" CssClass="form-control" disabled runat="server" Text='<%# Eval("CGSTAmt") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtSGST" CssClass="form-control" disabled runat="server" Text='<%# Eval("SGSTAmt") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtCess" CssClass="form-control" disabled runat="server" Text='<%# Eval("CESSAmt") %>'></asp:TextBox></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit"  runat="server" CssClass="btn btn-success btn-xs"  data-trigger="hover" data-placement="right" title="Edit" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_9B_CRDR_REG" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%# Eval("TOTALCOUNT") %>
                                </td>

                                <%--  <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>--%>
                                <%-- </td>--%>
                                <td><%# Eval("TOTALINVOICEVALUE") %></td>
                                <td><%# Eval("TOTALTAXABLEVALUE") %></td>
                                <td><%# Eval("IGSTAMT") %></td>
                                <td><%# Eval("CGSTAMT") %></td>
                                <td><%# Eval("SGSTAMT") %></td>
                                <td><%# Eval("CESSAMT") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_crunreg" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%# Eval("TOTALCOUNT") %>
                                </td>

                                <%--<td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>--%>
                                <td><%# Eval("TOTALINVOICEVALUE") %></td>
                                <td><%# Eval("TOTALTAXABLEVALUE") %></td>
                                <td><%# Eval("TOTALTAXLIABILITY") %></td>
                                <td><%# Eval("IGSTAMT") %></td>
                                <td><%# Eval("CESSAMT") %></td>

                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_6AExport" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%# Eval("InvoiceNo") %>
                                </td>

                                <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>
                                <td><%# Eval("TotalValue") %></td>
                                <td><%# Eval("TotalTaxableValue") %></td>
                                <td><%# Eval("IGSTAmt") %></td>

                                <td><%# Eval("CGSTAmt") %></td>
                                <td><%# Eval("SGSTAmt") %></td>
                                <td><%# Eval("CESSAmt") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" CommandArgument='<%# Eval("InvoiceID") %>' ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_nill" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>



                                <%-- <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>--%>
                                <td><%# Eval("TotalCount") %></td>
                                <td><%# Eval("TotalValue") %></td>
                                <td><%# Eval("EXEXAMPTEDAMOUNT") %></td>
                                <td><%# Eval("TOTALNONGSTNAMOUNT") %></td>

                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_11Ataxlability" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%# Eval("TOTALCOUNT") %>
                                </td>

                                <%-- <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>--%>
                                <td><%# Eval("TOTALINVOICEVALUE") %></td>
                                <td><%# Eval("TOTALTAXABLEVALUE") %></td>
                                <td><%# Eval("TOTALTAXLIABILITY") %></td>
                                <td><%# Eval("IGSTAMT") %></td>
                                <td><%# Eval("CGSTAMT") %></td>
                                <td><%# Eval("SGSTAMT") %></td>
                                <td><%# Eval("UGSTAMT") %></td>
                                <td><%# Eval("CESSAMT") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_11Badvance" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%# Eval("TotalCount") %>
                                </td>

                                <%-- <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>--%>
                                <td><%# Eval("TotalValue") %></td>
                                <td><%# Eval("TotalTaxableValue") %></td>
                                <td><%# Eval("TotalTaxLiability") %></td>
                                <td><%# Eval("IGSTAmt") %></td>
                                <td><%# Eval("CGSTAmt") %></td>
                                <td><%# Eval("SGSTAmt") %></td>
                                <td><%# Eval("CESSAmt") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_9A_AMDB2B" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%# Eval("InvoiceNo") %>
                                </td>

                                <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>
                                <td><%# Eval("TotalValue") %></td>
                                <td><%# Eval("TotalTaxableValue") %></td>
                                <td><%# Eval("IGSTAmt") %></td>
                                <td><%# Eval("CGSTAmt") %></td>
                                <td><%# Eval("SGSTAmt") %></td>
                                <td><%# Eval("CESSAmt") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_9A_AMD_B2CL" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%# Eval("InvoiceNo") %>
                                </td>

                                <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>
                                <td><%# Eval("TotalValue") %></td>
                                <td><%# Eval("TotalTaxableValue") %></td>
                                <td><%# Eval("IGSTAmt") %></td>
                                <td><%# Eval("CGSTAmt") %></td>
                                <td><%# Eval("SGSTAmt") %></td>
                                <td><%# Eval("CESSAmt") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_9C_ame_crdrreg" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%# Eval("InvoiceNo") %>
                                </td>

                                <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>
                                <td><%# Eval("TotalValue") %></td>
                                <td><%# Eval("TotalTaxableValue") %></td>
                                <td><%# Eval("IGSTAmt") %></td>
                                <td><%# Eval("CGSTAmt") %></td>
                                <td><%# Eval("SGSTAmt") %></td>
                                <td><%# Eval("CESSAmt") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_9C_uncrdr" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%# Eval("TOTALCOUNT") %>
                                </td>
                                <td><%# Eval("TOTALINVOICEVALUE") %></td>
                                <td><%# Eval("TOTALTAXABLEVALUE") %></td>
                                <td><%# Eval("IGSTAMT") %></td>
                                <td><%# Eval("CGSTAMT") %></td>
                                <td><%# Eval("SGSTAMT") %></td>
                                <td><%# Eval("CESSAMT") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_amd_exp" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%# Eval("InvoiceNo") %>
                                </td>

                                <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>
                                <td><%# Eval("TotalValue") %></td>
                                <td><%# Eval("TotalTaxableValue") %></td>
                                <td><%# Eval("IGSTAmt") %></td>
                                <td><%# Eval("CGSTAmt") %></td>
                                <td><%# Eval("SGSTAmt") %></td>
                                <td><%# Eval("CESSAmt") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_amd_b2cl" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%# Eval("InvoiceNo") %>
                                </td>

                                <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>
                                <td><%# Eval("TotalValue") %></td>
                                <td><%# Eval("TotalTaxableValue") %></td>
                                <td><%# Eval("IGSTAmt") %></td>
                                <td><%# Eval("CGSTAmt") %></td>
                                <td><%# Eval("SGSTAmt") %></td>
                                <td><%# Eval("CESSAmt") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_11A_AMD" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%-- <%# Eval("InvoiceNo") %>--%>
                                </td>

                                <%--   <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>--%>
                                <td><%# Eval("TOTALINVOICEVALUE") %></td>
                                <td><%# Eval("TOTALTAXABLEVALUE") %></td>
                                <td><%# Eval("IGSTAMT") %></td>
                                <td><%# Eval("CGSTAmt") %></td>
                                <td><%# Eval("SGSTAMT") %></td>
                                <td><%# Eval("CESSAMT") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_11B_Advance" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%--<%# Eval("recivergstn") %>--%>
                                </td>

                                <td>
                                    <%--    <%# Eval("InvoiceNo") %>--%>
                                </td>

                                <%--  <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>--%>
                                <td><%# Eval("TotalValue") %></td>
                                <td><%# Eval("TotalTaxableValue") %></td>
                                <td><%# Eval("IGSTAmt") %></td>
                                <td><%# Eval("CGSTAmt") %></td>
                                <td><%# Eval("SGSTAmt") %></td>
                                <td><%# Eval("CESSAmt") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lv_gstr1_7" runat="server">
                        <EmptyDataTemplate>
                            <table class="table">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%></td>
                                <td>
                                    <%# Eval("recivergstn") %>
                                </td>

                                <td>
                                    <%--   <%# Eval("InvoiceNo") %>--%>
                                </td>

                                <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>
                                </td>
                                <td><%# Eval("TotalValue") %></td>
                                <td><%# Eval("TotalTaxableValue") %></td>
                                <td><%# Eval("IGSTAmt") %></td>
                                <td><%# Eval("CGSTAmt") %></td>
                                <td><%# Eval("SGSTAmt") %></td>
                                <td><%# Eval("CESSAmt") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' data-toggle="modal" ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                    <%--<uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" TotalTransitChargeAmount='<%#Eval("Freight")!=null? Convert.ToDecimal(Eval("Freight")):0 + Eval("Insurance")!=null? Convert.ToDecimal(Eval("Insurance")):0 + Eval("PackingAndForwadingCharges")!=null? Convert.ToDecimal(Eval("PackingAndForwadingCharges")):0 %>' InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />--%>
                                                    <%-- <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.GST_TRN_INVOICE_DATA")%>'/>--%>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info" runat="server" data-dismiss="modal" aria-hidden="true" text="Close"></button>
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
                                        <th style="width: 10px">S.No</th>
                                        <th style="width: 10px">Receiver GSTIN</th>
                                        <th style="width: 10px">Invoice No.</th>
                                        <th style="width: 10px">Invoice Date</th>
                                        <th style="width: 15px">Total Invoice Value</th>
                                        <th style="width: 40px">Total Taxable Value</th>
                                        <th style="width: 40px">IGST</th>
                                        <th style="width: 40px">CGST</th>
                                        <th style="width: 40px">SGST/UTGST</th>
                                        <th style="width: 40px">CESS</th>
                                        <th style="width: 20px">Action</th>
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
                <%--usercontrol for filereturn edit--%>
                <uc1:uc_FileReturn_Edit runat="server" visible="false" id="uc_FileReturn_Edit" />

                <div class="box-footer">
                    <asp:LinkButton ID="lkbInvBack" CssClass="btn btn-danger" OnClick="lkbInvBack_Click" runat="server"><i class="fa fa-backward">&nbsp;</i> Back</asp:LinkButton>
                    <%--<asp:LinkButton ID="lkbUpdateInvoice" OnClick="lkbUpdateInvoice_Click" Visible="false" CssClass="btn btn-primary " runat="server"><i class="fa fa-save"></i><span style="margin:3px;"> Update Invoice</span></asp:LinkButton>--%>
                </div>
            </div>
            <div id="GstrdetailsTileview" runat="server">
                <div class="box box-danger">
                    <div class="box-header with-border">
                        <h2 class="box-title">GSTR-1
                            <small>Invoice Details</small> </h2>
                    </div>
                    <div class="box-body">
                        <uc1:uc_Gstr1_Details_Tileview runat="server" ID="uc_Gstr1_Details_Tileview" />
                    </div>
                </div>
                <div class="box-footer">
                    <p>
                        <asp:CheckBox ID="chk" runat="server" />
                        &nbsp; &nbsp;
            I acknowledge that I have reviewed the details of the preview and the information is correct and would like to submit the details. I am aware that no changes can be made after submit.
                    </p>

                    <asp:LinkButton ID="lkbBack" CssClass="btn btn-default pull-left" Style="margin-right: 10px;" OnClick="lkbBack_Click" runat="server"><i class="fa fa-backward"></i>&nbsp;Back</asp:LinkButton>
                    <asp:LinkButton ID="lkbPreview" CssClass="btn btn-success pull-left" runat="server"><i class="fa fa-eye"></i>&nbsp;Preview</asp:LinkButton>
                    <asp:LinkButton ID="lkbSave" CssClass="btn btn-success pull-right" OnClick="lkbSave_Click" runat="server"><i class="fa fa-save"></i>&nbsp;Save</asp:LinkButton>
                    <asp:LinkButton ID="lkbSubmit" CssClass="btn btn-primary pull-right" Style="margin-right: 20px;" OnClick="lkbSubmit_Click" runat="server"><i class="fa fa-save"></i>&nbsp;Submit</asp:LinkButton>
                    <asp:LinkButton ID="lkbFile" OnClick="lkbFile_Click1" CssClass="btn btn-success pull-right" Style="margin-right: 10px;" runat="server"><i class="fa fa-file-text-o"></i>&nbsp;Proceed to File</asp:LinkButton>


                    <%--<asp:LinkButton ID="LinkButton1" OnClick="lkbReturn_Click" CssClass="btn btn-primary pull-right" data-target="#modal1" data-toggle="modal" Style="margin-right: 10px;" runat="server">File Return</asp:LinkButton>--%>
                </div>
            </div>
            <div class="row">
                <div class="modal" role="dialog" id="modal1" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog" style="overflow-y: auto; max-height: 85%; overflow: hidden;">
                        <div class="modal-content" style="height: auto !important;">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;
                                </button>
                                <h4 class="modal-title"><b><i class="fa fa-globe"></i>
                                    <asp:Label ID="lblModalTitle" runat="server" Text="OTP"></asp:Label>
                                </b></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtOtp" CssClass="form-control" placeholder="Enter OTP" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:LinkButton ID="lkbOtpVerify" OnClick="lkbOtpVerify_Click" CssClass="btn btn-primary" OnClientClick="$('.modal-backdrop').remove();" Text="Submit" runat="server"></asp:LinkButton>
                                        </div>
                                        <uc1:uc_sucess runat="server" ID="uc_sucess" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <%--        <uc1:uc_sucess runat="server" ID="uc_sucess1" />
                                <asp:LinkButton ID="btnSubmit" CssClass="btn btn-info" runat="server" OnClientClick="$('.modal-backdrop').remove();">Submit</asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="secondary" visible="false" runat="server">
            <div class="box box-default" runat="server" visible="true">
                <div class="box-body">
                    <div class="form-group">
                        <asp:CheckBox ID="chkreturn" AutoPostBack="true" OnCheckedChanged="chkreturn_CheckedChanged" runat="server" />I/We hereby solemnly affirm and declare that the information given herein above is true and correct to the the best of my knowledge and belief and nothing has been concealed therefrom.
                    </div>
                    <div id="innersecondary" runat="server" visible="false">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div id="AddSignatory" runat="server">
                                        <label>Authorized Signatory</label>
                                        <asp:DropDownList ID="ddlSinatory" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:LinkButton ID="lkbBackInv" OnClick="lkbBackInv_Click" CssClass="btn btn-default pull-left" Style="margin-right: 10px;" runat="server"><i class="fa fa-backward"></i>&nbsp; Back</asp:LinkButton>
                        <asp:LinkButton ID="lkbDraft" CssClass="btn btn-primary pull-left" Style="margin-right: 10px;" runat="server">&nbsp;Preview Draft Gstr-1</asp:LinkButton>
                        <asp:LinkButton ID="lkbEVC" OnClick="lkbEVC_Click" CssClass="btn btn-primary pull-left" Style="margin-right: 10px;" runat="server"><i class="fa fa-file-text-o"></i>&nbsp;File with EVC</asp:LinkButton>
                        <asp:LinkButton ID="lkbDSC" CssClass="btn btn-primary pull-left" Style="margin-right: 10px;" runat="server"><i class="fa fa-file-text-o"></i>&nbsp;File with DSC</asp:LinkButton>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
