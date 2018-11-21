<%@ Page Title="" Language="C#" MasterPageFile="../User.Master" AutoEventWireup="true" CodeBehind="GSTR4.aspx.cs" Inherits="UserInterface.GSTR4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- <div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">GSTR4-Quarterly Return for Compounding Taxable person</h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>--%>
      
    <div class="content-header">
        <h1>GSTR4
        <small>Quarterly return for registered person opting for composition levy</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">RETURN</a></li>
            <li class="active">GSTR4</li>
        </ol>
    </div>

    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Taxpayer Details</h3>
            </div>

            <div class="box-body">
                <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>
                        1. GSTIN:
                    </label>
                    <asp:Label ID="lblGSTIN" runat="server"> </asp:Label>
                   
                </div>
            </div>
            <div class="col-md-8">
            <div class="pull-right">
                <label>Financial Year: </label>
                <asp:Label ID="lblFinYear" Text="FinancialYear" runat="server"> </asp:Label>
            </div>
        </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>
                     2(a). Legal name of the registered person:
                    </label>
                    <asp:Label ID="lblTaxpayerName" runat="server"></asp:Label>
                </div>
            </div>
             <div class="col-md-8">
            <div class="pull-right">
                <label>Month: </label>
                <asp:Label ID="lblMonth" Text="Month" runat="server"></asp:Label>
            </div>
        </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>
                         &nbsp; &nbsp;(b). Trade name, if any:
                    </label>
                    <asp:Label ID="lblTurnoverYear" runat="server" Style="display: none;"></asp:Label>
                </div>
            </div>
        </div>
                <div class="row">
            <div class="col-md-5">
                <div class="form-group">
                    <label>
                         3(a). Aggregate Turnover in the preceding Financial Year:
                    </label>
                    <asp:Label ID="Label1" runat="server" Style="display: none;"></asp:Label>
                </div>
            </div>
        </div>

                <div class="row">
            <div class="col-md-5">
                <div class="form-group">
                    <label>
                      &nbsp;   &nbsp;(b). Aggregate Turnover - April to June, 2017:
                    </label>
                    <asp:Label ID="Label2" runat="server" Style="display: none;"></asp:Label>
                </div>
            </div>
        </div>
            </div>
                 
        </div>
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">4.&nbsp; Inward supplies including supplies on which tax is to be paid on reverse charge </h3>
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
                <asp:ListView ID="lv_GSTR4_4A" runat="server">
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
                              <%# Eval("GSTNNO") %>
                            </td>
                            <td>
                                <%# Eval("INVOICENO") %>
                            </td>
                            <td>
                                <%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("TOTALAMOUNT") %>
                            </td>
                            <td style="text-align: center;">
                             <%# Eval("RATE") %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("TAXABLEAMOUNT") %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %>
                            <td>
                               <%# Eval("STATENAME") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-responsive">
                            <tr>
                                <th colspan="12">4A.&nbsp; Inward supplies received from a registered supplier (other than supplies attracting reverse charge)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center;" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate<br/>(<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="text-align: center; vertical-align: bottom;">Value </th>
                                <th style="text-align: center;">IGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
              
                <asp:ListView ID="lv_GSTR4_4B" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-striped">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                             <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                              <%# Eval("GSTNNO") %>
                            </td>
                            <td>
                                <%# Eval("INVOICENO") %>
                            </td>
                            <td>
                                <%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("TOTALAMOUNT") %>
                            </td>
                            <td style="text-align: center;">
                             <%# Eval("RATE") %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("TAXABLEAMOUNT") %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %>
                            <td>
                               <%# Eval("STATENAME") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                             <tr>
                            <th colspan="12">4B.&nbsp; Inward supplies received from a registered supplier (attracting reverse charge)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center;" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="text-align: center; vertical-align: bottom;">Value </th>
                                <th style="text-align: center;">IGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                
                <asp:ListView ID="lv_GSTR4_4C" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-striped">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                             <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                              <%# Eval("GSTNNO") %>
                            </td>
                            <td>
                                <%# Eval("INVOICENO") %>
                            </td>
                            <td>
                                <%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("TOTALAMOUNT") %>
                            </td>
                            <td style="text-align: center;">
                             <%# Eval("RATE") %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("TAXABLEAMOUNT") %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %>
                            <td>
                               <%# Eval("STATENAME") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                             <tr>
                                <th colspan="12">4C.&nbsp; Inward supplies received from an unregistered supplier</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center;" colspan="3">Invoice Details</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom;" rowspan="2">Taxable value<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="text-align: center; vertical-align: bottom;">Value </th>
                                <th style="text-align: center;">IGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR4_4D" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-striped">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                             <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                              <%# Eval("GSTNNO") %>
                            </td>
                            <td>
                                <%# Eval("INVOICENO") %>
                            </td>
                            <td>
                                <%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("TOTALAMOUNT") %>
                            </td>
                            <td style="text-align: center;">
                             <%# Eval("RATE") %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("TAXABLEAMOUNT") %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %>
                            <td>
                               <%# Eval("STATENAME") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="12">4D.&nbsp; Import of service</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center;" colspan="3">Invoice Details</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom;" rowspan="2">Taxable value<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="text-align: center; vertical-align: bottom;">Value </th>
                                <th style="text-align: center;">IGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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

         <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">5.Amendments to details of inward supplies furnished in returns for earlier tax periods in Table 4 [including debit notes/credit notes and their subsequent amendments]</h3>
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
                <asp:ListView ID="lv_GSTR4_5A" runat="server">
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
                                <td><%# Eval("ORGINAL_GSTN") %></td>
                                <td><%# Eval("ORGINAL_NO") %></td>
                                <td><%# Eval("ORGINAL_DATE") %></td>
                                <td><%# Eval("F_GSTN") %></td>
                                <td><%# Eval("F_NO") %></td>
                                <td><%# Eval("F_DATE") %></td>
                                <td><%# Eval("RATE") %></td>
                                <td><%# Eval("TAXABLE_VALUE") %></td>
                                <td><%# Eval("IGST_TAX") %></td>
                                <td><%# Eval("CGST_TAX") %></td>
                                <td><%# Eval("SGST_TAX") %><%# Eval("UGST_TAX") %></td>
                                <td><%# Eval("CESS") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="15">5A.&nbsp;Supplies [Information furnished in Table 4 of earlier returns]-If details furnished earlier were incorrect</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" colspan="3">Details of original invoice</th>
                                <th style="vertical-align:bottom; text-align: center;" colspan="4">Revised details of invoice </th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate<br />(<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">GSTIN</th>
                                 <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                 <th style="vertical-align: bottom;" >GSTIN</th>
                                 <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="vertical-align: bottom;">Value</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
              
                <asp:ListView ID="lv_GSTR4_5B" runat="server">
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
                                <td><%# Eval("ORGINAL_GSTN") %></td>
                                <td><%# Eval("ORGINAL_NO") %></td>
                                <td><%# Eval("ORGINAL_DATE") %></td>
                                <td><%# Eval("F_GSTN") %></td>
                                <td><%# Eval("F_NO") %></td>
                                <td><%# Eval("F_DATE") %></td>
                                <td><%# Eval("RATE") %></td>
                                <td><%# Eval("TAXABLE_VALUE") %></td>
                                <td><%# Eval("IGST_TAX") %></td>
                                <td><%# Eval("CGST_TAX") %></td>
                                <td><%# Eval("SGST_TAX") %><%# Eval("UGST_TAX") %></td>
                                <td><%# Eval("CESS") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="15">5B.&nbsp;Debit Notes/Credit Notes [original)]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" colspan="3">Details of original invoice</th>
                                <th style="vertical-align:bottom; text-align: center;" colspan="4">Revised details of invoice </th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate<br />(<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">GSTIN</th>
                                 <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                 <th style="vertical-align: bottom;" >GSTIN</th>
                                 <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="vertical-align: bottom;">Value</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR4_5C" runat="server">
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
                                <td><%# Eval("ORGINAL_GSTN") %></td>
                                <td><%# Eval("ORGINAL_NO") %></td>
                                <td><%# Eval("ORGINAL_DATE") %></td>
                                <td><%# Eval("F_GSTN") %></td>
                                <td><%# Eval("F_NO") %></td>
                                <td><%# Eval("F_DATE") %></td>
                                <td><%# Eval("RATE") %></td>
                                <td><%# Eval("TAXABLE_VALUE") %></td>
                                <td><%# Eval("IGST_TAX") %></td>
                                <td><%# Eval("CGST_TAX") %></td>
                                <td><%# Eval("SGST_TAX") %><%# Eval("UGST_TAX") %></td>
                                <td><%# Eval("CESS") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="15">5C.&nbsp;Debit Notes/ Credit Notes [amendment of debit notes/credit notes furnished in earlier tax periods]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" colspan="3">Details of original invoice</th>
                                <th style="vertical-align:bottom; text-align: center;" colspan="4">Revised details of invoice </th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate<br />(<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">GSTIN</th>
                                 <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                 <th style="vertical-align: bottom;" >GSTIN</th>
                                 <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="vertical-align: bottom;">Value</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">6.&nbsp;Tax on outward supplies made (Net of advance and goods returned) </h3>
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
                <asp:ListView ID="lv_GSTR4_6" runat="server">
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
                                <td><%# Eval("Rate_Of_Tax") %></td>
                                <td><%# Eval("Turnover") %></td>
                                <td><%# Eval("Composition_CGST_TAX") %></td>
                                <td><%# Eval("Composition_SGST_TAX") %><%# Eval("Composition_UTGST_TAX") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate of tax</th>
                                <th style="vertical-align:bottom; text-align: center;" rowspan="2">Turnover</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="2" >Composition tax amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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

        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">7.&nbsp;Amendments to Outward Supply details furnished in returns for earlier tax periods in Table No. 6</h3>
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
                <asp:ListView ID="lv_GSTR4_7" runat="server">
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
                                <td><%# Eval("Quarter") %></td>
                                <td><%# Eval("Rate") %></td>
                                <td><%# Eval("Original_Turnover") %></td>
                                <td><%# Eval("Original_CGST_TAX") %></td>
                                <td><%# Eval("Original_SGST_TAX") %>
                              <%# Eval("Original_UGST_TAX") %></td>
                                <td><%# Eval("Revised_Turnover") %></td>
                                <td><%# Eval("Revised_CGST_TAX") %></td>
                               <td><%# Eval("Revised_SGST_TAX") %>
                              <%# Eval("Revised_UGST_TAX") %></td>
                                
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">Quarter</th>
                                <th style="vertical-align:bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align: bottom;"colspan="3">Original details</th>
                                 <th style="text-align: center; vertical-align: bottom;" colspan="3">Revised details</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">Turnover</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Turnover</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">8.&nbsp;Consolidated Statement of Advances paid/Advance adjusted on account of receipt of supply</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>
            </div>
              <h5 style="font-weight: bold;">&nbsp;&nbsp;&nbsp;8A.&nbsp; Advance amount paid for reverse charge supplies in the tax period (tax amount to be added to output tax liability) </h5>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR_4_8A_1" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                               <td><%# Eval("") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">8A (1).&nbsp;Intra-State supplies (Rate Wise)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="vertical-align:bottom; text-align: center;" colspan="4">Gross Advance Paid</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Place of supply</th>
                                 <th style="text-align: center; vertical-align: bottom;" rowspan="2">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR_4_8A_2" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">8A (2).&nbsp;Inter-State Supplies (Rate Wise)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="vertical-align:bottom; text-align: center;" colspan="4">Gross Advance Paid</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Place of supply</th>
                                 <th style="text-align: center; vertical-align: bottom;" rowspan="2">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                  <h5 style="font-weight: bold;">8B.&nbsp; Advance amount on which tax was paid in earlier period but invoice has been received in the current period [reflected in Table 4 above] (tax amount to be reduced from output tax liability)</h5>
                <asp:ListView ID="lv_GSTR_4_8B_1" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                               <td><%# Eval("") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">8B (1).&nbsp;Intra-State Supplies (Rate Wise)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="vertical-align:bottom; text-align: center;" colspan="4">Gross Advance Paid</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Place of supply</th>
                                 <th style="text-align: center; vertical-align: bottom;" rowspan="2">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR_4_8B_2" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                               <td><%# Eval("") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">8B (2).&nbsp;Intra-State Supplies (Rate Wise)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="vertical-align:bottom; text-align: center;" colspan="4">Gross Advance Paid</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Place of supply</th>
                                 <th style="text-align: center; vertical-align: bottom;" rowspan="2">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess</th>
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

        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">9.&nbsp;TDS Credit received </h3>
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
                <asp:ListView ID="lv_GSTR4_9" runat="server">
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
                                <td><%# Eval("GSTIN_OF_DEDUCTOR") %></td>
                                <td><%# Eval("GROSS_VALUE") %></td>
                                <td><%# Eval("CGST_TAX") %></td>
                                <td><%# Eval("SGST_TAX") %>
                              <%# Eval("UTGST_TAX") %></td>
                                
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN of Deductor</th>
                                <th style="vertical-align:bottom; text-align: center;" rowspan="2">Gross Value</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="2" >Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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

        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">10.&nbsp;Tax payable and paid</h3>
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
                <asp:ListView ID="lv_GSTR4_10A" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">(a) Integrated Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="vertical-align:bottom; text-align: center;">Tax amount payable</th>
                                <th style="text-align: center; vertical-align: bottom;">Pay tax amount</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR4_10B" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">(b) Central Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="vertical-align:bottom; text-align: center;">Tax amount payable</th>
                                <th style="text-align: center; vertical-align: bottom;">Pay tax amount</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
              
                <asp:ListView ID="lv_GSTR4_10C" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">(c) State/UT Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="vertical-align:bottom; text-align: center;">Tax amount payable</th>
                                <th style="text-align: center; vertical-align: bottom;">Pay tax amount</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR4_10D" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">(d) Cess</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="vertical-align:bottom; text-align: center;">Tax amount payable</th>
                                <th style="text-align: center; vertical-align: bottom;">Pay tax amount</th>
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

        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">11.&nbsp;Interest, Late Fee payable and paid</h3>
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
                <asp:ListView ID="lv_GSTR4_11_A" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">11.a. Integrated tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="vertical-align:bottom; text-align: center;">Amount payable</th>
                                <th style="text-align: center; vertical-align: bottom;">Amount Paid</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
              <asp:ListView ID="lv_GSTR4_11B" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                               
                                
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">(b).Central Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="vertical-align:bottom; text-align: center;">Amount payable</th>
                                <th style="text-align: center; vertical-align: bottom;">Amount Paid</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR4_11C" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                               
                                
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">(c) State/UT Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="vertical-align:bottom; text-align: center;">Amount payable</th>
                                <th style="text-align: center; vertical-align: bottom;">Amount Paid</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR4_11D" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                               
                                
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">(d) Cess</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="vertical-align:bottom; text-align: center;">Amount payable</th>
                                <th style="text-align: center; vertical-align: bottom;">Amount Paid</th>
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
        <%--<div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">11.&nbsp;Interest, Late Fee payable and paid</h3>
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
                
            </div>
        </div>--%>
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">12.&nbsp;Refund claimed from Electronic cash ledger</h3>
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
                <asp:ListView ID="lv_GSTR4_12" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                               <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                 <td><%# Eval("") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="vertical-align:bottom; text-align: center;">Tax</th>
                                <th style="text-align: center; vertical-align: bottom;">Interest</th>
                                <th style="text-align: center; vertical-align: bottom;">Penalty</th>
                                <th style="text-align: center; vertical-align: bottom;">Fee</th>
                                <th style="text-align: center; vertical-align: bottom;">Other</th>
                                <th style="text-align: center; vertical-align: bottom;">Debit Entry Nos.</th>
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
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">13.&nbsp; Debit entries in cash ledger for tax /interest payment [to be populated after payment of tax and submissions of return]</h3>
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
                <asp:ListView ID="lv_GSTR4_13" runat="server">
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
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                                <td><%# Eval("") %></td>
                               <td><%# Eval("") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="vertical-align:bottom; text-align: center;">Tax paid in cash</th>
                                <th style="text-align: center; vertical-align: bottom;">Interest</th>
                                <th style="text-align: center; vertical-align: bottom;">Late fee</th>
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
        
    </div>
</asp:Content>
