using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;

namespace PX.STI.STICustom.DACExt
{
    public sealed class ARPriceClassExt : PXCacheExtension<ARPriceClass>
    {
        public static bool IsActive() { return true; }

        #region UsrParentID

        public abstract class usrParentID : BqlString.Field<usrParentID> { }
        [PXDBString(30)]
        [PXUIField(DisplayName = "Parent Customer ID")]
        public string UsrParentID { get; set; }

        #endregion
        #region UsrParentDescription

        public abstract class usrParentDescription : BqlString.Field<usrParentDescription> { }
        [PXDBString(100)]
        [PXUIField(DisplayName = "Parent Description")]
        public string UsrParentDescription { get; set; }

        #endregion
        #region UsrPriceClassBase

        public abstract class usrPriceClassBase : BqlString.Field<usrPriceClassBase> { }
        [PXDBString(50)]
        [PXUIField(DisplayName = "Price Class Base")]
        public string UsrPriceClassBase { get; set; }

        #endregion
    }
}
