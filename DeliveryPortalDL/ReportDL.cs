using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalDL
{
    public class ReportDL
    {
        DashboardEntities _context = new DashboardEntities();

        public DataTable GetDEDashboardDetails(int idp, int accountId, int geoId, int sectorId, int weekId, bool isStrategic, bool isRA,int sectorId2)
        {
            List<GetDEDashboardDetails_Result> dashboardResults = _context.GetDEDashboardDetails(idp, accountId, geoId, sectorId, weekId, isStrategic, isRA,sectorId2).ToList();
            List<GetProjectAttributes_Result> attributeResults = _context.GetProjectAttributes().ToList();

            DataTable dtDashboardDetails = new DataTable();
            dtDashboardDetails.Columns.Add("Project", typeof(System.String));
            dtDashboardDetails.Columns.Add("Prior Week Status", typeof(System.String));
            dtDashboardDetails.Columns.Add("Current Week Status", typeof(System.String));
            dtDashboardDetails.Columns.Add("R AR Since", typeof(System.DateTime));

            // Add attribute names as columns
            foreach (string attributeName in attributeResults.Select(a => a.AttributeName).Distinct().ToList())
            {
                if (attributeName != null)
                {
                    dtDashboardDetails.Columns.Add(" " + attributeName, typeof(System.String));
                }
            }

            dtDashboardDetails.Columns.Add("Latest Updates", typeof(System.String));
            dtDashboardDetails.Columns.Add("Risks", typeof(System.String));
            dtDashboardDetails.Columns.Add("Issues", typeof(System.String));
            dtDashboardDetails.Columns.Add("Corrective Actions", typeof(System.String));

            DataRow drDashboardRow = null;
            List<int> projectIds = new List<int>();

            string geoName = "";

            foreach (GetDEDashboardDetails_Result result in dashboardResults)
            {
                if (! projectIds.Contains(result.ProjectId.Value))
                {
                    if (geoName != result.GeoName)
                    {
                        geoName = result.GeoName;

                        // Add a new separator row
                        drDashboardRow = dtDashboardDetails.NewRow();
                        drDashboardRow["Project"] = geoName;

                        dtDashboardDetails.Rows.Add(drDashboardRow);
                    }                    

                    drDashboardRow = dtDashboardDetails.NewRow();
                    drDashboardRow["Project"] = result.ProjectName + "~~NextLine~~" + result.AccountName + " - " + result.GeoName + " - " + result.SectorName;                    
                    
                    //GetDEDashboardDetails_Result priorWeekRecord = dashboardResults.Where(d => d.ProjectId == result.ProjectId.Value && d.WeekId == result.WeekId - 1).FirstOrDefault();
                    //if (priorWeekRecord != null)
                    //{
                    //    drDashboardRow["Prior Week Status"] = priorWeekRecord.CurrentOverallStatus;
                    //}
                    //else
                    //{
                    //    drDashboardRow["Prior Week Status"] = DBNull.Value;
                    //}

                    if (result.PriorOverallStatus != null)
                    {
                        drDashboardRow["Prior Week Status"] = result.PriorOverallStatus;
                    }
                    else
                    {
                        drDashboardRow["Prior Week Status"] = DBNull.Value;
                    }

                    if (result.CurrentOverallStatus != null)
                    {
                        drDashboardRow["Current Week Status"] = result.CurrentOverallStatus;
                    }
                    else
                    {
                        drDashboardRow["Current Week Status"] = DBNull.Value;
                    }

                    if (result.RARSince != null)
                    {
                        drDashboardRow["R AR Since"] = result.RARSince;
                    }
                    else
                    {
                        drDashboardRow["R AR Since"] = DBNull.Value;
                    }
                    drDashboardRow["Latest Updates"] = result.LatestUpdates;
                    drDashboardRow["Risks"] = result.RiskItems;
                    drDashboardRow["Issues"] = result.IssueItems;
                    drDashboardRow["Corrective Actions"] = result.CorrectiveActions;

                    List<GetProjectAttributes_Result> projectAttributes = attributeResults.Where(r => r.ProjectId == result.ProjectId).ToList().OrderByDescending(o => o.WeeklyStatusId).ToList();

                    //int maxWeekStatusId = 0;

                    if (projectAttributes.FirstOrDefault() != null && projectAttributes.FirstOrDefault().WeeklyStatusId.HasValue)
                    {
                        // Get Max WeeklyStatusId for current project 
                        //maxWeekStatusId = projectAttributes.FirstOrDefault().WeeklyStatusId.Value;

                        //projectAttributes = projectAttributes.Where(p => p.WeeklyStatusId.Value == maxWeekStatusId).ToList();
                        projectAttributes = projectAttributes.Where(p => p.WeekId == weekId).ToList();

                    }
                    foreach (GetProjectAttributes_Result projectAttribute in projectAttributes)
                    {
                        foreach (DataColumn dataColumn in dtDashboardDetails.Columns)
                        {
                            // Get Attribute column name
                            if (dataColumn.ColumnName == " " + projectAttribute.AttributeName)
                            {
                                drDashboardRow[dataColumn.ColumnName] = projectAttribute.FlagName;
                                break;
                            }
                        }
                    }

                    dtDashboardDetails.Rows.Add(drDashboardRow);
                    projectIds.Add(result.ProjectId.Value);
                }

            }
            return dtDashboardDetails;
        }
    }
}
