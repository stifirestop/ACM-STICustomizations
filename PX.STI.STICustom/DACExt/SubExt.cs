using System;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class SubExt : PXCacheExtension<Sub>
    {
        #region UsrNewDept

        public abstract class usrNewDept : BqlString.Field<usrNewDept> { }
        [PXDBString(4)]
        [PXUIField(DisplayName = "New Dept", Visibility = PXUIVisibility.Visible)]
        public string UsrNewDept { get; set; }

        #endregion
        #region UsrOldDept

        public abstract class usrOldDept : BqlString.Field<usrOldDept> { }
        [PXDBString(30)]
        [PXUIField(DisplayName = "OldDept", Visibility = PXUIVisibility.Visible)]
        public string UsrOldDept { get; set; }

        #endregion
        #region UsrOldDept2

        public abstract class usrOldDept2 : BqlString.Field<usrOldDept2> { }
        [PXDBString(30)]
        [PXUIField(DisplayName = "Old Dept", Visibility = PXUIVisibility.Visible)]
        public string UsrOldDept2 { get; set; }

        #endregion
    }
}
