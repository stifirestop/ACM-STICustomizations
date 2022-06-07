using PX.Data;
using PX.Objects.SO;
using PX.STI.STICustom.DACExt;
using PX.STI.STICustom.Service.Interface;
using System;

namespace PX.STI.STICustom.DACExt.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DisplayAttentionEventsAttribute : PXEventSubscriberAttribute, IPXFieldDefaultingSubscriber, IPXFieldSelectingSubscriber, IPXFieldUpdatedSubscriber
    {
        [InjectDependencyOnTypeLevel]
        private protected IPurchaseContactService ContactService { get; set; }

        public void FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs eventHandler)
        {
            SOOrder row = eventHandler.Row as SOOrder;
            if (row is null) return;

            eventHandler.NewValue = ContactService?.FindBuyerAttentionForOrder(cache, row);
        }

        public void FieldSelecting(PXCache cache, PXFieldSelectingEventArgs eventHandler)
        {
            SOOrder row = eventHandler.Row as SOOrder;
            if (row is null) return;

            eventHandler.ReturnValue = ContactService?.FindBuyerAttentionForOrder(cache, row);
        }

        public void FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs eventHandler)
        {
            SOOrder row = eventHandler.Row as SOOrder;
            if (row is null) return;

            SOOrderExt extendedRow = row.GetExtension<SOOrderExt>();

            if (extendedRow.UsrCSROverride is true)
                cache.SetValueExt<SOOrderExt.usrCSRAttention>(row, extendedRow.UsrCSRDisplayAttention);
        }
    }
}

