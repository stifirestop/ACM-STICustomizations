using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;

namespace PX.STI.STICustom.Service
{
    public static class AccountService
    {
        public static BAccount FetchBAccount(PXGraph graph, int? customerID)
        {
            BAccount result = null;

            using (new PXConnectionScope())
            {
                result = SelectFrom<BAccount>
                    .Where<BAccount.bAccountID.IsEqual<@P.AsInt>>
                    .View.Select(graph, customerID);
            }

            return result;
        }

        public static Customer FetchCustomer(PXGraph graph, int? customerID)
        {
            Customer result = null;

            using (new PXConnectionScope())
            {
                result = SelectFrom<Customer>
                    .Where<Customer.bAccountID.IsEqual<@P.AsInt>>
                    .View.Select(graph, customerID);
            }

            return result;
        }

        public static Address FetchCustomerAddress(PXGraph graph, int? customerID, int? addressID)
        {
            Address result = null;

            using (new PXConnectionScope())
            {
                result = graph.Select<Address>()
                    .Where(address =>
                        address.BAccountID == customerID
                        && address.AddressID == addressID
                    ).FirstOrDefault();
            }

            return result;
        }

        public static string FetchCustomerSubChannel(PXGraph graph, Customer customer)
        {
            string result = null;
            CSAnswers xRow = null;

            using (new PXConnectionScope())
            {
                xRow = SelectFrom<CSAnswers>
                .Where<CSAnswers.refNoteID.IsEqual<@P.AsGuid>
                .And<CSAnswers.attributeID.IsEqual<@P.AsString>>>
                .View.Select(graph, customer.NoteID, "SUBCHANNEL");

                if (xRow != null)
                {
                    result = xRow.Value;
                }
            }

            return result;
        }
    }
}

