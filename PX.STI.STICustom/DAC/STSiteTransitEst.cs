using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.STI.STICustom.Common;
using PX.STI.STICustom.DACExt.Attribute;
using System;

namespace PX.STI.STICustom.DAC
{
    [Serializable]
    [PXPrimaryGraph(typeof(INSiteMaint))]
    [PXCacheName(CustomView.TransitEst, PXDacType.Details)]
    public class STSiteTransitEst : IBqlTable, IPXSelectable
    {
        #region Keys

        public class PK : PrimaryKeyOf<STSiteTransitEst>.By<siteID, countryID, stateID>
        {
            public static STSiteTransitEst Find(PXGraph graph, int? siteID, string countryID, string stateID)
                => FindBy(graph, siteID, countryID, stateID);
        }

        public static class FK
        {
            public class Site : INSite.PK.ForeignKeyOf<STSiteTransitEst>.By<siteID> { }
            public class Country : Objects.CS.Country.PK.ForeignKeyOf<STSiteTransitEst>.By<countryID> { }
        }

        #endregion
        #region Selected

        public abstract class selected : BqlBool.Field<selected> { }
        protected bool? _Selected;

        [PXBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Selected", Visibility = PXUIVisibility.Service)]
        public virtual bool? Selected
        {
            get => _Selected;
            set => _Selected = value;
        }

        #endregion
        #region SiteID

        public abstract class siteID : BqlInt.Field<siteID> { }
        protected int? _SiteID;

        [PXDBInt(IsKey = true)]
        [PXDBDefault(typeof(INSite.siteID))]
        [PXUIField(DisplayName = "Warehouse", Visibility = PXUIVisibility.Invisible, Enabled = false)]
        public int? SiteID
        {
            get => _SiteID;
            set => _SiteID = value;
        }

        #endregion
        #region CountryID

        public abstract class countryID : BqlString.Field<countryID> { }
        protected string _CountryID;

        [PXDBString(100, IsKey = true)]
        [SiteTransitCountryEvents]
        [PXDefault(typeof(Search<Objects.GL.Branch.countryID,
            Where<Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
        [PXUIField(DisplayName = "Country", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<Country.countryID,
            Where<Country.countryID.IsEqual<CountryCode.UnitedStates>
                .Or<Country.countryID.IsEqual<CountryCode.Canada>>>>),
            DescriptionField = typeof(Country.description))]
        public virtual string CountryID
        {
            get => _CountryID;
            set => _CountryID = value;
        }

        #endregion
        #region StateID

        public abstract class stateID : BqlString.Field<stateID> { }
        protected string _StateID;

        [PXDBString(50, IsKey = true, IsUnicode = true)]
        [PXDefault]
        [PXUIField(DisplayName = "State", Visibility = PXUIVisibility.Visible)]
        [State(typeof(countryID),
            DescriptionField = typeof(State.name))]
        public string StateID
        {
            get => _StateID;
            set => _StateID = value;
        }

        #endregion
        #region EstTransitDays

        public abstract class estTransitDays : BqlInt.Field<estTransitDays> { }
        protected int? _EstTransitDays;

        [PXDBInt]
        [PXDefault]
        [PXUIField(DisplayName = "Est. Transit Days", Visibility = PXUIVisibility.Visible)]
        public int? EstTransitDays
        {
            get => _EstTransitDays;
            set => _EstTransitDays = value;
        }

        #endregion
        #region CreatedByID

        public abstract class createdByID : BqlGuid.Field<createdByID> { }
        protected Guid? _CreatedByID;

        [PXDBCreatedByID]
        public virtual Guid? CreatedByID
        {
            get => _CreatedByID;
            set => _CreatedByID = value;
        }

        #endregion
        #region CreatedByScreenID

        public abstract class createdByScreenID : BqlString.Field<createdByScreenID> { }
        protected string _CreatedByScreenID;

        [PXDBCreatedByScreenID]
        public virtual string CreatedByScreenID
        {
            get => _CreatedByScreenID;
            set => _CreatedByScreenID = value;
        }

        #endregion
        #region CreatedDateTime

        public abstract class createdDateTime : BqlDateTime.Field<createdDateTime> { }
        protected DateTime? _CreatedDateTime;

        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
        public virtual DateTime? CreatedDateTime
        {
            get => _CreatedDateTime;
            set => _CreatedDateTime = value;
        }

        #endregion
        #region LastModifiedByID

        public abstract class lastModifiedByID : BqlGuid.Field<lastModifiedByID> { }
        protected Guid? _LastModifiedByID;

        [PXDBLastModifiedByID]
        public virtual Guid? LastModifiedByID
        {
            get => _LastModifiedByID;
            set => _LastModifiedByID = value;
        }

        #endregion
        #region LastModifiedByScreenID

        public abstract class lastModifiedByScreenID : BqlString.Field<lastModifiedByScreenID> { }
        protected string _LastModifiedByScreenID;

        [PXDBLastModifiedByScreenID]
        public virtual string LastModifiedByScreenID
        {
            get => _LastModifiedByScreenID;
            set => _LastModifiedByScreenID = value;
        }

        #endregion
        #region LastModifiedDateTime

        public abstract class lastModifiedDateTime : BqlDateTime.Field<lastModifiedDateTime> { }
        protected DateTime? _LastModifiedDateTime;

        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
        public virtual DateTime? LastModifiedDateTime
        {
            get => _LastModifiedDateTime;
            set => _LastModifiedDateTime = value;
        }

        #endregion
        #region tstamp

        public abstract class tstamp : BqlByteArray.Field<tstamp> { }
        protected byte[] _Tstamp;

        [PXDBTimestamp]
        public virtual byte[] Tstamp
        {
            get => _Tstamp;
            set => _Tstamp = value;
        }

        #endregion
    }
}

