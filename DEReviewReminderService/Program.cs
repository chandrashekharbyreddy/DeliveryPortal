using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryPortalDL;
using DeliveryPortalEntities;
using System.Configuration;
using Common;

namespace DEReviewReminderService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Reminder to be sent to Project Owner and EM and Reviewer one day prior to the review date and also on the review date
                SendMailsForUpcomingDEReviews();

                // Reminder to be sent after 1 month if review comments are still open
                SendMailsForOpenDEReviews();

                // Reminder to be sent after 1 week of Reviewer Upd dt to PM if he has not updated the corrective actions
                SendMailsForDEReviewsWithPendingCorrectiveActions();
                //Reminder to be send within 2 days of scheduled date to Review to update the review comments if not already done
                SendMailForAfter2DaysIfNotUpdated();
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("DEReviewReminderService", string.Format("Message : {0} and Inner Exception : {1} ", ex.Message, ex.InnerException));
            }
        }

        private static void SendMailsForUpcomingDEReviews()
        {
            ReminderServiceDL reminderServiceDL = new ReminderServiceDL();

            // Reminder to be sent to Project Owner and EM and Reviewer one day prior to the review date and also on the review date
            List<DEReviewReminderModel> upcomingDEReviewsReminders = reminderServiceDL.GetUpcomingDEReviewsReminder();

            foreach (DEReviewReminderModel deReviewReminderModel in upcomingDEReviewsReminders)
            {
                string emailIdTo = string.Empty;
                if (deReviewReminderModel.ProjectOwner != null && deReviewReminderModel.ProjectOwner != string.Empty)
                {
                    emailIdTo = deReviewReminderModel.ProjectOwner + ",";
                }
                if (deReviewReminderModel.EM != null && deReviewReminderModel.EM != string.Empty)
                {
                    emailIdTo += deReviewReminderModel.EM + ",";
                }

                foreach (string reviewerEmail in deReviewReminderModel.Reviewer)
                {
                    emailIdTo += reviewerEmail + ",";
                }
                emailIdTo = emailIdTo.TrimEnd(',');

                string emailIdFrom = Convert.ToString(ConfigurationManager.AppSettings["EmailIdFrom"]);
                string subject = "DE Review Reminder";
                string bodyText = "DE Review is scheduled for " + deReviewReminderModel.ProjectName + " on " + deReviewReminderModel.ReviewDate.ToString("dd-MMM-yyyy") + ".";

                Utilities.SendEmails(emailIdFrom, emailIdTo, subject, bodyText);
            }            
        }

        private static void SendMailsForOpenDEReviews()
        {
            ReminderServiceDL reminderServiceDL = new ReminderServiceDL();

            // Reminder to be sent after 1 month if review comments are still open
            List<DEReviewReminderModel> openDEReviews = reminderServiceDL.GetOpenDEReviews();

            if (openDEReviews != null)
            {
                foreach (DEReviewReminderModel deReviewReminderModel in openDEReviews)
                {
                    string emailIdTo = string.Empty;
                    if (deReviewReminderModel.ProjectOwner != null && deReviewReminderModel.ProjectOwner != string.Empty)
                    {
                        emailIdTo = deReviewReminderModel.ProjectOwner + ",";
                    }
                    if (deReviewReminderModel.EM != null && deReviewReminderModel.EM != string.Empty)
                    {
                        emailIdTo += deReviewReminderModel.EM + ",";
                    }

                    emailIdTo = emailIdTo.TrimEnd(',');

                    string emailIdFrom = Convert.ToString(ConfigurationManager.AppSettings["EmailIdFrom"]);
                    string subject = "DE Review comments OPEN";
                    string bodyText = "DE Review comments for the DE Review conducted for " + deReviewReminderModel.ProjectName + " on " + deReviewReminderModel.ReviewDate.ToString("dd-MMM-yyyy") + " are OPEN.";

                    Utilities.SendEmails(emailIdFrom, emailIdTo, subject, bodyText);
                }
            }
        }
        private static void SendMailForAfter2DaysIfNotUpdated()
        {
            ReminderServiceDL reminderServiceDL = new ReminderServiceDL();

            // Reminder to be sent after 1 month if review comments are still open
            List<DEReviewReminderModel> openDEReviews = reminderServiceDL.Get2DayPrior();


            if (openDEReviews != null)
            {
                foreach (DEReviewReminderModel deReviewReminderModel in openDEReviews)
                {
                    string emailIdTo = string.Empty;
                    if (deReviewReminderModel.ProjectOwner != null && deReviewReminderModel.ProjectOwner != string.Empty)
                    {
                        emailIdTo = deReviewReminderModel.ProjectOwner + ",";
                    }

                    emailIdTo = emailIdTo.TrimEnd(',');

                    string emailIdFrom = Convert.ToString(ConfigurationManager.AppSettings["EmailIdFrom"]);
                    string subject = "DE Review Update Necessary";
                    string bodyText = "DE Review necessary for the DE Review conducted for " + deReviewReminderModel.ProjectName + " on " + deReviewReminderModel.ReviewDate.ToString("dd-MMM-yyyy") + " are OPEN.";

                    Utilities.SendEmails(emailIdFrom, emailIdTo, subject, bodyText);
                }
            }
        }

        private static void SendMailsForDEReviewsWithPendingCorrectiveActions()
        {
            ReminderServiceDL reminderServiceDL = new ReminderServiceDL();

            // Reminder to be sent after 1 week of Reviewer Upd dt to PM if he has not updated the corrective actions
            List<DEReviewReminderModel> openDEReviews = reminderServiceDL.GetDEReviewsWithPendingCorrectiveActions();

            if (openDEReviews != null)
            {
                foreach (DEReviewReminderModel deReviewReminderModel in openDEReviews)
                {
                    string emailIdTo = string.Empty;
                    if (deReviewReminderModel.ProjectOwner != null && deReviewReminderModel.ProjectOwner != string.Empty)
                    {
                        emailIdTo = deReviewReminderModel.ProjectOwner + ",";
                    }
                    if (deReviewReminderModel.EM != null && deReviewReminderModel.EM != string.Empty)
                    {
                        emailIdTo += deReviewReminderModel.EM + ",";
                    }

                    emailIdTo = emailIdTo.TrimEnd(',');

                    string emailIdFrom = Convert.ToString(ConfigurationManager.AppSettings["EmailIdFrom"]);
                    string subject = "DE Review Corrective Actions Pending";
                    string bodyText = "DE Review Corrective Actions Pending for the DE Review conducted for " + deReviewReminderModel.ProjectName + " on " + deReviewReminderModel.ReviewDate.ToString("dd-MMM-yyyy");

                    Utilities.SendEmails(emailIdFrom, emailIdTo, subject, bodyText);
                }
            }
        }
    }
}
