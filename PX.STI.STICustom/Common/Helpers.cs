using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;
using PX.Objects.IN;
using PX.STI.STICustom.DAC;

namespace PX.STI.STICustom.Common
{
    public static class Helpers
    {
        public const string _BeginDate = "201806";
        public const string _EndDate = "201906";

        public static string CalculateStartPeriod(PXGraph graph, string startPeriod)
        {
            if (graph is null) return _BeginDate;

            Int32 currentYear;
            Int32 currentMonth;

            string currentDate = graph.Accessinfo.BusinessDate.ToString();
            Int32.TryParse(currentDate.Split('/')[2].Substring(0, 4), out currentYear);
            Int32.TryParse(currentDate.Split('/')[0], out currentMonth);

            string previousYearPeriod = (currentYear - 1).ToString() + (currentMonth - 1).ToString().PadLeft(2, '0');
            previousYearPeriod = previousYearPeriod.CompareTo(_BeginDate) < 0 ? _BeginDate : previousYearPeriod;

            return (startPeriod?.CompareTo(_BeginDate) < 0)
                ? _BeginDate
                : startPeriod ?? previousYearPeriod;
        }

        public static string CalculateEndPeriod(PXGraph graph, string endPeriod)
        {
            if (graph is null) return _EndDate;

            Int32 currentYear;
            Int32 currentMonth;

            string currentDate = graph.Accessinfo.BusinessDate.ToString();
            Int32.TryParse(currentDate.Split('/')[2].Substring(0, 4), out currentYear);
            Int32.TryParse(currentDate.Split('/')[0], out currentMonth);

            string currentPeriod = currentYear.ToString() + (currentMonth - 1).ToString().PadLeft(2, '0');
            return endPeriod ?? currentPeriod;
        }

        public static string CalculatePreviousPeriod(string currentPeriod)
        {
            string result = currentPeriod;
            Int32 currentYear;
            Int32 currentMonth;

            Int32.TryParse(currentPeriod.Substring(0, 4), out currentYear);
            Int32.TryParse(currentPeriod.Substring(4, 2), out currentMonth);

            if (currentMonth == 1)
                result = (currentYear - 1).ToString() + "12";
            else
                result = currentYear.ToString() + (currentMonth - 1).ToString().PadLeft(2, '0');

            return result;
        }

        public static Decimal? CalculateInventoryCost(STInventoryHistory row)
        {
            Decimal? result = 0.00M;
            if (row is null) return result;

            try
            {
                result = row.StandardCost * (row.EndQty - row.ScrapQty) ?? result;
            }
            catch (Exception e) { }

            return result;
        }

        public static String CalculateCustomerClassCD(String accountType, String customerClassID)
        {
            String result = null;

            result = (accountType != null && accountType != CustomerAccountType.Standard)
                ? (accountType == CustomerAccountType.PointOfSale)
                    ? CustomerClassType.PointOfSale
                    : (accountType == CustomerAccountType.House)
                        ? CustomerClassType.HouseAccount
                        : customerClassID
                : customerClassID;

            return result;
        }


        public static String CalculateDefaultTerritoryCD()
        {
            String result = null;
            return result;
        }


        public static String CalculateDefaultSalesPersonCD(String accountType, String customerClassID, STLocationAddress locationAddress)
        {
            String result = null;

            if (accountType is null || customerClassID is null || locationAddress is null)
                return result;

            result = (accountType != null && accountType != CustomerAccountType.Standard)
                ? (accountType == CustomerAccountType.PointOfSale)
                    ? customerClassID + "-5900"
                    : (accountType == CustomerAccountType.House)
                        ? customerClassID + "-5900"
                        : null
                : (customerClassID == "01")
                    ? "01-" + locationAddress.LegacyTerritoryCD + "0"
                    : (customerClassID == "03")
                        ? "03-" + locationAddress.LegacyTerritoryCD + "0"
                        : (customerClassID == "04")
                            ? "04-6000"
                            : ((customerClassID == "06") && (locationAddress.CountryID == "US" || locationAddress.CountryID == "CA") && (!CustomMessages.StateExceptions.Contains(locationAddress.State)))
                                ? "06-5060"
                                : ((customerClassID == "06") && (locationAddress.CountryID == "US" || locationAddress.CountryID == "CA") && (CustomMessages.StateExceptions.Contains(locationAddress.State)))
                                    ? "06-562E"
                                    : (customerClassID == "07")
                                        ? "07-" + locationAddress.LegacyTerritoryCD + "0"
                                        : (customerClassID == "11")
                                        ? "11-5900"
                                        : null;

            return result;
        }
    }

    public class STLocationAddress
    {
        public int? LocationID;
        public int? AddressID;
        public String PostalCode;
        public String State;
        public String CountryID;
        public String SalesTerritoryCD;
        public String LegacyTerritoryCD;
    }
}

