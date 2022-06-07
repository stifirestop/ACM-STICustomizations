using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class AccountExt : PXCacheExtension<Account>
    {
        #region UsrMASAcct

        public abstract class usrMASAcct : BqlString.Field<usrMASAcct> { }
        [PXDBString(4)]
        [PXUIField(DisplayName = "MAS Acct")]
        public string UsrMASAcct { get; set; }

        #endregion
    }
}
