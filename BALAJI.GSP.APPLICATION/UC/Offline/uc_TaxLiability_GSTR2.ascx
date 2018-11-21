<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_TaxLiability_GSTR2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_TaxLiability_GSTR2" %>
<div class="box box-primary">
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lv_Taxliability" OnItemDataBound="lv_Taxliability_ItemDataBound" runat="server">
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
                        <asp:DropDownList ID="ddlPos" Class="form-control" Width="140px" runat="server"></asp:DropDownList></td>
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <td>
                        <asp:DropDownList ID="ddl_SupplyType" Class="form-control" Width="100px" runat="server"></asp:DropDownList></td>
                    <td>
                        <asp:LinkButton ID="btnAddMore" runat="server" CssClass="btn" data-trigger="hover" data-placement="right" title="Add More Line Items"><span class="glyphicon glyphicon-plus-sign" style="color:green"></span></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th>
                                <asp:CheckBox ID="Chk1" Class="form-control" runat="server" /></th>
                            <th>Place of Supply</th>
                            <th>Supply Type</i></th>
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
