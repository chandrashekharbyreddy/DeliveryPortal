using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
namespace DeliveryPortalDL
{
    public class WeeklyStatusDL
    {
        DashboardEntities _context = new DashboardEntities();

        public ProjectWeeklyStatusModel GetWeeklyStatus(int projectId, int weekId, int year, int LevelId, bool isForPrevious = false)
        {
            ProjectWeeklyStatusModel weeklyDashboard = null;

            Tran_Proj_Wkly_Status_SelectAll_Result objweeklyDashboard = new Tran_Proj_Wkly_Status_SelectAll_Result();
            objweeklyDashboard = _context.Tran_Proj_Wkly_Status_SelectAll(projectId, weekId).FirstOrDefault();
            if (objweeklyDashboard != null)
            {
                weeklyDashboard = new ProjectWeeklyStatusModel();
                weeklyDashboard.CurrentOverallStatusId = objweeklyDashboard.CurrentOverallStatusId.HasValue ? objweeklyDashboard.CurrentOverallStatusId.Value : 0;
                weeklyDashboard.ProrOverallStatusId = objweeklyDashboard.PriorOverallStatusId.HasValue ? objweeklyDashboard.PriorOverallStatusId.Value : 0;
                weeklyDashboard.WeeklyStatusId = objweeklyDashboard.WeeklyStatusId.HasValue ? objweeklyDashboard.WeeklyStatusId.Value : 0;
                weeklyDashboard.FlagUpdatedByLevel = objweeklyDashboard.FlagsUpdatedByLevel.HasValue ? objweeklyDashboard.FlagsUpdatedByLevel.Value : 0;
                weeklyDashboard.IssueItems = objweeklyDashboard.IssueItems;
                weeklyDashboard.LatestUpdates = objweeklyDashboard.LatestUpdates;
                weeklyDashboard.CorrectiveActions = objweeklyDashboard.CorrectiveActions;
                weeklyDashboard.RiskItems = objweeklyDashboard.RiskItems;
                weeklyDashboard.WeekStart = objweeklyDashboard.WeekStartDate.HasValue ? objweeklyDashboard.WeekStartDate : null;
                weeklyDashboard.WeekId = objweeklyDashboard.WeekId.HasValue ? objweeklyDashboard.WeekId.Value : 0;
                weeklyDashboard.Year = objweeklyDashboard.Year.HasValue ? objweeklyDashboard.Year.Value : 0;
                
                weeklyDashboard.AttributeStatusValues = _context.Tran_Proj_Wkly_Status_SelectAll(projectId, weekId).Select(x => new ProjectWeeklyAttributeStatus
                {
                    AttributeId = x.AttributeId.HasValue ? x.AttributeId.Value : 0,
                    AttributeName = x.AttributeName,
                    FlagId = x.FlagId.HasValue ? x.FlagId.Value : 0,
                    FlagName = x.FlagName,
                    IsLevelEditable = (LevelId >= weeklyDashboard.FlagUpdatedByLevel)
                }).ToList();
                ProjectWeeklyAttributeStatus objCurrentWeek = new ProjectWeeklyAttributeStatus();
                objCurrentWeek.AttributeId = -1;
                objCurrentWeek.AttributeName = "Week Status";
                objCurrentWeek.FlagId = objweeklyDashboard.CurrentOverallStatusId.HasValue ? objweeklyDashboard.CurrentOverallStatusId.Value : 0;
                objCurrentWeek.FlagName = ((CommonConstants.Flag)objCurrentWeek.FlagId).ToString();
                objCurrentWeek.IsLevelEditable = false;
                
                weeklyDashboard.AttributeStatusValues.Add(objCurrentWeek);
                if (!isForPrevious)
                {
                    ProjectWeeklyAttributeStatus objPriorWeek = new ProjectWeeklyAttributeStatus();
                    objPriorWeek.AttributeId = -1;
                    objPriorWeek.AttributeName = "Prior Week Status";
                    objPriorWeek.FlagId = objweeklyDashboard.PriorOverallStatusId.HasValue ? objweeklyDashboard.PriorOverallStatusId.Value : 0;
                    objPriorWeek.FlagName = ((CommonConstants.Flag)objPriorWeek.FlagId).ToString(); ;
                    objPriorWeek.IsLevelEditable = false;
                    weeklyDashboard.AttributeStatusValues.Add(objPriorWeek);
                }
            }
            return weeklyDashboard;
        }
        public ProjectWeeklyStatusModel GetWeeklyStatusHeader(int projectId, int weekId, int year, int LevelId, bool isForPrevious = false)
        {
            ProjectWeeklyStatusModel weeklyDashboard = null;

            Tran_Proj_Wkly_Status_SelectAll_Result objweeklyDashboard = new Tran_Proj_Wkly_Status_SelectAll_Result();
            for (int i = weekId; i > weekId - 7; i--)
            {
                objweeklyDashboard = _context.Tran_Proj_Wkly_Status_SelectAll(projectId, i).FirstOrDefault();

                if (objweeklyDashboard != null)
                {
                    break;
                }
            }
            

            if (objweeklyDashboard != null)
            {
                weeklyDashboard = new ProjectWeeklyStatusModel();
                
            


                weeklyDashboard.AttributeStatusValues = _context.Tran_Proj_Wkly_Status_SelectAll(projectId, weekId).Select(x => new ProjectWeeklyAttributeStatus
                {
                    AttributeId = x.AttributeId.HasValue ? x.AttributeId.Value : 0,
                    AttributeName = x.AttributeName,
                    FlagId = x.FlagId.HasValue ? x.FlagId.Value : 0,
                    FlagName = x.FlagName,
                    IsLevelEditable = (LevelId >= weeklyDashboard.FlagUpdatedByLevel)
                }).ToList();
                ProjectWeeklyAttributeStatus objCurrentWeek = new ProjectWeeklyAttributeStatus();
                objCurrentWeek.AttributeId = -1;
                objCurrentWeek.AttributeName = "Week Status";
                objCurrentWeek.FlagId = objweeklyDashboard.CurrentOverallStatusId.HasValue ? objweeklyDashboard.CurrentOverallStatusId.Value : 0;
                objCurrentWeek.FlagName = ((CommonConstants.Flag)objCurrentWeek.FlagId).ToString();
                objCurrentWeek.IsLevelEditable = false;

                weeklyDashboard.AttributeStatusValues.Add(objCurrentWeek);
                if (!isForPrevious)
                {
                    ProjectWeeklyAttributeStatus objPriorWeek = new ProjectWeeklyAttributeStatus();
                    objPriorWeek.AttributeId = -1;
                    objPriorWeek.AttributeName = "Prior Week Status";
                    objPriorWeek.FlagId = objweeklyDashboard.PriorOverallStatusId.HasValue ? objweeklyDashboard.PriorOverallStatusId.Value : 0;
                    objPriorWeek.FlagName = ((CommonConstants.Flag)objPriorWeek.FlagId).ToString(); ;
                    objPriorWeek.IsLevelEditable = false;
                    weeklyDashboard.AttributeStatusValues.Add(objPriorWeek);
                }
            }
            return weeklyDashboard;
        }
        //public List<ProjectWeeklyStatusModel>  GetWeeklyStatusList(int projectId, int weekId, int year, int LevelId)
        //{
        //    ProjectWeeklyStatusModel weeklyDashboard = new ProjectWeeklyStatusModel();
        //    List<ProjectWeeklyStatusModel> weeklyDashboardList = new  List<ProjectWeeklyStatusModel>();
        //    List<Vw_DashboardView> objWeeklyDashboard = new List<Vw_DashboardView>();
        //    //Tran_Proj_Wkly_Status_SelectAll_Result objweeklyDashboard = new Tran_Proj_Wkly_Status_SelectAll_Result();


