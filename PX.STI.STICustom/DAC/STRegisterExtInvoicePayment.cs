using System;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.GL;

namespace PX.STI.STICustom.DAC
{
    [Serializable()]
    [PXCacheName("STRegisterExtInvoicePayment")]
    [PXProjection(typeof(Select<STRegisterExtInvoicePayment>),
        Persistent = false)]
    public class STRegisterExtInvoicePayment : IBqlTable
    {
        #region DocType

        public abstract class docType : BqlString.Field<docType>
        {
            public const int Length = 3;
        }

        [PXDBString(docType.Length, IsKey = true, IsFixed = true)]
        [PXUIField(DisplayName = "Tran Type", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual String DocType { get; set; }

        #endregion
        #region RefNbr

        public abstract class refNbr : BqlString.Field<refNbr> { }
        [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
        [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(Search<ARRegister.refNbr,
            Where<ARRegister.docType, Equal<Optional<ARRegister.docType>>>>),
            Filterable = true)]
        public virtual String RefNbr { get; set; }

        #endregion
        #region Status

        public abstract class status : BqlString.Field<status> { }
        [PXDBString(1, IsFixed = true)]
        [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        [ARDocStatus.List]
        public virtual String Status { get; set; }

        #endregion
        #region ParentAccountID

        public abstract class parentAccountID : BqlInt.Field<parentAccountID> { }
        [PXDBInt()]
        [PXUIField(DisplayName = "Customer Account", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, IsReadOnly = true)]
        [PXSelector(typeof(Search<BAccount.bAccountID>),
            SubstituteKey = typeof(BAccount.acctCD),
            DescriptionField = typeof(BAccount.acctName))]
        public virtual Int32? ParentAccountID { get; set; }

        #endregion
        #region CustomerID

        public abstract class customerID : BqlInt.Field<customerID> { }
        [PXDBInt()]
        [PXUIField(DisplayName = "Customer Account", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, IsReadOnly = true)]
        [PXSelector(typeof(Search<BAccount.bAccountID>),
            SubstituteKey = typeof(BAccount.acctCD),
            DescriptionField = typeof(BAccount.acctName))]
        public virtual Int32? CustomerID { get; set; }

        #endregion
        #region DocDesc

        public abstract class docDesc : BqlString.Field<docDesc> { }
        [PXDBString(Objects.Common.Constants.TranDescLength, IsUnicode = true)]
        [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual String DocDesc { get; set; }

        #endregion
        #region CountryID

        public abstract class countryID : BqlString.Field<countryID> { }
        [PXDBString(100)]
        [PXDefault(typeof(Search<Objects.GL.Branch.countryID,
            Where<Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>),
            PersistingCheck = PXPersistingCheck.Nothing)]
        [Country()]
        [PXUIField(DisplayName = "Country")]
        public virtual String CountryID { get; set; }

        #endregion
        #region CuryID

        public abstract class curyID : BqlString.Field<curyID> { }
        [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
        [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
        [PXDefault(typeof(Search<Company.baseCuryID>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXSelector(typeof(Currency.curyID))]
        public virtual String CuryID { get; set; }

        #endregion
        #region DocDate

        public abstract class docDate : BqlDateTime.Field<docDate> { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Invoice Date", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        public virtual DateTime? DocDate { get; set; }

        #endregion
        #region DueDate

        public abstract class dueDate : BqlDateTime.Field<dueDate> { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Invoice Due Date", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        public virtual DateTime? DueDate { get; set; }

        #endregion
        #region PurchaseOrderNbr

        public abstract class purchaseOrderNbr : BqlString.Field<purchaseOrderNbr> { }
        [PXDBString(40, IsUnicode = true)]
        [PXUIField(DisplayName = "Purchase Order Number", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual String PurchaseOrderNbr { get; set; }

        #endregion
        #region SalesPersonID

        public abstract class salesPersonID : BqlInt.Field<salesPersonID> { }
        [SalesPerson()]
        [PXSelector(typeof(Search<SalesPerson.salesPersonID>),
            SubstituteKey = typeof(SalesPerson.salesPersonCD),
            DescriptionField = typeof(SalesPerson.descr))]
        public virtual Int32? SalesPersonID { get; set; }

        #endregion
        #region RegisterAmt

        public abstract class registerAmt : BqlDecimal.Field<registerAmt> { }
        [PXDBBaseCury()]
        [PXDefault(TypeCode.Decimal, "0.00", PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual Decimal? RegisterAmt { get; set; }

        #endregion
        #region CuryRegisterAmt

        public abstract class curyRegisterAmt : BqlDecimal.Field<curyRegisterAmt> { }
        [PXDBBaseCury()]
        [PXDefault(TypeCode.Decimal, "0.00", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Register Amt", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        public virtual Decimal? CuryRegisterAmt { get; set; }

        #endregion
        #region CurySOAdjustAmt

        public abstract class curySOAdjustAmt : BqlDecimal.Field<curySOAdjustAmt> { }
        [PXDBBaseCury()]
        [PXDefault(TypeCode.Decimal, "0.00", PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual Decimal? CurySOAdjustAmt { get; set; }

        #endregion
        #region CuryARCreditAdjustAmt

        public abstract class curyARCreditAdjustAmt : BqlDecimal.Field<curyARCreditAdjustAmt> { }
        [PXDBBaseCury()]
        [PXDefault(TypeCode.Decimal, "0.00", PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual Decimal? CuryARCreditAdjustAmt { get; set; }

        #endregion
        #region CuryARDebitAdjustAmt

        public abstract class curyARDebitAdjustAmt : BqlDecimal.Field<curyARDebitAdjustAmt> { }
        [PXDBBaseCury()]
        [PXDefault(TypeCode.Decimal, "0.00", PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual Decimal? CuryARDebitAdjustAmt { get; set; }

        #endregion
        #region RegisterBalance

        public abstract class registerBalance : BqlDecimal.Field<registerBalance> { }
        protected Decimal? _RegisterBalance;
        [PXDBBaseCury()]
        [PXDefault(TypeCode.Decimal, "0.00", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Register Balance USD")]
        public virtual Decimal? RegisterBalance
        {
            get => ((this.DocType == ARDocType.Payment || this.DocType == ARDocType.CreditMemo) && this._RegisterBalance != 0.00M && this._RegisterBalance != null)
                    ? this._RegisterBalance * -1.00M
                    : this._RegisterBalance;

            set => this._RegisterBalance = value;
        }

        #endregion
        #region CuryRegisterBalance

        public abstract class curyRegisterBalance : BqlDecimal.Field<curyRegisterBalance> { }
        protected Decimal? _CuryRegisterBalance;
        [PXDBBaseCury()]
        [PXDefault(TypeCode.Decimal, "0.00", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Register Currency Balance", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Decimal? CuryRegisterBalance
        {
            get => ((this.DocType == ARDocType.Payment || this.DocType == ARDocType.CreditMemo) && this._CuryRegisterBalance != 0.00M && this._CuryRegisterBalance != null)
                    ? this._CuryRegisterBalance * -1.00M
                    : this._CuryRegisterBalance;

            set => this._CuryRegisterBalance = value;
        }

        #endregion
    }
}
