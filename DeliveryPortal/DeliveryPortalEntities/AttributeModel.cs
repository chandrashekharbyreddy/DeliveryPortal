using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class AttributeModel
    {
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public int? ParentAttributeId { get; set; }
        public bool? IsDE { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> EffectiveStartDate { get; set; }
        public Nullable<System.DateTime> EffectiveEndDate { get; set; }
        public string SampleQuestions { get; set; }
        public Nullable<int> LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    }
}
