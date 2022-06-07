using System;
using System.Linq;
using System.Collections.Generic;


using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CR;
//using PX.Objects.CR.Standalone;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.CM;
using PX.STI.STICustom.Common;
using PX.STI.STICustom.Service;
using PX.STI.STICustom.DAC;
using PX.STI.STICustom.DACExt;
using CRLocation = PX.Objects.CR.Standalone.Location;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class CustomerMaintExt : PXGraphExtension<CustomerMaint>
    {
        #region Actions
        public PXAction<PX.Objects.AR.Customer> CalcSalesRepData;

        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Update Sales Rep Data")]
        protected void calcSalesRepData()
        {
            Customer customer = Base.CurrentCustomer.Current;


            UpdateLocationTerritories(base.Base, customer);
            UpdateDefSalesPeople(base.Base, customer);

        }
        #endregion
        #region Event Handlers

        //protected void _(Events.FieldUpdated<Address, Address.postalCode> eventHandler)
        //{
        //    var row = eventHandler.Row;
        //    Customer customer = Base.CurrentCustomer.Current;

        //    PXCache customerCache = Base.CurrentCustomer.Cache;

        //    if (row is null) return;

        //    if (customerCache.GetStatus(customer) == PXEntryStatus.Inserted)
        //    {
        //        // Acuminator disable once PX1043 SavingChangesInEventHandlers [Justification]
        //        UpdateDefLocationTerritory(eventHandler.Cache.Graph);
        //        UpdateDefLocationSalesPerson(eventHandler.Cache.Graph);
        //    }
        //    else
        //    {
        //        // Acuminator disable once PX1043 SavingChangesInEventHandlers [Justification]
        //        UpdateLocationTerritories(eventHandler.Cache.Graph, customer);
        //        UpdateDefSalesPeople(eventHandler.Cache.Graph, customer);
        //    }
        //}
        //protected void _(Events.FieldUpdated<Customer, Customer.customerClassID> eventHandler)
        //{
        //    Customer row = eventHandler.Row;
        //    PXCache customerCache = Base.CurrentCustomer.Cache;

        //    //Address address = AccountService.FetchCustomerAddress(eventHandler.Cache.Graph, row.BAccountID, row.DefAddressID);
        //    //CRLocation cLoc = Base.BaseLocations.Current;



        //    if (row is null)
        //        return;

        //    if (customerCache.GetStatus(row) == PXEntryStatus.Inserted)
        //    {
        //        UpdateDefLocationTerritory(eventHandler.Cache.Graph);
        //        UpdateDefLocationSalesPerson(eventHandler.Cache.Graph);
        //    }
        //    else
        //    {
        //        UpdateLocationTerritories(eventHandler.Cache.Graph, row);
        //        UpdateDefSalesPeople(eventHandler.Cache.Graph, row);
        //    }
        //}
        //protected void _(Events.FieldUpdated<Customer, BAccountExt.usrAccountType> eventHandler)
        //{
        //    Customer row = eventHandler.Row;
        //    PXCache customerCache = Base.CurrentCustomer.Cache;

        //    //Address address = AccountService.FetchCustomerAddress(eventHandler.Cache.Graph, row.BAccountID, row.DefAddressID);
        //    //CRLocation cLoc = Base.BaseLocations.Current;

        //    if (row is null)
        //        return;

        //    if (customerCache.GetStatus(row) == PXEntryStatus.Inserted)
        //    {
        //        UpdateDefLocationTerritory(eventHandler.Cache.Graph);
        //        UpdateDefLocationSalesPerson(eventHandler.Cache.Graph);
        //    }
        //    else
        //    {
        //        UpdateLocationTerritories(eventHandler.Cache.Graph, row);
        //        UpdateDefSalesPeople(eventHandler.Cache.Graph, row);


        //    }
        //}

        protected void Address_PostalCode_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
        {
            var row = (Address)e.Row;
            Customer customer = Base.CurrentCustomer.Current;
            CRLocation cLoc = Base.BaseLocations.Current;

            PXCache customerCache = Base.CurrentCustomer.Cache;

            if (row is null) return;

            if (customerCache.GetStatus(customer) == PXEntryStatus.Inserted)
            {
                UpdateDefLocationTerritory(cache.Graph);
                UpdateDefLocationSalesPerson(cache.Graph);
            }
            else
            {
                UpdateLocationTerritories(cache.Graph, customer);
                UpdateDefSalesPeople(cache.Graph, customer);
            }




            //if (cLoc is null || row is null || customer is null)
            //    return;

            //if (row.AddressID == customer.DefAddressID)
            //{
            //    if (cLoc.LocationCD.Trim(' ') == "MAIN")
            //    {
            //        UpdateDefLocationTerritory(cache.Graph);
            //        UpdateDefLocationSalesPerson(cache.Graph);
            //    }

            //    UpdateLocationTerritories(cache.Graph, customer);
            //    UpdateDefSalesPeople(cache.Graph, customer);
            //}
        }

        protected void Address_State_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
        {
            var row = (Address)e.Row;
            Customer customer = Base.CurrentCustomer.Current;
            CRLocation cLoc = Base.BaseLocations.Current;

            PXCache customerCache = Base.CurrentCustomer.Cache;

            if (row is null) return;

            if (customerCache.GetStatus(customer) == PXEntryStatus.Inserted)
            {
                UpdateDefLocationTerritory(cache.Graph);
                UpdateDefLocationSalesPerson(cache.Graph);
            }
            else
            {
                UpdateLocationTerritories(cache.Graph, customer);
                UpdateDefSalesPeople(cache.Graph, customer);
            }

            //if (cLoc is null || row is null || customer is null)
            //    return;

            //if (row.AddressID == customer.DefAddressID)
            //{
            //    if (cLoc.LocationCD.Trim(' ') == "MAIN")
            //    {
            //        UpdateDefLocationTerritory(cache.Graph);
            //        UpdateDefLocationSalesPerson(cache.Graph);
            //    }
            //    UpdateLocationTerritories(cache.Graph, customer);
            //    UpdateDefSalesPeople(cache.Graph, customer);

            //}
        }

        protected void _(Events.FieldUpdated<CSAnswers, CSAnswers.value> eventHandler)
        {
            CSAnswers row = eventHandler.Row;
            Customer customer = Base.CurrentCustomer.Current;

            if (row is null || customer is null)
                return;

            if (row.AttributeID == "SUBCHANNEL")
            {
                if (customer.CustomerClassID == "02")
                {
                    UpdateLocationTerritories(eventHandler.Cache.Graph, customer);
                    UpdateDefSalesPeople(eventHandler.Cache.Graph, customer);
                }
            }
        }

        protected void CustSalesPeople_RowSelecting(PXCache cache, PXRowSelectingEventArgs e)
        {
            var row = (CustSalesPeople)e.Row;

            if (row is null)
                return;

            Location cloc = LocationService.FetchSalesPersonLocation(cache.Graph, row.BAccountID, row.LocationID);

            if (cloc is null)
                return;

            Address spAddress = AccountService.FetchCustomerAddress(cache.Graph, cloc?.BAccountID, cloc?.DefAddressID);

            if (spAddress is null)
                return;

            CustSalesPeopleExt rowExt = row.GetExtension<CustSalesPeopleExt>();

            if (rowExt != null)
            {
                rowExt.UsrLocationPostalCode = spAddress.PostalCode;
                rowExt.UsrLocationState = spAddress.State;
            }
        }
        protected void _(Events.FieldUpdated<Customer, Customer.customerClassID> eventHandler)
        {
            Customer row = eventHandler.Row;
            Address address = AccountService.FetchCustomerAddress(eventHandler.Cache.Graph, row.BAccountID, row.DefAddressID);
            CRLocation cLoc = Base.BaseLocations.Current;

            PXCache customerCache = Base.CurrentCustomer.Cache;

            if (row is null) return;

            if (customerCache.GetStatus(row) == PXEntryStatus.Inserted)
            {
                UpdateDefLocationTerritory(eventHandler.Cache.Graph);
                UpdateDefLocationSalesPerson(eventHandler.Cache.Graph);
            }
            else
            {
                UpdateLocationTerritories(eventHandler.Cache.Graph, row);
                UpdateDefSalesPeople(eventHandler.Cache.Graph, row);
            }

            //if (cLoc is null || row is null || address is null)
            //    return;

            //if (cLoc.LocationCD.Trim(' ') == "MAIN")
            //{
            //    UpdateDefLocationTerritory(eventHandler.Cache.Graph);
            //    UpdateDefLocationSalesPerson(eventHandler.Cache.Graph);
            //}

            //UpdateLocationTerritories(eventHandler.Cache.Graph, row);
            //UpdateDefSalesPeople(eventHandler.Cache.Graph, row);

        }

        protected void _(Events.FieldUpdated<Customer, BAccountExt.usrAccountType> eventHandler)
        {
            Customer row = eventHandler.Row;
            Address address = AccountService.FetchCustomerAddress(eventHandler.Cache.Graph, row.BAccountID, row.DefAddressID);
            //CRLocation cLoc = Base.BaseLocations.Current;

            PXCache customerCache = Base.CurrentCustomer.Cache;

            if (row is null || address is null)
                return;

            if (customerCache.GetStatus(row) == PXEntryStatus.Inserted)
            {
                UpdateDefLocationTerritory(eventHandler.Cache.Graph);
                UpdateDefLocationSalesPerson(eventHandler.Cache.Graph);
            }
            else
            {
                UpdateLocationTerritories(eventHandler.Cache.Graph, row);
                UpdateDefSalesPeople(eventHandler.Cache.Graph, row);
            }
        }

        //protected void _(Events.FieldDefaulting<LocationExtAddress, LocationExtAddressExt.usrSalesTerritoryCD> eventHandler)
        //{
        //    LocationExtAddress row = eventHandler.Row;
        //    if (row is null) return;

        //    LocationExtAddressExt extendedLocationAddress = eventHandler.Cache.GetExtension<LocationExtAddressExt>(row);

        //    BAccount account = AccountService.FetchBAccount(eventHandler.Cache.Graph, row?.BAccountID);
        //    Customer customer = AccountService.FetchCustomer(eventHandler.Cache.Graph, row?.BAccountID);
        //    Address address = AccountService.FetchCustomerAddress(eventHandler.Cache.Graph, row?.BAccountID, row?.DefAddressID);

        //    if (address is null || account is null || customer is null)
        //        return;

        //    LocationExtAddressExt extendedLocation = row.GetExtension<LocationExtAddressExt>();
        //    BAccountExt extendedAccount = account.GetExtension<BAccountExt>();

        //    if (extendedLocation.UsrSalesTerritoryOverride is true
        //        || (address?.CountryID != "US" && address?.CountryID != "CA")
        //        || (CustomerClassType.CustomerClassesByCountry.Contains(customer?.CustomerClassID)))
        //        return;

        //    if ((address?.CountryID == "US" && address?.CountryID == "CA") && string.IsNullOrEmpty(address.PostalCode))
        //    {
        //        string dateErr = "Postal Code Cannot Be Blank";
        //        PXUIFieldAttribute.SetWarning<Address.postalCode>(eventHandler.Cache, eventHandler.Row, dateErr);
        //    }

        //    ////*** Old Sales Territory Fetch Logic ***//

        //    //string customerClassCD = Helper.CalculateCustomerClassCD(extendedAccount.UsrAccountType, customer.CustomerClassID);
        //    //SegmentValue salesTerritory = TerritoryService.FetchTerritoryByCustomerClass(eventHandler.Cache.Graph, customerClassCD);

        //    //if (salesTerritory?.Value is null)
        //    //    salesTerritory = TerritoryService.FetchTerritoryByPostalCode(eventHandler.Cache.Graph, address.PostalCode);


        //    //eventHandler.NewValue = salesTerritory?.Value ?? extendedLocation?.UsrSalesTerritoryCD;


        //    string customerClassCD = Helper.CalculateCustomerClassCD(extendedAccount.UsrAccountType, customer.CustomerClassID);
        //    if (customerClassCD == "04" || customerClassCD == "11" || customerClassCD == "HSE")
        //    {
        //        SegmentValue custClassTerritory = TerritoryService.FetchTerritoryByCustomerClass(eventHandler.Cache.Graph, customerClassCD);

        //        eventHandler.NewValue = custClassTerritory?.Value ?? extendedLocation?.UsrSalesTerritoryCD;
        //    }
        //    else
        //    {
        //        STSalesTerritoryMapping salesTerritory = TerritoryService.FetchTerritoryByPostalCode(eventHandler.Cache.Graph, address.PostalCode);
        //        string strSalesTerritoryCD = null;

        //        if (salesTerritory != null)
        //        {
        //            if (customer.CustomerClassID == CustomerClassType.Marine)
        //            {
        //                strSalesTerritoryCD = salesTerritory.MarineTerritoryCD;
        //            }
        //            else
        //            {
        //                strSalesTerritoryCD = salesTerritory.SalesTerritoryCD;
        //            }
        //        }

        //        eventHandler.NewValue = strSalesTerritoryCD; //salesTerritory?.Value ?? extendedLocation?.UsrSalesTerritoryCD;
        //    }
        //}

        protected void _(Events.FieldDefaulting<LocationExtAddress, LocationExtAddressExt.usrDefSalesPersonID> eventHandler)
        {
            LocationExtAddress row = eventHandler.Row;
            if (row is null) return;

            LocationExtAddressExt extendedLocationAddress = eventHandler.Cache.GetExtension<LocationExtAddressExt>(row);

            CustSalesPeople salesPerson = LocationService.FetchCustLocationSalesPerson(eventHandler.Cache.Graph, row.BAccountID, row.LocationID);
            eventHandler.NewValue = salesPerson?.SalesPersonID ?? extendedLocationAddress.UsrDefSalesPersonID;
        }

        protected void _(Events.FieldDefaulting<LocationExtAddress, LocationExtAddressExt.usrDefSalesPersonDescr> eventHandler)
        {
            LocationExtAddress row = eventHandler.Row;
            if (row is null) return;

            LocationExtAddressExt extendedLocationAddress = eventHandler.Cache.GetExtension<LocationExtAddressExt>(row);

            CustSalesPeople salesPerson = LocationService.FetchCustLocationSalesPerson(eventHandler.Cache.Graph, row.BAccountID, row.LocationID);
            eventHandler.NewValue = salesPerson?.SalesPersonID ?? extendedLocationAddress?.UsrDefSalesPersonID;
        }

        protected void _(Events.FieldUpdated<LocationExtAddress, LocationExtAddress.cFOBPointID> eventHandler)
        {
            LocationExtAddress row = eventHandler.Row;
            if (row is null) return;

            row.CShipTermsID = row.CFOBPointID;
        }

        protected void _(Events.RowSelected<LocationExtAddress> eventHandler)
        {
            LocationExtAddress row = eventHandler.Row;

            if (row is null) return;

            CRLocation cLoc = Base.BaseLocations.Current;

            if (cLoc != null)
            {

                LocationExtAddressExt rowExt = eventHandler.Cache.GetExtension<LocationExtAddressExt>(row);
                StandaloneLocationExt cLocExt = cLoc.GetExtension<StandaloneLocationExt>();

                if (rowExt != null || cLocExt != null)
                {
                    if (rowExt.UsrDefSalesPersonID == null && cLocExt.UsrDefSalesPersonID != null)
                    {
                        rowExt.UsrDefSalesPersonID = cLocExt.UsrDefSalesPersonID;
                        rowExt.UsrDefSalesPersonDescr = cLocExt.UsrDefSalesPersonID;
                    }
                }
                // TODO: Refactor to use PXSelectorAttribute

            }

            eventHandler.Cache.SetDefaultExt<LocationExtAddressExt.usrDefSalesPersonID>(row);
            eventHandler.Cache.SetDefaultExt<LocationExtAddressExt.usrDefSalesPersonDescr>(row);

        }

        // TODO: Rework this logic - should not update all locations when a new location is added.
        protected void _(Events.RowInserted<LocationExtAddress> eventHandler)
        {
            LocationExtAddress row = eventHandler.Row;
            Customer customer = Base.CurrentCustomer.Current;
            Address address = AccountService.FetchCustomerAddress(eventHandler.Cache.Graph, customer.BAccountID, customer.DefAddressID);

            if (row is null || customer is null || address is null || customer.AcctName is null || customer.AcctName == "")
                return;

            UpdateLocationTerritories(eventHandler.Cache.Graph, customer);
            UpdateDefSalesPeople(eventHandler.Cache.Graph, customer);

            Base.CurrentCustomer.View.RequestRefresh();
        }

        #endregion
        #region Data Types
        #endregion

        public int TerritoryUpdateCount
        {
            get => (PXContext.Session["TerritoryUpdateCount"] is null)
                ? 0
                : int.Parse(PXContext.Session["TerritoryUpdateCount"].ToString());

            set => PXContext.Session.SetString("TerritoryUpdateCount", value.ToString());
        }

        public int SalesPersonUpdateCount
        {
            get => (PXContext.Session["SalesPersonUpdateCount"] is null)
                ? 0
                : int.Parse(PXContext.Session["SalesPersonUpdateCount"].ToString());

            set => PXContext.Session.SetString("SalesPersonUpdateCount", value.ToString());
        }

        public virtual void UpdateLocationTerritories(PXGraph graph, Customer customer)
        {
            //TODO

            if (graph is null || customer is null
                || CustomerClassType.CustomerClassesByCountry.Contains(customer?.CustomerClassID))
                return;


            PXCache customerCache = Base.CurrentCustomer.Cache;

            PXCache addressCache = Base.BillAddress.Cache;
            BAccountExt extendedAccount = PXCache<BAccount>.GetExtension<BAccountExt>(customer);


            ////*** Old Sales Territory Fetch Logic ***//

            //string customerClassCD = Helper.CalculateCustomerClassCD(extendedAccount.UsrAccountType, customer.CustomerClassID);
            //SegmentValue salesTerritory = TerritoryService.FetchTerritoryByCustomerClass(graph, customerClassCD);

            //Boolean isDefaultingByCustomerClass = salesTerritory != null;

            //if (!isDefaultingByCustomerClass)
            //    salesTerritory = TerritoryService.FetchTerritoryByPostalCode(graph, address.PostalCode);

            //TODO Old Code Ends Here


            PXResultset<CRLocation> crLoc = SelectFrom<CRLocation>
            .Where<CRLocation.bAccountID.IsEqual<@P.AsInt>>
                .View.Select(graph, customer.BAccountID);


            foreach (CRLocation record in crLoc)
            {
                Address locAddress = SelectFrom<Address>
                    .Where<Address.bAccountID.IsEqual<@P.AsInt>
                    .And<Address.addressID.IsEqual<@P.AsInt>>>
                    .View.Select(graph, customer.BAccountID, record.DefAddressID);

                Location custLoc = SelectFrom<Location>
                    .Where<Location.bAccountID.IsEqual<@P.AsInt>
                    .And<Location.locationID.IsEqual<@P.AsInt>>>
                    .View.Select(graph, customer.BAccountID, record.LocationID);


                if (locAddress.CountryID != "US" && locAddress.CountryID != "CA")
                    continue;

                if ((locAddress?.CountryID == "US" && locAddress?.CountryID == "CA") && string.IsNullOrEmpty(locAddress.PostalCode))
                {
                    string dateErr = "Postal Code Cannot Be Blank";
                    PXUIFieldAttribute.SetWarning<Address.postalCode>(addressCache, addressCache.Current, dateErr);
                }

                StandaloneLocationExt extendedRecord = record.GetExtension<StandaloneLocationExt>();

                //StandaloneLocationExt extendedRecord = record.GetExtension<StandaloneLocationExt>();
                //***Old Sales Territory Fetch***//

                //if (customerCache.GetStatus(customer) == PXEntryStatus.Inserted)
                //{
                //    if (salesTerritory != null)
                //    {
                //        SegmentValueExt salesTerritoryExt = salesTerritory.GetExtension<SegmentValueExt>();

                //        extendedRecord.UsrSalesTerritoryCD = salesTerritory.Value;
                //        extendedRecord.UsrLegacyTerritoryCD = salesTerritoryExt.UsrLegacyDeptTerritory;
                //    }
                //    else
                //    {
                //        extendedRecord.UsrSalesTerritoryCD = extendedRecord.UsrSalesTerritoryCD;
                //        extendedRecord.UsrLegacyTerritoryCD = extendedRecord.UsrLegacyTerritoryCD;
                //    }
                //}
                //else
                //{
                //    if (extendedRecord.UsrSalesTerritoryOverride is true)
                //        continue;

                //    if (customer.DefAddressID != record.DefAddressID && !isDefaultingByCustomerClass)
                //        salesTerritory = TerritoryService.FetchTerritoryByPostalCode(graph, locAddress.PostalCode);

                //    if (salesTerritory != null)
                //    {
                //        SegmentValueExt salesTerritoryExt = salesTerritory.GetExtension<SegmentValueExt>();

                //        extendedRecord.UsrSalesTerritoryCD = salesTerritory.Value;
                //        extendedRecord.UsrLegacyTerritoryCD = salesTerritoryExt.UsrLegacyDeptTerritory;
                //    }
                //    else
                //    {
                //        extendedRecord.UsrSalesTerritoryCD = extendedRecord.UsrSalesTerritoryCD;
                //        extendedRecord.UsrLegacyTerritoryCD = extendedRecord.UsrLegacyTerritoryCD;
                //    }
                //}

                //***New Sales Territory Fetch***//

                string customerClassCD = Helpers.CalculateCustomerClassCD(extendedAccount.UsrAccountType, customer.CustomerClassID);
                SegmentValue custClassSalesTerritory = TerritoryService.FetchTerritoryByCustomerClass(graph, customerClassCD);
                bool defaultTerritory = custClassSalesTerritory != null; //(customerClassCD == "04" || customerClassCD == "04");
                string strSalesTerritory;
                string strLegacyTerritory;


                if (customerCache.GetStatus(customer) == PXEntryStatus.Inserted || extendedRecord.UsrSalesTerritoryOverride == null || extendedRecord.UsrSalesTerritoryOverride == false)
                {

                    if (defaultTerritory)
                    {

                        SegmentValueExt custClassSalesTerritoryExt = custClassSalesTerritory.GetExtension<SegmentValueExt>();

                        strSalesTerritory = custClassSalesTerritory?.Value;
                        strLegacyTerritory = custClassSalesTerritoryExt?.UsrLegacyDeptTerritory;
                    }
                    else
                    {
                        STSalesTerritoryMapping salesTerritory = TerritoryService.FetchTerritoryByPostalCode(graph, locAddress.PostalCode);

                        if (salesTerritory != null)
                        {
                            if (customer.CustomerClassID == CustomerClassType.Marine)
                            {
                                strSalesTerritory = salesTerritory?.MarineTerritoryCD;
                                strLegacyTerritory = salesTerritory?.MarineLegacyTerritoryCD;
                            }
                            else
                            {
                                strSalesTerritory = salesTerritory?.SalesTerritoryCD;
                                strLegacyTerritory = salesTerritory?.LegacyTerritoryCD;
                            }
                        }
                        else
                        {
                            strSalesTerritory = extendedRecord.UsrSalesTerritoryCD;
                            strLegacyTerritory = extendedRecord.UsrLegacyTerritoryCD;
                        }
                    }


                    extendedRecord.UsrSalesTerritoryCD = strSalesTerritory;
                    extendedRecord.UsrLegacyTerritoryCD = strLegacyTerritory;

                    Base.BaseLocations.Cache.Update(record);
                    Base.BaseLocations.View.RequestRefresh();

                    Base.BAccount.View.RequestRefresh();

                    graph.Persist();


                    //CustomerLocationMaint clGraph = PXGraph.CreateInstance<CustomerLocationMaint>();
                    //clGraph.BusinessAccount.Current = clGraph.BusinessAccount.Search<Location.bAccountID>(record.BAccountID);
                    //clGraph.Location.Current = custLoc;
                    //clGraph.LocationCurrent.Current = clGraph.LocationCurrent.Search<Location.locationID>(record.LocationID);

                    //Location clRow = clGraph.LocationCurrent.Current;

                    //if (clRow != null)
                    //{
                    //    LocationExt clRowExt = clRow.GetExtension<LocationExt>();

                    //    clRowExt.UsrSalesTerritoryCD = strSalesTerritory;
                    //    clRowExt.UsrLegacyTerritoryCD = strLegacyTerritory;
                    //}
                }
            }

        }

        public virtual void UpdateDefSalesPeople(PXGraph graph, Customer customer)
        {
            //TODO
            if (graph is null || customer is null || customer?.CustomerClassID is null)
                return;

            List<CustSalesPeople> recordsToInsert = new List<CustSalesPeople>();
            Base.RowSelected.RemoveHandler<CustSalesPeople>(this.Base.CustSalesPeople_RowSelected);
            Base.RowInserting.RemoveHandler<CustSalesPeople>(this.Base.CustSalesPeople_RowInserting);


            PXResultset<Location> cLoc = SelectFrom<Location>
                .Where<Location.bAccountID.IsEqual<@P.AsInt>>
                    .View.Select(graph, customer.BAccountID);

            foreach (Location record in cLoc)
            {
                Address locAddress = AccountService.FetchCustomerAddress(graph, record.BAccountID, record.DefAddressID);
                CRLocation crLoc = SelectFrom<CRLocation>
                    .Where<CRLocation.bAccountID.IsEqual<@P.AsInt>
                    .And<CRLocation.locationID.IsEqual<@P.AsInt>>>
                    .View.Select(graph, customer.BAccountID, record.LocationID);

                LocationExt extendedRecord = record.GetExtension<LocationExt>();
                StandaloneLocationExt crLocExt = crLoc.GetExtension<StandaloneLocationExt>();

                BAccountExt extendedAccount = customer.GetExtension<BAccountExt>();

                if (extendedRecord is null || extendedAccount is null
                    || (extendedRecord?.UsrSalesPersonOverride ?? false) is true
                    || extendedRecord.UsrSalesTerritoryCD is null)
                    continue;

                if (extendedRecord.UsrLegacyTerritoryCD is null)
                {
                    SegmentValue salesTerritory = TerritoryService.FetchSalesTerritory(graph, extendedRecord.UsrSalesTerritoryCD);
                    extendedRecord.UsrLegacyTerritoryCD = salesTerritory.GetExtension<SegmentValueExt>().UsrLegacyDeptTerritory;
                }

                STLocationAddress locationAddress = new STLocationAddress()
                {
                    LocationID = record.LocationID,
                    AddressID = locAddress.AddressID,
                    State = locAddress.State,
                    CountryID = locAddress.CountryID,
                    SalesTerritoryCD = extendedRecord.UsrSalesTerritoryCD,
                    LegacyTerritoryCD = extendedRecord.UsrLegacyTerritoryCD
                };

                //TODO
                //string newSalesPersonCD = Helper.CalculateDefaultSalesPersonCD(extendedAccount.UsrAccountType, customer.CustomerClassID, locationAddress);
                //SalesPerson newSalesPerson = null;
                //newSalesPerson = LocationService.FetchSalesPersonByCD(graph, newSalesPersonCD);


                //***New Sales Rep Fetch***//
                string customerClassCD = Helpers.CalculateCustomerClassCD(extendedAccount.UsrAccountType, customer.CustomerClassID);
                bool defaultTerritory = (customerClassCD == "04" || customerClassCD == "11" || customerClassCD == "HSE" || customerClassCD == "POS");
                string newSalesPersonCD = null;
                string strSubChannel = AccountService.FetchCustomerSubChannel(graph, customer);

                if (defaultTerritory)
                {
                    newSalesPersonCD = Helpers.CalculateDefaultSalesPersonCD(extendedAccount.UsrAccountType, customer.CustomerClassID, locationAddress);
                }
                else
                {
                    newSalesPersonCD = TerritoryService.FetchDefaultSalesPersonCD(graph, extendedRecord.UsrSalesTerritoryCD, locAddress.PostalCode, customerClassCD, strSubChannel);
                }

                //STSalesDepartmentReps newSalesRepData = TerritoryService.FetchDefaultSalesPersonCD(graph, extendedRecord.UsrSalesTerritoryCD, customerClassCD);
                //if (newSalesRepData is null)
                //    return;

                SalesPerson newSalesPerson = LocationService.FetchSalesPersonByCD(graph, newSalesPersonCD);


                CustSalesPeople oldSalesPerson = LocationService.FetchCustLocationSalesPerson(graph, record.BAccountID, record.LocationID);

                if (newSalesPerson == null && oldSalesPerson == null)
                    return;

                if (newSalesPerson is null || newSalesPerson.SalesPersonID == (oldSalesPerson?.SalesPersonID ?? null))
                {
                    CustSalesPeopleExt oldSalesPersonExt = oldSalesPerson.GetExtension<CustSalesPeopleExt>();
                    if (oldSalesPersonExt.UsrSalesTerritoryCD != extendedRecord.UsrSalesTerritoryCD)
                    {
                        oldSalesPersonExt.UsrSalesTerritoryCD = extendedRecord.UsrSalesTerritoryCD;
                    }
                    if (oldSalesPersonExt.UsrLegacyTerritoryCD != extendedRecord.UsrLegacyTerritoryCD)
                    {
                        oldSalesPersonExt.UsrLegacyTerritoryCD = extendedRecord.UsrLegacyTerritoryCD;
                    }
                    continue;
                }

                foreach (CustSalesPeople salesPerson in LocationService.FetchAllSalesPeopleForLocation(graph, record.BAccountID, record.LocationID))
                {
                    Base.SalesPersons.Delete(salesPerson);
                }

                recordsToInsert.Add(new CustSalesPeople()
                {
                    SalesPersonID = newSalesPerson.SalesPersonID,
                    BAccountID = record.BAccountID,
                    LocationID = record.LocationID,
                    IsDefault = true,
                    CommisionPct = newSalesPerson.CommnPct ?? 0.00M
                });

                extendedRecord.UsrDefSalesPersonID = newSalesPerson.SalesPersonID;
                crLocExt.UsrDefSalesPersonID = newSalesPerson.SalesPersonID;
                Base.BaseLocations.Update(crLoc);
            }

            foreach (CustSalesPeople newSalesPerson in recordsToInsert)
            {
                Base.SalesPersons.Insert(newSalesPerson);
            }

            //Base.Locations.Cache.IsDirty = true;
            //Base.Locations.View.RequestRefresh();
            Base.SalesPersons.Cache.IsDirty = true;
            Base.SalesPersons.View.RequestRefresh();

            Base.RowInserting.AddHandler<CustSalesPeople>(this.Base.CustSalesPeople_RowInserting);
            Base.RowSelected.AddHandler<CustSalesPeople>(this.Base.CustSalesPeople_RowSelected);
        }

        public virtual void UpdateDefLocationTerritory(PXGraph graph)
        {
            /*
                Sets the value of the SalesTerritoryCD based 
                on the defaulting rules.
            */
            //TODO

            CRLocation location = Base.BaseLocations.Current;

            if (graph is null) return;

            if (location is null || location.LocationCD is null)
                return;

            BAccount account = AccountService.FetchBAccount(graph, location?.BAccountID);
            Customer customer = AccountService.FetchCustomer(graph, location?.BAccountID);
            Address address = AccountService.FetchCustomerAddress(graph, location?.BAccountID, location?.DefAddressID);
            PXCache addressCache = Base.BillAddress.Cache;

            if (address is null || account is null || customer is null)
                return;

            StandaloneLocationExt extendedLocation = location.GetExtension<StandaloneLocationExt>();
            BAccountExt extendedAccount = account.GetExtension<BAccountExt>();

            if (extendedLocation is null || extendedAccount is null)
                return;

            if (extendedLocation.UsrSalesTerritoryOverride is true
                || (address?.CountryID != "US" && address?.CountryID != "CA")
                || (CustomerClassType.CustomerClassesByCountry.Contains(customer?.CustomerClassID)))
                return;

            if ((address?.CountryID != "US" && address?.CountryID != "CA") && string.IsNullOrEmpty(address.PostalCode))
            {
                string dateErr = "Postal Code Cannot Be Blank";
                PXUIFieldAttribute.SetWarning<Address.postalCode>(addressCache, addressCache.Current, dateErr);
            }

            //*** Old Sales Territory Fetch Logic ***//


            //string customerClassCD = Helper.CalculateCustomerClassCD(extendedAccount.UsrAccountType, customer.CustomerClassID);
            //SegmentValue salesTerritory = TerritoryService.FetchTerritoryByCustomerClass(graph, customerClassCD);

            //if (salesTerritory?.Value is null)
            //    salesTerritory = TerritoryService.FetchTerritoryByPostalCode(graph, address.PostalCode);

            //string customerClassCD = Helper.CalculateCustomerClassCD(extendedAccount.UsrAccountType, customer.CustomerClassID);
            //SegmentValue salesTerritory = TerritoryService.FetchTerritoryByCustomerClass(graph, customerClassCD);

            //if (salesTerritory?.Value is null)
            //    salesTerritory = TerritoryService.FetchTerritoryByPostalCode(graph, address.PostalCode);

            //extendedLocation.UsrSalesTerritoryCD = salesTerritory?.Value ?? extendedLocation?.UsrSalesTerritoryCD;


            //*** New Fetch Logic***//
            string customerClassCD = Helpers.CalculateCustomerClassCD(extendedAccount.UsrAccountType, customer.CustomerClassID);
            SegmentValue custClassSalesTerritory = TerritoryService.FetchTerritoryByCustomerClass(graph, customerClassCD);
            bool defaultTerritory = custClassSalesTerritory != null; //(customerClassCD == "04" || customerClassCD == "04");
            string strSalesTerritory;
            string strLegacyTerritory;

            if (defaultTerritory)
            {
                //SegmentValue custClassSalesTerritory = TerritoryService.FetchTerritoryByCustomerClass(graph, customerClassCD);

                SegmentValueExt custClassSalesTerritoryExt = custClassSalesTerritory.GetExtension<SegmentValueExt>();

                strSalesTerritory = custClassSalesTerritory?.Value;
                strLegacyTerritory = custClassSalesTerritoryExt?.UsrLegacyDeptTerritory;
            }
            else
            {
                STSalesTerritoryMapping salesTerritory = TerritoryService.FetchTerritoryByPostalCode(graph, address.PostalCode);

                if (salesTerritory != null)
                {
                    if (customer.CustomerClassID == CustomerClassType.Marine)
                    {
                        strSalesTerritory = salesTerritory?.MarineTerritoryCD;
                        strLegacyTerritory = salesTerritory?.MarineLegacyTerritoryCD;
                    }
                    else
                    {
                        strSalesTerritory = salesTerritory?.SalesTerritoryCD;
                        strLegacyTerritory = salesTerritory?.LegacyTerritoryCD;
                    }
                }
                else
                {
                    strSalesTerritory = extendedLocation.UsrSalesTerritoryCD;
                    strLegacyTerritory = extendedLocation.UsrLegacyTerritoryCD;
                }
            }


            //STSalesTerritoryMapping salesTerritory = TerritoryService.FetchTerritoryByPostalCode(graph, address.PostalCode);


            extendedLocation.UsrSalesTerritoryCD = strSalesTerritory; //extendedLocation.UsrSalesTerritoryCD;
            extendedLocation.UsrLegacyTerritoryCD = strLegacyTerritory; //extendedLocation.UsrLegacyTerritoryCD;

            //Base.BaseLocations.Cache.IsDirty = true;
            //Base.BaseLocations.Cache.PersistUpdated(location);
            //Base.BaseLocations.View.RequestRefresh();

            Base.BaseLocations.Update(location);
            Base.BaseLocations.View.RequestRefresh();

        }

        public virtual void UpdateDefLocationSalesPerson(PXGraph graph)
        {
            /*
                Sets the value of the DefSalespersonID based 
                on the defaulting rules.
            */

            if (graph is null) return;
            CRLocation location = Base.BaseLocations.Current;

            if (location is null || location.LocationCD is null)
                return;

            BAccount account = AccountService.FetchBAccount(graph, location.BAccountID);
            Customer customer = AccountService.FetchCustomer(graph, location?.BAccountID);
            Address address = AccountService.FetchCustomerAddress(graph, location.BAccountID, location.DefAddressID);

            if (address is null || account is null || customer is null)
                return;

            StandaloneLocationExt extendedLocation = location.GetExtension<StandaloneLocationExt>();
            BAccountExt extendedAccount = account.GetExtension<BAccountExt>();
            SegmentValue salesTerritory = TerritoryService.FetchSalesTerritory(graph, extendedLocation.UsrSalesTerritoryCD);

            if (extendedLocation is null || extendedAccount is null)
                return;

            if (extendedLocation.UsrSalesPersonOverride is true || salesTerritory is null)
                return;

            SegmentValueExt extendedSalesTerritory = salesTerritory.GetExtension<SegmentValueExt>();

            STLocationAddress locationAddress = new STLocationAddress()
            {
                LocationID = location.LocationID,
                AddressID = address.AddressID,
                PostalCode = address.PostalCode,
                State = address.State,
                CountryID = address.CountryID,
                SalesTerritoryCD = salesTerritory.Value ?? "",
                LegacyTerritoryCD = extendedSalesTerritory.UsrLegacyDeptTerritory ?? ""
            };

            //TODO
            string customerClassCD = Helpers.CalculateCustomerClassCD(extendedAccount.UsrAccountType, customer.CustomerClassID);
            bool defaultTerritory = (customerClassCD == "04" || customerClassCD == "11" || customerClassCD == "HSE" || customerClassCD == "POS");
            string newSalesPersonCD = null;
            string strSubChannel = AccountService.FetchCustomerSubChannel(graph, customer);

            if (defaultTerritory)
            {
                newSalesPersonCD = Helpers.CalculateDefaultSalesPersonCD(extendedAccount.UsrAccountType, customer.CustomerClassID, locationAddress);
            }
            else
            {
                newSalesPersonCD = TerritoryService.FetchDefaultSalesPersonCD(graph, salesTerritory.Value, address.PostalCode, customerClassCD, strSubChannel);
            }



            //string newSalesPersonCD = Helper.CalculateDefaultSalesPersonCD(extendedAccount.UsrAccountType, customer.CustomerClassID, locationAddress);
            SalesPerson newSalesPerson = null;

            //newSalesPerson = LocationService.FetchSalesPersonByCD(graph, newSalesPersonCD);

            //if (newSalesPersonCD != null)
            //{
            //    newSalesPerson = LocationService.FetchSalesPersonByCD(graph, newSalesPersonCD);
            //}
            //else if (extendedLocation.UsrDefSalesPersonID != null)
            //{
            //    newSalesPerson = LocationService.FetchSalesPersonByID(graph, extendedLocation.UsrDefSalesPersonID);
            //}


            //******//
            //STSalesDepartmentReps newSalesRepData = TerritoryService.FetchDefaultSalesPersonCD(graph, salesTerritory.Value);
            //SalesPerson newSalesPerson = null;
            //if (newSalesRepData is null)
            //    return;

            newSalesPerson = LocationService.FetchSalesPersonByCD(graph, newSalesPersonCD);

            int? result = (newSalesPerson is null)
                ? extendedLocation.UsrDefSalesPersonID
                : newSalesPerson.SalesPersonID;

            extendedLocation.UsrDefSalesPersonID = result;
            newSalesPerson = LocationService.FetchSalesPersonByID(graph, extendedLocation.UsrDefSalesPersonID);

            foreach (CustSalesPeople salesPerson in LocationService.FetchAllSalesPeopleForLocation(graph, location.BAccountID, location.LocationID))
            {
                Base.SalesPersons.Delete(salesPerson);
            }

            CustSalesPeople recordToInsert = new CustSalesPeople();

            if (newSalesPerson == null && extendedLocation.UsrDefSalesPersonID == null)
                return;

            if (newSalesPerson != null)
            {
                recordToInsert.SalesPersonID = newSalesPerson.SalesPersonID;
                recordToInsert.CommisionPct = newSalesPerson.CommnPct;
            }
            else
            {
                recordToInsert.SalesPersonID = extendedLocation.UsrDefSalesPersonID;
                recordToInsert.CommisionPct = 0.00M;
            }
            //recordToInsert.SalesPersonID =  ?? extendedLocation.UsrDefSalesPersonID; 
            //recordToInsert.SalesPersonID = result;
            recordToInsert.BAccountID = location.BAccountID;
            recordToInsert.LocationID = location.LocationID;
            recordToInsert.IsDefault = true;

            Base.SalesPersons.Insert(recordToInsert);

            Base.SalesPersons.Cache.IsDirty = true;
            Base.SalesPersons.View.RequestRefresh();

            Base.RowInserting.AddHandler<CustSalesPeople>(this.Base.CustSalesPeople_RowInserting);
            Base.RowSelected.AddHandler<CustSalesPeople>(this.Base.CustSalesPeople_RowSelected);

        }
    }
}
