using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalDL
{
    public class ReminderServiceDL
    {
        DashboardEntities _context = new DashboardEntities();

        /// <summary>
        /// Reminder to be sent to Project Owner, EM and Reviewer one day prior to the review date and on the day of review date.
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public List<DEReviewReminderModel> GetUpcomingDEReviewsReminder()
        {
            List<DEReviewReminderModel> deReviewsUpcoming = new List<DEReviewReminderModel>();
            DateTime today = DateTime.Now;
            DateTime tomorrow = DateTime.Now.AddDays(1);
            List<Tran_DE_Calendar> deReviews = _context.Tran_DE_Calendar.Where(t => EntityFunctions.TruncateTime(t.ReviewDate) == EntityFunctions.TruncateTime(today)
                                                                                 || EntityFunctions.TruncateTime(t.ReviewDate) == EntityFunctions.TruncateTime(tomorrow)).ToList();
            MST_Project project = null;

            foreach (Tran_DE_Calendar deReview in deReviews)
            {
                DEReviewReminderModel deReviewReminder = new DEReviewReminderModel();
                deReviewReminder.ReviewDate = deReview.ReviewDate;
                deReviewReminder.Reviewer = new List<string>();
                deReviewReminder.Reviewer.Add(GetEmployeeEmailAddress(deReview.EmployeeId));
                project = _context.MST_Project.Where(p => p.ProjectId == deReview.ProjectId).FirstOrDefault();
                if (project != null)
                {
                    deReviewReminder.ProjectOwner = project.PMId.HasValue ? GetEmployeeEmailAddress(project.PMId.Value) : "";
                    deReviewReminder.EM = project.EMId.HasValue ? GetEmployeeEmailAddress(project.EMId.Value) : "";
                    deReviewReminder.ProjectName = project.ProjectName;
                }
                deReviewsUpcoming.Add(deReviewReminder);
            }
            return deReviewsUpcoming;
        }

        //public List<DEReviewReminderModel> Get2DayPrior()
        //{
        //    List<DEReviewReminderModel> de2Day = new List<DEReviewReminderModel>();
        //   // DateTime today = DateTime.Now;
        //    DateTime BeforeTwoDays = DateTime.Now.AddDays(-2);
        //    List<Tran_DE_Calendar> deReviews = _context.Tran_DE_Calendar.Where(t => EntityFunctions.TruncateTime(t.ReviewDate) < EntityFunctions.TruncateTime(BeforeTwoDays)).ToList();
        //    Tran_DE_Calendar projectId = null;

        //    foreach (Tran_DE_Calendar deReview in deReviews)
        //    {
        //       projectId =_context.Tran_DE_Calendar.Where(p=>p.ProjectId==deReview.ProjectId);
        //        //DEReviewReminderModel deReviewReminder = new DEReviewReminderModel();
        //        // deReviewReminder.Reviewer = new List<string>();
        //        //deReviewReminder.Reviewer.Add(GetEmployeeEmailAddress(deReview.EmployeeId));
        //        //project = _context.Tran_DE_Calendar.Where(p => p.ProjectId == deReview.ProjectId).FirstOrDefault();
        //        //if (project != null)
        //        {


        //}


        public string GetEmployeeEmailAddress(int employeeId)
        {
            string emailAddress = null;
            MST_Employee employee = _context.MST_Employee.Where(e => e.EmployeeId == employeeId).FirstOrDefault();
            if (employee != null)
            {
                emailAddress = employee.EmailId;
            }
            return emailAddress;
        }

        /// <summary>
        /// Reminder to be sent after 1 month if review comments are still open
        /// </summary>
        /// <returns></returns>
        /// 
        public List<DEReviewReminderModel> Get2DayPrior()
        {
            //List<DEReviewReminderModel> deReviewReminderModel = new List<DEReviewReminderModel>();
            //List<DECalendarModel> ProjectIdAndReviewDate = GetObjectOfDeCalendarModel();

            DateTime BeforeTwoDays = DateTime.Now.AddDays(-2);
            List<DEReviewReminderModel> deReviewReminderModel = _context.Tran_DE_Calendar.Where(t => EntityFunctions.TruncateTime(t.ReviewDate) <= EntityFunctions.TruncateTime(BeforeTwoDays) && t.Tran_Proj_DE_Review.Count() == 0)
                .Select(proj =>
                new DEReviewReminderModel { ProjectName = proj.MST_Project.ProjectName, ReviewDate = proj.ReviewDate, ProjectOwner = proj.MST_Employee.EmailId }).ToList();

            //foreach (DECalendarModel deCalendar in ProjectIdAndReviewDate)
            //{


            //    int count = _context.Tran_Proj_DE_Review.Where(t => t.DEReviewCalendarId == deCalendar.DEReviewCalendarId).Count();
            //    if (count == 0)
            //    {
            //        DEReviewReminderModel deReviewModel = new DEReviewReminderModel();
            //        deReviewModel.ProjectName = deCalendar.ProjectName;
            //        deReviewModel.ReviewDate = deCalendar.ReviewDate;
            //        deReviewModel.ProjectOwner = deCalendar.ReviewerName;
            //        //deReviewModel.EM =

            //        deReviewReminderModel.Add(deReviewModel);
            //    }
            //}
            return deReviewReminderModel;

        }

        public List<DEReviewReminderModel> GetOpenDEReviews()
        {
            List<int> projectIds = GetProjectIdsForDEReviewsOneMonthBefore();

            List<DEReviewModel> deReviews = GetDEReviewIds(projectIds);

            List<DEReviewReminderModel> deReviewsOpen = null;

            int completeReviewStatusId = -1;

            MST_ReviewStatus completeReviewStatus = _context.MST_ReviewStatus.Where(r => r.ReviewStatusCode == "CMPL").FirstOrDefault();
            if (completeReviewStatus != null)
            {
                completeReviewStatusId = completeReviewStatus.ReviewStatusId;
            }

            if (deReviews != null && deReviews.Count > 0)
            {
                deReviewsOpen = new List<DEReviewReminderModel>();
                foreach (DEReviewModel deReview in deReviews)
                {
                    Tran_Proj_DE_Attibute projectDEAttribute = _context.Tran_Proj_DE_Attibute.Where(p => p.DEReviewId == deReview.DEReviewId && p.ReviewStatusId != completeReviewStatusId).FirstOrDefault();
                    if (projectDEAttribute != null)
                    {
                        DEReviewReminderModel deReviewReminder = new DEReviewReminderModel();
                        MST_Project project = _context.MST_Project.Where(p => p.ProjectId == deReview.ProjectId).FirstOrDefault();
                        if (project != null)
                        {
                            if (project.PMId.HasValue)
                            {
                                deReviewReminder.ProjectOwner = GetEmployeeEmailAddress(project.PMId.Value);
                            }
                            if (project.EMId.HasValue)
                            {
                                deReviewReminder.EM = GetEmployeeEmailAddress(project.EMId.Value);
                            }
                            deReviewReminder.ProjectName = project.ProjectName;
                            if (deReview.ReviewDate.HasValue)
                            {
                                deReviewReminder.ReviewDate = deReview.ReviewDate.Value;
                            }
                            deReviewsOpen.Add(deReviewReminder);
                        }

                    }
                }
            }

            return deReviewsOpen;
        }

        private List<int> GetProjectIdsForDEReviewsOneMonthBefore()
        {
            DateTime dateBeforeOneMonth = DateTime.Now.AddMonths(-1).Date;
            List<Tran_DE_Calendar> projects = _context.Tran_DE_Calendar.Where(t => EntityFunctions.TruncateTime(t.ReviewDate) <= EntityFunctions.TruncateTime(dateBeforeOneMonth)).ToList();
            List<int> projectIds = new List<int>();
            if (projects != null && projects.Count > 0)
            {
                foreach (Tran_DE_Calendar deCalendar in projects)
                {
                    projectIds.Add(deCalendar.ProjectId);
                }
            }
            return projectIds;
        }
        private List<DECalendarModel> GetObjectOfDeCalendarModel()
        {

            DateTime BeforeTwoDays = DateTime.Now.AddDays(-2);
            List<DECalendarModel> decalendarmodel = _context.Tran_DE_Calendar.Where(t => EntityFunctions.TruncateTime(t.ReviewDate) <= EntityFunctions.TruncateTime(BeforeTwoDays) && t.Tran_Proj_DE_Review.Count() == 0)
                .Select(proj =>
                new DECalendarModel { DEReviewCalendarId = proj.DEReviewCalendarId, ReviewDate = proj.ReviewDate, ProjectName = proj.MST_Project.ProjectName, ReviewerName = proj.MST_Employee.EmailId }).ToList();
            // DECalendarModel calendarModel = null;
            //if (projects != null && projects.Count > 0)
            //{
            //    decalendarmodel = new List<DECalendarModel>();
            //    foreach(Tran_DE_Calendar deCalendar in projects)
            //    {
            //        calendarModel = new DECalendarModel();
            //        calendarModel.ProjectId = deCalendar.ProjectId;
            //        calendarModel.ReviewDate = deCalendar.ReviewDate;
            //        decalendarmodel.Add(calendarModel);
            //    }
            // }
            return decalendarmodel;
        }

        private List<DEReviewModel> GetDEReviewIds(List<int> projectIds)
        {
            List<DEReviewModel> deReviews = null;
            if (projectIds != null && projectIds.Count > 0)
            {
                deReviews = new List<DEReviewModel>();
                foreach (int projectId in projectIds)
                {
                    List<Tran_Proj_DE_Review> projectDEReviews = _context.Tran_Proj_DE_Review.Where(t => t.ProjectId == projectId).ToList();
                    if (projectDEReviews != null)
                    {
                        foreach (Tran_Proj_DE_Review projectDEReview in projectDEReviews)
                        {
                            deReviews.Add(new DEReviewModel { DEReviewId = projectDEReview.DEReviewId, ProjectId = projectDEReview.ProjectId, ReviewDate = projectDEReview.ReviewDate });
                        }
                    }
                }

            }
            return deReviews;
        }

        /// <summary>
        /// Reminder to be sent after 1 week of Reviewer Upd dt to PM if he has not updated the corrective actions
        /// </summary>
        /// <returns></returns>
        public List<DEReviewReminderModel> GetDEReviewsWithPendingCorrectiveActions()
        {
            DEReviewReminderModel deReviewReminder = null;
            List<DEReviewReminderModel> deReviewsWithPendingActions = null;
            DateTime dateBeforeOneWeek = DateTime.Now.AddDays(-7).Date;

            // This is the list of reviews for which reviewers have created an entry for the DE Reviews held.
            List<Tran_Proj_DE_Review> deReviews = _context.Tran_Proj_DE_Review.Where(d => EntityFunctions.TruncateTime(d.ReviewDate) <= dateBeforeOneWeek).ToList();

            if (deReviews != null && deReviews.Count > 0)
            {
                deReviewsWithPendingActions = new List<DEReviewReminderModel>();
                foreach (Tran_Proj_DE_Review deReview in deReviews)
                {
                    // Check for all the attributes for each DEReviewId
                    List<Tran_Proj_DE_Attibute> deAttributes = _context.Tran_Proj_DE_Attibute.Where(d => d.DEReviewId == deReview.DEReviewId).ToList();

                    if (deAttributes != null && deAttributes.Where(d => d.CorrectiveActions == null || d.CorrectiveActions == string.Empty).Count() > 0)
                    {
                        deReviewReminder = new DEReviewReminderModel();
                        MST_Project project = _context.MST_Project.Where(p => p.ProjectId == deReview.ProjectId).FirstOrDefault();
                        if (project != null)
                        {
                            if (project.PMId.HasValue)
                            {
                                deReviewReminder.ProjectOwner = GetEmployeeEmailAddress(project.PMId.Value);
                            }
                            if (project.EMId.HasValue)
                            {
                                deReviewReminder.EM = GetEmployeeEmailAddress(project.EMId.Value);
                            }
                            deReviewReminder.ProjectName = project.ProjectName;
                            if (deReview.ReviewDate.HasValue)
                            {
                                deReviewReminder.ReviewDate = deReview.ReviewDate.Value;
                            }
                            deReviewsWithPendingActions.Add(deReviewReminder);
                        }
                    }
                }
            }
            return deReviewsWithPendingActions;
        }
        public List<FunctionalityModel> GetEmailConfigurationList()
        {
            //List<EmailConfiguration> emailConfig = null;
            List<FunctionalityModel> functionality = _context.MST_Functionality.Select(p => new FunctionalityModel
            {
                FunctionalityId = p.FunctionalityId,
                FunctionalityCode = p.FunctionalityCode,
                FunctionalityName = p.FunctionalityName

            }).ToList();
            //List<Functionality> functionality = _context.MST_Functionality.Select(p => new Functionality
            //{
            //    FunctionalityId = p.FunctionalityId,
            //    FunctionalityCode = p.FunctionalityCode,
            //    FunctionalityName = p.FunctionalityName,
            //    EmailIdList = p.Tran_EmailConfiguration.Select(e => e.MST_Employee.EmailId).ToList()
            //}).ToList();
            foreach (FunctionalityModel func in functionality)
            {
                StringBuilder stringbuilder = new StringBuilder();
                List<EmailConfigurationModel> emailConfig = _context.Tran_EmailConfiguration.Where(p => p.FunctionalityId == func.FunctionalityId).Select(p => new EmailConfigurationModel { EmailConfigId = p.EmailConfigId, EmailIds = p.MST_Employee.EmailId, FunctionalityId = p.FunctionalityId.Value }).ToList();
                foreach (EmailConfigurationModel eConfig in emailConfig)
                {
                    //emailConfig.Add(new EmailConfiguration { EmailConfigId = temailconfig.EmailConfigId, EmployeeId = temailconfig.EmployeeId, FunctionalityId = temailconfig.FunctionalityId });
                    //EmployeeModel emp = _context.MST_Employee.Where(e => e.EmployeeId == eConfig.EmployeeId).Select(empmodel => new EmployeeModel { EmployeeId = empmodel.EmployeeId, EmailId = empmodel.EmailId }).FirstOrDefault();
                    //if (string.IsNullOrEmpty(emp.EmailId))
                    //if(emp != null)
                    //{
                    stringbuilder.Append(eConfig.EmailIds);
                    //}
                    stringbuilder.Append(", ");
                }
                func.EmailIds = stringbuilder.ToString().Trim(' ').Trim(',');

            }
            return functionality;
        }
        public string GetEmailIdList(string funcCode) 
        {
            FunctionalityModel func = _context.MST_Functionality.Where(p => p.FunctionalityCode == funcCode).Select(p => new FunctionalityModel { FunctionalityId = p.FunctionalityId, FunctionalityCode = p.FunctionalityCode }).FirstOrDefault();
            
            StringBuilder stringbuilder = new StringBuilder();
            List<EmailConfigurationModel> emailConfig = _context.Tran_EmailConfiguration.Where(p => p.FunctionalityId == func.FunctionalityId).Select(p => new EmailConfigurationModel { EmailConfigId = p.EmailConfigId, EmailIds = p.MST_Employee.EmailId, FunctionalityId = p.FunctionalityId.Value }).ToList();
            foreach (EmailConfigurationModel eConfig in emailConfig)
            {
                //emailConfig.Add(new EmailConfiguration { EmailConfigId = temailconfig.EmailConfigId, EmployeeId = temailconfig.EmployeeId, FunctionalityId = temailconfig.FunctionalityId });
                //EmployeeModel emp = _context.MST_Employee.Where(e => e.EmployeeId == eConfig.EmployeeId).Select(empmodel => new EmployeeModel { EmployeeId = empmodel.EmployeeId, EmailId = empmodel.EmailId }).FirstOrDefault();
                //if (string.IsNullOrEmpty(emp.EmailId))
                //if(emp != null)
                //{
                stringbuilder.Append(eConfig.EmailIds);
                //}
                stringbuilder.Append(", ");
            }
            func.EmailIds = stringbuilder.ToString().Trim(' ').Trim(',');
            return func.EmailIds;
        }

    }
}
