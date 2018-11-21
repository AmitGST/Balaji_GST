<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_InvoiceView.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.UC_Invoice.uc_InvoiceView" %>
<style type="text/css">
    .auto-style1 {
        vertical-align: bottom;
    }
</style>

<!-- info row -->
<div class="box-body table-responsive">
    <!--Seller-->
    <div class="col-sm-3 invoice-col">
        From         
                <address>
                    <strong>
                        <asp:Literal ID="liFrom" runat="server"></asp:Literal>,</strong><br>
                    <strong>
                        <asp:Literal ID="liorg" runat="server"></asp:Literal>,</strong><br>
                    <asp:Literal ID="liAdd" runat="server"></asp:Literal><br />
                   
                    <strong>
                        <asp:Literal ID="liPOSSeller" runat="server"></asp:Literal></strong><br>
                </address>
    </div>

    <!--reciever-->
    <div class="col-sm-3 invoice-col">
        Receiver          
                <address>
                    <strong>
                        <asp:Literal ID="liFrom1" runat="server"></asp:Literal>.</strong><br>
                    <strong>
                        <asp:Literal ID="liorg1" runat="server"></asp:Literal>.</strong><br>
                    <asp:Literal ID="liAdd1" runat="server"></asp:Literal><br />
                   
                    <strong>
                        <asp:Literal ID="liPOSReceiver" runat="server"></asp:Literal></strong><br>
                </address>
    </div>

    <!--Consignee-->
    <div class="col-sm-3 invoice-col">
        Consignee         
                 <address>
                     <strong>
                         <asp:Literal ID="liFrom2" runat="server"></asp:Literal>.</strong><br>
                     <strong>
                         <asp:Literal ID="liorg2" runat="server"></asp:Literal>.</strong><br>
                     <asp:Literal ID="liAdd3" runat="server"></asp:Literal><br />
                    
                    <strong>
                        <asp:Literal ID="liPOSConsignee" runat="server"></asp:Literal></strong><br>
                 </address>
    </div>

    <div style="display: table-cell" class="col-sm-3 invoice-col">
        <b>Invoice #<asp:Literal ID="liinvoiceno" runat="server"></asp:Literal></b><br>

        <b>Invoice Date:</b>
        <asp:Literal ID="lidate1" runat="server"></asp:Literal><br>

       
    </div>
