using System;
using System.Linq;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.CR;
using PX.Objects.AP;
using PX.STI.STICustom.Common;
using PX.STI.STICustom.Service;
using PX.STI.STICustom.DACExt;

namespace PX.STI.STICustom.DAC
{


    [Serializable()]
    public partial class STSalesTerritoryMapping : PX.Data.IBqlTable
    {
        /*****************
         * Database Fields
        ******************/

        #region SalesTerritoryMappingID

        public abstract class salesTerritoryMappingID : PX.Data.BQL.BqlInt.Field<salesTerritoryMappingID> { }

        [PXDBIdentity(IsKey = true)]
        public virtual int? SalesTerritoryMappingID { get; set; }

        #endregion
        #region PostalCode

        public abstract class postalCode : PX.Data.BQL.BqlString.Field<postalCode> { }

        [PXDBString(10)]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Postal Code")]
        public virtual String PostalCode { get; set; }

        #endregion
        #region SalesTerritoryCD

        public abstract class salesTerritoryCD : PX.Data.BQL.BqlString.Field<salesTerritoryCD> { }

        [PXDBString(30, IsUnicode = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Sales Territory")]
        [PXParent(typeof(Select<SegmentValue,
            Where<SegmentValue.dimensionID, Equal<TerritorySegmentType.Dimension>,
            And<SegmentValue.segmentID, Equal<TerritorySegmentType.Segment>,
            And<SegmentValue.value, Equal<Current<STSalesTerritoryMapping.salesTerritoryCD>>>>>>))]
        [PXSelector(typeof(Search<SegmentValue.value,
            Where<SegmentValue.dimensionID, Equal<TerritorySegmentType.Dimension>,
            And<SegmentValue.segmentID, Equal<TerritorySegmentType.Segment>,
            And<SegmentValue.active, Equal<True>,
            And<SegmentValueExt.usrIsSalesTerritory, Equal<True>>>>>>),
            DescriptionField = typeof(SegmentValue.descr))]
        public virtual String SalesTerritoryCD { get; set; }

        #endregion

        #region MarineTerritoryCD

        public abstract class marineTerritoryCD : PX.Data.BQL.BqlString.Field<marineTerritoryCD> { }

        [PXDBString(30, IsUnicode = true)]
        //[PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Marine Territory")]
        [PXParent(typeof(Select<SegmentValue,
            Where<SegmentValue.dimensionID, Equal<TerritorySegmentType.Dimension>,
            And<SegmentValue.segmentID, Equal<TerritorySegmentType.Segment>,
            And<SegmentValue.value, Equal<Current<STSalesTerritoryMapping.marineTerritoryCD>>>>>>))]
        [PXSelector(typeof(Search<SegmentValue.value,
            Where<SegmentValue.dimensionID, Equal<TerritorySegmentType.Dimension>,
            And<SegmentValue.segmentID, Equal<TerritorySegmentType.Segment>,
            And<SegmentValue.active, Equal<True>,
            And<SegmentValueExt.usrIsSalesTerritory, Equal<True>>>>>>),
            DescriptionField = typeof(SegmentValue.descr))]
        public virtual String MarineTerritoryCD { get; set; }

        #endregion

        #region City

        public abstract class city : PX.Data.BQL.BqlString.Field<city> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "City")]
        public virtual String City { get; set; }

        #endregion
        #region County

