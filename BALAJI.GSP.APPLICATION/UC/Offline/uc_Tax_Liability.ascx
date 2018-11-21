<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_Tax_Liability.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_Tax_Liability" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Src="~/UC/Offline/Controls/uc_SupplyType.ascx" TagPrefix="uc1" TagName="uc_SupplyType" %>



<div class="box box-primary">
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lv_Tax_Liability" runat="server" InsertItemPosition="LastItem" OnItemCreated="lv_Tax_Liability_ItemCreated" OnItemInserting="lv_Tax_Liability_ItemInserting" OnItemDataBound="lv_Tax_Liability_ItemDataBound" OnItemEditing="lv_Tax_Liability_ItemEditing" OnPagePropertiesChanging="lv_Tax_Liability_PagePropertiesChanging" DataKeyNames="ValueId">
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
                        <asp:CheckBox ID="chkSelect" CssClass="selectone" runat="server" />
                    </td>
                    <uc1:uc_SupplyType runat="server" ID="uc_SupplyType" />
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <asp:HiddenField ID="Hdn_SupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />
                    <td>
                        <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-primary btn-xs pull-right" CommandName="Edit" data-trigger="hover" data-placement="right" title="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkSelect" CssClass="selectone" disabled runat="server" />
                    </td>
                    <uc1:uc_SupplyType runat="server" ID="uc_SupplyType" />
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <asp:HiddenField ID="Hdn_SupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />
                    <td>
                        <asp:LinkButton ID="btnAddMore" runat="server" OnClick="btnAddMore_Click" CommandArgument='<%# Eval("ValueId") %>' CssClass="btn btn-primary btn-xs pull-right" data-trigger="hover" title="Add Items"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                    </td>
                </tr>
            </EditItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <td></td>
                    <uc1:uc_SupplyType runat="server" ID="uc_SupplyType" />
                    <td>
                        <asp:LinkButton ID="lkbInsert" runat="server" CssClass="btn btn-primary btn-xs pull-right" CommandName="Insert" data-trigger="hover" title="Add Items"><i class="fa fa-plus-circle"></i></asp:LinkButton></td>
                </tr>
            </InsertItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th style="width:5%;">
                                <asp:CheckBox ID="Chk1" CssClass="selectall" runat="server" /></th>
                            <th style="align-content: center; width:45%">Place of Supply</th>
                            <th style="width:45%">Supply Type</th>
                            <th  style="text-align: right; width:5%">Actions</th>
                        </tr>

                    </thead>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server" />
                    </tbody>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </div>
    <div class="box-footer clearfix">
        <asp:LinkButton ID="lkbDelete" CssClass="btn btn-danger" OnClick="lkbDelete_Click" runat="server"><i class="fa fa-trash"></i>&nbsp;Delete</asp:LinkButton>
        <div class="pagination pagination-sm no-margin pull-right">
            <asp:DataPager ID="dpTaxLiability" runat="server" PagedControlID="lv_Tax_Liability" PageSize="10" class="btn-group-sm pager-buttons">
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
                            </div> <div class="modal-body">
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