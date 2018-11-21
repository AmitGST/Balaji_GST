<%@ Page Title="" Language="C#" MasterPageFile="../User.Master" AutoEventWireup="true" CodeBehind="GSTR6A.aspx.cs" Inherits="UserInterface.GSTR6A" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <%--<div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">GSTR6A-AUTO DRAFTED DETAILS</h2>
        </div>
         </div>--%>
		 <div class="content-header">
        <h1>GSTR6A
        <small>GSTR6A-Details of supplies auto-drafted from</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">Gstr6A</li>
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
                <h3 class="box-title">3. Input tax credit received for distribution</h3>
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
                <asp:ListView ID="lv_GSTR6A_3" runat="server">
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
                            <td style="text-align: center;"><%# Eval("GSTNNO") %></td>
                            <td style="text-align: center;"><%# Eval("INVOICENO") %></td>
                            <td style="text-align: center;"><%# Eval("INVOICEDATE") %></td>
                            <td style="text-align: center;"><%# Eval("TOTALAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT")%><%# Eval("UGSTAMT")%></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center;" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center;" colspan="3">Invoice Details</th>
                                <th style="text-align: center;" rowspan="2">Rate</th>
                                <th style="text-align: center;" rowspan="2">Taxable value</th>
                                <th style="text-align: center;" colspan="4">Amount of Tax</th>
                                
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">No.</th>
                                <th style="text-align: center; vertical-align:bottom">Date</th>
                                <th style="text-align: center; vertical-align:bottom">Value </th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">4. Debit / Credit notes (including amendments thereof) received during current tax period</h3>
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
                <asp:ListView ID="lv_GSTR6A_4" runat="server">
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
                            <td style="text-align: center;"><%# Eval("GSTNNO") %></td>
                            <td style="text-align: center;"><%# Eval("INVOICENO") %></td>
                            <td style="text-align: center;"><%# Eval("INVOICEDATE") %></td>
                            <td style="text-align: center;"><%# Eval("GSTNNO") %></td>
                            <td style="text-align: center;"><%# Eval("INVOICENO") %></td>
                            <td style="text-align: center;"><%# Eval("INVOICEDATE") %></td>
                            <td style="text-align: center;"><%# Eval("TOTALAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT")%><%# Eval("UGSTAMT")%></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th rowspan="3">#</th>
                                <th style="text-align: center;" colspan="3">Details of original document</th>
                                <th style="text-align: center;" colspan="11">Revised details of document or details of Debit / Credit Note</th>
                            </tr>
                            <tr>
                                <th rowspan="2" style="text-align: center; vertical-align:bottom">GSTIN of supplier</th>
                                <th rowspan="2" style="text-align: center; vertical-align:bottom">No.</th>
                                <th rowspan="2" style="text-align: center; vertical-align:bottom">Date </th>
                                 <th rowspan="2" style="text-align: center; vertical-align:bottom">GSTIN of supplier</th>
                                <th rowspan="2" style="text-align: center; vertical-align:bottom">No.</th>
                                <th  rowspan="2" style="text-align: center; vertical-align:bottom">Date </th>
                                <th  rowspan="2" style="text-align: center; vertical-align:bottom">Value </th>
                                <th  rowspan="2" style="text-align: center; vertical-align:bottom">Rate </th>
                                <th  rowspan="2" style="text-align: center; vertical-align:bottom">Taxable value<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                               <th colspan="4">Amount of tax</th>
                                 </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">IGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
