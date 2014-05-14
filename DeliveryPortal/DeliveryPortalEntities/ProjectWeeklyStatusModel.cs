using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class ProjectWeeklyStatusModel
    {
        public int WeeklyStatusId { get; set; }
        public int WeekId { get; set; }
        public DateTime? WeekStart { get; set; }
        public int Year { get; set; }
        public int ProjectId { get; set; }
        public int ProrOverallStatusId { get; set; }
        public int CurrentOverallStatusId { get; set; }
        public string LatestUpdates { get; set; }
        public string RiskItems { get; set; }
        public string IssueItems { get; set; }
        public string CorrectiveActions { get; set; }
        public int OverrideStatusId { get; set; }
        public int FlagUpdatedByLevel { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        private List<ProjectWeeklyAttributeStatus> _attributeStatus;

        public List<ProjectWeeklyAttributeStatus> AttributeStatusValues
        {
            get { if (_attributeStatus != null) return _attributeStatus; else { _attributeStatus = new List<ProjectWeeklyAttributeStatus>(); return _attributeStatus; } }
            set { _attributeStatus = value; }
        }
        
    }
}
