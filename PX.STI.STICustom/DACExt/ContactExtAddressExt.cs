using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.STI.STICustom.Common;

namespace PX.STI.STICustom.DACExt
{
    public sealed class ContactExtAddressExt : PXCacheExtension<ContactExtAddress>
    {
        public static bool IsActive() { return true; }

        #region UsrAccountLocationID

        public abstract class usrAccountLocationID : BqlInt.Field<usrAccountLocationID> { }

        [PXDBInt(BqlField = typeof(ContactExtAddressExt.usrAccountLocationID))]
        [PXUIField(DisplayName = "Account Location", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(SearchFor<Location.locationID>
            .Where<Location.bAccountID.IsEqual<Contact.bAccountID.FromCurrent>>),
            SubstituteKey = typeof(Location.locationCD),
            DescriptionField = typeof(Location.descr))]
        public int? UsrAccountLocationID { get; set; }

        #endregion
        #region UsrIsPurchaser

        public abstract class usrIsPurchaser : BqlBool.Field<usrIsPurchaser> { }

        [PXDBBool(BqlField = typeof(ContactExtAddressExt.usrIsPurchaser))]
        [PXUIField(DisplayName = "Buyer", Visibility = PXUIVisibility.Visible)]
        public bool? UsrIsPurchaser { get; set; }

        #endregion
        #region UsrContactSubClassID

        public abstract class usrContactSubClassID : BqlString.Field<usrContactSubClassID> { }

        [PXDBString(BqlField = typeof(ContactExtAddressExt.usrContactSubClassID))]
        [PXUIField(DisplayName = "Contact Sub Class", Visibility = PXUIVisibility.Visible)]
        public string UsrContactSubClassID { get; set; }

        #endregion
        #region UsrVendorID

        public abstract class usrVendorID : BqlInt.Field<usrVendorID> { }

        [PXDBInt()]
        [PXUIField(DisplayName = "Rep ID", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<Vendor.bAccountID>),
            SubstituteKey = typeof(Vendor.acctCD),
            DescriptionField = typeof(Vendor.acctName))]
        // TODO: Implement with visible only when set to Sales Rep / Independant Rep
        //[PXUIVisible()]
        public int? UsrVendorID { get; set; }

        #endregion
        #region ClassID

        public abstract class classID : BqlString.Field<classID> { }

        [PXDBString(BqlField = typeof(Contact.classID))]
        [PXUIField(DisplayName = "Contact Class", Visibility = PXUIVisibility.Visible)]
        public string ClassID { get; set; }

        #endregion
        #region UsrRepNbr

        public abstract class usrRepNbr : BqlString.Field<usrRepNbr> { }

        [PXDBString(30, BqlField = typeof(ContactExtAddressExt.usrRepNbr))]
        [PXUIField(DisplayName = "Rep Nbr.")]

        public string UsrRepNbr { get; set; }

        #endregion
        #region UsrRepNbrDescr

        public abstract class usrRepNbrDescr : BqlString.Field<usrRepNbrDescr> { }

        [PXDBString(200, BqlField = typeof(ContactExtAddressExt.usrRepNbrDescr))]
        [PXUIField(DisplayName = "Rep Name")]

        public string UsrRepNbrDescr { get; set; }

        #endregion
        #region UsrAvalara

        public abstract class usrAvalara : BqlBool.Field<usrAvalara> { }

        [PXDBBool(BqlField = typeof(ContactExtAddressExt.usrAvalara))]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Avalara", Visibility = PXUIVisibility.Visible)]
        public bool? UsrAvalara { get; set; }

        #endregion
        #region UsrIsEskerContact

        public abstract class usrIsEskerContact : BqlBool.Field<usrIsEskerContact> { }

        [PXDBBool(BqlField = typeof(usrIsEskerContact))]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Esker", Visibility = PXUIVisibility.Visible)]
        public bool? UsrIsEskerContact { get; set; }

        #endregion
    }
}
