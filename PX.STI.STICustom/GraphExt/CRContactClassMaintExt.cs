using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.STI.STICustom.DAC;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class CRContactClassMaintExt : PXGraphExtension<CRContactClassMaint>
    {
        #region Actions
        #endregion
        #region Events
        #endregion
        #region Data Types
        #endregion

        public SelectFrom<STContactSubClass>
            .Where<STContactSubClass.contactClassID.IsEqual<CRContactClass.classID.FromCurrent>>
            .View ContactSubClasses;
    }
}