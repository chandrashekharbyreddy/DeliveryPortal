//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeliveryPortalDL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tran_Proj_IDP_Attributes
    {
        public int ProjectIDPAttributeId { get; set; }
        public int ProjectId { get; set; }
        public int AttributeId { get; set; }
        public Nullable<int> AttributeValueId { get; set; }
        public string AttributeTextValue { get; set; }
        public int LastUpdatedBy { get; set; }
        public System.DateTime LastUpdateDate { get; set; }
    
        public virtual MST_Attributes MST_Attributes { get; set; }
        public virtual MST_Project MST_Project { get; set; }
    }
}
