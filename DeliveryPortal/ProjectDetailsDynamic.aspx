<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="ProjectDetailsDynamic.aspx.cs" Inherits="DeliveryPortal.ProjectDetailsDynamic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>

        $(function () {
            $(".datepicker").datepicker();

        });

        function ValidateDate(oSrc, args) {
            args.IsValid = checkdate(args.Value);
            return args.IsValid;
        }

        function checkdate(input) {
            var validformat = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input))
                alert("Invalid Date Format. Please correct and save again.")
            else { //Detailed check for valid date ranges
                var monthfield = input.split("/")[0]
                var dayfield = input.split("/")[1]
                var yearfield = input.split("/")[2]
                var dayobj = new Date(yearfield, monthfield - 1, dayfield)
                if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield))
                    alert("Invalid Day, Month, or Year range detected. Please correct and save again.")
                else
                    returnval = true
            }
            return returnval
        }





    </script>

    <div id="tabs">
  <ul>
    <li><a href="#fragment-1"><span style ="font-weight:bold">Project Code</span></a></li>
    <li><a href="#fragment-2"><span style ="font-weight:bold">Dynamic Attributes</span></a></li>
    <li><a href="#fragment-3"><span style ="font-weight:bold">Waiting...</span></a></li>
    <li><a href="#fragment-4"><span style ="font-weight:bold">Compliance Attributes</span></a></li>
  </ul>
  <div id="fragment-1">
      <table style="width: 100%;">
          <tr><td class="label"><span class="error">*</span>Project Code(s):</td>
              <td><asp:ListBox ID="lbProjectCode" runat="server" SelectionMode="Multiple" Width="153px"><%-- <asp:ListItem>5435564</asp:ListItem>
                <asp:ListItem>7686887</asp:ListItem>
                <asp:ListItem>4561234</asp:ListItem>
                <asp:ListItem>7894561</asp:ListItem>
                <asp:ListItem>1234567</asp:ListItem>--%></asp:ListBox>
                 
                
                  <asp:RequiredFieldValidator ID="rfvProjectCodes" runat="server" ErrorMessage="Please select Project Code(s)." ControlToValidate="lbProjectCode" Display="None"></asp:RequiredFieldValidator>
              </td>
          </tr>
          <tr><asp:Button ID="Button1" runat="server" Text="Button" CausesValidation="false" OnClick="Button1_Click"/></tr>
          

      </table>
      
      
  </div>
  <div id="fragment-2">
   
  </div>
  <div id="fragment-3">
  
  </div>

  <div id="fragment-4">
  
  </div>
</div>
 



    <div>
     <table style="width: 100%;">
          <tr>
            <td colspan="4" align="center"><b>Project Details</b></td>
            
        </tr>
        <tr align="center">
            <td colspan="4" align="center">
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label><input type="hidden" id="Hidden1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="BulletList" ShowSummary="true" />
            </td>
        </tr>
         <tr><td class="label"><span class="error">*</span>IDP:</td>
            <td>
                <asp:DropDownList ID="ddlIDP" runat="server" OnSelectedIndexChanged="ddlIDP_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvIDP" runat="server" ErrorMessage="Please select IDP" ControlToValidate="ddlIDP" Display="None"></asp:RequiredFieldValidator>
            </td></tr><tr>
             <td class="label"><span class="error">*</span>Account : </td>
            <td>
                <asp:DropDownList ID="ddlAccount" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="redAccount" runat="server" Display="None" ControlToValidate="ddlAccount" InitialValue="" ErrorMessage="Please select Account"></asp:RequiredFieldValidator>
            </td>
         </tr>
         <tr>
              <td class="label"><span class="error">*</span>Project Logical Name : </td>
            <td>
                <asp:TextBox ID="textProjectName" runat="server"></asp:TextBox><input type="hidden" id="hidProjectId" runat="server" />
                <asp:RequiredFieldValidator ID="rfvProjectName" runat="server" Display="None" ControlToValidate="textProjectName" ErrorMessage="Please Fill Project Logical Name"></asp:RequiredFieldValidator>
            </td></tr>
         <tr>
             <td class="label"><span class="error">*</span>Engagement Manager:</td>
            <td>
                <asp:DropDownList ID="ddlEM" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvEM" runat="server" ErrorMessage="Please select Engagement Manager" ControlToValidate="ddlEM" Display="None"></asp:RequiredFieldValidator>
            </td>

         </tr>
         <tr><td class="label"><span class="error">*</span>Project Manager:</td>
            <td>
                <asp:DropDownList ID="ddlPM" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPM" runat="server" ErrorMessage="Please select Project Manager" ControlToValidate="ddlPM" Display="None"></asp:RequiredFieldValidator>
            </td></tr>
               <tr>
             <td class="label">Is Strategic : </td>
            <td>
                <asp:CheckBox ID="chkIsStrategic" runat="server" /></td>
         </tr>

     </table>
    </div>
    <div>
        <asp:Table ID="tblRecipients" runat="server" HorizontalAlign="Center" >
           <%-- <asp:TableRow>
                <asp:TableCell><b>Project Details</b></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="label"><span class="error">*</span>IDP:</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlIDP" runat="server" OnSelectedIndexChanged="ddlIDP_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvIDP" runat="server" ErrorMessage="Please select IDP" ControlToValidate="ddlIDP" Display="None"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>                
                <asp:TableCell CssClass="label"><span class="error">*</span>Account : </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlAccount" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="redAccount" runat="server" Display="None" ControlToValidate="ddlAccount" InitialValue="" ErrorMessage="Please select Account"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="label"><span class="error">*</span>Project Logical Name : </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="textProjectName" runat="server"></asp:TextBox><input type="hidden" id="hidProjectId" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvProjectName" runat="server" Display="None" ControlToValidate="textProjectName" ErrorMessage="Please Fill Project Logical Name"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="label"><span class="error">*</span>Engagement Manager:</asp:TableCell><asp:TableCell>
                    <asp:DropDownList ID="ddlEM" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEM" runat="server" ErrorMessage="Please select Engagement Manager" ControlToValidate="ddlEM" Display="None"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="label"><span class="error">*</span>Project Manager:</asp:TableCell><asp:TableCell>
                    <asp:DropDownList ID="ddlPM" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvPM" runat="server" ErrorMessage="Please select Project Manager" ControlToValidate="ddlPM" Display="None"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>            
                <asp:TableCell CssClass="label">Is Strategic : </asp:TableCell><asp:TableCell>
                    <asp:CheckBox ID="chkIsStrategic" runat="server" /></asp:TableCell></asp:TableRow>--%>
        </asp:Table>
    </div>
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="label"><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"/></td>
                <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" /></td></tr></table></div>
    <div><asp:Label ID="lblValues" runat="server"></asp:Label></div>

    <script>
        $("#tabs").tabs();
   </script>

</asp:Content>
 