using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DashboardReminderService
{
    static class serviceStartup
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new DashboardReminderService() 
            };
            ServiceBase.Run(ServicesToRun);

        }

        //starts here ... to be removed later

        //public void SendReminderMails()
        //{
        //    int year = System.DateTime.Now.Year;
        //    CultureInfo ciCurr = CultureInfo.CurrentCulture;
        //    int weekNum = ciCurr.Calendar.GetWeekOfYear(System.DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);



        //    List<int?> result = (from a in _context.MST_Project
        //                         where !_context.Tran_Proj_Wkly_Status.Any(p => p.ProjectId == a.ProjectId && (p.Year != year && p.WeekId != weekNum))
        //                         select a.PMId).ToList<int?>();

        //    var final = (from MST_Employee a in _context.MST_Employee
        //                 where result.Contains<int?>(a.EmployeeId)
        //                 select new
        //                 {
        //                     a.EmailId
        //                 }).ToList();

        //    string emailIdList = string.Empty;
        //    foreach (var item in final)
        //    {

        //        emailIdList = emailIdList + item.EmailId.ToString() + ","; //String.Join(item.EmailId.ToString(), ";");           
        //    }


        //    emailIdList = emailIdList.TrimEnd(',');

        //    //grdView.DataSource = final.ToList();
        //    //grdView.DataBind();

        //    string smtpAddress = "smtp.mail.yahoo.com";
        //    int portNumber = 587;
        //    bool enableSSL = true;

        //    string emailFrom = "kamal.gurnani@capgemini.com";
        //    string password = "abcdefg";
        //    string emailTo = emailIdList;
        //    string subject = "Weekly Update";
        //    string body = "Please provide the weekly status of the project";

        //    using (MailMessage mail = new MailMessage())
        //    {
        //        mail.From = new MailAddress(emailFrom);
        //        mail.To.Add(emailTo);

        //        mail.Subject = subject;
        //        mail.Body = body;
        //        mail.IsBodyHtml = true;

        //        // Can set to false, if you are sending pure text.

        //        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
        //        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

        //        //using (SmtpClient smtp = new SmtpClient("127.0.0.1"))
        //        //{


        //        //   // smtp.EnableSsl = enableSSL;
        //        //    smtp.Send(mail);
        //        //}
        //        SmtpClient client = new SmtpClient();
        //        client.Send(mail);

        //    }


        //}
        //ends here
    }
}


