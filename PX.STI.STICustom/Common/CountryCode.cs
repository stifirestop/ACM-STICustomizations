using PX.Common;
using PX.Data.BQL;
using System;

namespace PX.STI.STICustom.Common
{
    [Serializable]
    [PXLocalizable]
    public static class CountryCode
    {
        public class UnitedStates : BqlString.Constant<UnitedStates>
        {
            public UnitedStates() : base("US") { }
        }

        public class Canada : BqlString.Constant<Canada>
        {
            public Canada() : base("CA") { }
        }
    }
}
