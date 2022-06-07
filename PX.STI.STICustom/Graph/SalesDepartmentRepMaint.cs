using System;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.STI.STICustom.DAC;


namespace PX.STI.STICustom.Graph
{
    public class SalesDepartmentRepMaint : PXGraph<SalesDepartmentRepMaint>
    {
        #region Actions
        #endregion
        #region Events

        protected void _(Events.FieldUpdating<STSalesDepartmentReps, STSalesDepartmentReps.deptRepID> eventHandler)
        {
            STSalesDepartmentReps row = eventHandler.Row;
            SalesPerson salesPersonRow = null;


            string newVal = null;


            if (eventHandler.NewValue == null)
            {
                row.DeptRepCD = null;
                return;
            }

            newVal = eventHandler.NewValue.ToString();

            using (new PXConnectionScope())
            {
                salesPersonRow = SelectFrom<SalesPerson>
                    .Where<SalesPerson.salesPersonCD.IsEqual<@P.AsString>>
                    .View.Select(eventHandler.Cache.Graph, newVal);
            }

            if (salesPersonRow != null)
            {
                row.DeptRepCD = salesPersonRow.Descr;
            }
            else
            {
                row.DeptRepCD = null;
            }
        }
        #endregion
        #region Data Types
        #endregion

        public PXSave<STSalesDepartmentReps> Save;
        public PXCancel<STSalesDepartmentReps> Cancel;

        [PXViewName("SalesTerritoryMappings")]
        public SelectFrom<STSalesDepartmentReps>.View SalesDepartmentReps;
    }
}
