using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeliveryPortalEntities;
using System.Globalization;
using DeliveryPortalDL;

namespace DeliveryPortal.UserControls
{
    public partial class ProjectAttributes : System.Web.UI.UserControl
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
            lblMessage.Text = "";
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

        public void SaveWeeklyDashboard()
        {
            try
            {
                WeeklyStatusDL objStatusDL = new WeeklyStatusDL();
                ProjectWeeklyStatusModel objWeeklyStatusModel = new ProjectWeeklyStatusModel();

                objWeeklyStatusModel.WeeklyStatusId = WeeklyStatusId;


                if (WeekDate.HasValue)
                {
                    objWeeklyStatusModel.WeekStart = WeekDate.Value;
                    objWeeklyStatusModel.WeekId = GetWeekNumber(WeekDate.Value);
                    objWeeklyStatusModel.Year = WeekDate.Value.Year;
                }
                objWeeklyStatusModel.ProjectId = ProjectId;

                objWeeklyStatusModel.CorrectiveActions = txtCorrectiveAction.Text;
                objWeeklyStatusModel.IssueItems = txtIssues.Text;
                objWeeklyStatusModel.RiskItems = txtRiskItems.Text;
                objWeeklyStatusModel.LatestUpdates = txtLatestUpdates.Text;
                objWeeklyStatusModel.FlagUpdatedByLevel = Common.EmployeeLevel;
                //objWeeklyStatusModel.AttributeStatusValues;
                objWeeklyStatusModel.AttributeStatusValues = new List<ProjectWeeklyAttributeStatus>();
                //foreach (GridViewRow row in gvAttributes.Rows)
                //{
                //    ProjectWeeklyAttributeStatus objStatus = new ProjectWeeklyAttributeStatus();
                //    DropDownList ddlFlag = (DropDownList)row.FindControl("ddlFlag");
                //    if (ddlFlag != null && ddlFlag.SelectedIndex != 0)
                //    {
                //        objStatus.FlagId = Convert.ToInt32(ddlFlag.SelectedItem.Value);
                //    }
                //    if (objStatus.FlagId != 0)
                //    {
                //        string attributeId = gvAttributes.DataKeys[row.RowIndex]["AttributeId"].ToString();
                //        objStatus.AttributeId = String.IsNullOrEmpty(attributeId) ? 0 : Convert.ToInt32(attributeId);

                //        objStatus.WeeklyStatusId = objWeeklyStatusModel.WeeklyStatusId;
                //        objStatus.LastUpdatedDate = DateTime.Now;
                //        objWeeklyStatusModel.AttributeStatusValues.Add(objStatus);
                //    }
                //}

                foreach (DataListItem row in dlAttributes.Items)
                {
                    ProjectWeeklyAttributeStatus objStatus = new ProjectWeeklyAttributeStatus();
                    DropDownList ddlFlag = (DropDownList)row.FindControl("ddlFlag");
                    if (ddlFlag != null && ddlFlag.SelectedIndex != 0)
                    {
                        objStatus.FlagId = Convert.ToInt32(ddlFlag.SelectedItem.Value);
                    }
                    if (objStatus.FlagId != 0)
                    {
                        string attributeId = dlAttributes.DataKeys[row.ItemIndex].ToString();
                        objStatus.AttributeId = String.IsNullOrEmpty(attributeId) ? 0 : Convert.ToInt32(attributeId);

                        objStatus.WeeklyStatusId = objWeeklyStatusModel.WeeklyStatusId;
                        objStatus.LastUpdatedDate = DateTime.Now;
                        if (objStatus.AttributeId != -1)
                            objWeeklyStatusModel.AttributeStatusValues.Add(objStatus);
                    }
                }
                objWeeklyStatusModel.LastUpdatedBy = Common.EmployeeId;
                WeeklyStatusId = objStatusDL.SaveWeeklyDashboard(objWeeklyStatusModel);
                lblMessage.Text = "Data Saved Successfully";

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        public void PopulateWeeklyDashboard()
        {
            if (ProjectId != 0 && WeekDate != DateTime.MinValue)
            {
                btnSave.Visible = !IsReadOnly;
                btnCancel.Visible = !IsReadOnly;
                trItemHeaders.Visible = !IsReadOnly;
                txtCorrectiveAction.Visible = !IsReadOnly;
                txtRiskItems.Visible = !IsReadOnly;
                txtLatestUpdates.Visible = !IsReadOnly;
                txtIssues.Visible = !IsReadOnly;
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
                ProjectWeeklyStatusModel objWeeklyStatusModel = objWeeklyStatusDL.GetWeeklyStatus(projectId, weekId, year, Common.EmployeeLevel);
                if (objWeeklyStatusModel != null && (objWeeklyStatusModel.WeeklyStatusId != 0 || !IsReadOnly))
                {
                    PopulateWeekDates(weekId, year);

                    dvProjectAttribute.Visible = true;
                    WeeklyStatusId = objWeeklyStatusModel.WeeklyStatusId;
                    txtCorrectiveAction.Text = objWeeklyStatusModel.CorrectiveActions;
                    txtRiskItems.Text = objWeeklyStatusModel.RiskItems;
                    txtLatestUpdates.Text = objWeeklyStatusModel.LatestUpdates;
                    txtIssues.Text = objWeeklyStatusModel.IssueItems;

                    lblCorrectiveAction.Text = objWeeklyStatusModel.CorrectiveActions;
                    lblRiskItems.Text = objWeeklyStatusModel.RiskItems;
                    lblLatestUpdates.Text = objWeeklyStatusModel.LatestUpdates;
                    lblIssues.Text = objWeeklyStatusModel.IssueItems;

                    AttributeStatusValues = objWeeklyStatusModel.AttributeStatusValues;
                    PopulateAttributes();
                }
                else
                {
                    dvProjectAttribute.Visible = false;
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
            CalendarWeekRule rule = CalendarWeekRule.FirstDay ;

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
            WeekDate = result;
            lblCurrentWeek.Text = result.ToString("dd-MMM") + " - " + result.AddDays(6).ToString("dd-MMM");
        }





        //protected void gvAttributes_RowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //    e.Row.Cells[1].Visible = IsReadOnly;
        //    e.Row.Cells[2].Visible = !IsReadOnly;
        //    if (!IsReadOnly)
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            DropDownList ddlFlag = (DropDownList)e.Row.FindControl("ddlFlag");
        //            if (ddlFlag != null)
        //            {
        //                WeeklyStatusDL objWeeklyStatusDL = new WeeklyStatusDL();

        //                ddlFlag.DataSource = objWeeklyStatusDL.GetFlags();
        //                ddlFlag.DataTextField = "FlagName";
        //                ddlFlag.DataValueField = "FlagId";
        //                ddlFlag.DataBind();
        //                ListItem listItem = new ListItem("-Please Select-", "0");
        //                ddlFlag.Items.Insert(0, listItem);

        //                ListItem lstSelected = ddlFlag.Items.FindByValue(((ProjectWeeklyAttributeStatus)(e.Row.DataItem)).FlagId.ToString());

        //                if (lstSelected != null)
        //                {
        //                    ddlFlag.SelectedIndex = -1;
        //                    ddlFlag.Items.FindByValue(((ProjectWeeklyAttributeStatus)(e.Row.DataItem)).FlagId.ToString()).Selected = true;
        //                }
        //            }

        //        }
        //    }
        //}

        protected void dlAttributes_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            // e.Row.Cells[1].Visible = IsReadOnly;
            //e.Row.Cells[2].Visible = !IsReadOnly;
            if (e.Item.FindControl("imgFlagStatus") != null)
            {
                e.Item.FindControl("imgFlagStatus").Visible = IsReadOnly;
            }
            if (e.Item.FindControl("ddlFlag") != null)
            {
                e.Item.FindControl("ddlFlag").Visible = !IsReadOnly;
            }

            if (!IsReadOnly)
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DropDownList ddlFlag = (DropDownList)e.Item.FindControl("ddlFlag");
                    if (ddlFlag != null)
                    {
                        WeeklyStatusDL objWeeklyStatusDL = new WeeklyStatusDL();

                        ddlFlag.DataSource = objWeeklyStatusDL.GetFlags();
                        ddlFlag.DataTextField = "FlagName";
                        ddlFlag.DataValueField = "FlagId";
                        ddlFlag.DataBind();
                        ListItem listItem = new ListItem("Not Applicable", "0");

                        ddlFlag.Items.Insert(0, listItem);

                        //foreach (ListItem item in ddlFlag.Items)
                        //{
                        //    if (item.Text.Equals("Amber"))
                        //        item.Attributes.Add("style", @"color:white;background-color:#FFBF00");
                        //    else
                        //        item.Attributes.Add("style", @"color:white;background-color:" + item.Text);
                        //}
                        //ddlFlag.Items[0].Attributes.Add("style", @"color:white;background-color:silver");
                        ListItem lstSelected = ddlFlag.Items.FindByValue(((Label)e.Item.FindControl("lblFlagId")).Text);
                        bool isEditable = Convert.ToBoolean(((Label)e.Item.FindControl("lblIsEditable")).Text);

                        if (e.Item.FindControl("imgFlagStatus") != null)
                        {
                            e.Item.FindControl("imgFlagStatus").Visible = !isEditable;
                        }
                        if (e.Item.FindControl("ddlFlag") != null)
                        {
                            e.Item.FindControl("ddlFlag").Visible = isEditable;
                        }

                        ddlFlag.Enabled = isEditable;
                        if (lstSelected != null)
                        {
                            ddlFlag.SelectedIndex = -1;
                            ddlFlag.Items.FindByValue(((ProjectWeeklyAttributeStatus)(e.Item.DataItem)).FlagId.ToString()).Selected = true;
                        }

                    }

                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveWeeklyDashboard();
            PopulateWeeklyDashboard();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("WeeklyDashboard.aspx");
        }
    }
}