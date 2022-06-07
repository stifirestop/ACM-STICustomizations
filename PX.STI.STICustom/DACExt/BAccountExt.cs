using System;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.STI.STICustom.Common;

namespace PX.STI.STICustom.DACExt
{
    public sealed class BAccountExt : PXCacheExtension<BAccount>
    {
        public static bool IsActive() { return true; }

        #region UsrIsNationalParent

        // TODO: Possibly deprecated - remove at future time.
        public abstract class usrIsNationalParent : BqlBool.Field<usrIsNationalParent> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Parent Account")]
        public bool? UsrIsNationalParent { get; set; }

        #endregion
        #region UsrIsParentAccount

        public abstract class usrIsParentAccount : BqlBool.Field<usrIsParentAccount> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Parent Account")]
        public bool? UsrIsParentAccount { get; set; }

        #endregion

        #region UsrIsPointOfSale

        /*public abstract class usrIsPointOfSale : PX.Data.IBqlField { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Point-Of-Sale Customer")]
        public virtual Boolean? UsrIsPointOfSale { get; set; }*/

        #endregion
        #region UsrAccountType

        public abstract class usrAccountType : PX.Data.IBqlField { }
        [PXDBString(3, IsFixed = true)]
        [PXDefault(CustomerAccountType.Standard, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXStringList(
            new string[] { CustomerAccountType.Standard, CustomerAccountType.PointOfSale, CustomerAccountType.House },
            new string[] { "Standard", "POS", "House" }
        )]
        [PXUIField(DisplayName = "Account Type")]
        public String UsrAccountType { get; set; }

        #endregion
        #region UsrEstablishedDate

        public abstract class usrEstablishedDate : PX.Data.IBqlField, PX.Data.IBqlOperand { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Established Date", Enabled = false)]
        [PXDefault(typeof(AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.Nothing)]
        public DateTime? UsrEstablishedDate { get; set; }

        #endregion
    }
}
