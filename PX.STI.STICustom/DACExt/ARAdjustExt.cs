using System;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;

namespace PX.STI.STICustom.DACExt
{
    [Serializable]
    public sealed class ARAdjustExt : PXCacheExtension<ARAdjust>
    {
        public static bool IsActive() { return true; }

        #region UsrAcceptDiscount

        public abstract class usrAcceptDiscount : PX.Data.BQL.BqlBool.Field<usrAcceptDiscount> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Take Cash Discount")]
        public Boolean? UsrAcceptDiscount { get; set; }

        #endregion
    }
}

