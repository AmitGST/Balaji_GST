<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/User.master" CodeBehind="GSTR2PreviewB2B.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.GSTR2PreviewB2B" %>
<%@ MasterType VirtualPath="~/User/User.master" %>
<%@ Register Src="~/UC/uc_GSTR_Taxpayer.ascx" TagPrefix="uc1" TagName="uc_GSTR_Taxpayer" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content-header">
        <h1>GSTR2
        <small>Inward supplies made by the taxpayer</small> </h1>
        <ol class="breadcrumb">


            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">GSTR2</li>
        </ol>
    </div>
    <!-- Main content -->
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Taxpayer Details</h3>
            </div>
            <div class="box-body">
                <uc1:uc_GSTR_Taxpayer runat="server" ID="uc_GSTR_Taxpayer" />
            </div>
            <div class="box-footer">
                <asp:LinkButton ID="lkvGSTR2" runat="server" CssClass="btn btn-success" OnClick="lkvGSTR2_Click"><i class="fa fa-cloud-download"></i> File GSTR - 2</asp:LinkButton>
            </div>
        </div>
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">3. Inward supplies received from a registered person other than the supplies attracting reverse charge</h3>
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
                <asp:ListView ID="lv_GSTR2_3" runat="server">
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
                            <td><%# Eval("GSTNNO") %></td>
                            <td><%# Eval("INVOICENO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                            <td><%# Eval("TOTALAMOUNT") %></td>
                            <td><%# Eval("RATE") %></td>
                            <td><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;">
                                  <%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %>
                            </td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                            <td><%# Eval("SERVICECAPITALGOODS") %></td>
                            <td style="text-align: center;"><%# Eval("ITCIGSTAMT").ToString()=="0.00"? " - " :Eval("ITCIGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("ITCCGSTAMT").ToString()=="0.00"? " - " :Eval("ITCCGSTAMT") %></td>
                            <td style="text-align: center;">
                                <%# Eval("ITCSGSTAMT").ToString()=="0.00"?(Eval("ITCUGSTAMT")=="0.00"?Eval("ITCUGSTAMT"):"-"):(Eval("ITCSGSTAMT")=="0.00"?"-":Eval("ITCSGSTAMT")) %></td>
                            <td  style="text-align: center;"><%# Eval("ITCCESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive table-bordered table-condensed">
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center;" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center;" colspan="3">Invoice Details</th>
                                <th style="text-align: center;" rowspan="2">Rate</th>
                                <th style="text-align: center;" rowspan="2">Taxable value</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                                <th style="text-align: center;" rowspan="2">Place of Supply</th>
                                <th rowspan="2">Whether input or input service/ Capital goods</th>
                                <th colspan="4">Amount of ITC available</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">No.</th>
                                <th style="text-align: center; vertical-align:bottom">Date</th>
                                <th style="text-align: center; vertical-align:bottom">Value </th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">4.&nbsp; Inward supplies on which tax is to be paid on reverse charge </h3>
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
                <asp:ListView ID="lv_GSTR2_4A" runat="server">
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
                            <td><%# Eval("GSTNNO") %></td>
                            <td><%# Eval("INVOICENO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                            <td><%# Eval("TOTALAMOUNT") %></td>
                            <td><%# Eval("RATE") %></td>
                            <td><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                               <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %> 
                                </td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                            <td><%# Eval("SERVICECAPITALGOODS") %></td>
                            <td style="text-align: center;"><%# Eval("ITCIGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("ITCCGSTAMT") %></td>
                            <td><%# Eval("ITCSGSTAMT") %><%#Eval("ITCUGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("ITCCESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-responsive table-condensed">
                            <tr>
                                <th colspan="12">4A.&nbsp;Inward supplies received from a registered supplier (attracting  reverse charge)</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center ; vertical-align:bottom" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">Amount</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Place of Supply</th>
                                <th rowspan="2">Whether input or input service/ Capital goods</th>
                                <th colspan="4">Amount of ITC available</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">No.</th>
                                <th style="text-align: center; vertical-align:bottom">Date</th>
                                <th style="text-align: center; vertical-align:bottom">Value </th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST <br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <asp:ListView ID="lv_GSTR2_4B" runat="server">
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
                            <td><%# Eval("GSTNNO") %></td>
                            <td><%# Eval("INVOICENO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                            <td><%# Eval("TOTALAMOUNT") %></td>
                            <td><%# Eval("RATE") %></td>
                            <td><%# Eval("TAXABLEAMOUNT") %></td>
                            <td><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT")  %></td>
                            <td><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %>
                                </td>
                            <td><%#  Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                            <td><%# Eval("SERVICECAPITALGOODS") %></td>
                            <td><%# Eval("ITCIGSTAMT") %></td>
                            <td><%# Eval("ITCCGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("ITCSGSTAMT").ToString()=="0.00"?(Eval("ITCUGSTAMT")=="0.00"?Eval("ITCUGSTAMT"):"-"):(Eval("ITCSGSTAMT")=="0.00"?"-":Eval("ITCSGSTAMT")) %></td>
                            <td><%# Eval("ITCCESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="11">4B.&nbsp;Inward supplies received from an unregistered supplier </th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">Amount</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Place of Supply</th>
                                <th rowspan="2">Whether input or input service/ Capital goods</th>
                                <th colspan="4">Amount of ITC available</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">No.</th>
                                <th style="text-align: center; vertical-align:bottom">Date</th>
                                <th style="text-align: center; vertical-align:bottom">Value </th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST <br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <asp:ListView ID="ListView4C" runat="server">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("GSTNNO") %></td>
                            <td><%# Eval("INVOICENO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                            <td><%# Eval("TOTALAMOUNT") %></td>
                            <td><%# Eval("RATE") %></td>
                            <td><%# Eval("TAXABLEAMOUNT") %></td>
                            <td><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                            <td><%# Eval("SERVICECAPITALGOODS") %></td>
                            <td><%# Eval("ITCIGSTAMT") %></td>
                            <td><%# Eval("ITCCGSTAMT") %></td>
                            <td><%# Eval("ITCSGSTAMT") %></td>
                            <td><%# Eval("ITCUGSTAMT") %></td>
                            <td><%# Eval("ITCCESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th>4C. Import of service</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">Amount</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Place of Supply</th>
                                <th rowspan="2">Whether input or input service/ Capital goods</th>
                                <th colspan="4">Amount of ITC available</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">No.</th>
                                <th style="text-align: center; vertical-align:bottom">Date</th>
                                <th style="text-align: center; vertical-align:bottom">Value </th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST <br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">5.&nbsp;Inputs/Capital goods received from Overseas or from SEZ units on a Bill of Entry  </h3>
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
                            <td><%# Eval("GSTNNO") %></td>
                            <td><%# Eval("INVOICENO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                            <td><%# Eval("TOTALAMOUNT") %></td>
                            <td><%# Eval("RATE") %></td>
                            <td><%# Eval("TAXABLEAMOUNT") %></td>
                            <td><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="12">5A.&nbsp;Imports</th>
                            </tr>
                            <tr>
                                <th style="vertical-align:bottom;">#</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">Amount</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Place of Supply</th>
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
                            <td><%# Eval("GSTNNO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                            <td><%# Eval("INVOICEDATE") %></td>
                            <td><%# Eval("TOTALAMOUNT") %></td>
                            <td><%# Eval("RATE") %></td>
                            <td><%# Eval("TAXABLEAMOUNT") %></td>
                            <td><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="12">5B.Received from SEZ</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">Amount</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Place of Supply</th>
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
                <h3 class="box-title">6.&nbsp;Amendments to details of inward supplies furnished in returns for earlier tax periods in Tables 3, 4 and 5
