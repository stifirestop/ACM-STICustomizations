using System;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.STI.STICustom.DAC;
using PX.STI.STICustom.DACExt;


namespace PX.STI.STICustom.Graph
{
    public class SalesTerritoryMappingMaint : PXGraph<SalesTerritoryMappingMaint>
    {
        #region Actions
        #endregion
        #region Events
        protected void _(Events.FieldDefaulting<STSalesTerritoryMapping, STSalesTerritoryMapping.comRepDesc> e)
        {
            STSalesTerritoryMapping row = e.Row;
            if (row.ComRep != null)
            {
                SalesPerson spLookup = SelectFrom<SalesPerson>.Where<SalesPerson.salesPersonCD.IsEqual<@P.AsString>>.View.Select(this, row.ComRep);

                e.NewValue = spLookup.Descr;
            }
        }
        protected void _(Events.FieldDefaulting<STSalesTerritoryMapping, STSalesTerritoryMapping.constructRepDesc> e)
        {
            STSalesTerritoryMapping row = e.Row;
            if (row.ConstuctRep != null)
            {
                SalesPerson spLookup = SelectFrom<SalesPerson>.Where<SalesPerson.salesPersonCD.IsEqual<@P.AsString>>.View.Select(this, row.ConstuctRep);

                e.NewValue = spLookup.Descr;
            }
        }
        protected void _(Events.FieldDefaulting<STSalesTerritoryMapping, STSalesTerritoryMapping.electricRepDesc> e)
        {
            STSalesTerritoryMapping row = e.Row;
            if (row.ElectricRep != null)
            {
                SalesPerson spLookup = SelectFrom<SalesPerson>.Where<SalesPerson.salesPersonCD.IsEqual<@P.AsString>>.View.Select(this, row.ElectricRep);

                e.NewValue = spLookup.Descr;
            }
        }
        protected void _(Events.FieldDefaulting<STSalesTerritoryMapping, STSalesTerritoryMapping.marineRepDesc> e)
        {
            STSalesTerritoryMapping row = e.Row;
            if (row.MarineRep != null)
            {
                SalesPerson spLookup = SelectFrom<SalesPerson>.Where<SalesPerson.salesPersonCD.IsEqual<@P.AsString>>.View.Select(this, row.MarineRep);

                e.NewValue = spLookup.Descr;
            }
        }

        protected void _(Events.FieldUpdating<STSalesTerritoryMapping, STSalesTerritoryMapping.marineTerritoryCD> eventHandler)
        {
            STSalesTerritoryMapping row = eventHandler.Row;
            STSalesDepartmentReps deptRow;

            if (eventHandler.NewValue == null)
            {
                return;
            }


            row.MarineRep = null;
            row.MarineRepDesc = null;

            using (new PXConnectionScope())
            {
                deptRow = SelectFrom<STSalesDepartmentReps>
                    .Where<STSalesDepartmentReps.salesDepartmentCD.IsEqual<@P.AsString>>
                    .View.Select(eventHandler.Cache.Graph, eventHandler.NewValue);
            }

            if (deptRow != null)
            {
                if (!string.IsNullOrEmpty(deptRow.DeptRepID))
                {
                    row.MarineRep = deptRow.DeptRepID;
                    row.MarineRepDesc = deptRow.DeptRepCD;
                }
            }
        }
        protected void _(Events.FieldUpdating<STSalesTerritoryMapping, STSalesTerritoryMapping.salesTerritoryCD> eventHandler)
        {
            STSalesTerritoryMapping row = eventHandler.Row;
            STSalesDepartmentReps deptRow;
            Location locRow;

            string newVal = null;

            if (eventHandler.NewValue == null)
            {
                return;
            }

            newVal = eventHandler.NewValue.ToString();

            using (new PXConnectionScope())
            {
                deptRow = SelectFrom<STSalesDepartmentReps>
                    .Where<STSalesDepartmentReps.salesDepartmentCD.IsEqual<@P.AsString>>
                    .View.Select(eventHandler.Cache.Graph, newVal);
            }

            if (deptRow != null)
            {
                locRow = SelectFrom<Location>
                    .Where<LocationExt.usrRepNbr.IsEqual<@P.AsString>
                    .And<Location.status.IsEqual<@P.AsString>>>
                    .View.Select(eventHandler.Cache.Graph, deptRow.DeptRepID, "A");

                if (locRow != null)
                {
                    LocationExt locRowExt = locRow.GetExtension<LocationExt>();

                    /*if ()
                    if (!string.IsNullOrEmpty(deptRow.ConstructRepID))
                    {
                        row.ConstuctRep = deptRow.ConstructRepID;
                        row.ConstructRepDesc = deptRow.ConstructRepCD;
                    }

                    if (!string.IsNullOrEmpty(deptRow.ElectricRepID))
                    {
                        row.ElectricRep = deptRow.ElectricRepID;
                        row.ElectricRepDesc = deptRow.ElectricRepCD;
                    }

                    if (!string.IsNullOrEmpty(deptRow.CommRepID))
                    {
                        row.ComRep = deptRow.CommRepID;
                        row.ComRepDesc = deptRow.CommRepCD;
                    }*/
                }

                //if (!string.IsNullOrEmpty(deptRow.InternationalRepID))
                //{
                //    row.ComRep = deptRow.InternationalRepID;
                //}

                //if (!string.IsNullOrEmpty(deptRow.FabRepID))
                //{
                //    row.ComRep = deptRow.FabRepID;
                //}
            }
        }
        //protected void _(Events.FieldUpdating<STSalesTerritoryMapping, STSalesTerritoryMapping.comRep> eventHandler)
        //{
        //    STSalesTerritoryMapping row = eventHandler.Row;
        //    Location locationRow = null;


