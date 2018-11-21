<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_Adv_Adjustment_GSTR1.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Add_Offline.uc_Adv_Adjustment_GSTR1" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>

<div class="content">
    <div class="content-header">
        <h1>Offline 
            <small></small></h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">Offline</li>
            <li>Tax Liability</li>
        </ol>
    </div>
    <div class="box box-primary">
       <div class="box-header with-border">
            <div class="col-md-2">
                <div class="form-group">
                    <label>Section:&nbsp;</label>
                    <asp:Literal ID="litSection" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
        <div class="box-body table-responsive no-padding">
            <asp:ListView ID="lv_Adv_Adjustment_GSTR1" runat="server" DataKeyNames="RATE_ID">
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
                            <asp:HiddenField ID="hdnRateId" runat="server" Value='<%# RateId = Convert.ToInt32(Eval("RATE_ID")) %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblRate"><strong><%# Eval("Rate")+ " %" %></strong></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_GrossAdvanceReceived" CssClass="form-control text-right" onkeypress="return onlyNos(event,this);" Text='<%# TotalTaxableAmount().Where(x=>x.RateId== RateId).FirstOrDefault()!=null? TotalTaxableAmount().Where(x=>x.RateId== RateId).FirstOrDefault().GrossAdvance.ToString() : "0.0" %>' onkeyup="calculateIGSTAdv(this)" runat="server"></asp:TextBox>
                            <asp:HiddenField ID="hdnOfflineDataId" runat="server" Value='<%# TotalTaxableAmount().Where(x=>x.RateId== RateId).FirstOrDefault()!=null? TotalTaxableAmount().Where(x=>x.RateId== RateId).FirstOrDefault().OfflineDataID.ToString() : "0" %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="txtIGST" CssClass="form-control text-right" onkeypress="return onlyNos(event,this);" Text='<%# TotalTaxableAmount().Where(x=>x.RateId== RateId).FirstOrDefault()!=null?TotalTaxableAmount().Where(x=>x.RateId== RateId).FirstOrDefault().IGSTAmt.ToString():"0.0"%>' runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCess" CssClass="form-control text-right" onkeypress="return onlyNos(event,this);" runat="server" Text='<%#TotalTaxableAmount().Where(x=>x.RateId== RateId).FirstOrDefault()!=null? TotalTaxableAmount().Where(x=>x.RateId== RateId).FirstOrDefault().CessAmt.ToString() : "0.0" %>'></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table class="table table-responsive ">
                        <thead>
                            <tr>
                                <th style="text-align: center;"></th>
                                <th style="text-align: left;">Rate</th>
                                <th style="text-align: right;">Gross Advance Received</th>
                                <th style="text-align: right;">IGST</th>
                                <th style="text-align: right;">Cess</th>
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
            <uc1:uc_sucess runat="server" ID="uc_sucess" />
            <br />
            <asp:LinkButton ID="lkbSave" CssClass="btn btn-primary pull-right" runat="server" ToolTip="Save" OnClick="lkbSave_Click"><i class="fa fa-save"></i>&nbsp;Save</asp:LinkButton>
            <asp:LinkButton ID="lkbBack" CssClass="btn btn-danger pull-left" ToolTip="Back" OnClick="lkbBack_Click" runat="server"><i class="fa fa-backward"></i>&nbsp;Back</asp:LinkButton>
        </div>
    </div>
</div>
