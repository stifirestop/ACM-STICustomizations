using System;
using PX.Data;
using PX.Api;

namespace PX.STI.STICustom.DACExt
{
    public sealed class SYMappingExt : PXCacheExtension<SYMapping>
    {
        public static bool IsActive() { return true; }

        #region Name

        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXCustomizeSelectorColumns(
            typeof(SYMapping.name),
            typeof(SYMapping.screenID),
            typeof(SYMapping.lastModifiedDateTime)
        )]
        public string Name { get; set; }

        #endregion
        #region ProviderID

        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXCustomizeSelectorColumns(
            typeof(SYProvider.name),
            typeof(SYProvider.providerType),
            typeof(SYProvider.lastModifiedDateTime)
        )]
        public Guid? ProviderID { get; set; }

        #endregion
    }
}
