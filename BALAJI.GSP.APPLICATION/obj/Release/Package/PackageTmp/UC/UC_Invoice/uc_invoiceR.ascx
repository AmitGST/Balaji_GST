<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_invoiceR.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.UC_Invoice.uc_invoiceR" %>



<asp:Repeater ID="rptInvoice" runat="server">
    <ItemTemplate>

        <!-- title row -->
        <%-- <div class="row">
            <div class="col-xs-12">
                <h2 class="page-header">
                    <i class="fa fa-globe"></i>Invoice 
            <small class="pull-right"></small>
                </h2>
            </div>
            <!-- /.col -->
        </div>--%>
        <!-- info row -->
        <div class="row invoice-info">
            <div class="box-body table-responsive">
                <div class="col-sm-3 invoice-col">
                    From         
                <address>
                    <strong>
                        <asp:Literal ID="liFrom" runat="server" Text='<%#Bind("NameAsOnGST") %>'></asp:Literal>.</strong><br>
                    <asp:Literal ID="Literal1" runat="server" Text='<%#Bind("Address") %>'></asp:Literal>
                    ,<br />
                    <b><%#DataBinder.Eval(Container.DataItem,"SellerStateCode")%>-<%#DataBinder.Eval(Container.DataItem,"SellerStateName")%></b>

                </address>


                </div>
                <!-- /.col -->
                <div class="col-sm-3 invoice-col">
                    Receiver          
                <address>
                    <strong>
                        <asp:Literal ID="Literal2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Reciever.NameAsOnGST")%>'></asp:Literal></strong><br>
                    <asp:Literal ID="Literal3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Reciever.Address")%>'></asp:Literal>,<br />
                    <b><%#DataBinder.Eval(Container.DataItem,"Reciever.StateCode")%>-<%#DataBinder.Eval(Container.DataItem,"Reciever.StateName")%></b><%--
                    <asp:Literal ID="Literal7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Consignee.StateName")%>'></asp:Literal>.<br />
                   Phone: (555) 539-1037<br>
                    Email: john.doe@example.com--%>
                </address>

                </div>
                <div class="col-sm-3 invoice-col">
                    Consignee         
                <address>
                    <strong>
                        <asp:Literal ID="Literal4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Consignee.NameAsOnGST")%>'></asp:Literal></strong><br>
                    <asp:Literal ID="Literal5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Consignee.Address")%>'></asp:Literal>
                    ,<br />
                    <b><%#DataBinder.Eval(Container.DataItem,"Consignee.StateCode")%>-<%#DataBinder.Eval(Container.DataItem,"Consignee.StateName")%></b><%--
                   <asp:Literal ID="Literal6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Consignee.StateName")%>'></asp:Literal>.<br />
                    Phone: (555) 539-1037<br>
                    Email: john.doe@example.com--%>
                </address>

                </div>
                <!-- /.col -->
                <div style="display: table-cell" class="col-sm-3 invoice-col">
                    <b>Invoice #<asp:Literal ID="Literal9" runat="server" Text='<%#Bind("SellerInvoice")%>'></asp:Literal></b><br>

                    <b>Invoice Date:</b>
                    <asp:Literal ID="Literal10" runat="server" Text='<%#Bind("DateOfInvoice")%>'></asp:Literal><br>
                    <%-- <b>Payment Due:</b> 2/22/2014<br>--%>
                    <%--<b>POS:</b>
                    <%#DataBinder.Eval(Container.DataItem,"SellerStateCode")%>-
                   <%#DataBinder.Eval(Container.DataItem,"SellerStateName")%><br>--%>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
            <div class="table-responsive">

                <asp:ListView ID="lstInvoiceR" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem,"Invoice.LineEntryDBType")%>'>
                    <EmptyDataTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;"><%# Container.DataItemIndex + 1%></td>

                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ItemCode")%> 
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Description")%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"Qty")%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Unit")%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"Rate")%>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTotal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TotalAmount")%>' /><%--TotalLineIDWise--%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"Discount")%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"TaxableAmount")%>
                            </td>
                            <td style="text-align: center;">

                                <%#DataBinder.Eval(Container.DataItem,"CGSTRate").ToString()=="0"?"-":DataBinder.Eval(Container.DataItem,"CGSTRate")%>
                            </td>

                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"CGSTAmt").ToString()=="0"?"-":DataBinder.Eval(Container.DataItem,"CGSTAmt")%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"SGSTRate").ToString()=="0"?"-":DataBinder.Eval(Container.DataItem,"SGSTRate")%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"SGSTAmt").ToString()=="0"?"-":DataBinder.Eval(Container.DataItem,"SGSTAmt")%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"IGSTRate").ToString()=="0"?"-":DataBinder.Eval(Container.DataItem,"IGSTRate")%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"IGSTAmt").ToString()=="0"?"-":DataBinder.Eval(Container.DataItem,"IGSTAmt")%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"UGSTRate").ToString()=="0"?"-":DataBinder.Eval(Container.DataItem,"UGSTRate")%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"UGSTAmt").ToString()=="0"?"-":DataBinder.Eval(Container.DataItem,"UGSTAmt")%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"CessRate").ToString()=="0.00"?"-":DataBinder.Eval(Container.DataItem,"CessRate")%>
                            </td>
                            <td style="text-align: center;">
                                <%#DataBinder.Eval(Container.DataItem,"CessAmt").ToString()=="0.00"?"-":DataBinder.Eval(Container.DataItem,"CessAmt")%>
                            </td>
                        </tr>

                    </ItemTemplate>

                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th style="width: 10px; vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom" rowspan="2">HSN/SAC</th>
                                <th style="vertical-align: bottom" rowspan="2">Description</th>
                                <th style="vertical-align: bottom" rowspan="2">Qty.</th>
                                <th style="vertical-align: bottom" rowspan="2">Unit</th>
                                <th style="vertical-align: bottom" rowspan="2">Rate<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom" rowspan="2">Total<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom" rowspan="2">Discount<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="vertical-align: bottom" rowspan="2">Taxable value</th>
                                <th colspan="2" style="text-align: center;">CGST</th>
                                <th colspan="2" style="text-align: center;">SGST</th>
                                <th colspan="2" style="text-align: center;">IGST</th>
                                <th colspan="2" style="text-align: center;">UTGST</th>
                                <th colspan="2" style="text-align: center;">CESS</th>
                            </tr>
                            <tr>

                                <th style="text-align: center;">Rate<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center;">Amt<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Rate<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center;">Amt<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Rate<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center;">Amt<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Rate<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center;">Amt<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Rate<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center;">Amt<br />
                                    (<i style="vertical-align: middle; font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>

                        </table>
                    </LayoutTemplate>
                </asp:ListView>

            </div>
        </div>

        <!-- Table row -->

        <!-- /.row -->
        <%--payment--%>
        <div class="row">
            <!-- accepted payments column -->
            <div class="col-md-6">
                <%--<p class="lead">Payment Methods:</p>
                <img src="../../dist/img/credit/visa.png" alt="Visa">
                <img src="../../dist/img/credit/mastercard.png" alt="Mastercard">
                <img src="../../dist/img/credit/american-express.png" alt="American Express">
                <img src="../../dist/img/credit/paypal2.png" alt="Paypal">

                <p class="text-muted well well-sm no-shadow" style="margin-top: 10px;">
                    Etsy doostang zoodles disqus groupon greplin oooj voxy zoodles, weebly ning heekya handango imeem plugg
            dopplr jibjab, movity jajah plickers sifteo edmodo ifttt zimbra.
                </p>--%>
            </div>
            <!-- /.col -->
            <div class="col-md-offset-2 col-md-4">
                <%--  <p class="lead">Amount Due 2/22/2014</p>--%>

                <div class="table-responsive table-condensed">
                    <table class="table table-condensed">
                        <tbody>
                            <tr>
                                <th style="width: 70%; vertical-align: middle;">Total Amount:</th>
                                <td style="width: 30%"><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<b><%#DataBinder.Eval(Container.DataItem,"TotalAmount")%></b></td>
                            </tr>
                            <tr>
                                <th style="width: 70%; vertical-align: middle;">Total Taxable Value:</th>
                                <td style="width: 30%"><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<b><%#DataBinder.Eval(Container.DataItem,"TotalTaxableAmount")%> </b></td>
                            </tr>
                            <tr>
                                <th style="width: 70%; vertical-align: middle;">Total CGST<%#Common.RCMTextBind(Eval("Invoice.InvoiceSpecialCondition")) %>:</th>
                                <td style="width: 30%"><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<b><%#DataBinder.Eval(Container.DataItem,"TotalCGSTAmount")%></b></td>
                            </tr>
                            <tr>
                                <th style="width: 70%; vertical-align: middle;">Total SGST/UTGST<%#Common.RCMTextBind(Eval("Invoice.InvoiceSpecialCondition")) %>:</th>
                                <td style="width: 30%"><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<b><%#DataBinder.Eval(Container.DataItem,"TotalSGSTAmount")%></b></td>
                            </tr>
                            <tr>
                                <th style="width: 70%; vertical-align: middle;">Total IGST<%#Common.RCMTextBind(Eval("Invoice.InvoiceSpecialCondition")) %>:</th>
                                <td style="width: 30%"><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<b><%#DataBinder.Eval(Container.DataItem,"TotalIGSTAmount")%></b></td>
                            </tr>
                            <tr>
                                <th style="width: 70%; vertical-align: middle;">Total Cess<%#Common.RCMTextBind(Eval("Invoice.InvoiceSpecialCondition")) %>:</th>
                                <td style="width: 30%"><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<b><%#DataBinder.Eval(Container.DataItem,"TotalCessAmount").ToString().Replace(".00","")%></b></td>
                            </tr>
                            <tr>
                                <th style="width: 70%; vertical-align: middle;">Freight+ Insurance + Pkg. Charges :</th>
                                <td style="width: 30%"><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<b>
                                    <%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"Invoice.Freight"))+Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"Invoice.Insurance"))+Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"Invoice.PackingAndForwadingCharges"))%>

                                </b></td>
                            </tr>
                            <tr>
                                <th style="width: 70%; vertical-align: middle;">Grand Total:</th>
                                <td style="width: 30%"><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<b>
                                    <%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"TotalAmountWithTax"))+Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"Invoice.Freight"))+Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"Invoice.Insurance"))+Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"Invoice.PackingAndForwadingCharges"))%></b></td>
                            </tr>

                            <%--  <tr>
                                <th style="width: 70%; vertical-align:middle; ">Gross Total: </th>
                                <td style="width:30%"><i style=" font-size:10px;" class="fa fa-inr"></i></td>
                            </tr>
                           <tr>
                                <th style="width: 50%">Total CGST Amount:</th>
                                <td><i class="fa fa-inr"></i> <%#DataBinder.Eval(Container.DataItem,"TotalCGSTAmount")%></td>
                            </tr>
                            </tr>
                            <tr>
                                <th style="width: 50%">Total IGST Amount:</th>
                                <td><i class="fa fa-inr"></i> <%#DataBinder.Eval(Container.DataItem,"TotalIGSTAmount")%></td>
                            </tr>
                            <tr>
                                <th style="width: 50%">Total SGST Amount:</th>
                                <td><i class="fa fa-inr"></i> <%#DataBinder.Eval(Container.DataItem,"TotalSGSTAmount")%></td>
                            </tr>--%>
                        </tbody>
                    </table>
                </div>
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->

        <!-- this row will not appear when printing -->
        <div class="row no-print">
            <div class="col-xs-12">
                <a href="invoice-print.html" target="_blank" style="display: none" class="btn btn-default"><i class="fa fa-print"></i>Print</a>
                <button type="button" style="display: none" class="btn btn-success pull-right">
                    <i class="fa fa-credit-card"></i>Submit Payment
                </button>
                <button type="button" style="display: none; margin-right: 5px;" class="btn btn-primary pull-right">
                    <i class="fa fa-download"></i>Generate PDF
                </button>
            </div>
        </div>

    </ItemTemplate>
</asp:Repeater>

