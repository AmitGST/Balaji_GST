<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="ErrorLog.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.ErrorLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="content-header">
        <h1>Error Handling</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">Error Handling</li>
        </ol>
    </div>
       <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Error Log Management</h3>
            </div>
            <div class="box-body">
           <asp:ListView ID="lv_ErrorHandling" OnItemEditing="lv_ErrorHandling_ItemEditing" OnItemDataBound="lv_HSN_ItemDataBound" OnItemUpdating="lv_ErrorHandling_ItemUpdating" OnItemCreated="lv_ErrorHandling_ItemCreated" DataKeyNames="ErrorLogId" OnPagePropertiesChanging="lv_ErrorHandling_PagePropertiesChanging" runat="server">
               <EmptyDataTemplate>
                   <table class="table">
                  <tr>
                  <td>No data was returned.</td>
                  </tr>
                  </table>
               </EmptyDataTemplate>
                 <ItemTemplate>
                <tr>
                  <td>
                        <%# Eval("UserIP") %>
                   </td>
                  <td>
                  <%# Eval("ErrorPage")%>
                   </td>
          <td>
            <%#Eval("RequestUrl") %>
            </td>
        <td>
            <asp:DropDownList ID="ddlStatus" runat="server" SelectMethod="BindErrorStatus" Width="100%" DataTextField="Key" DataValueField="Value" Class="form-control input-sm" ></asp:DropDownList>
            <asp:HiddenField ID="hdnStatus" Value='<%#Eval("Status") %>' runat="server" />
            </td>
          <td>
            <%#DateTimeAgo.GetFormatDate(Eval("CreatedDate")) %>
            </td>
         <td>
         <%#Eval("CreatedBy") %>
            </td>
        <%--<td>
         <%#Eval("ServerName") %>
            </td>--%>
        <%--<td>
             <%#Eval("ErrorSource") %>
            </td>--%>
        <td>
           <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary btn-xs" CommandName="Edit" data-trigger="hover" data-placement="right" title="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
      <asp:LinkButton ID="lkbAddSignatory" CssClass="btn btn-warning btn-xs" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalSIGN"%>' runat="server" data-trigger="hover" data-placement="right" ToolTip="Add Signatory"><i class="fa fa-eye"></i></asp:LinkButton>
            <div class="modal" id='<%# Container.DataItemIndex +"_myModalSIGN"%>' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                <div class="modal-dialog" style="width: 80%;">
                                    <asp:UpdatePanel ID="upviewPurchaseRegigsterModel" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                        &times;
                                                    </button>
                                                    <h4 class="modal-title">
                                                        <b><i class="fa fa-user"></i>
                                                            <asp:Label ID="lblModalTitle" runat="server" Text="Error Log"></asp:Label></b>
                                                    </h4>
                                                </div>
                                                <div class="modal-body">
                                                    <table style="border: 1px solid black; border-collapse: collapse;">
                                                        <tbody>
                                                         <tr>
                                                            <th style="border: 1px solid black">Message</th>
                                                            <th style="border: 1px solid black">Target Site</th>
                                                            <th style="border: 1px solid black">Stack Trace</th>
                                                        </tr>
                                                        <tr>
                                                   <td style="border: 1px solid black"><%#Eval("Message")%></td>
                                                   <td style="border: 1px solid black"> <%#Eval("TargetSite")%></td>
                                                 <td style="border: 1px solid black"><%#Eval("StackTrace")%></td> 
                                                            </tr>
                                                       </tbody>
                                                        </table>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-info pull-right" data-dismiss="modal" aria-hidden="true">Close</button>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
            </td>
        </tr>
                </ItemTemplate>
              <EditItemTemplate>
                   <tr>
                  <td>
                       <%# Eval("UserIP") %>
                   </td>
                  <td>
                <%# Eval("ErrorPage")%>
                   </td>
                  
          <td>
            <%#Eval("RequestUrl") %>
            </td>
        <td> 
            <asp:DropDownList ID="ddlStatus" runat="server" SelectMethod="BindErrorStatus" DataTextField="Key" DataValueField="Value" Width="100%"  Class="form-control input-sm"></asp:DropDownList>
            <asp:HiddenField ID="hdnStatus"  Value='<%#Eval("Status") %>'  runat="server" />
            </td>
          <td>
           <%# DateTimeAgo.GetFormatDate(Eval("CreatedDate"))%>
            </td>
         <td>
             <%#Eval("CreatedBy") %>
            </td>
       <%-- <td>
           <%#Eval("ServerName") %>
            </td>--%>
        <%--<td>
            <%#Eval("ErrorSource") %>
            </td>--%>
        <td>
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkbUpdate" />
                            </Triggers>
                        </asp:UpdatePanel>
         <asp:LinkButton ID="lkbUpdate" runat="server" CommandArgument='<%# Eval("ErrorLogId") %>' CommandName="Update" CssClass="btn btn-success btn-xs"><i class="fa fa-save"></i></asp:LinkButton>
    <%--      <asp:LinkButton ID="lkbView" runat="server" data-toggle="modal" CssClass="btn btn-warning btn-xs" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>'"><i class="fa fa-eye"></i></asp:LinkButton>--%>
        </td>
        </tr>
               </EditItemTemplate>
                <LayoutTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th style="vertical-align: bottom; width: 10%" >User IP</th>
                            <th style="vertical-align: bottom; width: 15%" >Error Page</th>
                            <th style="vertical-align: bottom; width: 27%">Request Url</th>
                            <th style="vertical-align: bottom; width: 10%">Status</th>
                              <th style="vertical-align: bottom; width: 10%">Created Date</th>
                              <th style="vertical-align: bottom; width: 13%">Created By</th>
                            <%--<th style="vertical-align: bottom; width: 15%">Server Name</th>--%>
                            <th style="vertical-align:bottom; width:5%">View</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server" />
                    </tbody>
                </table>
            </LayoutTemplate>
    </asp:ListView>
                </div>
            <div class="box-footer clearfix">
      <div class="pagination pagination-sm no-margin pull-right">
      <asp:DataPager ID="dpError" runat="server" PagedControlID="lv_ErrorHandling" PageSize="10" class="btn-group-sm pager-buttons">
      <Fields>
      <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
      <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
      <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
      </Fields>
      </asp:DataPager>
      </div>
      </div>
            </div>
           </div>
      
</asp:Content>
