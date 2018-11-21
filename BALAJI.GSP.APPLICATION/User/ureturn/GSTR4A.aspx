<%@ Page Title="" Language="C#" MasterPageFile="../User.Master" AutoEventWireup="true" CodeBehind="GSTR4A.aspx.cs" Inherits="UserInterface.GSTR4A" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--  <div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">GSTR4A- AUTO DRAFTED DETAILS</h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>--%>

    <div class="content-header">
        <h1>GSTR4A
        <small>Auto-drafted details for registered person opting for composition levy</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">RETURN</a></li>
            <li class="active">GSTR4A</li>
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
            </div>
                 
        </div>

        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">3.&nbsp; Inward supplies received from registered person including supplies attracting reverse charge</h3>
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
                <asp:ListView ID="lv_GSTR4A_3A" runat="server">
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
                                <%# Eval("IGSTAMT")%>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("CGSTAMT")%>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("SGSTAMT") %><%# Eval("UGSTAMT") %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("CESSAMT")%>
                            <td>
                               <%# Eval("STATENAME") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-responsive">
                            <tr>
                                <th colspan="12">3A.&nbsp; Inward supplies received from a registered supplier (other than supplies attracting reverse charge)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center;" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate<br/>(<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount of tax</th>
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

                <asp:ListView ID="lv_GSTR4A_3B" runat="server">
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
                                <%# Eval("IGSTAMT")%>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("CGSTAMT")%>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("SGSTAMT") %><%# Eval("UGSTAMT") %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("CESSAMT")%>
                            <td>
                               <%# Eval("STATENAME") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-responsive">
                            <tr>
                                <th colspan="12">3B.&nbsp; Inward supplies received from a registered supplier (attracting reverse charge)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center;" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate<br/>(<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount of tax</th>
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
                <h3 class="box-title">4. Debit notes/credit notes (including amendments thereof) received during current period</h3>
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
                <asp:ListView ID="lv_GSTR4A_4" runat="server">
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
                                <%# Eval("IGSTAMT")%>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("CGSTAMT")%>
                            </td>
                            <td style="text-align: center;">
                               <%# Eval("SGSTAMT")%><%# Eval("UGSTAMT")%>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("CESSAMT") %>
                            <td>
                               <%# Eval("STATENAME") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-responsive">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" colspan="3">Details of original document</th>
                                <th style="text-align: center;" colspan="4">Revised details of document Debit / Credit Note</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate<br/>(<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount of tax</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">GSTIN</th>
                                <th style="vertical-align: bottom;">No.</th>
                                <th style="text-align: center; vertical-align: bottom;">Date </th>
                                <th style="vertical-align: bottom;">GSTIN</th>
                                <th style="vertical-align: bottom;">No.</th>
                                <th style="text-align: center; vertical-align: bottom;">Date </th>
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
                <h3 class="box-title">5. TDS Credit received</h3>
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
                <asp:ListView ID="lv_GSTR4A_5" runat="server">
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
                              <%# Eval("GSTN_NO") %>
                            </td>
                            <td>
                                <%# Eval("GROSS_VALUE") %>
                            </td>
                            <td>
                               <%# Eval("CGST_TAX") %>
                            </td>
                            <td>
                             <%# Eval("SGST_TAX") %><%# Eval("UTGST_TAX") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-responsive">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN of deductor</th>
                                <th style="vertical-align: bottom;" rowspan="2">Gross value</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="2">Amount of tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">Central Tax</th>
                                <th style="vertical-align: bottom;">State/UT Tax</th>
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
