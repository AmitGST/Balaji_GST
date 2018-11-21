<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="ItextSharp.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.Trans.ItextSharp" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <div>
      This Message by Ankita<br />
    </div>
    <div style = "font-family:Arial">This is a test page</div>
    <div>
       <table border = "1" Width = "100">
          <tr><td>Name</td><td>Age</td></tr>
          <tr><td>John</td><td>11</td></tr>
          <tr><td>Sam</td><td>13</td></tr>
          <tr><td>Tony</td><td>12</td></tr>
       </table>
    </div>
   
       <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
  
  
</asp:Content>
