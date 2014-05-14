using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeliveryPortalDL;
using DeliveryPortalEntities;

namespace DeliveryPortal
{
    public partial class EmailConfigurationList : System.Web.UI.Page
    {
        ReminderServiceDL _reminderServiceDL = new ReminderServiceDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PopulateEmailList();
        }
        private void PopulateEmailList()
        {
            grdEmailList.DataSource = _reminderServiceDL.GetEmailConfigurationList();
            grdEmailList.DataBind();
        }

        protected void grdEmailList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdEmailList.PageIndex = e.NewPageIndex;
            grdEmailList.DataSource = _reminderServiceDL.GetEmailConfigurationList();
            grdEmailList.DataBind();
        }
    }
}