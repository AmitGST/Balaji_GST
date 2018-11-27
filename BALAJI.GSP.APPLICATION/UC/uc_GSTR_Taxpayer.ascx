<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_GSTR_Taxpayer.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.uc_GSTR_Taxpayer" %>
<%@ Register Src="~/UC/uc_GSTNUsers.ascx" TagPrefix="uc1" TagName="uc_GSTNUsers" %>
<%@ Register Src="~/UC/UC_Invoice/uc_invoiceMonth.ascx" TagPrefix="uc1" TagName="uc_invoiceMonth" %>

<uc1:uc_GSTNUsers runat="server" ID="uc_GSTNUsers" Visible="false" />

<div class="row">
    <div class="col-md-4">
        <div class="form-group">
            <label>
                GSTIN:
            </label>
            <asp:Label ID="lblGSTIN" runat="server"> </asp:Label>

        </div>
    </div>
    <div class="col-md-5">
    </div>

    <div class="col-md-3">
        <label class="control-label">Financial Year: </label>
        &nbsp;&nbsp; 
        <asp:Label ID="lblFinYear" Text="FinancialYear" runat="server"></asp:Label>
    </div>

</div>
<div class="row">
    <div class="col-md-4">
        <div class="form-group">
            <label>
                Taxpayer Name:
            </label>
            <asp:Label ID="lblTaxpayerName" runat="server"></asp:Label>
        </div>
    </div>
    <div class="col-md-5">
    </div>

    <div class="col-md-1">
        <label class="control-label">Month: </label>
    </div>
    <div class="col-md-2">
        <uc1:uc_invoiceMonth Visible="false" runat="server" ID="uc_invoiceMonth" />
    </div>
</div>


<%--<div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>
                        Turnover in the preceding Financial Year:
                    </label>
                    <asp:Label ID="lblTurnoverYear" runat="server" Style="display: none;"></asp:Label>
                </div>
            </div>
        </div>--%>
<div class="row">
    <div class="col-md-4">
        <div class="form-group">
            <label>
                Aggregate Turnover - :
            </label>
            <asp:Label ID="lblTurnoverAMT" runat="server"></asp:Label>
        </div>
    </div>
</div>
<%--<div class="box-body">
              <dl class="dl-horizontal">
                <dt>  GSTIN:</dt>
                <dd> <asp:Label ID="lblGSTIN" runat="server"> </asp:Label></dd>
                <dt>Taxpayer Name:</dt>
                <dd><asp:Label ID="lblTaxpayerName" runat="server"></asp:Label></dd>
                   <dt>Turnover in the preceding Financial Year: </dt>
                <dd> <asp:Label ID="lblTurnoverYear" runat="server" Style="display: none;"></asp:Label></dd>
                <dt>  Aggregate Turnover - April to June, 2017 :</dt>
                <dd> <asp:Label ID="lblTurnoverAMT" runat="server" Style="display: none;"></asp:Label>
                </dd>
              </dl>
     
            <div class="pull-right">
                <label>Financial Year: </label>
                <asp:Label ID="lblFinYear" Text="FinancialYear" runat="server"> </asp:Label>
            </div>
            <div class="pull-right">
                <label>Month: </label>
                <asp:Label ID="lblMonth" Text="Month" runat="server"></asp:Label>
            
            </div>

        </div>--%>