using System;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class SegmentValueExt : PXCacheExtension<SegmentValue>
    {
        #region UsrDeptSubAccount

        public abstract class usrDeptSubAccount : BqlInt.Field<usrDeptSubAccount> { }
        [PXUIField(DisplayName = "Budget Subaccount", Visibility = PXUIVisibility.SelectorVisible)]
        [SubAccount()]
        public int? UsrDeptSubAccount { get; set; }

        #endregion
        #region UsrLegacyDeptTerritory

        public abstract class usrLegacyDeptTerritory : BqlString.Field<usrLegacyDeptTerritory> { }
        [PXDBString(50)]
        [PXUIField(DisplayName = "Legacy Dept Territory")]
        public virtual String UsrLegacyDeptTerritory { get; set; }

        #endregion
        #region UsrIsSalesTerritory

        public abstract class usrIsSalesTerritory : BqlBool.Field<usrIsSalesTerritory> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Sales Territory")]
        public virtual Boolean? UsrIsSalesTerritory { get; set; }

        #endregion
        #region UsrIsMarineTerritory

        public abstract class usrIsMarineTerritory : BqlBool.Field<usrIsMarineTerritory> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Marine Territory")]
        public virtual Boolean? UsrIsMarineTerritory { get; set; }

        #endregion
    }
}

