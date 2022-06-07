using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.STI.STICustom.DACExt;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class ARPaymentEntryExt : PXGraphExtension<ARPaymentEntry>
    {
        #region Actions
        #endregion
        #region Events
        protected void ARAdjust_UsrAcceptDiscount_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
        {

            ARAdjust row = (ARAdjust)e.Row;
            if (row == null)
                return;

            if (cache.GetStatus(row) == PXEntryStatus.Updated)
            {

                string allowDisc = e.NewValue.ToString();

                ARInvoice invoice = SelectFrom<ARInvoice>
                            .Where<ARInvoice.refNbr.IsEqual<@P.AsString>>
                            .View.Select(cache.Graph, row.AdjdRefNbr);

                if (invoice == null)
                    return;

                DACExt.ARInvoiceExt invoiceExt = invoice.GetExtension<DACExt.ARInvoiceExt>();

                if (invoiceExt != null)
                {
                    if (allowDisc == "True")
                    {

                        if (invoiceExt.usrOrigDiscDate < DateTime.Now)
                        {
                            if (row.CuryDiscBal > row.CuryAdjgAmt)
                            {
                                row.CuryAdjgAmt = row.CuryAdjgAmt - row.CuryAdjgAmt;
                                row.CuryAdjgPPDAmt = row.CuryAdjgAmt;
                                row.CuryDiscBal = row.CuryDiscBal - row.CuryAdjgAmt;

                            }
                            else
                            {
                                row.CuryAdjgAmt = row.CuryAdjgAmt - row.CuryDiscBal;
                                row.CuryAdjgPPDAmt = row.CuryDiscBal;
                                row.CuryDiscBal = 0;

                            }



                            string dateErr = "Cash Discount Date " + string.Format(invoiceExt.usrOrigDiscDate.ToString(), "MM-dd-yyyy") + " has expired";
                            PXUIFieldAttribute.SetWarning<ARAdjust.curyAdjgPPDAmt>(cache, row, dateErr);

                        }
                        else
                        {
                            if (row.CuryDiscBal > row.CuryAdjgAmt)
                            {
                                row.CuryAdjgAmt = row.CuryAdjgAmt - row.CuryAdjgAmt;
                                row.CuryAdjgPPDAmt = row.CuryAdjgAmt;
                                row.CuryDiscBal = row.CuryDiscBal - row.CuryAdjgAmt;

                            }
                            else
                            {
                                row.CuryAdjgAmt = row.CuryAdjgAmt - row.CuryDiscBal;
                                row.CuryAdjgPPDAmt = row.CuryDiscBal;
                                row.CuryDiscBal = 0;

                            }
                        }
                    }
                    else
                    {
                        if (invoiceExt.usrOrigDiscDate < DateTime.Now)
                        {
                            if (row.CuryAdjgPPDAmt > 0)
                            {
                                row.CuryAdjgAmt = row.CuryAdjgAmt + row.CuryAdjgPPDAmt;
                                row.CuryDiscBal = row.CuryAdjgPPDAmt;
                                row.CuryAdjgPPDAmt = 0;

                            }
                        }
                    }
                }
            }
        }
        protected void ARAdjust_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
        {
            ARAdjust row = (ARAdjust)e.Row;

            if (row == null)
                return;

            ARInvoice invoice = SelectFrom<ARInvoice>
                        .Where<ARInvoice.refNbr.IsEqual<@P.AsString>>
                        .View.Select(cache.Graph, row.AdjdRefNbr);

            if (invoice == null)
                return;

            DACExt.ARInvoiceExt invoiceExt = invoice.GetExtension<DACExt.ARInvoiceExt>();
            ARAdjustExt rowExt = row.GetExtension<ARAdjustExt>();

            if (invoiceExt != null)
            {
                if (row.CuryAdjgPPDAmt > 0 || row.CuryDiscBal > 0)
                {
                    if (invoiceExt.usrOrigDiscDate < DateTime.Now)
                    {
                        rowExt.UsrAcceptDiscount = true;

                        string dateErr = "Cash Discount Date " + string.Format(invoiceExt.usrOrigDiscDate.ToString(), "MM-dd-yyyy") + " has expired";
                        PXUIFieldAttribute.SetWarning<ARAdjust.curyAdjgPPDAmt>(cache, row, dateErr);
                    }
                    else
                    {
                        rowExt.UsrAcceptDiscount = true;
                    }
                }
                else
                {
                    rowExt.UsrAcceptDiscount = false;
                }
            }
        }
        protected void ARAdjust_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
        {
            ARAdjust row = (ARAdjust)e.Row;

            if (row == null)
                return;

            ARInvoice invoice = SelectFrom<ARInvoice>
                        .Where<ARInvoice.refNbr.IsEqual<@P.AsString>>
                        .View.Select(cache.Graph, row.AdjdRefNbr);

            if (invoice == null)
                return;

            DACExt.ARInvoiceExt invoiceExt = invoice.GetExtension<DACExt.ARInvoiceExt>();
            ARAdjustExt rowExt = row.GetExtension<ARAdjustExt>();

            if (invoiceExt != null)
            {
                if (invoiceExt.usrOrigDiscDate < DateTime.Now)
                {
                    if (row.CuryAdjgPPDAmt > 0)
                    {
                        rowExt.UsrAcceptDiscount = false;

                    }
                }
                else
                {
                    rowExt.UsrAcceptDiscount = true;

                }
            }
        }
        #endregion
        #region Data Types
        #endregion
    }
}

