using PX.Data;
using PX.Objects.GL;
using PX.STI.STICustom.Service;

namespace PX.STI.STICustom.DACExt
{
    public class GLBudgetEntryExt : PXGraphExtension<GLBudgetEntry>
    {
        public static bool IsActive() { return true; }

        protected void _(Events.FieldVerifying<BudgetFilter, BudgetFilter.finYear> eventHandler)
        {
            BudgetFilter row = eventHandler.Row;
            if (row == null || eventHandler.NewValue == null)
                return;
            string str = eventHandler.NewValue.ToString();
            if (!BudgetSecurityService.ValidateBudgetAccess(eventHandler.Cache, PXAccess.GetUserID(), str, row.LedgerID))
            {
                eventHandler.NewValue = null;
                this.Base.BudgetArticles.Cache.Clear();
                throw new PXSetPropertyException(BudgetSecurityService.AccessError(str), (PXErrorLevel)4);
            }
        }

        protected void _(Events.FieldUpdated<BudgetFilter, BudgetFilter.ledgerID> eventHandler)
        {
            BudgetFilter row = eventHandler.Row;
            if (row == null || eventHandler.NewValue == null)
                return;
            string finYear = row.FinYear;
            int? ledgerId = row.LedgerID;
            if (BudgetSecurityService.ValidateBudgetAccess(eventHandler.Cache, PXAccess.GetUserID(), finYear, ledgerId))
                return;
            eventHandler.Cache.SetValueExt<BudgetFilter.finYear>(row, null);
            this.Base.BudgetArticles.Cache.Clear();
            PXUIFieldAttribute.SetError<BudgetFilter.finYear>(eventHandler.Cache, row, BudgetSecurityService.AccessError(finYear));
        }

        protected void _(Events.RowInserting<BudgetFilter> eventHandler)
        {
            BudgetFilter row = eventHandler.Row;
            if (row == null || BudgetSecurityService.ValidateBudgetAccess(eventHandler.Cache, PXAccess.GetUserID(), row.FinYear, row.LedgerID))
                return;
            eventHandler.Cache.SetValueExt<BudgetFilter.finYear>(row, null);

            this.Base.BudgetArticles.Cache.Clear();
            PXUIFieldAttribute.SetError<BudgetFilter.finYear>(eventHandler.Cache, row, BudgetSecurityService.AccessError(row.FinYear));
        }
    }
}
