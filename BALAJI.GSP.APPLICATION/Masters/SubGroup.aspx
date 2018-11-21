<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="SubGroup.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.SubGroup" %>

<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>Sub-Group
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">Sub-Group</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Add</h3>
            </div>
            <div class="box-body">
                <div class="row">

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Item Type</label>
                            <asp:DropDownList ID="ddlItemType" CssClass="form-control" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlItem" runat="server" ValidationGroup="vgSubGroup" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlItemType"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <%-- </div>
                <div class="row">--%>
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Select Group Code</label>
                            <asp:DropDownList ID="ddlGroup" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlGroup" runat="server" CssClass="help-block" ValidationGroup="vgSubGroup" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlGroup"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <%--</div>
                <div class="row">--%>
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Sub-Group Code</label>
                            <asp:TextBox ID="txtSubGrpCode" class="form-control" onkeypress="return isDigitKey(event,this);" onpaste="return false;" autocomplete="off" placeholder="Sub-Group Code" MaxLength="8" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSubGrpCode" CssClass="help-block" ValidationGroup="vgSubGroup" Display="Dynamic" ControlToValidate="txtSubGrpCode" runat="server" ErrorMessage="Please specify the Sub-Group Code"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label>Description</label>
                            <asp:TextBox ID="txtDescription" CssClass="form-control" placeholder="Description limit is 500 characters." TextMode="MultiLine" autocomplete="off" Rows="3" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescription" CssClass="help-block" ValidationGroup="vgSubGroup" Display="Dynamic" ControlToValidate="txtDescription" runat="server" ErrorMessage="This field cannot be left empty"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-12">
                        <uc1:uc_sucess runat="server" ID="uc_sucess" />
                        <asp:Button ID="btnSubGroup" CssClass="btn btn-primary" runat="server" ValidationGroup="vgSubGroup" Text="Submit" OnClick="btnSubGroup_Click" />
                    </div>
                </div>
            </div>

            <div class="box-body table-responsive no-padding">
                <asp:ListView ID="lvSubgrp" runat="server" OnPagePropertiesChanging="lvSubgrp_PagePropertiesChanging">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%>.</td>
                            <td><%#DataBinder.Eval(Container.DataItem,"GST_MST_GROUP.GroupCode")%></td>
                            <td>
                                <%# Eval("SubGroupCode") %>
                            </td>
                            <td>
                                <%# Eval("Description") %>
                            </td>
                            <td><%#Eval("Status").ToString()=="False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("Status") %></span> </td>
                            <td>
                                <asp:LinkButton ID="lkbSubGroup" OnClick="lkbSubGroup_Click" CommandArgument='<%# Eval("SubGroupID") %>' runat="server" ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table">
                            <tr>
                                <th style="width: 5%">#</th>
                                <th style="width: 10%">Group Code</th>
                                <th style="width: 10%">SubGroup Code</th>
                                <th style="width: 60%">Description</th>
                                <th style="width: 10%">Status</th>
                                <th style="width: 5%">#</th>
                                <tbody class="table-responsive">
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <div class="box-footer clearfix">
                    <div class="pagination pagination-sm no-margin">
                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvSubgrp" PageSize="10" class="btn-group-sm pager-buttons">
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
    </div>
</asp:Content>
