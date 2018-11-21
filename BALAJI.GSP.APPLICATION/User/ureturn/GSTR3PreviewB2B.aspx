<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/User.master" CodeBehind="GSTR3PreviewB2B.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.GSTR3PreviewB2B" %>

<%@ Register Src="~/UC/uc_GSTR_Taxpayer.ascx" TagPrefix="uc1" TagName="uc_GSTR_Taxpayer" %>



<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%-- <div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">GSTR3-OUTWARD SUPPLIES MADE BY THE TAXPAYER</h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>--%>

    <div class="content-header">
        <h1>GSTR3
        <small>Outward supplies made by the taxpayer</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">GSTR3</li>
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
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success" OnClick="lkvGSTR3_Click"><i class="fa fa-cloud-download"></i> Import - 3</asp:LinkButton>
                </div>--%>
            </div>
        </div>
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">3. Turnover</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
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
                            <td><%# Eval("") %> </td>
                            <td><%# Eval("") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;">Type of Turnover</th>
                                <th style="text-align: center;">Amount</th>
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
                <h4 class="box-title">4. Outward Supplies</h4>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <h5 class="box-title">4.1 Inter-State supplies (Net Supply for the month)</h5>
                <asp:ListView ID="lv_GSTR3_4_1_A" runat="server">
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
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CessAmt").ToString()=="0.00"? " - " :Eval("CessAmt") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="5">A. Taxable supplies (other than reverse charge and zero rated supply) [Tax Rate Wise]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable Value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="2">Amount of Tax</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR3_4_1_B" runat="server">
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
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>

                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="5">B. Supplies attracting reverse charge-Tax payable by recipient of supply</th>
                            </tr>

                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable Value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="2">Amount of Tax</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR3_4_1_C" runat="server">
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
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>

                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="5">C. Zero rated supply made with payment of Integrated Tax</th>
                            </tr>

                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable Value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="2">Amount of Tax</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR3_4_1_D" runat="server">
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
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("CessAmt")%></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>

                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="5">D. Out of the supplies mentioned at A, the value of supplies made though an e-commerce operator attracting TCS-[Ratewise]</th>
                            </tr>

                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Rate</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable Value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="2">Amount of Tax</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                 <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <h5 class="box-title">4.2 Intra-State supplies (Net supply for the month)</h5>
                <asp:ListView ID="lv_GSTR3_4_2_A" runat="server">
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
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="6">A. Taxable supplies (other than reverse charge) [Tax Rate wise]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable Value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="3">Amount of Tax</th>
                            </tr>
                            <tr>
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

                <asp:ListView ID="lv_GSTR3_4_2_B" runat="server">
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
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT")=="0.00"?Eval("UGSTAMT"):"-"):(Eval("SGSTAMT")=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="6">B. Supplies attracting reverse charge- Tax payable by the recipient of supply</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable Value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="3">Amount of Tax</th>
                            </tr>
                            <tr>
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

                <asp:ListView ID="lv_GSTR3_4_2_C" runat="server">
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
                            <td style="text-align: center;"><%# Eval("RATE") %></td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %></td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %></td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">C. Out of the supplies mentioned at A, the value of supplies made though an e-commerce operator attracting TCS [Ratewise]</th>
                            </tr>
                            <tr>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable Value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="3">Amount of Tax</th>
                            </tr>
                            <tr>
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
                <h5 class="box-title">4.3 Tax effect of amendments made in respect of outward supplies</h5>
                <h5 class="box-title">(I) Inter-State supplies</h5>
                <asp:ListView ID="lv_GSTR3_4_3_A" runat="server">
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
                            <td><%# Eval("Rate") %> </td>
                            <td><%# Eval("") %> </td>
                            <td><%# Eval("") %> </td>
                            <td><%# Eval("") %></td>
                            <td><%# Eval("") %> </td>
                            <td><%# Eval("") %> </td>
                            <td><%# Eval("") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">A. Taxable supplies (other than reverse charge) [Tax Rate wise]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center;" rowspan="2">Net differential value</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="5">Amount of Tax</th>
                            </tr>
                            <tr>
                                <th>IGST<br />(<i class="fa fa-inr"></i>)</th>
                                <th>CGST<br />(<i class="fa fa-inr"></i>)</th>
                                <th>SGST<br />(<i class="fa fa-inr"></i>)</th>
                                <th>UGST<br />(<i class="fa fa-inr"></i>)</th>
                                <th>Cess<br />(<i class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR3_4_3_B" runat="server">
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
                            <td><%# Eval("Differential_value") %></td>
                            <td><%# Eval("IGSTTax") %> </td>
                            <td><%# Eval("CGSTTax") %></td>
                            <td><%# Eval("SGSTTax") %>
                                <%# Eval("UGSTTax") %></td>
                            <td><%# Eval("Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">B. Zero rated supply made with payment of Integrated Tax [Rate wise]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center;" rowspan="2">Net differential value</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="4">Amount of Tax</th>
                            </tr>
                            <tr>
                                <th>IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <asp:ListView ID="lv_GSTR3_4_3_C" runat="server">
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
                            <td><%# Eval("Differential_value") %> </td>
                            <td><%# Eval("IGSTTax") %> </td>
                            <td><%# Eval("CGSTTax") %></td>
                            <td><%# Eval("SGSTTax") %>
                                <%# Eval("UGSTTax") %></td>
                            <td><%# Eval("Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">C. Out of the Supplies mentioned at A, the value of supplies made though an e-commerce operator attracting TCS</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center;" rowspan="2">Net differential value</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="4">Amount of Tax</th>
                            </tr>
                            <tr>
                                <th>IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>SGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <h5 class="box-title">(II) Intra-state supplies</h5>
                <asp:ListView ID="lv_GSTR3_4_3_2_A" runat="server">
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
                            <td><%# Eval("Rate") %> </td>
                            <td><%# Eval("Differential_value") %></td>
                            <td><%# Eval("IGSTTax") %> </td>
                            <td><%# Eval("CGSTTax") %> </td>
                            <td><%# Eval("SGSTTax") %>
                           <%# Eval("UGSTTax") %> </td>
                            <td><%# Eval("Cess") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">A Taxable supplies (other than reverse charge) [Rate wise]</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center;" rowspan="2">Net differential value</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="4">Amount of Tax</th>
                            </tr>
                            <tr>
                                <th>IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR3_4_3_2_B" runat="server">
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
                            <td><%# Eval("Rate") %> </td>
                            <td><%# Eval("Differential_value") %></td>
                            <td><%# Eval("IGSTTax") %></td>
                            <td><%# Eval("CGSTTax") %> </td>
                            <td><%# Eval("SGSTTax") %>
                                <%# Eval("UGSTTax") %></td>
                            <td><%# Eval("Cess") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">B. Out of the supplies mentioned at A, the value of supplies made though an e-commerce operator attracting TCS</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="vertical-align: bottom;" rowspan="2">Rate</th>
                                <th style="text-align: center;" rowspan="2">Net differential value</th>
                                <th style="text-align: center; vertical-align: bottom;" colspan="4">Amount of Tax</th>
                            </tr>
                            <tr>
                                <th>IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th>Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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

                <h3 class="box-title">5. Inward supplies attracting reverse charge including import of services (Net of advance adjustments)</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3_5_A_1" runat="server">
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
                            <td style="text-align: center;"><%# Eval("RateOfTax") %> </td>
                            <td style="text-align: center;"><%# Eval("TaxableValue") %> </td>
                            <td style="text-align: center;"><%# Eval("IGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT") %> </td>

                            <td style="text-align: center;"><%# Eval("SGST_TAX_AMT") %>
                                <%# Eval("UGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("Cess") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">

                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate of tax</th>
                                <th style="text-align: center;" rowspan="2">Taxable Value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount of Tax</th>
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
                <asp:ListView ID="lv_GSTR3_5_A_2" runat="server">
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
                            <td style="text-align: center;"><%# Eval("RateOfTax") %> </td>
                            <td style="text-align: center;"><%# Eval("TaxableValue") %> </td>
                            <td style="text-align: center;"><%# Eval("IGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT") %> </td>

                            <td style="text-align: center;"><%# Eval("SGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("UGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("Cess") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">

                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate of tax</th>
                                <th style="text-align: center;" rowspan="2">Taxable Value</th>
                                <th style="text-align: center;" colspan="5">Amount of Tax</th>
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
                <asp:ListView ID="lv_GSTR3_5_B_1" runat="server">
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
                            <td style="text-align: center;"><%# Eval("RateOfTax") %> </td>
                           <%-- <td style="text-align: center;"><%# Eval("TaxableValue") %> </td>--%>
                            <td style="text-align: center;"><%# Eval("IGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("SGST_TAX_AMT") %>
                                <%# Eval("UGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("Cess") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">

                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate of tax</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable Value</th>
                                <th style="text-align: center;" colspan="5">Amount of Tax</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center; vertical-align: bottom;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR3_5_B_2" runat="server">
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
                            <td><%# Eval("RateOfTax") %> </td>
                           <%-- <td><%# Eval("TaxableValue") %> </td>--%>
                            <td><%# Eval("IGST_TAX_AMT") %> </td>
                            <td><%# Eval("CGST_TAX_AMT") %> </td>
                            <td><%# Eval("SGST_TAX_AMT") %>
                                <%# Eval("UGST_TAX_AMT") %> </td>
                            <td><%# Eval("Cess") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">5B. Tax effect of amendments in respect of supplies attracting reverse charge</th>
                            </tr>

                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate of tax</th>
                                <th style="text-align: center;" rowspan="2">Differential Taxable Value</th>
                                <th style="text-align: center;" colspan="5">Amount of Tax</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/ UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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

                <h3 class="box-title">6. Input tax credit ITC on inward taxable supplies, including imports and ITC received from ISD [Net of debit notes/credit notes]</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3_6_1" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td style="text-align: center;"><%# Eval("TaxableValue") %> </td>
                            <td style="text-align: center;"><%# Eval("IGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("SGST_TAX_AMT") %>
                                <%# Eval("UGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("Cess") %> </td>
                            <td style="text-align: center;"><%# Eval("ITC_IGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("ITC_CGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("ITC_SGST_TAX_AMT") %>
                                <%# Eval("ITC_UGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("ITC_Cess") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="11">1. On account of supplies received and debit notes/credit notes received during the current tax period</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable Value</th>
                                <th style="text-align: center;" colspan="4">Amount of Tax</th>
                                <th style="text-align: center;" colspan="4">Amount of ITC</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <asp:ListView ID="lv_GSTR3_6_2" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td style="text-align: center;"><%# Eval("TaxableValue") %> </td>
                            <td style="text-align: center;"><%# Eval("IGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("CGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("SGST_TAX_AMT") %>
                                <%# Eval("UGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("Cess") %> </td>
                            <td style="text-align: center;"><%# Eval("ITC_IGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("ITC_CGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("ITC_SGST_TAX_AMT") %>
                                <%# Eval("ITC_UGST_TAX_AMT") %> </td>
                            <td style="text-align: center;"><%# Eval("ITC_Cess") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="11">(II) On account of amendments made (of the details furnished in earlier tax periods)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable Value</th>
                                <th style="text-align: center;" colspan="4">Amount of Tax</th>
                                <th style="text-align: center;" colspan="4">Amount of ITC</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br /><br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">Cess<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">7. Addition and reduction of amount in output tax for mismatch and other reasons</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3_7" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("OutputLiability") %> </td>
                            <td><%# Eval("IGST_TAX_AMT") %> </td>
                            <td><%# Eval("CGST_TAX_AMT") %> </td>
                            <td><%# Eval("SGST_TAX_AMT") %>
                                <%# Eval("UGST_TAX_AMT") %> </td>
                            <td><%# Eval("Cess") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Add to or reduce from output liability</th>
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
            </div>
        </div>


        <div class="box box-danger">
            <div class="box-header with-border">

                <h3 class="box-title">8. Total tax liability</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3_8A" runat="server">
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
                            <%-- <td><%# Eval("INVOICEID") %> </td>
                            <td><%# Eval("INVOICENO") %> </td>--%>
                            <td style="text-align: center;"><%# Eval("RATE") %> </td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %> </td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %> </td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %> </td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %> </td>


                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">8A. On outward supplies</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate of Tax</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount of tax</th>
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

                <asp:ListView ID="lv_GSTR3_8_B" runat="server">
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
                            <%--    <td><%# Eval("INVOICEID") %> </td>
                            <td><%# Eval("INVOICENO") %> </td>--%>
                            <td style="text-align: center;"><%# Eval("RATE") %> </td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %> </td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %> </td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %> </td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %> </td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %> </td>

                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">8B. On inward supplies attracting reverse charge</th>
                            </tr>

                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate of Tax</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount of tax</th>
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

                <asp:ListView ID="lv_GSTR3_8_C" runat="server">
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
                            <%-- <td><%# Eval("INVOICEID") %> </td>
                            <td><%# Eval("INVOICENO") %> </td>--%>
                            <td style="text-align: center;"><%# Eval("RATE") %> </td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %> </td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %> </td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %> </td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %> </td>

                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">8C. On account of Input Tax Credit Reversal/reclaim</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate of Tax</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable value<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount of tax</th>
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

                <asp:ListView ID="lv_GSTR3_8_D" runat="server">
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
                            <%--<td><%# Eval("INVOICEID") %> </td>
                            <td><%# Eval("INVOICENO") %> </td>--%>
                            <td style="text-align: center;"><%# Eval("RATE") %> </td>
                            <td style="text-align: center;"><%# Eval("TAXABLEAMOUNT") %> </td>
                            <td style="text-align: center;"><%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %> </td>
                            <td style="text-align: center;"><%# Eval("CGSTAMT").ToString()=="0.00"? " - " :Eval("CGSTAMT") %> </td>
                            <td style="text-align: center;"><%# Eval("SGSTAMT").ToString()=="0.00"?(Eval("UGSTAMT").ToString()=="0.00"?"-":Eval("UGSTAMT")):(Eval("SGSTAMT").ToString()=="0.00"?"-":Eval("SGSTAMT")) %></td>
                            <td style="text-align: center;"><%# Eval("CESSAMT").ToString()=="0.00"? " - " :Eval("CESSAMT") %> </td>

                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="7">8D. On account of mismatch/ rectification /other reasons</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Rate of Tax</th>
                                <th style="vertical-align: bottom; text-align: center;" rowspan="2">Taxable value(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;" colspan="4">Amount of tax</th>
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

                <h3 class="box-title">9. Credit of TDS and TCS</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3_9_A" runat="server">
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
                            <td><%# Eval("IGST_TAX_AMT") %> </td>
                            <td><%# Eval("CGST_TAX_AMT") %> </td>
                            <td><%# Eval("SGST_TAX_AMT") %>
                                <%# Eval("UGST_TAX_AMT") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">

                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>

                                <th style="text-align: center;" colspan="3">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">CGST<br /> (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align: center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>

                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR3_9_B" runat="server">
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
                            <td><%# Eval("IGST_TAX_AMT") %> </td>
                            <td><%# Eval("CGST_TAX_AMT") %> </td>
                            <td><%# Eval("SGST_TAX_AMT") %>
                                <%# Eval("UGST_TAX_AMT") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">

                            <tr>
                                <th rowspan="2">#</th>

                                <th style="text-align: center;" colspan="3">Amount</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">IGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                <h3 class="box-title">10. Interest liability (Interest as on ...............)</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3_10A" runat="server">
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
                            <td><%# Eval("OnAccount") %> </td>
                            <td><%# Eval("Outputliability") %> </td>
                            <td><%# Eval("ITC_MismatchedInvoice") %> </td>
                            <td><%# Eval("ITCreversal") %> </td>
                            <td><%# Eval("ExcessReduction") %> </td>
                            <td><%# Eval("InterestOnRectification") %> </td>
                            <td><%# Eval("Interestliability") %> </td>
                            <td><%# Eval("DelayInPayment") %> </td>
                            <td><%# Eval("interest_liability") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="10">Integrated Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align:bottom;">#</th>
                                <th style="vertical-align:bottom;">On account of</th>
                                <th style="vertical-align:bottom;">Output liability on mismatch</th>
                                <th style="vertical-align:bottom;">ITC claimed on mismatched invoice</th>
                                <th style="vertical-align:bottom;">Onaccount of other ITC reversal</th>
                                <th style="vertical-align:bottom;">Undue excess claims orexcess reduction</th>
                                <th style="vertical-align:bottom;">Credit of interest on rectification of mismatch</th>
                                <th style="vertical-align:bottom;">Interest liability carry forward</th>
                                <th style="vertical-align:bottom;">Delay inpayment of tax</th>
                                <th style="vertical-align:bottom;">Total interest liability</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR3_10B" runat="server">
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
                            <td><%# Eval("OnAccount") %> </td>
                            <td><%# Eval("Outputliability") %> </td>
                            <td><%# Eval("ITC_MismatchedInvoice") %> </td>
                            <td><%# Eval("ITCreversal") %> </td>
                            <td><%# Eval("ExcessReduction") %> </td>
                            <td><%# Eval("InterestOnRectification") %> </td>
                            <td><%# Eval("Interestliability") %> </td>
                            <td><%# Eval("DelayInPayment") %> </td>
                            <td><%# Eval("interest_liability") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="10">Central Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align:bottom;">#</th>
                                <th style="vertical-align:bottom;">On account of</th>
                                <th style="vertical-align:bottom;">Output liability on mismatch</th>
                                <th style="vertical-align:bottom;">ITC claimed on mismatched invoice</th>
                                <th style="vertical-align:bottom;">Onaccount of other ITC reversal</th>
                                <th style="vertical-align:bottom;">Undue excess claims orexcess reduction</th>
                                <th style="vertical-align:bottom;">Credit of interest on rectification of mismatch</th>
                                <th style="vertical-align:bottom;">Interest liability carry forward</th>
                                <th style="vertical-align:bottom;">Delay inpayment of tax</th>
                                <th style="vertical-align:bottom;">Total interest liability</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR3_10C" runat="server">
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
                            <td><%# Eval("OnAccount") %> </td>
                            <td><%# Eval("Outputliability") %> </td>
                            <td><%# Eval("ITC_MismatchedInvoice") %> </td>
                            <td><%# Eval("ITCreversal") %> </td>
                            <td><%# Eval("ExcessReduction") %> </td>
                            <td><%# Eval("InterestOnRectification") %> </td>
                            <td><%# Eval("Interestliability") %> </td>
                            <td><%# Eval("DelayInPayment") %> </td>
                            <td><%# Eval("interest_liability") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="10">State / UT Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th style="vertical-align:bottom;">On account of</th>
                                <th style="vertical-align:bottom;">Output liability on mismatch</th>
                                <th style="vertical-align:bottom;">ITC claimed on mismatched invoice</th>
                                <th style="vertical-align:bottom;">Onaccount of other ITC reversal</th>
                                <th style="vertical-align:bottom;">Undue excess claims orexcess reduction</th>
                                <th style="vertical-align:bottom;">Credit of interest on rectification of mismatch</th>
                                <th style="vertical-align:bottom;">Interest liability carry forward</th>
                                <th style="vertical-align:bottom;">Delay inpayment of tax</th>
                                <th style="vertical-align:bottom;">Total interest liability</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR3_10D" runat="server">
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
                            <td><%# Eval("OnAccount") %> </td>
                            <td><%# Eval("Outputliability") %> </td>
                            <td><%# Eval("ITC_MismatchedInvoice") %> </td>
                            <td><%# Eval("ITCreversal") %> </td>
                            <td><%# Eval("ExcessReduction") %> </td>
                            <td><%# Eval("InterestOnRectification") %> </td>
                            <td><%# Eval("Interestliability") %> </td>
                            <td><%# Eval("DelayInPayment") %> </td>
                            <td><%# Eval("interest_liability") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="10">Cess</th>
                            </tr>
                            <tr>
                                <th style="vertical-align:bottom;">#</th>
                                <th style="vertical-align:bottom;">On account of</th>
                                <th style="vertical-align:bottom;">Output liability on mismatch</th>
                                <th style="vertical-align:bottom;">ITC claimed on mismatched invoice</th>
                                <th style="vertical-align:bottom;">Onaccount of other ITC reversal</th>
                                <th style="vertical-align:bottom;">Undue excess claims orexcess reduction</th>
                                <th style="vertical-align:bottom;">Credit of interest on rectification of mismatch</th>
                                <th style="vertical-align:bottom;">Interest liability carry forward</th>
                                <th style="vertical-align:bottom;">Delay inpayment of tax</th>
                                <th style="vertical-align:bottom;">Total interest liability</th>
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

                <h3 class="box-title">11. Late Fee</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3_11" runat="server">
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
                            <td><%# Eval("OnAccount") %> </td>
                            <td><%# Eval("CGST_Tax") %> </td>
                            <td><%# Eval("SGST_Tax") %>
                                <%# Eval("UGST_Tax") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align:bottom;">On account of</th>
                                <th style="text-align:center;">CGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                <th style="text-align:center;">SGST/UTGST<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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

                <h3 class="box-title">12. Tax payable and paid</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3_12A" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("Taxpayable") %> </td>
                            <td><%# Eval("cash") %> </td>
                            <td><%# Eval("ITC_IGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_CGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_SGSTTax_Paid") %>
                                <%# Eval("ITC_UGSTTax_Paid") %> </td>
                            <td><%# Eval("Cess") %> </td>
                            <%--  <td><%# Eval("TaxPaid") %> </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="9">Integrated Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Paid in cash</th>
                                <th style="text-align: center;" colspan="4">Paid through ITC</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Tax Paid</th>
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
                <asp:ListView ID="lv_GSTR3_12B" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("Taxpayable") %> </td>
                            <td><%# Eval("cash") %> </td>
                            <td><%# Eval("ITC_IGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_CGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_SGSTTax_Paid") %>
                                <%# Eval("ITC_UGSTTax_Paid") %> </td>
                            <td><%# Eval("Cess") %> </td>
                            <%-- <td><%# Eval("TaxPaid") %> </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="9">Central Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Paid in cash</th>
                                <th style="text-align: center;" colspan="4">Paid through ITC</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Tax Paid</th>
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
                <asp:ListView ID="lv_GSTR3_12C" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("Taxpayable") %> </td>
                            <td><%# Eval("cash") %> </td>
                            <td><%# Eval("ITC_IGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_CGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_SGSTTax_Paid") %>
                                <%# Eval("ITC_UGSTTax_Paid") %> </td>
                            <td><%# Eval("Cess") %> </td>
                            <%--  <td><%# Eval("TaxPaid") %> </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="9">State/UT Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Paid in cash</th>
                                <th style="text-align: center;" colspan="4">Paid through ITC</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Tax Paid</th>
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
                <asp:ListView ID="lv_GSTR3_12D" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("Taxpayable") %> </td>
                            <td><%# Eval("cash") %> </td>
                            <td><%# Eval("ITC_IGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_CGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_SGSTTax_Paid") %>
                                <%# Eval("ITC_UGSTTax_Paid") %> </td>
                            <td><%# Eval("Cess") %> </td>
                            <%--<td><%# Eval("TaxPaid") %> </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="9">Cess</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Taxable value</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Paid in cash</th>
                                <th style="text-align: center;" colspan="4">Paid through ITC</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Tax Paid</th>
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

                <h3 class="box-title">13. Interest, Late Fee and any other amount (other than tax) payable and paid</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3_13_1" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("AmountPayable") %> </td>
                            <td><%# Eval("AmountPaid") %> </td>
                            <%--Ankita<td><%# Eval("ITC_IGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_CGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_SGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_UGSTTax_Paid") %> </td>
                            <td><%# Eval("Cess") %> </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th>Description</th>
                                <th>Amount payable</th>
                                <th>Amount Paid</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR3_13_2" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("AmountPayable") %> </td>
                            <td><%# Eval("AmountPaid") %> </td>
                            <%--Ankita <td><%# Eval("ITC_IGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_CGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_SGSTTax_Paid") %> </td>
                            <td><%# Eval("ITC_UGSTTax_Paid") %> </td>
                            <td><%# Eval("Cess") %> </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th>Description</th>
                                <th>Amount payable</th>
                                <th>Amount Paid</th>
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
                <h3 class="box-title">14.&nbsp; Refund claimed from Electronic cash ledger</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3_14A" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("Tax") %> </td>
                            <td><%# Eval("Interest") %> </td>
                            <td><%# Eval("Penalty") %> </td>
                            <td><%# Eval("Fee") %> </td>
                            <td><%# Eval("Other") %> </td>
                            <td><%# Eval("DebitEntryNo") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">A.&nbsp;Integrated tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax</th>
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
                <asp:ListView ID="lv_GSTR3_14B" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("Tax") %> </td>
                            <td><%# Eval("Interest") %> </td>
                            <td><%# Eval("Penalty") %> </td>
                            <td><%# Eval("Fee") %> </td>
                            <td><%# Eval("Other") %> </td>
                            <td><%# Eval("DebitEntryNo") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">B.&nbsp;Central Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax</th>
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
                <asp:ListView ID="lv_GSTR3_14C" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("Tax") %> </td>
                            <td><%# Eval("Interest") %> </td>
                            <td><%# Eval("Penalty") %> </td>
                            <td><%# Eval("Fee") %> </td>
                            <td><%# Eval("Other") %> </td>
                            <td><%# Eval("DebitEntryNo") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">C.&nbsp;State/UT Tax</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax</th>
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
                <asp:ListView ID="lv_GSTR3_14D" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("Tax") %> </td>
                            <td><%# Eval("Interest") %> </td>
                            <td><%# Eval("Penalty") %> </td>
                            <td><%# Eval("Fee") %> </td>
                            <td><%# Eval("Other") %> </td>
                            <td><%# Eval("DebitEntryNo") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="8">D.&nbsp;Cess</th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Description</th>
                                <th>Tax</th>
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

                <h3 class="box-title">15. Debit entries in electronic cash/Credit ledger for tax/interest payment [to be populated after payment of tax and submissions of return]</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3_15A" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("TaxPaid") %> </td>
                            <td><%# Eval("ITC_IGSTTax") %> </td>
                            <td><%# Eval("ITC_CGSTTax") %> </td>
                            <td><%# Eval("ITC_SGSTTax") %>
                                <%# Eval("ITC_UGSTTax") %> </td>
                            <td><%# Eval("Cess") %> </td>
                            <td><%# Eval("Interest") %> </td>
                            <td><%# Eval("LateFes") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="9">A.Integrated tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Tax paid in cash</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Tax paid through ITC</th>
                                <th style="text-align: center;" colspan="4">Interest</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Late fee</th>
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
                <asp:ListView ID="lv_GSTR3_15B" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("TaxPaid") %> </td>
                            <td><%# Eval("ITC_IGSTTax") %> </td>
                            <td><%# Eval("ITC_CGSTTax") %> </td>
                            <td><%# Eval("ITC_SGSTTax") %>
                                <%# Eval("ITC_UGSTTax") %> </td>
                            <td><%# Eval("Cess") %> </td>
                            <td><%# Eval("Interest") %> </td>
                            <td><%# Eval("LateFes") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="9">B.Central Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Tax paid in cash</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Tax paid through ITC</th>
                                <th style="text-align: center;" colspan="4">Interest</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Late fee</th>
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
                <asp:ListView ID="lv_GSTR3_15C" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("TaxPaid") %> </td>
                            <td><%# Eval("ITC_IGSTTax") %> </td>
                            <td><%# Eval("ITC_CGSTTax") %> </td>
                            <td><%# Eval("ITC_SGSTTax") %>
                                <%# Eval("ITC_UGSTTax") %> </td>
                            <td><%# Eval("Cess") %> </td>
                            <td><%# Eval("Interest") %> </td>
                            <td><%# Eval("LateFes") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="9">C.State/UT Tax</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Tax paid in cash</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Tax paid through ITC</th>
                                <th style="text-align: center;" colspan="4">Interest</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Late fee</th>
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
                <asp:ListView ID="lv_GSTR3_15D" runat="server">
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
                            <td><%# Eval("Description") %> </td>
                            <td><%# Eval("TaxPaid") %> </td>
                            <td><%# Eval("ITC_IGSTTax") %> </td>
                            <td><%# Eval("ITC_CGSTTax") %> </td>
                            <td><%# Eval("ITC_SGSTTax") %>
                                <%# Eval("ITC_UGSTTax") %> </td>
                            <td><%# Eval("Cess") %> </td>
                            <td><%# Eval("Interest") %> </td>
                            <td><%# Eval("LateFes") %> </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="9">D.Cess</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;" rowspan="2">#</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Description</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Tax paid in cash</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Tax paid through ITC</th>
                                <th style="text-align: center;" colspan="4">Interest</th>
                                <th style="text-align: center; vertical-align: bottom;" rowspan="2">Late fee</th>
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








    <%--<div align="right" style="margin-right: 15px;">
        <asp:Button ID="btnAddExport" runat="server" Text="Add Export" Width="100px" CssClass="btn btn-primary" />

        <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" Visible="false" CssClass="btn btn-primary" OnClick="btnBack_Click" />

        </div>--%>
</asp:Content>
