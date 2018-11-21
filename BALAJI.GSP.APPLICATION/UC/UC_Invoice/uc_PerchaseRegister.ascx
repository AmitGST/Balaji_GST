<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_PerchaseRegister.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.UC_Invoice.uc_PerchaseRegister" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<div class="box-body">
    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Item Code</label>
                <asp:TextBox ID="txtItem" placeholder="Item Code" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Item Qty</label>
                <asp:TextBox ID="txtQty" placeholder="Item Qty" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Rate</label>
                <asp:TextBox ID="txtPerUnitRate" placeholder="Rate" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Stock Inward Date</label>
                <asp:TextBox ID="txtStockInwardDate" placeholder="Stock Inward Date" CssClass="form-control input-sm date-picker" runat="server"></asp:TextBox>
                <ajaxtoolkit:calendarextender id="CEsTOCKinWARD" targetcontrolid="txtStockInwardDate" runat="server" />
            </div>

        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label>Order Po</label>
                <asp:TextBox ID="txtOrderPo" placeholder="Order Po" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Order Po Date</label>
                <asp:TextBox ID="txtOrderPoDate" placeholder="Order Po Date" CssClass="form-control input-sm  date-picker" runat="server"></asp:TextBox>
                <ajaxtoolkit:calendarextender id="ceOrderPoDate" targetcontrolid="txtOrderPoDate" runat="server" />
            </div>
            <div class="form-group">
                <label>Supplier Invoice No.</label>
                <asp:TextBox ID="txtSupplierInvoiceNo" placeholder="Supplier Invoice No." CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Stock Order Date</label>
                <asp:TextBox ID="txtStockOrderDate" placeholder="MM/DD/YYY" CssClass="form-control input-sm date-picker" runat="server"></asp:TextBox>
                <ajaxtoolkit:calendarextender id="CalendarExtender1" targetcontrolid="txtStockOrderDate" runat="server" />
            </div>
        </div>
    </div>
    <asp:Button ID="btnAddQty" OnClick="btnAddQty_Click" CssClass="btn btn-primary" Visible="false" runat="server" Text="Update Purchase Register" />
</div>






