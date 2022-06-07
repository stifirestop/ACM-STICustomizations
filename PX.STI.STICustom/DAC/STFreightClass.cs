using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.STI.STICustom.Common;
using PX.STI.STICustom.Graph;
using System;

namespace PX.STI.STICustom.DAC
{
    [Serializable]
    [PXPrimaryGraph(typeof(FreightClassMaint))]
    [PXCacheName(CustomView.FreightClass, PXDacType.Catalogue)]
    public class STFreightClass : IBqlTable
    {
        #region Keys

        public class PK : PrimaryKeyOf<STFreightClass>.By<freightClassID>
        {
            public static STFreightClass Find(PXGraph graph, int? freightClassID)
                => FindBy(graph, freightClassID);
        }

        #endregion
        #region FreightClassID

        public abstract class freightClassID : BqlInt.Field<freightClassID> { }
        protected int? _FreightClassID;

        [PXDBIdentity(IsKey = true)]
        public virtual int? FreightClassID
        {
            get => _FreightClassID;
            set => _FreightClassID = value;
        }

        #endregion
        #region FreightClassCD

        public abstract class freightClassCD : BqlString.Field<freightClassCD> { }
        protected string _FreightClassCD;

        [PXDBString(15, IsUnicode = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Freight Class", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string FreightClassCD
        {
            get => _FreightClassCD;
            set => _FreightClassCD = value;
        }

        #endregion
        #region Descr

        public abstract class descr : BqlString.Field<descr> { }
        protected string _Descr;

        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string Descr
        {
            get => _Descr;
            set => _Descr = value;
        }

        #endregion
        #region DOT

        public abstract class dOT : BqlString.Field<dOT> { }
        protected string _DOT;

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "DOT", Visibility = PXUIVisibility.Visible)]
        public virtual string DOT
        {
            get => _DOT;
            set => _DOT = value;
        }

        #endregion
        #region NMFC

        public abstract class nMFC : BqlString.Field<nMFC> { }
        protected string _NMFC;

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "NMFC", Visibility = PXUIVisibility.Visible)]
        public virtual string NMFC
        {
            get => _NMFC;
            set => _NMFC = value;
        }

        #endregion
        #region HSTariffCode

        public abstract class hSTariffCode : BqlString.Field<hSTariffCode> { }
        protected string _HSTariffCode;

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "HS Tariff Code", Visibility = PXUIVisibility.Visible)]
        public virtual string HSTariffCode
        {
            get => _HSTariffCode;
            set => _HSTariffCode = value;
        }

        #endregion
        #region TariffCodeDescr

        public abstract class tariffCodeDescr : BqlString.Field<tariffCodeDescr> { }
        protected string _TariffCodeDescr;

        [PXDBString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Tariff Code Desc", Visibility = PXUIVisibility.Visible)]
        public virtual string TariffCodeDescr
        {
            get => _TariffCodeDescr;
            set => _TariffCodeDescr = value;
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
        public virtual String CreatedByScreenID
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
        public virtual String LastModifiedByScreenID
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
        protected Byte[] _Tstamp;

        [PXDBTimestamp]
        public virtual Byte[] Tstamp
        {
            get => _Tstamp;
            set => _Tstamp = value;
        }

        #endregion
    }
}

