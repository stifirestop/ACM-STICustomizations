using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class CarrierExt : PXCacheExtension<Carrier>
    {
        #region UsrCarrierSCACCD

        public abstract class usrCarrierSCACCD : BqlString.Field<usrCarrierSCACCD> { }

        [PXDBString(10)]
        [PXUIField(DisplayName = "SCAC Code", Visibility = PXUIVisibility.Visible)]
        public string UsrCarrierSCACCD { get; set; }

        #endregion
        #region UsrIsParcel

        public abstract class usrIsParcel : BqlBool.Field<usrIsParcel> { }

        [PXDBBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Parcel", Visibility = PXUIVisibility.Visible)]
        public bool? UsrIsParcel { get; set; }

        #endregion
    }
}