        public abstract class county : PX.Data.BQL.BqlString.Field<county> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "County")]
        public virtual String County { get; set; }

        #endregion
        #region StateCD

        public abstract class stateCD : PX.Data.BQL.BqlString.Field<stateCD> { }

        [PXDBString(2)]
        [PXUIField(DisplayName = "State")]
        public virtual String StateCD { get; set; }

        #endregion
        #region CountryCD

        public abstract class countryCD : PX.Data.BQL.BqlString.Field<countryCD> { }

        [PXDBString(2)]
        [PXUIField(DisplayName = "Country CD")]
        public virtual String CountryCD { get; set; }

        #endregion
        #region Country

        public abstract class country : PX.Data.BQL.BqlString.Field<country> { }

        [PXDBString(75)]
        [PXUIField(DisplayName = "Country")]
        public virtual String Country { get; set; }

        #endregion
        #region ComRep

        public abstract class comRep : PX.Data.BQL.BqlString.Field<comRep> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "Communication Rep ID")]
        [PXSelector(typeof(Search<LocationExt.usrRepNbr,
                            Where<LocationExt.usrRepNbr, IsNotNull,
                            And<Location.status, Equal<CustLocationStatus.active>,
                            And<LocationExt.usrIsCommunication, Equal<True>>>>>),
                    typeof(Location.descr)
        )]
        [PXCustomizeSelectorColumns(typeof(LocationExt.usrRepNbr), typeof(Location.descr))]

        public virtual String ComRep { get; set; }

        #endregion

        #region ConstuctRep

        public abstract class constuctRep : PX.Data.BQL.BqlString.Field<constuctRep> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "Construction Rep")]
        [PXSelector(typeof(Search2<LocationExt.usrRepNbr,
                            LeftJoin<BAccount, On<Location.bAccountID, Equal<BAccount.bAccountID>>>,
                            Where<LocationExt.usrRepNbr, IsNotNull,
                            And<Location.status, Equal<CustLocationStatus.active>,
                            And<LocationExt.usrIsConstruction, Equal<True>>>>>),
            typeof(BAccount.acctCD),
            typeof(Location.descr)
        )]
        [PXCustomizeSelectorColumns(typeof(LocationExt.usrRepNbr), typeof(Location.descr))]
        public virtual String ConstuctRep { get; set; }

        #endregion

        #region ElectricRep

        public abstract class electricRep : PX.Data.BQL.BqlString.Field<electricRep> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "Electrical Rep ID")]
        [PXSelector(typeof(Search2<LocationExt.usrRepNbr,
                            LeftJoin<BAccount, On<Location.bAccountID, Equal<BAccount.bAccountID>>>,
                            Where<LocationExt.usrRepNbr, IsNotNull,
                            And<Location.status, Equal<CustLocationStatus.active>,
                            And<LocationExt.usrIsElectrical, Equal<True>>>>>),
            typeof(BAccount.acctCD),
            typeof(Location.descr)
        )]
        [PXCustomizeSelectorColumns(typeof(LocationExt.usrRepNbr), typeof(Location.descr))]
        public virtual String ElectricRep { get; set; }

        #endregion

        #region MarineRep

        public abstract class marineRep : PX.Data.BQL.BqlString.Field<marineRep> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "Marine Sales Rep")]
        [PXSelector(typeof(Search2<LocationExt.usrRepNbr,
                            LeftJoin<BAccount, On<Location.bAccountID, Equal<BAccount.bAccountID>>>,
                            Where<LocationExt.usrRepNbr, IsNotNull,
                            And<Location.status, Equal<CustLocationStatus.active>,
                            And<LocationExt.usrIsMarine, Equal<True>>>>>),
            typeof(BAccount.acctCD),
            typeof(Location.descr)
        )]
        [PXCustomizeSelectorColumns(typeof(LocationExt.usrRepNbr), typeof(Location.descr))]
        public virtual String MarineRep { get; set; }

        #endregion

        #region TStamp

        [PXDBTimestamp()]
        public virtual byte[] tstamp { get; set; }

        public abstract class Tstamp : PX.Data.IBqlField { }

        #endregion
        #region CreatedByID

        [PXDBCreatedByID]
        public virtual Guid? CreatedByID { get; set; }

        public abstract class createdByID : PX.Data.IBqlField { }

        #endregion
        #region CreatedByScreenID

        [PXDBCreatedByScreenID]
        public virtual string CreatedByScreenID { get; set; }

        public abstract class createdByScreenID : PX.Data.IBqlField { }

        #endregion
        #region CreatedDateTime

        [PXDBCreatedDateTime]
        public virtual DateTime? CreatedDateTime { get; set; }

        public abstract class createdDateTime : PX.Data.IBqlField { }

        #endregion
        #region LastModifiedByID

        [PXDBLastModifiedByID]
        public virtual Guid? LastModifiedByID { get; set; }

        public abstract class lastModifiedByID : PX.Data.IBqlField { }

        #endregion
        #region LastModifiedByScreenID

        [PXDBLastModifiedByScreenID]
        public virtual string LastModifiedByScreenID { get; set; }

        public abstract class lastModifiedByScreenID : PX.Data.IBqlField { }

        #endregion
        #region LastModifiedDateTime

        [PXDBLastModifiedDateTime]
        public virtual DateTime? LastModifiedDateTime { get; set; }

        public abstract class lastModifiedDateTime : PX.Data.IBqlField { }

        #endregion

        /*****************
         * Virtual Fields
        ******************/

        #region LegacyTerritoryCD

        public abstract class legacyTerritoryCD : PX.Data.BQL.BqlString.Field<legacyTerritoryCD> { }

        [PXString(50)]
        [PXUIField(DisplayName = "Legacy Territory", IsReadOnly = true)]
        [PXFormula(typeof(Selector<STSalesTerritoryMapping.salesTerritoryCD, SegmentValueExt.usrLegacyDeptTerritory>))]
        public virtual String LegacyTerritoryCD { get; set; }

        #endregion

        #region MarineLegacyTerritoryCD

        public abstract class marineLegacyTerritoryCD : PX.Data.BQL.BqlString.Field<marineLegacyTerritoryCD> { }

        [PXString(50)]
        [PXUIField(DisplayName = "Marine Legacy Territory", IsReadOnly = true)]
        [PXFormula(typeof(Selector<STSalesTerritoryMapping.marineTerritoryCD, SegmentValueExt.usrLegacyDeptTerritory>))]
        public virtual String MarineLegacyTerritoryCD { get; set; }

        #endregion

        #region ComRepDesc
        //public abstract class comRepDesc : IBqlField { }
        public abstract class comRepDesc : PX.Data.BQL.BqlString.Field<comRepDesc> { }

        [PXString]
        [PXUnboundDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Communication Rep Name")]

        public virtual String ComRepDesc { get; set; }

        #endregion

        #region ElectricRepDesc

        public abstract class electricRepDesc : PX.Data.BQL.BqlString.Field<electricRepDesc> { }

        [PXString]
        [PXUnboundDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Electrical Rep Name")]

        public virtual String ElectricRepDesc { get; set; }

        #endregion

        #region ConstructRepDesc
        public abstract class constructRepDesc : PX.Data.BQL.BqlString.Field<constructRepDesc> { }

        [PXString]
        [PXUnboundDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Construction Rep Name")]

        public virtual String ConstructRepDesc { get; set; }

        #endregion

        #region MarineRepDesc

        public abstract class marineRepDesc : PX.Data.BQL.BqlString.Field<marineRepDesc> { }

        [PXString]
        [PXUnboundDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Marine Rep Name")]

        public virtual String MarineRepDesc { get; set; }

        #endregion
    }
}
