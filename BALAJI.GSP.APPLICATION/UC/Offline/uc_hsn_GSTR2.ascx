<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_hsn_GSTR2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_hsn_GSTR2" %>
<div class="box box-primary">
    <div class="box-body">
        <asp:ListView ID="lv_HSNInwardSupplies" runat="server" OnItemDataBound="lv_HSNInwardSupplies_ItemDataBound">
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
                        <asp:CheckBox ID="chk" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txt_HSN" Text='<%# Eval("GST_TRN_OFFLINE_INVOICE.HSN") %>' Width="50px" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Description" Text='<%# Eval("GST_TRN_OFFLINE_INVOICE.HSNDescription") %>' runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_UQC" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Quantity" Text='<%# DataBinder.Eval(Container.DataItem,"TotalQuantity") %>' Width="30px" Class="form-control" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Value" Text='<%# DataBinder.Eval(Container.DataItem,"TotalValue") %>' Width="50px" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Taxable_value" Text='<%# DataBinder.Eval(Container.DataItem,"TotalTaxableValue") %>' Class="form-control" Width="50px" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_IGST" Text='<%# DataBinder.Eval(Container.DataItem,"IGSTAmt") %>' Class="form-control" Width="50px" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_SGSTUTGST" Text='<%# DataBinder.Eval(Container.DataItem,"SGSTAmt") %>' Class="form-control" Width="50px" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_CGST" Text='<%# DataBinder.Eval(Container.DataItem,"CGSTAmt") %>' Class="form-control" Width="50px" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Cess" Text='<%# DataBinder.Eval(Container.DataItem,"CessAmt") %>' Class="form-control" Width="50px" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:LinkButton ID="btnAddMore" runat="server" CssClass="btn" data-trigger="hover" data-placement="right" title="Add More Line Items"><span class="glyphicon glyphicon-plus-sign" style="color:green"></span></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th rowspan="2">
                                <asp:CheckBox ID="Chk1" runat="server" /></th>
                            <th rowspan="2">HSN</th>
                            <th rowspan="2">Description</th>
                            <th rowspan="2">UQC</th>
                            <th rowspan="2">Total Quantity</th>
                            <th rowspan="2">Total Value &nbsp;(<i class="fa fa-rupee"></i>)</th>
                            <th rowspan="2">Total Taxable Value &nbsp;(<i class="fa fa-rupee"></i>)</th>
                            <th colspan="4">Amount</th>
                            <th rowspan="2">Actions</th>
                        </tr>
                        <tr>
                            <th>IGST</th>
                            <th>SGST/UTGST</th>
                            <th>CGST</th>
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
