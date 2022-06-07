using System;
using System.Linq;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.SO;
using PX.Objects.IN;
using PX.STI.STICustom.Common;
using PX.STI.STICustom.DACExt;
using System.Collections;
using System.Collections.Generic;
using Messages = PX.STI.STICustom.Common.CustomMessages;

namespace PX.STI.STICustom.GraphExt
{
    public class SOOrderEntryExt : PXGraphExtension<SOOrderEntry>
    {
        public static bool IsActive() { return true; }
        #region Actions

        public PXAction<SOOrder> viewNote;
        [PXUIField(DisplayName = "Customer Note")]
        [PXButton()]
        protected void ViewNote()
        {
            SOOrder order = Base.Document.Current;
            string combinedNote;

            if (order is null)
                return;

            using (new PXConnectionScope())
            {
                Customer customer = Base.customer.Select();
                Note note = ShowDataNote.SelectSingle();

                if (note is null)
                    return;

                string noteText = (note.NoteText?.Trim().Length > 0)
                    ? note.NoteText.Trim()
                    : "";

                string notePopupText = (note.NotePopupText?.Trim().Length > 0)
                    ? note.NotePopupText.Trim()
                    : "";

                combinedNote = (noteText.Length > 0 && notePopupText.Length > 0)
                    ? string.Format("{0}{1}{2}-----{3}{4}{5}", noteText, Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, notePopupText)
                    : (noteText.Length > 0 && notePopupText.Length <= 0)
                        ? noteText
                        : (noteText.Length <= 0 && notePopupText.Length > 0)
                            ? notePopupText
                            : "";

                if (combinedNote.Length > 0 && combinedNote.Contains(customer.AcctCD))
                    combinedNote = string.Empty;
            }

            if (combinedNote.Length != 0 && combinedNote.Trim() != "")
            {
                WebDialogResult result = Base.Document.View.Ask(Base.Document.Current, "Customer Note", combinedNote, MessageButtons.OK, MessageIcon.None);
            }
        }

        #endregion
        #region Events

        protected void _(Events.FieldUpdated<SOOrder, SOOrder.customerOrderNbr> eventHandler)
        {
            SOOrder row = eventHandler.Row;
            if (row is null || row.CustomerOrderNbr is null) return;

            SOOrder record;

            if (eventHandler.Cache.GetStatus(row) != PXEntryStatus.InsertedDeleted && eventHandler.Cache.GetStatus(row) != PXEntryStatus.Inserted)
                record = SelectFrom<SOOrder>
                    .Where<SOOrder.customerOrderNbr.IsEqual<SOOrder.customerOrderNbr.FromCurrent>
                    .And<SOOrder.orderNbr.IsNotEqual<SOOrder.orderNbr.FromCurrent>>>
                    .View.ReadOnly.Select(Base);
            else
                record = SelectFrom<SOOrder>
                    .Where<SOOrder.customerOrderNbr.IsEqual<SOOrder.customerOrderNbr.FromCurrent>>
                    .View.ReadOnly.Select(Base);

            if (record != null)
            {
                string message = "Customer PO Number already exists!";
                eventHandler.Cache.RaiseExceptionHandling<SOOrder.customerOrderNbr>(row, row.CustomerOrderNbr,
                    new PXSetPropertyException(message, PXErrorLevel.Warning));
            }
        }

        protected void _(Events.RowPersisting<SOOrder> eventHandler)
        {
            SOOrder row = eventHandler.Row;
            if (row is null) return;

            if (PXContext.GetScreenID() == "SO.30.10.00"
            && Messages.OrderTypesRequiringCustNbr.Contains(row.OrderType)
            && row.CustomerOrderNbr is null)
                eventHandler.Cache.RaiseExceptionHandling<SOOrder.customerOrderNbr>(row, row.CustomerOrderNbr,
                    new PXSetPropertyException(Messages.CustomerOrderNbrRequired, PXErrorLevel.Error));
        }

        // Has not been changed to FBQL CacheAttached because HeaderUDF1 is not available in this customization.
        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXUIField(DisplayName = "Account Number", Visibility = PXUIVisibility.Visible)]
        private protected virtual void SOOrder_ISPSHeaderUDF1_CacheAttached(PXCache cache) { }

        private protected virtual void _(Events.FieldDefaulting<SOLine, SOLineExt.usrShipVia> eventHandler)
        {
            SOLine row = eventHandler.Row;
            if (row is null || Base.Document.Current.ShipVia is null) return;

            SOLineExt extendedRow = row.GetExtension<SOLineExt>();
            extendedRow.UsrShipVia = Base.Document.Current.ShipVia.Trim();
        }

