<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_UserList.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.uc_UserList" %>
<%@ Register Src="~/Account/uc_CreateGroup.ascx" TagPrefix="uc1" TagName="uc_CreateGroup" %>
<%@ Register Src="~/Account/uc_Groups.ascx" TagPrefix="uc1" TagName="uc_Groups" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Src="~/UC/uc_modalUserList.ascx" TagPrefix="uc1" TagName="uc_modalUserList" %>
<%@ Register Src="~/UC/uc_modelUser.ascx" TagPrefix="uc1" TagName="uc_modelUser" %>

<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">Users</h3>
    </div>
    <div class="box-body">
        <uc1:uc_sucess runat="server" ID="uc_sucess" />
        <div class="table-responsive no-padding">
            <asp:ListView ID="lvUsers" runat="server" OnPagePropertiesChanging="lvUsers_PagePropertiesChanging">
                <EmptyDataTemplate>
                    <table class="table table-striped">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Container.DataItemIndex + 1%>.</td>
                        <td>
                            <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("UserName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="GSTNNoLabel" runat="server" Text='<%# Eval("GSTNNo") %>' />
                        </td>
                        <td>
                            <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") +" "+ Eval("LastName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />

                        </td>
                        <td>
                            <%--<asp:Label ID="UserType" runat="server" Text='<%#Common.UserType(DataBinder.Eval(Container.DataItem,"UserType").ToString()) %>' />--%>
                            <%#Common.UserType(DataBinder.Eval(Container.DataItem,"UserType").ToString()) %>
                        </td>
                        <td>
                            <asp:Label ID="StateName" runat="server" Text='<%#UserStateName(Eval("StateCode").ToString()) %>' />
                        </td>
                        <td>
                            <asp:Label ID="Organization" runat="server" Text='<%# Eval("OrganizationName") %>' /></td>
                        <td>
                            <asp:LinkButton ID="lkbAddSignatory" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalSIGN"%>' runat="server" CssClass="btn" data-trigger="hover" data-placement="right" ToolTip="Add Signatory"><i class="fa fa-eye text-orange"></i></asp:LinkButton>

                            <div class="modal" id='<%# Container.DataItemIndex +"_myModalSIGN"%>' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                <div class="modal-dialog" style="width: 60%;">
                                    <asp:UpdatePanel ID="upviewPurchaseRegigsterModel" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                        &times;
                                                    </button>
                                                    <h4 class="modal-title">
                                                        <b><i class="fa fa-user"></i>
                                                            <asp:Label ID="k" runat="server" Text="Signatory Person"></asp:Label></b>
                                                    </h4>
                                                </div>
                                                <div class="modal-body">
                                                    <%-- DataSource='<%#DataBinder.Eval(Container.DataItem,"GST_MST_USER_SIGNATORY") %>'--%>
                                                    <uc1:uc_modelUser UserID='<%# Eval("ID") %>' runat="server" ID="uc_modelUser" />
                                                    <%--     <uc1:uc_modelUser user runat="server" id="uc_modelUser" />--%>
                                                    <%-- <uc1:uc_modalUserList runat="server" UserID='<%# Eval("ID") %>' ID="uc_modalUserList" />--%>
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
                        <td>
                            <asp:LinkButton ID="lkb" OnClick="lkb_Click" CommandArgument='<%#Eval("ID") %>' runat="server" ToolTip="Assign to group"><i class="fa fa-object-group"></i></asp:LinkButton>

                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table class="table table-striped">
                        <tr>
                            <th style="width: 10px">#</th>
                            <th>Login Name</th>
                            <th>GSTIN</th>
                            <th>Name</th>
                            <th>Email</th>
                            <th>User Type</th>
                            <th>State Name</th>
                            <th>Organization</th>
                            <th>Signatory</th>
                            <th>Action</th>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </tbody>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
        </div>

        <%--<div class="row">
                <div class="col-sm-12">
                    <div class="box-group" id="accordion1">
                        <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
                        <div class="panel box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" class="collapsed">Signatory
                                    </a>
                                </h3>
                            </div>
                            <div id="collapseTwo" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
                                <div class="box-body">
                                   
                                  
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>

        <%--<asp:DataPager ID="DataPagerProducts" runat="server" PagedControlID="LvCategoryItems" PageSize="10" OnPreRender="PagerCategoryItems_PreRender" class="btn-group pager-buttons">
                <Fields>
                    <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn" RenderNonBreakingSpacesBetweenControls="false" />
                    <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                    <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn" RenderNonBreakingSpacesBetweenControls="false" />
                </Fields>
            </asp:DataPager>--%>

        <div class="table no-padding" style="table-layout: fixed;">
            <uc1:uc_Groups runat="server" ID="uc_Groups" />
        </div>
        <div>
            <asp:Button ID="btnAssingGroup" runat="server" class="btn btn-primary" OnClick="btnAssingGroup_Click" Text="Assign user to group(s)" />
        </div>
    </div>
    <div class="box-footer">
        <div class="dataTables_paginate paging_simple_numbers pull-right">
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvUsers" PageSize="10" class="btn-group-sm pager-buttons">
                <Fields>
                    <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                    <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                    <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                    <%-- <asp:NextPreviousPagerField ButtonType="Button" ButtonCssClass="btn" FirstPageText="First" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                        <asp:NumericPagerField NumericButtonCssClass="paginate_button" CurrentPageLabelCssClass="paginate_button active" NextPreviousButtonCssClass="paginate_button next" />
                        <asp:NextPreviousPagerField ButtonType="Button" ButtonCssClass="paginate_button next" LastPageText="Last" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                    --%>
                </Fields>
            </asp:DataPager>
        </div>

    </div>


</div>
<div class="box box-danger">
    <div class="box-header with-border">

        <h3 class="box-title">ADD Signatory </h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
        </div>
    </div>
    <div class="box-body">
        <uc1:uc_modalUserList runat="server" ID="uc_modalUserList" />

    </div>
</div>
<%--<script>
    $(document).ready(function () {
        $('#lkbAddSignatory').on('shown.bs.modal', function () {
            $(this).find('.modal-dialog').css({
                width: 'auto',
                height: 'auto',
                'max-height': '100%'
            });
        });
    });
</script>--%>

