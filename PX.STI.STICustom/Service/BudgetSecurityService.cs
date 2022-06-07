using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PX.STI.STICustom.Service
{
    public static class BudgetSecurityService
    {
        public static string AccessError(string finYear) => "User is not authorized to access this financial year: " + finYear;

        public static bool ValidateBudgetAccess(PXCache cache, Guid userID, string year, int? ledgerID)
        {
            bool flag = false;
            if (((cache == null) || (year == null)) || !ledgerID.HasValue)
            {
                return true;
            }
            object[] objArray1 = new object[] { ledgerID };
            Ledger ledger = PXSelectBase<Ledger, PXViewOf<Ledger>.BasedOn<SelectFromBase<Ledger, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Ledger.ledgerID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(cache.Graph, objArray1);
            if (ledger?.LedgerCD == "BUDGET" && year == "2022" || ledger?.LedgerCD == "ESTIMATE" && year == "2021" || year == null || !ledgerID.HasValue)
            {
                flag = true;
            }
            else
            {
                object[] objArray2 = new object[] { userID };
                Users users = PXSelectBase<Users, PXViewOf<Users>.BasedOn<SelectFromBase<Users, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Users.pKID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select(cache.Graph, objArray2);
                if (users == null)
                {
                    return flag;
                }
                object[] objArray3 = new object[] { users.Username };
                List<UsersInRoles> list = GraphHelper.RowCast<UsersInRoles>(PXSelectBase<UsersInRoles, PXViewOf<UsersInRoles>.BasedOn<SelectFromBase<UsersInRoles, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<UsersInRoles.username, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(cache.Graph, objArray3)).ToList<UsersInRoles>();
                //if ((users.Username == "azgombic" || users.Username == "aalcantara" || users.Username == "jgoodfellow") || list.Exists(x => x.Rolename.Equals("Administrator")))
                if (list.Exists(x => x.Rolename.Equals("Budget Admin")) || list.Exists(x => x.Rolename.Equals("Administrator")))
                {
                    flag = true;
                }
            }
            return flag;
        }



    }
}
