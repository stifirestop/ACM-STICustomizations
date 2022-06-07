using PX.Data;
using PX.Data.BQL;
using PX.Objects.SO;

namespace PX.STI.STICustom.DACExt
{
    public sealed class SOInvoiceExt : PXCacheExtension<SOInvoice>
    {
        public static bool IsActive() { return true; }

        #region usrSOType

        public abstract class UsrSOType : BqlString.Field<UsrSOType> { }
        [PXDBString(10)]
        [PXUIField(DisplayName = "Order Type")]

        public string usrSOType { get; set; }

        #endregion
        #region UsrShippingTerm

        public abstract class usrShippingTerm : BqlString.Field<usrShippingTerm> { }

        [PXString(20)]
        [PXUIField(DisplayName = "Ship Terms ID", Visibility = PXUIVisibility.Visible)]
        public string UsrShippingTerm { get; set; }

        #endregion
    }
}
