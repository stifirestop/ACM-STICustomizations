using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Common;

namespace PX.STI.STICustom.Common
{
    [Serializable()]
    [PXLocalizable("STI Common Messages")]
    public static class CustomMessages
    {
        public const string CustomerOrderNbrRequired = "Customer Order Number required for this Order Type!";
        public static readonly string[] OrderTypesRequiringCustNbr = { "SO", "RC" };

        public const string WarehouseIsRequired = "The warehouse is required!";
        public const string ShipViaMisMatch = "Ship Via on the other Sales Order Lines is not matching for the same Warehouse!";
        public const string TrackingNumberRequired = "Tracking number is required for all packages before shipment.";
        public const string WarehouseNotFound = "Warehouse not found for shipment.";
        public const string TransitDaysLessThanZero = "Transit days cannot be less than zero.";

        public const string ShipViaUpdateAllLines = "Do you want to update all Order Lines with the new Ship Via?";

        public const string SalesTerritoryRequired = "Sales Territory is required!";
        public const string SalespersonRequired = "Default Salesperson is required!";
        public static readonly string[] StateExceptions = { "AK", "CA", "HI", "OR", "WA", "BC" };
    }

    [Serializable()]
    [PXLocalizable("STI Last Purchase Cost Messages")]
    public static class Messages
    {
    }

    [Serializable()]
    [PXLocalizable("STI Last Purchase Cost Exceptions")]
    public static class ErrorMessages
    {
    }

    [Serializable()]
    [PXLocalizable("STI Last Purchase Cost Cache Names")]
    public static class STCacheName
    {
        public const string ItemLastPurchaseCost = "STI Item Last Purchase Cost";
    }

    [PXLocalizable("STI UPS Integration Messages")]
    public static class WhseMessages
    {
        public static readonly string[] IntegratedWarehouses = { "001" };
        public static readonly string[] IntegratedShipmentMethods = { "UPS" };
    }

    [PXLocalizable("STI Cache Names")]
    public static class STTransitCacheName
    {
        public const string TransitEst = "STI Transit Estimate";
    }
}
