<%@ Page Title="" Language="C#" MasterPageFile="../User.Master" AutoEventWireup="true" CodeBehind="GSTR6.aspx.cs" Inherits="UserInterface.GSTR6" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content-header">
        <h1>GSTR6
        <small>RETURN FOR INPUT SERVICE DISTRIBUTOR</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">Gstr6</li>
        </ol>

         </div>
        <div class="content">
            <%--<div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">GSTR6</h3>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel-heading">
                            Taxpayer Details
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-4">
                                <asp:Label ID="lblGSTIN" runat="server">test</asp:Label>
                            </div>
                            <div class="col-lg-4">
                                <asp:Label ID="lblregisteredName" runat="server">test1</asp:Label>
                            </div>
                            <div class="col-lg-4">
                                <asp:Label ID="lblPeriod" runat="server">test2</asp:Label>
                            </div>
                            <asp:Label ID="lblGSTINVal" runat="server" Style="display: none;">test</asp:Label>
                        </div>
                    </div>
                </div>

                
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label>
                            <asp:TextBox ID="txtFromDate" runat="server" class="form-control" Width="100px" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label3" runat="server" Text="To Date"></asp:Label>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Width="100px" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

            </div>--%>
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
                <asp:ListView ID="lv_GSTR6_3" runat="server">
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
                            <td style="text-align: center;"><%#  DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString())%></td>
                            <td style="text-align: center;"><%# Eval("TOTALAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
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

            <%--<div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">4. Total ITC/Eligible ITC/Ineligible ITC to be distributed for tax period (From Table No. 3)</h3>
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
                <asp:ListView ID="lvGSTR6_4A" runat="server">
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
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                           <tr>
                                <th colspan="12">4A.&nbsp;Total ITC available for distribution</th>
                               
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center;" rowspan="2">Description</th>
                                <th style="text-align: center;" colspan="3">IGST</th>
                                <th style="text-align: center;" rowspan="2">CGST</th>
                                <th style="text-align: center;" rowspan="2">SGST/UTGST</th>
                                <th style="text-align: center;" colspan="4">CESS</th>
                                 <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                            
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
        </div>--%>

            <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">4. Total ITC/Eligible ITC/Ineligible ITC to be distributed for tax period (From Table No. 3)</h3>
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
                <asp:ListView ID="lv_GSTR6_4A" runat="server">
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
                             <td style="text-align: center;"><%# Eval("Description") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT")%></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT")%><%# Eval("UGSTAMT")%></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                           <tr>
                                <th colspan="12">4A.&nbsp;Total ITC available for distribution</th>
                               
                            </tr>
                            <tr>
                                <th>#</th>
                                <th style="text-align: center;" >Description</th>
                                <th style="text-align: center;" >IGST</th>
                                <th style="text-align: center;" >CGST</th>
                                <th style="text-align: center;" >SGST/UTGST</th>
                                <th style="text-align: center;" >CESS</th>
                                 <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                            
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR6_4B" runat="server">
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
                            <td style="text-align: center;"><%# Eval("Description") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT")%></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT")%><%# Eval("UGSTAMT")%></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                           <tr>
                                <th colspan="12">4B.&nbsp;Amount of eligible ITC</th>
                               
                            </tr>
                            <tr>
                                <th>#</th>
                                <th style="text-align: center;" >Description</th>
                                <th style="text-align: center;" >IGST</th>
                                <th style="text-align: center;" >CGST</th>
                                <th style="text-align: center;" >SGST/UTGST</th>
                                <th style="text-align: center;" >CESS</th>
                                 <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                            
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR6_4C" runat="server">
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
                            <td style="text-align: center;"><%# Eval("Description") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT")%></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT")%><%# Eval("UGSTAMT")%></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                           <tr>
                                <th colspan="12">4C.&nbsp;Amount of ineligible ITC</th>
                               
                            </tr>
                            <tr>
                                <th>#</th>
                                <th style="text-align: center;">Description</th>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST</th>
                                <th style="text-align: center;">SGST/UTGST</th>
                                <th style="text-align: center;">CESS</th>
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
                <h3 class="box-title">5. Distribution of input tax credit reported in Table 4</h3>
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
                <asp:ListView ID="lv_GSTR6_5A" runat="server">
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
                            <td style="text-align: center;"><%# Eval("GSTIN_NO") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_NO") %></td>
                            <td style="text-align: center;"><%# Eval("inv_Date")%></td>
                            <td style="text-align: center;"><%# Eval("ITC_ISD_IGST") %></td>
                            <td style="text-align: center;"><%# Eval("ITC_ISD_CGST") %></td>
                            <td style="text-align: center;"><%# Eval("ITC_ISD_SGST")%><%# Eval("ITC_ISD_UTGST")%></td>
                            <td style="text-align: center;"><%# Eval("ITC_ISD_Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                           <tr>
                                <th colspan="12">5A.&nbsp;Distribution of the amount of eligible ITC</th>
                               
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center;" rowspan="2">GSTIN of recipient/State, if recipient is unregistered</th>
                                <th style="text-align: center;" colspan="2">ISD invoice</th>
                                <th style="text-align: center;" colspan="4">Distribution of ITC by ISD</th>
                                </tr>
                            <tr>
                                <th>No.</th>
                                <th>Date</th>
                                <th>IGST</th>
                                <th>CGST</th>
                                <th>SGST / UTGST</th>
                                <th>Cess</th>
                                 <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                            
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR6_5B" runat="server">
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
                           <td style="text-align: center;"><%# Eval("GSTIN_NO") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_NO") %></td>
                            <td style="text-align: center;"><%# Eval("Date")%></td>
                            <td style="text-align: center;"><%# Eval("ITC_ISD_IGST") %></td>
                            <td style="text-align: center;"><%# Eval("ITC_ISD_CGST") %></td>
                            <td style="text-align: center;"><%# Eval("ITC_ISD_SGST")%><%# Eval("ITC_ISD_UTGST")%></td>
                            <td style="text-align: center;"><%# Eval("ITC_ISD_Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                           <tr>
                                <th colspan="12">5B.&nbsp; Distribution of the amount of ineligible ITC</th>
                               
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center;" rowspan="2">GSTIN of recipient/State, if recipient is unregistered</th>
                                <th style="text-align: center;" colspan="2">ISD invoice</th>
                                <th style="text-align: center;" colspan="4">Distribution of ITC by ISD</th>
                                 </tr>
                            <tr>
                                <th>No.</th>
                                <th>Date</th>
                                <th>IGST</th>
                                <th>CGST</th>
                                <th>SGST / UTGST</th>
                                <th>Cess</th>
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
                <h3 class="box-title">6. Amendments in information furnished in earlier returns in Table No. 3</h3>
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
                <asp:ListView ID="lv_GSTR6_6A" runat="server">
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
                            <td style="text-align: center;"><%#  DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString())%></td>
                            <td style="text-align: center;"><%# Eval("GSTNNO") %></td>
                            <td style="text-align: center;"><%# Eval("INVOICENO") %></td>
                            <td style="text-align: center;"><%#  DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString())%></td>
                            <td style="text-align: center;"><%# Eval("TOTALAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                           <tr>
                                <th colspan="12">6A.&nbsp;Information furnished in Table 3 in an earlier period was incorrect</th>
                               
                            </tr>
                            <tr>
                                <th rowspan="3">#</th>
                                <th style="text-align: center;"colspan="3">Original details</th>
                                <th style="text-align: center;" colspan="10" >Revised details</th>
                            </tr>
                            <tr>
                                  <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">No.</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Date</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="3">Invoice/debit note/credit note details</th>
                                <th rowspan="2" style="text-align: center;">Rate</th>
                                <th rowspan="2" style="text-align: center;">Taxable value</th>
                                <th colspan="4" style="text-align: center;">Amount of Tax</th>
                                </tr>
                            <tr>
                                 <th>No</th>
                                <th>Date</th>
                                <th>Value</th>
                                <th>IGST</th>
                                <th>CGST</th>
                                <th>SGST/UTGST</th>
                                <th>CESS</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                             
                            
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR6_6B" runat="server">
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
                            <td style="text-align: center;"><%#  DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString())%></td>
                            <td style="text-align: center;"><%# Eval("GSTNNO") %></td>
                            <td style="text-align: center;"><%# Eval("INVOICENO") %></td>
                            <td style="text-align: center;"><%#  DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString())%></td>
                            <td style="text-align: center;"><%# Eval("TOTALAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                           <tr>
                                <th colspan="12">6B.&nbsp; Debit Notes/Credit Notes received [Original]</th>
                               
                            </tr>
                            <tr>
                                <th rowspan="3">#</th>
                                <th style="text-align: center;"colspan="3">Original details</th>
                                <th style="text-align: center;" colspan="10" >Revised details</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">No.</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Date</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="3">Invoice/debit note/credit note details</th>
                                <th rowspan="2">Rate</th>
                                <th rowspan="2">Taxable value</th>
                                <th colspan="4">Amount of Tax</th>
                                </tr>
                            <tr>
                                 <th>No</th>
                                <th>Date</th>
                                <th>Value</th>
                                <th>IGST</th>
                                <th>CGST</th>
                                <th>SGST/UTGST</th>
                                <th>CESS</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                             
                            
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR6_6C" runat="server">
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
                            <td style="text-align: center;"><%#  DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString())%></td>
                            <td style="text-align: center;"><%# Eval("GSTNNO") %></td>
                            <td style="text-align: center;"><%# Eval("INVOICENO") %></td>
                            <td style="text-align: center;"><%#  DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString())%></td>
                            <td style="text-align: center;"><%# Eval("TOTALAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                           <tr>
                                <th colspan="12">6C.&nbsp; Debit Notes/Credit Notes [Amendments]</th>
                               
                            </tr>
                            <tr>
                                <th rowspan="3">#</th>
                                <th style="text-align: center;"colspan="3">Original details</th>
                                <th style="text-align: center;" colspan="10" >Revised details</th>
                            </tr>
                            <tr>
                                  <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">No.</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Date</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="3">Invoice/debit note/credit note details</th>
                                <th rowspan="2">Rate</th>
                                <th rowspan="2">Taxable value</th>
                                <th colspan="4">Amount of Tax</th>
                                </tr>
                            <tr>
                                 <th>No</th>
                                <th>Date</th>
                                <th>Value</th>
                                <th>IGST</th>
                                <th>CGST</th>
                                <th>SGST/UTGST</th>
                                <th>CESS</th>
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
                <h3 class="box-title">7. Input tax credit mis-matches and reclaims to be distributed in the tax period</h3>
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
                <asp:ListView ID="lv_GSTR6_7A" runat="server">
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
                            <td style="text-align: center;"><%# Eval("Description") %></td>
                            <td style="text-align: center;"><%# Eval("IGST")%></td>
                            <td style="text-align: center;"><%# Eval("CGST") %></td>
                            <td style="text-align: center;"><%# Eval("SGST")%><%# Eval("UTGST")%></td>
                            <td style="text-align: center;"><%# Eval("Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th colspan="5">7A. Input tax credit mismatch</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center;">Description</th>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST</th>
                                <th style="text-align: center;">SGST/UTGST</th>
                                <th style="text-align: center;">Cess</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR6_7B" runat="server">
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
                            <td style="text-align: center;"><%# Eval("Description") %></td>
                            <td style="text-align: center;"><%# Eval("IGST")%></td>
                            <td style="text-align: center;"><%# Eval("CGST") %></td>
                            <td style="text-align: center;"><%# Eval("SGST")%><%# Eval("UGST")%></td>
                            <td style="text-align: center;"><%# Eval("CESS") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th colspan="5">7B. Input tax credit reclaimed on rectification of mismatch</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center;">Description</th>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST</th>
                                <th style="text-align: center;">SGST/UTGST</th>
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
                <h3 class="box-title">8. Distribution of input tax credit reported in Table No. 6 and 7 (plus / minus)</h3>
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
                <asp:ListView ID="lv_GSTR6_8A" runat="server">
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
                            <td style="text-align: center;"><%# Eval("GSTIN_NO") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_CRDIT_NO") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_CRDIT_Date") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_INVOICE_NO") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_INVOICE_Date") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_TAX_IGST") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_TAX_CGST") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_TAX_SGST") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_TAX_Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th colspan="5">8A. Distribution of the amount of eligible ITC</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center;" rowspan="2">GSTIN of recipient</th>
                                <th style="text-align: center;" colspan="2">ISD credit no.</th>
                                <th style="text-align: center;" colspan="2">ISD invoice</th>
                                <th style="text-align: center;" colspan="4">Input tax distribution by ISD</th>
                                </tr>
                            <tr>
                                  <th>No.</th>
                                <th>Date</th>
                                <th>No.</th>
                                 <th>Date</th>
                                <th>IGST</th>
                                 <th>CGST</th>
                                 <th>SGST</th>
                                <th>Cess</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR6_8B" runat="server">
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
                            <td style="text-align: center;"><%# Eval("GSTIN_NO") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_CRDIT_NO") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_CRDIT_Date") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_INVOICE_NO") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_INVOICE_Date") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_TAX_IGST") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_TAX_CGST") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_TAX_SGST") %></td>
                            <td style="text-align: center;"><%# Eval("ISD_TAX_Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th colspan="5">8B. Distribution of the amount of ineligible ITC</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center;" rowspan="2">GSTIN of recipient</th>
                                <th style="text-align: center;" colspan="2">ISD credit no.</th>
                                <th style="text-align: center;" colspan="2">ISD invoice</th>
                                <th style="text-align: center;" colspan="4">Input tax distribution by ISD</th>
                                </tr>
                            <tr>
                                  <th>No.</th>
                                <th>Date</th>
                                <th>No.</th>
                                 <th>Date</th>
                                <th>IGST</th>
                                 <th>CGST</th>
                                 <th>SGST</th>
                                <th>Cess</th>
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
                <h3 class="box-title">9. Redistribution of ITC distributed to a wrong recipient (plus / minus)</h3>
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
                <asp:ListView ID="lv_GSTR6_9A" runat="server">
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
                            <td style="text-align: center;"><%# Eval("TAX_CREDIT_GSTIN_ORIGINAL_RECIPIENT") %></td>
                            <td style="text-align: center;"><%# Eval("TAX_CREDIT_ISD_INVOICE_NO") %></td>
                            <td style="text-align: center;"><%# Eval("TAX_CREDIT_ISD_INVOICE_Date") %></td>
                            <td style="text-align: center;"><%# Eval("TAX_CREDIT_ISD_credit_NO") %></td>
                            <td style="text-align: center;"><%# Eval("TAX_CREDIT_ISD_credit_Date") %></td>
                            <td style="text-align: center;"><%# Eval("Correct_RECIPIENT_GSTIN_NEW_RECIPIENT") %></td>
                            <td style="text-align: center;"><%# Eval("Correct_RECIPIENT_ISD_INVOICE_No") %></td>
                            <td style="text-align: center;"><%# Eval("Correct_RECIPIENT_ISD_INVOICE_Date") %></td>
                            <td style="text-align: center;"><%# Eval("CREDIT_REDISTRIBUTED_IGST") %></td>
                            <td style="text-align: center;"><%# Eval("CREDIT_REDISTRIBUTED_CGST") %></td>
                            <td style="text-align: center;"><%# Eval("CREDIT_REDISTRIBUTED_SGST") %></td>
                            <td style="text-align: center;"><%# Eval("CREDIT_REDISTRIBUTED_CESS") %></td>
                            
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th colspan="13">9A.&nbsp; Distribution of the amount of eligible ITC</th>
                            </tr>
                            <tr>
                                <th rowspan="3">#</th>
                                <th style="text-align: center;" colspan="3">Original input tax credit distribution</th>
                                <th style="text-align: center;" colspan="3">Re-distribution of input tax credit to the correct recipient</th>
                               </tr>
                            <tr>
                                <th style="text-align: center;" rowspan="2">GSTIN of original recipient</th>
                                <th style="text-align: center;" colspan="2">ISD invoice detail</th>
                                  <th style="text-align: center;" colspan="2">ISD credit note</th>
                                  <th style="text-align: center;"rowspan="2">GSTIN of new recipient</th>
                                <th style="text-align: center;"colspan="2">ISD invoice</th>
                                 <th style="text-align: center;" colspan="4">Input tax credit redistributed</th>
                                </tr>
                            <tr>
                                <th>No.</th>
                                <th>Date</th>
                                <th>No.</th>
                                <th>Date</th>
                                <th>No.</th>
                                <th>Date</th>
                                <th>IGST</th>
                                <th>CGST</th>
                                <th>SGST</th>
                                <th>Cess</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR6_9B" runat="server">
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
                            <td style="text-align: center;"><%# Eval("TAX_CREDIT_GSTIN_ORIGINAL_RECIPIENT") %></td>
                            <td style="text-align: center;"><%# Eval("TAX_CREDIT_ISD_INVOICE_NO") %></td>
                            <td style="text-align: center;"><%# Eval("TAX_CREDIT_ISD_INVOICE_Date") %></td>
                            <td style="text-align: center;"><%# Eval("TAX_CREDIT_ISD_credit_NO") %></td>
                            <td style="text-align: center;"><%# Eval("TAX_CREDIT_ISD_credit_Date") %></td>
                            <td style="text-align: center;"><%# Eval("Correct_RECIPIENT_GSTIN_NEW_RECIPIENT") %></td>
                            <td style="text-align: center;"><%# Eval("Correct_RECIPIENT_ISD_INVOICE_No") %></td>
                            <td style="text-align: center;"><%# Eval("Correct_RECIPIENT_ISD_INVOICE_Date") %></td>
                            <td style="text-align: center;"><%# Eval("CREDIT_REDISTRIBUTED_IGST") %></td>
                            <td style="text-align: center;"><%# Eval("CREDIT_REDISTRIBUTED_CGST") %></td>
                            <td style="text-align: center;"><%# Eval("CREDIT_REDISTRIBUTED_SGST") %></td>
                            <td style="text-align: center;"><%# Eval("CREDIT_REDISTRIBUTED_CESS") %></td>
                            
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th colspan="13">9B.&nbsp;Distribution of the amount of ineligible ITC</th>
                            </tr>
                            <tr>
                                <th rowspan="3">#</th>
                                <th style="text-align: center;" colspan="3">Original input tax credit distribution</th>
                                <th style="text-align: center;" colspan="3">Re-distribution of input tax credit to the correct recipient</th>
                               </tr>
                            <tr>
                                <th style="text-align: center;" rowspan="2">GSTIN of original recipient</th>
                                <th style="text-align: center;" colspan="2">ISD invoice detail</th>
                                  <th style="text-align: center;" colspan="2">ISD credit note</th>
                                  <th style="text-align: center;"rowspan="2">GSTIN of new recipient</th>
                                <th style="text-align: center;"colspan="2">ISD invoice</th>
                                 <th style="text-align: center;" colspan="4">Input tax credit redistributed</th>
                                </tr>
                            <tr>
                                <th>No.</th>
                                <th>Date</th>
                                <th>No.</th>
                                <th>Date</th>
                                <th>No.</th>
                                <th>Date</th>
                                <th>IGST</th>
                                <th>CGST</th>
                                <th>SGST</th>
                                <th>Cess</th>
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
                <h3 class="box-title">10. Late Fee</h3>
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
                <asp:ListView ID="lv_GSTR6_10" runat="server">
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
                            <td style="text-align: center;"><%# Eval("AMOUNT_LATE_FEE") %></td>
                            <td style="text-align: center;"><%# Eval("CGST") %></td>
                            <td style="text-align: center;"><%# Eval("SGST")%><%# Eval("UTGST")%></td>
                            <td style="text-align: center;"><%# Eval("Debit_ENTRY_NO") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center;">On account of</th>
                                <th style="text-align: center;">CGST</th>
                                <th style="text-align: center;">SGST/UTGST</th>
                                <th style="text-align: center;">Debit Entry No.</th>
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
                <h3 class="box-title">11. Refund claimed from electronic cash ledger</h3>
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
                <asp:ListView ID="lv_GSTR6_11_A" runat="server">
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
                            <td style="text-align: center;"><%# Eval("DESCRIPTION") %></td>
                            <td style="text-align: center;"><%# Eval("FEE") %></td>
                            <td style="text-align: center;"><%# Eval("other") %></td>
                            <td style="text-align: center;"><%# Eval("Debit_Entry_NO") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th>11 A.Central Tax</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center;">Description</th>
                                <th style="text-align: center;">Fee</th>
                                <th style="text-align: center;">Other</th>
                                <th style="text-align: center;">Debit Entry No.</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR6_11_B" runat="server">
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
                            <td style="text-align: center;"><%# Eval("DESCRIPTION") %></td>
                            <td style="text-align: center;"><%# Eval("FEE") %></td>
                            <td style="text-align: center;"><%# Eval("other") %></td>
                            <td style="text-align: center;"><%# Eval("Debit_Entry_NO") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th>11B. State/UT Tax</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center;">Description</th>
                                <th style="text-align: center;">Fee</th>
                                <th style="text-align: center;">Other</th>
                                <th style="text-align: center;">Debit Entry No.</th>
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
