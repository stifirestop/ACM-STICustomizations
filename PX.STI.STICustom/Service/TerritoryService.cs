using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.STI.STICustom.Common;
using PX.STI.STICustom.DAC;
using PX.STI.STICustom.DACExt;

namespace PX.STI.STICustom.Service
{
    public static class TerritoryService
    {
        public static SegmentValue FetchSalesTerritory(PXGraph graph, String salesTerritoryCD)
        {
            SegmentValue result = null;

            using (new PXConnectionScope())
            {
                result = SelectFrom<SegmentValue>
                    .Where<SegmentValue.dimensionID.IsEqual<TerritorySegmentType.Dimension>
                    .And<SegmentValue.segmentID.IsEqual<TerritorySegmentType.Segment>
                    .And<SegmentValue.value.IsEqual<@P.AsString>>>>
                    .View.Select(graph, salesTerritoryCD);
            }

            return result;
        }

        public static SegmentValue FetchTerritoryByCustomerClass(PXGraph graph, string customerClassCD)
        {
            SegmentValue result = null;

            PXSelectBase<SegmentValue> query = new SelectFrom<SegmentValue>
                .Where<SegmentValue.dimensionID.IsEqual<TerritorySegmentType.Dimension>
                .And<SegmentValue.segmentID.IsEqual<TerritorySegmentType.Segment>>>
                .View(graph);

            switch (customerClassCD)
            {
                case CustomerClassType.Construction:
                    query.WhereAnd<Where<SegmentValue.value.IsEqual<CustomerClassType.construction>>>();
                    break;
                case CustomerClassType.Electrical:
                    query.WhereAnd<Where<SegmentValue.value.IsEqual<CustomerClassType.electrical>>>();
                    break;
                case CustomerClassType.International:
                    query.WhereAnd<Where<SegmentValue.value.IsEqual<CustomerClassType.international>>>();
                    break;
                case CustomerClassType.OEM:
                    query.WhereAnd<Where<SegmentValue.value.IsEqual<CustomerClassType.oem>>>();
                    break;
                case CustomerClassType.eBMP:
                    query.WhereAnd<Where<SegmentValue.value.IsEqual<CustomerClassType.ebmp>>>();
                    break;
                case CustomerClassType.Marine:
                    query.WhereAnd<Where<SegmentValue.value.IsEqual<CustomerClassType.marine>>>();
                    break;
                case CustomerClassType.Samples:
                    query.WhereAnd<Where<SegmentValue.value.IsEqual<CustomerClassType.samples>>>();
                    break;
                case CustomerClassType.Fabricators:
                    query.WhereAnd<Where<SegmentValue.value.IsEqual<CustomerClassType.fabricators>>>();
                    break;
                case CustomerClassType.PointOfSale:
                    query.WhereAnd<Where<SegmentValue.value.IsEqual<CustomerClassType.pointOfSale>>>();
                    break;
                case CustomerClassType.HouseAccount:
                    query.WhereAnd<Where<SegmentValue.value.IsEqual<CustomerClassType.houseAccount>>>();
                    break;
                default:
                    query.WhereAnd<Where<SegmentValue.value.IsEqual<CustomerClassType.construction>>>();
                    break;
            }

            using (new PXConnectionScope())
            {
                result = query.Select();
            }

            return result;
        }
        //public static SegmentValue FetchTerritoryByPostalCode(PXGraph graph, string postalCode)
        //{
        //    SegmentValue result = null;
        //    postalCode = postalCode?.Substring(0, 5) ?? null;

        //    using (new PXConnectionScope())
        //    {
        //        STSalesTerritoryMapping mapping = SelectFrom<STSalesTerritoryMapping>
        //            .Where<STSalesTerritoryMapping.postalCode.IsEqual<@P.AsString>>
        //            .View.Select(graph, postalCode);

        //        if (mapping != null && mapping.SalesTerritoryCD != null)
        //            result = FetchSalesTerritory(graph, mapping.SalesTerritoryCD);
        //    }

        //    return result;
        //}

        public static STSalesTerritoryMapping FetchTerritoryByPostalCode(PXGraph graph, string postalCode)
        {
            STSalesTerritoryMapping result = null;
            postalCode = postalCode?.Substring(0, 5) ?? null;

            using (new PXConnectionScope())
            {
                result = SelectFrom<STSalesTerritoryMapping>
                    .Where<STSalesTerritoryMapping.postalCode.IsEqual<@P.AsString>>
                    .View.Select(graph, postalCode);
            }

            return result;
        }
        public static string FetchDefaultSalesPersonCD(PXGraph graph, string territory, string postalcode, string classid, string subchannel)
        {
            string result = null;
            postalcode = postalcode?.Substring(0, 5) ?? null;

            if (territory is null || classid is null)
                return result;

            using (new PXConnectionScope())
            {
                STSalesTerritoryMapping stData = SelectFrom<STSalesTerritoryMapping>
                    .Where<STSalesTerritoryMapping.postalCode.IsEqual<@P.AsString>>
                    .View.Select(graph, postalcode);

                STSalesDepartmentReps tRepData = SelectFrom<STSalesDepartmentReps>
                    .Where<STSalesDepartmentReps.salesDepartmentCD.IsEqual<@P.AsString>
                    >
                    .View.Select(graph, territory);


                //if (classid == CustomerClassType.Marine)
                //{
                //    result = stData.MarineRep;
                //}
                //else
                if (classid == CustomerClassType.Electrical)
                {
                    if (stData != null)
                    {
                        switch (subchannel)
                        {
                            case "08ELEC":
                                result = stData.ElectricRep;
                                break;
                            case "09COMM":
                                result = stData.ComRep;
                                break;
                            case "DUAL":
                                result = stData.ElectricRep ?? (stData.ComRep ?? null);
                                break;
                            default:
                                result = stData.ElectricRep ?? (stData.ComRep ?? null);
                                break;
                        }
                    }
                }
                else
                {
                    if (tRepData != null)
                    {
                        result = tRepData.DeptRepID ?? null;
                    }
                }

            }


            return result;
        }




    }
}