        //    objWeeklyDashboard = _context.Vw_DashboardView.Where(dv => dv.ProjectId == projectId ).Select(dv => dv).ToList();
        //    var groupBy = objWeeklyDashboard.GroupBy(x => x.WeekId, x => x, (key, g) => new { weekId = key, Dashboard = g.ToList() });



        //    //if (objWeeklyDashboard != null)
        //    //{
        //    //    weeklyDashboard = new ProjectWeeklyStatusModel();
        //    //    weeklyDashboard.WeeklyStatusId = objWeeklyDashboard.WeeklyStatusId.HasValue ? objWeeklyDashboard.WeeklyStatusId.Value : 0;
        //    //    weeklyDashboard.FlagUpdatedByLevel = objWeeklyDashboard.FlagsUpdatedByLevel.HasValue ? objWeeklyDashboard.FlagsUpdatedByLevel.Value : 0;
        //    //    weeklyDashboard.IssueItems = objweeklyDashboard.IssueItems;
        //    //    weeklyDashboard.LatestUpdates = objweeklyDashboard.LatestUpdates;
        //    //    weeklyDashboard.CorrectiveActions = objweeklyDashboard.CorrectiveActions;
        //    //    weeklyDashboard.RiskItems = objweeklyDashboard.RiskItems;
        //    //    weeklyDashboard.WeekStart = objweeklyDashboard.WeekStartDate.HasValue ? objweeklyDashboard.WeekStartDate : null;
        //    //    weeklyDashboard.WeekId = objweeklyDashboard.WeekId.HasValue ? objweeklyDashboard.WeekId.Value : 0;
        //    //    weeklyDashboard.Year = objweeklyDashboard.Year.HasValue ? objweeklyDashboard.Year.Value : 0;
        //    //    weeklyDashboard.AttributeStatusValues = _context.Tran_Proj_Wkly_Status_SelectAll(projectId, weekId).Select(x => new ProjectWeeklyAttributeStatus
        //    //    {
        //    //        AttributeId = x.AttributeId.HasValue ? x.AttributeId.Value : 0,
        //    //        AttributeName = x.AttributeName,
        //    //        FlagId = x.FlagId.HasValue ? x.FlagId.Value : 0,
        //    //        FlagName = x.FlagName,
        //    //        IsLevelEditable = (LevelId >= weeklyDashboard.FlagUpdatedByLevel)
        //    //    }).ToList();
        //    //}
        //    return weeklyDashboardList;
        //}

