using System;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.SO;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class SOShipmentExt : PXCacheExtension<SOShipment>
    {
        #region UsrDelAppReq

        public abstract class usrDelAppReq : BqlBool.Field<usrDelAppReq> { }

        [PXDBBool]
        [PXUIField(DisplayName = "Delivery Appt Req", Visibility = PXUIVisibility.Visible)]
        public bool? UsrDelAppReq { get; set; }

        #endregion
        //#region UsrTotalPallets

        //public abstract class usrTotalPallets : BqlInt.Field<usrTotalPallets> { }

        //[PXDBInt]
        //[PXUIField(DisplayName = "# Pallets / Packages", Visibility = PXUIVisibility.Visible)]
        //public int? UsrTotalPallets { get; set; }

        //#endregion
        #region UsrExtBOLNbr

        public abstract class usrExtBOLNbr : BqlString.Field<usrExtBOLNbr> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "External BOL Nbr", Visibility = PXUIVisibility.Visible)]
        public string UsrExtBOLNbr { get; set; }

        #endregion

        #region ISPSDeliveryDate
        public abstract class ispsDeliveryDate : PX.Data.IBqlField, PX.Data.IBqlOperand { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Delivery Date")]
        public virtual DateTime? ISPSDeliveryDate { get; set; }
        #endregion

        #region ISPSDelTime
        public abstract class ispsDelTime : PX.Data.IBqlField, PX.Data.IBqlOperand { }
        [PXDBTimeSpan()]
        //[PXDBInt()]
        [PXUIField(DisplayName = "Delivery Time")]
        public virtual int? ISPSDelTime { get; set; }
        #endregion

        #region ISPSProNumber
        public abstract class ispsProNumber : PX.Data.IBqlField, PX.Data.IBqlOperand { }
        [PXDBString(30)]
        [PXUIField(DisplayName = "PRO Number")]
        public virtual string ISPSProNumber { get; set; }
        #endregion

        #region UsrTotalPallets
        public abstract class usrTotalPallets : PX.Data.IBqlField, PX.Data.IBqlOperand { }
        [PXDBInt()]
        [PXUIField(DisplayName = "Total Pallets")]
        public virtual int? UsrTotalPallets { get; set; }
        #endregion

        #region ISPSTotalNumPallets
        public abstract class ispsTotalNumPallets : PX.Data.IBqlField, PX.Data.IBqlOperand { }
        [PXDBInt()]
        [PXUIField(DisplayName = "Total Number of Pallets")]
        public virtual int? ISPSTotalNumPallets { get; set; }
        #endregion
    }
}

