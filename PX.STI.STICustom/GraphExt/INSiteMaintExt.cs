using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using PX.STI.STICustom.Common;
using PX.STI.STICustom.DAC;
using System;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class INSiteMaintExt : PXGraphExtension<INSiteMaint>
    {
        #region Actions
        #endregion
        #region Events

        private protected void _(Events.FieldVerifying<STSiteTransitEst, STSiteTransitEst.estTransitDays> eventHandler)
        {
            STSiteTransitEst row = eventHandler.Row;
            if (row is null) return;

            int days;

            try
            {
                days = int.Parse(eventHandler.NewValue.ToString());
            }
            catch (Exception e)
            {
                PXTrace.WriteError($"{ e.Message }");
                days = 0;
            }

            if (days < 0)
                throw new PXSetPropertyException(CustomMessages.TransitDaysLessThanZero, "EstTransitDays");
        }

        #endregion
        #region Data Types
        #endregion

        public SelectFrom<STSiteTransitEst>
            .Where<STSiteTransitEst.siteID.IsEqual<INSite.siteID.FromCurrent>>
            .View SiteTransitEstimates;
    }
}

