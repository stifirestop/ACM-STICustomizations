using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.SO;
using PX.STI.STICustom.DACExt;
using PX.STI.STICustom.DAC.Projection;
using PX.STI.STICustom.Service.Interface;

namespace PX.STI.STICustom.Service
{
    public class PurchaseContactService : IPurchaseContactService
    {
        public virtual string FindBuyerAttentionForOrder(PXCache cache, SOOrder record)
        {
            string result = null;

            if (record != null)
            {
                SOOrderExt extendedRecord = record.GetExtension<SOOrderExt>();
                STPurchaserContact purchasingContact = null;

                using (new PXConnectionScope())
                {
                    purchasingContact = SelectFrom<STPurchaserContact>
                        .Where<STPurchaserContact.bAccountID.IsEqual<@P.AsInt>>
                        .View.Select(cache.Graph, record.CustomerID ?? 0);
                }

                result = (extendedRecord.UsrCSROverride ?? false)
                    ? extendedRecord.UsrCSRAttention
                    : purchasingContact?.Attention;
            }

            return result;
        }

        public virtual string FindBuyerEmailForOrder(PXCache cache, SOOrder record)
        {
            string result = null;

            if (record != null)
            {
                SOOrderExt extendedRecord = record.GetExtension<SOOrderExt>();
                STPurchaserContact purchasingContact = null;

                using (new PXConnectionScope())
                {
                    purchasingContact = SelectFrom<STPurchaserContact>
                        .Where<STPurchaserContact.bAccountID.IsEqual<@P.AsInt>>
                        .View.Select(cache.Graph, record.CustomerID ?? 0);
                }

                result = (extendedRecord.UsrCSROverride ?? false)
                    ? extendedRecord.UsrCSREmail
                    : purchasingContact?.EMail;
            }

            return result;
        }

        public virtual STPurchaserContact FindPurchaserContact(PXGraph graph, int? customerID)
        {
            STPurchaserContact result = null;

            if (graph != null && customerID != null)
            {
                using (new PXConnectionScope())
                {
                    result = SelectFrom<STPurchaserContact>
                        .Where<STPurchaserContact.bAccountID.IsEqual<@P.AsInt>>
                        .View.Select(graph, customerID);
                }
            }

            return result;
        }
    }
}

