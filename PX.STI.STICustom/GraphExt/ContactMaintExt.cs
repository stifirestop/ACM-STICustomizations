using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.AP;
using PX.STI.STICustom.DACExt;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class ContactMaintExt : PXGraphExtension<ContactMaint>
    {
        #region Actions
        #endregion
        #region Events

        protected void _(Events.FieldUpdated<Contact, Contact.classID> eventHandler)
        {
            Contact row = eventHandler.Row;
            if (row is null) return;

            ContactExt rowExt = row.GetExtension<ContactExt>();
            rowExt.UsrContactSubClassID = null;
        }

        protected void _(Events.FieldUpdated<Contact, Contact.bAccountID> eventHandler)
        {
            Contact row = eventHandler.Row;
            if (row is null) return;

            ContactExt rowExt = row.GetExtension<ContactExt>();
            rowExt.UsrAccountLocationID = null;
        }



        protected void _(Events.RowInserting<Contact> eventHandler)
        {
            Contact row = eventHandler.Row;
            if (row is null) return;

            ContactExt rowExt = row.GetExtension<ContactExt>();

            if (rowExt.UsrAccountLocationID != null)
            {
                Location locRow = SelectFrom<Location>
                    .Where<Location.bAccountID.IsEqual<@P.AsInt>
                    .And<Location.locationID.IsEqual<@P.AsInt>>>
                    .View.Select(Base, row.BAccountID, rowExt.UsrAccountLocationID);

                LocationExt locExtRow = locRow.GetExtension<LocationExt>();

                if (locRow != null)
                {
                    rowExt.UsrRepNbr = locExtRow.UsrRepNbr;
                }
            }
        }
        protected void _(Events.RowUpdated<Contact> eventHandler)
        {
            Contact row = eventHandler.Row;
            if (row is null) return;

            ContactExt rowExt = row.GetExtension<ContactExt>();

            if (rowExt.UsrAccountLocationID != null)
            {
                Location locRow = SelectFrom<Location>
                    .Where<Location.bAccountID.IsEqual<@P.AsInt>
                    .And<Location.locationID.IsEqual<@P.AsInt>>>
                    .View.Select(Base, row.BAccountID, rowExt.UsrAccountLocationID);

                LocationExt locExtRow = locRow.GetExtension<LocationExt>();

                if (locRow != null)
                {
                    rowExt.UsrRepNbr = locExtRow.UsrRepNbr;
                }
            }
        }

        #endregion
        #region Data Types
        #endregion
    }
}