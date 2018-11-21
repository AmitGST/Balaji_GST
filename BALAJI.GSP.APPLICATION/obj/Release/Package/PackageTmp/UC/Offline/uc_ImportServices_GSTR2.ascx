<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_ImportServices_GSTR2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_ImportServices_GSTR2" %>
<div class="box box-primary">
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lv_ImportServices" runat="server" OnItemDataBound="lv_ImportServices_ItemDataBound">
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
                        <asp:TextBox ID="txtInvoiceNo" width="140px" Class="form-control" Text='<%# Eval("InvoiceNo") %>' runat="server" disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_InvoiceDate" Width="100px" Text='<%#DateTimeAgo.GetFormatDate(Eval("InvoiceDate")) %>' placeholder="MM/DD/YYY" CssClass="form-control" runat="server" disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceValue" Width="100px" Class="form-control" runat="server" disabled></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddlPos" Class="form-control" Width="140px" runat="server" disabled></asp:DropDownList>
                    </td>
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <td>
                        <asp:LinkButton ID="btnAddMore" runat="server" CssClass="btn" data-trigger="hover" data-placement="right" title="Add Items"><span class="glyphicon glyphicon-plus-sign" style="color:green"></span></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive ">
                    <thead>
                        <tr>
                            <th>
                                <asp:CheckBox ID="Chk1" Class="form-control" runat="server" /></th>
                            <th>Invoice No.</th>
                            <th>Invoice Date</th>
                            <th>Total Invoice Value &nbsp;<i class="fa fa-rupee"></i></th>
                            <th>Place of Supply</th>
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
