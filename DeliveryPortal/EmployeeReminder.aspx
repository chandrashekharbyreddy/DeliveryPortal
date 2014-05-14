<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="EmployeeReminder.aspx.cs" Inherits="DeliveryPortal.EmployeeReminder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=gridviewEmployee.ClientID %>");
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
            <td colspan="5" style="text-align: center">
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="label" style="width: 15%">Functionality Name : </td>
            <td style="width: 15%">
                <asp:Label Text="" ID="lblFunctionalityName" runat="server" />
            </td>
            <td class="label" style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 40%">&nbsp;</td>
        </tr>


        <tr>
            <td colspan="5">
                <%--<asp:Button ID="BtnDeleteAttribute" runat="server" Text="Delete" OnClick="BtnDeleteAttribute_Click" OnClientClick="return ConfirmDelete();" />--%>
                <label style="text-align: center">Employee List</label></td>
        </tr>



        <%--        <tr>
            <td class="label" style="width: 15%">Employee Code : </td>
            <td style="width: 15%">
                <asp:TextBox ID="txtEmployeeCode" runat="server"></asp:TextBox></td>
            <td class="label" style="width: 15%">Employee Name : </td>
            <td style="width: 15%">
                <asp:TextBox ID="txtEmployeeName" runat="server"></asp:TextBox></td>
            <td style="width: 40%">
                <asp:Button ID="btnEmployeeSearch" runat="server" Text="Search" OnClick="btnEmployeeSearch_Click" />

                <%--<asp:Button ID="btnAddNewEmployee" runat="server" Text="Add New Employee" OnClick="btnAddNewEmployee_Click" /></td>
                
                </td>
        </tr>--%>
        <input type="hidden" id="hidPageSize" runat="server" />
        <input type="hidden" id="hidPageIndex" runat="server" />
        <tr>
            <td colspan="5">
                <asp:GridView ID="gridviewEmployee" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeId" AllowPaging="true" Width="100%" PageSize="10" EmptyDataText="Records Not Found">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" runat="server" Onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" name="chkSelect" runat="server" Checked='<%# Eval("IsEmailConfigured") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EmployeeId" HeaderText="Employee Id" SortExpression="EmployeeId" HeaderStyle-Width="0%" ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName" HeaderStyle-Width="26%" ItemStyle-HorizontalAlign="Left" />
                        <%--<asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation" HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Left" />--%>
                        <%--<asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Left" />--%>
                        <asp:BoundField DataField="EmailId" HeaderText="Email Id" SortExpression="EmailId" HeaderStyle-Width="27%" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </asp:GridView>

            </td>
        </tr>

        <tr>
            <td colspan="5" style="text-align:center">
                <asp:Button Text="Save" ID="btnSave" runat="server" OnClick="btnSave_Click" />
                 <asp:Button Text="Cancel" ID="btnCancel" runat="server" OnClick="btnCancel_Click" />
            </td>
            
        </tr>
    </table>

</asp:Content>
