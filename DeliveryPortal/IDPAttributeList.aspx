<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="IDPAttributeList.aspx.cs" Inherits="DeliveryPortal.Attribute" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        function confirmation() {
            // return confirm("Are you sure.?");
            var count = 0;

            var gv = document.getElementById("<%=grdAttribute.ClientID%>");

                var chk = gv.getElementsByTagName("input");

                for (var i = 0; i < chk.length; i++) {

                    if (chk[i].checked && chk[i].id.indexOf("chkboxSelectAll") == -1) {

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
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdAttribute.ClientID %>");
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
    <table width="100%">
        <tr><td class="label">Attribute :</td>
            <td><asp:TextBox ID="txtAttribute" runat="server"></asp:TextBox></td>
            <td><asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return confirmation()" OnClick="btnSearch_Click" /><asp:Button ID="btnAddAttribute" runat="server" Text="Add New Attribute" OnClick="btnAddAttribute_Click" /></td>

        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="BtnDeleteAttribute" runat="server" align="left" Text="Delete" OnClick="BtnDeleteAttribute_Click" OnClientClick="return confirmation()"/>
             <label style="text-align:center">Attributes</label><input type="hidden" id="hidPageIndex" runat="server" /><input type="hidden" id="hidPageSize" runat="server" />      </td>
                       
                    </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="grdAttribute" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records" EmptyDataRowStyle-Font-Bold="true" AllowPaging="true" PageSize="10" DataKeyNames="AttributeId" OnPageIndexChanging="grdAttribute_PageIndexChanging">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" CssClass="checkbox" runat="server" Onclick="CheckAllEmp(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkAttr" CssClass="checkbox" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField HeaderText="Attribute" DataTextField="AttributeName" SortExpression="AttributeName" DataNavigateUrlFields="AttributeId" DataNavigateUrlFormatString="~/IDPAttributeMaster.aspx?AttributeId={0}"/>
                    <asp:BoundField HeaderText="Start-Date" DataField="AttributeStartDate" SortExpression="AttributeStartDate" DataFormatString="{0:dd-MMM-yyyy}" />
                    <asp:BoundField HeaderText="End-Date" DataField="AttributeEndDate" SortExpression="AttributeEndDate" DataFormatString="{0:dd-MMM-yyyy}" />
                    <asp:BoundField HeaderText="Attribute Type" DataField="AttributeTypeName" SortExpression="AttributeTypeName" />
                </Columns>
                </asp:GridView></td>
        </tr>
        
    </table>
</asp:Content>
