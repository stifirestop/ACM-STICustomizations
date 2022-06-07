using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.CR;
using PX.Objects.SO;

namespace PX.STI.STICustom.DACExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class CustomerExt : PXCacheExtension<Customer>
    {
        #region AcctCD

        [PXMergeAttributes(Method = MergeMethod.Append)]
        [PXCustomizeSelectorColumns(
            typeof(Customer.acctCD),
            typeof(Customer.acctName),
            typeof(Customer.customerClassID),
            typeof(Customer.status),
            typeof(PX.Objects.CR.Contact.phone1),
            typeof(Address.city),
            typeof(Address.state),
            typeof(Address.countryID),
            typeof(PX.Objects.CR.Contact.eMail))]
        public string AcctCD { get; set; }

        #endregion
        #region TermsID

        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXDefault(typeof(Search<CustomerClass.termsID,
            Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
        public String TermsID { get; set; }

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
                            Where<State.countryID, Equal<Current<CustomerExt.usrBrokerCountry>>

            >>),
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

