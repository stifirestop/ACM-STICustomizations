using System;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.STI.STICustom.Common;

namespace PX.STI.STICustom.DACExt
{
    public class CustSalesPeopleExt : PXCacheExtension<CustSalesPeople>
    {
        public static bool IsActive() { return true; }

        #region UsrLocationPostalCode

        public abstract class usrLocationPostalCode : PX.Data.BQL.BqlString.Field<usrLocationPostalCode> { }
        [PXString(20)]
        [PXUIField(DisplayName = "Zip Code", IsReadOnly = true)]
        [PXFormula(typeof(Selector<CustSalesPeople.bAccountID, Address.postalCode>))]
        public virtual String UsrLocationPostalCode { get; set; }

        #endregion
        #region UsrLocationState

        public abstract class usrLocationState : PX.Data.BQL.BqlString.Field<usrLocationState> { }
        [PXString(10)]
        [PXUIField(DisplayName = "State", IsReadOnly = true)]
        [PXFormula(typeof(Selector<CustSalesPeople.bAccountID, Address.state>))]
        public virtual String UsrLocationState { get; set; }

        #endregion
        #region UsrSalesTerritoryCD

        public abstract class usrSalesTerritoryCD : PX.Data.IBqlField { }
        [PXString(4)]
        [PXUIField(DisplayName = "Sales Territory", IsReadOnly = true)]
        [PXSelector(typeof(Search<SegmentValue.value,
            Where<SegmentValue.dimensionID, Equal<TerritorySegmentType.Dimension>,
            And<SegmentValue.segmentID, Equal<TerritorySegmentType.Segment>,
            And<SegmentValue.active, Equal<True>>>>>),
            typeof(SegmentValue.value),
            typeof(SegmentValueExt.usrLegacyDeptTerritory),
            typeof(SegmentValue.descr),
            DescriptionField = typeof(SegmentValueExt.usrLegacyDeptTerritory))]
        [PXFormula(typeof(Selector<CustSalesPeople.locationID, StandaloneLocationExt.usrSalesTerritoryCD>))]
        public virtual String UsrSalesTerritoryCD { get; set; }

        #endregion
        #region UsrLegacyTerritoryCD

        public abstract class usrLegacyTerritoryCD : PX.Data.IBqlField { }
        [PXString(50)]
        [PXUIField(DisplayName = "Legacy Territory", IsReadOnly = true)]
        [PXSelector(typeof(Search<SegmentValueExt.usrLegacyDeptTerritory,
            Where<SegmentValue.dimensionID, Equal<TerritorySegmentType.Dimension>,
            And<SegmentValue.segmentID, Equal<TerritorySegmentType.Segment>,
            And<SegmentValue.active, Equal<True>>>>>),
            typeof(SegmentValue.value),
            typeof(SegmentValueExt.usrLegacyDeptTerritory),
            typeof(SegmentValue.descr),
            DescriptionField = typeof(SegmentValueExt.usrLegacyDeptTerritory))]
        [PXFormula(typeof(Selector<usrSalesTerritoryCD, SegmentValueExt.usrLegacyDeptTerritory>))]
        public virtual String UsrLegacyTerritoryCD { get; set; }

        #endregion
    }
}
