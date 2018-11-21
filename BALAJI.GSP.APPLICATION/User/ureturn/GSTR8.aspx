<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="GSTR8.aspx.cs" Inherits="UserInterface.GSTR8" %>

<%@ Register Src="~/UC/uc_GSTR_Taxpayer.ascx" TagPrefix="uc1" TagName="uc_GSTR_Taxpayer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="content-header">
        <h1>GSTR8
        <small> Statement for tax collection at source</small> </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">GSTR8</li>
        </ol>
    </div>
     <%--<div class="row">
        <div class="col-lg-12">
            <h2 class="page-header"></h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>--%>
    

    
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
                         &nbsp; &nbsp;(b) Trade name, if any:
                    </label>
                    <asp:Label ID="lblTurnoverYear" runat="server" Style="display: none;"></asp:Label>
                </div>
            </div>
        </div>
            </div>
            </div>     
        
    <div class="box box-danger">
            <div class="box-header with-border">

                <h3 class="box-title">3. Details of supplies made through e-commerce operator</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR8_3A" runat="server">
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
                            <td><%# Eval("TCS_SUPPLIES_MADE") %></td>
                            <td style="text-align: center;"><%# Eval("TCS_SUPPLIES_RETURN") %></td>
                            <td style="text-align: center;"><%# Eval("TCS_NET_AMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("TAX_COLLECTED_IGST") %> </td>
                            <td style="text-align: center;"><%# Eval("TAX_COLLECTED_CGST")%></td>
                            <td style="text-align: center;"><%# Eval("TAX_COLLECTED_SGST")%><%# Eval(" TAX_COLLECTED_UTGST") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">3A.&nbsp; Supplies made to registered persons</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center;" colspan="3">Details of supplies made which attract TCS</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="3">Amount of tax collected at source</th>
                            </tr>
                            <tr>
                                <th>Gross value of supplies made</th>
                                <th>Value of supplies returned</th>
                                <th style="text-align: center;">Net amount liable for TCS(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/ UTGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                   <asp:ListView ID="lv_GSTR8_3B" runat="server">
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
                            <td><%# Eval("TCS_SUPPLIES_MADE") %></td>
                            <td style="text-align: center;"><%# Eval("TCS_SUPPLIES_RETURN") %></td>
                            <td style="text-align: center;"><%# Eval("TCS_NET_AMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("TAX_COLLECTED_IGST") %> </td>
                            <td style="text-align: center;"><%# Eval("TAX_COLLECTED_CGST")%></td>
                            <td style="text-align: center;"><%# Eval("TAX_COLLECTED_SGST")%><%# Eval(" TAX_COLLECTED_UTGST") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">3B.&nbsp;Supplies made to unregistered persons</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center;" colspan="3">Details of supplies made which attract TCS</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="3">Amount of tax collected at source</th>
                            </tr>
                            <tr>
                                <th>Gross value of supplies made</th>
                                <th>Value of supplies returned</th>
                                <th style="text-align: center;">Net amount liable for TCS(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">4. Amendments to details of supplies in respect of any earlier statement</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR8_4A" runat="server">
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
                            <td><%# Eval("ORGINAL_DETAIL_MONTHS") %></td>
                            <td><%# Eval("ORGINAL_DETAIL_GSTIN_NO") %></td>
                            <td><%# Eval("REVISED_DETAILS_GSTIN_NO") %></td>
                            <td><%# Eval("REVISED_DETAILS_TCS_GROSS_VALUE") %></td>
                            <td><%# Eval("REVISED_DETAILS_TCS_SUPPLY_RETURN") %></td>
                            <td><%# Eval("REVISED_DETAILS_TCS_AMOUNT_TCS") %> </td>
                            <td><%# Eval("TAX_IGST")%></td>
                            <td><%# Eval("TAX_CGST")%></td>
                            <td><%# Eval("TAX_SGST")%><%# Eval("TAX_UTGST")%></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="10">4A.&nbsp; Supplies made to registered persons</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="3">#</th>
                                <th style="vertical-align: bottom;" colspan="2">Original details</th>
                                <th style="text-align: center;" colspan="7">Revised details </th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">Month</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN of supplier</th>
                                <th  rowspan="2" style="text-align: center;">GSTIN of supplier(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th colspan="3">Details of supplies made which attract TCS<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th colspan="3">Amount of tax collected at source<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                </tr>
                            <tr>
                                <th>Gross value of supplies made</th>
                                <th>Value of supply returned</th>
                                <th>Net amount liable for TCS</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/ UTGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR8_4B" runat="server">
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
                            <td><%# Eval("ORGINAL_DETAIL_MONTHS") %></td>
                            <td><%# Eval("ORGINAL_DETAIL_GSTIN_NO") %></td>
                            <td><%# Eval("REVISED_DETAILS_GSTIN_NO") %></td>
                            <td><%# Eval("REVISED_DETAILS_TCS_GROSS_VALUE") %></td>
                            <td><%# Eval("REVISED_DETAILS_TCS_SUPPLY_RETURN") %></td>
                            <td><%# Eval("REVISED_DETAILS_TCS_AMOUNT_TCS") %> </td>
                            <td><%# Eval("TAX_IGST")%></td>
                            <td><%# Eval("TAX_CGST")%></td>
                            <td><%# Eval("TAX_SGST")%><%# Eval("TAX_UTGST")%></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="10">4B.&nbsp;Supplies made to unregistered persons</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="3">#</th>
                                <th style="vertical-align: bottom;" colspan="2">Original details</th>
                                <th style="text-align: center;" colspan="7">Revised details </th>
                            </tr>
                            <tr>
                                <th rowspan="2">Month</th>
                                <th rowspan="2">GSTIN of supplier</th>
                                <th  rowspan="2" style="text-align: center;">GSTIN of supplier(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th colspan="3">Details of supplies made which attract TCS<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th colspan="3">Amount of tax collected at source<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                </tr>
                            <tr>
                                <th>Gross value of supplies made</th>
                                <th>Value of supply returned</th>
                                <th>Net amount liable for TCS</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">5. Details of interest</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR8_5" runat="server">
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
                            <td><%# Eval("ACCOUNT_LATE_PAYMENT") %></td>
                            <td><%# Eval("DEFAULT_AMOUNT") %></td>
                            <td><%# Eval("INTEREST_IGST") %></td>
                            <td><%# Eval("INTEREST_CGST") %></td>
                            <td><%# Eval("INTEREST_SGST") %><%# Eval("INTEREST_UTGST") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">On account of</th>
                                <th rowspan="2">Amount in default</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="3">Amount of interest</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">6. Tax payable and paid</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR8_6A" runat="server">
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
                            <td><%# Eval("TAX_PAYABLE") %></td>
                            <td><%# Eval("AMOUNT_PAID") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">a.&nbsp; Integrated Tax </th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax payable</th>
                                <th>Amount paid</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR8_6B" runat="server">
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
                            <td><%# Eval("TAX_PAYABLE") %></td>
                            <td><%# Eval("AMOUNT_PAID") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">b.&nbsp; Central Tax </th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax payable</th>
                                <th>Amount paid</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR8_6C" runat="server">
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
                            <td><%# Eval("TAX_PAYABLE") %></td>
                            <td><%# Eval("AMOUNT_PAID") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">c.&nbsp; State / UT Tax </th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax payable</th>
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

                <h3 class="box-title">7. Interest payable and paid</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR8_7A" runat="server">
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
                            <td><%# Eval("INTERST_PAYABLE") %></td>
                            <td><%# Eval("AMOUNT_PAID") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">a. Integrated tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Amount of interest payable</th>
                                <th>Amount paid</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR8_7B" runat="server">
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
                            <td><%# Eval("INTEREST_PAYABLE") %></td>
                            <td><%# Eval("AMOUNT_PAID") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">7B.Central Tax</th>
                            </tr>
                            <table class="table table-bordered table-condensed">
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Amount of interest payable</th>
                                <th>Amount paid</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR8_7C" runat="server">
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
                            <td><%# Eval("INTEREST_PAYABLE") %></td>
                            <td><%# Eval("AMOUNT_PAID") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                            <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">7C.&nbsp;State/UT Tax</th>
                            </tr>
                            <table class="table table-bordered table-condensed">
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Amount of interest payable</th>
                                <th>Amount paid</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                        </table>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
              

            </div>
        </div>
    <div class="box box-danger">
            <div class="box-header with-border">

                <h3 class="box-title">8. Refund claimed from electronic cash ledger</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR8_8A" runat="server">
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
                            <td><%# Eval("INTERST") %></td>
                            <td><%# Eval("PENALTY") %></td>
                            <td><%# Eval("OTHER") %> </td>
                            <td><%# Eval("DR_ENTRY_NO")%></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">a.&nbsp;Integrated tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax</th>
                                <th>Interest</th>
                                <th>Penalty</th>
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

                <asp:ListView ID="lv_GSTR8_8B" runat="server">
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
                            <td><%# Eval("INTERST") %></td>
                            <td><%# Eval("PENALTY") %></td>
                            <td><%# Eval("OTHER") %> </td>
                            <td><%# Eval("DR_ENTRY_NO")%></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">b.&nbsp;Central Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax</th>
                                <th>Interest</th>
                                <th>Penalty</th>
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

                <asp:ListView ID="lv_GSTR8_8C" runat="server">
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
                            <td><%# Eval("INTERST") %></td>
                            <td><%# Eval("PENALTY") %></td>
                            <td><%# Eval("OTHER") %> </td>
                            <td><%# Eval("DR_ENTRY_NO")%></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">c.&nbsp;State/UT Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax</th>
                                <th>Interest</th>
                                <th>Penalty</th>
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

                <h3 class="box-title">9. Debit entries in cash ledger for TCS/interest payment [to be populated after payment of tax and submissions of return]</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR8_9A" runat="server">
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
                            <td><%# Eval("TAX_CASH") %></td>
                            <td><%# Eval("INTEREST") %></td>
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
                                <th>Tax paid in cash </th>
                                <th>Interest</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR8_9B" runat="server">
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
                            <td><%# Eval("TAX_CASH") %></td>
                            <td><%# Eval("INTEREST") %></td>
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
                                <th>Tax paid in cash </th>
                                <th>Interest</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR8_9C" runat="server">
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
                            <td><%# Eval("TAX_CASH") %></td>
                            <td><%# Eval("INTEREST") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="4">c. State / UT Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax paid in cash </th>
                                <th>Interest</th>
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
