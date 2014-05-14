using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class AttributeValuesModel
    {
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; }
        public string AttributeText { get; set; }
        public int? AttributeValueId { get; set; }
    }
}
