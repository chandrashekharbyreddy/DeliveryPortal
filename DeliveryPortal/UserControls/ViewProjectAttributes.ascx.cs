using DeliveryPortalDL;
using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliveryPortal.UserControls
{
    public partial class ViewProjectAttributes : System.Web.UI.UserControl
    {
        #region "Properties"


        public bool IsReadOnly
        {
            get { return ViewState["IsReadOnly"] != null ? Convert.ToBoolean(ViewState["IsReadOnly"]) : false; }
            set { ViewState["IsReadOnly"] = value; }
        }


        private List<ProjectWeeklyAttributeStatus> _attributeStatusValues;

        public List<ProjectWeeklyAttributeStatus> AttributeStatusValues
        {
            get { return _attributeStatusValues; }
            set { _attributeStatusValues = value; }
        }

        public int WeeklyStatusId
        {
            get { return hdnWeeklyStatusId.Value != string.Empty ? Convert.ToInt32(hdnWeeklyStatusId.Value) : 0; }
            set { hdnWeeklyStatusId.Value = value.ToString(); }
        }

        public int ProjectId
        {
            get { return ViewState["ProjectId"] != null ? Convert.ToInt32(ViewState["ProjectId"]) : 0; }
            set { ViewState["ProjectId"] = value; }
        }


        public DateTime? WeekDate
        {
            get
            {
                if (ViewState["WeekStartDate"] != null)
                    return Convert.ToDateTime(ViewState["WeekStartDate"].ToString());
                else
                    return null;
            }
            set { ViewState["WeekStartDate"] = value; }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateAttributes()
        {
            if (_attributeStatusValues != null)
            {
                //gvAttributes.DataSource = _attributeStatusValues;
                //gvAttributes.DataBind();

                dlAttributes.DataSource = _attributeStatusValues;
                dlAttributes.DataBind();



            }
        }
        public bool OnlyHeaders { get; set; }
        public void PopulateWeeklyDashboard()
        {
            if (ProjectId != 0 && WeekDate != DateTime.MinValue)
            {

                //trItemHeaders.Visible = !IsReadOnly;
                
                trItemHeaders.Visible = OnlyHeaders;
                trItems.Visible = !OnlyHeaders;
                lblCorrectiveAction.Visible = IsReadOnly;
                lblIssues.Visible = IsReadOnly;
                lblLatestUpdates.Visible = IsReadOnly;
                lblRiskItems.Visible = IsReadOnly;
                int projectId = ProjectId;
                int weekId = 0;
                if (WeekDate.HasValue)
                    weekId = GetWeekNumber(WeekDate.Value);

                int year = WeekDate.Value.Year;

                WeeklyStatusDL objWeeklyStatusDL = new WeeklyStatusDL();

                ProjectWeeklyStatusModel objWeeklyStatusModel = null;
                if (OnlyHeaders)
                {
                    objWeeklyStatusModel = objWeeklyStatusDL.GetWeeklyStatusHeader(projectId, weekId, year, Common.EmployeeLevel, true);
                    AttributeStatusValues = objWeeklyStatusModel.AttributeStatusValues;
                    if (objWeeklyStatusModel.AttributeStatusValues.Count > 0)
                    {
                        PopulateAttributes();
                        tblProjectAttribute.Visible = true;
                    }
                    else
                    {
                        tblProjectAttribute.Visible = false;
                    }
                    PopulateWeekDates(weekId, year);
                }
                else
                {
                    objWeeklyStatusModel = objWeeklyStatusDL.GetWeeklyStatus(projectId, weekId, year, Common.EmployeeLevel, true);

                    if (objWeeklyStatusModel != null && (objWeeklyStatusModel.WeeklyStatusId != 0 || !IsReadOnly))
                    {

                        PopulateWeekDates(weekId, year);

                        tblProjectAttribute.Visible = true;
                        WeeklyStatusId = objWeeklyStatusModel.WeeklyStatusId;

                        lblCorrectiveAction.Text = objWeeklyStatusModel.CorrectiveActions;
                        lblRiskItems.Text = objWeeklyStatusModel.RiskItems;
                        lblLatestUpdates.Text = objWeeklyStatusModel.LatestUpdates;
                        lblIssues.Text = objWeeklyStatusModel.IssueItems;

                        AttributeStatusValues = objWeeklyStatusModel.AttributeStatusValues;
                        PopulateAttributes();
                    }
                    else
                    {
                        tblProjectAttribute.Visible = false;
                    }
                }
            }
        }
        public int GetWeekNumber(DateTime dt)
        {
            CultureInfo cul = CultureInfo.CurrentCulture;
            int weekId = cul.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekId;
        }

        private void PopulateWeekDates(int weekNum, int year)
        {
            CalendarWeekRule rule = CalendarWeekRule.FirstDay;

            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);

            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstMonday, rule, DayOfWeek.Monday);

            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            DateTime result = firstMonday.AddDays((weekNum - 1) * 7);
            if (OnlyHeaders)
                lblCurrentWeek.Text = "Week Date ";
            else
                lblCurrentWeek.Text = result.ToString("dd-MMM") + " - " + result.AddDays(6).ToString("dd-MMM");
        }

        protected void dlAttributes_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.FindControl("dvItem") != null && OnlyHeaders)
                {
                    e.Item.FindControl("dvItem").Visible = !OnlyHeaders;
                }
                if (e.Item.FindControl("dvHeader") != null && OnlyHeaders)
                {
                    e.Item.FindControl("dvHeader").Visible = OnlyHeaders;
                }
            }
        }




    }
}