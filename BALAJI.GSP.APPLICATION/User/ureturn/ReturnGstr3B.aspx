<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="ReturnGstr3B.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.ReturnGstr3B" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>GSTR-3B Monthly Return
        <small>Details of outward supplies of goods or services</small> </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="#">Returns</a></li>
            <li class="active">GSTR1</li>
        </ol>
    </div>
    <div class="content">
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
                                    <label for="">Status:</label>
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
                                    <label>Duedate:</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        <asp:ListView ID="lvtest" runat="server">
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
                                <%# Eval("RECIVERGSTN") %>
                            </td>

                            <td>
                                <%# Eval("InvoiceNo") %>
                            </td>

                            <td>'<%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>'
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
        <asp:ListView ID="ListView1" runat="server">
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
                                <%# Eval("RECIVERGSTN") %>
                            </td>

                            <td>
                                <%# Eval("InvoiceNo") %>
                            </td>

                            <td>'<%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>'
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
        <asp:ListView ID="ListView2" runat="server">
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
                                <%# Eval("RECIVERGSTN") %>
                            </td>

                            <td>
                                <%# Eval("InvoiceNo") %>
                            </td>

                            <td>'<%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>'
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
        <asp:ListView ID="ListView3" runat="server">
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
                                <%# Eval("RECIVERGSTN") %>
                            </td>

                            <td>
                                <%# Eval("InvoiceNo") %>
                            </td>

                            <td>'<%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>'
                            </td>
                            <td><%# Eval("TotalValue") %></td>
                            <td><%# Eval("TotalTaxableValue") %></td>
                            <td><%# Eval("IGSTAmt") %></td>
                            <td><%# Eval("CGSTAmt") %></td>
                            <td><%# Eval("SGSTAmt") %></td>
                            <td><%# Eval("CESSAmt") %></td>
                            <td>
                                <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server"  ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                
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
        <asp:ListView ID="ListView4" runat="server">
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
                                <%# Eval("RECIVERGSTN") %>
                            </td>

                            <td>
                                <%# Eval("InvoiceNo") %>
                            </td>

                            <td>'<%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>'
                            </td>
                            <td><%# Eval("TotalValue") %></td>
                            <td><%# Eval("TotalTaxableValue") %></td>
                            <td><%# Eval("IGSTAmt") %></td>
                            <td><%# Eval("CGSTAmt") %></td>
                            <td><%# Eval("SGSTAmt") %></td>
                            <td><%# Eval("CESSAmt") %></td>
                            <td>
                                <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server"  ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                
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
        <asp:ListView ID="ListView5" runat="server">
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
                                <%# Eval("RECIVERGSTN") %>
                            </td>

                            <td>
                                <%# Eval("InvoiceNo") %>
                            </td>

                            <td>'<%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>'
                            </td>
                            <td><%# Eval("TotalValue") %></td>
                            <td><%# Eval("TotalTaxableValue") %></td>
                            <td><%# Eval("IGSTAmt") %></td>
                            <td><%# Eval("CGSTAmt") %></td>
                            <td><%# Eval("SGSTAmt") %></td>
                            <td><%# Eval("CESSAmt") %></td>
                            <td>
                                <asp:LinkButton ID="lkbView" CssClass="btn btn-danger btn-xs" runat="server"  ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                
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
</asp:Content>
