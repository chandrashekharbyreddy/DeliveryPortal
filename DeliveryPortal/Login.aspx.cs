using DeliveryPortalDL;
using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Common;
using System.Configuration;

namespace DeliveryPortal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtWindowsUserId.Text = Common.WindowsUserId;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //   vccutxtWindowsUserId.Validate();
            if (vccutxtWindowsUserId.IsValid)
            {
                //string LDapConnectionString = Convert.ToString(ConfigurationManager.AppSettings["LDAPConnectionString"]);
                //LdapAuthentication objAuthentication = new LdapAuthentication("LDAP://DC=CORP,DC=CAPGEMINI,DC=COM");
                //LdapAuthentication objAuthentication = new LdapAuthentication(LDapConnectionString);

                Response.Redirect("Index.aspx");
            }

        }

        protected void vccutxtWindowsUserId_ServerValidate(object source, ServerValidateEventArgs args)
        {

            EmployeeDL objEmployeeDL = new EmployeeDL();
            Common.WindowsUserId = txtWindowsUserId.Text;
            EmployeeModel objEmployee = objEmployeeDL.GetEmployeeDetails(Common.WindowsUserId);
            if (objEmployee != null)
            {
                FormsAuthentication.SetAuthCookie(ddlLevel.SelectedItem.Value, false);

                Common.EmployeeId = objEmployee.EmployeeId;
                Common.EmployeeName = objEmployee.EmployeeName;
                Common.EmployeeLevel = Convert.ToInt32(ddlLevel.SelectedItem.Value);
                Common.EmployeeLevelName = ddlLevel.SelectedItem.Text;


                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
                vccutxtWindowsUserId.ErrorMessage = "User with entered Windows Id is not created. Please check with Administrator to add your windows Id in the system.";
            }


        }
    }
}