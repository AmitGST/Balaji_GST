<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_sucess.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.uc_sucess" %>


<div class="text-red">
    <asp:PlaceHolder runat="server" ID="errorMessage" Visible="<%# VisibleError %>" ViewStateMode="Disabled">
        <%: ErrorMessage%>
    </asp:PlaceHolder>
</div>
<div class="text-green">
    <asp:PlaceHolder runat="server" ID="successMessage" Visible="<%# Visible %>" ViewStateMode="Disabled">
        <%: SuccessMessage %>
    </asp:PlaceHolder>
</div>


<%--<asp:PlaceHolder runat="server" ID="successMessage" Visible="<%# Visible %>" ViewStateMode="Disabled">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
        <h4><i class="icon fa fa-check"></i>Message!</h4>
        <%: SuccessMessage %>.
    </asp:PlaceHolder>--%>

<%--<asp:PlaceHolder runat="server" ID="errorMessage" Visible="<%# Visible %>" ViewStateMode="Disabled">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
        <h4><i class="icon fa fa-check"></i>Message!</h4>
        <%: SuccessMessage %>.
    </asp:PlaceHolder>--%>

<%-- <div class="alert alert-success alert-dismissible">
        <a href="#" class="close" data-dismiss="alert" aria-label="close" aria-hidden="true" title="close">×</a>
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
            <h4><i class="icon fa fa-check"></i>Alert!</h4>
            <%: SuccessMessage %>
        
    </div>--%>
    
        
         
   
