using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System.Linq;
using System;
using System.Collections;
using PX.STI.STICustom.DAC;
using PX.STI.STICustom.DACExt;
using PX.STI.STICustom.Common;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class SOShipmentEntryExt : PXGraphExtension<SOShipmentEntry>
    {
        /*
            This graph extension is used to customize the base UPS integration logic
            inside Acumatica and provide us with greater control of the shipments
            that are provided to UPS.
        */

        #region Actions
        public PXAction<SOShipment> action;
        [PXButton]
        [PXUIField(DisplayName = "Actions", MapEnableRights = PXCacheRights.Select)]
        protected IEnumerable Action(PXAdapter adapter
                                    , [PXIntList(new int[] { 1 }
                                ,new string[] { "Confirm Shipment" })
                                ,PXInt] int? actionID)
        {
            //actionID = 1 means the Confirm Shipment action was the one invoked
            if (actionID == 1)
            {
                SOShipmentExt extendrow = null;
                INSite site = null;
                SOOrderShipment order = null;
                SOShipmentAddress orderadd = null;
                STSiteTransitEst transit = null;
                SOPackageDetailEx packdata = null;

                order = SelectFrom<SOOrderShipment>
                    .Where<SOOrderShipment.shipmentNbr.IsEqual<@P.AsString>>
                    .View.Select(Base, Base.Document.Current.ShipmentNbr);

                if (order.OrderType == "ED")
                {

                    extendrow = Base.Document.Current.GetExtension<SOShipmentExt>();

                    site = SelectFrom<INSite>
                        .Where<INSite.siteID.IsEqual<@P.AsInt>>
                        .View.Select(Base, Base.Document.Current.SiteID);

                    orderadd = SelectFrom<SOShipmentAddress>
                        .Where<SOShipmentAddress.addressID.IsEqual<@P.AsInt>>
                        .View.Select(Base, Base.Document.Current.ShipAddressID);

                    packdata = SelectFrom<SOPackageDetailEx>
                        .Where<SOPackageDetailEx.shipmentNbr.IsEqual<@P.AsString>>
                        .View.Select(Base, Base.Document.Current.ShipmentNbr);

                    transit = SelectFrom<STSiteTransitEst>
                        .Where<STSiteTransitEst.siteID.IsEqual<@P.AsInt>
                        .And<STSiteTransitEst.countryID.IsEqual<@P.AsString>>
                        .And<STSiteTransitEst.stateID.IsEqual<@P.AsString>>>
                        .View.Select(Base, Base.Document.Current.SiteID, orderadd.CountryID, orderadd.State);

                    if (transit != null && packdata != null)
                    {
                        int tdays = (int)transit.EstTransitDays;
                        DateTime estShipDate = DateTime.Now;

                        for (int inti = 1; inti <= tdays; inti++)
                        {
                            estShipDate = estShipDate.AddDays(1);

                            if (estShipDate.DayOfWeek == DayOfWeek.Saturday)
                            {
                                estShipDate = estShipDate.AddDays(2);
                            }
                            else if (estShipDate.DayOfWeek == DayOfWeek.Sunday)
                            {
                                estShipDate = estShipDate.AddDays(1);
                            }
                        }

                        string dt = estShipDate.ToShortDateString();

                        extendrow.ISPSDeliveryDate = DateTime.Parse(dt);
                        extendrow.ISPSDelTime = 720;
                        extendrow.ISPSProNumber = packdata.TrackNumber;

                        if (extendrow.UsrTotalPallets == null || extendrow.UsrTotalPallets == 0)
                        {
                            extendrow.ISPSTotalNumPallets = Base.Document.Current.PackageCount;
                        }
                        else
                        {
                            extendrow.ISPSTotalNumPallets = extendrow.UsrTotalPallets;
                        }

                        Base.Document.Update(Base.Document.Current);
                    }
                }
            }

            //calls the basic action that was invoked
            return Base.action.Press(adapter);
        }
        #endregion
        #region Events
        #endregion
        #region Data Types
        #endregion

        public delegate IEnumerable ConfirmShipmentActionDelegate(PXAdapter adapter);
        [PXOverride]
        public IEnumerable ConfirmShipmentAction(PXAdapter adapter, ConfirmShipmentActionDelegate baseMethod)
        {
            SOShipmentExt extendrow = null;
            INSite site = null;
            SOOrderShipment order = null;
            SOShipmentAddress orderadd = null;
            STSiteTransitEst transit = null;
            SOPackageDetailEx packdata = null;

            order = SelectFrom<SOOrderShipment>
                .Where<SOOrderShipment.shipmentNbr.IsEqual<@P.AsString>>
                .View.Select(Base, Base.Document.Current.ShipmentNbr);

            if (order.OrderType == "ED")
            {

                extendrow = Base.Document.Current.GetExtension<SOShipmentExt>();

                site = SelectFrom<INSite>
                    .Where<INSite.siteID.IsEqual<@P.AsInt>>
                    .View.Select(Base, Base.Document.Current.SiteID);

                orderadd = SelectFrom<SOShipmentAddress>
                    .Where<SOShipmentAddress.addressID.IsEqual<@P.AsInt>>
                    .View.Select(Base, Base.Document.Current.ShipAddressID);

                packdata = SelectFrom<SOPackageDetailEx>
                    .Where<SOPackageDetailEx.shipmentNbr.IsEqual<@P.AsString>>
                    .View.Select(Base, Base.Document.Current.ShipmentNbr);

                transit = SelectFrom<STSiteTransitEst>
                    .Where<STSiteTransitEst.siteID.IsEqual<@P.AsInt>
                    .And<STSiteTransitEst.countryID.IsEqual<@P.AsString>>
                    .And<STSiteTransitEst.stateID.IsEqual<@P.AsString>>>
                    .View.Select(Base, Base.Document.Current.SiteID, orderadd.CountryID, orderadd.State);

                if (transit != null && packdata != null)
                {
                    int tdays = (int)transit.EstTransitDays;
                    DateTime estShipDate = DateTime.Now;

                    for (int inti = 1; inti <= tdays; inti++)
                    {
                        estShipDate = estShipDate.AddDays(1);

                        if (estShipDate.DayOfWeek == DayOfWeek.Saturday)
                        {
                            estShipDate = estShipDate.AddDays(2);
                        }
                        else if (estShipDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            estShipDate = estShipDate.AddDays(1);
                        }
                    }

                    string dt = estShipDate.ToShortDateString();

                    extendrow.ISPSDeliveryDate = DateTime.Parse(dt);
                    extendrow.ISPSDelTime = 720;
                    extendrow.ISPSProNumber = packdata.TrackNumber;

                    if (extendrow.UsrTotalPallets == null || extendrow.UsrTotalPallets == 0)
                    {
                        extendrow.ISPSTotalNumPallets = Base.Document.Current.PackageCount;
                    }
                    else
                    {
                        extendrow.ISPSTotalNumPallets = extendrow.UsrTotalPallets;
                    }

                    Base.Document.Update(Base.Document.Current);
                }
            }

            return baseMethod(adapter);
        }

        public delegate void ShipPackagesDelegate(SOShipment shiporder);


        [PXOverride()]
        public virtual void ShipPackages(SOShipment shiporder, ShipPackagesDelegate baseMethod)
        {
            /*
                Overriding the ShipPackages method as the cleanest point
                to interrupt base logic and perform our validation and checks.
            */
            if (shiporder is null) return;

            var carrier = Carrier.PK.Find(Base, shiporder.ShipVia);
            INSite site = null;
            SOOrderShipment order = null;

            // Runs the query within a new scope to prevent issues with orphaned queries.
            using (new PXConnectionScope())
            {
                site = SelectFrom<INSite>
                    .Where<INSite.siteID.IsEqual<@P.AsInt>>
                    .View.Select(Base, shiporder.SiteID);

                order = SelectFrom<SOOrderShipment>
                   .Where<SOOrderShipment.shipmentNbr.IsEqual<@P.AsString>>
                   .View.Select(Base, shiporder.ShipmentNbr);
            }

            if (site is null)
            {
                string message = $"STI: Unable to find warehouse for Shipment {shiporder.ShipmentNbr}.";
                throw new PXException(message, PXErrorLevel.Error);
            }

            string warehouse = site.SiteCD.Trim();

            // Only invokes the base logic if it meets our extended criteria.
            if (UseCarrierService(shiporder, carrier) && WhseMessages.IntegratedWarehouses.Contains(warehouse))
            {
                baseMethod?.Invoke(shiporder);
            }
            else
            {
                // Requires the tracking number on all packages not being sent to UPS except orders with the order type AD which are quick process orders and do not get shipped.

                PXSelectBase<SOPackageDetailEx> query = new SelectFrom<SOPackageDetailEx>
                    .Where<SOPackageDetailEx.shipmentNbr.IsEqual<@P.AsString>>.View(Base);

                foreach (SOPackageDetailEx package in query.Select(shiporder.ShipmentNbr))
                {
                    if ((package.TrackNumber is null || package.TrackNumber.Trim().Length == 0) && order.OrderType != "AD")
                    {
                        string message = "Tracking number is required for all packages before shipment.";
                        throw new PXException(message, PXErrorLevel.Error);
                    }
                }
            }
        }
        protected virtual bool UseCarrierService(SOShipment row, Carrier carrier)
            => carrier != null && carrier.IsExternal == true && AllowCalculateFreight(row, carrier);

        protected virtual bool AllowCalculateFreight(SOShipment row, Carrier carrier)
        {
            if (row.Operation == SOOperation.Receipt)
                return carrier.CalcFreightOnReturn == true;
            return true;
        }
    }
}
