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
    
    public partial class MST_ReviewQuestion
    {
        public int QuestionId { get; set; }
        public int QuestionnaireId { get; set; }
        public string QuestionDescription { get; set; }
        public string QuestionGuideLines { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int AttributeId { get; set; }
    
        public virtual MST_ProjectAttributes MST_ProjectAttributes { get; set; }
        public virtual MST_Questionnaire MST_Questionnaire { get; set; }
    }
}
