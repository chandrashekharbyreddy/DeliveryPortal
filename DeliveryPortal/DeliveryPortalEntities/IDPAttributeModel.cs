using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class IDPAttributeModel
    {
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public Nullable<System.DateTime> AttributeStartDate { get; set; }
        public Nullable<System.DateTime> AttributeEndDate { get; set; }
        public int AttributeTypeId { get; set; }
        public string AttributeTypeName { get; set; }
        public List<string> AttributeValueStringList { get; set;}
    }
}
