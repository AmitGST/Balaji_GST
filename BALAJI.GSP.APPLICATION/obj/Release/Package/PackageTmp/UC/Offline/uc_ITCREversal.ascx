<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_ITCREversal.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_ITCREversal" %>
<div class="box box-primary">
    <div class="box-body">
        <asp:ListView ID="lv_ITC_Reversal" runat="server">
            <EmptyDataTemplate>
                <table class="table table-responsive">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <ItemTemplate>
                <tr>
                    <td><asp:CheckBox ID="chk" runat="server" /></td>
                    <td><asp:TextBox ID="txtDescription" Text='<%# Eval("GST_TRN_OFFLINE_INVOICE_SECTION_RULE.RuleId") %>' runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtIGST" Text='<%# Eval("IGSTAmt") %>' runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtCGST" Text='<%# Eval("CGSTAmt") %>' runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtSGST" Text='<%# Eval("SGSTAmt") %>' runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtCess" Text='<%# Eval("CessAmt") %>' runat="server"></asp:TextBox></td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th>
                            <asp:CheckBox ID="Chk1" runat="server" /></th>
                            <th>Description</th>
                            <th>IGST</th>
                            <th>CGST</th>
                            <th>SGST/ UTGST</th>
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