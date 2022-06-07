using PX.Data.BQL;

namespace PX.STI.STICustom.Common
{
    public class CustLocationStatus
    {
        public const string Active = "A";
        public const string Inactive = "I";

        public class active : BqlString.Constant<active> { public active() : base(Active) { } }
        public class inactive : BqlString.Constant<inactive> { public inactive() : base(Inactive) { } }
    }
    public partial class TerritorySegmentType
    {
        public class Dimension : BqlString.Constant<Dimension> { public Dimension() : base("SUBACCOUNT") { } }
        public class Segment : BqlInt.Constant<Segment> { public Segment() : base(1) { } }
    }

    public class CustomerAccountType
    {
        public const string Standard = "STD";
        public const string House = "HSE";
        public const string PointOfSale = "POS";

        public class standard : BqlString.Constant<standard> { public standard() : base(Standard) { } }
        public class house : BqlString.Constant<house> { public house() : base(House) { } }
        public class pointOfSale : BqlString.Constant<pointOfSale> { public pointOfSale() : base(PointOfSale) { } }
    }

    public static class CustomerClassType
    {
        public const string Construction = "01";
        public const string Electrical = "02";
        public const string International = "03";
        public const string OEM = "04";
        public const string eBMP = "05";
        public const string Marine = "06";
        public const string Samples = "07";
        public const string Fabricators = "11";
        public const string PointOfSale = "POS";
        public const string HouseAccount = "HSE";

        public class construction : BqlString.Constant<construction> { public construction() : base(null) { } }
        public class electrical : BqlString.Constant<electrical> { public electrical() : base(null) { } }
        public class international : BqlString.Constant<international> { public international() : base(null) { } }
        public class oem : BqlString.Constant<oem> { public oem() : base("1802") { } }
        public class ebmp : BqlString.Constant<ebmp> { public ebmp() : base(null) { } }
        public class marine : BqlString.Constant<marine> { public marine() : base(null) { } }
        public class samples : BqlString.Constant<samples> { public samples() : base(null) { } }
        public class fabricators : BqlString.Constant<fabricators> { public fabricators() : base("1712") { } }
        public class pointOfSale : BqlString.Constant<pointOfSale> { public pointOfSale() : base("1711") { } }
        public class houseAccount : BqlString.Constant<houseAccount> { public houseAccount() : base("1712") { } }

        public static string[] CustomerClassesWithDefaults = { "04", "06", "11", "POS", "HSE" };
        public static string[] CustomerClassesByCountry = { "03" };
    }
}