<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_B2CS.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_B2CS" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Src="~/UC/Offline/Controls/uc_SupplyType_B2CS.ascx" TagPrefix="uc1" TagName="uc_SupplyType_B2CS" %>

<div class="box box-primary">
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lv_B2CS" runat="server" InsertItemPosition="LastItem" OnItemCreated="lv_B2CS_ItemCreated" OnItemInserting="lv_B2CS_ItemInserting" OnItemDataBound="lv_B2CS_ItemDataBound" OnItemEditing="lv_B2CS_ItemEditing" OnItemUpdating="lv_B2CS_ItemUpdating" DataKeyNames="ValueId" OnPagePropertiesChanging="lv_B2CS_PagePropertiesChanging">
            <EmptyDataTemplate>
                <table class="table table-responsive">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkSelect" CssClass="selectone" runat="server" Disabled="disabled" /></td>
                    <td>

                        <asp:DropDownList ID="ddlType" Width="100px" class="form-control input-sm" runat="server" Disabled="disabled"></asp:DropDownList>
                        <asp:HiddenField ID="hdnType" runat="server" Value='<%# Eval("GST_TRN_OFFLINE_INVOICE.Type") %>' />

                    </td>
                    <uc1:uc_SupplyType_B2CS runat="server" ID="uc_SupplyType_B2CS" />
                    <asp:HiddenField ID="hdnCess" runat="server" Value='<% # Eval("CessAmt")%>' />
                    <asp:HiddenField ID="hdnIGST" runat="server" Value='<% # Eval("IGSTAmt")%>' />
                    <asp:HiddenField ID="hdnSGST" runat="server" Value='<% # Eval("SGSTAmt")%>' />
                    <asp:HiddenField ID="hdnCGST" runat="server" Value='<% # Eval("CGSTAmt")%>' />
                    <asp:HiddenField ID="hdnTotalTaxableValue" runat="server" Value='<% # Eval("TotalTaxableValue")%>' />
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("GST_TRN_OFFLINE_INVOICE.PlaceOfSupply") %>' />
                    <asp:HiddenField ID="Hdn_SupplyType" runat="server" Value='<%# Eval("GST_TRN_OFFLINE_INVOICE.SupplyType") %>' />
                    <asp:HiddenField ID="hdnRate" runat="server" Value='<%# Eval("RateId") %>' />
                    <td>
                        <asp:TextBox ID="txtECommerce" class="form-control input-sm" Width="140px" Disabled="disabled" Text='<%# Eval("GST_TRN_OFFLINE_INVOICE.ECommerce_GSTIN") %>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary btn-xs" CommandName="Edit" data-trigger="hover" data-placement="right" title="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="selectone" /></td>
                    <td>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlType" Width="100px" class="form-control input-sm" runat="server"></asp:DropDownList>
                            <asp:HiddenField ID="hdnType" runat="server" Value='<%# Eval("GST_TRN_OFFLINE_INVOICE.Type") %>' />
                            <asp:RequiredFieldValidator ID="rfvType" runat="server" Display="Dynamic" ControlToValidate="ddlType" InitialValue="-1" CausesValidation="true" CssClass="help-block" ErrorMessage="Please specify Type" ValidationGroup="vgB2CSEdit"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <uc1:uc_SupplyType_B2CS runat="server" ID="uc_SupplyType_B2CS" />
                    <asp:HiddenField ID="hdnCess" runat="server" Value='<% # Eval("CessAmt")%>' />
                    <asp:HiddenField ID="hdnIGST" runat="server" Value='<% # Eval("IGSTAmt")%>' />
                    <asp:HiddenField ID="hdnSGST" runat="server" Value='<% # Eval("SGSTAmt")%>' />
                    <asp:HiddenField ID="hdnCGST" runat="server" Value='<% # Eval("CGSTAmt")%>' />
                    <asp:HiddenField ID="hdnTotalTaxableValue" runat="server" Value='<% # Eval("TotalTaxableValue")%>' />
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("GST_TRN_OFFLINE_INVOICE.PlaceOfSupply") %>' />
                    <asp:HiddenField ID="Hdn_SupplyType" runat="server" Value='<%# Eval("GST_TRN_OFFLINE_INVOICE.SupplyType") %>' />
                    <asp:HiddenField ID="hdnRate" runat="server" Value='<%# Eval("RateId") %>' />
                    <td>
                        <asp:TextBox ID="txtECommerce" class="form-control input-sm" autocomplete="off" Width="140px" Text='<%# Eval("GST_TRN_OFFLINE_INVOICE.ECommerce_GSTIN") %>' Disabled="disabled" runat="server"></asp:TextBox></td>
                    <asp:RegularExpressionValidator ID="revECommerce" runat="server" Display="Dynamic" ControlToValidate="txtECommerce" CssClass="help-block" ErrorMessage="Invalid format" ValidationExpression="^([0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1})$" ValidationGroup="vgEdit"></asp:RegularExpressionValidator>
                    <td>
                        <asp:UpdatePanel ID="UPanel" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkbUpdate" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:LinkButton ID="lkbUpdate" CssClass="btn btn-success btn-xs" CommandArgument='<%# Eval("OfflineDataID") %>' ValidationGroup="vgEdit" runat="server" ToolTip="Update" CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                    </td>
                </tr>
            </EditItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <td></td>

                    <td>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlType" Width="100px" class="form-control input-sm" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvType" runat="server" Display="Dynamic" CausesValidation="true" ControlToValidate="ddlType" InitialValue="-1" CssClass="help-block" ErrorMessage="Please specify Type" ValidationGroup="vgB2CSInsert"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <uc1:uc_SupplyType_B2CS runat="server" ID="uc_SupplyType_B2CS" />
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtECommerce" class="form-control input-sm" autocomplete="off" Width="140px" Text='<%# Eval("GST_TRN_OFFLINE_INVOICE.ECommerece_GSTIN") %>' runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revEcommerce" runat="server" Display="Dynamic" ControlToValidate="txtECommerce" CssClass="help-block" ErrorMessage="Invalid GSTIN" ValidationExpression="^([0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1})$" ValidationGroup="vgB2CSInsert"></asp:RegularExpressionValidator>
                        </div>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UPanel2" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkbInsert" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:LinkButton ID="lkbInsert" CssClass="btn btn-success btn-xs" ValidationGroup="vgB2CSInsert" runat="server" CommandName="Insert"><i class="fa fa-save"></i></asp:LinkButton>
                    </td>
                </tr>
            </InsertItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th>
                                <asp:CheckBox ID="Chk1" CssClass="selectall" runat="server" /></th>
                            <th>Type</th>
                            <th>Place of Supply</th>
                            <th>Total Taxable Value &nbsp;<i class="fa fa-rupee"></i></th>
                            <th>Supply Type</th>
                            <th>Rate</th>
                            <th>IGST</th>
                            <th>CGST</th>
                            <th>SGST/UTGST</th>
                            <th>CESS</th>
                            <th>E-Commerce GSTIN</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server" />
                    </tbody>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </div>
    <div class="box-footer">
        <asp:LinkButton ID="lkbDelete" OnClick="lkbDelete_Click" CssClass="btn btn-danger" runat="server"><i class="fa fa-trash"></i>&nbsp;Delete</asp:LinkButton>
        <div class="pagination pagination-sm no-margin pull-right">
            <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
            <asp:DataPager ID="dpB2CS" runat="server" PagedControlID="lv_B2CS" PageSize="10" class="btn-group-sm pager-buttons">
                <Fields>
                    <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                    <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                    <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                </Fields>
            </asp:DataPager>
        </div>
        <uc1:uc_sucess runat="server" ID="uc_sucess" />
    </div>

