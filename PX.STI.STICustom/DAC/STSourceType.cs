using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.STI.STICustom.Common;
using PX.STI.STICustom.Graph;
using System;

namespace PX.STI.STICustom.DAC
{
	[Serializable]
	[PXPrimaryGraph(typeof(SourceTypeMaint))]
	[PXCacheName(CustomView.SourceType, PXDacType.Catalogue, CacheGlobal = true)]
	public class STSourceType : IBqlTable
	{
		#region Keys

		public class PK : PrimaryKeyOf<STSourceType>.By<sourceTypeID>
		{
			public static STSourceType Find(PXGraph graph, int? sourceTypeID)
				=> FindBy(graph, sourceTypeID);
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
		#region SourceTypeID

		public abstract class sourceTypeID : BqlInt.Field<sourceTypeID> { }
		protected Int32? _SourceTypeID;

		[PXDBIdentity(IsKey = true)]
		public virtual Int32? SourceTypeID
		{
			get => _SourceTypeID;
			set => _SourceTypeID = value;
		}

		#endregion
		#region SourceTypeCD

		public abstract class sourceTypeCD : BqlString.Field<sourceTypeCD> { }
		protected String _SourceTypeCD;

		[PXCheckUnique]
		[PXDBString(2, IsFixed = true, InputMask = ">aa")]
		[PXDefault]
		[PXUIField(DisplayName = "Source Type", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String SourceTypeCD
		{
			get => _SourceTypeCD;
			set => _SourceTypeCD = value;
		}

		#endregion
		#region Description

		public abstract class description : BqlString.Field<description> { }
		protected string _Description;

		[PXDBLocalizableString(DescriptionLength.Medium, IsUnicode = true)]
		[PXDefault]
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
		#region Direction?

		// Direction of integration

		#endregion
		#region APIEndpoint?

		// API endpoint of integration

		#endregion
		#region NoteID

		public abstract class noteID : BqlGuid.Field<noteID> { }
		protected Guid? _NoteID;

		[PXNote]
		public virtual Guid? NoteID
		{
			get => _NoteID;
			set => _NoteID = value;
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
