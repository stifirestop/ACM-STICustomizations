using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.SO;
using PX.STI.STICustom.DACExt;
using System.Collections;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class SOReleaseInvoiceExt : PXGraphExtension<SOReleaseInvoice>
    {
        #region Actions

        #endregion
        #region Events

        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXDBScalar(typeof(SearchFor<SOFreightDetail.shipTermsID>
            .Where<SOFreightDetail.docType.IsEqual<ARInvoice.docType>
                .And<SOFreightDetail.refNbr.IsEqual<ARInvoice.refNbr>>>))]
        private protected virtual void _(Events.CacheAttached<PX.STI.STICustom.DACExt.ARInvoiceExt.usrShippingTerm> eventHandler) { }

        #endregion
        #region Data Types
        #endregion


    }
}

