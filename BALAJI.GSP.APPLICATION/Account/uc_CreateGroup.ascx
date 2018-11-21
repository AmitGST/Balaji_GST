<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_CreateGroup.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.uc_CreateGroup" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>



<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">Create Group</h3>
    </div>
    <div class="box-body">
        <div class="row">
            <div class="col-md-3 col-md-9">
                <div class="form-group">
                    <asp:TextBox ID="txtGroupName" CssClass="form-control" onkeypress="return isAlpha(event,this);" onpaste="return false;" autocomplete="off" placeholder="Group Name" MaxLength="20" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvGroupName" runat="server" CssClass="help-block" ControlToValidate="txtGroupName" ErrorMessage="Please enter group name" ValidationGroup="creategroup"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-md-9">
                <div class="form-group">
                    <asp:TextBox ID="txtDescription" CssClass="form-control" placeholder="Group Description" TextMode="MultiLine" Rows="3" MaxLength="50" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvGroupDes" runat="server" CssClass="help-block" ControlToValidate="txtDescription" ErrorMessage="Please Enter group description" ValidationGroup="creategroup"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="btnCreateGroup" class="btn btn-primary" OnClick="btnCreateGroup_Click" ValidationGroup="creategroup" runat="server" Text="Create Group" />
            </div>
        </div>
        <uc1:uc_sucess runat="server" ID="uc_sucess" />
    </div>
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lvGroups" runat="server" ItemType="BALAJI.GSP.APPLICATION.Model.ApplicationGroup" SelectMethod="Groups">
            <LayoutTemplate>
                <table class="table">
                    <tr>
                        <th style="width: 10px">#</th>
                        <th style="width: 20px">Group Name</th>
                        <th style="width: 60px">Description</th>
                        <th style="width: 10px">Action</th> 
                    </tr>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Container.DataItemIndex + 1%>.</td>
                    <td><%#:Item.Name%></td>
                    <td><%#:Item.Description%></td>
                    <td>
                        <asp:LinkButton ID="lkbRemoveButton" runat="server" OnClick="RemoveGroup" CommandArgument='<%#:Item.Id%>' OnClientClick="return confirm('Are you sure you want delete');" ToolTip="Delete" CausesValidation="false"><i class="fa fa-trash-o text-red"></i></asp:LinkButton>
                        <%--   <asp:Button runat="server" Text="Remove"  />--%> 
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>

</div>

