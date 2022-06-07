using PX.Data;
using PX.Objects.SO;
using PX.STI.STICustom.DAC.Projection;

namespace PX.STI.STICustom.Service.Interface
{
    public interface IPurchaseContactService
    {
        string FindBuyerAttentionForOrder(PXCache cache, SOOrder record);
        string FindBuyerEmailForOrder(PXCache cache, SOOrder record);
        STPurchaserContact FindPurchaserContact(PXGraph graph, int? customerID);
    }
}
