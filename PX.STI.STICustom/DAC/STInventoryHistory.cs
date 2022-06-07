using System;
using PX.Data;
using PX.Objects.IN;

namespace PX.STI.STICustom.DAC
{
    [Serializable()]
    [PXCacheName("STInventoryHistory")]
    [PXProjection(typeof(Select<STInventoryHistory>),
        Persistent = false)]
    public class STInventoryHistory : PX.Data.IBqlTable
    {
        #region InventoryID

        public abstract class inventoryID : PX.Data.BQL.BqlInt.Field<inventoryID> { }
        [StockItem(IsKey = true, DirtyRead = true)]
        [PXUIField(DisplayName = "Inventory ID", IsReadOnly = true)]
        public virtual Int32? InventoryID { get; set; }

        #endregion
        #region InventoryDescr

        public abstract class inventoryDescr : PX.Data.BQL.BqlString.Field<inventoryDescr> { }
        [PXDBString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Description", IsReadOnly = true)]
        public virtual String InventoryDescr { get; set; }

        #endregion
        #region ItemClassID

        public abstract class itemClassID : PX.Data.BQL.BqlString.Field<itemClassID> { }
        [PXDBInt()]
        [PXSelector(typeof(Search<INItemClass.itemClassID>),
            DescriptionField = typeof(INItemClass.descr),
            SubstituteKey = typeof(INItemClass.descr))]
        [PXUIField(DisplayName = "Item Class", IsReadOnly = true)]
        public virtual int? ItemClassID { get; set; }

        #endregion
        #region ItemClassDescr

        public abstract class itemClassDescr : PX.Data.BQL.BqlString.Field<itemClassDescr> { }
        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Item Class Description", IsReadOnly = true)]
        public virtual String ItemClassDescr { get; set; }

        #endregion
        #region ItemClassType

        public abstract class itemClassType : PX.Data.BQL.BqlString.Field<itemClassType> { }
        [PXDBString(1, IsFixed = true)]
        [PXUIField(DisplayName = "Item Type", IsReadOnly = true)]
        [INItemTypes.List()]
        public virtual String ItemClassType { get; set; }

        #endregion
        #region UnitOfMeasure

        public abstract class unitOfMeasure : PX.Data.BQL.BqlString.Field<unitOfMeasure> { }
        [PXDBString()]
        [PXUIField(DisplayName = "UOM", IsReadOnly = true)]
        public virtual String UnitOfMeasure { get; set; }

        #endregion
        #region StandardCost

        public abstract class standardCost : PX.Data.BQL.BqlDecimal.Field<standardCost> { }
        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0000", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Std Cost", IsReadOnly = true)]
        public virtual Decimal? StandardCost { get; set; }

        #endregion
        #region LastPurchaseDate

        public abstract class lastPurchaseDate : PX.Data.BQL.BqlDateTime.Field<lastPurchaseDate> { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Last Purchase Dt", IsReadOnly = true)]
        public virtual DateTime? LastPurchaseDate { get; set; }

        #endregion
        #region LastSellDate

        public abstract class lastSellDate : PX.Data.BQL.BqlDateTime.Field<lastSellDate> { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Last Sell Dt", IsReadOnly = true)]
        public virtual DateTime? LastSellDate { get; set; }

        #endregion
        #region CreatedDate

        public abstract class createdDate : PX.Data.BQL.BqlDateTime.Field<createdDate> { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Created Dt", IsReadOnly = true)]
        public virtual DateTime? CreatedDate { get; set; }

        #endregion
        #region StartQty

