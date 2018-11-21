<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="SubClass.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.SubClass" %>

<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>Sub-Class
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">Sub-Class</li>
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
                            <label>Select Class Code</label>
                            <asp:DropDownList ID="ddlClass" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlClass" runat="server" ValidationGroup="vgSubClass" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlClass"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <%-- </div>
                <div class="row">--%>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Sub-Class Code</label>
                            <asp:TextBox ID="txtSubClsCode" class="form-control" onkeypress="return isDigitKey(event);" onpaste="return false;" MaxLength="10" autocomplete="off" placeholder="Sub-Class Code" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSubClsCode" CssClass="help-block" ValidationGroup="vgSubClass" Display="Dynamic" ControlToValidate="txtSubClsCode" runat="server" ErrorMessage="Please specify the Sub-Class Code"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label>Description</label>
                            <asp:TextBox ID="txtDescription" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500" runat="server" autocomplete="off" placeholder="Description limit is 500 characters."></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescription" CssClass="help-block" ValidationGroup="vgSubClass" Display="Dynamic" ControlToValidate="txtDescription" runat="server" ErrorMessage="This field cannot be left empty"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-12">
                        <uc1:uc_sucess runat="server" ID="uc_sucess" />
                        <asp:Button ID="btnSubClass" CssClass="btn btn-primary" ValidationGroup="vgSubClass" runat="server" Text="Submit" OnClick="btnSubClass_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="box-body table-responsive no-padding">
                        <asp:ListView ID="lvsubclass" runat="server" OnPagePropertiesChanging="lvsubclass_PagePropertiesChanging">
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
                                    <%--<td><%#DataBinder.Eval(Container.DataItem,"GST_MST_GROUP.GroupCode")%></td>--%>
                                    <%--<td><%#DataBinder.Eval(Container.DataItem,"GST_MST_SUBGROUP.SubGroupCode")%></td>--%>
                                    <td><%#DataBinder.Eval(Container.DataItem,"GST_MST_CLASS.ClassCode")%></td>
                                    <td>
                                        <%# Eval("SubClassCode") %>
                                    </td>
                                    <td>
                                        <%# Eval("Description") %>
                                    </td>
                                    <td><%#Eval("Status").ToString()=="False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("Status") %></span> </td>
                                    <td>
                                        <asp:LinkButton ID="lkb" runat="server" CommandArgument='<%# Eval("SubClassID") %>' OnClick="lkbSubClass_Click" ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table class="table">
                                    <tr>
                                        <th style="width: 10px">#</th>
                                        <th>Class Code</th>
                                        <th>Sub-Class Code</th>
                                        <th>Description</th>
                                        <th>Status</th>
                                        <th>#</th>
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
                                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvsubclass" PageSize="10" class="btn-group-sm pager-buttons">
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
        <%--<script type="text/javascript">
               $(document).ready(function () {
                $("[id*=btnSubClass").bind("click", function () {
                    var t1 = $("#<%=txtSubClsCode.ClientID%>").val();
                     var t2 = $("#<%=txtDescription.ClientID%>").val();
                     var t3 = $("#<%=ddlClass.ClientID%>").val();
                     if (t1 == "" || t2 == "" || t3 <= 0) {
                         alert("Field Cannot be null");
                         return false;
                     }
                 })
             });
          
</script>--%>

</asp:Content>
