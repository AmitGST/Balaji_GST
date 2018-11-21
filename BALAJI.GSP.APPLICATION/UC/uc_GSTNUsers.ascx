<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_GSTNUsers.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.uc_GSTNUsers" %>
<div class="col-md-3">
    <asp:DropDownList ID="ddlGSTNUsers" AutoPostBack="true" CssClass="form-control"  runat="server" OnSelectedIndexChanged="ddlGSTNUsers_SelectedIndexChanged"></asp:DropDownList>
</div>
<div class="col-md-2">
    <asp:CheckBox ID="ckbSelfGSTN" AutoPostBack="true" Checked="true" Text="Invoice/Return for Self" runat="server" OnCheckedChanged="ckbSelfGSTN_CheckedChanged"></asp:CheckBox>
</div> 



<div class="example-modal">
        <div class="modal modal-warning">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">×</span></button>
                <h4 class="modal-title">Warning Modal</h4>
              </div>
              <div class="modal-body">
                <p>One fine body…</p>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-outline">Save changes</button>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
      </div>