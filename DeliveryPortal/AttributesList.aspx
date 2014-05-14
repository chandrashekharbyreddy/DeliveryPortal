<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="AttributesList.aspx.cs" Inherits="DeliveryPortal.ProjectAttributes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function confirmation() {
            // return confirm("Are you sure.?");
            var count = 0;

            var gv = document.getElementById("<%=GridViewAttribute.ClientID%>");

            var chk = gv.getElementsByTagName('input');

                for (var i = 0; i < chk.length; i++) {
                    
                    if ((chk[i].checked && chk[i].id.indexOf("chkSelect") != -1) && chk[i].id.indexOf("chkAll") == -1) {

                        count++;

                    }

                }

                if (count == 0) {

                    alert("No records to delete.");

                    return false;

                }

                else {

                    return confirm("Do you want to delete " + count + " records.");

                }


            }
    </script>
<%--    <style>
        .checkBox {
        }
    </style>--%>
    <script type="text/javascript" language="javascript">
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridViewAttribute.ClientID %>");
            var hdnPageSize = document.getElementById("<%=hidPageSize.ClientID %>");
            var hidPageIndex = document.getElementById("<%=hidPageIndex.ClientID%>");
            var PageIndex = parseInt(hidPageIndex.value);
            var PageSize = parseInt(hdnPageSize.value);
            if (GridVwHeaderChckbox.rows.length == PageSize + 2) {
                for (i = 1; i < GridVwHeaderChckbox.rows.length - 1; i++) {
                    GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName('input')[0].checked = Checkbox.checked;
                }
            }
            else if (GridVwHeaderChckbox.rows.length != PageSize + 2 && PageIndex > 0) {
                for (i = 1; i < GridVwHeaderChckbox.rows.length - 1; i++) {
                    GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName('input')[0].checked = Checkbox.checked;
                }
            }
            else {
                for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                    GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName('input')[0].checked = Checkbox.checked;
                }
            }
        }
    </script>
    <table width="100%" cellspacing="4" cellpadding="4">

        <tr>

                        <td class="label" width="20%">Attribute Name: </td>
            <td width="15%">
                            <asp:TextBox ID="txtAttributeName" runat="server"></asp:TextBox></td>
            <td>
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />
                            <input type="hidden" id="hidPageSize" runat="server" />
                             <input type="hidden" id="hidPageIndex" runat="server" />
                            <asp:Button ID="BtnAddNewAttribute" runat="server" Text="Add New Attribute" OnClick="BtnAddNewAttribute_Click" /></td>
                        
                    </tr>
                    <tr>
            <td colspan="3">
                <asp:Button ID="BtnDeleteAttribute" runat="server" align="left" Text="Delete" OnClick="BtnDeleteAttribute_Click" OnClientClick="return confirmation()" />
             <label style="text-align:center">Attributes List</label>      </td>
                       
                    </tr>
                    <tr>
            <td colspan="3">
                <asp:GridView ID="GridViewAttribute" runat="server" AutoGenerateColumns="False"  EmptyDataText="Record Not Found" EmptyDataRowStyle-Font-Bold="true" AllowPaging="true" PageSize="10" DataKeyNames="AttributeId" OnRowDataBound="GridViewAttribute_RowDataBound" OnPageIndexChanging="GridViewAttribute_PageIndexChanging" >
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" CssClass="checkBox" runat="server" Onclick="CheckAllEmp(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" CssClass="checkBox" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:HyperLinkField HeaderText="Attribute Name" DataTextField="AttributeName" SortExpression="AttributeName" DataNavigateUrlFields="AttributeId" DataNavigateUrlFormatString="AttributeMaster.aspx?AttributeId={0}" />
                                    <asp:BoundField DataField="EffectiveStartDate" HeaderText="Start Date" SortExpression="EffectiveStartDate" DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="true" />
                                    <asp:BoundField DataField="EffectiveEndDate" HeaderText="End Date" SortExpression="EffectiveEndDate" DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Parent Attribute">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParentAttribute" runat="server" Text='<% #GetParentAttributeName(Eval("ParentAttributeId"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField>
                                        
                                        <ItemTemplate>
                                <asp:Label ID="lblIsDE" HeaderText="IsDE" runat="server" Visible="false" Text='<% #Bind("IsDE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is DE Attribute?">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsDE" HeaderText="IsDE" SortExpression="IsDE" DataField="IsDE" Enabled="false" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    

                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>

    </table>



</asp:Content>
