using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.STI.STICustom.Common;
using System;

namespace PX.STI.STICustom.DAC
{
    [Serializable]
    [PXPrimaryGraph(typeof(EmployeeMaint))]
    [PXCacheName(CustomView.EmployeeDept, PXDacType.Catalogue)]
    public class STEmployeeDept : IBqlTable, INotable, IPXSelectable
    {
        #region Keys

        public class PK : PrimaryKeyOf<STEmployeeDept>.By<employeeID, employeeDeptCD>
        {
            public static STEmployeeDept Find(PXGraph graph, int? employeeID, string employeeDeptCD)
                => FindBy(graph, employeeID, employeeDeptCD);
        }

        public static class FK
        {
            public class Employee : EPEmployee.PK.ForeignKeyOf<STEmployeeDept>.By<employeeID> { }
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
        #region EmployeeDeptID

        public abstract class employeeDeptID : BqlInt.Field<employeeDeptID> { }
        protected int? _EmployeeDeptID;
        [PXDBIdentity()]
        public virtual int? EmployeeDeptID
        {
            get => _EmployeeDeptID;
            set => _EmployeeDeptID = value;
        }

        #endregion
        #region EmployeeID

        public abstract class employeeID : BqlInt.Field<employeeID> { }
        protected int? _EmployeeID;
        [PXDBInt(IsKey = true)]
        [PXDBDefault(typeof(EPEmployee.bAccountID))]
        [PXUIField(DisplayName = "Employee ID", Visibility = PXUIVisibility.Invisible)]
        //[PXParent(typeof(Select<EPEmployee,
        //    Where<EPEmployee.bAccountID.IsEqual<EPEmployeePosition.employeeID.FromCurrent>>>))]
        public virtual int? EmployeeID
        {
            get => _EmployeeID;
            set => _EmployeeID = value;
        }

        #endregion
        #region EmployeeDeptCD

        public abstract class employeeDeptCD : BqlString.Field<employeeDeptCD> { }
        protected string _EmployeeDeptCD;
        [PXDBString(30, IsUnicode = true, IsKey = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Department", Visibility = PXUIVisibility.SelectorVisible)]
        [PXParent(typeof(Select<SegmentValue,
            Where<SegmentValue.dimensionID.IsEqual<DepartmentSegmentType.Dimension>
                .And<SegmentValue.segmentID.IsEqual<DepartmentSegmentType.Segment>
                .And<SegmentValue.value.IsEqual<employeeDeptCD.FromCurrent>>>>>))]
        [PXSelector(typeof(Search<SegmentValue.value,
            Where<SegmentValue.dimensionID.IsEqual<DepartmentSegmentType.Dimension>
                .And<SegmentValue.segmentID.IsEqual<DepartmentSegmentType.Segment>
                .And<SegmentValue.active.IsEqual<True>>>>>),
            DescriptionField = typeof(SegmentValue.descr))]
        public virtual string EmployeeDeptCD
        {
            get => _EmployeeDeptCD;
            set => _EmployeeDeptCD = value;
        }

        #endregion
        #region IsReportingDept

        public abstract class isReportingDept : BqlBool.Field<isReportingDept> { }
        protected bool? _IsReportingDept;
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Sales Reporting Dept.", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual bool? IsReportingDept
        {
            get => _IsReportingDept;
            set => _IsReportingDept = value;
        }

        #endregion
        #region IsBudgetDept

        public abstract class isBudgetDept : BqlBool.Field<isBudgetDept> { }
        protected bool? _IsBudgetDept;
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Budget Dept.", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual bool? IsBudgetDept
        {
            get => _IsBudgetDept;
            set => _IsBudgetDept = value;
        }

        #endregion
        #region NoteID

        public abstract class noteID : BqlGuid.Field<noteID> { }
        protected Guid? _NoteID;
        [PXNote()]
        public virtual Guid? NoteID
        {
            get => _NoteID;
            set => _NoteID = value;
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
        public virtual string CreatedByScreenID
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
        public virtual string LastModifiedByScreenID
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
        protected byte[] _Tstamp;
        [PXDBTimestamp]
        public virtual byte[] Tstamp
        {
            get => _Tstamp;
            set => _Tstamp = value;
        }

        #endregion
    }

    public static class DepartmentSegmentType
    {
        public class Dimension : BqlString.Constant<Dimension> { public Dimension() : base("SUBACCOUNT") { } }
        public class Segment : BqlInt.Constant<Segment> { public Segment() : base(1) { } }
    }
}