        protected void _(Events.FieldUpdated<SOLine, SOLineExt.usrShipVia> eventHandler)
        {
            SOLine row = eventHandler.Row;
            if (row is null) return;

            CheckSOLineShipVia(row);
        }

        protected void _(Events.FieldUpdated<SOLine, SOLine.orderQty> eventHandler)
        {
            SOLine row = eventHandler.Row;
            if (row is null) return;

            bool stockBtn = ((string)PXContext.Session["AddStockBtn"] == "Y");
            string screenID = PXContext.GetScreenID();

            if (screenID == "SO.30.20.00" || screenID != "SO.30.10.00" || stockBtn) return;
            if (row.OrderQty is null || row.OrderQty == 0) return;

            if (row.OrderType == "CS" || row.OrderType == "SA" || row.OrderType == SOOrderTypeConstants.QuoteOrder
                || row.OrderType == SOOrderTypeConstants.Invoice || row.OrderType == SOOrderTypeConstants.SalesOrder
                || row.OrderType == SOOrderTypeConstants.TransferOrder)
            {
                InventoryItem item = (InventoryItem)PXSelectorAttribute.Select<SOLine.inventoryID>(eventHandler.Cache, row);
                INSite warehouse = (INSite)PXSelectorAttribute.Select<SOLine.siteID>(eventHandler.Cache, row);

                INSiteStatus inventory = SelectFrom<INSiteStatus>
                    .Where<INSiteStatus.inventoryID.IsEqual<@P.AsInt>
                    .And<INSiteStatus.siteID.IsEqual<@P.AsInt>
                    .And<INSiteStatus.subItemID.IsEqual<@P.AsInt>>>>
                    .View.Select(eventHandler.Cache.Graph, row.InventoryID, row.SiteID, row.SubItemID);

                if (inventory is null) return;

                if (row.OrderQty != Decimal.Zero && row.OrderQty > inventory.QtyAvail)
                {
                    string message = "Updating Item " + item.InventoryCD + "in Warehouse " + warehouse.SiteCD + "exceeds Qty Available!";
                    WebDialogResult result = Base.Document.View.Ask("1", row, "Item Status", message, MessageButtons.OK, MessageIcon.Information, true);
                }

                InventoryItemExt extendedItem = PXCache<InventoryItem>.GetExtension<InventoryItemExt>(item);

                if (row.OrderQty != Decimal.Zero && extendedItem.UsrMultipleOfReq != Decimal.Zero)
                {
                    decimal? modulo = (row.OrderQty % extendedItem.UsrMultipleOfReq);

                    if (row.OrderQty % extendedItem.UsrMultipleOfReq != 0)
                    {
                        string message2 = "Item " + item.InventoryCD + " has a Case Qty of  " + extendedItem.UsrMultipleOfReq + Environment.NewLine + " Item must be sold in the multiples.";
                        WebDialogResult result2 = Base.Document.View.Ask("2", row, "Item Case Qty", message2, MessageButtons.OK, MessageIcon.Information, true);
                    }
                }
            }
        }

        protected void _(Events.FieldUpdated<SOLine, SOLine.siteID> eventHandler)
        {
            SOLine row = eventHandler.Row;
            if (row is null) return;

            if (row.OrderQty is null || row.OrderQty == Decimal.Zero)
                return;

            eventHandler.Cache.RaiseFieldUpdated<SOLine.orderQty>(row, null);
            Base.Transactions.Cache.RaiseFieldUpdated<SOLine.orderQty>(row, null);
            CheckSOLineShipVia(row);
        }

        protected void _(Events.RowPersisting<SOLine> eventHandler)
        {
            SOLine row = eventHandler.Row;
            if (row is null) return;

            if (PXContext.GetScreenID() == "SO.30.10.00")
                CheckSOLineShipVia(row);
        }

        protected void _(Events.FieldUpdated<SOOrder, SOOrder.fOBPoint> eventHandler)
        {
            SOOrder row = eventHandler.Row;
            if (row is null) return;

            row.ShipTermsID = row.FOBPoint;
        }

        protected void _(Events.FieldUpdated<SOOrder, SOOrder.shipVia> eventHandler)
        {
            SOOrder row = eventHandler.Row;
            if (row is null) return;

            if (PXContext.GetScreenID() == "SO.30.10.00" && row.ShipVia != null)
            {
                List<SOLine> transactions = Base.Transactions.Select().RowCast<SOLine>().ToList();

                if (transactions.Count == 0)
                    return;

                WebDialogResult result = Base.Document.View.Ask("ShipViaUpdate", row, "Ship Via", Messages.ShipViaUpdateAllLines, MessageButtons.YesNo, MessageIcon.Information);

                if (result == WebDialogResult.Yes)
                {
                    foreach (SOLine line in transactions)
                    {
                        SOLineExt extendedLine = PXCache<SOLine>.GetExtension<SOLineExt>(line);
                        extendedLine.UsrShipVia = row.ShipVia.Trim();
                        Base.Transactions.Update(line);
                        Base.Transactions.View.RequestRefresh();
                    }
                }
            }
        }

