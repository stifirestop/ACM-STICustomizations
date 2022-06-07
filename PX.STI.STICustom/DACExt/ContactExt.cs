using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.STI.STICustom.Common;
using PX.STI.STICustom.DAC;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class ContactExt : PXCacheExtension<Contact>
    {
        #region UsrAccountLocationID

        public abstract class usrAccountLocationID : BqlInt.Field<usrAccountLocationID> { }

        [PXDBInt]
        [PXUIEnabled(typeof(Where<Contact.bAccountID.IsNotNull>))]
        [PXUIField(DisplayName = "Account Location", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(SearchFor<Location.locationID>
            .Where<Location.bAccountID.IsEqual<Contact.bAccountID.FromCurrent>>),
            SubstituteKey = typeof(Location.locationCD),
            DescriptionField = typeof(Location.descr))]
        [PXForeignReference(typeof(CompositeKey<
            Field<Contact.bAccountID>.IsRelatedTo<Location.bAccountID>,
            Field<ContactExt.usrAccountLocationID>.IsRelatedTo<Location.locationID>>))]
        public int? UsrAccountLocationID { get; set; }

        #endregion
        #region UsrContactSubClassID

        public abstract class usrContactSubClassID : BqlString.Field<usrContactSubClassID> { }

        [PXDBString(10, IsUnicode = true)]
        [PXUIEnabled(typeof(Where<Contact.classID.IsNotNull>))]
        [PXUIField(DisplayName = "Contact Sub Class", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(SearchFor<STContactSubClass.contactSubClassID>
            .Where<STContactSubClass.contactClassID.IsEqual<Contact.classID.FromCurrent>>),
            DescriptionField = typeof(STContactSubClass.description))]
        [PXForeignReference(typeof(CompositeKey<
            Field<Contact.classID>.IsRelatedTo<STContactSubClass.contactClassID>,
            Field<ContactExt.usrContactSubClassID>.IsRelatedTo<STContactSubClass.contactSubClassID>>))]
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
        #region UsrIsPrimary

        public abstract class usrIsPrimary : BqlBool.Field<usrIsPrimary> { }

        [PXDBBool(BqlField = typeof(usrIsPrimary))]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Primary", Visibility = PXUIVisibility.Visible)]
        public bool? UsrIsPrimary { get; set; }

        #endregion
        #region UsrIsPurchaser

        public abstract class usrIsPurchaser : BqlBool.Field<usrIsPurchaser> { }

        [PXDBBool(BqlField = typeof(usrIsPurchaser))]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Buyer", Visibility = PXUIVisibility.Visible)]
        public bool? UsrIsPurchaser { get; set; }

        #endregion
        #region UsrIsEskerContact

        public abstract class usrIsEskerContact : BqlBool.Field<usrIsEskerContact> { }

        [PXDBBool(BqlField = typeof(usrIsEskerContact))]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Esker", Visibility = PXUIVisibility.Visible)]
        public bool? UsrIsEskerContact { get; set; }

        #endregion
        #region UsrRepNbr

        public abstract class usrRepNbr : BqlString.Field<usrRepNbr> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "Rep Nbr.")]
        public string UsrRepNbr { get; set; }

        #endregion

        #region UsrRepNbrDescr

        [PXDBString(200)]
        [PXUIField(DisplayName = "Rep Name")]
        public abstract class usrRepNbrDescr : BqlString.Field<usrRepNbrDescr> { }
        public string UsrRepNbrDescr { get; set; }

        #endregion

        #region UsrAvalara

        public abstract class usrAvalara : BqlBool.Field<usrAvalara> { }

        [PXDBBool(BqlField = typeof(usrAvalara))]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Avalara", Visibility = PXUIVisibility.Visible)]
        public bool? UsrAvalara { get; set; }

        #endregion
        #region UsrSourceTypeID

        public abstract class usrSourceTypeID : BqlInt.Field<usrSourceTypeID> { }

        [PXDBInt()]
        [PXUIField(DisplayName = "Source Type", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<STSourceType.sourceTypeID,
            Where<STSourceType.isActive.IsEqual<True>>>),
            SubstituteKey = typeof(STSourceType.sourceTypeCD),
            DescriptionField = typeof(STSourceType.description))]
        public int? UsrSourceTypeID { get; set; }

        #endregion
        #region UsrSourceRefID

        public abstract class usrSourceRefID : BqlString.Field<usrSourceRefID> { }

        [PXDBString(72, IsUnicode = true)]
        [PXUIField(DisplayName = "Source Reference ID", Visibility = PXUIVisibility.Visible)]
        public string UsrSourceRefID { get; set; }

        #endregion
    }
}
