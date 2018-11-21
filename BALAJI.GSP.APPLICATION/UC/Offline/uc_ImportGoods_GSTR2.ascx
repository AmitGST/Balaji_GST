<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_ImportGoods_GSTR2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_ImportGoods_GSTR2" %>
<div class="box box-primary">
    <div class="box-body">
        <asp:ListView ID="lv_ImportGoods_GSTR2" runat="server">
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
                        <asp:TextBox ID="txtportCode" Class="form-control" Text='<%# Eval("PortCode") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtBillNo"  Class="form-control" Text='<%# Eval("BillOfEntry") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtBillDate" Text='<%#DateTimeAgo.GetFormatDate(Eval("BillofEntryDate")) %>' placeholder="MM/DD/YYY" CssClass="form-control" runat="server" Disabled></asp:TextBox></td>
                   <td>
                       <asp:TextBox ID="txtBillValue" Class="form-control" Text='<%# Eval("BillofEntryValue") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                       <asp:CheckBox ID="chxImport"  runat="server"></asp:CheckBox></td>
                    <td> <asp:TextBox ID="txtSupplierGSTIN" Class="form-control" Text='<%# Eval("SupplierGSTIN") %>' runat="server" Disabled></asp:TextBox></td>
                     <td><asp:LinkButton ID="btnAddMore" Class="form-control" runat="server" CssClass="btn" data-trigger="hover" data-placement="right" title="Add Items"><span class="glyphicon glyphicon-plus-sign" style="color:green"></span></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            
            <LayoutTemplate>
                <table class="table table-responsive ">
                    <thead>
                        <tr>
                            <th>
                                <asp:CheckBox ID="Chk1" runat="server" /></th>
                            <th>Port Code</th>
                            <th>Bill of Entry No.</th>
                            <th>Bill of Entry Date </th>
                            <th>Bill of Entry Value</th>
                            <th>Import From SEZ</th>
                            <th>Supplier GSTIN</th>
                            <th>Actions</th>
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

