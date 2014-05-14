using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class TranProjIDPAttributesModel
    {
        //public int ProjectIDPAttributeId { get; set; }
        public int ProjectId { get; set; }
        public int AttributeId { get; set; }
        public int? AttributeValueId { get; set; }
        public string AttributeTextValue { get; set; }
        public int LastUpdatedBy { get; set; }
        public System.DateTime LastUpdateDate { get; set; }
    }
}
