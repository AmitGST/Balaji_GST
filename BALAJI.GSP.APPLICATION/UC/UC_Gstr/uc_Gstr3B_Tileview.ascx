<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_Gstr3B_Tileview.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.UC_Gstr.uc_Gstr3B_Tileview" %>
<%@ Register Src="~/UC/UC_Invoice/uc_invoiceMonth.ascx" TagPrefix="uc1" TagName="uc_invoiceMonth" %>
<uc1:uc_invoiceMonth runat="server" ID="uc_invoiceMonth" Visible="false" />
<asp:Repeater ID="rptGSTR3B"  runat="server">
   
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
       
        <div class="col-md-4 no-padding" style="padding: 5px !important;">
            <div class='<%#Eval("Css")%>'>
                 <div class="box-header with-border"><h5 style="font-weight:bold !important;"><%#Eval("Description") %></h5>
                       </div>
                <div class="inner">
                   <div class="icon">
                    <i class="fa fa-file-text-o" style="padding-top:76px !important;"></i>
                </div>
                    <div class="row">
                        <div class="col-md-12">
                            <p>
                                <b>Total Value  :</b> <%#Eval("TotalValue") %>
                            </p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <p>
                                <b>Total Taxable value :</b> <%#Eval("TotalTaxableValue") %>
                            </p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <p><b>Total Tax Liability:</b> <%#Eval("TotalTaxLiability") %></p>
                        </div>
                    </div>
                </div>
               
                <asp:LinkButton ID="lbinfo" CssClass="small-box-footer" runat="server">View Detail <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>