using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using PX.Objects.SO;
using PX.Objects.AR;
using PX.STI.STICustom.Common;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class SOInvoiceShipmentExt : PXGraphExtension<SOInvoiceShipment>
    {
        #region Actions
        #endregion
        #region Events

        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXDefault(typeof(SearchFor<INSite.siteID>
            .Where<INSite.siteCD.IsEqual<CustomDefault.siteCD>>),
            PersistingCheck = PXPersistingCheck.Null)]
        private protected virtual void _(Events.CacheAttached<SOShipmentFilter.siteID> eventHandler) { }

        private protected virtual void _(Events.FieldUpdated<SOShipmentFilter, SOShipmentFilter.siteID> eventHandler)
        {
            SOShipmentFilter row = eventHandler.Row;
            if (row is null) return;

            Base.Orders.SetProcessEnabled(row.SiteID.HasValue);
            Base.Orders.SetProcessAllEnabled(row.SiteID.HasValue);

            if (row.SiteID.HasValue is false)
                eventHandler.Cache.RaiseExceptionHandling<SOShipmentFilter.siteID>(row, row.SiteID,
                    new PXSetPropertyException(CustomMessages.WarehouseIsRequired));
        }

        private protected virtual void _(Events.RowInserting<ARRegister> eventHandler)
        {
            ARRegister row = eventHandler.Row;
        }

        private protected virtual void _(Events.RowInserting<ARInvoice> eventHandler)
        {
            ARInvoice row = eventHandler.Row;
        }

        private protected virtual void _(Events.RowInserting<SOInvoice> eventHandler)
        {
            SOInvoice row = eventHandler.Row;
        }

        #endregion
        #region Data Types
        #endregion
    }
}
