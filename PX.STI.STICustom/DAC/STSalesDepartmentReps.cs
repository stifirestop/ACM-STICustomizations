using System;
using System.Linq;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.CR;
using PX.Objects.AR;
using PX.STI.STICustom.Common;
using PX.STI.STICustom.Service;
using PX.STI.STICustom.DACExt;

namespace PX.STI.STICustom.DAC
{
    [Serializable()]
    public partial class STSalesDepartmentReps : PX.Data.IBqlTable
    {

        #region SalesDepartmentID

        public abstract class salesDepartmentID : PX.Data.BQL.BqlInt.Field<salesDepartmentID> { }

        [PXDBIdentity(IsKey = true)]
        public virtual int? SalesDepartmentID { get; set; }

        #endregion

        #region SalesDepartmentCD

        public abstract class salesDepartmentCD : PX.Data.BQL.BqlString.Field<salesDepartmentCD> { }

        [PXDBString(30, IsUnicode = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Sales Territory")]
        [PXParent(typeof(Select<SegmentValue,
            Where<SegmentValue.dimensionID, Equal<TerritorySegmentType.Dimension>,
            And<SegmentValue.segmentID, Equal<TerritorySegmentType.Segment>,
            And<SegmentValue.value, Equal<Current<STSalesDepartmentReps.salesDepartmentCD>>>>>>))]
        [PXSelector(typeof(Search<SegmentValue.value,
            Where<SegmentValue.dimensionID, Equal<TerritorySegmentType.Dimension>,
            And<SegmentValue.segmentID, Equal<TerritorySegmentType.Segment>,
            And<SegmentValue.active, Equal<True>,
            And<SegmentValueExt.usrIsSalesTerritory, Equal<True>>>>>>),
            DescriptionField = typeof(SegmentValue.descr))]
        public virtual String SalesDepartmentCD { get; set; }

        #endregion
        #region LegacyDepartmentCD

        public abstract class legacyDepartmentCD : PX.Data.BQL.BqlString.Field<legacyDepartmentCD> { }

        [PXString(30)]
        [PXUIField(DisplayName = "Legacy Territory", IsReadOnly = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Selector<STSalesDepartmentReps.salesDepartmentCD, SegmentValueExt.usrLegacyDeptTerritory>))]
        public virtual String LegacyDepartmentCD { get; set; }

        #endregion
        #region DeptRepID

        public abstract class deptRepID : PX.Data.BQL.BqlString.Field<deptRepID> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "Salesperson ID")]

        [PXSelector(typeof(Search<SalesPerson.salesPersonCD,
                            Where<SalesPerson.isActive, Equal<True>
            //And<LocationExt.usrIsMarine, Equal<True>


            >>
            ),
            typeof(SalesPerson.salesPersonCD),
            typeof(SalesPerson.descr)
        )]
        [PXCustomizeSelectorColumns(typeof(SalesPerson.salesPersonCD), typeof(SalesPerson.descr))]

        public virtual String DeptRepID { get; set; }

        #endregion


        #region DeptRepCD

        public abstract class deptRepCD : PX.Data.BQL.BqlString.Field<deptRepCD> { }

        [PXString(256)]
        [PXUIField(DisplayName = "Salesperson Name")]
        [PXFormula(typeof(Selector<STSalesDepartmentReps.deptRepID, SalesPerson.descr>))]

        public virtual String DeptRepCD { get; set; }

        #endregion

        #region DepartmentName

        public abstract class departmentName : PX.Data.BQL.BqlString.Field<departmentName> { }

        [PXString(256)]
        [PXUIField(DisplayName = "Territory Name")]
        [PXFormula(typeof(Selector<STSalesDepartmentReps.salesDepartmentCD, SegmentValue.descr>))]

        public virtual String DepartmentName { get; set; }

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

    }
}
