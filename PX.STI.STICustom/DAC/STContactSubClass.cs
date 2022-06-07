using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.STI.STICustom.Common;
using System;

namespace PX.STI.STICustom.DAC
{
    [Serializable]
    [PXPrimaryGraph(typeof(ContactMaint))]
    [PXCacheName(CustomView.ContactSubClass, PXDacType.Catalogue)]
    public class STContactSubClass : IBqlTable, IPXSelectable
    {
        #region Keys

        public class PK : PrimaryKeyOf<STContactSubClass>.By<contactClassID, contactSubClassID>
        {
            public static STContactSubClass Find(PXGraph graph, string contactClassID, string contactSubClassID)
                => FindBy(graph, contactClassID, contactSubClassID);
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
        #region ContactClassID

        public abstract class contactClassID : BqlString.Field<contactClassID> { }
        protected string _ContactClassID;

        [PXDBString(10, IsUnicode = true, IsKey = true)]
        [PXDBDefault(typeof(CRContactClass.classID))]
        [PXUIField(DisplayName = "Contact Class ID", Visibility = PXUIVisibility.Invisible, Enabled = false)]
        [PXParent(typeof(SelectFrom<CRContactClass>
            .Where<CRContactClass.classID.IsEqual<contactClassID.FromCurrent>>))]
        public virtual string ContactClassID
        {
            get => _ContactClassID;
            set => _ContactClassID = value;
        }

        #endregion
        #region ContactSubClassID

        public abstract class contactSubClassID : BqlString.Field<contactSubClassID> { }
        protected string _ContactSubClassID;

        [PXDBString(10, IsUnicode = true, IsKey = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Contact Sub Class", Visibility = PXUIVisibility.SelectorVisible)]
        [PXReferentialIntegrityCheck]
        public virtual string ContactSubClassID
        {
            get => _ContactSubClassID;
            set => _ContactSubClassID = value;
        }

        #endregion
        #region Description

        public abstract class description : BqlString.Field<description> { }
        protected string _Description;
        [PXDBLocalizableString(DescriptionLength.Medium, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual String Description
        {
            get => _Description;
            set => _Description = value;
        }

        #endregion
        #region IsActive

        public abstract class isActive : BqlBool.Field<isActive> { }
        protected Boolean? _IsActive;
        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Active", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Boolean? IsActive
        {
            get => _IsActive;
            set => _IsActive = value;
        }

        #endregion
        #region CreatedByID

        public abstract class createdByID : BqlGuid.Field<createdByID> { }
        protected Guid? _CreatedByID;
        [PXDBCreatedByID()]
        public virtual Guid? CreatedByID
        {
            get => _CreatedByID;
            set => _CreatedByID = value;
        }

        #endregion
        #region CreatedByScreenID

        public abstract class createdByScreenID : BqlString.Field<createdByScreenID> { }
        protected string _CreatedByScreenID;
        [PXDBCreatedByScreenID()]
        public virtual String CreatedByScreenID
        {
            get => _CreatedByScreenID;
            set => _CreatedByScreenID = value;
        }

        #endregion
        #region CreatedDateTime

        public abstract class createdDateTime : BqlDateTime.Field<createdDateTime> { }
        protected DateTime? _CreatedDateTime;
        [PXDBCreatedDateTime()]
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
        [PXDBLastModifiedByID()]
        public virtual Guid? LastModifiedByID
        {
            get => _LastModifiedByID;
            set => _LastModifiedByID = value;
        }

        #endregion
        #region LastModifiedByScreenID

        public abstract class lastModifiedByScreenID : BqlString.Field<lastModifiedByScreenID> { }
        protected string _LastModifiedByScreenID;
        [PXDBLastModifiedByScreenID()]
        public virtual String LastModifiedByScreenID
        {
            get => _LastModifiedByScreenID;
            set => _LastModifiedByScreenID = value;
        }

        #endregion
        #region LastModifiedDateTime

        public abstract class lastModifiedDateTime : BqlDateTime.Field<lastModifiedDateTime> { }
        protected DateTime? _LastModifiedDateTime;
        [PXDBLastModifiedDateTime()]
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
