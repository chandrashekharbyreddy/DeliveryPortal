using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class DEAttributeModel
    {
        public int DEReviewId { get; set; }
        public string AttributeName { get; set; }
        public string SampleQuestions { get; set; }
        public int AttributeId { get; set; }
        public int? FlagId { get; set; }
        public string Observations { get; set; }
        public string Recommendations { get; set; }
        public string LatestUpdates { get; set; }
        public string CorrectiveActions { get; set; }
        public DateTime? ETA { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Nullable<int> ReviewStatusId { get; set; }
    }
}
