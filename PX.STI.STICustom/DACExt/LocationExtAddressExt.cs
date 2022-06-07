using System;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.STI.STICustom.Common;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class LocationExtAddressExt : PXCacheExtension<LocationExtAddress>
    {
        #region UsrRepNbr

        public abstract class usrRepNbr : BqlString.Field<usrRepNbr> { }

        [PXDBString(DescriptionLength.Code, BqlField = typeof(LocationExtAddressExt.usrRepNbr))]
        [PXUIField(DisplayName = "Rep Nbr.", Visibility = PXUIVisibility.Visible)]
        public string UsrRepNbr { get; set; }

        #endregion
        #region UsrSalesTerritoryOverride

        public abstract class usrSalesTerritoryOverride : PX.Data.BQL.BqlBool.Field<usrSalesTerritoryOverride> { }
        [PXDBBool(BqlField = typeof(StandaloneLocationExt.usrSalesTerritoryOverride))]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Sales Territory Override")]
        public virtual Boolean? UsrSalesTerritoryOverride { get; set; }

        #endregion
        #region UsrSalesTerritoryCD

        public abstract class usrSalesTerritoryCD : PX.Data.BQL.BqlString.Field<usrSalesTerritoryCD> { }
        [PXDBString(30, IsUnicode = true, BqlField = typeof(StandaloneLocationExt.usrSalesTerritoryCD))]
        [PXUIField(DisplayName = "Sales Territory", IsReadOnly = true)]
        [PXSelector(typeof(Search<SegmentValue.value,
            Where<SegmentValue.dimensionID, Equal<TerritorySegmentType.Dimension>,
            And<SegmentValue.segmentID, Equal<TerritorySegmentType.Segment>,
            And<SegmentValue.active, Equal<True>>>>>),
            typeof(SegmentValue.value),
            typeof(SegmentValueExt.usrLegacyDeptTerritory),
            typeof(SegmentValue.descr),
            DescriptionField = typeof(SegmentValueExt.usrLegacyDeptTerritory))]
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
        [PXFormula(typeof(Selector<LocationExtAddressExt.usrSalesTerritoryCD, SegmentValueExt.usrLegacyDeptTerritory>))]
        public virtual String UsrLegacyTerritoryCD { get; set; }

        #endregion
        #region UsrSalesPersonOverride

        public abstract class usrSalesPersonOverride : PX.Data.BQL.BqlBool.Field<usrSalesPersonOverride> { }
        [PXDBBool(BqlField = typeof(StandaloneLocationExt.usrSalesPersonOverride))]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Salesperson Override")]
        public virtual Boolean? UsrSalesPersonOverride { get; set; }

        #endregion
        #region UsrDefSalesPersonID

        public abstract class usrDefSalesPersonID : PX.Data.BQL.BqlInt.Field<usrDefSalesPersonID> { }
        [PXInt()]
        [PXUIField(DisplayName = "Sales Person ID", IsReadOnly = true)]
        [PXSelector(typeof(Search<SalesPerson.salesPersonID>),
            typeof(SalesPerson.salesPersonCD),
            typeof(SalesPerson.descr),
            SubstituteKey = typeof(SalesPerson.salesPersonCD),
            DescriptionField = typeof(SalesPerson.descr))]
        public virtual int? UsrDefSalesPersonID { get; set; }

        #endregion
        #region UsrDefSalesPersonDescr

        public abstract class usrDefSalesPersonDescr : PX.Data.BQL.BqlInt.Field<usrDefSalesPersonDescr> { }
        [PXInt()]
        [PXUIField(DisplayName = "Sales Person", IsReadOnly = true)]
        [PXSelector(typeof(Search<SalesPerson.salesPersonID>),
            typeof(SalesPerson.salesPersonCD),
            typeof(SalesPerson.descr),
            SubstituteKey = typeof(SalesPerson.descr),
            DescriptionField = typeof(SalesPerson.descr))]
        public virtual int? UsrDefSalesPersonDescr { get; set; }

        #endregion
    }
}