[including debit notes/credit notes issued and their subsequent amendments] </h3>
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
                <asp:ListView ID="lv_GSTR2_6A" runat="server">
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
                            <td><%# Eval("INVGSTN") %></td>
                            <td><%# Eval("INVNO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVCDATE").ToString())%></td>
                            <td><%# Eval("GSTNNO") %></td>
                            <td><%#  Eval("INVOICENO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                            <td><%# Eval("TOTALAMOUNT") %></td>
                            <td><%# Eval("RATE") %></td>
                            <td><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                            <td><%# Eval("SERVICECAPITALGOODS") %></td>
                            <td style="text-align: center;"><%# Eval("ITCIGSTAMT").ToString()=="0.00"? " - " :Eval("ITCIGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("ITCCGSTAMT").ToString()=="0.00"? " - " :Eval("ITCCGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("ITCSGSTAMT").ToString()=="0.00"?(Eval("ITCUGSTAMT")=="0.00"?Eval("ITCUGSTAMT"):"-"):(Eval("ITCSGSTAMT")=="0.00"?"-":Eval("ITCSGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("ITCCESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="20">6A.&nbsp;Supplies other than import of goods or goods received from SEZ [Information furnished in Table 3 and 4 of earlier returns]-If
details furnished earlier were incorrect</th>
                            </tr>
                            <tr>
                                <th style= "vertical-align:bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align:bottom;" colspan="3">Details of Original Invoice</th>
                                <th style="text-align: center; vertical-align:bottom;" colspan="4"> Revised details of Invoice</th>
                                <th style="text-align: center; vertical-align:bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align:bottom;" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align:bottom;" colspan="4">Amount</th>
                                <th style="text-align: center; vertical-align:bottom;" rowspan="2">Place of Supply</th>
                                <th style="text-align: center; vertical-align:bottom;" rowspan="2">Whetherinput orinputservice/Capitalgoods/Ineligiblefor ITC</th>
                                <th style="text-align: center; vertical-align:bottom;" colspan="4">Amount of ITC available</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">GSTIN</th>
                                <th style="text-align: center; vertical-align:bottom">No.</th>
                                <th style="text-align: center; vertical-align:bottom">Date</th>
                                <th style="text-align: center; vertical-align:bottom">GSTIN</th>
                                <th style="text-align: center; vertical-align:bottom">No.</th>
                                <th style="text-align: center; vertical-align:bottom">Date</th>
                                <th style="text-align: center; vertical-align:bottom">Value </th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br/>(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <asp:ListView ID="lv_GSTR2_6B" runat="server">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("INVGSTN") %></td>
                            <td><%# Eval("INVNO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVCDATE").ToString())%></td>
                            <td><%# Eval("GSTNNO") %></td>
                            <td><%# Eval("INVOICENO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                            <td><%# Eval("TOTALAMOUNT") %></td>
                            <td><%# Eval("RATE") %></td>
                            <td><%# Eval("TAXABLEAMOUNT") %></td>
                            <td><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                            <td><%# Eval("SERVICECAPITALGOODS") %></td>
                            <td><%# Eval("ITCIGSTAMT") %></td>
                            <td><%# Eval("ITCCGSTAMT") %></td>
                            <td><%# Eval("ITCSGSTAMT") %></td>
                            <td><%# Eval("ITCUGSTAMT") %></td>
                            <td><%# Eval("ITCCESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th>6B.&nbsp;Supplies by way of import of goods  or goods received from SEZ [Information furnished in Table 5 of earlier returns]-If details furnished earlier were incorrect</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">Amount</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">No.</th>
                                <th style="text-align: center; vertical-align:bottom">Date</th>
                                <th style="text-align: center; vertical-align:bottom">Value </th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="ListView8" runat="server">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("INVGSTN") %></td>
                            <td><%# Eval("INVNO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVCDATE").ToString()) %></td>
                            <td><%# Eval("GSTNNO") %></td>
                            <td><%# Eval("INVOICENO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                            <td><%# Eval("TOTALAMOUNT") %></td>
                            <td><%# Eval("RATE") %></td>
                            <td><%# Eval("TAXABLEAMOUNT") %></td>
                            <td><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT")  %></td>
                            <td><%# Eval("STATENAME") %></td>
                            <td><%# Eval("SERVICECAPITALGOODS") %></td>
                            <td><%# Eval("ITCIGSTAMT") %></td>
                            <td><%# Eval("ITCCGSTAMT") %></td>
                            <td><%# Eval("ITCSGSTAMT") %></td>
                            <td><%# Eval("ITCUGSTAMT") %></td>
                            <td><%# Eval("ITCCESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="11">6C.&nbsp;Debit Notes/Credit Notes [original]
                                </th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">Amount</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">No.</th>
                                <th style="text-align: center; vertical-align:bottom">Date</th>
                                <th style="text-align: center; vertical-align:bottom">Value </th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th  style="text-align: center; vertical-align:bottom">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="ListView9" runat="server">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("INVGSTN") %></td>
                            <td><%# Eval("INVNO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVCDATE").ToString()) %></td>
                            <td><%# Eval("GSTNNO") %></td>
                            <td><%# Eval("INVOICENO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                            <td><%# Eval("TOTALAMOUNT") %></td>
                            <td><%# Eval("RATE") %></td>
                            <td><%# Eval("TAXABLEAMOUNT") %></td>
                            <td><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                            <td><%# Eval("SERVICECAPITALGOODS") %></td>
                            <td><%# Eval("ITCIGSTAMT") %></td>
                            <td><%# Eval("ITCCGSTAMT") %></td>
                            <td><%# Eval("ITCSGSTAMT") %></td>
                            <td><%# Eval("ITCUGSTAMT") %></td>
                            <td><%# Eval("ITCCESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="11">6D.&nbsp;Debit Notes/ Credit Notes [amendment of debit notes/credit notes furnished in earlier tax periods]</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">Amount</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">No.</th>
                                <th style="text-align: center; vertical-align:bottom">Date</th>
                                <th style="text-align: center; vertical-align:bottom">Value </th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">7.&nbsp;Supplies received from composition taxable person and other exempt/Nil rated/Non GST supplies received </h3>
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
                <asp:ListView ID="ListView10" runat="server">
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
                            <td><%# Eval("COMPOSITIONTAXABLEPRSN") %></td>
                            <td><%# Eval("ISEXEMPTED") %></td>
                            <td><%# Eval("ISNILRATED") %></td>
                            <td><%# Eval("ISNONGSTGOODS") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="6">7A. Inter-State supplies</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">DESCRIPTION</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">Value of supplies received from</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">Composition taxable person</th>
                                <th style="text-align: center; vertical-align:bottom">Exempt supply</th>
                                <th style="text-align: center; vertical-align:bottom">Nil Rated supply </th>
                                <th style="text-align: center; vertical-align:bottom">Non GST supply</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="ListView11" runat="server">
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
                            <td><%# Eval("COMPOSITIONTAXABLEPRSN") %></td>
                            <td><%# Eval("ISEXEMPTED") %></td>
                            <td><%# Eval("ISNILRATED") %></td>
                            <td><%# Eval("ISNONGSTGOODS") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="6">7B.&nbsp;Intra-state supplies</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;" rowspan="2">#</th>
                                <th style="text-align: center;" rowspan="2">DESCRIPTION</th>
                                <th style="text-align: center;" colspan="4">Value of supplies received from</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">Composition taxable person</th>
                                <th style="text-align: center;">Exempt supply</th>
                                <th style="text-align: center;">Nil Rated supply </th>
                                <th style="text-align: center;">Non GST supply</th>
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
                <h3 class="box-title">7B.   Intra-state supplie</h3>
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
                <h3 class="box-title">8.&nbsp;ISD credit received </h3>
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
                <asp:ListView ID="lv_GSTR2_8A" runat="server">
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
                            <td><%# Eval("GSTIN_of_ISD") %></td>
                            <td><%# Eval("ISD_No") %></td>
                            <td><%# Eval("ISD_Date") %></td>
                            <td><%# Eval("IGST_TAX_AMT_ISD") %></td>
                            <td><%# Eval("CGST_TAX_AMT_ISD") %></td>
                            <td><%# Eval("SGST_TAX_AMT_ISD") %><%# Eval("UGST_TAX_AMT_ISD") %></td>
                            <td><%# Eval("Cess_ISD") %></td>
                            <td><%# Eval("IGST_TAX_AMT_ITC") %></td>
                            <td><%# Eval("CGST_TAX_AMT_ITC") %></td>
                            <td><%# Eval("SGST_TAX_AMT_ITC") %><%# Eval("UGST_TAX_AMT_ITC") %></td>
                            <td><%# Eval("Cess_ITC") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="12">A. ISD Invoice</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN of ISD</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="2">ISD Document Details</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">ISD Credit received</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">Amount of eligible ITC</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">No.</th>
                                <th style="text-align: center; vertical-align:bottom">Date</th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR2_8B" runat="server">
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
                            <td><%# Eval("GSTIN_of_ISD") %></td>
                            <td><%# Eval("ISD_No") %></td>
                            <td><%# Eval("ISD_Date") %></td>
                            <td style="text-align: center;"><%# Eval("IGST_TAX_AMT_ISD") %></td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT_ISD") %></td>
                            <td style="text-align: center;"><%# Eval("SGST_TAX_AMT_ISD") %><%# Eval("UGST_TAX_AMT_ISD") %></td>
                            <td><%# Eval("Cess_ISD") %></td>
                            <td style="text-align: center;"><%# Eval("IGST_TAX_AMT_ITC") %></td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT_ITC") %></td>
                            <td style="text-align: center;"><%# Eval("SGST_TAX_AMT_ITC") %><%# Eval("UGST_TAX_AMT_ITC") %></td>
                            <td><%# Eval("Cess_ITC") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="12">B.&nbsp;ISD Credit Note</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align:bottom" rowspan="2">GSTIN of ISD</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="2">ISD Document Details</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">ISD Credit received</th>
                                <th style="text-align: center; vertical-align:bottom" colspan="4">Amount of eligible ITC</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">No.</th>
                                <th style="text-align: center; vertical-align:bottom">Date</th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">9.&nbsp;TDS and TCS Credit received </h3>
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
                <asp:ListView ID="lv_GSTR2_9A" runat="server">
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
                            <td><%# Eval("GSTIN_of_ecommerce") %></td>
                            <td><%# Eval("Amount_Value") %></td>
                            <td><%# Eval("SalesReturn") %></td>
                            <td><%# Eval("Net_Value") %></td>
                            <td><%# Eval("IGST_TAX_AMT") %></td>
                            <td><%# Eval("CGST_TAX_AMT") %></td>
                            <td><%# Eval("SGST_TAX_AMT") %><%# Eval("UGST_TAX_AMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">A. TDS</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align:bottom;" rowspan="2">GSTIN of Deductor</th>
                                <th style="text-align: center; vertical-align:bottom;" rowspan="2">Gross Value</th>
                                <th style="text-align: center; vertical-align:bottom;" rowspan="2">Sales Return</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Net Value</th>
                                <th style="text-align: center;" colspan="3">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR2_9B" runat="server">
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
                            <td><%# Eval("GSTIN_of_ecommerce") %></td>
                            <td><%# Eval("Amount_Value") %></td>
                            <td><%# Eval("SalesReturn") %></td>
                            <td><%# Eval("Net_Value") %></td>
                            <td><%# Eval("IGST_TAX_AMT") %></td>
                            <td><%# Eval("CGST_TAX_AMT") %></td>
                            <td><%# Eval("SGST_TAX_AMT") %><%# Eval("UGST_TAX_AMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">B. TCS</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">GSTIN of Deductor</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Gross Value</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Sales Return</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Net Value</th>
                                <th style="text-align: center;" colspan="3">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">10.&nbsp;Consolidated Statement of Advances paid/Advance adjusted on account of receipt of supply  </h3>
                <h3 class="box-title">1.Information for the current month </h3>
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
                <h5 style="font-weight: bold;">10A.&nbsp; Advance amount paid for reverse charge supplies in the tax period (tax amount to be added to output tax liability)</h5>
                <asp:ListView ID="lv_GSTR2_10A1" runat="server">
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
                            <td><%# Eval("Rate") %></td>
                            <td><%# Eval("Rate") %></td>
                            <td><%# Eval("State") %></td>
                            <td><%# Eval("IGST_TAX_AMT") %>
                            <td><%# Eval("CGST_TAX_AMT") %>
                            <td><%# Eval("SGST_TAX_AMT").ToString()=="0.00"?(Eval("UGST_TAX_AMT").ToString()=="0.00"?"-":Eval("UGST_TAX_AMT")):(Eval("SGST_TAX_AMT").ToString()=="0.00"?"-":Eval("SGST_TAX_AMT")) %></td>
                            <td><%# Eval("Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">A.1. Intra-State supplies (Rate Wise)</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align:bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Gross Advance</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Place of supply</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR2_10A2" runat="server">
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
                            <td><%# Eval("Rate") %></td>
                            <td><%# Eval("Rate") %></td>
                            <td><%# Eval("State") %></td>
                            <td><%# Eval("IGST_TAX_AMT") %></td>
                            <td><%# Eval("CGST_TAX_AMT") %></td>
                            <td><%# Eval("SGST_TAX_AMT").ToString()=="0.00"?(Eval("UGST_TAX_AMT").ToString()=="0.00"?"-":Eval("UGST_TAX_AMT")):(Eval("SGST_TAX_AMT").ToString()=="0.00"?"-":Eval("SGST_TAX_AMT")) %></td>
                            <td><%# Eval("Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">A.2. Inter -State Supplies (Rate Wise)</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Gross Advance</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Place of supply</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <h5 style="font-weight: bold;">10B. Advance amount on which tax was paid in earlier period but invoice has been received in the current period</h5>
                <asp:ListView ID="lv_GSTR2_10B1" runat="server">
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
                            <td><%# Eval("Rate") %></td>
                            <td><%# Eval("Rate") %></td>
                            <td><%# Eval("State") %></td>
                            <td><%# Eval("IGST_TAX_AMT") %></td>
                            <td><%# Eval("CGST_TAX_AMT") %></td>
                            <td><%# Eval("SGST_TAX_AMT").ToString()=="0.00"?(Eval("UGST_TAX_AMT").ToString()=="0.00"?"-":Eval("UGST_TAX_AMT")):(Eval("SGST_TAX_AMT").ToString()=="0.00"?"-":Eval("SGST_TAX_AMT")) %></td>
                            <td><%# Eval("Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">B.1. Intra-State Supplies (Rate Wise)</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align:bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Gross Advance</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Place of supply</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                            </tr>
                            <tr>
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
                <asp:ListView ID="lv_GSTR2_10B2" runat="server">
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
                            <td><%# Eval("Rate") %></td>
                            <td><%# Eval("Rate") %></td>
                            <td><%# Eval("State") %></td>
                            <td><%# Eval("IGST_TAX_AMT") %></td>
                            <td><%# Eval("CGST_TAX_AMT") %></td>
                            <td><%# Eval("SGST_TAX_AMT").ToString()=="0.00"?(Eval("UGST_TAX_AMT").ToString()=="0.00"?"-":Eval("UGST_TAX_AMT")):(Eval("SGST_TAX_AMT").ToString()=="0.00"?"-":Eval("SGST_TAX_AMT")) %></td>
                            <td><%# Eval("Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">B.2. Intra-State Supplies (Rate Wise)</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Gross Advance</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Place of supply</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="4">Amount</th>
                            </tr>
                            <tr>
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
                <h3 class="box-title">11.&nbsp; Input Tax Credit Reversal / Reclaim </h3>
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
                <asp:ListView ID="lv_GSTR2_11A" runat="server">
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
                            <td><%# Eval("Rate") %></td>
                            <td><%# Eval("Gross_Adv_Paid") %></td>
                            <%--Ankita  <td><%# Eval("State") %></td>--%>
                            <td><%# Eval("IGST_TAX_AMT") %></td>
                            <td><%# Eval("CGST_TAX_AMT") %></td>
                            <td><%# Eval("SGST_TAX_AMT")%><%#Eval("UGST_TAX_AMT")%></td>
                            <td><%# Eval("Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description for reversal of ITC</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">To be added to or reduced from output liability</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="4">Amount of ITC</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR2_11B" runat="server">
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
                            <td><%# Eval("Rate") %></td>
                            <td><%# Eval("Gross_Adv_Paid") %></td>
                            <%--Ankita <td><%# Eval("State") %></td>--%>
                            <td><%# Eval("IGST_TAX_AMT") %></td>
                            <td><%# Eval("CGST_TAX_AMT") %></td>
                            <td><%# Eval("SGST_TAX_AMT")%> <%#Eval("UGST_TAX_AMT") %></td>
                            <td><%# Eval("Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description for reversal of ITC</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">To be added to or reduced from output liability</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="4">Amount of ITC</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">12.&nbsp; Addition and reduction of amount in output tax for mismatch and other reasons</h3>
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
                <asp:ListView ID="lv_GSTR2_12" runat="server">
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
                            <td><%# Eval("Description") %></td>
                            <td><%# Eval("ADD_Reduce") %></td>
                            <td><%# Eval("IGST_TAX_AMT") %></td>
                            <td><%# Eval("CGST_TAX_AMT") %></td>
                            <td><%# Eval("SGST_TAX_AMT")%><%#Eval("UGST_TAX_AMT") %></td>
                            <td><%# Eval("Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align:bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Add to or reduce from output liability</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="4">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align:bottom;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align:bottom;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">13.&nbsp; HSN summary of inward supplies</h3>
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
                <asp:ListView ID="lv_GSTR2_13" runat="server">
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
                            <td><%# Eval("HSN") %></td>
                            <td><%# Eval("Description") %></td>
                            <td><%# Eval("UQC") %></td>
                            <td><%# Eval("Total_quantity") %></td>
                            <td style="text-align: center;"><%# Eval("Total_Value") %></td>
                            <td style="text-align: center;"><%# Eval("Total_Tax_Value") %></td>
                            <td style="text-align: center;"><%# Eval("IGST_TAX_AMT").ToString()=="0.00"? " - " :Eval("IGST_TAX_AMT")  %></td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT").ToString()=="0.00"? " - " :Eval("CGST_TAX_AMT")  %></td>
                            <td style="text-align: center;"><%# Eval("SGST_TAX_AMT").ToString()=="0.00"?(Eval("UGST_TAX_AMT").ToString()=="0.00"?"-":Eval("UGST_TAX_AMT")):(Eval("SGST_TAX_AMT").ToString()=="0.00"?"-":Eval("SGST_TAX_AMT")) %></td>
                            <td style="text-align: center;"><%# Eval("Cess").ToString()=="0.00"? " - " :Eval("Cess")  %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">HSN </th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">UQC</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Total Quantity</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Total value</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable Value</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="4">Amount</th>
                            </tr>
                            <tr>
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
    </div>

</asp:Content>

