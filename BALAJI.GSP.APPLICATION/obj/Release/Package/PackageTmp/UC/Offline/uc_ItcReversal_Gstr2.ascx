<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_ItcReversal_Gstr2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_ItcReversal_Gstr2" %>
<div class="box box-primary">
    <div class="box-body">
        <asp:ListView ID="lv_ITC_Reversal_GSTR2" runat="server">
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
                    <td><asp:TextBox ID="txtDescription" Class="form-control" Text='<%# Eval("HSNDescription") %>' runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtIGST" Class="form-control" Text='<%# DataBinder.Eval(Container.DataItem,"GST_TRN_OFFLINE_INVOICE_DATAITEM.IGSTAmt")%>'  runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtCGST"  Class="form-control" Text='<%# DataBinder.Eval(Container.DataItem,"GST_TRN_OFFLINE_INVOICE_DATAITEM.CGSTAmt")%>' runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtSGST"  Class="form-control"  Text='<%# DataBinder.Eval(Container.DataItem,"GST_TRN_OFFLINE_INVOICE_DATAITEM.SGSTAmt") %>' runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtCess"  Class="form-control" Text='<%# DataBinder.Eval(Container.DataItem,"GST_TRN_OFFLINE_INVOICE_DATAITEM.CessAmt") %>' runat="server"></asp:TextBox></td>
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
