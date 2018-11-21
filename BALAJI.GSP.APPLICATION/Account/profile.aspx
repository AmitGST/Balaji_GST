<%@ Page Title="User Profile" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.profile" %>

<%@ Register Src="~/Account/uc_ProfileUpdate.ascx" TagPrefix="uc1" TagName="uc_ProfileUpdate" %>
<%@ Register Src="~/Account/uc_ChangePassword.ascx" TagPrefix="uc1" TagName="uc_ChangePassword" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:uc_ProfileUpdate runat="server" ID="uc_ProfileUpdate" />
    <%--<uc1:uc_ChangePassword runat="server" ID="uc_ChangePassword" />--%>
</asp:Content>
