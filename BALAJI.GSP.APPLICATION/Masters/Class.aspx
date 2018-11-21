<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Class.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.Class" %>

<%@ Register Src="../UC/uc_sucess.ascx" TagName="uc_sucess" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>Class
            <small></small></h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">Class</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Add</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Select Sub-Group Code</label>
                            <asp:DropDownList ID="ddlSubGroup" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlSubGroup" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgClass" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlSubGroup"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Class-Code</label>
                            <asp:TextBox ID="txtClassCode" class="form-control" onkeypress="return isDigitKey(event);" onpaste="return false;" autocomplete="off" placeholder="Class-Code" MaxLength="8" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvClassCode" CssClass="help-block" Display="Dynamic" ValidationGroup="vgClass" ControlToValidate="txtClassCode" runat="server" ErrorMessage="Please specify the Class Code"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label>Description</label>
                            <asp:TextBox ID="txtDescription" CssClass="form-control" TextMode="MultiLine" placeholder="Description limit is 500 characters." Rows="3" autocomplete="off" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescription" CssClass="help-block" Display="Dynamic" ValidationGroup="vgClass" ControlToValidate="txtDescription" runat="server" ErrorMessage="This field cannot be left empty"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <uc1:uc_sucess ID="uc_sucess1" runat="server" />
                        <asp:Button ID="btnClass" CssClass="btn btn-primary" runat="server" ValidationGroup="vgClass" Text="Submit" OnClick="btnClass_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="box-body table-responsive no-padding">
                        <asp:ListView ID="lvclass" runat="server" OnPagePropertiesChanging="lvclass_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <table class="table ">
                                    <tr>
                                        <td>No data was returned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.DataItemIndex + 1%>.</td>
                                    <td><%#DataBinder.Eval(Container.DataItem,"GST_MST_SUBGROUP.SubGroupCode")%></td>
                                    <td>
                                        <%# Eval("ClassCode") %>
                                    </td>
                                    <td>
                                        <%# Eval("classDescription") %>
                                    </td>
                                    <td><%#Eval("Status").ToString()=="False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("Status") %></span></td>

                                    <td>
                                        <asp:LinkButton ID="lkb" runat="server" OnClick="lkbClass_Click" CommandArgument='<%# Eval("ClassID") %>' ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table class="table">
                                    <tr>
                                        <th style="width: 5px;">#</th>
                                        <th style="width: 10px">Sub-Group Code</th>
                                        <th style="width: 10px">Class Code</th>
                                        <th style="width: 60px">Description</th>
                                        <th style="width: 10px">Status</th>
                                        <th style="width: 5px">#</th>
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
                                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvclass" PageSize="10" class="btn-group-sm pager-buttons">
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
