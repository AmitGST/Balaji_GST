<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_seller.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.UC_Invoice.uc_seller" %>

<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">Details of Seller</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
        </div>
    </div>
    <div class="box-body">

        <div class="row">
            <div class="col-lg-3 col-md-3 col-xs-3">
                <label>
                    <asp:Label ID="lblSellerGSTIN" runat="server" Text="GSTIN"></asp:Label>
                </label>
                <br />
                <asp:Label ID="txtSelletGSTIN" class="invoice-col" runat="server" onkeypress="return isAlphaNumeric(event)" ReadOnly="true" AutoPostBack="true"></asp:Label>
            </div>
            <div class="col-lg-3 col-md-3 col-xs-3">
                <label>
                    <asp:Label ID="lblInvoiceDate" runat="server" Text="Date Of Invoice"></asp:Label>
                </label>
                <br />
                <asp:Label ID="txtInvoiceDate" runat="server" ReadOnly="true"></asp:Label>
            </div>
            <div class="col-lg-3 col-md-3 col-xs-3"></div>
            <div class="col-lg-3 col-md-3 col-xs-3"></div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-3 col-xs-3">
                <label>
                    <asp:Label ID="lblInvoiceNumber" runat="server" Text="Invoice Number"></asp:Label>
                </label>
                <br />
                <asp:Label ID="txtInvoiceNumber" runat="server" ReadOnly="true"></asp:Label>
            </div>
            <div class="col-lg-3 col-md-3 col-xs-3"></div>
            <div class="col-lg-3 col-md-3 col-xs-3"></div>
            <div class="col-lg-3 col-md-3 col-xs-3"></div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-3 col-xs-3">
                <label>
                    <asp:Label ID="lblSellerName" runat="server" Text="Name"></asp:Label>
                </label>
                <br />
                <asp:Label ID="txtSellerName" runat="server" ReadOnly="true"></asp:Label>
            </div>
            <div class="col-lg-3 col-md-3 col-xs-3"></div>
            <div class="col-lg-3 col-md-3 col-xs-3"></div>
            <div class="col-lg-3 col-md-3 col-xs-3"></div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-3 col-xs-3">
                <label>
                    <asp:Label ID="lblSellerAddress" runat="server" Text="Address"></asp:Label></label><br />
                <asp:Label ID="txtSellerAddress" runat="server" ReadOnly="true"></asp:Label>
            </div>
            <div class="col-lg-3 col-md-3 col-xs-3"></div>
            <div class="col-lg-3 col-md-3 col-xs-3"></div>
            <div class="col-lg-3 col-md-3 col-xs-3"></div>
            <div class="col-lg-3 col-md-3 col-xs-3"></div>
        </div>
    </div>
</div>