</div>
<%----end--%>
<div class="table-responsive">
    <%--DataSource='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>'--%>
    <asp:ListView ID="lstInvoiceR" runat="server" OnItemDataBound="lstInvoiceR_ItemDataBound" ItemType="DataAccessLayer.GST_TRN_INVOICE_DATA">
        <EmptyDataTemplate>
            <table class="table  table-bordered table-condensed">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr>
                <td style="text-align: center;"><%# Container.DataItemIndex + 1%></td>


                <td style="text-align: center;">

                    <asp:Label ID="lblHsn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ItemCode")%>' />
                </td>
                <td>
                    <asp:Label ID="lblDesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Description")%>' />
                </td>
                <td style="text-align: center;">
                    <asp:Label ID="lblQty" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Qty")%>' />
                </td>
                <td style="text-align: center;">
                    <asp:Label ID="lblUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Unit")%>' />
                </td>
                <td style="text-align: center;">
                    <asp:Label ID="lblRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Rate")%>' />
                </td>
                <td style="text-align: center;">
                    <asp:Label ID="lblTotal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TotalAmount")%>' /><%--TotalLineIDWise--%>
                </td>
                <td style="text-align: center;">
                    <asp:Label ID="lblDis" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Discount")%>' />
                </td>
                <td style="text-align: center;">
                    <asp:Label ID="lblTaxval" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TaxableAmount")%>' />
                </td>
                <td style="text-align: center;">

                    <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CGSTRate").ToString()=="0.00"?"-":DataBinder.Eval(Container.DataItem,"CGSTRate")%>' />
                </td>

                <td style="text-align: center;">
                    <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CGSTAmt").ToString()=="0.00"?"-":DataBinder.Eval(Container.DataItem,"CGSTAmt")%>' />
                </td>
                <td style="text-align: center;">
                    <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SGSTRate").ToString()=="0.00"?"-":DataBinder.Eval(Container.DataItem,"SGSTRate")%>' />
                </td>
                <td style="text-align: center;">
                    <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SGSTAmt").ToString()=="0.00"?"-":DataBinder.Eval(Container.DataItem,"SGSTAmt")%>' />
                </td>
                <td style="text-align: center;">
                    <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IGSTRate").ToString()=="0.00"?"-":DataBinder.Eval(Container.DataItem,"IGSTRate")%>' />
                </td>
                <td style="text-align: center;">
                    <asp:Label ID="lblIGSTAmt" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IGSTAmt").ToString()=="0.00"?"-":DataBinder.Eval(Container.DataItem,"IGSTAmt")%>' />
                </td>
                <td style="text-align: center;">
                    <asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UGSTRate").ToString()=="0.00"?"-":DataBinder.Eval(Container.DataItem,"UGSTRate")%>' />
                <td style="text-align: center;">
                    <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UGSTAmt").ToString()=="0.00"?"-":DataBinder.Eval(Container.DataItem,"UGSTAmt")%>' />
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CessRate").ToString()=="0.00"?"-":DataBinder.Eval(Container.DataItem,"CessRate")%>' />
                </td>
                <td style="text-align: center;">
                    <%#DataBinder.Eval(Container.DataItem,"CessAmt").ToString()=="0.00"?"-":DataBinder.Eval(Container.DataItem,"CessAmt")%>
                </td>

            </tr>


        </ItemTemplate>

        <LayoutTemplate>
            <table class="table table-condensed table-bordered">
                <tr>
                    <th style="width: 10px; vertical-align: bottom;" rowspan="2">#</th>
                    <th style="vertical-align: bottom" rowspan="2">HSN/SAC</th>
                    <th style="vertical-align: bottom" rowspan="2">Description</th>
                    <th style="vertical-align: bottom" rowspan="2">Qty.</th>
                    <th style="vertical-align: bottom" rowspan="2">Unit</th>
                    <th style="vertical-align: bottom" rowspan="2">Rate<br />
                        (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                    <th style="vertical-align: bottom" rowspan="2">Total<br />
                        (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                    <th style="vertical-align: bottom" rowspan="2">Discount<br />
                        (<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                    <th style="vertical-align: bottom" rowspan="2">Taxable value</th>
                    <th colspan="2" style="text-align: center;">CGST</th>
                    <th colspan="2" style="text-align: center;">SGST</th>
                    <th colspan="2" style="text-align: center;">IGST</th>
                    <th colspan="2" style="text-align: center;">UTGST</th>
                    <th colspan="2" style="text-align: center;">CESS</th>
                </tr>
                <tr>
                    <th style="text-align: center;">Rate<br />
                        (<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                    <th style="text-align: center;">Amt<br />
                        (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                    <th style="text-align: center;">Rate<br />
                        (<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                    <th style="text-align: center;">Amt<br />
                        (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                    <th style="text-align: center;">Rate<br />
                        (<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                    <th style="text-align: center;">Amt<br />
                        (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                    <th style="text-align: center;">Rate<br />
                        (<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                    <th style="text-align: center;">Amt<br />
                        (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                    <th style="text-align: center;">Rate<br />
                        (<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                    <th style="text-align: center;">Amt<br />
                        (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </tbody>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
</div>
<div class="row">
    <div class="col-md-6">
        <%-- <p class="lead">Payment Methods:</p>
        <img src="../../dist/img/credit/visa.png" alt="Visa">
        <img src="../../dist/img/credit/mastercard.png" alt="Mastercard">
        <img src="../../dist/img/credit/american-express.png" alt="American Express">
        <img src="../../dist/img/credit/paypal2.png" alt="Paypal">

        <p class="text-muted well well-sm no-shadow" style="margin-top: 10px;">
            Etsy doostang zoodles disqus groupon greplin oooj voxy zoodles, weebly ning heekya handango imeem plugg
            dopplr jibjab, movity jajah plickers sifteo edmodo ifttt zimbra.
        </p>--%>
    </div>
    <div class="col-md-offset-2 col-md-4">
        <%--  <p class="lead">Amount Due 2/22/2014</p>--%>

        <div class="table-responsive table-condensed">
            <table class="table table-condensed">
                <tbody>
                    <tr>
                        <th style="width: 70%;">
                            <asp:Literal ID="litTotalAmount" runat="server">Total Amount</asp:Literal>:</th>
                        <td style="width: 30%;"><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<b><asp:Label ID="lblTotalAmt" runat="server"></asp:Label><%--<%#TotalAmt(DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")) %>--%></b></td>
                    </tr>
                    <tr>
                        <th style="width: 70%; vertical-align: middle;">
                            <asp:Literal ID="litTotalTaxableValue" runat="server">Total Taxable Value</asp:Literal>:</th>
                        <td style="width: 30%;" class="auto-style3"><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<b><asp:Label ID="lbltotaltaxvalue" runat="server" Text="Label"></asp:Label></b></td>
                    </tr>

                    <tr>
                        <th style="width: 70%; vertical-align: middle;" class="auto-style1">
                            <asp:Literal ID="litTotalCGST" runat="server"></asp:Literal>:</th>
                        <td style="width: 30%;" class="auto-style3"><b><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<asp:Label ID="lbltotalCGST" runat="server" Text="Label"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <th style="width: 70%; vertical-align: middle;">
                            <asp:Literal ID="litTotalSGSTUTGST" runat="server"></asp:Literal>:</th>
                        <td style="width: 30%;"><b><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<asp:Label ID="lbltotalSGST" runat="server" Text="Label"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <th style="width: 70%; vertical-align: middle;">
                            <asp:Literal ID="litTotalIGST" runat="server"></asp:Literal>:</th>
                        <td style="width: 30%;"><b><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<asp:Label ID="lbltotalIGST" runat="server" Text="Label"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <th style="width: 70%; vertical-align: middle;" class="auto-style1">
                            <asp:Literal ID="litTotalCess" runat="server"></asp:Literal>:</th>
                        <td style="width: 30%;"><b><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<asp:Label ID="lbltotalCess" runat="server" Text="Label"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <th style="width: 70%; vertical-align: middle;" class="auto-style1">Freight + Insurance + Pkg. Charges:</th>
                        <td style="width: 30%;"><b><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<asp:Label ID="lbltotalfreightcharges" runat="server"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <th style="width: 70%; vertical-align: middle;" class="auto-style1">Grand Total:</th>
                        <td style="width: 30%;"><i style="font-size: 10px;" class="fa fa-inr"></i>&nbsp;<b><asp:Label ID="lblTotalAmtwithDis" runat="server"></asp:Label></b></td>
                    </tr>
                    <%-- <tr>
                        <th style="width: 70%; vertical-align:middle;">Gross Total:</th>
                        <td style="width:30%"><i style="font-size:10px;" class="fa fa-inr"></i><b> <asp:Label ID="Label6" runat="server"></asp:Label></b></td>
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
</div>

<!-- this row will not appear when printing -->
<div class="row no-print" style="display: none">
    <div class="col-xs-12">
        <a href="invoice-print.html" target="_blank" class="btn btn-default"><i class="fa fa-print"></i>Print</a>
        <button type="button" class="btn btn-success pull-right">
            <i class="fa fa-credit-card"></i>Submit Payment
        </button>
        <button type="button" class="btn btn-primary pull-right" style="margin-right: 5px;">
            <i class="fa fa-download"></i>Generate PDF
        </button>
    </div>
</div>

<%--   </ItemTemplate>
</asp:FormView>--%>
