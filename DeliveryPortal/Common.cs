using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;


namespace DeliveryPortal
{
    public static class Common
    {

        //public static string WindowsUserId
        //{
        //    get
        //    {
        //        return WindowsIdentity.GetCurrent().Name.Substring(WindowsIdentity.GetCurrent().Name.IndexOf(@"\") + 1);
        //    }

        //}
        public static string WindowsUserId
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["WindowsUserId"] != null)
                    return Convert.ToString(System.Web.HttpContext.Current.Session["WindowsUserId"]);
                else
                    return string.Empty;
            }
            set { System.Web.HttpContext.Current.Session["WindowsUserId"] = value; }
        }

        public static string EmployeeLevelName
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["EmployeeLevelName"] != null)
                    return Convert.ToString(System.Web.HttpContext.Current.Session["EmployeeLevelName"]);
                else
                    return string.Empty;
            }
            set { System.Web.HttpContext.Current.Session["EmployeeLevelName"] = value; }
        }

        public static int EmployeeLevel
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["EmployeeLevel"] != null)
                    return Convert.ToInt32(System.Web.HttpContext.Current.Session["EmployeeLevel"]);
                else
                    return 0;
            }
            set { System.Web.HttpContext.Current.Session["EmployeeLevel"] = value; }
        }


        public static int EmployeeId
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["EmployeeId"] != null)
                    return Convert.ToInt32(System.Web.HttpContext.Current.Session["EmployeeId"]);
                else
                    return 0;
            }
            set { System.Web.HttpContext.Current.Session["EmployeeId"] = value; }
        }

        public static string EmployeeName
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["EmployeeName"] != null)
                    return Convert.ToString(System.Web.HttpContext.Current.Session["EmployeeName"]);
                else
                    return string.Empty;
            }
            set { System.Web.HttpContext.Current.Session["EmployeeName"] = value; }
        }

    }
}