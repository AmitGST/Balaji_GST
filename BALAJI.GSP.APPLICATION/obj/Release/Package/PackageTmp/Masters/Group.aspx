<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Group.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.Group" %>

<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content-header">
        <h1>Group
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">Group</li>
        </ol>
    </div>

    <div class="content">

        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Add</h3>
            </div>
            <div id="validation" runat="server">
                <div class="box-body">
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>Item Type</label>
                                <asp:DropDownList ID="ddlItemType" CssClass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlItem" runat="server" ValidationGroup="vgGroup" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlItemType"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <%--</div>
                <div class="row">--%>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>Group Code</label>
                                <asp:TextBox ID="txtGroupCode" class="form-control" onkeypress="return isDigitKey(event,this);" onpaste="return false;" autocomplete="off" MaxLength="5" placeholder="Group Code" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvgroupcode" runat="server" ValidationGroup="vgGroup" CssClass="help-block" Display="Dynamic" ErrorMessage="Please specify the Group code" ControlToValidate="txtGroupCode"></asp:RequiredFieldValidator>
                                <h5 id="codecheck"></h5>
                            </div>
                        </div>
                        <div class="col-offset-sm-9"></div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label>Description</label>
                                <asp:TextBox ID="txtDescription" CssClass="form-control" placeholder="Description limit is 500 characters." MaxLength="500" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDescription" CssClass="help-block" ValidationGroup="vgGroup" Display="Dynamic" ControlToValidate="txtDescription" runat="server" ErrorMessage="This field cannot be left empty"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-6"></div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <uc1:uc_sucess runat="server" ID="uc_sucess" />
                            <asp:Button ID="btnGroup" CssClass="btn btn-primary" ValidationGroup="vgGroup" runat="server" Text="Submit" OnClick="btnGroup_Click" />
                        </div>
                    </div>
                </div>

            </div>
            <%--      <div class="box">--%>
            <div class="box-body table-responsive no-padding">
                <asp:ListView ID="lvItems" runat="server" OnPagePropertiesChanging="lvItems_PagePropertiesChanging">
                    <EmptyDataTemplate>
                        <table class="table table-responsive">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%>.</td>
                            <td>
                                <%# Eval("GroupCode") %>
                            </td>
                            <td>
                                <%# Eval("Description") %>
                            </td>
                            <td><%#Eval("Status").ToString()=="False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("Status") %></span> </td>
                            <td>
                                <asp:LinkButton ID="lkb" runat="server" OnClick="lkbGroup_Click" CommandArgument='<%# Eval("GroupID") %>' ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive">
                            <tr>
                                <th style="width: 5%">#</th>
                                <th style="width: 15%">Group Code</th>
                                <th style="width: 60%">Description</th>
                                <th style="width: 15%">Status</th>
                                <th style="width: 5%">#</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <div class="box-footer clearfix">
                    <div class="pagination pagination-sm no-margin">
                        <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvItems" PageSize="10" class="btn-group-sm pager-buttons">
                            <Fields>
                                <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                                <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </div>
            </div>
            <%-- </div>--%>
        </div>

    </div>
</asp:Content>

