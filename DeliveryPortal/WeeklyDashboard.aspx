<%@ Page Title="" Language="C#" MasterPageFile="~/ChildMaster.Master" AutoEventWireup="true" CodeBehind="WeeklyDashboard.aspx.cs" Inherits="DeliveryPortal.WeeklyDashboard" ValidateRequest="false" %>


<%@ Register Src="UserControls/ProjectAttributes.ascx" TagName="ProjectAttributes" TagPrefix="uc1" %>
<%@ Register Src="UserControls/ViewProjectAttributes.ascx" TagName="ViewProjectAttributes" TagPrefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphNote" runat="server">
    <asp:ValidationSummary ID="vsSummary" runat="server" />
<style>
    .hidebr br {
        display: none;
    }

    .tdContainer {
        width: 75px;
        float: left;
    }
    
</style>
    <script src="Scripts/tinymce.min.js"></script>
    <script type="text/javascript">
        tinymce.init({
            selector: "textarea",
            plugins: [
               "advlist  lists ",
               "wordcount nonbreaking",
               "textcolor textcolor"
            ],
            theme: 'modern',
            toolbar1: "forecolor strikethrough underline | bullist",
            menubar: false,
            statusbar: false,
            toolbar_items_size: 'small',
            paste_auto_cleanup_on_paste: true,
            paste_convert_middot_lists: false,
            paste_text_sticky: true,
            paste_text_sticky_default: true,
            paste_strip_class_attributes: "all",
            paste_remove_styles: true,
            paste_remove_spans: true,
            paste_block_drop: true,
            spellchecker_languages: "+English=en_GB"
          

          
        });


        $(function () {
            var startDate;
            var endDate;

            var selectCurrentWeek = function () {
                window.setTimeout(function () {
                    $('.ui-weekpicker').find('.ui-datepicker-current-day a').addClass('ui-state-active').removeClass('ui-state-default');
                }, 1);
            }

            var setDates = function (input) {

                var $input = $(input);
                $input.datepicker('option', 'firstDay', 1);
                var date = $input.datepicker('getDate');
                if (date !== null) {
                    var firstDay = $input.datepicker("option", "firstDay");
                    var dayAdjustment = date.getDay() - firstDay;
                    if (dayAdjustment < 0) {
                        dayAdjustment += 7;
                    }
                    startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - dayAdjustment);
                    endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - dayAdjustment + 6);

                    var inst = $input.data('datepicker');
                    var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;


                    $('#lblWeekDates').text($.datepicker.formatDate(dateFormat, startDate, inst.settings).toString() + " - " + $.datepicker.formatDate(dateFormat, endDate, inst.settings));
                    $('#weekDates').text($.datepicker.formatDate(dateFormat, startDate, inst.settings));

                }
            }

            $('.week-picker').datepicker({

                beforeShow: function () {
                    $('#ui-datepicker-div').addClass('ui-weekpicker');
                    selectCurrentWeek();
                },
                onClose: function () {
                    $('#ui-datepicker-div').removeClass('ui-weekpicker');
                    setDates(this);
                },
                showOtherMonths: true,
                selectOtherMonths: true,
                onSelect: function (dateText, inst) {
                    setDates(this);
                    selectCurrentWeek();
                    $(this).change();

                },
                beforeShowDay: function (date) {
                    var cssClass = '';
                    if (date >= startDate && date <= endDate)
                        cssClass = 'ui-datepicker-current-day';
                    return [true, cssClass];
                },
                onChangeMonthYear: function (year, month, inst) {
                    selectCurrentWeek();
                }
            });

            setDates('.week-picker');
            $('.week-picker').datepicker('option', 'firstDay', 1);
            $('.week-picker').datepicker('option', 'maxDate', new Date());
            var $calendarTR = $('.ui-weekpicker .ui-datepicker-calendar tr');
            $calendarTR.live('mousemove', function () {
                $(this).find('td a').addClass('ui-state-hover');
            });
            $calendarTR.live('mouseleave', function () {
                $(this).find('td a').removeClass('ui-state-hover');
            });
        });
    </script>
    <table id="tblMain" style="width: 100%">
        <tr>
            <td class="label" style="width: 15%"><span class="error">*</span>


                Account : 
            </td>
            <td style="width: 15%">
                <asp:DropDownList ID="ddlAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccount_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfddlAccount" runat="server" InitialValue="0" ErrorMessage="Please Select an Account" ControlToValidate="ddlAccount" Display="None"></asp:RequiredFieldValidator>
            </td>
            <td class="label" style="width: 15%"><span class="error">*</span> Project : </td>
            <td style="width: 15%">
                <asp:DropDownList ID="ddlProject" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfddlProject" runat="server" InitialValue="0" ErrorMessage="Please select Project" ControlToValidate="ddlProject" Display="None"></asp:RequiredFieldValidator>
            </td>

            <td class="label" style="width: 15%"><span class="error">*</span> Week : </td>
            <td style="width: 15%">
                <asp:TextBox ID="txtWeekStart" runat="server" class="week-picker">
                    
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="rftxtWeekStart" runat="server" ErrorMessage="Please select Week" ControlToValidate="txtWeekStart" Display="None"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="weekDates" runat="server"></asp:HiddenField>

            </td>

            <td style="width: 10%">
                <asp:Button ID="btnSearch" runat="server" Text="Go" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="5"></td>
            <td colspan="2">
                <label id="lblWeekDates" style="font-size: 8pt"></label>

            </td>
        </tr>
        <tr>

            <td colspan="7">
                <uc1:ProjectAttributes ID="ucprojectCurrentWeek" runat="server" IsReadOnly="false" />
                <table id="tblProjectAttribute" style="padding:0px; word-spacing:0px; width: 100%; border: solid 1px black; padding: 4px; background-color: white;" >

                    <uc2:ViewProjectAttributes ID="ucProjectPreviousWeekHeaders" runat="server" IsReadOnly="true" OnlyHeaders="true" />
                    <uc2:ViewProjectAttributes ID="ucProjectPreviousWeek1" runat="server" IsReadOnly="true" />
                    <uc2:ViewProjectAttributes ID="ucProjectPreviousWeek2" runat="server" IsReadOnly="true" />
                    <uc2:ViewProjectAttributes ID="ucProjectPreviousWeek3" runat="server" IsReadOnly="true" />
                    <uc2:ViewProjectAttributes ID="ucProjectPreviousWeek4" runat="server" IsReadOnly="true" />
                    <uc2:ViewProjectAttributes ID="ucProjectPreviousWeek5" runat="server" IsReadOnly="true" />
                    <uc2:ViewProjectAttributes ID="ucProjectPreviousWeek6" runat="server" IsReadOnly="true" />
                    <uc2:ViewProjectAttributes ID="ucProjectPreviousWeek7" runat="server" IsReadOnly="true" />
                    <uc2:ViewProjectAttributes ID="ucProjectPreviousWeek8" runat="server" IsReadOnly="true" />
                    <uc2:ViewProjectAttributes ID="ucProjectPreviousWeek9" runat="server" IsReadOnly="true" />
                </table>
            </td>


        </tr>

    </table>
</asp:Content>
