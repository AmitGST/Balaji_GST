<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_InvoiceEdit.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.UC_Invoice.uc_InvoiceEdit" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>




<asp:GridView ID="gvInvoice_Items" DataKeyNames="InvoiceDataID" CssClass="table table-responsive no-padding table-striped table-bordered table-condensed" AutoGenerateColumns="false" runat="server">
    <Columns>
        <asp:TemplateField HeaderText="#" Visible="false">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="HSN/SAC" ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:TextBox ID="txtItemCode" AutoPostBack="true" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ItemCode") %>' OnTextChanged="txtItemCode_TextChanged" autocomplete="off" MaxLength="8" onpaste="return false;" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description" ItemStyle-Width="30%">
            <ItemTemplate>
                <asp:TextBox ID="txtGoodService" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Description") %>' CssClass="form-control input-sm" ReadOnly="true" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Qty." ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:TextBox ID="txtQty" MaxLength="6"  AutoPostBack="true" Text='<%#DataBinder.Eval(Container.DataItem,"Qty") %>' OnTextChanged="txtQty_TextChanged" onpaste="return false;" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Unit" ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:TextBox ID="txtUnit" CssClass="form-control input-sm" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Unit") %>' ReadOnly="true" AutoPostBack="true" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Rate(per item)" ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:TextBox ID="txtRate" AutoPostBack="true" CssClass="form-control input-sm" OnTextChanged="txtQty_TextChanged" Text='<%#DataBinder.Eval(Container.DataItem,"Rate") %>' onkeypress="return onlyDecNos(event,this);" onpaste="return false;" MaxLength="10" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total(<i class='fa fa-inr' aria-hidden='true'></i>)" ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:Label ID="txtTotal" Text='<%#DataBinder.Eval(Container.DataItem,"TotalAmount") %>' ReadOnly="true" CssClass="form-control input-sm" runat="server"></asp:Label>
                <%--  <asp:TextBox ID="txtTotal" ReadOnly="true" runat="server"></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Discount" ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:TextBox ID="txtDiscount" AutoPostBack="true" OnTextChanged="txtQty_TextChanged" Text='<%#DataBinder.Eval(Container.DataItem,"Discount") %>' MaxLength="5" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Taxable Val.(<i class='fa fa-inr' aria-hidden='true'></i>)">
            <ItemTemplate>
                <asp:Label ID="txtTaxableValue" Text='<%#DataBinder.Eval(Container.DataItem,"TaxableAmount") %>' CssClass="form-control input-sm" ReadOnly="true" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<div class="box-footer">
    <asp:LinkButton ID="lkbUpdateInvoice" OnClick="lkbUpdateInvoice_Click" Visible="false" CssClass="btn btn-primary pull-left" runat="server"><i class="fa fa-save"></i><span style="margin:3px;"> Update Invoice</span></asp:LinkButton>
    <uc1:uc_sucess runat="server" ID="uc_sucess" />
</div>
