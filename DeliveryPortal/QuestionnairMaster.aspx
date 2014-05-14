<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionnairMaster.aspx.cs" Inherits="DeliveryPortal.QuestionnairMaster" MasterPageFile="~/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            font-weight: 700;
            text-align: right;
            width: 277px;
        }
        .auto-style4 {
            height: 49px;
            font-size: large;
            font-weight: bold;
        }
        .auto-style5 {
            width: 328px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       

    <table cellspacing="4" cellpadding="4" align="center">
       
         <tr>
            <td colspan="2" align="center" class="auto-style4">Questionnair Master</td>
            
        </tr>
         <tr align="center">
            <td colspan="4" align="center">
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
            &nbsp;
                <asp:Label ID="idpval" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td colspan="2">
                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="BulletList" ShowSummary="true" />
            </td>
        </tr>

        <tr>
            <td class="auto-style3">Question Name: </td>
            <td class="auto-style5">
                <asp:TextBox ID="txtQuestionName" runat="server" MaxLength="500" Enabled="true" Width="173px" OnTextChanged="txtQuestionName_TextChanged"></asp:TextBox><input type="hidden" id="hidQuestionnaireId" runat="server" />
                <asp:RequiredFieldValidator ID="reqQuestionName" Display="None" runat="server" ControlToValidate="txtQuestionName" ErrorMessage="Please enter Question Name"></asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="auto-style3">IDP : </td>
            <td class="auto-style5">
                <asp:DropDownList ID="drpIDP" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqdrpIDP" Display="None" runat="server" ControlToValidate="drpIDP" ErrorMessage="Please select IDP"></asp:RequiredFieldValidator>
            </td>

        </tr>

        <tr>
            <td class="auto-style3">Questionnair Type : </td>
            <td class="auto-style5">
                <asp:DropDownList ID="drpQuestionnairType" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqdrpQuestionnairType" Display="None" runat="server" ControlToValidate="drpQuestionnairType" ErrorMessage="Please select Questionnair Type"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
          <td class="auto-style3">Active : </td>
           <td class="auto-style5">
               
               <asp:CheckBox ID="chkIsActive" runat="server" />
               
           </td>
        </tr>
        

          <tr>
            <td colspan="2"></td>
        </tr>
       <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" />
            
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
                <asp:Button ID="btnProjectList" runat="server" Text="Cancel" OnClick="btnProjectList_Click" CausesValidation="false" />
            </td>
           
        </tr>
    </table>


</asp:Content>



