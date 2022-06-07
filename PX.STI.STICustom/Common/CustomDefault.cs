using PX.Common;
using PX.Data.BQL;
using System;

namespace PX.STI.STICustom.Common
{
    [Serializable]
    [PXLocalizable]
    public static class CustomDefault
    {
        public static readonly string[] UPSIntegratedWarehouses = { "001" };

        public const string SiteCD = "001";

        public class siteCD : BqlString.Constant<siteCD>
        {
            public siteCD() : base(SiteCD) { }
        }
    }
}
