<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_OTP.ascx.cs" Inherits="UserInterface.UserControl.OTP.uc_OTP" %>
<div runat="server" id="divOTP" visible="false">
<div class="col-lg-2"></div>
<div class="col-lg-2"></div>
<div class="col-lg-2"></div>
<div class="col-lg-3"></div>
<div class="col-lg-1">
    <asp:TextBox ID="txtOTP" Width="90px" CssClass="form-control" MaxLength="8" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
   
        <div class="text-success">
            <small> <asp:Literal ID="litOtpMessage" runat="server"></asp:Literal> </small>
        </div>
   
</div>
<div class="col-lg-2" style="text-align: left;padding-left:20px;visibility:hidden;">
    <asp:Button ID="btnSubmitOTP" runat="server" CssClass="btn btn-primary" Text="Resend" OnClick="btnSubmitOTP_Click" />
</div>
    </div>
