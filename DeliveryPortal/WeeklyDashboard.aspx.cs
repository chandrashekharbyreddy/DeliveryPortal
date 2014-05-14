using DeliveryPortalDL;
using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliveryPortal
{
    public partial class WeeklyDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateAccounts();
                ListItem listItem = new ListItem("-Please Select-", "0");
                ddlProject.Items.Insert(0, listItem);

            }

        }

        private void PopulateProjects(int accountId)
        {
            ddlProject.Items.Clear();
            WeeklyStatusDL weeklyStatusDL = new WeeklyStatusDL();
            ddlProject.DataSource = weeklyStatusDL.GetProjects(accountId);
            ddlProject.DataTextField = "ProjectName";
            ddlProject.DataValueField = "ProjectId";
            ddlProject.DataBind();
            ListItem listItem = new ListItem("-Please Select-", "0");
            ddlProject.Items.Insert(0, listItem);
        }



        private void PopulateAccounts()
        {
            WeeklyStatusDL weeklyStatusDL = new WeeklyStatusDL();
            ddlAccount.DataSource = weeklyStatusDL.GetAccounts();
            ddlAccount.DataTextField = "AccountName";
            ddlAccount.DataValueField = "AccountId";
            ddlAccount.DataBind();
            ListItem listItem = new ListItem("-Please Select-", "0");
            ddlAccount.Items.Insert(0, listItem);

        }

        protected void ddlAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateProjects(Convert.ToInt32(ddlAccount.SelectedItem.Value));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int projectId = Convert.ToInt32(ddlProject.SelectedItem.Value);
            DateTime dtWeekStartDate = Convert.ToDateTime(txtWeekStart.Text);



            ucprojectCurrentWeek.ProjectId = projectId;
            ucprojectCurrentWeek.WeekDate = Convert.ToDateTime(txtWeekStart.Text);
            ucprojectCurrentWeek.PopulateWeeklyDashboard();

            ucProjectPreviousWeekHeaders.ProjectId = projectId;
            ucProjectPreviousWeekHeaders.WeekDate = Convert.ToDateTime(txtWeekStart.Text);
            ucProjectPreviousWeekHeaders.PopulateWeeklyDashboard();


            ucProjectPreviousWeek1.ProjectId = projectId;
            ucProjectPreviousWeek1.WeekDate = dtWeekStartDate.AddDays(-7);
            ucProjectPreviousWeek1.PopulateWeeklyDashboard();


            ucProjectPreviousWeek2.ProjectId = projectId;
            ucProjectPreviousWeek2.WeekDate = dtWeekStartDate.AddDays(-14);
            ucProjectPreviousWeek2.PopulateWeeklyDashboard();


            ucProjectPreviousWeek3.ProjectId = projectId;
            ucProjectPreviousWeek3.WeekDate = dtWeekStartDate.AddDays(-21);
            ucProjectPreviousWeek3.PopulateWeeklyDashboard();

            ucProjectPreviousWeek4.ProjectId = projectId;
            ucProjectPreviousWeek4.WeekDate = dtWeekStartDate.AddDays(-28);
            ucProjectPreviousWeek4.PopulateWeeklyDashboard();

            ucProjectPreviousWeek5.ProjectId = projectId;
            ucProjectPreviousWeek5.WeekDate = dtWeekStartDate.AddDays(-35);
            ucProjectPreviousWeek5.PopulateWeeklyDashboard();

            ucProjectPreviousWeek6.ProjectId = projectId;
            ucProjectPreviousWeek6.WeekDate = dtWeekStartDate.AddDays(-42);
            ucProjectPreviousWeek6.PopulateWeeklyDashboard();

            ucProjectPreviousWeek7.ProjectId = projectId;
            ucProjectPreviousWeek7.WeekDate = dtWeekStartDate.AddDays(-49);
            ucProjectPreviousWeek7.PopulateWeeklyDashboard();

            ucProjectPreviousWeek8.ProjectId = projectId;
            ucProjectPreviousWeek8.WeekDate = dtWeekStartDate.AddDays(-56);
            ucProjectPreviousWeek8.PopulateWeeklyDashboard();

            ucProjectPreviousWeek9.ProjectId = projectId;
            ucProjectPreviousWeek9.WeekDate = dtWeekStartDate.AddDays(-63);
            ucProjectPreviousWeek9.PopulateWeeklyDashboard();
        }


    }
}