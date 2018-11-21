<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_AdvacesATADJ_GSTR2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_AdvacesATADJ_GSTR2" %>
<div class="box box-primary">
    <div class="box-body">
        <asp:ListView ID="lv_Adjust_AdvancesGSTR2" runat="server" OnItemDataBound="lv_Adjust_Advances_ItemDataBound">
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
                        <asp:DropDownList ID="ddlPos" runat="server"></asp:DropDownList></td>
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <td>
                        <asp:DropDownList ID="ddl_SupplyType" runat="server"></asp:DropDownList></td>
                    <td></td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive ">
                    <thead>
                        <tr>
                            <th>
                                <asp:CheckBox ID="Chk1" runat="server" /></th>
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
