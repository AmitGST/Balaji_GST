<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_B2BUR_Invoice_GSTR2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_B2BUR_Invoice_GSTR2" %>
<div class="box box-primary">
    <div class="box-body">
        <asp:ListView ID="lv_B2b_Invoice" runat="server" OnItemDataBound="lv_B2b_Invoice_ItemDataBound">
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
                        <asp:CheckBox ID="chk" Class="form-control" runat="server" /></td>
                    <td>
                        <asp:TextBox ID="txtGSTIN" Class="form-control" Width="140px" Text='<%# Eval("SupplierGSTIN") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceNo" Class="form-control" Width="140px" Text='<%# Eval("InvoiceNo") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_InvoiceDate" Text='<%#DateTimeAgo.GetFormatDate(Eval("InvoiceDate")) %>' Width="90px" placeholder="MM/DD/YYY" CssClass="form-control" runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceValue" Class="form-control" Width="100px" Text='<%# Eval("TotalInvoiceValue") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddlPos" Class="form-control" Width="130px" runat="server" Disabled></asp:DropDownList></td>
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <td>
                        <asp:DropDownList ID="ddl_SupplyType" Class="form-control" Width="100px" runat="server" disabled></asp:DropDownList></td>
                    <td>
                        <asp:LinkButton ID="btnAddMore" runat="server" CssClass="btn" data-trigger="hover" data-placement="right" title="Add More Line Items"><span class="glyphicon glyphicon-plus-sign" style="color:green"></span></asp:LinkButton></td>
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive ">
                    <thead>
                        <tr>
                            <th>
                                <asp:CheckBox ID="Chk1" Class="form-control" runat="server" /></th>
                            <th>Supplier Name</th>
                            <th>Invoice No.</th>
                            <th>Invoice Date</th>
                            <th>Total Invoice Value &nbsp;<i class="fa fa-rupee"></i></th>
                            <th>Place of Supply</th>
                            <th>Supply Type</th>
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
