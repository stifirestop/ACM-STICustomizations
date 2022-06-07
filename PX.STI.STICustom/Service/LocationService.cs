using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CR;
using CRLocation = PX.Objects.CR.Standalone.Location;

namespace PX.STI.STICustom.Service
{
    public static class LocationService
    {
        public static SalesPerson FetchSalesPersonByID(PXGraph graph, int? salesPersonID)
        {
            SalesPerson result = null;

            using (new PXConnectionScope())
            {
                result = SelectFrom<SalesPerson>
                    .Where<SalesPerson.salesPersonID.IsEqual<@P.AsInt>>
                    .View.Select(graph, salesPersonID);
            }

            return result;
        }

        public static SalesPerson FetchSalesPersonByCD(PXGraph graph, string salesPersonCD)
        {
            SalesPerson result = null;

            using (new PXConnectionScope())
            {
                result = SelectFrom<SalesPerson>
                    .Where<SalesPerson.salesPersonCD.IsEqual<@P.AsString>>
                    .View.Select(graph, salesPersonCD);
            }

            return result;
        }

        public static CustSalesPeople FetchCustLocationSalesPerson(PXGraph graph, int? customerID, int? locationID)
        {
            CustSalesPeople result = null;

            using (new PXConnectionScope())
            {
                result = SelectFrom<CustSalesPeople>
                    .Where<CustSalesPeople.bAccountID.IsEqual<@P.AsInt>
                    .And<CustSalesPeople.locationID.IsEqual<@P.AsInt>
                    .And<CustSalesPeople.isDefault.IsEqual<True>>>>
                    .View.Select(graph, customerID, locationID);
            }

            return result;
        }

        public static List<CustSalesPeople> FetchAllSalesPeopleForLocation(PXGraph graph, int? customerID, int? locationID)
        {
            List<CustSalesPeople> result = null;

            using (new PXConnectionScope())
            {
                result = SelectFrom<CustSalesPeople>
                    .Where<CustSalesPeople.bAccountID.IsEqual<@P.AsInt>
                    .And<CustSalesPeople.locationID.IsEqual<@P.AsInt>>>
                    .View.Select(graph, customerID, locationID)
                    .RowCast<CustSalesPeople>()
                    .ToList();
            }

            return result;
        }

        public static LocationExtAddress FetchLocationAddress(PXGraph graph, int? customerID, int? addressID, int? locationID)
        {
            LocationExtAddress result = null;

            using (new PXConnectionScope())
            {
                result = SelectFrom<LocationExtAddress>
                    .Where<LocationExtAddress.bAccountID.IsEqual<@P.AsInt>
                    .And<LocationExtAddress.addressID.IsEqual<@P.AsInt>
                    .And<LocationExtAddress.locationID.IsEqual<@P.AsInt>>>>
                    .View.Select(graph, customerID, addressID, locationID);
            }

            return result;
        }

        public static Location FetchSalesPersonLocation(PXGraph graph, int? customerID, int? locationID)
        {
            Location result = null;

            using (new PXConnectionScope())
            {
                result = SelectFrom<Location>
                    .Where<CRLocation.bAccountID.IsEqual<@P.AsInt>
                    .And<CRLocation.locationID.IsEqual<@P.AsInt>>>
                    .View.Select(graph, customerID, locationID);
            }

            return result;
        }

    }
}

