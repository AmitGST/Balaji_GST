<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_Advances_GSTR2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_Advances_GSTR2" %>
<div class="box box-primary">
    <div class="box-body">
        <asp:ListView ID="lv_Advances" runat="server" Style="margin-right: 184px" OnItemDataBound="lv_Advances_ItemDataBound">
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
                        <asp:DropDownList ID="ddl_Pos" runat="server"></asp:DropDownList></td>
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <td>
                        <asp:DropDownList ID="ddl_SupplyType" runat="server"></asp:DropDownList></td>
                   <%-- <td>
                        <asp:LinkButton ID="btnAddMore" runat="server" CssClass="btn" OnClick="btnAddMore_Click" data-trigger="hover" data-placement="right" title="Add More Line Items"><span class="glyphicon glyphicon-plus-sign" style="color:green"></span></asp:LinkButton></td>--%>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th>
                                <asp:CheckBox ID="Chk1" runat="server" /></th>
                            <th>Place Of Supply</th>
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
