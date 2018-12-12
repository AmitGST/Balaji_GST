<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="GSTR3BDetails.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.GSTR3BDetails" %>

<%@ Register Src="~/UC/UC_Gstr/uc_Gstr3B_Tileview.ascx" TagPrefix="uc1" TagName="uc_Gstr3B_Tileview" %>
<%@ Register Src="~/UC/UC_Invoice/uc_invoiceMonth.ascx" TagPrefix="uc1" TagName="uc_invoiceMonth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:uc_invoiceMonth runat="server" Visible="false" ID="uc_invoiceMonth" />

    <div class="content-header">
        <h1>GSTR-3B
        <small>Monthly Returns</small> </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="#">Returns</a></li>
            <li class="active">GSTR3B</li>
        </ol>
    </div>
    <!--Main Content-->
    <div class="content">
        <div class="box box-primary">
            <div class="box-body">
                <asp:ListView ID="lvGstr3B_1" runat="server">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Container.DataItemIndex+1 %>.
                            </td>
                            <td style="width: 55%">
                                <asp:Label ID="lblDescrip" runat="server" Text='<%# Eval("SUBSECTIONNAME") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalValue" disabled runat="server" Text='<%# Eval("TOTALTAXABLEVALUE") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIGST" disabled runat="server" Text='<%# Eval("IGSTAMT") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCGST" disabled runat="server" Text='<%# Eval("CGSTAMT") %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtSGST" disabled runat="server" Text='<%# Eval("SGSTAMT") %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtCess" disabled runat="server" Text='<%# Eval("CESSAMT") %>'></asp:TextBox></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive">
                            <thead>
                                <tr>
                                    <th style="width: 5%">S.No</th>
                                    <th style="width: 55%">Description</th>
                                    <th>Total Taxable Value</th>
                                    <th>IntegratedTax</th>
                                    <th>CentralTax</th>
                                    <th>StateTex</th>
                                    <th>CESS</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
        </div>
        <div class="box box-primary">
            <div class="box-body">
                <asp:ListView ID="lv_Gstr3B_3_2" runat="server">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex+1 %>.</td>
                            <td>
                                <asp:TextBox ID="txtCGST" disabled runat="server"  Text='<%# Eval("PlaceOfSupply") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalValue" disabled runat="server" Text='<%# Eval("TotalTaxableValue") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIGST" disabled runat="server" Text='<%# Eval("AmountOfIntegratedTax") %>'></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive">
                            <thead>
                                <tr>
                                    <th style="width: 2%">S.No</thstyle="width:>
                                    <th style="width:2%">Place Of Supply</th>
                                    <th style="width:2%">Total Taxable Value</th>
                                    <th >IntegratedTax</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
        </div>
        <div class="box box-primary">
            <div class="box-body">
                <asp:ListView ID="lv_Gstr3B_5" runat="server">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex+1 %>.</td>
                            <td>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalValue" disabled runat="server" Text='<%# Eval("InterStateSupplies") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIGST" disabled runat="server" Text='<%# Eval("IntraStateSupplies") %>'></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive">
                            <thead>
                                <tr>
                                    <th style="width:2%"">S.No</th>
                                    <th style="width:22%">Description</th>
                                    <th style="width:2%">Inter-State</th>
                                    <th>Intra-State</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
        </div>
        <div class="box box-primary">
            <div class="box-body">
                <asp:ListView ID="lv_GSTR3B_5_1" runat="server">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex+1 %>.</td>
                            <td>
                                <asp:TextBox ID="txtCGST" disabled runat="server" Text='<%# Eval("Description") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalValue" disabled runat="server" Text='<%# Eval("InterStateSupplies") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIGST" disabled runat="server" Text='<%# Eval("IntraStateSupplies") %>'></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive">
                            <thead>
                                <tr>
                                    <th style="width: 5%">S.No</th>
                                    <th style="width: 70%">Description</th>
                                    <th style="width: 15%">Inter-State</th>
                                    <th style="width: 10%">Intra-State</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
        </div>
        <div class="box box-primary">
            <div class="box-body">
                <asp:ListView ID="lvGSTR3B_ITC" runat="server">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex+1 %></td>

                            <td>
                                <asp:TextBox ID="txtCGST" disabled runat="server" Text='<%# Eval("Description") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalValue" disabled runat="server" Text='<%# Eval("InterStateSupplies") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIGST" disabled runat="server" Text='<%# Eval("IntraStateSupplies") %>'></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive">
                            <thead>
                                <tr>
                                    <th style="width: 5%">S.No</th>
                                    <th style="width: 70%">Description</th>
                                    <th style="width: 15%">Inter-State</th>
                                    <th style="width: 10%">Intra-State</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
            <div class="box-footer">
                <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-danger"><i class="fa fa-backward"></i>&nbsp;Back</asp:LinkButton>
                <%--<asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-danger" />--%>
            </div>
        </div>
    </div>

</asp:Content>
