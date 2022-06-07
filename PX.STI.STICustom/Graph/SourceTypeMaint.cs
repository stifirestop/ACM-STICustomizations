using PX.Data;
using PX.Data.BQL.Fluent;
using PX.STI.STICustom.DAC;

namespace PX.STI.STICustom.Graph
{
    public class SourceTypeMaint : PXGraph<SourceTypeMaint>
    {
        #region Actions
        #endregion
        #region Events
        #endregion
        #region Data Types
        #endregion

        public PXSavePerRow<STSourceType> Save;
        public PXCancel<STSourceType> Cancel;

        public SelectFrom<STSourceType>.View Types;
    }
}