using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.SO;
using PX.Objects.CR;
using PX.Objects.IN;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class SOLineExt : PXCacheExtension<SOLine>
    {
        #region UsrShipVia

        public abstract class usrShipVia : BqlString.Field<usrShipVia> { }

        [PXDBString(15)]
        [PXDefault(typeof(Search<Location.cCarrierID,
            Where<Location.bAccountID.IsEqual<SOOrder.customerID.FromCurrent>
                .And<Location.locationID.IsEqual<SOOrder.customerLocationID.FromCurrent>>>>),
            PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "ShipVia Line", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<Carrier.carrierID>),
            typeof(Carrier.carrierID),
            typeof(Carrier.description),
            typeof(Carrier.isCommonCarrier),
            typeof(Carrier.confirmationRequired),
            typeof(Carrier.packageRequired),
            DescriptionField = typeof(Carrier.description),
            CacheGlobal = true)]
        public string UsrShipVia { get; set; }

        #endregion
        #region UsrQuoteNbr

        public abstract class usrQuoteNbr : BqlString.Field<usrQuoteNbr> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Quote Nbr", Visibility = PXUIVisibility.Visible)]
        public string UsrQuoteNbr { get; set; }

        #endregion
        #region UsrItemWeightUOM

        public abstract class usrItemWeightUOM : BqlString.Field<usrItemWeightUOM> { }

        [PXString(6, IsUnicode = true)]
        [PXUIField(DisplayName = "Item Weight UOM", Visibility = PXUIVisibility.Visible, IsReadOnly = true)]
        [PXFormula(typeof(Selector<SOLine.inventoryID, InventoryItem.weightUOM>))]
        public string UsrItemWeightUOM { get; set; }

        #endregion
        #region UsrBaseItemWeight

        public abstract class usrBaseItemWeight : BqlDecimal.Field<usrBaseItemWeight> { }

        [PXDecimal]
        [PXFormula(typeof(Selector<SOLine.inventoryID, InventoryItem.baseItemWeight>))]
        public decimal? UsrBaseItemWeight { get; set; }

        #endregion
        #region UsrTotalLineWeight

        public abstract class usrTotalLineWeight : BqlDecimal.Field<usrTotalLineWeight> { }

        [PXQuantity(typeof(InventoryItem.weightUOM), typeof(InventoryItem.baseItemWeight), HandleEmptyKey = true)]
        [PXUIField(DisplayName = "Total Line Weight", Visibility = PXUIVisibility.Visible, IsReadOnly = true)]
        [PXFormula(typeof(Mult<SOLine.orderQty, usrBaseItemWeight>))]
        public decimal? UsrTotalLineWeight { get; set; }

        #endregion
    }
}

