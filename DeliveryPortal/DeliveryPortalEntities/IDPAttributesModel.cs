using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class IDPAttributesModel
    {
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public DateTime? AttributeStartDate { get; set; }
        public DateTime? AttributeEndDate { get; set; }
        public int AttributeTypeId { get; set; }
    }
}