</div>
<div class="modal modal-warning fade" id="viewInvoiceModelWarningMessage" role="dialog" aria-labelledby="viewInvoiceModelWarningMessage" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                    &times;
                </button>
                <h4 class="modal-title">
                    <i class="fa fa-exclamation-triangle"></i>
                    <asp:Label ID="Label1" runat="server" Text="Warning Message"></asp:Label>
                </h4>
            </div>
            <div class="modal-body">
                <p>
                    <asp:Label ID="lblWarning" runat="server"></asp:Label>
                </p>
            </div>

            <div class="modal-footer">
                <button class="btn btn-outline pull-right" data-dismiss="modal" aria-hidden="true">
                    Close
                </button>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function Showalert() {
        alert('Call JavaScript function from codebehind');
    }
</script>
<%--<script type="text/javascript">
    function Validate() {
        var isValid = false;
        isValid = Page_ClientValidate('vgB2CSEdit');
        if (isValid) {
            isValid = Page_ClientValidate('vgB2CSInsert');
        }
        return isValid;
    }
    </script>--%>
<%--<script type="text/javascript">
     $(document).ready(function () {
         $("#lv_B2CS").validate({
             invalidHandler: addRules
         });

         addRules();

         $("#lkbUpdate").click(function () {
             $(".group2").each(function () {
                 $(this).rules("remove");
             });
         });
         $("lkbInsert").click(function () {
             $(".group1").each(function () {
                 $(this).rules("remove");
             });
         });

     });
     var addRules = function () {
         $("#input1").rules("add", { required: true });
         $("#input2").rules("add", { required: true });
     }
  
   //function validatePage() 
   //    {  
   //    var flag = Page_ClientValidate('vgB2CSEdit');
   //    alert("Validation Group1");
   //          if (flag) 
   //                //Executes all the validation controls associated with group1 validaiton Group2. 
   //              flag = Page_ClientValidate('vgB2CSInsert');

   //          alert("ValidationGroup2");
   //         //if (flag) 
   //         //     //Executes all the validation controls associated with group1 validaiton Group3. 
   //         //    flag = Page_ClientValidate('vgB2CSEdit');
   //           //if (flag) 
   //           //     //Executes all the validation controls which are not associated with any validation group. 
   //           //  flag = Page_ClientValidate(); 
   //           return flag; 
   //        } 
        </script>--%>
<%--<script type="text/javascript">
      $(function () {
          $('[id$=TextBox1]').datepicker({
              dateFormat: "dd/MM/yy",
              showOn: "button",
              buttonImage: "images/DateTime/Calendar.png",
              buttonImageOnly: true,
              changeMonth: true,
              changeYear: true
          });
      });
</script>--%>
