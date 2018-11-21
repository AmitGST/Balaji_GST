<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="B2Boffline.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Offline.B2Boffline" %>

<%@ Register Src="~/UC/Offline/uc_hsn.ascx" TagPrefix="uc1" TagName="uc_hsn" %>
<%@ Register Src="~/UC/Offline/uc_crdr_unregister.ascx" TagPrefix="uc1" TagName="uc_crdr_unregister" %>
<%@ Register Src="~/UC/Offline/uc_Adjust_Advance.ascx" TagPrefix="uc1" TagName="uc_Adjust_Advance" %>
<%@ Register Src="~/UC/Offline/uc_Tax_Liability.ascx" TagPrefix="uc1" TagName="uc_Tax_Liability" %>
<%@ Register Src="~/UC/Offline/uc_B2B_Invoices.ascx" TagPrefix="uc1" TagName="uc_B2B_Invoices" %>
<%@ Register Src="~/UC/Offline/uc_ExportsInvoices.ascx" TagPrefix="uc1" TagName="uc_ExportsInvoices" %>
<%@ Register Src="~/UC/Offline/uc_B2CL.ascx" TagPrefix="uc1" TagName="uc_B2CL" %>
<%@ Register Src="~/UC/Offline/uc_B2CS.ascx" TagPrefix="uc1" TagName="uc_B2CS" %>
<%@ Register Src="~/UC/Offline/uc_crdr_registered.ascx" TagPrefix="uc1" TagName="uc_crdr_registered" %>
<%@ Register Src="~/UC/Offline/uc_AdvacesATADJ_GSTR2.ascx" TagPrefix="uc1" TagName="uc_AdvacesATADJ_GSTR2" %>
<%@ Register Src="~/UC/Offline/uc_Advances_GSTR2.ascx" TagPrefix="uc1" TagName="uc_Advances_GSTR2" %>
<%@ Register Src="~/UC/Offline/uc_B2B_invoice_GSTR2.ascx" TagPrefix="uc1" TagName="uc_B2B_invoice_GSTR2" %>
<%@ Register Src="~/UC/Offline/uc_B2BUR_Invoice_GSTR2.ascx" TagPrefix="uc1" TagName="uc_B2BUR_Invoice_GSTR2" %>
<%@ Register Src="~/UC/Offline/uc_crdr_GSTR2.ascx" TagPrefix="uc1" TagName="uc_crdr_GSTR2" %>
<%@ Register Src="~/UC/Offline/uc_crdr_unregister_GSTR2.ascx" TagPrefix="uc1" TagName="uc_crdr_unregister_GSTR2" %>
<%@ Register Src="~/UC/Offline/uc_ImportGoods_GSTR2.ascx" TagPrefix="uc1" TagName="uc_ImportGoods_GSTR2" %>
<%@ Register Src="~/UC/Offline/uc_ImportServices_GSTR2.ascx" TagPrefix="uc1" TagName="uc_ImportServices_GSTR2" %>
<%@ Register Src="~/UC/Offline/uc_NilRated_GSTR2.ascx" TagPrefix="uc1" TagName="uc_NilRated_GSTR2" %>
<%@ Register Src="~/UC/Offline/uc_TaxLiability_GSTR2.ascx" TagPrefix="uc1" TagName="uc_TaxLiability_GSTR2" %>
<%@ Register Src="~/UC/Offline/uc_hsn_GSTR2.ascx" TagPrefix="uc1" TagName="uc_hsn_GSTR2" %>
<%@ Register Src="~/UC/AddOffline/uc_B2CL_GSTR1.ascx" TagPrefix="uc1" TagName="uc_B2CL_GSTR1" %>
<%@ Register Src="~/UC/AddOffline/uc_CreditCdnr_Gstr1.ascx" TagPrefix="uc1" TagName="uc_CreditCdnr_Gstr1" %>
<%@ Register Src="~/UC/AddOffline/uc_Adv_Adjustment_GSTR1.ascx" TagPrefix="uc1" TagName="uc_Adv_Adjustment_GSTR1" %>
<%@ Register Src="~/UC/Offline/uc_ViewSummary.ascx" TagPrefix="uc1" TagName="uc_ViewSummary" %>
<%@ Register Src="~/UC/AddOffline/uc_EXPORT_GSTR1.ascx" TagPrefix="uc1" TagName="uc_EXPORT_GSTR1" %>






