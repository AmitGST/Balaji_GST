<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_ModalReport.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.uc_ModalReport" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<div class="modal-body" style="max-height: calc(100vh - 280px); overflow-y: auto;">

    <asp:HiddenField runat="server" ID="hdnReportId" />
    <div class="box-body table no-padding">
    <asp:ListView ID="lvUsers" runat="server" DataKeyNames="Id">
        <EmptyDataTemplate>
            <table class="table table-responsive">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Container.DataItemIndex + 1%></td>
                <td><%#(Eval("GSTNNo")) %> </td>
                <td><asp:CheckBox ID="chkReport" runat="server" /></td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
           <table class="table table-responsive dataTable" id="lvItems">
               <thead>
                   <tr>
                        <th>#</th>
                        <th>GSTIN</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
            </table>
        </LayoutTemplate>
    </asp:ListView>
        </div>

</div>

<div class="modal-footer" style="padding: 10px;">
    <uc1:uc_sucess runat="server" ID="uc_sucess" />
    <asp:LinkButton ID="btnSubmit" CssClass="btn btn-info"  runat="server" OnClick="btnSubmit_Click" OnClientClick="$('.modal-backdrop').remove();">Submit</asp:LinkButton>
</div>
<script>
    $("#form1").submit(function () {
        $("#WaitDialog").modalDialog();
    });
</script>