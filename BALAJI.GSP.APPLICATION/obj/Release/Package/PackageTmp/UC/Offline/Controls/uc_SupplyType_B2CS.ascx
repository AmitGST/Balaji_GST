<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_SupplyType_B2CS.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.Controls.uc_SupplyType_B2CS" %>
<td>
    <div class="form-group">
        <asp:DropDownList ID="ddlPos" Width="95px" class="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlPos_SelectedIndexChanged" runat="server"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvddlpos" runat="server" Display="Dynamic" InitialValue="-1" ControlToValidate="ddlPos" CssClass="help-block" ValidationGroup="vgB2CSInsert" ErrorMessage="Please specify POS"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvddlposEdit" runat="server" Display="Dynamic" InitialValue="-1" ControlToValidate="ddlPos" CssClass="help-block" ValidationGroup="vgB2CSEdit" ErrorMessage="Please specify POS"></asp:RequiredFieldValidator>
    </div>
</td>
<td>
    <asp:TextBox ID="txt_Taxable_value" AutoPostBack="true" OnTextChanged="txt_Taxable_value_TextChanged" autocomplete="off" Width="90px" class="form-control input-sm" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Text='<%# Eval("TotalTaxableValue") %>' runat="server"></asp:TextBox></td>
<td>
    <asp:DropDownList ID="ddl_SupplyType" Width="100px" class="form-control input-sm" Disabled AutoPostBack="true" runat="server"></asp:DropDownList></td>
<td>
    <asp:DropDownList ID="ddlRate" OnSelectedIndexChanged="ddlRate_SelectedIndexChanged" class="form-control input-sm" Width="100px" AutoPostBack="true" runat="server"></asp:DropDownList></td>
<td>
    <asp:TextBox AutoPostBack="true" ID="txtIntegratedTax" Width="70px" class="form-control input-sm" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Text='<%# Eval("IGSTAmt") %>' runat="server"></asp:TextBox></td>
<td>
    <asp:TextBox AutoPostBack="true" ID="txtCentralTax" Width="70px" class="form-control input-sm" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Text='<%# Eval("CGSTAmt") %>' runat="server"></asp:TextBox></td>
<td>
    <asp:TextBox AutoPostBack="true" ID="txt_SGSTUTGST" Width="70px" class="form-control input-sm" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Text='<%# Eval("SGSTAmt") %>' runat="server"></asp:TextBox></td>
<td>
    <div class="form-group">
        <asp:TextBox AutoPostBack="true" ID="txtCess" Width="70px" class="form-control input-sm" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Text='<%# Eval("CessAmt") %>' runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCess" runat="server" Display="Dynamic" ControlToValidate="txtCess" CssClass="help-block" ValidationGroup="vgB2CSInsert" ErrorMessage="Please specify Cess"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvCessEdit" runat="server" Display="Dynamic" ControlToValidate="txtCess" CssClass="help-block" ValidationGroup="vgB2CSEdit" ErrorMessage="Please specify Cess"></asp:RequiredFieldValidator>

    </div>
</td>

