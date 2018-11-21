<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Maintheme.master" AutoEventWireup="true" CodeBehind="Registration1.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.Registration1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main1Content" runat="server">

    <div class="content">
        <div id="content" class="site-content">
            <div id="main" class="clearfix no-sidebar">
                <div class="tg-container">
                    <div id="primary">
                        <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                            <asp:View ID="ViewVerify" runat="server">
                                <h3 id="reply-title" class="comment-reply-title">Register <small>User Enrollment Verification</small></h3>
                                <hr />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>PAN of Organization</label>
                                                <asp:TextBox ID="txtPan" CssClass="form-control" placeholder="Enter PAN" autocomplete="off" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Aadhar No. of Authorized Signatory</label>
                                                <asp:TextBox ID="txtAadhar" CssClass="form-control" placeholder="Enter Aadhar No." autocomplete="off" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label>OTP</label>
                                            <asp:TextBox ID="txtOtp" CssClass="form-control" autocomplete="off" placeholder="Enter OTP" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            <label></label>
                                            <br />
                                            <asp:Button ID="btnOTP" CssClass="btn btn-success" Text="Get OTP" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="col-md-1">
                                                <asp:Button ID="btnReset" CssClass="btn btn-danger" Text="Reset" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button ID="btnNext" CssClass="btn btn-success" OnClick="btnNext_Click" Text="Next" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="ViewSignatory" runat="server">
                                <h3 id="reply-title1" class="comment-reply-title">Register <small>Registration of Signatory</small></h3>
                                <hr />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Login Id</label>
                                                <asp:TextBox ID="txtLogin" CssClass="form-control" placeholder="Enter Login Id" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Password</label>
                                                <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Enter Password" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>First Name</label>
                                                <asp:TextBox ID="txtFirst" CssClass="form-control" placeholder="Enter First Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Last Name</label>
                                                <asp:TextBox ID="txtLast" CssClass="form-control" placeholder="Enter Last Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Designation</label>
                                                <asp:DropDownList ID="ddlDesig" CssClass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Mobile No.</label>
                                                <asp:TextBox ID="txtMobile" CssClass="form-control" placeholder="Enter Mobile No." runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Address 1</label>
                                                <asp:TextBox ID="txtAdd1" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Address 2</label>
                                                <asp:TextBox ID="txtAdd2" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Email Id</label>
                                                <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Enter Email Id" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Role</label>
                                                <asp:DropDownList ID="ddlRole" CssClass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="col-md-1">
                                                <asp:Button ID="btnSigBack" CssClass="btn btn-danger" Text="Back" OnClick="btnSigBack_Click" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button ID="btnSigNext" CssClass="btn btn-success" Text="Next" OnClick="btnSigNext_Click" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </asp:View>
                            <asp:View ID="ViewOrganization" runat="server">
                                <h3 id="reply-title2" class="comment-reply-title">Register <small>Organization Details</small></h3>
                                <hr />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Name of Organization</label>
                                                <asp:TextBox ID="txtOrganization" CssClass="form-control" placeholder="Enter Organization Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Domain of Business</label>
                                                <asp:TextBox ID="txtBusiness" CssClass="form-control" placeholder="Enter Business Domain" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>User Type</label>
                                                <asp:DropDownList ID="ddlUserType" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>GSTIN</label>
                                                <asp:TextBox ID="txtGstin" CssClass="form-control" placeholder="Enter GSTIN" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>State</label>
                                                <asp:TextBox ID="txtState" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <label>GSTIN User Name</label>
                                            <asp:TextBox ID="txtGstinName" CssClass="form-control" placeholder="Enter GSTIN User Name" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Address 1</label>
                                                <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Address 2</label>
                                                <asp:TextBox ID="txtAddress1" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Address 3</label>
                                                <asp:TextBox ID="txtAdd3" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>City/ District</label>
                                                <asp:TextBox ID="txtCity" CssClass="form-control" placeholder="Enter City/ District" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Pincode</label>
                                                <asp:TextBox ID="txtPincode" CssClass="form-control" placeholder="Enter Pincode" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <div class="col-md-1">
                                                <asp:Button ID="btnBackOrg" CssClass="btn btn-danger" Text="Back" OnClick="btnBackOrg_Click" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button ID="btnPreview" CssClass="btn btn-success" Text="Preview" OnClick="btnPreview_Click" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="ViewPreview" runat="server">
                                <h3 id="reply-title3" class="comment-reply-title">Preview <small>Registration Details</small></h3>
                                <hr />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>PAN of Organization:</strong></label>
                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>Aadhar No. of Authorized Signatory:</strong></label>
                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <h3 id="reply-title4" class="comment-reply-title"><small>Signatory Details:</small></h3>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>Login Id:</strong></label>
                                                <asp:Label ID="Label3" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>First Name:</strong></label>
                                                <asp:Label ID="Label4" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>Last Name:</strong></label>
                                                <asp:Label ID="Label5" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>Designation:</strong></label>
                                                <asp:Label ID="Label6" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>Mobile No:</strong></label>
                                                <asp:Label ID="Label7" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>Address:</strong></label>
                                                <asp:Label ID="Label8" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>Email Id:</strong></label>
                                                <asp:Label ID="Label9" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>Role:</strong></label>
                                                <asp:Label ID="Label10" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <h3 id="reply-title5" class="comment-reply-title"><small>Organization Details</small></h3>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>Name of Organization:</strong></label>
                                                <asp:Label ID="Label11" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>Domain of Business:</strong></label>
                                                <asp:Label ID="Label12" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>User Type:</strong></label>
                                                <asp:Label ID="Label13" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>GSTIN:</strong></label>
                                                <asp:Label ID="Label14" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>State:</strong></label>
                                                <asp:Label ID="Label15" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>GSTIN User Name:</strong></label>
                                                <asp:Label ID="Label16" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>Address:</strong></label>
                                                <asp:Label ID="Label17" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>City/ District:</strong></label>
                                                <asp:Label ID="Label18" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label><strong>Pincode:</strong></label>
                                                <asp:Label ID="Label19" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <div class="col-md-1">
                                                <asp:Button ID="btnBackPreview" CssClass="btn btn-danger" Text="Back" OnClick="btnBackPreview_Click" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button ID="btnSubmit" CssClass="btn btn-success" Text="Submit" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