        public abstract class startQty : PX.Data.IBqlField { }
        [PXQuantity(4)]
        [PXDefault(TypeCode.Decimal, "0.0000", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Start Qty", IsReadOnly = true)]
        public virtual Decimal? StartQty { get; set; }

        #endregion
        #region IssuedQty

        public abstract class issuedQty : PX.Data.IBqlField { }
        [PXQuantity(4)]
        [PXDefault(TypeCode.Decimal, "0.00", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Issued Qty", IsReadOnly = true)]
        public virtual Decimal? IssuedQty { get; set; }

        #endregion
        #region ReceivedQty

        public abstract class receivedQty : PX.Data.IBqlField { }
        [PXQuantity(4)]
        [PXDefault(TypeCode.Decimal, "0.0000", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Received Qty", IsReadOnly = true)]
        public virtual Decimal? ReceivedQty { get; set; }

        #endregion
        #region SalesQty

        public abstract class salesQty : PX.Data.IBqlField { }
        [PXQuantity(4)]
        [PXDefault(TypeCode.Decimal, "0.0000", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Sales Qty", IsReadOnly = true)]
        public virtual Decimal? SalesQty { get; set; }

        #endregion
        #region DropShipSalesQty

        public abstract class dropShipSalesQty : PX.Data.IBqlField { }
        [PXQuantity(4)]
        [PXDefault(TypeCode.Decimal, "0.0000", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Drop Ship Sales Qty", IsReadOnly = true)]
        public virtual Decimal? DropShipSalesQty { get; set; }

        #endregion
        #region CreditMemoQty

        public abstract class creditMemoQty : PX.Data.IBqlField { }
        [PXQuantity(4)]
        [PXDefault(TypeCode.Decimal, "0.0000", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Credit Memo Qty", IsReadOnly = true)]
        public virtual Decimal? CreditMemoQty { get; set; }

        #endregion
        #region TransferInQty

        public abstract class transferInQty : PX.Data.IBqlField { }
        [PXQuantity(4)]
        [PXDefault(TypeCode.Decimal, "0.0000", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Transfer In Qty", IsReadOnly = true)]
        public virtual Decimal? TransferInQty { get; set; }

        #endregion
        #region TransferOutQty

        public abstract class transferOutQty : PX.Data.IBqlField { }
        [PXQuantity(4)]
        [PXDefault(TypeCode.Decimal, "0.0000", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Transfer Out Qty", IsReadOnly = true)]
        public virtual Decimal? TransferOutQty { get; set; }

        #endregion
        #region AdjustmentQty

        public abstract class adjustmentQty : PX.Data.IBqlField { }
        [PXQuantity(4)]
        [PXDefault(TypeCode.Decimal, "0.0000", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Adjustment Qty", IsReadOnly = true)]
        public virtual Decimal? AdjustmentQty { get; set; }

        #endregion
        #region EndQty

        public abstract class endQty : PX.Data.IBqlField { }
        [PXQuantity(4)]
        [PXDefault(TypeCode.Decimal, "0.0000", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "End Qty", IsReadOnly = true)]
        public virtual Decimal? EndQty { get; set; }

        #endregion
        #region IsValid

        public abstract class isValid : PX.Data.IBqlField { }
        [PXBool()]
        [PXUIField(DisplayName = "Validated", IsReadOnly = true)]
        public virtual Boolean? IsValid { get; set; }

        #endregion
        #region ScrapQty

        public abstract class scrapQty : PX.Data.IBqlField { }
        [PXQuantity(4)]
        [PXDefault(TypeCode.Decimal, "0.0000", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Scrap Qty", IsReadOnly = true)]
        public virtual Decimal? ScrapQty { get; set; }

        #endregion
        #region InventoryCost

        public abstract class inventoryCost : PX.Data.IBqlField { }
        [PXQuantity(4)]
        [PXDefault(TypeCode.Decimal, "0.00", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Inventory Cost", IsReadOnly = true)]
        public virtual Decimal? InventoryCost { get; set; }

        #endregion
        #region QualityControlQty
        #endregion
        #region YearsOnHand

        public abstract class yearsOnHand : PX.Data.IBqlField { }
        [PXQuantity(2)]
        [PXDefault(TypeCode.Decimal, "0.00", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Years", IsReadOnly = true)]
        public virtual Decimal? YearsOnHand { get; set; }

        #endregion
    }
}

