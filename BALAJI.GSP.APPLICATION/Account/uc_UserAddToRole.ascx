<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_UserAddToRole.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.uc_UserAddToRole" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>


<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">Assign Role</h3>
    </div>
    <div class="box-body">
        <div class="form-group">
            <uc1:uc_sucess runat="server" ID="uc_sucess" />
        </div>

        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <asp:TextBox ID="txtFindUsers" CssClass="form-control" Width="250px" placeholder="Users" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-1">
                <asp:Button ID="btnGetUsers" class="btn btn-primary" runat="server" OnClick="btnGetUsers_Click" Text="Get User(s)" />
            </div>
            <div class="col-md-8">
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="box-body no-padding">
                    <%--<asp:GridView ID="lvRoles1" runat="server" ItemType="BALAJI.GSP.APPLICATION.Model.ApplicationUser" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUserName" Text='<%#:Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    &nbsp;|&nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>--%>
                    <asp:ListView runat="server" ItemType="BALAJI.GSP.APPLICATION.Model.ApplicationUser" ID="lvRoles" OnPagePropertiesChanging="lvRoles_PagePropertiesChanging">
                        <LayoutTemplate>

                            <table class="table table-condensed">

                                <tr>
                                    <th style="width: 10px">#</th>
                                    <th>User Name</th>
                                    <th>Action</th>
                                </tr>

                                <tbody>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.DataItemIndex + 1%>.</td>
                                <td><%#:Item.UserName%></td>
                                <td>
                                    <asp:LinkButton runat="server" OnClick="lkbAssingRoles_Click" ToolTip="Group/UnGroup" ID="LinkButton1" CommandArgument='<%#:Item.Id%>' UserName='<%#:Item.UserName%>'><i class="fa fa-object-group"></i> / <i class="fa fa-object-ungroup"></i></asp:LinkButton>
                                </td>

                            </tr>
                        </ItemTemplate>

                    </asp:ListView>

                </div>
                <div class="box-footer clearfix">
                    <div class="pagination pagination-sm no-margin">
                        <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
                        <asp:DataPager ID="dproles" runat="server" PagedControlID="lvRoles" PageSize="10" class="btn-group-sm pager-buttons">
                            <Fields>
                                <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                                <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <asp:PlaceHolder runat="server" ID="phUserAddRole">Add "<asp:Literal ID="litUserName" runat="server"></asp:Literal>" to given role:
                </asp:PlaceHolder>
                <div class="radiobuttonlist ">
                    <asp:RadioButtonList runat="server" ID="chkRoleList" DataTextField="Name" DataValueField="ID" RepeatDirection="Vertical" RepeatLayout="Flow"></asp:RadioButtonList>
                </div>
                <div class="footer">

                    <asp:Button ID="btnAddUserToRole" class="btn btn-primary pull-left" OnClick="btnAddUserToRole_Click" runat="server" Text="Add user to Role(s)" CommandName="addRole" />

                    <asp:Button ID="btnRemoveUserFromRole" class="btn btn-primary pull-right" OnClick="btnAddUserToRole_Click" CommandName="removeRole" runat="server" Text="Remove user to Role(s)" />
                </div>
            </div>
        </div>
    </div>
</div>










