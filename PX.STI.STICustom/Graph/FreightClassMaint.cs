using PX.Data;
using PX.Data.BQL.Fluent;
using PX.STI.STICustom.DAC;

namespace PX.STI.STICustom.Graph
{
    public class FreightClassMaint : PXGraph<FreightClassMaint>
    {
        public SelectFrom<STFreightClass>.View FrtClasses;
        public PXSavePerRow<STFreightClass> Save;
        public PXCancel<STFreightClass> Cancel;
    }
}
