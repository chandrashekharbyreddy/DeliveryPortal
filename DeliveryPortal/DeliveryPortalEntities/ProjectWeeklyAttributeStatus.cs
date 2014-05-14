using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class ProjectWeeklyAttributeStatus
    {
        public int WeeklyStatusId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public int FlagId { get; set; }
        public string FlagName { get; set; }
        public bool IsLevelEditable { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
