using PX.Data;
using System;
using PX.STI.STICustom.DAC;

namespace PX.STI.STICustom.DACExt.Attribute
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class SiteTransitCountryEventsAttribute : PXEventSubscriberAttribute, IPXFieldUpdatedSubscriber
    {
        public void FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs eventHandler)
        {
            STSiteTransitEst row = eventHandler.Row as STSiteTransitEst;
            if (row is null) return;

            row.StateID = null;
            row.EstTransitDays = null;
        }
    }
}
