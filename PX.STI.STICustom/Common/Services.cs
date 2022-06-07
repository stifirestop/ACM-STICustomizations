using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.STI.STICustom.DAC;

namespace PX.STI.STICustom.Common
{
    public static class Services
    {
        public static Decimal CalculateStartingQty(PXGraph graph, int? inventoryID)
        {
            if (graph is null || inventoryID is null)
                return 0.00M;

            List<Object> queryParams = new List<Object>();
            queryParams.Add(inventoryID);
            queryParams.Add(Common.Helpers._BeginDate);

            List<INItemSiteHist> records = PXSelect<INItemSiteHist,
                Where<INItemSiteHist.inventoryID, Equal<Required<INItemSiteHist.inventoryID>>,
                And<INItemSiteHist.finPeriodID, LessEqual<Required<INItemSiteHist.finPeriodID>>>>,
                OrderBy<
                    Asc<INItemSiteHist.inventoryID,
                    Asc<INItemSiteHist.siteID,
                    Asc<INItemSiteHist.locationID,
                    Desc<INItemSiteHist.finPeriodID>>>>>>
                .Select(graph, queryParams.ToArray())
                .RowCast<INItemSiteHist>()
                .ToList()
                .GroupBy(item => new
                {
                    item.InventoryID,
                    item.SiteID,
                    item.LocationID
                })
                .Select(item => new INItemSiteHist
                {
                    FinPtdQtyAdjusted = item.FirstOrDefault().FinPtdQtyAdjusted
                })
                .ToList();

            return records.Sum(x => x.FinPtdQtyAdjusted) ?? 0.00M;
        }

        public static Decimal CalculateEndingQty(PXGraph graph, int? inventoryID, string endPeriod)
        {
            if (graph is null || inventoryID is null || endPeriod is null)
                return 0.00M;

            List<Object> queryParams = new List<object>();
            queryParams.Add(inventoryID);
            queryParams.Add(endPeriod);

            List<INItemSiteHist> records = PXSelect<INItemSiteHist,
                Where<INItemSiteHist.inventoryID, Equal<Required<INItemSiteHist.inventoryID>>,
                And<INItemSiteHist.finPeriodID, LessEqual<Required<INItemSiteHist.finPeriodID>>>>,
                OrderBy<
                    Asc<INItemSiteHist.inventoryID,
                    Asc<INItemSiteHist.siteID,
                    Asc<INItemSiteHist.locationID,
                    Desc<INItemSiteHist.finPeriodID>>>>>>
                .Select(graph, queryParams.ToArray())
                .RowCast<INItemSiteHist>()
                .ToList()
                .GroupBy(x => new
                {
                    x.InventoryID,
                    x.SiteID,
                    x.LocationID
                })
                .Select(y => new INItemSiteHist
                {
                    FinYtdQty = y.FirstOrDefault().FinYtdQty
                })
                .ToList();

            return records.Sum(x => x.FinYtdQty) ?? 0.00M;
        }

        public static INItemSiteHist CalculateMovementQuantities(PXGraph graph, int? inventoryID)
        {
            INItemSiteHist result = null;

            if (graph is null || inventoryID is null)
                return result;

            SurplusObsoleteFilter filter = graph.Caches[typeof(SurplusObsoleteFilter)].Current as SurplusObsoleteFilter;

            if (filter is null) return result;

            List<Object> queryParams = new List<object>();
            queryParams.Add(inventoryID);
            queryParams.Add(Common.Helpers.CalculateStartPeriod(graph, filter.StartPeriod));
            queryParams.Add(Common.Helpers.CalculateEndPeriod(graph, filter.EndPeriod));

            result = PXSelectGroupBy<INItemSiteHist,
                Where<INItemSiteHist.inventoryID, Equal<Required<INItemSiteHist.inventoryID>>,
                And<INItemSiteHist.finPeriodID, GreaterEqual<Required<INItemSiteHist.finPeriodID>>,
                And<INItemSiteHist.finPeriodID, LessEqual<Required<INItemSiteHist.finPeriodID>>>>>,
                Aggregate<
                    GroupBy<INItemSiteHist.inventoryID,
                    Sum<INItemSiteHist.finPtdQtyIssued,
                    Sum<INItemSiteHist.finPtdQtyReceived,
                    Sum<INItemSiteHist.finPtdQtySales,
                    Sum<INItemSiteHist.finPtdQtyDropShipSales,
                    Sum<INItemSiteHist.finPtdQtyCreditMemos,
                    Sum<INItemSiteHist.finPtdQtyTransferIn,
                    Sum<INItemSiteHist.finPtdQtyTransferOut>>>>>>>>>>
                .Select(graph, queryParams.ToArray());

            return result;
        }

