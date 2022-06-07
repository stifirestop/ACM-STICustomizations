using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.EP;
using PX.STI.STICustom.DAC;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class EmployeeMaintExt : PXGraphExtension<EmployeeMaint>
    {
        #region Actions
        #endregion
        #region Events

        protected void _(Events.RowSelected<EPEmployee> eventHandler)
        {
            EPEmployee row = eventHandler.Row;
            if (row is null) return;

            EmployeeDepts.View.RequestRefresh();
        }

        #endregion
        #region Data Types
        #endregion

        public SelectFrom<STEmployeeDept>
            .Where<STEmployeeDept.employeeID.IsEqual<EPEmployee.bAccountID.FromCurrent>>
            .OrderBy<STEmployeeDept.employeeID.Asc, STEmployeeDept.employeeDeptCD.Asc>
            .View EmployeeDepts;
    }
}

