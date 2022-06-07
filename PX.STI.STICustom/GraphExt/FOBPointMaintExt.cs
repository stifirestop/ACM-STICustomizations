using PX.Data;
using PX.Objects.CS;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class FOBPointMaintExt : PXGraphExtension<FOBPointMaint>
    {
        #region Actions
        #endregion
        #region Events

        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
        private protected virtual void _(Events.CacheAttached<FOBPoint.fOBPointID> eventHandler) { }

        #endregion
        #region Data Types
        #endregion
    }
}

