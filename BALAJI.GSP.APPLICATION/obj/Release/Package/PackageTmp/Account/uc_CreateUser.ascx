<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_CreateUser.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.uc_CreateUser" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Create User</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtUserName">Login ID</label>
                            <asp:TextBox ID="txtUserName" runat="server" class="form-control" autocomplete="off" placeholder="Login ID" MaxLength="20" AutoCompleteType="None" ViewStateMode="Disabled" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" Display="Dynamic" ControlToValidate="txtUserName" CssClass="help-block" ErrorMessage="Please specify user name." ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revUserName" runat="server" Display="Dynamic" ControlToValidate="txtUserName" CssClass="help-block" ErrorMessage="Minimum user name length is 8 character" ValidationExpression=".{8}.*" ValidationGroup="createUser"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="txtFirstName">First Name</label>
                            <asp:TextBox ID="txtFirstName" class="form-control" autocomplete="off" placeholder="First Name" onkeypress="return ValidateAlpha(event);" onpaste="return false;" runat="server" MaxLength="15" TabIndex="5"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" Display="Dynamic" ControlToValidate="txtFirstName" CssClass="help-block" ErrorMessage="Please specify first name." ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="txtGSTNNo">GSTIN</label>
                            <asp:TextBox ID="txtGSTNNumber" class="form-control"  autocomplete="off" placeholder="GSTIN" runat="server" TabIndex="9"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvGSTNNo" runat="server" Display="Dynamic" ControlToValidate="txtGSTNNumber" CssClass="help-block" ErrorMessage="Please specify GSTIN" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revGSTNO" runat="server" Display="Dynamic" ControlToValidate="txtGSTNNumber" CssClass="help-block" ErrorMessage="Please specify GSTIN in proper format" ValidationExpression="^([0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1})$" ValidationGroup="createUser" />
                            <%--<span id="spnError4" style="color: Red; display: none">Only alphanumeric characters are allowed</span>--%>
                        </div>
                        <div class="form-group">
                            <label for="txtEmailID">Email ID</label>
                            <asp:TextBox ID="txtEmailID" class="form-control" autocomplete="off" placeholder="Email Id" runat="server" TabIndex="13"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmailID" CssClass="help-block" ErrorMessage="Please specify email id." ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmailid" runat="server" Display="Dynamic" ErrorMessage="Please specify valid email id" CssClass="help-block" ControlToValidate="txtEmailID" ValidationGroup="createUser" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <%--<span id="spnError5" style="color: Red; display: none">Please specify valid email id</span>--%>
                        </div>
                        <div class="form-group">
                            <label for="txtnamesignatory">Name Of Signatory</label>
                            <asp:TextBox ID="txtnamesignatory" class="form-control" autocomplete="off" placeholder="Name of Signatory" onkeypress="return ValidateAlpha(event);" onpaste="return false;" runat="server" MaxLength="30" TabIndex="17"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSignatory" runat="server" Display="Dynamic" ControlToValidate="txtnamesignatory" CssClass="help-block" ErrorMessage="Please specify Name of Signatory" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                        </div>
                        <%--<div class="form-group">
                    <label>Business Type</label>
                    <asp:ListBox  ID="lbBuisnessType" Class="form-control" SelectionMode="Multiple" DataTextField="Buisness-type" runat="server" TabIndex="17"></asp:ListBox>
                </div>--%>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtPassword">Password</label>
                            <asp:TextBox ID="txtPassword" class="form-control" autocomplete="off" AutoCompleteType="Disabled" placeholder="Password" runat="server" MaxLength="15" TextMode="Password" ViewStateMode="Disabled" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" Display="Dynamic" ControlToValidate="txtPassword" CssClass="help-block" ErrorMessage="Please specify password." ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPassword" Display="Dynamic" runat="server" ErrorMessage="Please specify valid password" CssClass="help-block" ControlToValidate="txtPassword" ValidationGroup="createUser" ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).*$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="txtLastName">Last Name</label>
                            <asp:TextBox ID="txtLastName" class="form-control" autocomplete="off" placeholder="Last Name" onkeypress="return ValidateAlpha(event);" onpaste="return false;" runat="server" MaxLength="15" TabIndex="6"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" Display="Dynamic" ControlToValidate="txtLastName" CssClass="help-block" ErrorMessage="Please specify last name." ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="txtGstId">GSTIN User ID</label>
                            <asp:TextBox ID="txtGstUser" class="form-control" autocomplete="off" placeholder="GSTIN User ID" runat="server" TabIndex="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvGstId" runat="server" ErrorMessage="Please specify the GSTIN Id" Display="Dynamic" ControlToValidate="txtGstUser" CssClass="help-block" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revgstn" runat="server" ControlToValidate="txtGstUser" Display="Dynamic" ValidationExpression="[a-zA-Z0-9%*#@-]*$" CssClass="help-block" ErrorMessage="Please enter alphanumeric and special characters only"></asp:RegularExpressionValidator>
                            <%--<span id="spnError3" style="color: Red; display: none">Please enter alphanumeric and special characters only</span>--%>
                        </div>

                        <div class="form-group">
                            <label for="lblorganization">Name of Organization</label>
                            <asp:TextBox ID="txtOrganizationName" class="form-control" autocomplete="off" placeholder="Name of the Organization" runat="server" TabIndex="14"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvOrganization" runat="server" Display="Dynamic" ControlToValidate="txtOrganizationName" CssClass="help-block" ErrorMessage="Please specify Organization name" ValidationExpression="([^a-zA-Z0-9]|^\s)" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revOrganization" runat="server" ControlToValidate="txtOrganizationName" Display="Dynamic" ValidationExpression="[a-zA-Z0-9%*# @-]*$" CssClass="help-block" ErrorMessage="Only alphanumeric & special characters are allowed"></asp:RegularExpressionValidator>
                            <%--<span id="spnError2" style="color: Red; display: none">Only alphanumeric & special characters are allowed</span>--%>
                        </div>
                        <div class="form-group">
                            <label>Business Type</label>
                            <asp:ListBox ID="lbBuisnessType" Class="form-control" SelectionMode="Multiple" DataTextField="Buisness-type" runat="server" TabIndex="17"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlDesig">Designation</label>
                            <asp:DropDownList ID="ddlDesig" CssClass="form-control" runat="server" Placeholder="Select Designation" TabIndex="3"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDesig" runat="server" ErrorMessage="Please specify the Designation" Display="Dynamic" ControlToValidate="ddlDesig" InitialValue="0" CssClass="help-block" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="txtAddress">Address</label>
                            <asp:TextBox ID="txtCreateAdd" class="form-control" autocomplete="off" placeholder="Address" runat="server" TabIndex="7"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" Display="Dynamic" ErrorMessage="Field cannot be left blank" CssClass="help-block" ControlToValidate="txtCreateAdd" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator ID="revAddress" runat="server" Display="Dynamic" ErrorMessage="Only ,.,/,_,space and alphanumeric characters are allowed.." ValidationExpression="^[A-Za-z0-9 _/.,]+$" CssClass="help-block" ValidationGroup="vgcreateUser" ControlToValidate="txtCreateAdd"></asp:RegularExpressionValidator>
                             </div>
                        <div class="form-group">
                            <label for="txt_ITC">ITC</label>
                            <asp:TextBox ID="txt_ITC" class="form-control" autocomplete="off" placeholder="ITC" onkeypress="return onlyDecNos(event,this);" onpaste="return false;" runat="server" MaxLength="12" TabIndex="11"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvITC" runat="server" Display="Dynamic" ErrorMessage="Field cannot be left blank" CssClass="help-block" ControlToValidate="txt_ITC" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                        </div>
                        <div id="RoleId" visible="false">
                            <div class="form-group">
                                <label>Select Role</label>
                                <asp:DropDownList class="form-control" placeholder="Select User in Role" ID="ddlRolesList" DataTextField="Name" DataValueField="ID" ItemType="BALAJI.GSP.APPLICATION.Infrastructure.ApplicationRole" runat="server" TabIndex="15"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRole" runat="server" ValidationGroup="vgcreateUser" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlRolesList"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>User Photo</label>
                        </div>
                        <%-- <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" ChildrenAsTriggers="true" runat="server">
                    <%--<Triggers>
                          <asp:AsyncPostBackTrigger ControlID = "btnAsyncUpload" EventName = "Click" />
                        </Triggers>--%>
                        <%-- <Triggers>
                        <%-- <asp:AsyncPostBackTrigger ControlID="FiSmallImage" EventName="Click" />  --%>
                        <%--   <asp:PostBackTrigger ControlID="btnCreateUsers" />
                    </Triggers>
                    <ContentTemplate>--%>
                        <asp:FileUpload ID="FiSmallImage" EnableViewState="true" runat="server" />
                        <%-- <asp:Image ID="NormalImage" runat="server" />--%>
                        <%--  <asp:FileUpload ID="FiSmallImage" runat="server" />
                        <asp:Button ID="btnAsyncUpload" runat="server" Text="Async_Upload" OnClick="btnAsyncUpload_Click" />--%>
                        <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txt_Grossturnover">Gross TurnOver</label>
                            <asp:TextBox ID="txt_Grossturnover" Class="form-control" autocomplete="off" onkeypress="return onlyDecNos(event,this);" onpaste="return false;" placeholder="Gross Turn_over" runat="server" TabIndex="4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvGrossTurnover" runat="server" Display="Dynamic" ErrorMessage="Field cannot be left blank" CssClass="help-block" ControlToValidate="txt_Grossturnover" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="txtPhoneNo">Mobile No.</label>
                            <asp:TextBox ID="txtPhoneNumber" class="form-control" autocomplete="off" placeholder="Mobile No." onkeypress="return onlyNos(event,this);" onpaste="return false;" runat="server" MaxLength="10" TabIndex="8"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPhoneNo" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txtPhoneNumber" ErrorMessage="Please specify the Phone No." ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPhone" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtPhoneNumber" ValidationGroup="vgcreateUser" ErrorMessage="Mobile number not valid" ValidationExpression="^[6-9]\d{9}$"></asp:RegularExpressionValidator>
                            <%--   <span id="spnError6" style="color: Red; display: none">Please enter valid phone number</span>--%>
                        </div>
                        <div class="form-group">
                            <label for="txt_statecode">State Name</label>
                            <asp:DropDownList ID="ddlStateCode" CssClass="form-control" runat="server" TabIndex="12"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvStateCode" runat="server" ValidationGroup="vgcreateUser" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlStateCode"></asp:RequiredFieldValidator>
                        </div>
                        <%--amits register with us--%>
                         <div class="form-group">
                            <label>Registered with us</label>
                            <asp:DropDownList AutoPostBack="true" ID="ddlRegistered" Class="form-control" placeholder="Select Option" DataTextField="Registered with us" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvReigster" runat="server" ValidationGroup="vgcreateUser" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlRegistered"></asp:RequiredFieldValidator>
                        </div> 
                        <div class="form-group">
                            <label>User Type</label>
                            <asp:DropDownList ID="ddluser_type" Class="form-control" placeholder="Select User Type" DataTextField="User-type" runat="server" TabIndex="16"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvUsertype" runat="server" ValidationGroup="vgcreateUser" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddluser_type"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-footer">
                <uc1:uc_sucess runat="server" ID="uc_sucess" />
                <asp:Button ID="btnCreateUsers" class="btn btn-primary" ValidationGroup="vgcreateUser" OnClick="btnCreateUsers_Click" runat="server" Text="Submit" TabIndex="17" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>


<script type="text/javascript">
    <%--var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(InitializeRequest);

    function InitializeRequest(sender, args) {
        var updateProgress = $get('UpdateProgress1');
        var postBackElement = args.get_postBackElement();
        if (postBackElement.id == '<%= btnCreateUsers.ClientID %>') {
            updateProgress.control._associatedUpdatePanelId = 'UpdatePanel2';
        }
        else {
            updateProgress.control._associatedUpdatePanelId = null;
        }
    }--%>

</script>

