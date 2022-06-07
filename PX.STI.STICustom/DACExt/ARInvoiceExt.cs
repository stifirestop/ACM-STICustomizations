using System;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class ARInvoiceExt : PXCacheExtension<ARInvoice>
    {
        #region UsrShippingTerm

        public abstract class usrShippingTerm : BqlString.Field<usrShippingTerm> { }

        [PXString(20)]
        [PXUIField(DisplayName = "Ship Terms ID", Visibility = PXUIVisibility.Visible)]
        public string UsrShippingTerm { get; set; }

        #endregion

        #region usrOrigDiscDate

        public abstract class UsrOrigDiscDate : PX.Data.BQL.BqlDateTime.Field<UsrOrigDiscDate> { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Cash Discount Date", Enabled = true)]
        //[PXDefault(typeof(AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.Nothing)]
        public DateTime? usrOrigDiscDate { get; set; }

        #endregion
        #region UsrAcceptDiscount

        public abstract class usrAcceptDiscount : PX.Data.BQL.BqlBool.Field<usrAcceptDiscount> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Take Discount")]
        public Boolean? UsrAcceptDiscount { get; set; }

        #endregion
    }
}
