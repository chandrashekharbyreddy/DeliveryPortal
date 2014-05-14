using DeliveryPortalDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Configuration;

namespace DDReminderService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                ProjectDL projectDL = new ProjectDL();

                string emailIdTo = "shashank.hemani@capgemini.com";
                //string emailIdTo = projectDL.GetDDReminderServiceEmailIdList();
                string emailIdFrom = Convert.ToString(ConfigurationManager.AppSettings["EmailIdFrom"]);
                string subject = "Weekly Update";
                string bodyText = Convert.ToString(ConfigurationManager.AppSettings["EmailBody"]);

                if (emailIdTo != string.Empty)
                    Utilities.SendEmails(emailIdFrom, emailIdTo, subject, bodyText);
            }
            catch (Exception ex)
            {

                System.Diagnostics.EventLog.WriteEntry("DDReminderService", string.Format("Message : {0} and Inner Exception : {1} ", ex.Message, ex.InnerException));
            }

        }
    }
}
