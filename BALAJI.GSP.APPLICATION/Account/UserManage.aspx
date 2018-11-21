<%@ Page Title="User manage" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.frm_UserManage" %>


<%@ Register Src="~/Account/uc_CreateGroup.ascx" TagPrefix="uc1" TagName="uc_CreateGroup" %>
<%@ Register Src="~/Account/uc_Groups.ascx" TagPrefix="uc1" TagName="uc_Groups" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   
   
    <%--<asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="BALAJI.GSP.APPLICATION.Infrastructure.ApplicationUserManager" EntityTypeName="" OrderBy="UserName" Select="new (FirstName, LastName, GSTNNo, Email, EmailConfirmed, LockoutEnabled, UserName)" TableName="Users">
    </asp:LinqDataSource>
  <asp:DataPager ID="DataPager1" runat="server"></asp:DataPager>--%> <uc1:uc_sucess runat="server" ID="uc_sucess" />
   <%--<asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="BALAJI.GSP.APPLICATION.Infrastructure.ApplicationUserManager" EntityTypeName="" OrderBy="UserName" Select="new (FirstName, LastName, GSTNNo, Email, EmailConfirmed, LockoutEnabled, UserName)" TableName="Users">
    </asp:LinqDataSource>
  <asp:DataPager ID="DataPager1" runat="server"></asp:DataPager>--%>
   
</asp:Content>
