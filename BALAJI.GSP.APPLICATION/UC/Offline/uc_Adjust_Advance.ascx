<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_Adjust_Advance.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_Adjust_Advance" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Src="~/UC/Offline/Controls/uc_SupplyType.ascx" TagPrefix="uc1" TagName="uc_SupplyType" %>



<div class="box box-primary">
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lv_Adjust_Advances" runat="server" InsertItemPosition="LastItem" OnItemCreated="lv_Adjust_Advances_ItemCreated" DataKeyNames="ValueId" OnItemInserting="lv_Adjust_Advances_ItemInserting" OnItemDataBound="lv_Adjust_Advances_ItemDataBound" OnItemEditing="lv_Adjust_Advances_ItemEditing" OnPagePropertiesChanging="lv_Adjust_Advances_PagePropertiesChanging">
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
                    <uc1:uc_SupplyType runat="server" id="uc_SupplyType" />
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <asp:HiddenField ID="HdnSupply" runat="server" Value='<%# Eval("SupplyType") %>' />
                    <td>
                        <asp:LinkButton ID="lkbEdit" CommandName="Edit" runat="server" CssClass="btn btn-primary btn-xs pull-right" data-trigger="hover" data-placement="right"><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkSelect" CssClass="selectone" runat="server" />
                    </td>
                    <uc1:uc_SupplyType runat="server" id="uc_SupplyType" />
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <asp:HiddenField ID="HdnSupply" runat="server" Value='<%# Eval("SupplyType") %>' />
                    <td>
                        <asp:LinkButton ID="btnAddMore" OnClick="btnAddMore_Click" runat="server" CommandArgument='<%# Eval("ValueId") %>' CssClass="btn btn-primary btn-xs pull-right" data-trigger="hover" data-placement="right" title="Add More Line Items"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                    </td>
                </tr>
            </EditItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <td></td>
                    <uc1:uc_SupplyType runat="server" id="uc_SupplyType" />

                    <td>
                        <asp:LinkButton ID="lkbInsert" runat="server" CommandName="Insert" CssClass="btn btn-primary btn-xs pull-right"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                    </td>
                </tr>
            </InsertItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive ">
                    <thead>
                        <tr>
                            <th style="Width:5%">
                                <asp:CheckBox ID="Chk1" CssClass="selectall" runat="server" /></th>
                            <th style="Width:45%">Place of Supply</th>
                            <th style="Width:45%">Supply Type</th>
                            <th style="text-align: right; width:5%">Actions</th>
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
            <asp:DataPager ID="dpAdjustAdvance" runat="server" PagedControlID="lv_Adjust_Advances" PageSize="10" class="btn-group-sm pager-buttons">
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