        protected void _(Events.FieldUpdated<SOOrder, SOOrder.customerID> eventHandler)
        {
            SOOrder row = eventHandler.Row;
            if (row is null) return;

            Customer cust = SelectFrom<Customer>
                   .Where<Customer.bAccountID.IsEqual<@P.AsInt>>
                   .View.Select(eventHandler.Cache.Graph, row.CustomerID);

            if (cust is null) return;

            SOOrderExt rowExt = row.GetExtension<SOOrderExt>();
            CustomerExt custExt = cust.GetExtension<CustomerExt>();

            if (rowExt != null && custExt != null)
            {
                rowExt.UsrBrokerAccountNumber = custExt.UsrBrokerAccountNumber;
                rowExt.UsrBrokerAddrCity = custExt.UsrBrokerAddrCity;
                rowExt.UsrBrokerAddrLine1 = custExt.UsrBrokerAddrLine1;
                rowExt.UsrBrokerAddrLine2 = custExt.UsrBrokerAddrLine2;
                rowExt.UsrBrokerAddrName = custExt.UsrBrokerAddrName;
                rowExt.UsrBrokerAddrState = custExt.UsrBrokerAddrState;
                rowExt.UsrBrokerAddrZip = custExt.UsrBrokerAddrZip;
                rowExt.UsrBrokerBN = custExt.UsrBrokerBN;
                rowExt.UsrBrokerCountry = custExt.UsrBrokerCountry;
                rowExt.UsrBrokerEmail = custExt.UsrBrokerEmail;
                rowExt.UsrBrokerFax = custExt.UsrBrokerFax;
                rowExt.UsrBrokerPhone = custExt.UsrBrokerPhone;
                rowExt.UsrBrokerPOCName = custExt.UsrBrokerPOCName;
            }
        }

        #endregion
        #region Data Types
        #endregion

        public delegate IEnumerable AddInvSelBySiteDelegate(PXAdapter adapter);

        [PXOverride()]
        public IEnumerable AddInvSelBySite(PXAdapter adapter, AddInvSelBySiteDelegate baseMethod)
        {
            foreach (SOSiteStatusSelected line in Base.sitestatus.Cache.Cached)
            {
                if (line.Selected is true && line.QtySelected > Decimal.Zero)
                    PXContext.Session.SetString("AddStockBtn", "Y");
            }

            return baseMethod(adapter);
        }

        public void CheckSOLineShipVia(SOLine record, PXErrorLevel errorLevel = PXErrorLevel.Warning)
        {
            SOLineExt extendedRecord = record.GetExtension<SOLineExt>();

            bool error = SelectFrom<SOLine>
                .Where<SOLine.orderType.IsEqual<@P.AsString>
                    .And<SOLine.orderNbr.IsEqual<@P.AsString>
                    .And<SOLine.lineNbr.IsNotEqual<@P.AsInt>
                    .And<SOLine.siteID.IsEqual<@P.AsInt>
                    .And<SOLineExt.usrShipVia.IsNotEqual<@P.AsString>>>>>>
                .View.Select(Base, record.OrderType, record.OrderNbr, record.LineNbr, record.SiteID, extendedRecord.UsrShipVia)
                .Count() > 0;

            if (error)
                Base.Transactions.Cache.RaiseExceptionHandling<SOLineExt.usrShipVia>(record, extendedRecord.UsrShipVia,
                    new PXSetPropertyException(Messages.ShipViaMisMatch, errorLevel));
        }

        public int MessageCounter
        {
            get => (PXContext.Session["MessageCounter"] is null)
                ? 0
                : int.Parse(PXContext.Session["MessageCounter"].ToString());

            set => PXContext.Session.SetString("MessageCounter", value.ToString());
        }

        public string MessageOrderNbr
        {
            get => (PXContext.Session["MessageOrderNbr"] is null)
                ? ""
                : PXContext.Session["MessageOrderNbr"].ToString();

            set => PXContext.Session.SetString("MessageOrderNbr", (value ?? ""));
        }

        public SelectFrom<Note>
            .InnerJoin<Customer>
                .On<Customer.noteID.IsEqual<Note.noteID>>
            .Where<Customer.bAccountID.IsEqual<SOOrder.customerID.FromCurrent>>
            .View ShowDataNote;

        public override void Initialize()
        {
            base.Initialize();

            viewNote.SetEnabled(true);
        }
    }
}
