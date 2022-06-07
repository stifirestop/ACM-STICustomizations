using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.STI.STICustom.Common;
using System;

namespace PX.STI.STICustom.DAC.Projection
{
    [Serializable]
    [PXProjection(typeof(Select<ARRegister,
        Where<
            Brackets<
                ARRegister.docType.IsEqual<ARDocType.payment>
                .Or<ARRegister.docType.IsEqual<ARDocType.prepayment>>>
            .And<Brackets<
                ARRegister.status.IsEqual<ARDocStatus.open>
                .Or<ARRegister.status.IsEqual<ARDocStatus.closed>>>>>>),
        Persistent = false)]
    [PXBreakInheritance()]
    [PXCacheName(CustomView.PaymentView)]
    public class STPaymentRegisterView : IBqlTable
    {
        #region CustomerID

        public abstract class customerID : BqlInt.Field<customerID> { }

        [PXDBInt(BqlField = typeof(ARRegister.customerID))]
        public int? CustomerID { get; set; }

        #endregion
        #region DocType

        public abstract class docType : BqlString.Field<docType> { }

        [PXDBString(BqlField = typeof(ARRegister.docType))]
        public string DocType { get; set; }

        #endregion
        #region RefNbr

        public abstract class refNbr : BqlString.Field<refNbr> { }

        [PXDBString(BqlField = typeof(ARRegister.refNbr))]
        public string RefNbr { get; set; }

        #endregion
        #region PaymentDate

        public abstract class paymentDate : BqlDateTime.Field<paymentDate> { }

        [PXDBDate(BqlField = typeof(ARRegister.docDate))]
        public DateTime? PaymentDate { get; set; }

        #endregion
        #region PaymentAmt

        public abstract class paymentAmt : BqlDecimal.Field<paymentAmt> { }

        [PXDBDecimal(BqlField = typeof(ARRegister.curyOrigDocAmt))]
        public decimal? PaymentAmt { get; set; }

        #endregion
    }
}

