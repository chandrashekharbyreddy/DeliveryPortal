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
    
    public partial class Tran_Proj_ProjCode_Details
    {
        public int ProjectTranId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectCodeId { get; set; }
    
        public virtual MST_Project MST_Project { get; set; }
        public virtual MST_ProjectCodes MST_ProjectCodes { get; set; }
    }
}