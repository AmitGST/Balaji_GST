<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_TileView.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.uc_TileView" %>
                        <asp:Repeater ID="rptInvoices" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="col-md-4 no-padding" style="padding: 5px !important;">
                                    <div class='<%#BusinessLogic.Repositories.cls_Invoice.InvoiceColor(Eval("INVSPLCONDITION")) %>'>
                                        <div class="inner">
                                            <h4><%#((GST.Utility.EnumConstants.InvoiceSpecialCondition)Convert.ToInt32(Eval("INVSPLCONDITION").ToString())).ToDescription() %>(<%#Eval("TotalInvoice") %>)</h4>
                                           <div class="row">
                                             <div class="col-md-12"> <p>
                                                TAXABLEAMOUNT  : <%# Eval("TAXABLEAMOUNT")%>
                                            </p></div>
                                          </div>
                                           <div class="row">
                                                <div class="col-md-6"> <p>
                                               IGST : <%# Eval("IGSTAMT")%>
                                            </p></div>
                                               <div class="col-md-6"><p>CGST: <%# Eval("CGSTAMT")%></p></div>
                                               
                                              
                                           </div>
                                            <div class="row">
                                                <div class="col-md-6"><p>SGST: <%# Eval("SGSTAMT")%></p></div>
                                                <div class="col-md-6"> <p>CESS: <%# Eval("CESSAMT")%></p></div>
                                            </div>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-book"></i>
                                        </div>
                                        <asp:LinkButton ID="lbinfo" CssClass="small-box-footer" CommandArgument='<%# Eval("InvoiceType")%>' CommandName='<%# Eval("INVSPLCONDITION")%>' OnClick="lbinfo_Click" runat="server">View Detail <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                  
      
