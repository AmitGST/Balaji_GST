<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_Gstr_Tileview.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.UC_Gstr.uc_Gstr_Tileview" %>


<asp:Repeater ID="rptGSTR1" runat="server" >
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <div class="col-md-4 no-padding" style="padding: 5px !important;">
            <div class='<%#BusinessLogic.Repositories.cls_Invoice.GstrColor(Eval("ReturnStatus")) %>'>
                <div class="box-header with-border">
                    <h5 style="color: white;"><%# Eval("HeaderName") %> <%#InvoiceCount(Eval9,"GST_TRN_INVOICE.InvoiceUserID",3) %>DataBinder.InvoiceCount(Eval("userId").ToString()</h5> 
                    <%--  <%#((GST.Utility.EnumConstants.Return)Convert.ToInt32(Eval("ReturnStatus").ToString())).ToDescription()%> --%>
                </div>
                <div class="inner">



                    <%--<div class="row">
                            <div class="col-md-4">
                          <%--  Status:   <%#((GST.Utility.EnumConstants.ReturnFileStatus)Convert.ToInt32(Eval("Status").ToString())).ToDescription()
                        <%--<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_AUDIT_TRAIL.AuditTrailStatus")%></p>--
                                    <%# Eval("AuditrailStatus")%>
                    </div>
                       </div>--%>


                    <%-- visible='<%#Eval("Action")==(byte)(EnumConstant.C)=false:true; %>'--%>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:LinkButton ID="btnPrepareOffline" Text="Prepare Offline" CommandName="Offline" CommandArgument='<%# Eval("ReturnStatus")%>' OnClick="lbinfo_Click" runat="server" CssClass="btn btn-primary" />
                        </div>
                        <div class="col-md-4">
                            <asp:LinkButton ID="btnPrepareOnline" CommandName="Online" CommandArgument='<%# Eval("ReturnStatus")%>' Text="Prepare Online" OnClick="lbinfo_Click" runat="server" CssClass="btn btn-primary" />
                        </div>
                        <div class="col-md-4"></div>
                    </div>

                </div>
                <div class="icon">
                    <i class="fa fa-book"></i>
                </div>
                <asp:LinkButton ID="lbinfo" CssClass="small-box-footer" CommandName="ViewDetail" OnClick="lbinfo_Click" CommandArgument='<%# Eval("ReturnStatus")%>' runat="server">View Details <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
