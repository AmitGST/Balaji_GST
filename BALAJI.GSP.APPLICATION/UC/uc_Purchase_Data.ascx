<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_Purchase_Data.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.uc_Purchase_Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%--<%@ MasterType VirtualPath="~/User/User.master" %>--%>

<div class="row">
    <div class="col-md-2">
        <div class="form-group">
            <label>Invoice Number</label>
            <asp:DropDownList ID="ddlInvoiceNum" OnSelectedIndexChanged="ddlInvoiceNum_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <div>
    </div>
</div>

<asp:ListView ID="lv_purchasedata" DataKeyNames="PurchaseDataID" runat="server">
    <EmptyDataTemplate>
        <table class="table">
            <tr>
                <td>No data was returned.</td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <ItemTemplate>
        <tr>
            <td><%# Container.DataItemIndex + 1%></td>
            <td>
                <asp:TextBox ID="txtItem" Width="100%" onkeypress="return isDigitKey(event,this);" Text='<%# DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ItemCode")%>' MaxLength="8" runat="server"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender
                    runat="server"
                    ID="AutoCompleteExtender1"
                    TargetControlID="txtItem"
                    ServicePath="~/Service/AutoPopulate.asmx"
                    ServiceMethod="GetItems"
                    MinimumPrefixLength="2"
                    CompletionInterval="10"
                    EnableCaching="true"
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender>
            </td>
            <td>
                <asp:TextBox ID="txtQty" Text='<%# Eval("Qty")%>' Width="100%" onkeypress="return isNumberKey(event,this);" runat="server"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txtRate" Text='<%# Eval("Rate") %>' Width="100%" onkeypress="return isNumberKey(event,this);" runat="server"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txttotalAmount" Text='<%# Eval("TotalAmount") %>' Width="100%" onkeypress="return isNumberKey(event,this);" runat="server"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txtdiscount" Text='<%# Eval("Discount") %>' Width="100%" onkeypress="return isNumberKey(event,this);" runat="server"></asp:TextBox>
                <asp:RangeValidator ID="rvDiscount" ForeColor="Red" Font-Size="10px" Type="Double" ControlToValidate="txtdiscount" Display="Dynamic" MinimumValue="0.00"
                    MaximumValue="99.99" SetFocusOnError="true" ValidationGroup="savePurchaseData" runat="server" ErrorMessage="0-100"></asp:RangeValidator>

            </td>

            <td>
                <asp:TextBox ID="txttaxableAmt" Text='<%# Eval("TaxableAmount") %>' Width="100%" onkeypress="return isNumberKey(event,this);" runat="server"></asp:TextBox></td>
            <td style="text-align: center;">
                <asp:TextBox ID="txtAmountTax" Text='<%# Eval("TotalAmountWithTax") %>' onkeypress="return isNumberKey(event,this);" Width="100%" runat="server"></asp:TextBox></td>
            <td style="text-align: center;">
                <asp:TextBox ID="txtIGSTRate" Text='<%# Eval("IGSTRate") %>' onkeypress="return isNumberKey(event,this);" Width="100%" runat="server"></asp:TextBox></td>
            <td style="text-align: center;">
                <asp:TextBox ID="txtIGSTAmount" Text='<%# Eval("IGSTAmt") %>' onkeypress="return isNumberKey(event,this);" Width="100%" runat="server"></asp:TextBox></td>
            <td style="text-align: center;">
                <asp:TextBox ID="txtCGSTRate" Text='<%# Eval("CGSTRate") %>' onkeypress="return isNumberKey(event,this);" Width="100%" runat="server"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txtCGSTAmount" Text='<%# Eval("CGSTAmt") %>' onkeypress="return isNumberKey(event,this);" Width="100%" runat="server"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txtSGSTRate" Text='<%# Eval("SGSTRate") %>' onkeypress="return isNumberKey(event,this);" Width="100%" runat="server"></asp:TextBox></td>
            <td style="text-align: center;">
                <asp:TextBox ID="txtSGSTAmount" Text='<%# Eval("SGSTAmt") %>' onkeypress="return isNumberKey(event,this);" Width="100%" runat="server"></asp:TextBox></td>
            <td style="text-align: center;">
                <asp:TextBox ID="txtUTGSTRate" Text='<%# Eval("UGSTRate") %>' onkeypress="return isNumberKey(event,this);" Width="100%" runat="server"></asp:TextBox></td>
            <td style="text-align: center;">
                <asp:TextBox ID="txtUTGSTAmount" Text='<%# Eval("UGSTAmt") %>' onkeypress="return isNumberKey(event,this);" Width="100%" runat="server"></asp:TextBox></td>
            <td style="text-align: center;">
                <asp:TextBox ID="txtCessRate" Text='<%# Eval("CessRate") %>' onkeypress="return isNumberKey(event,this);" Width="100%" runat="server"></asp:TextBox></td>
            <td style="text-align: center;">
                <asp:TextBox ID="txtCessAmount" Text='<%# Eval("CessAmt") %>' onkeypress="return isNumberKey(event,this);" Width="100%" runat="server"></asp:TextBox></td>
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
        <table class="table table-bordered table-responsive">
            <tr>
                <th style="vertical-align: bottom; width: 2%; text-align: center;" rowspan="2">#</th>
                <th style="vertical-align: bottom; width: 8%; text-align: center;" rowspan="2">HSN/SAC</th>
                <th style="vertical-align: bottom; width: 4%; text-align: center;" rowspan="2">Quantity</th>
                <th style="vertical-align: bottom; width: 6%; text-align: center;" rowspan="2">Rate</th>
                <th style="vertical-align: bottom; width: 6%; text-align: center;" rowspan="2">Total Amount</th>
                <th style="vertical-align: bottom; width: 5%; text-align: center;" rowspan="2">Discount</th>
                <th style="vertical-align: bottom; width: 8%; text-align: center;" rowspan="2">Taxable Amount</th>
                <th style="width: 8%; vertical-align: bottom; text-align: center;" rowspan="2">Total Amount with Tax</th>
                <th style="width: 11%; text-align: center;" colspan="2">IGST</th>
                <th style="width: 11%; text-align: center;" colspan="2">CGST</th>
                <th style="width: 11%; text-align: center;" colspan="2">SGST</th>
                <th style="width: 11%; text-align: center;" colspan="2">UTGST</th>
                <th style="text-align: center;" colspan="2">Cess</th>
            </tr>
            <tr>
                <th style="text-align: center;">Rate</th>
                <th style="text-align: center;">Amount<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                <th style="text-align: center;">Rate </th>
                <th style="text-align: center;">Amount<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                <th style="text-align: center;">Rate </th>
                <th style="text-align: center;">Amount<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                <th style="text-align: center;">Rate</th>
                <th style="text-align: center;">Amount<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                <th style="width: 5%; text-align: center;">Rate</th>
                <th style="width: 6%; text-align: center;">Amount<br />(<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
            </tr>
        </table>
    </LayoutTemplate>
</asp:ListView>
<div class="box-footer">
    <asp:LinkButton ID="btnPurchaseData" OnClick="btnPurchaseData_Click" ValidationGroup="savePurchaseData" CssClass="btn btn-success" runat="server"><i class="fa fa-save"></i>&nbsp;Submit</asp:LinkButton>
    <uc1:uc_sucess runat="server" ID="uc_sucess" />
</div>