        public List<FlagModel> GetFlags()
        {

            List<FlagModel> flags = _context.MST_Flags.Select(f => new FlagModel { FlagId = f.FlagId, FlagName = f.FlagName }).ToList();

            return flags;
        }

        public List<ProjectModel> GetProjects(int accountId)
        {
            List<ProjectModel> projects = null;

            if (accountId != 0)
                projects = _context.MST_Project.Where(p => p.AccountId == accountId).Select(g => new ProjectModel { ProjectId = g.ProjectId, ProjectName = g.ProjectName }).ToList<ProjectModel>();
            return projects;
        }
        public List<AccountModel> GetAccounts()
        {
            List<AccountModel> accounts = null;

            accounts = _context.MST_Account.Select(g => new AccountModel { AccountId = g.AccountId, AccountName = g.AccountName }).ToList<AccountModel>();
            return accounts;
        }
        //public ProjectWeeklyStatusModel GetWeeklyAttribute(int WeeklyStatusId)
        //{
        //    ProjectWeeklyStatusModel weeklyDashboard = null;
        //    DashboardEntities context = new DashboardEntities();
        //    var objweeklyDashboard = context.Tran_Proj_Wkly_Status_SelectAll().Where(x => x.WeeklyStatusId == WeeklyStatusId).FirstOrDefault();
        //    weeklyDashboard.IssueItems = objweeklyDashboard.IssueItems;
        //    weeklyDashboard.CorrectiveActions = objweeklyDashboard.CorrectiveActions;
        //    weeklyDashboard.RiskItems = objweeklyDashboard.RiskItems;
        //    weeklyDashboard.WeekStart = objweeklyDashboard.WeekStartDate.HasValue ? objweeklyDashboard.WeekStartDate : null;
        //    weeklyDashboard.WeekId = objweeklyDashboard.WeekId.HasValue ? objweeklyDashboard.WeekId.Value : 0;
        //    weeklyDashboard.Year = objweeklyDashboard.Year.HasValue ? objweeklyDashboard.Year.Value : 0;

