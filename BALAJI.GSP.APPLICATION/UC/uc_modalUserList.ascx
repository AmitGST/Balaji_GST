<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_modalUserList.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.uc_modalUserList" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<div class="box-body">
    <div class="row">
        <div class="col-sm-3">
            <div class="form-group">
                <label>User List</label>
                <asp:DropDownList ID="ddlUserList" CssClass="form-control"  runat="server"></asp:DropDownList>
                 <asp:RequiredFieldValidator ID="rfvUserList" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgSignatory" InitialValue="0" ControlToValidate="ddlUserList" ErrorMessage="Value Required"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-group">
                <label>Name of Signatory</label>
                <asp:TextBox ID="txtSignatory" CssClass="form-control" onkeypress="return isAlpha(event,this);" placeholder="Name of Signatory" onpaste="return false;" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSignatory" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtSignatory" ValidationGroup="vgSignatory" ErrorMessage="Please enter the Signatory name"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-group">
                <label for="txt_statecode">State Name</label>
                <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCertKey" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="ddlState" ValidationGroup="vgSignatory" InitialValue="0" ErrorMessage="Value Required"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-group">
                <label>Organization Address</label>
                <asp:TextBox ID="txtOrgAddress" runat="server" CssClass="form-control" placeholder="Organization Address"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvOrgAdd" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtOrgAddress" ValidationGroup="vgSignatory" ErrorMessage="Please enter the Organization Address "></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-3">
            <div class="form-group">
                <label>GSTIN</label>
                <asp:TextBox ID="txtGSTIN" runat="server" CssClass="form-control gstinnumbers" MaxLength="15" placeholder="GSTIN"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvGSTIN" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtGSTIN" ValidationGroup="vgSignatory" ErrorMessage="Please enter theGSTIN"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-group">
                <label>Email</label>
                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control validateEmail" placeholder="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtemail" ValidationGroup="vgSignatory" ErrorMessage="Please enter the Email"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" CssClass="help-block" ErrorMessage="Email-Id is invalid" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ValidationGroup="vgSignatory"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-group">
                <label>Pin Code</label>
                <asp:TextBox ID="txtcode" runat="server" CssClass="form-control" MaxLength="6" onkeypress="return isDigitKey(event,this);" onpaste="return false;" placeholder="Pin Code"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvcode" runat="server" CssClass="help-block" ControlToValidate="txtcode" Display="Dynamic" ValidationGroup="vgSignatory" ErrorMessage="Please enter the Pin Code"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-group">
                <label>Contact Number</label>
                <asp:TextBox ID="txtphone" runat="server" CssClass="form-control validatephonenos" onkeypress="return isDigitKey(event,this);" onpaste="return false;" MaxLength="10" placeholder="Contact Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConNo" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtphone" ValidationGroup="vgSignatory" ErrorMessage="Please enter the Contact Number"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-3">
            <div class="form-group">
                <%--  <label >Certificate Key</label>
            <asp:TextBox ID="txtCertKey" runat="server" CssClass="form-control" Visible="false" placeholder="Certificate Key" Width="300px"></asp:TextBox>--%>
                <%--<asp:RequiredFieldValidator ID="rfvCertKey" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgSignatory" ErrorMessage="Please enter the Signatory name" ></asp:RequiredFieldValidator>--%>
            </div>
        </div>
    </div>
    <div class="row" style="display: none;">
        <div class="col-sm-4">
            <div class="form-group">
                <label></label>
                <asp:TextBox ID="txtzbc" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-8">
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <uc1:uc_sucess runat="server" ID="uc_sucess" />
            <asp:LinkButton ID="lkbAdd" OnClick="lkbAdd_Click" CssClass="btn btn-primary" ValidationGroup="vgSignatory" runat="server">Add Signatory</asp:LinkButton>
        </div>
    </div>
</div>