        //    string newVal = null;


        //    if (eventHandler.NewValue == null)
        //    {
        //        row.ComRepDesc = string.Empty;
        //        return;
        //    }

        //    newVal = eventHandler.NewValue.ToString();

        //    using (new PXConnectionScope())
        //    {
        //        locationRow = SelectFrom<Location>
        //            .Where<LocationExt.usrRepNbr.IsEqual<@P.AsString>
        //            .And<Location.status.IsEqual<@P.AsString>>>
        //            .View.Select(eventHandler.Cache.Graph, newVal, "A");
        //    }



        //    if (locationRow != null)
        //    {
        //        row.ComRepDesc = locationRow.Descr;
        //    }
        //    else
        //    {
        //        row.ComRepDesc = string.Empty;
        //    }
        //}

        //protected void _(Events.FieldUpdating<STSalesTerritoryMapping, STSalesTerritoryMapping.constuctRep> eventHandler)
        //{
        //    STSalesTerritoryMapping row = eventHandler.Row;
        //    Location locationRow = null;


        //    string newVal = null;


        //    if (eventHandler.NewValue == null)
        //    {
        //        row.ConstructRepDesc = null;
        //        return;
        //    }

        //    newVal = eventHandler.NewValue.ToString();

        //    using (new PXConnectionScope())
        //    {
        //        locationRow = SelectFrom<Location>
        //            .Where<LocationExt.usrRepNbr.IsEqual<@P.AsString>
        //            .And<Location.status.IsEqual<@P.AsString>>>
        //            .View.Select(eventHandler.Cache.Graph, newVal, "A");
        //    }



        //    if (locationRow != null)
        //    {
        //        row.ConstructRepDesc = locationRow.Descr;
        //    }
        //    else
        //    {
        //        row.ConstructRepDesc = null;
        //    }
        //}

        //protected void _(Events.FieldUpdating<STSalesTerritoryMapping, STSalesTerritoryMapping.electricRep> eventHandler)
        //{
        //    STSalesTerritoryMapping row = eventHandler.Row;
        //    Location locationRow = null;


        //    string newVal = null;


        //    if (eventHandler.NewValue == null)
        //    {
        //        row.ElectricRepDesc = null;
        //        return;
        //    }

        //    newVal = eventHandler.NewValue.ToString();

        //    using (new PXConnectionScope())
        //    {
        //        locationRow = SelectFrom<Location>
        //            .Where<LocationExt.usrRepNbr.IsEqual<@P.AsString>
        //            .And<Location.status.IsEqual<@P.AsString>>>
        //            .View.Select(eventHandler.Cache.Graph, newVal, "A");
        //    }



        //    if (locationRow != null)
        //    {
        //        row.ElectricRepDesc = locationRow.Descr;
        //    }
        //    else
        //    {
        //        row.ElectricRepDesc = null;
        //    }
        //}

        //protected void _(Events.FieldUpdating<STSalesTerritoryMapping, STSalesTerritoryMapping.marineRep> eventHandler)
        //{
        //    STSalesTerritoryMapping row = eventHandler.Row;
        //    Location locationRow = null;


        //    string newVal = null;


        //    if (eventHandler.NewValue == null)
        //    {
        //        row.MarineRepDesc = null;
        //        return;
        //    }

        //    newVal = eventHandler.NewValue.ToString();

        //    using (new PXConnectionScope())
        //    {
        //        locationRow = SelectFrom<Location>
        //            .Where<LocationExt.usrRepNbr.IsEqual<@P.AsString>
        //            .And<Location.status.IsEqual<@P.AsString>>>
        //            .View.Select(eventHandler.Cache.Graph, newVal, "A");
        //    }



        //    if (locationRow != null)
        //    {
        //        row.MarineRepDesc = locationRow.Descr;
        //    }
        //    else
        //    {
        //        row.MarineRepDesc = null;
        //    }
        //}

        #endregion
        #region Data Types
        #endregion

        public PXSave<STSalesTerritoryMapping> Save;
        public PXCancel<STSalesTerritoryMapping> Cancel;

        [PXViewName("SalesTerritoryMappings")]
        public SelectFrom<STSalesTerritoryMapping>.View SalesTerritoryMappings;

        [PXViewName("SalesDepartmentReps")]
        public SelectFrom<STSalesDepartmentReps>.View SalesDepartmentReps;
    }
}