<%--For GSTR 2--%>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="MainContent" runat="server">
        <div class="content-header">
            <h1>Offline Section 
            <small></small></h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Offline</a></li>
                <li class="active">Offline Section</li>
            </ol>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-sm-6">
                   <%-- <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server">Import</asp:LinkButton>--%>
                   <%-- <asp:LinkButton ID="LinkButton2" CssClass="btn btn-primary" runat="server">Delete</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" CssClass="btn btn-primary" runat="server">Clear Section Data</asp:LinkButton>--%>
                  <%--  <asp:LinkButton ID="lkbViewSummary" OnClick="lkbViewSummary_Click" CssClass="btn btn-primary" runat="server">View JSON</asp:LinkButton>--%>
                </div>
                <div class="form-group col-sm-offset-4 col-sm-2">
		<div style="margin-left:0px !important; " class="radio-inline">
            <%--<label>Select Return:</label>--%>
			 <asp:RadioButtonList ID="rdbGSTR" CssClass="radio-inline" AutoPostBack="true" RepeatLayout="Flow" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbGSTR_SelectedIndexChanged" runat="server">
                        <asp:ListItem Value="0" Selected="True">GSTR-1</asp:ListItem>
                        <asp:ListItem style="margin-left:26px !important;" Value="2">GSTR-2</asp:ListItem>
                    </asp:RadioButtonList>
		</div>
	        </div>
            </div>
            <br />
            <div class="box box-primary">
                <div class="box-header with-border">
                    <div class="form-inline">
                        <div class="form-group">
                            <label>
                            Select Section:</label>&nbsp;&nbsp;<asp:DropDownList ID="ddlSection" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-sm-3">
                        </div>
                    </div>
                </div>
                
                <div id="ucb2binvGSTR1" runat="server" visible="false">
                    <uc1:uc_B2B_Invoices runat="server" ID="uc_B2B_Invoices" />
                </div>
                <div id="uccrdrGSTR1" runat="server" visible="false">
                    <uc1:uc_crdr_unregister runat="server" ID="uc_crdr_unregister" />
                </div>
                <div id="ucexportGSTR1" runat="server" visible="false">
                    <uc1:uc_ExportsInvoices runat="server" ID="uc_ExportsInvoices" />
                </div>
                <div id="uctaxliaGSTR1" runat="server" visible="false">
                    <uc1:uc_Tax_Liability runat="server" ID="uc_Tax_Liability" />
                </div>
                <div id="ucadjustadvGSTR1" runat="server" visible="false">
                    <uc1:uc_Adjust_Advance runat="server" ID="uc_Adjust_Advance" />
                </div>
                <div id="uchsnGSTR1" runat="server" visible="false">
                    <uc1:uc_hsn runat="server" ID="uc_hsn" />
                </div>
                <div id="ucB2CLGSTR1" runat="server" visible="false">
                    <uc1:uc_B2CL runat="server" ID="uc_B2CL" />
                </div>
                <div id="ucB2CSGSTR1" runat="server" visible="false">
                    <uc1:uc_B2CS runat="server" ID="uc_B2CS" />
                </div>
                <div id="uccrdrregGSTR1" runat="server" visible="false">
                    <uc1:uc_crdr_registered runat="server" ID="uc_crdr_registered" />
                </div>
                <%--gstr2--%>
                <div id="ucAdvADJGSTR2" runat="server" visible="false">
                    <uc1:uc_AdvacesATADJ_GSTR2 runat="server" ID="uc_AdvacesATADJ_GSTR2" />
                </div>
                <div id="ucAdvGSTR2" runat="server" visible="false">
                    <uc1:uc_Advances_GSTR2 runat="server" ID="uc_Advances_GSTR2" />
                </div>
                <div id="ucB2BGSTR2" runat="server" visible="false">
                    <uc1:uc_B2B_invoice_GSTR2 runat="server" ID="uc_B2B_invoice_GSTR2" />
                </div>
                <div id="ucB2BURGSTR2" runat="server" visible="false">
                    <uc1:uc_B2BUR_Invoice_GSTR2 runat="server" ID="uc_B2BUR_Invoice_GSTR2" />
                </div>
                <div id="ucCRDRGSTR2" runat="server" visible="false">
                    <uc1:uc_crdr_GSTR2 runat="server" ID="uc_crdr_GSTR2" />
                </div>
                <div id="ucCRDRunRegGSTR2" runat="server" visible="false">
                    <uc1:uc_crdr_unregister_GSTR2 runat="server" ID="uc_crdr_unregister_GSTR2" />
                </div>
                <div id="ucImportGoodsGSTR2" runat="server" visible="false">
                    <uc1:uc_ImportGoods_GSTR2 runat="server" ID="uc_ImportGoods_GSTR2" />
                </div>
                <div id="ucImportServicesGSTR2" runat="server" visible="false">
                    <uc1:uc_ImportServices_GSTR2 runat="server" ID="uc_ImportServices_GSTR2" />
                </div>
                <div id="ucHsnGstr2" runat="server" visible="false">
                    <uc1:uc_hsn_GSTR2 runat="server" ID="uc_hsn_GSTR2" />
                </div>
                <%--<div id="ucInwardGSTR2" runat="server" visible="false">
                <uc1:uc_InwardSupplies_GSTR2 runat="server" ID="uc_InwardSupplies_GSTR2" />
            </div>--%>
                <div id="ucNillRatedGSTR2" runat="server" visible="false">
                    <%--<uc1:uc_NilRated_GSTR2 runat="server" ID="uc_NilRated_GSTR2" />--%>
                </div>
                <div id="ucTaxLiaGSTR2" runat="server" visible="false">
                    <%--<uc1:uc_TaxLiability_GSTR2 runat="server" ID="uc_TaxLiability_GSTR2" />--%>
                </div>

                <%-- <div id="Div3" runat="server" visible="false">
                
            </div>--%>
                <div class="box-footer">
                    <asp:LinkButton ID="lkbMainBack" CssClass="btn btn-danger" OnClick="lkbMainBack_Click" runat="server"><i class="fa fa-backward"></i>&nbsp; Back</asp:LinkButton>
                </div>
            </div>
            
        </div>
    </div>
    <div id="B2CLGSTR1" runat="server" visible="false">
        <uc1:uc_B2CL_GSTR1 runat="server" ID="uc_B2CL_GSTR1" />
    </div>
    <div id="ucExportGstr1Add" runat="server" visible="false">
        <uc1:uc_EXPORT_GSTR1 runat="server" ID="uc_EXPORT_GSTR1" />
    </div>
    <div id="Credit_Gstr1" runat="server" visible="false">
        <uc1:uc_CreditCdnr_Gstr1 runat="server" id="uc_CreditCdnr_Gstr1" />
    </div>
    <div id="AdvanceGSTR1" runat="server" visible="false">
        <uc1:uc_Adv_Adjustment_GSTR1 runat="server" id="uc_Adv_Adjustment_GSTR1" />
    </div>
    <div id="SecondaryContent" runat="server" visible="false">
        <uc1:uc_ViewSummary runat="server" ID="uc_ViewSummary" />
    </div>
</asp:Content>
