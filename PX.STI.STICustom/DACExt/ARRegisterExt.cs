using System;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;

namespace PX.STI.STICustom.DACExt
{
    public sealed class ARRegisterExt : PXCacheExtension<ARRegister>
    {
        public static bool IsActive() { return true; }

        #region usrSOType

        public abstract class UsrSOType : BqlString.Field<UsrSOType> { }
        [PXDBString(10)]
        [PXUIField(DisplayName = "Order Type")]

        public string usrSOType { get; set; }

        #endregion

        #region usrOrigDiscDate

        public abstract class UsrOrigDiscDate : PX.Data.IBqlField, PX.Data.IBqlOperand { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Cash Discount Date", Enabled = true)]
        [PXDefault(typeof(Search<ARInvoiceExt.UsrOrigDiscDate,
            Where<ARInvoice.invoiceNbr, Equal<Current<ARInvoice.invoiceNbr>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        //    typeof(AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.Nothing)]
        public DateTime? usrOrigDiscDate { get; set; }

        #endregion
        #region UsrAcceptDiscount

        public abstract class usrAcceptDiscount : PX.Data.BQL.BqlBool.Field<usrAcceptDiscount> { }
        [PXDBBool()]
        [PXDefault(typeof(Search<ARInvoiceExt.usrAcceptDiscount,
            Where<ARInvoice.invoiceNbr, Equal<Current<ARInvoice.invoiceNbr>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Take Discount")]
        public Boolean? UsrAcceptDiscount { get; set; }

        #endregion
    }
}
