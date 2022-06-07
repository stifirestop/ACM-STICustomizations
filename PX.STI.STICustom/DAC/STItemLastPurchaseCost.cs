using System;
using PX.Data;
using PX.Objects.IN;
using PX.STI.STICustom.Common;

namespace PX.STI.STICustom.DAC
{
    [Serializable()]
    [PXProjection(typeof(Select<STItemLastPurchaseCost>),
        Persistent = false)]
    [PXCacheName(STCacheName.ItemLastPurchaseCost, PXDacType.Catalogue)]
    public class STItemLastPurchaseCost : PX.Data.IBqlTable
    {
        #region InventoryID

        public abstract class inventoryID : PX.Data.IBqlField { }
        [PXDBInt()]
        public virtual int? InventoryID { get; set; }

        #endregion
        #region InventoryCD

        public abstract class inventoryCD : PX.Data.IBqlField { }
        [PXDBString()]
        [PXUIField(DisplayName = "Inventory ID", IsReadOnly = true)]
        public virtual String InventoryCD { get; set; }

        #endregion
        #region Descr

        public abstract class descr : PX.Data.IBqlField { }
        [PXDBString(IsUnicode = true)]
        [PXUIField(DisplayName = "Description", IsReadOnly = true)]
        public virtual String Descr { get; set; }

        #endregion
        #region ItemType

        public abstract class itemType : PX.Data.IBqlField { }
        [PXDBString()]
        [PXUIField(DisplayName = "Item Type", IsReadOnly = true)]
        public virtual String ItemType { get; set; }

        #endregion
        #region ItemTypeCD

        public abstract class itemTypeCD : PX.Data.IBqlField { }
        [PXDBString()]
        [PXUIField(DisplayName = "Item Type CD", IsReadOnly = true)]
        [INItemTypes.List()]
        public virtual String ItemTypeCD { get; set; }

        #endregion
        #region StdCost

        public abstract class stdCost : PX.Data.IBqlField { }
        [PXDBDecimal()]
        [PXUIField(DisplayName = "Standard Cost", IsReadOnly = true)]
        public virtual Decimal? StdCost { get; set; }

        #endregion
        #region UnitCost

        public abstract class unitCost : PX.Data.IBqlField { }
        [PXDBDecimal()]
        [PXUIField(DisplayName = "Unit Cost", IsReadOnly = true)]
        public virtual Decimal? UnitCost { get; set; }

        #endregion
        #region QtyOnHand

        public abstract class qtyOnHand : PX.Data.IBqlField { }
        [PXDBDecimal()]
        [PXUIField(DisplayName = "Qty On Hand", IsReadOnly = true)]
        public virtual Decimal? QtyOnHand { get; set; }

        #endregion
        #region BAccountID

        public abstract class bAccountID : PX.Data.IBqlField { }
        [PXDBInt()]
        public virtual int? BAccountID { get; set; }

        #endregion
        #region AcctCD

        public abstract class acctCD : PX.Data.IBqlField { }
        [PXDBString()]
        [PXUIField(DisplayName = "Account ID", IsReadOnly = true)]
        public virtual String AcctCD { get; set; }

        #endregion
        #region AcctName

        public abstract class acctName : PX.Data.IBqlField { }
        [PXDBString()]
        [PXUIField(DisplayName = "Account Name", IsReadOnly = true)]
        public virtual String AcctName { get; set; }

        #endregion
        #region ReceiptNbr

        public abstract class receiptNbr : PX.Data.IBqlField { }
        [PXDBString()]
        [PXUIField(DisplayName = "Receipt Nbr")]
        public virtual String ReceiptNbr { get; set; }

        #endregion
        #region ReceiptDate

        public abstract class receiptDate : PX.Data.IBqlField { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Receipt Date", IsReadOnly = true)]
        public virtual DateTime? ReceiptDate { get; set; }

        #endregion
        #region ReceiptQty

        public abstract class receiptQty : PX.Data.IBqlField { }
        [PXDBDecimal()]
        [PXUIField(DisplayName = "Receipt Qty", IsReadOnly = true)]
        public virtual Decimal? ReceiptQty { get; set; }

        #endregion
    }
}
