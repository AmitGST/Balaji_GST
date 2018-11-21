<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_ViewSummary.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_ViewSummary" %>

  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                               <asp:LinkButton ID="lkbGenerateJSON" runat="server" Text="Generate JSON" CssClass="btn btn-primary pull-left" OnClick="lkbGenerateJSON_Click" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lkbGenerateJSON" />
                            </Triggers>
                        </asp:UpdatePanel>
