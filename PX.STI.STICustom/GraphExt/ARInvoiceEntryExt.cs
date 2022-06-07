using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.SO;
using PX.Objects.AR;
using PX.STI.STICustom.DACExt;

namespace PX.STI.STICustom.GraphExt
{
    public class ARInvoiceEntryExt : PXGraphExtension<ARInvoiceEntry>
    {
        public static bool IsActive() { return true; }

        #region Actions
        #endregion
        #region Events
        protected void _(Events.RowSelecting<ARInvoice> eventArgs)
        {
            //ARInvoice row = eventArgs.Row;
            //if (row is null) return;

            //if (row.DiscDate < DateTime.Now)
            //{
            //    row.DiscDate = DateTime.Now;
            //}
        }

        protected void ARInvoice_TermsID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
        {

            var row = (ARInvoice)e.Row;
            if (row is null) return;

            DACExt.ARInvoiceExt rowExt = row.GetExtension<DACExt.ARInvoiceExt>();

            if (row.DiscDate != null)
            {
                rowExt.usrOrigDiscDate = row.DiscDate;
            }

        }

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

            DACExt.ARInvoiceExt rowExt = eventArgs.Cache.GetExtension<DACExt.ARInvoiceExt>(row);

            if (rowExt is null) return;


            this.Base.GetDocumentState(eventArgs.Cache, row);

            ARInvoiceState state = this.Base.GetDocumentState(eventArgs.Cache, row);

            PXUIFieldAttribute.SetRequired<DACExt.ARInvoiceExt.UsrOrigDiscDate>(eventArgs.Cache, !state.IsDocumentCreditMemo);

            if (state.ShouldDisableHeader)
            {
                PXUIFieldAttribute.SetEnabled<DACExt.ARInvoiceExt.UsrOrigDiscDate>(eventArgs.Cache, row, (row.DocType != ARDocType.CreditMemo && row.DocType != ARDocType.SmallCreditWO && row.DocType != ARDocType.FinCharge) && row.OpenDoc == true && row.PendingPPD != true);
            }
        }


        protected void _(Events.RowInserting<ARInvoice> eventArgs)
        {
            ARInvoice row = eventArgs.Row;
            if (row is null) return;

            DACExt.ARInvoiceExt rowExt = eventArgs.Cache.GetExtension<DACExt.ARInvoiceExt>(row);

            row.DiscDate = rowExt.usrOrigDiscDate;
        }

        private protected virtual void _(Events.RowUpdating<ARRegister> eventHandler)
        {
            ARRegister row = eventHandler.NewRow;
            SOOrderShipment shipRow = null;

            using (new PXConnectionScope())
            {
                shipRow = SelectFrom<SOOrderShipment>
                    .Where<SOOrderShipment.invoiceNbr.IsEqual<@P.AsString>>
                    .View.Select(eventHandler.Cache.Graph, row.RefNbr);

            }

            if (shipRow != null)
            {
                ARRegisterExt extRow = row.GetExtension<ARRegisterExt>();

                extRow.usrSOType = shipRow.OrderType;
            }

        }

        #endregion
        #region Data Types
        #endregion
    }
}
