<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="ReportPermission.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Admin.ReportPermission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>Report Permission
            <small></small></h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Report Permission</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Add Report</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>
                            Users</label>
                            <asp:DropDownList ID="ddl_Users" AutoPostBack="true" placeholder="Users" DataTextField="User" Class="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="box-body table-responsive no-padding">
                            <asp:ListView ID="lvReport" runat="server" OnPagePropertiesChanging="lvReport_PagePropertiesChanging" >
                                <EmptyDataTemplate>
                                    <table class="table">
                                        <tr>
                                            <td>No data was returned</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Container.DataItemIndex + 1%>.</td>
                                        <td><%# Eval("SerialNo") %></td>
                                        <td><%# Eval("ReportName") %></td>
                                        <td><%# Eval("ReportPath") %></td>
                                        <td><%# Eval("ReportControlName") %></td>
                                        <td><%# DateTimeAgo.GetFormatDate(Eval("CreatedDate").ToString()) %></td>
                                        <td><%#Eval("Status").ToString()== "False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("Status") %></td>
                                        <td>
                                            <asp:LinkButton ID="lkbView" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV_vs"%>' ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
                                        </td>
                                        <td>
                                            <div class="modal fade" id='<%# Container.DataItemIndex +"_myModalINV_vs"%>' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                            &times;
                                                            </button>
                                                            <h4 class="modal-title"><b><i class="fa fa-globe"></i>
                                                                <asp:Label ID="lblModalTitle" runat="server" Text="View Report"></asp:Label>
                                                                </b></h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="table table-responsive">
                                                                <%--     <asp:ListView ID="lvReportPermission" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem,"GST_MST_REPORT") %>'>
                                                                    <EmptyDataTemplate>
                                                                        <table class="table table-bordered table-condensed">
                                                                            <tr>
                                                                                <td>No data was returned.</td>
                                                                            </tr>
                                                                        </table>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Container.DataItemIndex + 1%></td>
                                                                            <td><%# Eval("ReportName") %></td>
                                                                            <td><%# Eval("ReportPath") %></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <LayoutTemplate>
                                                                        <table class="table table responsive table-condensed table-bordered">
                                                                            <tr>
                                                                                <th>#</th>
                                                                                <th>Report Name</th>
                                                                                <th>Report Path</th>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </tbody>
                                                                            </tr>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                </asp:ListView>--%>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">
                                                            Close
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <table class="table">
                                        <tr>
                                            <th style="width: 10px">#</th>
                                            <th>Serial No.</th>
                                            <th>Report Name</th>
                                            <th>Report Path</th>
                                            <th>Report Control Name</th>
                                            <th>Created Date</th>
                                            <th>Status</th>
                                            <th>View</th>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </tbody>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                            </asp:ListView>
                        </div>
                        <div class="box-footer clearfix">
                            <div class="pagination pagination-sm no-margin pull-right">
                            <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
                                <asp:DataPager ID="dpReportPermission" runat="server" PagedControlID="lvReport" PageSize="10" class="btn-group-sm pager-buttons">
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
        </div>
    </div>
</asp:Content>
