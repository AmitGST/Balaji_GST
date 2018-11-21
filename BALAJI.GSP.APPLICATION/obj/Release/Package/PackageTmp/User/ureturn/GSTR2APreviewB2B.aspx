<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/User.master" CodeBehind="GSTR2APreviewB2B.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.GSTR2APreviewB2B" %>

<%@ Register Src="~/UC/uc_GSTR_Taxpayer.ascx" TagPrefix="uc1" TagName="uc_GSTR_Taxpayer" %>
<%@ MasterType VirtualPath="~/User/User.master" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>GSTR2A
        <small>Inward supplies made by the taxpayer</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">GSTR2A</li>
        </ol>
    </div>
    <div class="content">

        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Taxpayer Details</h3>
            </div>

            <div class="box-body">
                <uc1:uc_GSTR_Taxpayer runat="server" ID="uc_GSTR_Taxpayer" />
                <%--<div class="box-footer with-border">
                    <asp:LinkButton ID="lkvGSTR2A" runat="server" CssClass="btn btn-success" Visible="false" OnClick="lkvGSTR2A_Click"><i class="fa fa-cloud-download"></i> Import GSTR - 2A</asp:LinkButton>
                </div>--%>
            </div>
        </div>



        <%--new--%>
        


        <div class="box box-danger">
            <div class="box-header with-border">

                <h3 class="box-title">3.Inward supplies received from a registered person other than the supplies attracting reverse charge</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR2A_3" runat="server">
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
                            <td><%#  DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString())%></td>
                            <td style="text-align: center;"><%# Eval("TOTALAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %> </td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom" rowspan="2">#</th>
                                <th style="vertical-align: bottom" rowspan="2">GSTIN of supplier</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="3">Invoice Details</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable Value<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="4">Amount of Tax</th>
                                <th style="vertical-align: bottom" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom">No.</th>
                                <th style="vertical-align: bottom; width:20px";>Date</th>
                                <th style="vertical-align: bottom; text-align:center;">Value</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>) </th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>) </th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
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

                <h3 class="box-title">4A.Inward supplies received from a registered person on which tax is to be paid on reverse charge </h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR2A_4" runat="server">
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
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString())%></td>
                            <td style="text-align: center;"><%# Eval("TOTALAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %> </td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%#  Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center;" colspan="3">Invoice details </th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable Value<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount of tax</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of supply</th>
                            </tr>
                            <tr>
                                <th>No.</th>
                                <th>Date</th>
                                <th style="text-align: center;">Value(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/ UTGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>


                <%--amits--%>
              <%--  <asp:ListView ID="lv_GSTR2A_4B" runat="server">
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
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString())%></td>
                            <td style="text-align: center;"><%# Eval("TOTALAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %> </td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%#  Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="12">4B. Inward supplies received from an unregistered supplier 
                                </th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center;" colspan="3">Invoice details </th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable Value(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount of tax</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of supply</th>
                            </tr>
                            <tr>
                                <th>No.</th>
                                <th>Date</th>
                                <th style="text-align: center;">Value(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">IGST(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/ UTGST(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
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
                            <td><%# Eval("GSTNNO") %></td>
                            <td><%# Eval("INVOICENO") %></td>
                            <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString())%></td>
                            <td style="text-align: center;"><%# Eval("TOTALAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %> </td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%#  Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                            <td><%# Eval("STATENAME") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="12">4C. Import of service 
                                </th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN of supplier</th>
                                <th style="text-align: center;" colspan="3">Invoice details </th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable Value(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount of tax</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of supply</th>
                            </tr>
                            <tr>
                                <th>No.</th>
                                <th>Date</th>
                                <th style="text-align: center;">Value(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">IGST(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/ UTGST(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>--%>

            </div>
        </div>
        <div class="box box-danger">
            <div class="box-header with-border">

                <h3 class="box-title">5. Debit / Credit notes (including amendments thereof) received during current tax period</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR2A_5" runat="server">
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
                            <td><%# Eval("Orginal_GSTIN") %></td>
                            <td><%# Eval("Orginal_No") %></td>
                            <td><%# Eval("Orginal_Date") %></td>
                            <td><%# Eval("Revised_GSTIN") %></td>
                            <td style="text-align: center;"><%# Eval("Revised_no") %></td>
                            <td><%# Eval("Revised_Date") %> </td>
                            <td> <%--ankita--%></td>
                            <td style="text-align: center;"><%# Eval("Rate") %></td>
                            <td style="text-align: center;"><%# Eval("TaxableValue") %></td>
                            <td style="text-align: center;"><%# Eval("IGST_TAX_AMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT") %></td>
                             <td><%# Eval("SGST_TAX_AMT") %><%# Eval("UGST_TAX_AMT") %></td>
                          <%-- <td style="text-align: center;"><%# Eval("SGST_TAX_AMT").ToString()=="0.00"?(Eval("UGST_TAX_AMT")=="0.00"?Eval("UGST_TAX_AMT"):"-"):(Eval("SGST_TAX_AMT")=="0.00"?"-":Eval("SGST_TAX_AMT")) %>
                                </td>--%>
                            <td style="text-align: center;"><%# Eval("Cess_AMT") %></td>
                            <td style="text-align: center;"><%# Eval("State") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th rowspan="2" style="vertical-align: bottom">#</th>
                                <th style="vertical-align: bottom" colspan="3">Details of original document</th>
                                <th style="text-align: center;" colspan="4">Revised details of documentor details of original Debit /Credit note</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable Value<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount of Tax</th>
                                <th style="vertical-align: bottom" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom">GSTIN</th>
                                <th style="vertical-align: bottom">No.</th>
                                <th style="vertical-align: bottom">Date</th>
                                <th style="vertical-align: bottom">GSTIN</th>
                                <th style="vertical-align: bottom">No.</th>
                                <th style="vertical-align: bottom">Date</th>
                                <th style="text-align: center; vertical-align: bottom;">Value</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>) </th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>) </th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
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

                <h3 class="box-title">6.ISD credit (including amendments thereof) received </h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR2A_6_1" runat="server">
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
                            <td><%# Eval("IGST_TAX_AMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT") %></td>
                             <td><%# Eval("SGST_TAX_AMT") %><%# Eval("UGST_TAX_AMT") %></td>
                          <%-- <td style="text-align: center;"><%# Eval("SGST_TAX_AMT").ToString()=="0.00"?(Eval("UGST_TAX_AMT")=="0.00"?Eval("UGST_TAX_AMT"):"-"):(Eval("SGST_TAX_AMT")=="0.00"?"-":Eval("SGST_TAX_AMT")) %>--%>
                               </td>
                            <td style="text-align: center;"><%# Eval("Cess") %></td>

                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">A.  ISD Invoice –eligible ITC
                                </th>
                            </tr>
                            <tr>
                                <th  style="vertical-align: bottom" rowspan="2">#</th>
                                <th style="vertical-align: bottom" rowspan="2">GSTIN of ISD</th>
                                <th style="text-align: center;" colspan="2">ISD document details</th>
                                <th style="text-align: center;" colspan="4">ITC amount involved</th>
                            </tr>
                            <tr>
                                <th style="vertical-align:bottom;">No.</th>
                                <th style="vertical-align:bottom;">Date</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>) </th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR2A_6_2" runat="server">
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
                            <td><%# Eval("IGST_TAX_AMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT") %></td>
                          <td><%# Eval("SGST_TAX_AMT") %><%# Eval("UGST_TAX_AMT") %></td>
                               </td>
                            <td style="text-align: center;"><%# Eval("Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">B.  ISD Invoice –ineligible ITC
                                </th>
                            </tr>
                            <tr>
                                <th  style="vertical-align: bottom" rowspan="2">#</th>
                                <th style="vertical-align: bottom" rowspan="2">GSTIN of ISD</th>
                                <th style="text-align: center;" colspan="2">ISD document details</th>
                                <th style="text-align: center;" colspan="4">ITC amount involved</th>
                            </tr>
                            <tr>
                                <th style="vertical-align:bottom;">No.</th>
                                <th style="vertical-align:bottom;">Date</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>) </th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>) </th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR2A_6_3" runat="server">
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
                            <td style="text-align: center;"><%# Eval("") %></td>
                            <td><%# Eval("") %> </td>
                            <td style="text-align: center;"><%# Eval("") %></td>
                            <td style="text-align: center;"><%# Eval("") %></td>
                            <td style="text-align: center;"><%# Eval("") %>
                                <%# Eval("") %></td>
                            <td style="text-align: center;"><%# Eval("") %></td>
                            <td><%# Eval("") %></td>
                            <td style="text-align: center;"><%# Eval("") %>
                                <td style="text-align: center;"><%# Eval("") %>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">C.  ISD Credit note –eligible ITCISD Credit note –eligible ITC
                                </th>
                            </tr>
                            <tr>
                                <th  style="vertical-align: bottom" rowspan="2">#</th>
                                <th style="vertical-align: bottom" rowspan="2">GSTIN of ISD</th>
                                <th style="text-align: center;" colspan="2">ISD document details</th>
                                <th style="text-align: center;" colspan="4">ITC amount involved</th>
                            </tr>
                            <tr>
                                <th>No.</th>
                                <th>Date</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>) </th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>) </th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR2A_6_4" runat="server">
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
                            <td style="text-align: center;"><%# Eval("") %></td>
                            <td><%# Eval("") %> </td>
                            <td style="text-align: center;"><%# Eval("") %></td>
                            <td style="text-align: center;"><%# Eval("") %></td>
                            <td style="text-align: center;"><%# Eval("") %>
                                <%# Eval("") %></td>
                            <td style="text-align: center;"><%# Eval("") %></td>
                            <td><%# Eval("") %></td>
                            <td style="text-align: center;"><%# Eval("") %>
                                <td style="text-align: center;"><%# Eval("") %>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">D.  ISD Credit note –ineligible ITC
                                </th>
                            </tr>
                            <tr>
                                <th  style="vertical-align: bottom" rowspan="2">#</th>
                                <th style="vertical-align: bottom" rowspan="2">GSTIN of ISD</th>
                                <th style="text-align: center;" colspan="2">ISD document details</th>
                                <th style="text-align: center;" colspan="4">ITC amount involved</th>
                            </tr>
                            <tr>
                                <th>No.</th>
                                <th>Date</th>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>) </th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>) </th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 12px;" class="fa fa-inr"></i>)</th>
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

                <h3 class="box-title">7. TDS and TCS Credit (including amendments thereof) received </h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR2A_7A" runat="server">
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
                            <td style="text-align: center;"><%# Eval("Net_Value") %></td>
                            <td style="text-align: center;"><%# Eval("IGST_TAX_AMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT") %> </td>
                           <td><%# Eval("SGST_TAX_AMT") %><%# Eval("UGST_TAX_AMT") %></td>
                                </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">A. TDS
                                </th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Amount received</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Sales Return</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Net Value</th>
                                <th style="text-align: center;" colspan="3">Amount</th>
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
                <asp:ListView ID="lv_GSTR2A_7B" runat="server">
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
                            <td style="text-align: center;"><%# Eval("Net_Value") %></td>
                            <td style="text-align: center;"><%# Eval("IGST_TAX_AMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT") %> </td>
                           <td style="text-align: center;"><%# Eval("SGST_TAX_AMT") %>
                                <%# Eval("UGST_TAX_AMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">B.TCS
                                </th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Amount received</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Sales Return</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Net Value</th>
                                <th style="text-align: center;" colspan="3">Amount</th>
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
        <div>
        </div>
    </div>

</asp:Content>

