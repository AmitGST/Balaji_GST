<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_crdr_GSTR2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_crdr_GSTR2" %>
<div class="box box-primary">
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lv_crdr_GSTR2" runat="server" OnItemDataBound="lv_crdr_GSTR2_ItemDataBound">
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
                        <asp:CheckBox ID="chk"  runat="server" /></td>
                    <td>
                        <asp:TextBox ID="txtGSTIN" Width="140px" Class="form-control" Text='<%# Eval("SupplierGSTIN") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddl_DocumentType" Width="95px" Class="form-control" runat="server" Disabled></asp:DropDownList>
                    </td>
                    <asp:HiddenField ID="hdnDoc" runat="server" Value='<%# Eval("Document_Type") %>' />
                    <td>
                        <asp:TextBox ID="txtVoucherValue" Class="form-control" width="80px" Text='<%# Eval("Voucher_Value") %>' runat="server" Disabled></asp:TextBox></td>
                   
                      <td>
                          <asp:TextBox ID="txtVoucherNo" Class="form-control" width="80px" Text='<%# Eval("Voucher_No") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtVoucherDate" Width="90px" Text='<%#DateTimeAgo.GetFormatDate(Eval("Voucher_date")) %>' placeholder="MM/DD/YYY" CssClass="form-control" runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceNo" width="140px" Class="form-control" Text='<%# Eval("InvoiceNo") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_InvoiceDate" Width="90px" Class="form-control" Text='<%#DateTimeAgo.GetFormatDate(Eval("InvoiceDate")) %>' placeholder="MM/DD/YYY" CssClass="form-control" runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddlSupplyType" width="100px" Class="form-control" runat="server" Disabled></asp:DropDownList></td>
                    <td>
                        <asp:DropDownList ID="ddlIssuingNote" width="95px" Class="form-control" runat="server" ></asp:DropDownList>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkPreGST" Class="form-control" runat="server" />
                    </td>
                    <td>
                        <asp:LinkButton ID="btnAddMore" runat="server" CssClass="btn" data-trigger="hover" data-placement="right" title="Add More Line Items"><span class="glyphicon glyphicon-plus-sign" style="color:green"></span></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive ">
                    <thead>
                        <tr>
                            <th>
                                <asp:CheckBox ID="Chk1" runat="server" /></th>
                            <th>Supplier's GSTIN</th>
                            <th>Note Type</th>
                            <th>Note/Refund Voucher Value(₹)</th>
                            <th>Note/Refund Voucher Number</th>
                            <th>Note/Refund Voucher date</th>
                            <th>Invoice No</th>
                            <th>Invoice Date</th>
                            <th>Supply Type</th>
                            <th>Reason For Issuing Note</th>
                            <th>Pre GST</th>
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
