using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.SO;

namespace PX.STI.STICustom.GraphExt
{
    public class SOInvoiceEntryExt : PXGraphExtension<SOInvoiceEntry>
    {
        public static bool IsActive() { return true; }

        #region Actions
        #endregion
        #region Events

        protected void _(Events.RowSelected<ARInvoice> eventArgs)
        {
            ARInvoice row = eventArgs.Row;
            if (row is null) return;

            if (row.Released is true)
            {
                PXUIFieldAttribute.SetEnabled<ARInvoice.invoiceNbr>(eventArgs.Cache, row, true);
                PXUIFieldAttribute.SetEnabled<ARInvoice.docDesc>(eventArgs.Cache, row, true);
                Base.Transactions.Cache.AllowUpdate = true;
            }
        }

        #endregion
        #region Data Types
        #endregion
    }
}
