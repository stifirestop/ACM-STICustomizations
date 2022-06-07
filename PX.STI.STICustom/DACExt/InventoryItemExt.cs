using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using PX.STI.STICustom.DAC;

namespace PX.STI.STICustom.DACExt
{
    public sealed class InventoryItemExt : PXCacheExtension<InventoryItem>
    {
        public static bool IsActive() { return true; }

        #region UsrIsHazMat

        public abstract class usrIsHazMat : BqlBool.Field<usrIsHazMat> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "HazMat", Visibility = PXUIVisibility.Visible)]
        public bool? UsrIsHazMat { get; set; }

        #endregion
        #region UsrPrint01

        public abstract class usrPrint01 : BqlBool.Field<usrPrint01> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "01 - Construction", Visibility = PXUIVisibility.Visible)]
        public bool? UsrPrint01 { get; set; }

        #endregion
        #region UsrPrint02

        public abstract class usrPrint02 : BqlBool.Field<usrPrint02> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "02 - E/C", Visibility = PXUIVisibility.Visible)]
        public bool? UsrPrint02 { get; set; }

        #endregion
        #region UsrPrint03

        public abstract class usrPrint03 : BqlBool.Field<usrPrint03> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "03 - International", Visibility = PXUIVisibility.Visible)]
        public bool? UsrPrint03 { get; set; }

        #endregion
        #region UsrPrint04

        public abstract class usrPrint04 : BqlBool.Field<usrPrint04> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "04 - OEM", Visibility = PXUIVisibility.Visible)]
        public bool? UsrPrint04 { get; set; }

        #endregion
        #region UsrPrint06

        public abstract class usrPrint06 : BqlBool.Field<usrPrint06> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "06 - Marine", Visibility = PXUIVisibility.Visible)]
        public bool? UsrPrint06 { get; set; }

        #endregion
        #region UsrPrint11

        public abstract class usrPrint11 : BqlBool.Field<usrPrint11> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "11 - Fabricators", Visibility = PXUIVisibility.Visible)]
        public bool? UsrPrint11 { get; set; }

        #endregion

        #region UsrFreightClassID

        public abstract class usrFreightClassID : BqlString.Field<usrFreightClassID> { }

        [PXDBString(15)]
        [PXUIField(DisplayName = "Freight Class", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(STFreightClass.freightClassCD),
            DescriptionField = typeof(STFreightClass.descr))]
        public string UsrFreightClassID { get; set; }

        #endregion
        #region UsrMultipleOfReq

        public abstract class usrMultipleOfReq : BqlInt.Field<usrMultipleOfReq> { }

        [PXDBInt]
        [PXDefault(1, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Case Qty", Visibility = PXUIVisibility.Visible)]
        public int? UsrMultipleOfReq { get; set; }

        #endregion
        #region UsrPalletQty

        public abstract class usrPalletQty : BqlInt.Field<usrPalletQty> { }

        [PXDBInt]
        [PXUIField(DisplayName = "Pallet Qty", Visibility = PXUIVisibility.Visible)]
        public int? UsrPalletQty { get; set; }

        #endregion
    }
}

