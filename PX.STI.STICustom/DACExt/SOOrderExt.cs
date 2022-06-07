using PX.Data;
using PX.Data.BQL;
using PX.Objects.SO;
using PX.Objects.CS;
using PX.Objects.CR;
using PX.STI.STICustom.DACExt.Attribute;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class SOOrderExt : PXCacheExtension<SOOrder>
    {
        #region UsrCSROverride

        public abstract class usrCSROverride : BqlBool.Field<usrCSROverride> { }

        [PXDBBool]
        [BuyerOverrideEvents]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Override Contact", Visibility = PXUIVisibility.Visible)]
        public bool? UsrCSROverride { get; set; }

        #endregion
        #region UsrCSRAttention

        public abstract class usrCSRAttention : BqlString.Field<usrCSRAttention> { }

        [PXDBString(255, IsUnicode = true)]
        public string UsrCSRAttention { get; set; }

        #endregion
        #region UsrCSREmail

        public abstract class usrCSREmail : BqlString.Field<usrCSREmail> { }

        [PXDBString(255, IsUnicode = true)]
        public string UsrCSREmail { get; set; }

        #endregion
        #region UsrCSRDisplayAttention

        public abstract class usrCSRDisplayAttention : BqlString.Field<usrCSRDisplayAttention> { }

        [PXString(255, IsUnicode = true)]
        [DisplayAttentionEvents]
        [PXUIEnabled(typeof(Where<usrCSROverride.IsEqual<True>>))]
        [PXUIField(DisplayName = "Buyer Attention", Visibility = PXUIVisibility.Visible)]
        public string UsrCSRDisplayAttention { get; set; }

        #endregion
        #region UsrCSRDisplayEmail

        public abstract class usrCSRDisplayEmail : BqlString.Field<usrCSRDisplayEmail> { }

        [PXString(255, IsUnicode = true)]
        [DisplayEmailEvents]
        [PXUIEnabled(typeof(Where<usrCSROverride.IsEqual<True>>))]
        [PXUIField(DisplayName = "Buyer Email", Visibility = PXUIVisibility.Visible)]
        public string UsrCSRDisplayEmail { get; set; }

        #endregion
        #region CustomerLocationID

        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXCustomizeSelectorColumns(
            typeof(Location.locationCD),
            typeof(Location.descr),
            typeof(Address.addressLine1),
            typeof(Address.addressLine2),
            typeof(Address.city),
            typeof(Address.state),
            typeof(Address.countryID))]
        [LocationID(
            typeof(
                Where<Location.bAccountID.IsEqual<SOOrder.customerID.FromCurrent>
                    .And<Location.isActive.IsEqual<True>
                    .And<MatchWithBranch<Location.cBranchID>>>>),
            typeof(
                InnerJoin<Address,
                    On<Location.defAddressID.IsEqual<Address.addressID>>>),
            DescriptionField = typeof(Location.descr),
            Visibility = PXUIVisibility.SelectorVisible)]
        public int? CustomerLocationID { get; set; }

        #endregion
        #region UsrDelApptReq

        public abstract class usrDelApptReq : BqlBool.Field<usrDelApptReq> { }

        [PXDBBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Delivery Appt Req'd", Visibility = PXUIVisibility.Visible)]
        public bool? UsrDelApptReq { get; set; }

        #endregion
        #region Usr3dAccountNumber

        public abstract class usr3dAccountNumber : BqlString.Field<usr3dAccountNumber> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "Account Number", Visibility = PXUIVisibility.Visible)]
        public string Usr3dAccountNumber { get; set; }

        #endregion
        #region Usr3dAddrName

        public abstract class usr3dAddrName : BqlString.Field<usr3dAddrName> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "Address Name", Visibility = PXUIVisibility.Visible)]
        public string Usr3dAddrName { get; set; }

        #endregion
        #region Usr3dAddrCity

        public abstract class usr3dAddrCity : BqlString.Field<usr3dAddrCity> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "City", Visibility = PXUIVisibility.Visible)]
        public string Usr3dAddrCity { get; set; }

        #endregion
        #region Usr3dAddrZip

        public abstract class usr3dAddrZip : BqlString.Field<usr3dAddrZip> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "Zip Code", Visibility = PXUIVisibility.Visible)]
        public string Usr3dAddrZip { get; set; }

        #endregion
        #region Usr3dAddrState

        public abstract class usr3dAddrState : BqlString.Field<usr3dAddrState> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "State", Visibility = PXUIVisibility.Visible)]
        public string Usr3dAddrState { get; set; }

        #endregion
        #region Usr3dAddrLine1

        public abstract class usr3dAddrLine1 : BqlString.Field<usr3dAddrLine1> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "Address Line 1", Visibility = PXUIVisibility.Visible)]
        public string Usr3dAddrLine1 { get; set; }

        #endregion
        #region Usr3dAddrLine2

        public abstract class usr3dAddrLine2 : BqlString.Field<usr3dAddrLine2> { }

        [PXDBString(30)]
        [PXUIField(DisplayName = "Address Line 2", Visibility = PXUIVisibility.Visible)]
        public string Usr3dAddrLine2 { get; set; }

        #endregion

        #region UsrBrokerAccountNumber

        public abstract class usrBrokerAccountNumber : BqlString.Field<usrBrokerAccountNumber> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Account Number", Visibility = PXUIVisibility.Visible)]
        public string UsrBrokerAccountNumber { get; set; }

        #endregion
        #region UsrBrokerAddrName

        public abstract class usrBrokerAddrName : BqlString.Field<usrBrokerAddrName> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Broker Name", Visibility = PXUIVisibility.Visible)]
        public string UsrBrokerAddrName { get; set; }

        #endregion
        #region UsrBrokerPOCName

        public abstract class usrBrokerPOCName : BqlString.Field<usrBrokerPOCName> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Contact Name", Visibility = PXUIVisibility.Visible)]
        public string UsrBrokerPOCName { get; set; }

        #endregion
        #region UsrBrokerAddrCity

        public abstract class usrBrokerAddrCity : BqlString.Field<usrBrokerAddrCity> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "City", Visibility = PXUIVisibility.Visible)]
        public string UsrBrokerAddrCity { get; set; }

        #endregion
        #region UsrBrokerAddrZip

        public abstract class usrBrokerAddrZip : BqlString.Field<usrBrokerAddrZip> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Zip Code", Visibility = PXUIVisibility.Visible)]
        public string UsrBrokerAddrZip { get; set; }

        #endregion
        #region UsrBrokerCountry

        public abstract class usrBrokerCountry : BqlString.Field<usrBrokerCountry> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Country", Visibility = PXUIVisibility.Visible)]
        [PXDefault("US", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXSelector(typeof(Search<Country.countryID>),
            typeof(Country.description)
        )]
        [PXCustomizeSelectorColumns(typeof(Country.countryID), typeof(Country.description))]
        public string UsrBrokerCountry { get; set; }

        #endregion
        #region UsrBrokerAddrState

        public abstract class usrBrokerAddrState : BqlString.Field<usrBrokerAddrState> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "State", Visibility = PXUIVisibility.Visible)]

        [PXSelector(typeof(Search<State.stateID,
                            Where<State.countryID, Equal<Current<SOOrderExt.usrBrokerCountry

            >>>>),
            typeof(State.name)
        )]
        [PXCustomizeSelectorColumns(typeof(State.stateID), typeof(State.name))]
        public string UsrBrokerAddrState { get; set; }

        #endregion
        #region UsrBrokerAddrLine1

        public abstract class usrBrokerAddrLine1 : BqlString.Field<usrBrokerAddrLine1> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Address Line 1", Visibility = PXUIVisibility.Visible)]
        public string UsrBrokerAddrLine1 { get; set; }

        #endregion
        #region UsrBrokerAddrLine2

        public abstract class usrBrokerAddrLine2 : BqlString.Field<usrBrokerAddrLine2> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Address Line 2", Visibility = PXUIVisibility.Visible)]
        public string UsrBrokerAddrLine2 { get; set; }

        #endregion

        #region UsrBrokerPhone

        public abstract class usrBrokerPhone : BqlString.Field<usrBrokerPhone> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Phone", Visibility = PXUIVisibility.Visible)]
        public string UsrBrokerPhone { get; set; }

        #endregion

        #region UsrBrokerFax

        public abstract class usrBrokerFax : BqlString.Field<usrBrokerFax> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Fax", Visibility = PXUIVisibility.Visible)]
        public string UsrBrokerFax { get; set; }

        #endregion

        #region UsrBrokerEmail

        public abstract class usrBrokerEmail : BqlString.Field<usrBrokerEmail> { }

        [PXDBString(255)]
        [PXUIField(DisplayName = "Email", Visibility = PXUIVisibility.Visible)]
        public string UsrBrokerEmail { get; set; }

        #endregion

        #region UsrBrokerBN

        public abstract class usrBrokerBN : BqlString.Field<usrBrokerBN> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "BN", Visibility = PXUIVisibility.Visible)]
        public string UsrBrokerBN { get; set; }

        #endregion
    }
}
