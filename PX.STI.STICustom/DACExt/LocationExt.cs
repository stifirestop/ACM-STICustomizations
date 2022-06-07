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
    public class LocationExt : PXCacheExtension<Location>
    {
        #region UsrIsElectrical

        public abstract class usrIsElectrical : BqlBool.Field<usrIsElectrical> { }

        [PXDBBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Electrical", Visibility = PXUIVisibility.Visible)]
        public bool? UsrIsElectrical { get; set; }

        #endregion
        #region UsrIsCommunication

        public abstract class usrIsCommunication : BqlBool.Field<usrIsCommunication> { }

        [PXDBBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Communication", Visibility = PXUIVisibility.Visible)]
        public bool? UsrIsCommunication { get; set; }

        #endregion
        #region UsrIsConstruction

        public abstract class usrIsConstruction : BqlBool.Field<usrIsConstruction> { }

        [PXDBBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Construction", Visibility = PXUIVisibility.Visible)]
        public bool? UsrIsConstruction { get; set; }

        #endregion
        #region UsrIsMarine

        public abstract class usrIsMarine : BqlBool.Field<usrIsMarine> { }

        [PXDBBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Marine", Visibility = PXUIVisibility.Visible)]
        public bool? UsrIsMarine { get; set; }

        #endregion
        #region UsrRepNbr

        public abstract class usrRepNbr : BqlString.Field<usrRepNbr> { }

        [PXDBString(DescriptionLength.Code)]
        [PXUIField(DisplayName = "Rep Nbr.", Visibility = PXUIVisibility.Visible)]
        public string UsrRepNbr { get; set; }

        #endregion

        #region UsrSalesTerritoryOverride

        public abstract class usrSalesTerritoryOverride : PX.Data.BQL.BqlBool.Field<usrSalesTerritoryOverride> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Manual Sales Territory Override")]
        public virtual Boolean? UsrSalesTerritoryOverride { get; set; }

        #endregion
        #region UsrSalesTerritoryCD

        public abstract class usrSalesTerritoryCD : PX.Data.BQL.BqlString.Field<usrSalesTerritoryCD> { }
        [PXDBString(30, IsUnicode = true)]
        [PXUIField(DisplayName = "Sales Territory")]
        [PXSelector(typeof(Search<SegmentValue.value,
            Where<SegmentValue.dimensionID, Equal<TerritorySegmentType.Dimension>,
            And<SegmentValue.segmentID, Equal<TerritorySegmentType.Segment>,
            And<SegmentValue.active, Equal<True>>>>>),
            typeof(SegmentValue.value),
            typeof(SegmentValueExt.usrLegacyDeptTerritory),
            typeof(SegmentValue.descr),
            DescriptionField = typeof(SegmentValueExt.usrLegacyDeptTerritory))]
        [PXUIEnabled(typeof(Where<usrSalesTerritoryOverride.IsEqual<True>>))]
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
        public virtual string UsrLegacyTerritoryCD { get; set; }

        #endregion
        #region UsrSalesPersonOverride

        public abstract class usrSalesPersonOverride : PX.Data.BQL.BqlBool.Field<usrSalesPersonOverride> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Manual Salesperson Override")]
        public virtual Boolean? UsrSalesPersonOverride { get; set; }

        #endregion
        #region UsrDefSalespersonID

        public abstract class usrDefSalesPersonID : PX.Data.BQL.BqlInt.Field<usrDefSalesPersonID> { }
        [PXInt()]
        [PXUIField(DisplayName = "Default SalesPerson")]
        [PXSelector(typeof(Search<SalesPerson.salesPersonID>),
            typeof(SalesPerson.salesPersonCD),
            typeof(SalesPerson.descr),
            SubstituteKey = typeof(SalesPerson.salesPersonCD),
            DescriptionField = typeof(SalesPerson.descr))]
        [PXDBScalar(typeof(Search<CustSalesPeople.salesPersonID,
            Where<CustSalesPeople.bAccountID, Equal<Location.bAccountID>,
            And<CustSalesPeople.locationID, Equal<Location.locationID>,
            And<CustSalesPeople.isDefault, Equal<True>>>>>))]
        [PXUIEnabled(typeof(Where<usrSalesPersonOverride.IsEqual<True>>))]
        public virtual int? UsrDefSalesPersonID { get; set; }

        #endregion
        
    }

    // An extension of the Standalone Location table is required to use fields added to the Location table anywhere that
    // the LocationExtAddress projection is utilized. The LocationExtAddress projection can mostly be found on screens that
    // contain tabs that list a grid view of Locations tied to the primary screen document. You should still point the BqlField
    // at the original Location extension rather than the Standalone Location Extension.

    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class StandaloneLocationExt : PXCacheExtension<Objects.CR.Standalone.Location>
    {
        #region UsrIsElectrical

        public abstract class usrIsElectrical : BqlBool.Field<usrIsElectrical> { }

        [PXDBBool]
        public bool? UsrIsElectrical { get; set; }

        #endregion
        #region UsrIsCommunication

        public abstract class usrIsCommunication : BqlBool.Field<usrIsCommunication> { }

        [PXDBBool]
        public bool? UsrIsCommunication { get; set; }

        #endregion
        #region UsrIsConstruction

        public abstract class usrIsConstruction : BqlBool.Field<usrIsConstruction> { }

        [PXDBBool]
        public bool? UsrIsConstruction { get; set; }

        #endregion
        #region UsrIsMarine

        public abstract class usrIsMarine : BqlBool.Field<usrIsMarine> { }

        [PXDBBool]
        public bool? UsrIsMarine { get; set; }

        #endregion
        #region UsrSalesTerritoryOverride

        public abstract class usrSalesTerritoryOverride : PX.Data.BQL.BqlBool.Field<usrSalesTerritoryOverride> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Manual Sales Territory Override")]
        public virtual Boolean? UsrSalesTerritoryOverride { get; set; }

        #endregion
        #region UsrSalesTerritoryCD

        public abstract class usrSalesTerritoryCD : PX.Data.BQL.BqlString.Field<usrSalesTerritoryCD> { }
        [PXDBString(30, IsUnicode = true)]
        [PXUIField(DisplayName = "Sales Territory")]
        [PXSelector(typeof(Search<SegmentValue.value,
            Where<SegmentValue.dimensionID, Equal<TerritorySegmentType.Dimension>,
            And<SegmentValue.segmentID, Equal<TerritorySegmentType.Segment>,
            And<SegmentValue.active, Equal<True>>>>>),
            typeof(SegmentValue.value),
            typeof(SegmentValueExt.usrLegacyDeptTerritory),
            typeof(SegmentValue.descr),
            DescriptionField = typeof(SegmentValueExt.usrLegacyDeptTerritory))]
        [PXUIEnabled(typeof(Where<usrSalesTerritoryOverride.IsEqual<True>>))]
        public virtual String UsrSalesTerritoryCD { get; set; }

        #endregion
        #region UsrSalesPersonOverride

        public abstract class usrSalesPersonOverride : PX.Data.BQL.BqlBool.Field<usrSalesPersonOverride> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Manual Salesperson Override")]
        public virtual Boolean? UsrSalesPersonOverride { get; set; }

        #endregion
        #region UsrDefSalespersonID

        public abstract class usrDefSalesPersonID : PX.Data.BQL.BqlInt.Field<usrDefSalesPersonID> { }
        [PXInt()]
        [PXUIField(DisplayName = "Default SalesPerson")]
        [PXSelector(typeof(Search<SalesPerson.salesPersonID>),
            typeof(SalesPerson.salesPersonCD),
            typeof(SalesPerson.descr),
            SubstituteKey = typeof(SalesPerson.salesPersonCD),
            DescriptionField = typeof(SalesPerson.descr))]
        [PXDBScalar(typeof(Search<CustSalesPeople.salesPersonID,
            Where<CustSalesPeople.bAccountID, Equal<Location.bAccountID>,
            And<CustSalesPeople.locationID, Equal<Location.locationID>,
            And<CustSalesPeople.isDefault, Equal<True>>>>>))]
        [PXUIEnabled(typeof(Where<usrSalesPersonOverride.IsEqual<True>>))]
        public virtual int? UsrDefSalesPersonID { get; set; }

        #endregion
        #region UsrRepNbr

        public abstract class usrRepNbr : PX.Data.BQL.BqlString.Field<usrRepNbr> { }
        [PXDBString(10)]
        [PXUIField(DisplayName = "Rep. Nbr.")]
        public virtual String UsrRepNbr { get; set; }

        #endregion

        #region UsrRepNbrDesc

        public abstract class usrRepNbrDesc : PX.Data.BQL.BqlString.Field<usrRepNbrDesc> { }
        [PXDBString(256)]
        [PXUIField(DisplayName = "Rep. Desc.")]
        public virtual String UsrRepNbrDesc { get; set; }

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
        public virtual string UsrLegacyTerritoryCD { get; set; }

        #endregion

    }
}
