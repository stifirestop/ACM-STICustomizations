using System;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.GL;

namespace PX.STI.STICustom.DAC
{
    [Serializable]
    [PXCacheName("SurplusObsoleteFilter")]
    public partial class SurplusObsoleteFilter : PX.Data.IBqlTable
    {
        #region ItemClassType

        public abstract class itemClassType : PX.Data.IBqlField { }

        [PXString(1, IsFixed = true)]
        [PXUIField(DisplayName = "Item Type")]
        [INItemTypes.List()]
        public virtual String ItemClassType { get; set; }

        #endregion
        #region ItemClassDescr

        public abstract class itemClassDescr : PX.Data.IBqlField { }

        [PXString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Item Class")]
        public virtual String ItemClassDescr { get; set; }

        #endregion
        #region StartPeriod

        public abstract class startPeriod : PX.Data.IBqlField { }

        [FinPeriodID()]
        [PXUIField(DisplayName = "Start Period")]
        // [PXSelector(typeof(FinPeriod.finPeriodID))]
        public virtual String StartPeriod { get; set; }

        #endregion
        #region EndPeriod

        public abstract class endPeriod : PX.Data.IBqlField { }

        [FinPeriodID()]
        [PXUIField(DisplayName = "End Period")]
        // [PXSelector(typeof(FinPeriod.finPeriodID))]
        public virtual String EndPeriod { get; set; }

        #endregion
    }
}
