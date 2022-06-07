using System;
using System.Linq;
using System.Collections.Generic;

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.STI.STICustom.Common;
using PX.STI.STICustom.DAC;
using PX.STI.STICustom.DACExt;
using PX.STI.STICustom.Service;
using CRLocation = PX.Objects.CR.Standalone.Location;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class CustomerLocationMaintExt : PXGraphExtension<CustomerLocationMaint>
    {
        #region Actions
        public PXAction<PX.Objects.AR.Customer> CalcSalesRepData;

        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Update Sales Rep Data")]
        public void calcLocSalesRepData()
        {
            Location location = Base.LocationCurrent.Current;

            UpdateLocationTerritory(base.Base, location);
            UpdateDefSalesPerson(base.Base);
        }
        #endregion
        #region Events

        protected void _(Events.FieldUpdated<Address, Address.postalCode> eventHandler)
        {
            /*
                Updates the SalesTerritoryCD on the Location when
                the Address PostalCode changes.
            */

            Address row = eventHandler.Row;
            Location location = Base.Location.Current;

            if (row is null || location is null)
                return;

            UpdateLocationTerritory(eventHandler.Cache.Graph, location);
            UpdateDefSalesPerson(eventHandler.Cache.Graph);
        }

        protected void _(Events.FieldUpdated<Address, Address.state> eventHandler)
        {
            /*
                Updates the SalesTerritoryCD on the Location when
                the Address State changes.
            */

            Address row = eventHandler.Row;
            Location location = Base.Location.Current;

            if (row is null || location is null)
                return;

            UpdateLocationTerritory(eventHandler.Cache.Graph, location);
            UpdateDefSalesPerson(eventHandler.Cache.Graph);
        }
        protected void _(Events.FieldUpdated<Location, Location.overrideAddress> eventHandler)
        {
            /*
                Updates the SalesTerritoryCD on the Location when
                the Location Address updates.
            */

            Location row = eventHandler.Row;

            if (row is null)
                return;

            UpdateLocationTerritory(eventHandler.Cache.Graph, row);
            UpdateDefSalesPerson(eventHandler.Cache.Graph);
        }

        protected void _(Events.FieldUpdated<CRLocation, CRLocation.overrideAddress> eventHandler)
        {
            /*
                Updates the SalesTerritoryCD on the Location when
                the Location Address updates.
            */

            CRLocation row = eventHandler.Row;
            Location location = Base.Location.Current;

            if (row is null || location is null)
                return;

            UpdateLocationTerritory(eventHandler.Cache.Graph, location);
            UpdateDefSalesPerson(eventHandler.Cache.Graph);
        }

        protected void _(Events.FieldUpdated<Location, LocationExt.usrSalesTerritoryCD> eventHandler)
        {
            Location row = eventHandler.Row;

            if (row is null)
                return;

            UpdateLocationTerritory(eventHandler.Cache.Graph, row);
            UpdateDefSalesPerson(eventHandler.Cache.Graph);
        }

        protected void _(Events.FieldUpdated<CRLocation, LocationExt.usrSalesTerritoryCD> eventHandler)
        {
            /*
                Updates the SalesTerritoryCD on the Location when
                the Location Address updates.
            */

            CRLocation row = eventHandler.Row;
            Location location = Base.Location.Current;

            if (row is null || location is null)
                return;

            UpdateLocationTerritory(eventHandler.Cache.Graph, location);
            UpdateDefSalesPerson(eventHandler.Cache.Graph);
        }

        protected void _(Events.FieldUpdated<Location, LocationExt.usrSalesTerritoryOverride> eventHandler)
        {
            /*
                Runs defaulting logic to reset the value of the Sales Territory.
            */

            Location row = eventHandler.Row;

            if (row is null)
                return;

            UpdateLocationTerritory(eventHandler.Cache.Graph, row);
            UpdateDefSalesPerson(eventHandler.Cache.Graph);
        }

        protected void _(Events.FieldUpdated<Location, LocationExt.usrSalesPersonOverride> eventHandler)
        {
            /*
                Enables or disables the DefSalesPersonID field (how??) and runs defaulting
                logic to reset the value of the DefSalesPersonID.
            */

            Location row = eventHandler.Row;

            if (row is null)
                return;

            UpdateLocationTerritory(eventHandler.Cache.Graph, row);
            UpdateDefSalesPerson(eventHandler.Cache.Graph);
        }

        protected void _(Events.FieldUpdated<Location, Location.cFOBPointID> eventHandler)
        {
            /*
                Sets the CShipTermsID to match the CFOBPointID.
            */

            Location row = eventHandler.Row;

            if (row is null)
                return;

            row.CShipTermsID = row.CFOBPointID;
        }

        protected void _(Events.FieldUpdated<Location, Location.descr> eventHandler)
        {
            Location row = eventHandler.Row;
            if (row is null) return;

            UpdateLocationTerritory(eventHandler.Cache.Graph, row);
            UpdateDefSalesPerson(eventHandler.Cache.Graph);
        }

        protected void _(Events.RowSelecting<Location> eventHandler)
        {
            Location row = eventHandler.Row;
            if (row is null) return;

            LocationExt extRow = row.GetExtension<LocationExt>();

            if (extRow != null)
            {
                UpdateLocationTerritory(eventHandler.Cache.Graph, row);
                UpdateDefSalesPerson(eventHandler.Cache.Graph);
                //if (extRow.UsrDefSalesPersonID == null)
                //{
                //    UpdateDefSalesPerson(eventHandler.Cache.Graph);
                //}
            }
        }

        protected void _(Events.RowInserting<CRLocation> eventHandler)
        {
            CRLocation row = eventHandler.Row;

            if (row is null)
                return;
        }

        protected void _(Events.RowUpdated<Location> eventHandler)
        {
            Location row = eventHandler.Row;

            if (row is null)
                return;
        }

        protected void _(Events.RowPersisting<Location> eventHandler)
        {
            /*
                Checks if the SalesTerritryCD and DefSalespersonID
                fields are null and raises exception handling.
            */

            Location row = eventHandler.Row;

            if (row is null)
                return;

            LocationExt extendedLocation = eventHandler.Cache.GetExtension<LocationExt>(row) ?? new LocationExt();


            if (extendedLocation.UsrSalesTerritoryCD is null)
            {
                if (extendedLocation.UsrSalesTerritoryOverride == false)
                {
                    UpdateLocationTerritory(eventHandler.Cache.Graph, row);
                }

                if (extendedLocation.UsrSalesTerritoryCD is null)
                {
                    eventHandler.Cache.RaiseExceptionHandling<LocationExt.usrSalesTerritoryCD>(row, null,
                        new PXSetPropertyException(CustomMessages.SalesTerritoryRequired, PXErrorLevel.Error));
                }
            }

            if (extendedLocation.UsrDefSalesPersonID is null)
            {
                if (extendedLocation.UsrSalesTerritoryOverride == false)
                {
                    UpdateDefSalesPerson(eventHandler.Cache.Graph);
                }

                if (extendedLocation.UsrDefSalesPersonID is null)
                {
                    eventHandler.Cache.RaiseExceptionHandling<LocationExt.usrDefSalesPersonID>(row, null,
                    new PXSetPropertyException(CustomMessages.SalespersonRequired, PXErrorLevel.Error));
                }
            }



        }

        protected void _(Events.RowPersisted<Location> eventHandler)
        {
            /*
                Creates a new default salesperson for the location if it doesn't exist,
                and removes the old default salesperson if it exists.
            */

            Location row = eventHandler.Row;
            if (row is null) return;

            LocationExt extendedRow = row.GetExtension<LocationExt>();
            CustSalesPeople oldSalesPerson = LocationService.FetchCustLocationSalesPerson(eventHandler.Cache.Graph, row.BAccountID, row.LocationID);
            SalesPerson newSalesPerson = LocationService.FetchSalesPersonByID(eventHandler.Cache.Graph, extendedRow.UsrDefSalesPersonID);

            if (newSalesPerson is null)
                return;

            bool isDeleted = eventHandler.Cache.GetStatus(row) == PXEntryStatus.Deleted;

            if ((oldSalesPerson is null || oldSalesPerson.SalesPersonID != newSalesPerson.SalesPersonID) && !isDeleted)
            {
                foreach (CustSalesPeople record in LocationService.FetchAllSalesPeopleForLocation(eventHandler.Cache.Graph, row.BAccountID, row.LocationID))
                {
                    SalesPeople.Cache.Delete(record);
                }

                SalesPeople.Cache.Persist(PXDBOperation.Delete);

                SalesPeople.Cache.Insert(new CustSalesPeople()
                {
                    SalesPersonID = newSalesPerson.SalesPersonID,
                    BAccountID = row.BAccountID,
                    LocationID = row.LocationID,
                    IsDefault = true,
                    CommisionPct = newSalesPerson.CommnPct ?? 0.00M
                });

                SalesPeople.Cache.Persist(PXDBOperation.Insert);
            }
        }

        #endregion
        #region Data Types
        #endregion

        public SelectFrom<CustSalesPeople>
            .Where<CustSalesPeople.bAccountID.IsEqual<Customer.bAccountID.FromCurrent>
            .And<CustSalesPeople.locationID.IsEqual<Location.locationID.FromCurrent>>>
            .View SalesPeople;

        public virtual void UpdateDefLocationTerritory(PXGraph graph, PX.Objects.CR.Standalone.Location location)
        {
            /*
                Sets the value of the SalesTerritoryCD based 
                on the defaulting rules.
            */

            if (graph is null) return;

            PXCache locationCache = Base.Location.Cache;
        }


        public virtual void UpdateLocationTerritory(PXGraph graph, Location location)
        {
            /*
                Sets the value of the SalesTerritoryCD based 
                on the defaulting rules.
            */

            if (graph is null) return;

            PXCache locationCache = Base.Location.Cache;
            PXCache addressCache = Base.Address.Cache;

            if (locationCache is null || location is null || location.LocationCD is null)
                return;

            BAccount account = AccountService.FetchBAccount(graph, location?.BAccountID);
            Customer customer = AccountService.FetchCustomer(graph, location?.BAccountID);
            Address address = AccountService.FetchCustomerAddress(graph, location?.BAccountID, location?.DefAddressID);

            if (address is null || account is null || customer is null)
                return;

            LocationExt extendedLocation = location.GetExtension<LocationExt>();
            BAccountExt extendedAccount = account.GetExtension<BAccountExt>();

            if (extendedLocation.UsrSalesTerritoryOverride is true
                || (address?.CountryID != "US" && address?.CountryID != "CA")
                || (CustomerClassType.CustomerClassesByCountry.Contains(customer?.CustomerClassID)))
                return;

            if ((address?.CountryID == "US" && address?.CountryID == "CA") && string.IsNullOrEmpty(address.PostalCode))
            {
                string dateErr = "Postal Code Cannot Be Blank";
                PXUIFieldAttribute.SetWarning<Address.postalCode>(addressCache, addressCache.Current, dateErr);
            }

            ////*** Old Sales Territory Fetch Logic ***//

            //string customerClassCD = Helper.CalculateCustomerClassCD(extendedAccount.UsrAccountType, customer.CustomerClassID);
            //SegmentValue salesTerritory = TerritoryService.FetchTerritoryByCustomerClass(graph, customerClassCD);

            //if (salesTerritory?.Value is null)
            //    salesTerritory = TerritoryService.FetchTerritoryByPostalCode(graph, address.PostalCode);


            //locationCache.SetValueExt<LocationExt.usrSalesTerritoryCD>(location, salesTerritory?.Value ?? extendedLocation.UsrSalesTerritoryCD);
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

            //STSalesTerritoryMapping salesTerritory = TerritoryService.FetchTerritoryByPostalCode(graph, address.PostalCode);
            //string strSalesTerritoryCD = null;

            //if (salesTerritory != null)
            //{
            //    if (customer.CustomerClassID == CustomerClassType.Marine)
            //    {
            //        strSalesTerritoryCD = salesTerritory.MarineTerritoryCD;
            //    }
            //    else
            //    {
            //        strSalesTerritoryCD = salesTerritory.SalesTerritoryCD;
            //    }
            //}

            //locationCache.SetValueExt<LocationExt.usrSalesTerritoryCD>(location, strSalesTerritoryCD); //salesTerritory?.Value ?? extendedLocation.UsrSalesTerritoryCD);
        }

        public virtual void UpdateDefSalesPerson(PXGraph graph)
        {
            /*
                Sets the value of the DefSalespersonID based 
                on the defaulting rules.
            */

            if (graph is null) return;
            PXCache locationCache = Base.Location.Cache;
            Location location = Base.Location.Current;

            if (locationCache is null || location is null || location.LocationCD is null)
                return;

            BAccount account = AccountService.FetchBAccount(graph, location.BAccountID);
            Customer customer = AccountService.FetchCustomer(graph, location?.BAccountID);
            Address address = AccountService.FetchCustomerAddress(graph, location.BAccountID, location.DefAddressID);

            if (address is null || account is null || customer is null)
                return;

            LocationExt extendedLocation = location.GetExtension<LocationExt>();
            BAccountExt extendedAccount = account.GetExtension<BAccountExt>();
            SegmentValue salesTerritory = TerritoryService.FetchSalesTerritory(graph, extendedLocation.UsrSalesTerritoryCD);

            if (extendedLocation.UsrSalesPersonOverride is true || salesTerritory is null)
                return;

            SegmentValueExt extendedSalesTerritory = salesTerritory.GetExtension<SegmentValueExt>();

            STLocationAddress locationAddress = new STLocationAddress()
            {
                LocationID = location.LocationID,
                AddressID = address.AddressID,
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
            //STSalesDepartmentReps newSalesRepData = TerritoryService.FetchDefaultSalesPersonCD(graph, salesTerritory.Value);
            //if (newSalesRepData is null)
            //    return;

            SalesPerson newSalesPerson = LocationService.FetchSalesPersonByCD(graph, newSalesPersonCD);

            int? result = (newSalesPerson is null)
                ? extendedLocation.UsrDefSalesPersonID
                : newSalesPerson.SalesPersonID;

            locationCache.SetValue<LocationExt.usrDefSalesPersonID>(location, result);
        }
    }
}