        //    return weeklyDashboard;
        //}

        public int SaveWeeklyDashboard(ProjectWeeklyStatusModel objStatus)
        {
            Tran_Proj_Wkly_Status objNewWeeklyStatus;
            if (objStatus.WeeklyStatusId != 0)
            {
                objNewWeeklyStatus = _context.Tran_Proj_Wkly_Status.Where(t => t.WeeklyStatusId == objStatus.WeeklyStatusId).FirstOrDefault();
            }
            else
            {
                objNewWeeklyStatus = new Tran_Proj_Wkly_Status();
                objNewWeeklyStatus.WeeklyStatusId = objStatus.WeeklyStatusId;

            }

            objNewWeeklyStatus.CorrectiveActions = objStatus.CorrectiveActions;
            objNewWeeklyStatus.IssueItems = objStatus.IssueItems;
            objNewWeeklyStatus.LatestUpdates = objStatus.LatestUpdates;
            objNewWeeklyStatus.RiskItems = objStatus.RiskItems;
            objNewWeeklyStatus.FlagsUpdatedByLevel = objStatus.FlagUpdatedByLevel;
            objNewWeeklyStatus.WeekId = objStatus.WeekId;
            objNewWeeklyStatus.Year = objStatus.Year;
            if (objStatus.WeekStart.HasValue)
                objNewWeeklyStatus.WeekStartDate = objStatus.WeekStart.Value;
            objNewWeeklyStatus.ProjectId = objStatus.ProjectId;
            objNewWeeklyStatus.PriorOverallStatusId = 1;
            objNewWeeklyStatus.LastUpdatedBy = objStatus.LastUpdatedBy;
            if (objStatus.AttributeStatusValues.Count > 0)
            {
                objNewWeeklyStatus.Tran_Proj_Wkly_Attributes_Status.Clear();

            }

            int currentstatusId = 0; // Red
            foreach (var item in objStatus.AttributeStatusValues)
            {
                Tran_Proj_Wkly_Attributes_Status objNewStatus = new Tran_Proj_Wkly_Attributes_Status();
                objNewStatus.AttributeId = item.AttributeId;
                objNewStatus.FlagId = item.FlagId;
                objNewStatus.LastUpdatedDate = DateTime.Now;
                objNewStatus.WeeklyStatusId = item.WeeklyStatusId;
                objNewStatus.LastUpdatedBy = objStatus.LastUpdatedBy;
                objNewWeeklyStatus.Tran_Proj_Wkly_Attributes_Status.Add(objNewStatus);
                if (currentstatusId == 0)
                    currentstatusId = objNewStatus.FlagId.HasValue ? objNewStatus.FlagId.Value : 0;
                else if (currentstatusId > objNewStatus.FlagId)  // 1 = Red, 2 = Amber, 3 = Green
                    currentstatusId = objNewStatus.FlagId.HasValue ? objNewStatus.FlagId.Value : 0;
            }

            objNewWeeklyStatus.CurrentOverallStatusId = currentstatusId;

            if (objStatus.AttributeStatusValues.Count == 0)
            {
                throw new Exception("Please select RAG status for atleast one attribute.");
            }

            if (objStatus.WeeklyStatusId == 0)
            {
                _context.Tran_Proj_Wkly_Status.Add(objNewWeeklyStatus);
            }

            _context.SaveChanges();
            return objNewWeeklyStatus.WeeklyStatusId;

        }
        public List<ReviewStatusModel> GetReviewStatus()
        {
            List<ReviewStatusModel> rvStatus = null;
            rvStatus = _context.MST_ReviewStatus.Select(a => new ReviewStatusModel { ReviewStatusId = a.ReviewStatusId, ReviewStatusName = a.ReviewStatusName, ReviewStatusCode = a.ReviewStatusCode }).ToList<ReviewStatusModel>();
            return rvStatus;
        }
    }
}
