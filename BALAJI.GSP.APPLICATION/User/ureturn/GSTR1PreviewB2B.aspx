<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/User.master" CodeBehind="GSTR1PreviewB2B.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.GSTR1PreviewB2B" %>

<%@ MasterType VirtualPath="~/User/User.master" %>
<%@ Register Src="~/UC/uc_GSTR_Taxpayer.ascx" TagPrefix="uc1" TagName="uc_GSTR_Taxpayer" %>
<%@ Register Src="~/UC/uc_GSTNUsers.ascx" TagPrefix="uc1" TagName="uc_GSTNUsers" %>
<%@ Register Src="~/UC/UC_Invoice/uc_invoiceMonth.ascx" TagPrefix="uc1" TagName="uc_invoiceMonth" %>




<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:uc_invoiceMonth runat="server" ID="uc_invoiceMonth" Visible="false" />
    <div class="content-header">
        <h1>GSTR1
        <small>Outward supplies made by the taxpayer</small> </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">GSTR1</li>
        </ol>
    </div>
    <!-- Main content -->
    <div class="content">
        <div class="box box-primary">
            <div class="row">
                <div class="box-header with-border">
                    <uc1:uc_GSTNUsers runat="server" ID="uc_GSTNUsers" />
                </div>
            </div>
            <div class="box-body">
                <h4><strong>Taxpayer Details</strong></h4>
                <uc1:uc_GSTR_Taxpayer runat="server" ID="uc_GSTR_Taxpayer" />
            </div>
            <div class="box-footer">
                  <div class="col-sm-1">
                <asp:LinkButton ID="lkbBack" CssClass="btn btn-danger" Style="margin-left: 15px;" OnClick="lkbBack_Click" runat="server"><i class="fa fa-backward"></i> Back</asp:LinkButton>
                    </div>
                <div class="col-sm-1">
                <asp:LinkButton ID="lkbFileGSTR1" Visible="false" OnClick="lkbFileGSTR1_Click" CssClass="btn btn-success" runat="server"><i class="fa fa-cloud-upload"></i> File GSTR-1</asp:LinkButton>
                    </div>
                <div class="col-sm-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="lkbJson" Style="margin-left: 25px;" OnClick="lkbJson_Click" CssClass="btn btn-primary" Visible="true" runat="server"><i class="fa fa-download" aria-hidden="true"></i><span style="margin:3px;">Download Json</span></asp:LinkButton>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lkbJson" />
                </Triggers>
            </asp:UpdatePanel>
                </div>
               
            </div>
            
        </div>


        <%--GSTR_1_4--%>
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">4.&nbsp; Taxable outward supplies made to registered persons (including UIN-holders) other than supplies </h3>
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
                <asp:ListView ID="lv_GSTR1_4A" runat="server" >
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
                                <asp:Label ID="lblGSTNNo" runat="server" Text='<%# Eval("GSTNNO") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceno" runat="server" Text='<%# Eval("INVOICENO") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoicedate" runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("TOTALAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RATE") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTaxableAmount" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIgstAmt" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCgstAmt" runat="server" Text='<%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;"><%-- <%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?Eval("SGSTAMT"):"-") %>--%><%--<%# Eval("SGSTAMT") %> <%# Eval("UGSTAMT") %>--%><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCessAmt" runat="server" Text='<%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblStatename" runat="server" Text='<%# Eval("StateName") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-responsive">
                            <tr>
                                <th colspan="12">4A.&nbsp; Supplies other than those (i) attracting reverse charge and (ii) supplies made through e-commerce operator</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center;" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate<br />
                                    (<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="text-align: center; vertical-align: bottom;">Value </th>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <h5 style="font-weight: bold;">4B.&nbsp; Supplies attracting tax on reverse charge basis </h5>
                <asp:ListView ID="lv_GSTR1_4B" runat="server" DataKeyNames="InvoiceNo">
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
                                <asp:Label ID="lblGSTNNo" runat="server" Text='<%# Eval("GSTNNO") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceno" runat="server" Text='<%# Eval("INVOICENO") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoicedate" runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("TOTALAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RATE") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTaxableAmount" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIgstAmt" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCgstAmt" runat="server" Text='<%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;"><%--   <%# Eval("SGSTAMT")%> <%# Eval("UGSTAMT")%>--%><%--  <%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %>--%><%--  <%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %>--%><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCessAmt" runat="server" Text='<%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblStatename" runat="server" Text='<%# Eval("StateName") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center;" colspan="3">Invoice Details</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="text-align: center; vertical-align: bottom;">Value </th>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <h5 style="font-weight: bold;">4C.&nbsp; Supplies made through e-commerce operator attracting TCS (operator wise, rate wise)</h5>
                <asp:ListView ID="lv_GSTR1_4C" runat="server">
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
                                <asp:Label ID="lblGSTNNo" runat="server" Text='<%# Eval("GSTNNO") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceno" runat="server" Text='<%# Eval("INVOICENO") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoicedate" runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("TOTALAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RATE") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTaxableAmount" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIgstAmt" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT")  %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCgstAmt" runat="server" Text='<%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %><%--    <%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %>
                            </td>--%>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblCessAmt" runat="server" Text='<%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblStatename" runat="server" Text='<%# Eval("StateName") %>' />
                                </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">GSTIN/UIN</th>
                                <th style="text-align: center;" colspan="3">Invoice Details</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom;" rowspan="2">Taxable value<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of Supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="text-align: center; vertical-align: bottom;">Value </th>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
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
        </div>



        <%--GSTR_1_5--%>
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">5.&nbsp; Taxable outward inter-State supplies to un-registered persons where the invoice value is more than Rs2.5 lakh</h3>
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
                <h5 style="font-weight: bold;">5A.&nbsp; Outward supplies (other than supplies made through e-commerce operator, rate wise)</h5>
                <asp:ListView ID="lv_GSTR1_5A1" runat="server">
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
                                <asp:Label ID="lblStatename" runat="server" Text='<%# Eval("StateName") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceno" runat="server" Text='<%# Eval("INVOICENO") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoicedate" runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("TOTALAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RATE") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTaxableAmount" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGstAmt" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>' />
                            </td>
                            <td><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">State</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="3">Invoice Details</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate<br />
                                    (<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">TaxableValue<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="2">Amount</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">No.</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="vertical-align: bottom; text-align: center;">Value</th>
                                <th style="vertical-align: bottom; text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom; text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <div>
                    <h5 style="font-weight: bold;">5B.&nbsp;Supplies made through e-commerce operator attracting TCS (operator wise) [Rate wise] </h5>
                </div>
                <asp:ListView ID="lv_GSTR1_5A2" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-striped">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%>.</td>
                            <td>
                                <asp:Label ID="lblStatename" runat="server" Text='<%# Eval("StateName") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceno" runat="server" Text='<%# Eval("INVOICENO") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoicedate" runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("TOTALAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RATE") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTaxableAmount" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGstAmt" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT")  %>' />
                            </td>
                            <td><%--   <%# Eval("CESSAMT") %>--%></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">State</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="3">Invoice Details</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate<br />
                                    (<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">TaxableValue<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="2">Amount</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">No.</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="vertical-align: bottom; text-align: center;">Value<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom; text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom; text-align: center;">Cess<br />
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
        </div>


        <!--6A-Exports -->

        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">6.&nbsp;Zero rated supplies & Deemed Exports </h3>
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
                <div>
                    <h5 style="font-weight: bold;"></h5>
                </div>
                <asp:ListView ID="lvGSTR1_6A" runat="server">
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
                                <asp:Label ID="lblInvoiceID" runat="server" Text='<%# Eval("GSTINRECIPIENT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("TotalAmount") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblShippingNo" runat="server" Text='<%# Eval("InvoiceNo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblShippingDt" runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTRate" runat="server" Text='<%# Eval("IGSTRate").ToString()=="0.00"? " - " :Eval("IGSTRate") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTaxableAmount" runat="server" Text='<%# Eval("TaxableAmount") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTAmt" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="10">6A.&nbsp;Exports</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="vertical-align: bottom" rowspan="2">GSTIN Recipient</th>
                                <th style="text-align: center;" colspan="3">Invoice details</th>
                                <th style="text-align: center;" colspan="2">Shipping Bill's No.</th>
                                <th style="text-align: center;" colspan="3">Integrated Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom">No.</th>
                                <th style="vertical-align: bottom">Date</th>
                                <th style="vertical-align: bottom; text-align: center;">Value</th>
                                <th style="vertical-align: bottom">No</th>
                                <th style="vertical-align: bottom">Date</th>
                                <th style="text-align: center;">Rate<br />
                                    (<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center;">Taxable value<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Amt<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                            </tr>
                            <tr>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <div>
                </div>
                <asp:ListView ID="lvGSTR1_6B" runat="server">
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
                                <asp:Label ID="lblInvoiceID" runat="server" Text='<%# Eval("GSTINRECIPIENT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("TotalAmount") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblShippingNo" runat="server" Text='<%# Eval("InvoiceNo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblShippingDt" runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTRate" runat="server" Text='<%# Eval("IGSTRate").ToString()=="0.00"? " - " :Eval("IGSTRate") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTaxableAmount" runat="server" Text='<%# Eval("TaxableAmount") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTAmt" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="10">6B.&nbsp; Supplies made to SEZ unit or SEZ Developer</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom" rowspan="2">#</th>
                                <th style="vertical-align: bottom" rowspan="2">GSTIN Recipient</th>
                                <th style="text-align: center; vertical-align: bottom" colspan="3">Invoice details</th>
                                <th style="text-align: center;" colspan="2">Shipping Bill's No.</th>
                                <th style="text-align: center;" colspan="3">Integrated Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom; vertical-align: bottom">Date</th>
                                <th style="text-align: center; vertical-align: bottom">Value</th>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom">Date</th>
                                <th style="text-align: center;">Rate<br />
                                    (<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center;">Taxable value<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <div>
                </div>
                <asp:ListView ID="lvGSTR1_6C" runat="server">
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
                                <asp:Label ID="lblInvoiceID" runat="server" Text='<%# Eval("GSTINREC") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("INVOICEDATE").ToString()) %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("TotalAmount") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblShippingNo" runat="server" Text='<%# Eval("InvoiceNo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblShippingDt" runat="server" Text='<%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTRate" runat="server" Text='<%# Eval("IGSTRate") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTaxableAmount" runat="server" Text='<%# Eval("TaxableAmount") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTAmt" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="10">6C.&nbsp;Deemed exports</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th style="vertical-align: bottom" rowspan="2">GSTIN Recipient</th>
                                <th style="text-align: center;" colspan="3">Invoice details</th>
                                <th style="text-align: center;" colspan="2">Shipping Bill's No.</th>
                                <th style="text-align: center;" colspan="3">Integrated Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">No.</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="vertical-align: bottom; text-align: center;">Value</th>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="text-align: center;">Rate<br />
                                    (<i style="font-size: 10px;" class="fa fa-percent"></i>)</th>
                                <th style="text-align: center;">Taxable value<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
        </div>

        <!--6B-Supplies made to SEZ unit or SEZ Developer -->


        <%--7 Gstr1--%>
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">7.&nbsp;Taxable supplies (Net of debit notes and credit notes) to unregistered persons</h3>
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
                <h5 style="font-weight: bold;">7A.&nbsp;Intra-State supply </h5>
                <asp:ListView ID="lvGSTR1_7A1" runat="server">
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
                            <td style="text-align: center;">
                                <asp:Label ID="lblTAXABLEAMOUNT" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTOTALAMOUNT" runat="server" Text='<%# Eval("TOTALAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTAmount" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCGSTAmount" runat="server" Text='<%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCess" runat="server" Text='<%#Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">7A (1).&nbsp;Consolidated rate wise outward supplies [including supplies made through e-commerce operator attracting TCS]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom" rowspan="2">Rate of tax</th>
                                <th style="text-align: center; vertical-align: bottom" rowspan="2">Total Taxablevalue</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_7A2" runat="server">
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
                            <td style="text-align: center;">
                                <asp:Label ID="lblTAXABLEAMOUNT" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTOTALAMOUNT" runat="server" Text='<%# Eval("TOTALAMOUNT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTAmount" runat="server" Text='<%#Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCGSTAmount" runat="server" Text='<%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">2.&nbsp;Out of supplies mentioned at 7A(1), value of supplies made through e-Commerce Operators attracting TCS (operator wise, rate wise)</th>
                                <tr>
                                    <th style="vertical-align: bottom" rowspan="2">#</th>
                                    <th style="text-align: center; vertical-align: bottom" rowspan="2">Rate of tax</th>
                                    <th style="text-align: center; vertical-align: bottom" rowspan="2">Total Taxablevalue</th>
                                    <th style="text-align: center;" colspan="4">Amount</th>
                                </tr>
                                <tr>
                                    <th style="text-align: center;">IGST<br />
                                        (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                    <th style="text-align: center;">CGST<br />
                                        (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                    <th style="text-align: center;">SGST/UTGST<br />
                                        (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                    <th style="text-align: center;">Cess<br />
                                        (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </tbody>
                                </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <h5 style="font-weight: bold;">7B.&nbsp;Inter-State Supplies where invoice value is upto Rs 2.5 Lakh [Rate wise] </h5>
                <asp:ListView ID="lvGSTR1_7B1" runat="server">
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
                                <asp:Label ID="lblTAXABLEAMOUNT" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTOTALAMOUNT" runat="server" Text='<%# Eval("TOTALAMOUNT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIGSTAmount" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCGSTAmount" runat="server" Text='<%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>' />
                            </td>
                            <td><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td>
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">1.&nbsp;Place of Supply (Name of State)</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th rowspan="2">Taxable Value</th>
                                <th rowspan="2">Total Amount</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGSTT<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGSTT<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_7B2" runat="server">
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
                                <asp:Label ID="lblTAXABLEAMOUNT" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTOTALAMOUNT" runat="server" Text='<%# Eval("TOTALAMOUNT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIGSTAmount" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCGSTAmount" runat="server" Text='<%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>' />
                            </td>
                            <td><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td>
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">2.&nbsp;Out of the supplies mentioned in 8B, the supplies made through  e-Commerce Operators</th>
                            </tr>
                            <tr>
                                <th rowspan="2">#</th>
                                <th rowspan="2">Taxable Value</th>
                                <th rowspan="2">Total Amount</th>
                                <th colspan="4">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
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
        </div>
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">8.&nbsp;Nil rated, exempted and non GST outward supplies </h3>
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
                <asp:ListView ID="lvGSTR1_8A" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNilRatedSupplies" runat="server" Text='<%# Eval("IsNilRated") %>' />
                            </td>
                            <%-- <td>
                                <asp:Label ID="lblExempted" runat="server" Text='<%# Eval("IsExempted") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNonGSTsupplies" runat="server" Text='<%# Eval("IsNonGSTGoods") %>' />
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="3">A.1.&nbsp;Inter-State supplies to registered persons
                                </th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Nil Rated Supplies</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_8A_2" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNilRatedSupplies" runat="server" Text='<%# Eval("IsNilRated") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="3">A.2.&nbsp;Inter-State supplies to registered persons
                                </th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Exempted</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_8A_3" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNilRatedSupplies" runat="server" Text='<%# Eval("IsNilRated") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="3">A.3.&nbsp;Inter-State supplies to registered persons
                                </th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Non-GST supplies</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_8B" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td><%#Eval("IsNilRated").ToString()=="False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("IsNilRated") %></span></td>
                            <%--<td>
                                <asp:Label ID="lblExempted" runat="server" Text='<%# Eval("IsExempted") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNonGSTsupplies" runat="server" Text='<%# Eval("IsNonGSTGoods") %>' />
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="3">8B.1.&nbsp;Intra- State supplies to registered persons</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Nil Rated Supplies</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_8B_2" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td><%#Eval("IsNilRated").ToString()=="False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("IsNilRated") %></span></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="3">8B.2.&nbsp;Intra- State supplies to registered persons</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Exempted</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_8B_3" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td><%#Eval("IsNilRated").ToString()=="False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("IsNilRated") %></span></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="3">8B.2.&nbsp;Intra- State supplies to registered persons</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Non-GST supplies</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_8C" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNilRatedSupplies" runat="server" Text='<%# Eval("IsNilRated") %>' />
                            </td>
                            <%-- <td>
                                <asp:Label ID="lblExempted" runat="server" Text='<%# Eval("IsExempted") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNonGSTsupplies" runat="server" Text='<%# Eval("IsNonGSTGoods") %>' />
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="3">8C.1.&nbsp;Inter-State supplies to unregistered persons</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Nil Rated Supplies</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR1_8C_2" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNilRatedSupplies" runat="server" Text='<%# Eval("IsNilRated") %>' />
                            </td>
                            <%-- <td>
                                <asp:Label ID="lblExempted" runat="server" Text='<%# Eval("IsExempted") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNonGSTsupplies" runat="server" Text='<%# Eval("IsNonGSTGoods") %>' />
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="3">8C.2.&nbsp;Inter-State supplies to unregistered persons</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Exempted</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR1_8C_3" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNilRatedSupplies" runat="server" Text='<%# Eval("IsNilRated") %>' />
                            </td>
                            <%-- <td>
                                <asp:Label ID="lblExempted" runat="server" Text='<%# Eval("IsExempted") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNonGSTsupplies" runat="server" Text='<%# Eval("IsNonGSTGoods") %>' />
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="3">8C.3.&nbsp;Inter-State supplies to unregistered persons</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Non-GST Supplies</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_8D" runat="server">
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
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNilRatedSupplies" runat="server" Text='<%# Eval("IsNilRated") %>' />
                            </td>
                            <%-- <td>
                                <asp:Label ID="lblExempted" runat="server" Text='<%# Eval("IsExempted") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNonGSTsupplies" runat="server" Text='<%# Eval("IsNonGSTGoods") %>' />
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="3">8D.1.&nbsp;Intra-State supplies to unregistered persons</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Nil Rated Supplies</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_8D_2" runat="server">
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
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNilRatedSupplies" runat="server" Text='<%# Eval("IsNilRated") %>' />
                            </td>
                            <%-- <td>
                                <asp:Label ID="lblExempted" runat="server" Text='<%# Eval("IsExempted") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNonGSTsupplies" runat="server" Text='<%# Eval("IsNonGSTGoods") %>' />
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="3">8D.2.&nbsp;Intra-State supplies to unregistered persons</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Exempted</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_8D_3" runat="server">
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
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNilRatedSupplies" runat="server" Text='<%# Eval("IsNilRated") %>' />
                            </td>
                            <%-- <td>
                                <asp:Label ID="lblExempted" runat="server" Text='<%# Eval("IsExempted") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNonGSTsupplies" runat="server" Text='<%# Eval("IsNonGSTGoods") %>' />
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="3">8D.3.&nbsp;Intra-State supplies to unregistered persons</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Non-GST Supplies</th>
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
                <h3 class="box-title">9.&nbsp;Amendments to taxable outward supply details furnished in returns for earlier tax periods in Table 4,5 and 6 [including debit notes, credit notes, refund vouchers issued during current period and amendments thereof]</h3>
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
                <asp:ListView ID="lvGSTR1_9A" runat="server">
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
                                <asp:Label ID="lblOrginalGSTN" runat="server" Text='<%# Eval("Orginal_GSTN") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblOrginalno" runat="server" Text='<%# Eval("Orginal_no") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblOrginalDate" runat="server" Text=' <%# DateTimeAgo.GetFormatDate(Eval("Orginal_Date").ToString()) %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFGSTN" runat="server" Text='<%# Eval("F_GSTN") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblF_no" runat="server" Text='<%# Eval("F_no") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFDate" runat="server" Text='<%# Eval("F_Date") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTaxableValue" runat="server" Text='<%# Eval("Taxable_Value") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTTAX" runat="server" Text='<%# Eval("IGST_TAX").ToString()=="0.00"? " - " :Eval("IGST_TAX") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCGSTTAX" runat="server" Text='<%# Eval("CGST_TAX").ToString()=="0.00"? " - " :Eval("CGST_TAX")  %>' />
                            </td>
                            <td style="text-align: center;"><%# Eval("SGST_TAX").ToString()=="0.00"?(Eval("UGST_TAX")=="0.00"?Eval("UGST_TAX"):"-"):(Eval("SGST_TAX")=="0.00"?Eval("SGST_TAX"):"-") %></td>
                            <td>
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("Cess").ToString()=="0.00"? " - " :Eval("Cess") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSTATE" runat="server" Text='<%# Eval("STATE") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="14">A.&nbsp;If the invoice/Shipping bill details furnished earlier were incorrect</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="3">Details of original document</th>
                                <th style="text-align: center;" colspan="4">Revised details of document or details of original Debit/Credit Notes or refund vouchers</th>
                                <%-- <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>--%>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">TaxableValue</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Place of supply</th>
                            </tr>
                            <tr>
                                <th>GSTIN</th>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="vertical-align: bottom;">GSTIN</th>
                                <th style="text-align: center; vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="text-align: center;">Value</th>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_9B" runat="server">
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
                                <asp:Label ID="lblOrginalGSTN" runat="server" Text='<%# Eval("Orginal_GSTN") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblOrginalno" runat="server" Text='<%# Eval("Orginal_no") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblOrginalDate" runat="server" Text='<%# Eval("Orginal_Date") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFGSTN" runat="server" Text='<%# Eval("F_GSTN") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblF_no" runat="server" Text='<%# Eval("F_no") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFDate" runat="server" Text='<%# Eval("F_Date") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTaxableValue" runat="server" Text='<%# Eval("Taxable_Value") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIGSTTAX" runat="server" Text='<%# Eval("IGST_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCGSTTAX" runat="server" Text='<%# Eval("CGST_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSGSTTAX" runat="server" Text='<%# Eval("SGST_TAX") %>' />
                                <asp:Label ID="lblUGSTTAX" runat="server" Text='<%# Eval("UGST_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("Cess") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSTATE" runat="server" Text='<%# Eval("STATE") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="14">B.&nbsp;Debit Notes/Credit Notes/Refund voucher [original]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="3">Details of original document</th>
                                <th style="text-align: center;" colspan="4">Revised details of document or details of original Debit/Credit Notes or refund vouchers</th>
                                <%-- <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>--%>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">TaxableValue</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Place of supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">GSTIN</th>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="vertical-align: bottom;">GSTIN</th>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="vertical-align: bottom; text-align: center;">Value</th>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_9C" runat="server">
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
                                <asp:Label ID="lblOrginalGSTN" runat="server" Text='<%# Eval("Orginal_GSTN") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblOrginalno" runat="server" Text='<%# Eval("Orginal_no") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblOrginalDate" runat="server" Text='<%# Eval("Orginal_Date") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFGSTN" runat="server" Text='<%# Eval("F_GSTN") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblF_no" runat="server" Text='<%# Eval("F_no") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFDate" runat="server" Text='<%# Eval("F_Date") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTaxableValue" runat="server" Text='<%# Eval("Taxable_Value") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTTAX" runat="server" Text='<%# Eval("IGST_TAX") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCGSTTAX" runat="server" Text='<%# Eval("CGST_TAX") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblSGSTTAX" runat="server" Text='<%# Eval("SGST_TAX") %>' />
                                <asp:Label ID="lblUGSTTAX" runat="server" Text='<%# Eval("UGST_TAX") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("Cess") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblSTATE" runat="server" Text='<%# Eval("STATE") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="14">C.&nbsp;Debit Notes/Credit Notes/Refund voucher [amendments thereof]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="3">Details of original document</th>
                                <th style="text-align: center;" colspan="4">Revised details of document or details of original Debit/Credit Notes or refund vouchers</th>
                                <%--  <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>--%>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">TaxableValue</th>
                                <th style="vertical-align: bottom; text-align: center;" colspan="4">Amount</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Place of supply</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">GSTIN</th>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="vertical-align: bottom;">GSTIN</th>
                                <th style="vertical-align: bottom;">No</th>
                                <th style="vertical-align: bottom;">Date</th>
                                <th style="vertical-align: bottom; text-align: center;">Value</th>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
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
        </div>
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">10.&nbsp;Amendments to taxable outward supplies to unregistered persons furnished in returns for earlier tax periods</h3>
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
                <h5 style="font-weight: bold;">10.A.&nbsp;Tax period for which the details are being revised</h5>
                <asp:ListView ID="lvGSTR1_10A" runat="server">
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
                            <td style="text-align: center;">
                                <asp:Label ID="lblRateTax" runat="server" Text='<%# Eval("RateOfTax") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTaxablevalue" runat="server" Text='<%# Eval("Totaltaxablevalue") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGST" runat="server" Text='<%#  Eval("IGST_TAX") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCGST" runat="server" Text='<%# Eval("CGST_TAX") %>' />
                            </td>
                            <td style="text-align: center;"><%# Eval("SGST_TAX").ToString()=="0.00"?(Eval("UGST_TAX")=="0.00"?Eval("UGST_TAX"):"-"):(Eval("SGST_TAX")=="0.00"?Eval("SGST_TAX"):"-") %><%--<asp:Label ID="lblSGST" runat="server" Text='<%# Eval("SGST_TAX") %>' />
                                <asp:Label ID="lblUGSTTAX" runat="server" Text='<%# Eval("UGST_TAX") %>' />--%></td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("Cess") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">10.A.1&nbsp;Intra-State Supplies [Rate wise]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate of tax</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Total Taxable value</th>
                                <th style="text-align: center;" colspan="4">Amount </th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_10AA" runat="server">
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
                                <asp:Label ID="lblRateTax" runat="server" Text='<%# Eval("RateOfTax") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTaxablevalue" runat="server" Text='<%# Eval("Totaltaxablevalue") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIGST" runat="server" Text='<%# Eval("IGST_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCGST" runat="server" Text='<%# Eval("CGST_TAX") %>' />
                            </td>
                            <td><%--  <%# Eval("SGST_TAX").ToString() == "0.00" ? Eval("UGST_TAX") : Eval("SGST_TAX")%>--%>
                                <asp:Label ID="lblSGST" runat="server" Text='<%# Eval("SGST_TAX") %>' />
                                <%--  <asp:Label ID="lblUGSTTAX" runat="server" Text='<%# Eval("UGST_TAX") %>' />--%></td>
                            <td>
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("Cess") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">10A.2.&nbsp; Out of supplies mentioned at 10A, value of supplies made through  e-Commerce Operators attracting TCS (operator wise) [Rate wise] GSTIN of e-commerce operator	</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate of tax</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Total Taxable value</th>
                                <th style="text-align: center;" colspan="4">Amount </th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_10B" runat="server">
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
                                <asp:Label ID="lblRateTax" runat="server" Text='<%# Eval("RateOfTax") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTaxablevalue" runat="server" Text='<%# Eval("Totaltaxablevalue") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGST" runat="server" Text='<%#  Eval("IGST_TAX").ToString()=="0.00"? " - " :Eval("IGST_TAX") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCGST" runat="server" Text='<%# Eval("CGST_TAX").ToString()=="0.00"? " - " :Eval("CGST_TAX") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblSGST" runat="server" Text='<%# Eval("SGST_TAX") %>' />
                                <asp:Label ID="lblUGSTTAX" runat="server" Text='<%# Eval("UGST_TAX") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("Cess").ToString()=="0.00"? " - " :Eval("Cess") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">10B.&nbsp;Inter-State Supplies  [Rate wise]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate of tax</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Total Taxable value</th>
                                <th style="text-align: center;" colspan="4">Amount </th>
                                <%-- <th style="vertical-align: bottom; text-align: center;" rowspan="2">Place of Supply</th>--%>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_10BB" runat="server">
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
                            <td style="text-align: center;">
                                <asp:Label ID="lblRateTax" runat="server" Text='<%# Eval("RateOfTax") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTaxablevalue" runat="server" Text='<%# Eval("Totaltaxablevalue") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGST" runat="server" Text='<%# Eval("IGST_TAX") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCGST" runat="server" Text='<%# Eval("CGST_TAX") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblSGST" runat="server" Text='<%# Eval("SGST_TAX") %>' />
                                <asp:Label ID="lblUGSTTAX" runat="server" Text='<%# Eval("UGST_TAX") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("Cess") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">10B.1.&nbsp;Out of supplies mentioned at 10B, value of supplies made through  e-Commerce Operators attracting TCS (operator wise) [Rate wise]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate of tax</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Total Taxable value</th>
                                <th style="text-align: center;" colspan="4">Amount </th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
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
        </div>


        <!--11A1--Intra-state supplies (Rate Wise) -->
        <!--11A2--Inter-state supplies (Rate Wise) -->
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">11.&nbsp;Consolidated Statement of Advances Received/Advance adjusted in the current tax period/ Amendments of information furnished in earlier tax period</h3>
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
                <h5 style="font-weight: bold;">11A.&nbsp; Advance amount received in the tax period for which invoice has not been issued (tax amount to be added to
output tax liability)</h5>
                <h6 style="font-weight: bold;">I. Information for the current tax period</h6>
                <asp:ListView ID="lvGSTR1_11A1" runat="server">
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
                            <%--     <td>
                                <asp:Label ID="lblInvoiceID" runat="server" Text='<%# Eval("InvoiceID") %>' />
                            </td>--%>
                            <td style="text-align: center;">
                                <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("RATE") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTAXABLEAMOUNT" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSTATENAME" runat="server" Text='<%# Eval("STATENAME") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTAMT" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT")%>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCGSTAMT" runat="server" Text='<%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %><%--   <%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %> --%><%--<asp:Label ID="lblSGSTAMT" runat="server" Text='<%# Eval("SGSTAMT") %>' />
                                <asp:Label ID="lblUGSTAMT" runat="server" Text='<%# Eval("UGSTAMT") %>' />--%></td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCESSAMT" runat="server" Text='<%#  Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">11A.(1)&nbsp;Intra-State supplies (Rate Wise)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Gross Advance(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of supply</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_11A2" runat="server">
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
                            <%--     <td>
                                <asp:Label ID="lblInvoiceID" runat="server" Text='<%# Eval("InvoiceID") %>' />
                            </td>--%>
                            <td style="text-align: center;">
                                <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("RATE") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTAXABLEAMOUNT" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSTATENAME" runat="server" Text='<%# Eval("STATENAME") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTAMT" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCGSTAMT" runat="server" Text='<%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %><%-- <%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %>--%></td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCESSAMT" runat="server" Text='<%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT")  %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">11A.(2)&nbsp;Inter-State Supplies (Rate Wise)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Gross Advance(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of supply</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="Tr1" runat="server">
                                    </tr>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <h5 style="font-weight: bold;">11B.&nbsp; Advance amount received in earlier tax period and adjusted against the supplies being shown in this tax period in
Table Nos. 4, 5, 6 and 7</h5>
                <asp:ListView ID="lvGSTR1_11B1" runat="server">
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
                            <%--     <td>
                                <asp:Label ID="lblInvoiceID" runat="server" Text='<%# Eval("InvoiceID") %>' />
                            </td>--%>
                            <td style="text-align: center;">
                                <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("RATE") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTAXABLEAMOUNT" runat="server" Text='<%# Eval("GrossAdvanceReceived_adjusted") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSTATENAME" runat="server" Text='<%# Eval("state") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTAMT" runat="server" Text='<%# Eval("IGST_TAX_AMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCGSTAMT" runat="server" Text='<%# Eval("CGST_TAX_AMT") %>' />
                            </td>
                            <td style="text-align: center;"><%--<%# Eval("SGST_TAX_AMT").ToString()==null?Eval("UGST_TAX_AMT"):Eval("SGST_TAX_AMT") %>--%>
                                <asp:Label ID="lblSGSTAMT" runat="server" Text='<%# Eval("SGST_TAX_AMT") %>' />
                                <asp:Label ID="lblUGSTAMT" runat="server" Text='<%# Eval("UGST_TAX_AMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCESSAMT" runat="server" Text='<%# Eval("Cess_AMT") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">11B.1.&nbsp;Intra-State Supplies (Rate Wise)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Gross Advance(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of supply</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="Tr1" runat="server">
                                    </tr>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lvGSTR1_11B2" runat="server">
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
                            <%--     <td>
                                <asp:Label ID="lblInvoiceID" runat="server" Text='<%# Eval("InvoiceID") %>' />
                            </td>--%>
                            <td style="text-align: center;">
                                <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("RATE") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTAXABLEAMOUNT" runat="server" Text='<%# Eval("GrossAdvanceReceived_adjusted") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSTATENAME" runat="server" Text='<%# Eval("state") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGSTAMT" runat="server" Text='<%# Eval("IGST_TAX_AMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCGSTAMT" runat="server" Text='<%# Eval("CGST_TAX_AMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblSGSTAMT" runat="server" Text='<%# Eval("SGST_TAX_AMT") %>' />
                                <asp:Label ID="lblUGSTAMT" runat="server" Text='<%# Eval("UGST_TAX_AMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCESSAMT" runat="server" Text='<%# Eval("Cess_AMT") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">11B.2.&nbsp;Inter-State Supplies (Rate Wise)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Gross Advance(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="vertical-align: bottom;" rowspan="2">Place of supply</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <%--<tbody>
                                    <tr id="Tr1" runat="server">
                                    </tr>--%>
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
                <h3 class="box-title">12. HSN/ SAC wise summary of outward supplies</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>
                <%--<div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>--%>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lvGSTR1_12" runat="server">
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
                                <asp:Label ID="lblHSN_SAC" runat="server" Text='<%# Eval("HSN_SAC") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblUQC" runat="server" Text='<%# Eval("UQC") %>' />
                            </td>
                            <%-- <td>
                            <asp:Label ID="lblTotal_Qty" runat="server" Text='<%# Eval("Total_Qty") %>' />
                            </td>--%>
                            <td>
                                <asp:Label ID="lblTotal_value" runat="server" Text='<%# Eval("Total_value") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTotal_Taxable_Value" runat="server" Text='<%# Eval("Total_Taxable_Value") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIGST_TAX_AMT" runat="server" Text='<%# Eval("IGST_TAX_AMT").ToString()=="0.00"? " - " :Eval("IGST_TAX_AMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCGST_TAX_AMT" runat="server" Text='<%# Eval("CGST_TAX_AMT").ToString()=="0.00"? " - " :Eval("CGST_TAX_AMT") %>' />
                            </td>
                            <td style="text-align: center;"><%# Eval("SGST_TAX_AMT").ToString()=="0.00"?(Eval("UGST_TAX_AMT").ToString()=="0.00"?"-":Eval("UGST_TAX_AMT")):(Eval("SGST_TAX_AMT").ToString()=="0.00"?"-":Eval("SGST_TAX_AMT")) %><%-- <%# Eval("SGST_TAX_AMT").ToString().Replace("0.00","-")%> <%# Eval("UGST_TAX_AMT").ToString().Replace("0.00","-")%>--%><%-- <%# Eval("SGST_TAX_AMT").ToString()=="0.00"?(Eval("UGST_TAX_AMT")=="0.00"?"-":Eval("UGST_TAX_AMT")):(Eval("SGST_TAX_AMT")=="0.00"?"-":Eval("SGST_TAX_AMT")) %>--%></td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCess_AMT" runat="server" Text='<%# Eval("Cess_AMT").ToString()=="0.00"? " - " :Eval("Cess_AMT") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">HSN / SAC</th>
                                <th style="vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="vertical-align: bottom;" rowspan="2">UQC</th>
                                <%--<th style="vertical-align: bottom;" rowspan="2">Total Quantity</th>--%>
                                <th style="vertical-align: bottom;" rowspan="2">Total value</th>
                                <th style="vertical-align: bottom;" rowspan="2">Total Taxable Value</th>
                                <th style="text-align: center;" colspan="4">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />
                                    (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />
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
        </div>
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">13. Documents issued during the tax period</h3>
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
                <asp:ListView ID="gv_13_ecommerce" runat="server">
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
                                <asp:Label ID="lblNature" runat="server" Text='<%# Eval("") %>' />
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTotalnumber" runat="server" Text='<%# Eval("") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCancelled" runat="server" Text='<%# Eval("") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblNetIssued" runat="server" Text='<%# Eval("") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFrom" runat="server" Text='<%# Eval("") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTo" runat="server" Text='<%# Eval("") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-striped">
                            <tr>
                                <th rowspan="2">#</th>
                                <th rowspan="2">Nature of document</th>
                                <th colspan="2">Sr. No.</th>
                                <th rowspan="2">Total number</th>
                                <th rowspan="2">Cancelled</th>
                                <th rowspan="2">Net issued</th>
                            </tr>
                            <tr>
                                <th>From</th>
                                <th>To</th>
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


    <%--</div>--%>

    <%--    <div align="right" style="margin-right: 15px;">
        <asp:Button ID="btnAddExport" runat="server" Text="AddExport" Width="100px" Style="display: none;" CssClass="btn btn-primary" />
        <asp:Button ID="btnFileGSTR1" runat="server" Text="File GSTR1" Width="100px" CssClass="btn btn-primary" OnClick="btnFileGSTR1_Click" />
        <asp:Button ID="btnBack" runat="server" Text="Back" Visible="false" Width="100px" CssClass="btn btn-primary" OnClick="btnBack_Click" />
    </div>--%>
</asp:Content>