        public static Decimal? CalculateAdjustmentQty(PXGraph graph, int? inventoryID)
        {
            Decimal? result = 0.00M;
            if (graph is null || inventoryID is null) return result;

            SurplusObsoleteFilter filter = graph.Caches[typeof(SurplusObsoleteFilter)].Current as SurplusObsoleteFilter;

            String startPeriod = Common.Helpers.CalculateStartPeriod(graph, filter?.StartPeriod ?? "201806");
            startPeriod = startPeriod.CompareTo("201806") <= 0 ? "201807" : startPeriod;

            List<Object> queryParams = new List<object>();
            queryParams.Add(inventoryID);
            queryParams.Add(startPeriod);
            queryParams.Add(Common.Helpers.CalculateEndPeriod(graph, filter?.EndPeriod));
            queryParams.Add("201806");

            INItemSiteHist record = PXSelectGroupBy<INItemSiteHist,
                Where<INItemSiteHist.inventoryID, Equal<Required<INItemSiteHist.inventoryID>>,
                And<INItemSiteHist.finPeriodID, GreaterEqual<Required<INItemSiteHist.finPeriodID>>,
                And<INItemSiteHist.finPeriodID, LessEqual<Required<INItemSiteHist.finPeriodID>>,
                And<INItemSiteHist.finPeriodID, NotEqual<Required<INItemSiteHist.finPeriodID>>>>>>,
                Aggregate<
                    GroupBy<INItemSiteHist.inventoryID,
                    Sum<INItemSiteHist.finPtdQtyAdjusted>>>>
                .Select(graph, queryParams.ToArray());

            result = record?.FinPtdQtyAdjusted ?? 0.00M;
            return result;
        }

        public static Decimal? CalculateScrapQty(PXGraph graph, int? inventoryID)
        {
            Decimal? result = 0.00M;

            if (graph is null || inventoryID is null) return result;

            SurplusObsoleteFilter filter = graph.Caches[typeof(SurplusObsoleteFilter)].Current as SurplusObsoleteFilter;

            List<Object> queryParams = new List<object>();
            queryParams.Add(inventoryID);
            queryParams.Add(Common.Helpers.CalculateEndPeriod(graph, filter?.EndPeriod));
            queryParams.Add("Scrap");

            INItemSiteHist record = PXSelectJoinGroupBy<INItemSiteHist,
                InnerJoin<INSite,
                    On<INSite.siteID, Equal<INItemSiteHist.siteID>>>,
                Where<INItemSiteHist.inventoryID, Equal<Required<INItemSiteHist.inventoryID>>,
                And<INItemSiteHist.finPeriodID, LessEqual<Required<INItemSiteHist.finPeriodID>>,
                And<INSite.siteCD, Equal<Required<INSite.siteCD>>>>>,
                Aggregate<
                    GroupBy<INItemSiteHist.inventoryID,
                    GroupBy<INItemSiteHist.finPeriodID,
                    Sum<INItemSiteHist.finYtdQty>>>>,
                OrderBy<Desc<INItemSiteHist.finPeriodID>>>
                .SelectWindowed(graph, 0, 1, queryParams.ToArray());

            result = record?.FinYtdQty ?? 0.00M;
            return result;
        }

        public static Decimal? CalculateYearsOnHand(PXGraph graph, STInventoryHistory row)
        {
            Decimal? result = 0.00M;
            if (graph is null || row is null) return result;

            SurplusObsoleteFilter filter = graph.Caches[typeof(SurplusObsoleteFilter)].Current as SurplusObsoleteFilter;

            List<Object> queryParams = new List<object>();
            queryParams.Add(Common.Helpers.CalculateStartPeriod(graph, filter?.StartPeriod));
            queryParams.Add(Common.Helpers.CalculateEndPeriod(graph, filter?.EndPeriod));

            List<MasterFinPeriod> records = PXSelect<MasterFinPeriod,
                Where<MasterFinPeriod.finPeriodID, GreaterEqual<Required<MasterFinPeriod.finPeriodID>>,
                And<MasterFinPeriod.finPeriodID, LessEqual<Required<MasterFinPeriod.finPeriodID>>>>>
                .Select(graph, queryParams.ToArray())
                .RowCast<MasterFinPeriod>()
                .ToList();

            int months = records?.Count() ?? 0;
            Decimal productMoved = (row.IssuedQty + row.SalesQty + row.DropShipSalesQty + row.CreditMemoQty) ?? 0.00M;

            try
            {
                Decimal yearlyAvg = (productMoved / months) * 12;
                result = row.EndQty / yearlyAvg ?? 0.00M;
            }
            catch (DivideByZeroException) { }

            return result;
        }
    }
}

