<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="GSTR7.aspx.cs" Inherits="UserInterface.GSTR7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">GSTR7-TDS Return</h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>--%>

    <div class="content-header">
        <h1>GSTR7
        <small>Return for Tax Deducted at Source</small> </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">GSTR7</li>
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
                                2.(a) Legal name of the Deductor:
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
                                &nbsp;&nbsp;&nbsp; (b) Trade name, if any:
                            </label>
                            <asp:Label ID="lblTurnoverYear" runat="server" Style="display: none;"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-danger">
            <div class="box-header with-border">

                <h3 class="box-title">3. Details of the tax deducted at source</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR7_3" runat="server">
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
                            <td><%# Eval("GSTIN_NO") %></td>
                            <td><%# Eval("Amount_paid_deducted_tax") %></td>
                            <td style="text-align: center;"><%# Eval("deducted_IGST_TAX") %></td>
                            <td style="text-align: center;"><%# Eval("deducted_CGST_TAX") %></td>
                            <td style="text-align: center;"><%# Eval("deducted_SGST_TAX") %>
                           <%# Eval("deducted_UGST_TAX") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN of deductee</th>
                                <th style="vertical-align:bottom;" rowspan="2">Amount paid to deductee on which tax isdeducted</th>
                                <th style="text-align: center;" colspan="3">Amount of tax deducted at source</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                             
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

                <h3 class="box-title">4. Amendments to details of tax deducted at source in respect of any earlier tax period</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR7_4" runat="server">
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
                            <td><%# Eval("MONTH") %></td>
                            <td><%# Eval("GSTIN_NO") %></td>
                            <td><%# Eval("ORG_AMT_PAID_DEDUCTED_TAX") %></td>
                            <td><%# Eval("REV_GSTIN_NO") %></td>
                            <td><%# Eval("REV_AMT_PAID_DEDUCTED_TAX") %> </td>
                            <td><%# Eval("REV_DEDUCTED_IGST_TAX") %></td>
                            <td><%# Eval("REV_DEDUCTED_CGST_TAX") %></td>
                            <td><%# Eval("REV_DEDUCTED_SGST_TAX") %>
                                <%# Eval("REV_DEDUCTED_UGST_TAX")%></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="3">#</th>
                                <th style="text-align:center;" colspan="3">Original details</th>
                                <th style="text-align: center;" colspan="8">Revised details</th>
                                </tr>
                            <tr>
                                 <th style="vertical-align: bottom; text-align: center;" rowspan="2">Month</th>
                                 <th style="vertical-align: bottom; text-align: center;" rowspan="2">GSTIN of deductee</th>
                                 <th style="vertical-align: bottom; text-align: center;" rowspan="2">Amount paid to deductee on which tax is deducted</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">GSTIN of deductee</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Amount paid to deductee on which tax is deducted</th>
                                <th style="text-align: center;" colspan="3">Amount of tax deducted at source</th>
                            </tr>
                            <tr>
                               
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br /> (<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/ UTGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
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

                <h3 class="box-title">5. Tax deduction at source and paid</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR7_5A" runat="server">
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
                            <td><%# Eval("DESCRIPTION") %></td>
                            <td><%# Eval("TAX_AMOUNT") %></td>
                            <td><%# Eval("AMOUNT_PAID") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">a.&nbsp; Integrated Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Amount of tax deducted </th>
                                <th>Amount paid</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                  <asp:ListView ID="lv_GSTR7_5B" runat="server">
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
                            <td><%# Eval("DESCRIPTION") %></td>
                            <td><%# Eval("TAX_AMOUNT") %></td>
                            <td><%# Eval("AMOUNT_PAID") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">b.&nbsp;Central Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Amount of tax deducted </th>
                                <th>Amount paid</th>
                                 <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR7_5C" runat="server">
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
                            <td><%# Eval("DESCRIPTION") %></td>
                            <td><%# Eval("TAX_AMOUNT") %></td>
                            <td><%# Eval("AMOUNT_PAID") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">c.&nbsp;State/UT Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Amount of tax deducted </th>
                                <th>Amount paid</th>
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

                <h3 class="box-title">6. Interest, late Fee payable and paid</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR7_6" runat="server">
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
                            <td><%# Eval("DESCRIPTION") %></td>
                            <td><%# Eval("PAYBLE_AMOUNT") %></td>
                            <td><%# Eval("AMOUNT_PAID") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <h4>(I) Interest on account of TDS in respect of</h4>
                                <th colspan="4">a. Integrated tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Amount payable </th>
                                <th>Amount paid</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR7_6B" runat="server">
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
                                <th colspan="4">b. Central Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Amount payable </th>
                                <th>Amount paid</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR7_6C" runat="server">
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
                                <th colspan="4">c. State/UT Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Amount payable </th>
                                <th>Amount paid</th>
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

                <h3 class="box-title">7. Refund claimed from electronic cash ledger</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR7_7A" runat="server">
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
                            <td><%# Eval("DESCRIPTION") %></td>
                            <td><%# Eval("TAX") %></td>
                            <td><%# Eval("INTEREST") %></td>
                            <td><%# Eval("PENALTY") %></td>
                            <td><%# Eval("FEE") %> </td>
                            <td><%# Eval("OTHER") %></td>
                            <td><%# Eval("DEBIT_ENTRY_NO") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">a. &nbsp; Integrated Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax </th>
                                <th>Interest</th>
                                <th>Penalty</th>
                                <th>Fee</th>
                                <th>Other</th>
                                 <th>Debit Entry Nos.</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR7_7B" runat="server">
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
                            <td><%# Eval("DESCRIPTION") %></td>
                            <td><%# Eval("TAX") %></td>
                            <td><%# Eval("INTEREST") %></td>
                            <td><%# Eval("PENALTY") %></td>
                            <td><%# Eval("FEE") %> </td>
                            <td><%# Eval("OTHER") %></td>
                            <td><%# Eval("DEBIT_ENTRY_NO") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">b. &nbsp; Central Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax </th>
                                <th>Interest</th>
                                <th>Penalty</th>
                                <th>Fee</th>
                                <th>Other</th>
                                 <th>Debit Entry Nos.</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR7_7C" runat="server">
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
                             <td><%# Eval("DESCRIPTION") %></td>
                            <td><%# Eval("TAX") %></td>
                            <td><%# Eval("INTEREST") %></td>
                            <td><%# Eval("PENALTY") %></td>
                            <td><%# Eval("FEE") %> </td>
                            <td><%# Eval("OTHER") %></td>
                            <td><%# Eval("DEBIT_ENTRY_NO") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">c. &nbsp; State/UT Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax </th>
                                <th>Interest</th>
                                <th>Penalty</th>
                                <th>Fee</th>
                                <th>Other</th>
                                 <th>Debit Entry Nos.</th>
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
                <h3 class="box-title">8. Debit entries in electronic cash ledger for TDS/interest payment [to be populated after payment of tax and submissions of return]</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR7_8A" runat="server">
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
                            <td><%# Eval("DESCRIPTION") %></td>
                            <td><%# Eval("PAID_TAX") %></td>
                            <td><%# Eval("INTEREST") %></td>
                            <td><%# Eval("LATE_FEE") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                             <tr>
                                <th colspan="5">a. Integrated Tax</th>
                            </tr>
                            <tr>
                               <th>#</th>
                                <th>Description</th>
                                <th>Tax paid in cash </th>
                                <th>Interest</th>
                                <th>Late Fee</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                            
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR7_8B" runat="server">
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
                            <td><%# Eval("DESCRIPTION") %></td>
                            <td><%# Eval("PAID_TAX") %></td>
                            <td><%# Eval("INTEREST") %></td>
                            <td><%# Eval("LATE_FEE") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="5">b. Central Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="text-align: center;">Tax paid in cash </th>
                                <th style="vertical-align: bottom;">Interest</th>
                                <th style="vertical-align: bottom;">Late Fee</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                            
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR7_8C" runat="server">
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
                            <td><%# Eval("DESCRIPTION") %></td>
                            <td><%# Eval("PAID_TAX") %></td>
                            <td><%# Eval("INTEREST") %></td>
                            <td><%# Eval("LATE_FEE") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="5">c. State/UT Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="text-align: center;">Tax paid in cash </th>
                                <th style="vertical-align: bottom;">Interest</th>
                                <th style="vertical-align: bottom;">Late Fee</th>
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
