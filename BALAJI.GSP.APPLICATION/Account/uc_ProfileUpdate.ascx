<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_ProfileUpdate.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.uc_ProfileUpdate" %>
<%@ Register Src="~/Account/uc_ChangePassword.ascx" TagPrefix="uc1" TagName="uc_ChangePassword" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>


<div class="content-header">
    <h1>User Profile
    </h1>
    <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
        <%--<li><a href="#">Create Users</a></li>--%>
        <li class="active">User profile</li>
    </ol>
</div>
<div class="content">
    <div class="row">
        <%--<div class="col-md-3">
            <div class="box box-primary">
                <div class="box-body box-profile">
                    <div class="content">
                        <div class="row">
                            <div class="box-body box-profile">
                                <asp:Image ID="imgUser" ImageUrl="~/dist/img/avatar5.png" alt="User profile picture" class="profile-user-img img-responsive img-circle" runat="server" />
                                <h3 class="profile-username text-center">Test User</h3>
                                <%--<p class="text-muted text-center">Software Engineer</p>
                                 <a href="#" class="btn btn-primary btn-block"><b>Upload Image</b></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <%--amits--%>
        <%-- <div class="col-md-3">
          <!-- About Me Box -->
          <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">About Me</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <strong><i class="fa fa-book margin-r-5"></i> Education</strong>

              <p class="text-muted">
                B.S. in Computer Science from the University of Tennessee at Knoxville
              </p>

              <hr>

              <strong><i class="fa fa-map-marker margin-r-5"></i> Location</strong>

              <p class="text-muted">Malibu, California</p>

              <hr>

              <strong><i class="fa fa-pencil margin-r-5"></i> Skills</strong>

              <p>
                <span class="label label-danger">UI Design</span>
                <span class="label label-success">Coding</span>
                <span class="label label-info">Javascript</span>
                <span class="label label-warning">PHP</span>
                <span class="label label-primary">Node.js</span>
              </p>

              <hr>

              <strong><i class="fa fa-file-text-o margin-r-5"></i> Notes</strong>

              <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam fermentum enim neque.</p>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>--%>
        <!-- /.col -->
        <div class="col-md-12">
            <div id="myTab" class="nav-tabs-custom">
                <ul class="nav nav-tabs">
                    <li class="active">
                        <a href="#CurrentTurnover" data-toggle="tab">Current Turnover</a></li>
                    <li>
                        <a href="#UserProfile" data-toggle="tab">User Profile</a></li>
                    <li>
                        <a href="#Changepassword" data-toggle="tab">Change Password</a></li>
                </ul>


                <div class="tab-content">
                    <div class="tab-pane active" id="CurrentTurnover">
                        <div class="box-body table no-padding">
                            <asp:ListView ID="lv_CurrentTurnover" runat="server">
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
                                        <td><%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceNo")%></td>
                                        <td><%#DateTimeAgo.GetFormatDate(DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceDate"))%></td>
                                        <td><%#Convert.ToInt32(Eval("Month")).GetMonthName() %></td>
                                        <td><%# Eval("InvoiceAmountWithTax") %></td>
                                        <td><%# Eval("InvoiceAmountWithoutTax") %></td>
                                        <td><%# DateTimeAgo.GetFormatDate(Eval("CreatedDate").ToString()) %> </td>

                                        <%--<td><%# Eval("Status") %></td>--%>
                                    </tr>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <table class="table table-responsive dataTable" id="lvItems">
                                        <thead>
                                            <tr>
                                                <th style="width: 5%">#</th>
                                                <th style="width: 15%">Invoice</th>
                                                <th style="width: 15%">Date</th>
                                                <th style="width: 15%">Month</th>
                                                <th style="width: 20%">Total Amt</th>
                                                <th style="width: 15%">Taxable Amt</th>
                                                <th style="width: 15%">Created Date</th>
                                                <%--<th style="width: 15%">Update Date</th>
                                            <th style="width: 10%">Status</th>--%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                            </asp:ListView>
                            <%--<div class="box-footer clearfix">
                                <div class="pagination pagination-sm no-margin">
                                     <asp:DataPager ID="DataPager1" runat="server" PagedControlID="LVCurrentTurnover" PageSize="20" class="btn-group-sm pager-buttons">
                                            <Fields>
                                                <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                                <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                                                <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                            </Fields>
                                        </asp:DataPager>
                                </div>
                            </div>--%>
                        </div>

                    </div>

                    <div class="tab-pane" id="UserProfile">
                        <div class="box-body">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtGSTNNo" class="control-label">GSTIN</label>
                                    <asp:TextBox ID="txtGSTNNo" class="form-control" MaxLength="15" runat="server" disabled></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvGSTNNo" runat="server" Display="Dynamic" ControlToValidate="txtGSTNNo" CssClass="help-block" ErrorMessage="Please specify GSTIN" ValidationGroup="profileupdate"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revGSTNO" runat="server" Display="Dynamic" ControlToValidate="txtGSTNNo" CssClass="help-block" ErrorMessage="Invalid GSTIN No..!!" ValidationExpression="^([0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1})$" ValidationGroup="profileupdate" />--%>
                                </div>
                                <div class="form-group">
                                    <label for="txtUsername" class="control-label">User Name</label>
                                    <asp:Label ID="lblUserName" runat="server" class="form-control" Text="Label" MaxLength="15" Display="Dynamic"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label for="txtGSTNid" class="control-label">GSTIN User ID</label>
                                    <asp:TextBox ID="txtGSTNid" class="form-control" runat="server" MaxLength="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvGSTNid" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txtGSTNid" ErrorMessage="Please specify GSTN Id" ValidationGroup="profileupdate"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txtFirstName" class="control-label">First Name</label>
                                    <asp:TextBox ID="txtFirstName" class="form-control" runat="server" disabled></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFirstname" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txtFirstName" ErrorMessage="Please specify first name" ValidationGroup="profileupdate"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="revFirsName" runat="server" Display="Dynamic" ControlToValidate="txtFirstName" CssClass="field-validation-error" ErrorMessage="Minimum first name length is 8 character" ValidationExpression=".{8}.*" ValidationGroup="createUser" />--%>
                                </div>
                                <div class="form-group">
                                    <label for="txtLastName" class="control-label">Last Name</label>
                                    <asp:TextBox ID="txtLastName" class="form-control" runat="server" disabled></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLastname" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txtLastName" ErrorMessage="Please specify last name" ValidationGroup="profileupdate"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="revLastName" runat="server" Display="Dynamic" ControlToValidate="txtLastName" CssClass="field-validation-error" ErrorMessage="Minimum last name length is 8 character" ValidationExpression=".{8}.*" ValidationGroup="createUser" />--%>
                                </div>
                                <div style="display: none" class="form-group">
                                    <label for="Txt_ITC" class="control-label">ITC</label>
                                    <asp:TextBox ID="Txt_ITC" class="form-control" Visible="false" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvITC" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="Txt_ITC" ErrorMessage="Please specify the ITC" ValidationGroup="profileupdate"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                            <label>User Photo</label>
                            <asp:UpdatePanel ID="udpimg" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUpdateProfile" />
                                </Triggers>
                                <ContentTemplate>
                                    <%--   <input type="file" id="myFile" class="dropify" data-height="300" />--%>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                                <div class="form-group">
                                    <label for="Txt_GrossTurnover" class="control-label">Gross Turnover</label>
                                    <asp:TextBox ID="Txt_GrossTurnover" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtEmailID" class="control-label">Email-ID</label>
                                    <asp:TextBox ID="txtEmailID" class="form-control" runat="server" disabled></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmailid" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txtEmailID" ErrorMessage="Please specify email id" ValidationGroup="profileupdate"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txtDesignation" class="control-label">Designation</label>
                                    <asp:TextBox ID="txtDesignation" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txtDesignation" ErrorMessage="Please specify the Designation" ValidationGroup="profileupdate"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txtPhoneno" class="control-label">Mobile No.</label>
                                    <asp:TextBox ID="txtPhoneno" class="form-control" runat="server" onkeypress="return onlyNos(event,this);" onpaste="return false;" MaxLength="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPhoneNo" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txtPhoneno" ErrorMessage="Please specify the Phone No." ValidationGroup="profileupdate"></asp:RequiredFieldValidator>
                                </div>
                                <%--<div class="form-group">
                                    <label for="txtSigName" class="control-label">Name of Signatory</label>
                                    <asp:TextBox ID="txtSigName" class="form-control" runat="server" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSigName" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txtSigName" ErrorMessage="Please specify Signatory Name" ValidationGroup="profileupdate"></asp:RequiredFieldValidator>
                                </div>--%>
                                <div class="form-group">
                                    <label for="txtOrganization" class="control-label">Name Of Organization</label>
                                    <asp:TextBox ID="txtOrganization" class="form-control" runat="server" disabled></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvOrganization" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txtOrganization" ErrorMessage="Please specify Organization Name" ValidationGroup="profileupdate"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txt_address" class="control-label">Address</label>
                                    <asp:TextBox ID="txt_address" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txt_address" ErrorMessage="Please specify Address" ValidationGroup="profileupdate"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="ddl_StateCode" class="control-label" disabled>State Name</label>
                                    <asp:DropDownList ID="ddl_StateCode" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <%--  <asp:TextBox ID="txt_StateCode" class="form-control" runat="server" MaxLength="15"></asp:TextBox>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:CheckBox ID="chkConfirmed" class="checkbox" runat="server" Enabled="false"></asp:CheckBox>
                                    <b>Email Confirmed</b>
                                    <uc1:uc_sucess runat="server" ID="uc_sucess" />
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="col-md-12">
                                <uc1:uc_sucess runat="server" ID="uc_sucess1" />
                                <asp:Button ID="btnUpdateProfile" class="btn btn-primary" OnClick="btnUpdateProfile_Click" runat="server" Text="Update Profile" ValidationGroup="profileupdate" />
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="Changepassword">
                        <div class="form-horizontal">
                            <uc1:uc_ChangePassword runat="server" ID="uc_ChangePassword" />
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfTab" runat="server" />
            </div>
            <!-- /.nav-tabs-custom -->
        </div>
        <!-- /.col -->
    </div>
</div>

<%--<script type="text/javascript">
     $(document).ready(function () {
         $('#lvItems').DataTable();
     });
    </script>--%>








