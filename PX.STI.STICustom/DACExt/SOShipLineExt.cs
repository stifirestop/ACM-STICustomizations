using PX.Data;
using PX.Data.BQL;
using PX.Objects.SO;
using PX.STI.STICustom.DAC;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class SOShipLineExt : PXCacheExtension<SOShipLine>
    {
        #region UsrFreightClassID

        public abstract class usrFreightClassID : BqlString.Field<usrFreightClassID> { }

        [PXDBString(15)]
        [PXUIField(DisplayName = "Freight Class", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(STFreightClass.freightClassCD),
            DescriptionField = typeof(STFreightClass.descr))]
        public string UsrFreightClassID { get; set; }

        #endregion
        #region UsrHazmat

        public abstract class usrHazmat : BqlBool.Field<usrHazmat> { }

        [PXDBBool]
        [PXUIField(DisplayName = "Hazmat", Visibility = PXUIVisibility.Visible)]
        public bool? UsrHazmat { get; set; }

        #endregion
        #region UsrCaseCount

        public abstract class usrCaseCount : BqlInt.Field<usrCaseCount> { }

        [PXDBInt]
        [PXUIField(DisplayName = "Case Count", Visibility = PXUIVisibility.Visible)]
        public int? UsrCaseCount { get; set; }

        #endregion
    }
}

