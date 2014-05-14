using DeliveryPortalDL;
using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
namespace DeliveryPortal
{
    public partial class Default : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Common.EmployeeId == 0)
                //{
                //    EmployeeDL objEmployeeDL = new EmployeeDL();
                //    EmployeeModel objEmployee = objEmployeeDL.GetEmployeeDetails(Common.WindowsUserId);
                //    if (objEmployee != null)
                //    {
                //        Common.EmployeeId = objEmployee.EmployeeId;
                //        Common.EmployeeName = objEmployee.EmployeeName;
                //    }
                //}
                lblUserName.Text = Common.EmployeeName + " (" + Common.EmployeeLevelName + ")";
                SetMenu();

            }
        }
        private void SetMenu()
        {
            MenuItem itemTransactions = mnuMain.FindItem("Transactions");

            switch ((CommonConstants.Level)Common.EmployeeLevel)
            {
                case CommonConstants.Level.PM:
                    itemTransactions.ChildItems.Remove(mnuMain.FindItem("Transactions/DEVerification"));
                    itemTransactions.ChildItems.Remove(mnuMain.FindItem("Transactions/NewDEReview"));
                    itemTransactions.ChildItems.Remove(mnuMain.FindItem("Transactions/DECalendar"));
                    
                    break;
                default:
                    itemTransactions.ChildItems.Remove(mnuMain.FindItem("Transactions/DEUpdates"));
                    break;
            }
        }

        protected void lnkbtnLogout_Click(object sender, EventArgs e)
        {
            Common.EmployeeLevel = 0;
            Common.EmployeeId = 0;
            Common.EmployeeName = string.Empty;
            Common.EmployeeLevelName = string.Empty;
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }


    }
}