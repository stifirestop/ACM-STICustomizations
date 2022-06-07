using PX.Data;
using PX.Data.Maintenance.GI;

namespace PX.STI.STICustom.DACExt
{
    public sealed class GIDesignExt : PXCacheExtension<GIDesign>
    {
        public static bool IsActive() { return true; }

        #region Name

        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXCustomizeSelectorColumns(
            typeof(GIDesign.name),
            typeof(GIDesign.exposeViaOData),
            typeof(GIDesign.sitemapSelectorTitle),
            typeof(GIDesign.sitemapScreenID),
            typeof(GIDesign.lastModifiedDateTime)
        )]
        public string Name { get; set; }

        #endregion
    }
}
