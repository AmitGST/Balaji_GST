<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_modelUser.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.uc_modelUser" %>
<asp:ListView ID="lvSignatory" runat="server">
    <EmptyDataTemplate>
        <table class="table table-responsive ">
            <tr>
                <td>No data was returned.</td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <ItemTemplate>
        <tr>
            <td><%# Container.DataItemIndex + 1%></td>
           
            <td><%# Eval("SignatoryName") %> </td>
            <td><%#(Eval("GSTNNo")) %> </td>
            <td><%# Eval("OrgAddress") %> </td>
            <td><%# UserStateName(Eval("State").ToString()) %>  </td>
            <td><%# Eval("Pincode") %> </td>
            <td><%# Eval("Email") %> </td>
            <td><%# Eval("PhoneNumber") %> </td>
          
        </tr>
    </ItemTemplate>

    <LayoutTemplate>
        <table class="table table-responsive no-padding table-bordered">

            <tr>
                <th>#</th>
               
                <th>Signatory Name</th>
                <th>GSTIN</th>
                <th> Address</th>
                <th>State</th>
                <th>Pin Code</th>
                <th>Email</th>
                <th>Ph. Number</th>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
            </tr>
        </table>
    </LayoutTemplate>
</asp:ListView>
