using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryPortalEntities;

namespace DeliveryPortalDL
{
    public class IDPDL
    {
        DashboardEntities _context = new DashboardEntities();

        public List<IDPModel> GetIDPs()
        {
            List<IDPModel> idp = null;
            idp = _context.MST_IDP.Select(i => new IDPModel { IdpId = i.IDPId, IdpName = i.IDPName }).ToList<IDPModel>();
            return idp;
        }

        public List<IDPAttributesModel> GetAttributesList()
        {
            List<IDPAttributesModel> attributes = new List<IDPAttributesModel>();
            attributes = _context.MST_Attributes.Select(a => new IDPAttributesModel { AttributeId= a.AttributeId, AttributeName = a.AttributeName, AttributeStartDate = a.AttributeStartDate, AttributeEndDate = a.AttributeEndDate }).ToList();
            return attributes;
        }

        public void SetIDPAttributes(List<IDPAttributesMappingsModel> idpAttributesMappings, int idpId)
        {
            List<Tran_IDP_Attributes> idpAttributes = _context.Tran_IDP_Attributes.Where(i => i.IDPId == idpId).ToList();
            foreach(Tran_IDP_Attributes idpAttribute in idpAttributes)
            {
                _context.Tran_IDP_Attributes.Remove(idpAttribute);
            }
            
            foreach (IDPAttributesMappingsModel idpAttribute in idpAttributesMappings)
            {
                Tran_IDP_Attributes tranIdpAttribute = new Tran_IDP_Attributes();
                tranIdpAttribute.IDPId = idpAttribute.IDPId;
                tranIdpAttribute.AttributeId = idpAttribute.AttributeId;
                _context.Tran_IDP_Attributes.Add(tranIdpAttribute);
            }

            _context.SaveChanges();
        }

        public List<IDPAttributesMappingsModel> GetIDPAttributes(int idpId)
        {
            List<IDPAttributesMappingsModel> idpAttributesMappingsModel = new List<IDPAttributesMappingsModel>();
            idpAttributesMappingsModel = _context.Tran_IDP_Attributes.Select(t => new IDPAttributesMappingsModel { AttributeId = t.AttributeId, IDPId = t.IDPId, IDPAttributeId = t.IDPAttributeId }).Where(t => t.IDPId == idpId).ToList();            
            return idpAttributesMappingsModel;
        }
    }
}
