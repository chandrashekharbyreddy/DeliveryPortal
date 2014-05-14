using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class QuestionnaireModel
    {
        public int QuestionnaireId { get; set; }
        public int IDPId { get; set; }
        public string QuestionnaireName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int ReviewTypeId { get; set; }
        public string IDPName { get; set; }
        public string ReviewTypeName { get; set; }
    }
}
