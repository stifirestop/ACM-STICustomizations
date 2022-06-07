using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.STI.STICustom.DAC.Projection;
using System;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class ARDocumentEnqExt : PXGraphExtension<ARDocumentEnq>
    {
        #region Actions
        #endregion
        #region Events

        protected void _(Events.FieldUpdated<ARDocumentEnq.ARDocumentFilter, ARDocumentEnq.ARDocumentFilter.includeChildAccounts> eventHandler)
        {
            ARDocumentEnq.ARDocumentFilter row = eventHandler.Row;
            if (row is null) return;

            SetLastPaymentFields(Base, eventHandler.Cache, row);
        }

        protected void _(Events.FieldUpdated<ARDocumentEnq.ARDocumentFilter, ARDocumentEnq.ARDocumentFilter.customerID> eventHandler)
        {
            ARDocumentEnq.ARDocumentFilter row = eventHandler.Row;
            if (row is null) return;

            SetLastPaymentFields(Base, eventHandler.Cache, row);
        }

        #endregion
        #region Data Types
        #endregion

        // Awful to overrite initialize, should move this into an injected service but this was a one off request.
        // TODO: Move away from using graph constructor to initialize fields.
        public override void Initialize()
        {
            base.Initialize();
            SetLastPaymentFields(Base, Base.Filter.Cache, Base.Filter.Current);
        }

        public static void SetLastPaymentFields(PXGraph graph, PXCache cache, ARDocumentEnq.ARDocumentFilter filter)
        {
            if (filter.CustomerID is null) return;
            STPaymentRegisterView lastPaymentReceived = null;

            using (new PXConnectionScope())
            {
                if (filter.IncludeChildAccounts is true)
                {
                    lastPaymentReceived = SelectFrom<STPaymentRegisterView>
                        .InnerJoin<BAccount>
                            .On<BAccount.bAccountID.IsEqual<STPaymentRegisterView.customerID>>
                        .Where<STPaymentRegisterView.customerID.IsEqual<@P.AsInt>
                            .Or<BAccount.parentBAccountID.IsEqual<@P.AsInt>>>
                        .OrderBy<STPaymentRegisterView.paymentDate.Desc, STPaymentRegisterView.refNbr.Desc>
                        .View.ReadOnly.SelectWindowed(graph, 0, 1, filter.CustomerID, filter.CustomerID);
                }

                else
                {
                    lastPaymentReceived = SelectFrom<STPaymentRegisterView>
                        .InnerJoin<BAccount>
                            .On<BAccount.bAccountID.IsEqual<STPaymentRegisterView.customerID>>
                        .Where<STPaymentRegisterView.customerID.IsEqual<@P.AsInt>>
                        .OrderBy<STPaymentRegisterView.paymentDate.Desc, STPaymentRegisterView.refNbr.Desc>
                        .View.ReadOnly.SelectWindowed(graph, 0, 1, filter.CustomerID, filter.CustomerID);
                }
            }

            if (lastPaymentReceived != null)
            {
                cache.SetValue<ExtensionARDocumentFilter.usrLastPaymentDate>(filter, lastPaymentReceived.PaymentDate);
                cache.SetValue<ExtensionARDocumentFilter.usrCuryLastPaymentAmt>(filter, lastPaymentReceived.PaymentAmt);
            }
            else
            {
                cache.SetValue<ExtensionARDocumentFilter.usrLastPaymentDate>(filter, null);
                cache.SetValue<ExtensionARDocumentFilter.usrCuryLastPaymentAmt>(filter, 0.00M);
            }
        }
    }

    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class ExtensionARDocumentFilter : PXCacheExtension<ARDocumentEnq.ARDocumentFilter>
    {
        #region UsrLastPaymentDate

        public abstract class usrLastPaymentDate : BqlDateTime.Field<usrLastPaymentDate> { }

        [PXDate]
        [PXUIField(DisplayName = "Last Payment Date", Visibility = PXUIVisibility.Visible, IsReadOnly = true)]
        public DateTime? UsrLastPaymentDate { get; set; }

        #endregion
        #region UsrLastPaymentAmt

        public abstract class usrCuryLastPaymentAmt : BqlDecimal.Field<usrCuryLastPaymentAmt> { }

        [PXDecimal]
        [PXUIField(DisplayName = "Last Payment Amount", Visibility = PXUIVisibility.Visible, IsReadOnly = true)]
        public decimal? UsrCuryLastPaymentAmt { get; set; }

        #endregion
    }
}

