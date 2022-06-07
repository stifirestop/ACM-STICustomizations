using System;
using System.Collections;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using System.Diagnostics;
using PX.STI.STICustom.DAC;

namespace PX.STI.STICustom.Graph
{
    public class SurplusObsoleteMaint : PXGraph<SurplusObsoleteMaint>
    {
        #region Actions
        #endregion
        #region Events

        protected void _(Events.FieldDefaulting<SurplusObsoleteFilter, SurplusObsoleteFilter.startPeriod> eventHandler)
        {
            SurplusObsoleteFilter row = eventHandler.Row;
            if (row is null) return;

            string result = Common.Helpers.CalculateStartPeriod(eventHandler.Cache.Graph, null);
            eventHandler.NewValue = result.Substring(4, 2) + result.Substring(0, 4);
        }

        protected void _(Events.FieldDefaulting<SurplusObsoleteFilter, SurplusObsoleteFilter.endPeriod> eventHandler)
        {
            SurplusObsoleteFilter row = eventHandler.Row;
            if (row is null) return;

            string result = Common.Helpers.CalculateEndPeriod(eventHandler.Cache.Graph, null);
            eventHandler.NewValue = result.Substring(4, 2) + result.Substring(0, 4);
        }

        protected void _(Events.RowUpdated<SurplusObsoleteFilter> eventHandler)
        {
            SurplusObsoleteFilter row = eventHandler.Row;
            if (row is null) return;

            eventHandler.Cache.Graph.Caches.Clear();
        }

        #endregion
        #region Data Types
        #endregion

        public SurplusObsoleteMaint()
        {
            SurplusObsoleteFilter filter = Filter.Current;
            PXCache cache = this.Filter.Cache;

            Details.AllowInsert = false;
            Details.AllowDelete = false;

            if (filter.StartPeriod is null)
                cache.SetValueExt<SurplusObsoleteFilter.startPeriod>(filter, Common.Helpers.CalculateStartPeriod(this, null));

            if (filter.EndPeriod is null)
                cache.SetValueExt<SurplusObsoleteFilter.endPeriod>(filter, Common.Helpers.CalculateEndPeriod(this, null));
        }

        public PXFilter<SurplusObsoleteFilter> Filter;
        public PXSelect<STInventoryHistory> Details;

        public virtual IEnumerable details()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SurplusObsoleteFilter filter = Filter.Current;

            if (filter.StartPeriod == null || filter.StartPeriod == ""
                || filter.EndPeriod == null || filter.StartPeriod == "")
                yield return null;

            PXSelectBase<STInventoryHistory> query = new SelectFrom<STInventoryHistory>.View(this);

            foreach (STInventoryHistory record in query.Select())
            {
                record.EndQty = Common.Services.CalculateEndingQty(this, record.InventoryID, filter.EndPeriod);

                if (record.StandardCost != null && record.StandardCost != 0.00M && record.EndQty != 0.00M)
                {
                    record.StartQty = (filter.StartPeriod.CompareTo(Common.Helpers._BeginDate) <= 0)
                        ? Common.Services.CalculateStartingQty(this, record.InventoryID)
                        : Common.Services.CalculateEndingQty(this, record.InventoryID, Common.Helpers.CalculatePreviousPeriod(filter.StartPeriod));

                    INItemSiteHist movements = Common.Services.CalculateMovementQuantities(this, record.InventoryID);
                    record.IssuedQty = movements?.FinPtdQtyIssued ?? 0.00M;
                    record.ReceivedQty = movements?.FinPtdQtyReceived ?? 0.00M;
                    record.SalesQty = movements?.FinPtdQtySales ?? 0.00M;
                    record.DropShipSalesQty = movements?.FinPtdQtyDropShipSales ?? 0.00M;
                    record.CreditMemoQty = movements?.FinPtdQtyCreditMemos ?? 0.00M;
                    record.TransferInQty = movements?.FinPtdQtyTransferIn ?? 0.00M;
                    record.TransferOutQty = movements?.FinPtdQtyTransferOut ?? 0.00M;
                    record.AdjustmentQty = Common.Services.CalculateAdjustmentQty(this, record.InventoryID);
                    record.ScrapQty = Common.Services.CalculateScrapQty(this, record.InventoryID);
                    record.InventoryCost = Common.Helpers.CalculateInventoryCost(record);
                    record.YearsOnHand = Common.Services.CalculateYearsOnHand(this, record);

                    Decimal? total = record.StartQty + record.ReceivedQty + record.AdjustmentQty + record.TransferInQty - record.IssuedQty
                        - record.SalesQty - record.DropShipSalesQty + record.CreditMemoQty - record.TransferOutQty;

                    record.IsValid = (total == record.EndQty);

                    yield return record;
                }
            }

            Details.Cache.IsDirty = false;

            stopwatch.Stop();
            PXTrace.WriteInformation("Grid Select Time: " + stopwatch.ElapsedMilliseconds.ToString());
        }
    }
}
