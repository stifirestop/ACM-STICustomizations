using PX.Data;
using PX.Objects.SO;
using PX.STI.STICustom.DACExt;
using PX.STI.STICustom.DAC.Projection;
using PX.STI.STICustom.Service.Interface;
using System;

namespace PX.STI.STICustom.DACExt.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class BuyerOverrideEventsAttribute : PXEventSubscriberAttribute, IPXFieldUpdatedSubscriber//, IPXRowSelectedSubscriber
    {
        [InjectDependencyOnTypeLevel]
        private protected IPurchaseContactService ContactService { get; set; }

        public void FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs eventHandler)
        {
            SOOrder row = eventHandler.Row as SOOrder;
            if (row is null) return;

            SOOrderExt extendedRow = row.GetExtension<SOOrderExt>();

            string newAttentionValue = (extendedRow.UsrCSROverride is true)
                ? extendedRow.UsrCSRAttention
                : null;

            string newEmailValue = (extendedRow.UsrCSROverride is true)
                ? extendedRow.UsrCSREmail
                : null;

            if (extendedRow.UsrCSROverride is true && newAttentionValue is null && newEmailValue is null)
            {
                STPurchaserContact purchasingContact = ContactService.FindPurchaserContact(cache.Graph, row.CustomerID);
                newAttentionValue = purchasingContact.Attention;
                newEmailValue = purchasingContact.EMail;
            }

            // Saving the values to the hidden fields.
            cache.SetValueExt<SOOrderExt.usrCSRAttention>(row, newAttentionValue);
            cache.SetValueExt<SOOrderExt.usrCSREmail>(row, newEmailValue);

            // Setting the values for the display fields.
            cache.SetDefaultExt<SOOrderExt.usrCSRDisplayAttention>(row);
            cache.SetDefaultExt<SOOrderExt.usrCSRDisplayEmail>(row);
        }
    }
}

