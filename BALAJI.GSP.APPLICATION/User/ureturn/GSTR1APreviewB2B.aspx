<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/User.master" CodeBehind="GSTR1APreviewB2B.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.GSTR1APreviewB2B" %>
<%@ MasterType VirtualPath="~/User/User.master" %>
<%@ Register Src="~/UC/uc_GSTR_Taxpayer.ascx" TagPrefix="uc1" TagName="uc_GSTR_Taxpayer" %>
<%@ Register Src="~/UC/uc_Signatory.ascx" TagPrefix="uc1" TagName="uc_Signatory" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <%--<div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">GSTR1A</h2><small>OUTWARD SUPPLIES MADE BY THE TAXPAYER</small>
        </div>
    </div>--%>


    <%--<div class="row">--%>
              <div class="content-header">
        <h1>GSTR1A
        <small>OUTWARD SUPPLIES MADE BY THE TAXPAYER</small> </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">RETURN</a></li>
            <li class="active">GSTR1A</li>
        </ol>
    </div>
    <%--</div>--%>

               <div class="content">
     
                    <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Taxpayer Details</h3>
            </div>

            <div class="box-body">
                <uc1:uc_GSTR_Taxpayer runat="server" ID="uc_GSTR_Taxpayer" />
                <div class="box-footer with-border">
                    <uc1:uc_Signatory runat="server"  ID="uc_Signatory" />
                    <asp:LinkButton ID="lkvGSTR1A" runat="server" CssClass="btn btn-success"  OnClick="lkvGSTR1A_Click"><i class="fa fa-cloud-upload"></i> File GSTR - 1A</asp:LinkButton>
                    
                </div>
            </div>
        </div>

        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title"> 3.&nbsp;Taxable outward supplies made to registered persons including supplies attracting reverse charge other than the supplies</h3>
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
                    <asp:ListView ID="lv_GSTR1A_3A" runat="server">
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
                                <td><%# Eval("GSTNNO") %> </td>
                                <td><%# Eval("INVOICENO") %> </td>
                                <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                                <td style="text-align:center;"><%# Eval("TOTALAMOUNT") %> </td>
                                <td><%# Eval("RATE") %> </td>
                                <td style="text-align:center;"><%# Eval("TAXABLEAMOUNT") %> </td>
                                <td style="text-align:center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT")%> </td>
                                <td style="text-align:center;"><%#  Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %> </td>
                                <td style="text-align:center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                                <td style="text-align:center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %> </td>
                                <td><%# Eval("STATENAME") %> </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                             <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="12">A.&nbsp;Supplies other than those attracting reverse charge (From table 3 of GSTR-2)</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style=" vertical-align:bottom;" rowspan="2">GSTIN/ UIN</th>
                                <th style="text-align:center;" colspan="3">Invoice details</th>
                               <th style="text-align:center; vertical-align:bottom;"rowspan="2">Rate</th>
                                <th style="text-align:center; vertical-align:bottom;"rowspan="2">Taxable value</th>
                                  <th style="text-align:center;" colspan="4">Amount</th>
                                <th style="vertical-align:bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align:bottom;">No.</th>
                                <th style="vertical-align:bottom;">Date</th>
                                <th style="text-align:center; vertical-align:bottom;">Value</th>
                                <th style="text-align:center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align:center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align:center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                 <th style="text-align:center;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </tbody>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>
                 <asp:ListView ID="lv_GSTR1A_3B" runat="server">
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
                                <td><%# Eval("GSTNNO") %> </td>
                                <td><%# Eval("INVOICENO") %> </td>
                                <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                                <td style="text-align:center;"><%# Eval("TOTALAMOUNT") %> </td>
                                <td><%# Eval("RATE") %> </td>
                                <td style="text-align:center;"><%# Eval("TAXABLEAMOUNT") %> </td>
                                <td style="text-align:center;"> <%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %> </td>
                                <td  style="text-align:center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %> </td>
                              
                                <td style="text-align:center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %>

                                </td>
                                <td  style="text-align:center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %> </td>
                                <td><%# Eval("STATENAME") %></td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="12">3B.&nbsp;Supplies attracting reverse charge (From table 4A of GSTR-2)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align:bottom;" rowspan="2">#</th>
                                <th style="text-align:center; vertical-align:bottom;" rowspan="2">GSTIN/ UIN</th>
                                <th style="text-align:center;" colspan="3">Invoice details</th>
                               <th style="text-align:center; vertical-align:bottom;"rowspan="2">Rate</th>
                                <th style="text-align:center; vertical-align:bottom;"rowspan="2">Taxable value</th>
                                  <th style="text-align:center;" colspan="4">Amount</th>
                                <th style="vertical-align:bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align:bottom;">No.</th>
                                <th style="text-align:center; vertical-align:bottom;">Date</th>
                                <th style="text-align:center; vertical-align:bottom;">Value</th>
                                <th style="text-align:center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align:center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align:center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                 <th style="text-align:center;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">4.&nbsp;Zero rated supplies made to SEZ and deemed exports</h3>
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
                    <asp:ListView ID="lv_GSTR_1A_4A" runat="server">
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
                                <td><%# Eval("GSTINRECIPIENT") %> </td>
                                <td><%# Eval("InvoiceNo") %></td>
                                <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                                <td style="text-align:center;"><%# Eval("TOTALAMOUNT") %> </td>
                                <td style="text-align:center;"><%# Eval("IGSTRATE") %> </td>
                                <td style="text-align:center;"><%# Eval("TAXABLEAMOUNT") %> </td>
                                <td style="text-align:center;"><%# Eval("IGSTAMT") %> </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                             <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">A.&nbsp;Supplies other than those attracting reverse charge </th>
                            </tr>
                            <tr>
                                <th style="vertical-align:bottom;" rowspan="2">#</th>
                                <th style="text-align:center; vertical-align:bottom;" rowspan="2">GSTIN of recipient</th>
                                <th style="text-align:center;" colspan="3">Invoice details</th>
                               <th style="text-align:center;" colspan="3">IGST</th>
                            </tr>
                            <tr>
                                 <th style="vertical-align:bottom;">No.</th>
                                <th style="vertical-align:bottom;">Date</th>
                                <th style="vertical-align:bottom; text-align:center;">Value</th>
                                <th style="text-align:center;">Rate</th>
                                <th style="text-align:center;">Taxable value</th>
                                <th style="text-align:center;">Tax amount</th>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </tbody>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>

                 <asp:ListView ID="lv_GSTR_1A_4B" runat="server">
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
                                <td><%# Eval("GSTINRECIPIENT") %> </td>
                                <td><%# Eval("InvoiceNo") %></td>
                                <td><%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %></td>
                                <td style="text-align:center;"><%# Eval("TOTALAMOUNT")%> </td>
                                <td style="text-align:center;"><%# Eval("IGSTRATE") %></td>
                                <td style="text-align:center;"><%# Eval("TAXABLEAMOUNT") %> </td>
                                <td style="text-align:center;"><%# Eval("IGSTAMT") %> </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                             <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">B.&nbsp;Deemed exports </th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="vertical-align:bottom;" rowspan="2">GSTIN of recipient</th>
                                <th style="text-align:center; vertical-align:bottom;" colspan="3">Invoice details</th>
                               <th style="text-align:center;" colspan="3">IGST</th>
                            </tr>
                            <tr>
                                <th style="vertical-align:bottom;">No.</th>
                                <th style="vertical-align:bottom;">Date</th>
                                <th style="text-align:center;vertical-align:bottom;">Value</th>
                                <th style="text-align:center;">Rate</th>
                                <th style="text-align:center;">Taxable value</th>
                                <th style="text-align:center;">Tax amount</th>
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
                <h3 class="box-title">5. Debit notes, credit notes (including amendments thereof) issued during current period</h3>
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
                <asp:ListView ID="lv_GSTR_1A_5" runat="server">
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
                                <td><%# Eval("OrginalGSTIN") %></td>
                                <td><%# Eval("OrginalNo") %></td>
                                <td><%# Eval("OrginalDate") %></td>
                                <td><%# Eval("RevisedGSTN") %></td>
                                <td><%# Eval("RevisedNo") %></td>
                                <td><%# Eval("RevisedDate") %></td>
                                <td><%# Eval("RevisedValue") %></td>
                                <td><%# Eval("Rate") %></td>
                                <td><%# Eval("TaxableValue") %></td>
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
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" colspan="3">Details of original document</th>
                                <th style="vertical-align:bottom; text-align: center;" colspan="4">Revised details of documentor details of original Debit </th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate<br />(<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of Supply</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
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
        


  <%--  ankita--%>
        
        </div>
               
   <%-- <div class="content">
        <div class="row">
            <div align="right" style="margin-right:15px;">
            <asp:Button ID="btnAcceptGSTR1A" runat="server" Text="File GSTR1A" Width="100px" OnClick="btnAcceptGSTR1A_Click" CssClass="btn btn-primary" />                                        
                   <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" CssClass="btn btn-primary" Visible="false" OnClick="btnBack_Click" />
        </div>
        </div>
    </div>--%>
</asp:Content>
