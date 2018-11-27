<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_Gstr_Tileview.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.UC_Gstr.uc_Gstr_Tileview" %>


<asp:Repeater ID="rptGSTR1"  runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <div class="col-md-4 no-padding" style="padding: 5px !important;">
            <div class='<%#BusinessLogic.Repositories.cls_Invoice.GstrColor(Eval("ReturnStatus")) %>'>
                <div class="box-header with-border">
                        <h5 style="color: white;"><%#((GST.Utility.EnumConstants.Return)Convert.ToInt32(Eval("ReturnStatus").ToString())).ToDescription()%> </h5>
                    </div>
                <div class="inner">
                    <div class="icon" style="padding-top:68px !important; font-size:45px !important;"> 
                    <i class="fa fa-file-text-o"></i>
                </div>
                        <div id="afterFile" runat="server">
                             <div class="row">
                            <div class="col-md-4">
                            Status:   <%#((GST.Utility.EnumConstants.ReturnFileStatus)Convert.ToInt32(Eval("Status").ToString())).ToDescription() %>
                        <%--<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_AUDIT_TRAIL.AuditTrailStatus")%></p>--%>
                                    <%-- <%# Eval("AuditrailStatus")%>--%>
                                   
                           
                        
                       
                    </div>
                       </div>
                    <div id="beforeFile" runat="server" visible="false">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:LinkButton ID="btnPrepareOffline" Text="Prepare Offline" OnClick="btnPrepareOffline_Click" runat="server" CssClass="btn btn-primary" />
                            </div>
                            <div class="col-md-3">
                                <asp:LinkButton ID="btnPrepareOnline" Text="Prepare Online" OnClick="btnPrepareOnline_Click" runat="server" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
              
                </div>
               </div>
                <asp:LinkButton ID="lbinfo" CssClass="small-box-footer" OnClick="lbinfo_Click" CommandArgument='<%# Eval("ReturnStatus")%>' runat="server">View Details <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
             </div>
             </div>
             </ItemTemplate>
</asp:Repeater>

<%--<asp:Repeater ID="rptGsSTR3B" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <div class="col-md-4 no-padding" style="padding: 5px !important;">
            <div class="small-box bg-red">
                <div class="box-header with-border">
                        <h5 style="color: white;"><%#GST.Utility.EnumConstants.Return.Gstr3B.ToDescription()%> </h5>
                    </div>
                <div class="inner">
                    <div class="icon" style="padding-top:68px !important; font-size:45px !important;"> 
                    <i class="fa fa-file-text-o"></i>
                </div>
                        <div class="afterFile" runat="server">
                             <div class="row">
                            <div class="col-md-4">
                              
                                  <h5> Status: <%#((GST.Utility.EnumConstants.ReturnFileStatus)Convert.ToInt32(Eval("Status").ToString())).ToDescription() %></h5>--%>
                        <%--<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_AUDIT_TRAIL.AuditTrailStatus")%></p>--%>
                                    <%-- <%# Eval("AuditrailStatus")%>
                                   
                           
                        
                       
                    </div>
                       </div>
                    <div id="beforeFile" runat="server" visible="false">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Button ID="btnPrepareOffline" Text="Prepare Offline" OnClick="btnPrepareOffline_Click" runat="server" CssClass="btn btn-primary" />
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btnPrepareOnline" Text="Prepare Online" OnClick="btnPrepareOnline_Click" runat="server" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
              
                </div>
               </div>
                <asp:LinkButton ID="lbinfo" CssClass="small-box-footer" OnClick="lbinfo_Click" runat="server">View GSTR-1 <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
             </div>
             </div>
             </ItemTemplate>
</asp:Repeater>--%>