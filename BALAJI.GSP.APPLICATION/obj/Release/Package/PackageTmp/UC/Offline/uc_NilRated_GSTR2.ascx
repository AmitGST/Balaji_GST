<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_NilRated_GSTR2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_NilRated_GSTR2" %>
<div class="box box-primary">
    <div class="box-body">
        <asp:ListView ID="lv_NilRated" runat="server">
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
                        <asp:Label ID="lblDescription" runat="server" Text="Inter-State"></asp:Label>
                      <%--  <asp:TextBox ID="txtdescription" runat="server"></asp:TextBox>--%>

                    </td>
                    <td>
                        <asp:TextBox ID="txtCompounding" Class="form-control" Text='<%# Eval("CompoundingDealerSupplies") %>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtNilSupplies" Class="form-control" Text='<%# Eval("NilRated") %>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtExempted" Class="form-control" Text='<%# Eval("IsExempted") %>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtGSTSupplies" Class="form-control" Text='<%# Eval("NonGSTSupplies") %>' runat="server"></asp:TextBox></td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th>
                                <asp:CheckBox ID="Chk1" Class="form-control" runat="server" /></th>
                            <th>Description</th>
                            <th>Compounding Dealer supplies</th>
                            <th>Nil-rated Supplies</th>
                            <th>Exempted(Other than Nil rated/non-GST supply)<i style="font-size: 10px;" class="fa fa-rupee"></i></th>
                            <th>Non-GST Supplies<i style="font-size: 10px;" class="fa fa-rupee"></i></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server" />
                    </tbody>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </div>
    <div class="box-footer">
        <asp:LinkButton ID="lkbSave" runat="server">Save</asp:LinkButton>
    </div>
</div>
