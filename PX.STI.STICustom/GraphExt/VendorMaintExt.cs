using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.Extensions;
using PX.Objects.CR;
using PX.Objects.AP;
using PX.STI.STICustom.DACExt;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class VendorMaintExt : PXGraphExtension<VendorMaint>
    {
        #region Actions
        #endregion
        #region Events
        #endregion

        protected void _(Events.RowSelected<Vendor> eventHandler)
        {
            Vendor row = eventHandler.Row;
            //ContactExtAddress results = null;
            if (row is null) return;

            foreach (ContactExtAddress conExtAddRow in vendContacts.Select())
            {
                ContactExtAddressExt contactExtAddRow = conExtAddRow.GetExtension<ContactExtAddressExt>();

                if ((contactExtAddRow.UsrRepNbr == null || contactExtAddRow.UsrRepNbrDescr == null) && contactExtAddRow.UsrAccountLocationID != null)
                {
                    Location locRow = SelectFrom<Location>
                        .Where<Location.bAccountID.IsEqual<@P.AsInt>
                        .And<Location.locationID.IsEqual<@P.AsInt>>>
                        .View.Select(Base, row.BAccountID, contactExtAddRow.UsrAccountLocationID);


                    if (locRow != null)
                    {
                        LocationExt locExtRow = locRow.GetExtension<LocationExt>();

                        contactExtAddRow.UsrRepNbr = locExtRow.UsrRepNbr;

                        Contact conRow = SelectFrom<Contact>
                        .Where<Contact.bAccountID.IsEqual<@P.AsInt>
                        .And<Contact.contactID.IsEqual<@P.AsInt>>>
                        .View.Select(Base, row.BAccountID, conExtAddRow.ContactID);

                        if (conRow != null)
                        {
                            DACExt.ContactExt conExtRow = conRow.GetExtension<DACExt.ContactExt>();

                            conExtRow.UsrRepNbr = locExtRow.UsrRepNbr;
                        }
                    }
                }
            }







            //ContactExtAddress contactRow = SelectFrom<ContactExtAddress>
            //        .Where<ContactExtAddress.bAccountID.IsEqual<@P.AsInt>>
            //        .View.Select(Base, row.BAccountID);

            //using (new PXConnectionScope())
            //{
            //    results = SelectFrom<ContactExtAddress>
            //        .Where<ContactExtAddress.bAccountID.IsEqual<@P.AsInt>>
            //        .View.Select(Base, row.BAccountID);
            //}

            //PXResultset<ContactExtAddress> results = PXSelectBase<ContactExtAddress, Where<ContactExtAddress.bAccountID.IsEqual<row.BAccountID>>>.Select<Base>;
            //PXSelectJoin<OrderDetail, InnerJoin<SalesOrder,
            //On<SalesOrder.orderNbr, Equal<OrderDetail.orderNbr>>>>.Select(this);


            //public PXSelect<ContactExtAddress, Where<ContactExtAddress.bAccountID.IsEqual<row.BAccountID> contactRows;

            /*if (contactRow != null)
            {
                ContactExtAddressExt contactExtRow = contactRow.GetExtension<ContactExtAddressExt>();

                if (contactExtRow.UsrRepNbr == null && contactExtRow.UsrAccountLocationID != null)
                {
                    Location locRow = SelectFrom<Location>
                    .Where<Location.bAccountID.IsEqual<@P.AsInt>
                    .And<Location.locationID.IsEqual<@P.AsInt>>>
                    .View.Select(Base, row.BAccountID, contactExtRow.UsrAccountLocationID);

                    if (locRow != null)
                    {
                        LocationExt locExtRow = locRow.GetExtension<LocationExt>();

                        contactExtRow.UsrRepNbr = locExtRow.UsrRepNbr;
                        contactExtRow.UsrRepNbrDescr = locExtRow.UsrRepNbr + " - " + row.AcctName;
                    }
                }
            }*/

            //ContactExt rowExt = row.GetExtension<ContactExt>();
            //rowExt.UsrAccountLocationID = null;
        }

        #region Data Types

        public SelectFrom<ContactExtAddress>
            .Where<ContactExtAddress.bAccountID.IsEqual<Vendor.bAccountID.FromCurrent>>
            .View vendContacts;

        #endregion

    }
}
