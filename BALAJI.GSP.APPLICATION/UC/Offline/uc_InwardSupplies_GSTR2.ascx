<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_InwardSupplies_GSTR2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_InwardSupplies_GSTR2" %>
<div class="box box-primary">
    <div class="box-body">
        <asp:ListView ID="lv_InwardSupplies" runat="server" OnItemDataBound="lv_InwardSupplies_ItemDataBound">
            <EmptyDataTemplate>
                <table class="table table-responsive">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chk" runat="server" /></td>
                    <td>
                        <asp:TextBox ID="txtHSN" Text='<%# Eval("HSN") %>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtDescription" Text='<%# Eval("HSNDescription") %>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddlUQC" runat="server"></asp:DropDownList></td>
                    <td>
                        <asp:TextBox ID="txtQty" Text='<%# DataBinder.Eval(Container.DataItem,"GSt_TRN_OFFLINE_INVOICE_DATAITEM.TotalQuantity")%>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtvalue" Text='<%# DataBinder.Eval(Container.DataItem,"GSt_TRN_OFFLINE_INVOICE_DATAITEM.TotalValue")%>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem,"GSt_TRN_OFFLINE_INVOICE_DATAITEM.TotalTaxableValue")%>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtIGST" Text='<%# DataBinder.Eval(Container.DataItem,"GSt_TRN_OFFLINE_INVOICE_DATAITEM.IGSTAmt")%>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCGST" Text='<%# DataBinder.Eval(Container.DataItem,"GSt_TRN_OFFLINE_INVOICE_DATAITEM.CGSTAmt")%>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtSGST" Text='<%# DataBinder.Eval(Container.DataItem,"GSt_TRN_OFFLINE_INVOICE_DATAITEM.SGSTAmt")%>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCess" Text='<%# DataBinder.Eval(Container.DataItem,"GSt_TRN_OFFLINE_INVOICE_DATAITEM.CessAmt")%>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:LinkButton ID="btnAddMore" runat="server" CssClass="btn" data-trigger="hover" data-placement="right" title="Add More Line Items"><span class="glyphicon glyphicon-plus-sign" style="color:green"></span></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th>
                                <asp:CheckBox ID="Chk1" runat="server" /></th>
                            <th rowspan="2">HSN</th>
                            <th rowspan="2">Description</th>
                            <th rowspan="2">UQC</th>
                            <th rowspan="2">Total Quantity</th>
                            <th rowspan="2">Total Value</th>
                            <th rowspan="2">Total Taxable Value</th>
                            <th colspan="4">Amount of Tax</th>
                            <th rowspan="2">Actions</th>
                        </tr>
                        <tr>
                            <th>IGST</th>
                            <th>CGST</th>
                            <th>SGST/UGST</th>
                            <th>Cess</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server" />
                    </tbody>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </div>
</div>
