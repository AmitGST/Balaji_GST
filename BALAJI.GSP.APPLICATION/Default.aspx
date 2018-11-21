<%@ Page Title="Home Page" Async="true" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BALAJI.GSP.APPLICATION._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <ajaxToolkit:AsyncFileUpload width="400px" ID="AsyncFileUpload1" throbberid="myThrobber"  maximumnumberoffiles="1" OnUploadedComplete="AsyncFileUpload1_UploadedComplete" runat="server" />
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   
  
</asp:Content>
