using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR.BQL;
using PX.Objects.AR.Repositories;
using PX.Objects.CA;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.CS;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.Common.Tools;
using PX.Objects.Common.MigrationMode;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects;
using PX.Objects.AR;

namespace PX.STI.STICustom.GraphExt
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class ARDocumentEnqExt : PXGraphExtension<ARDocumentEnq>
    {
        [PXFilterable]
        public PXSelectOrderBy<ARDocumentEnq.ARDocumentResult, OrderBy<Asc<ARDocumentEnq.ARDocumentResult.docDate>>> Documents;

        public delegate IEnumerable documentsDelegate();
        [PXOverride]
        public IEnumerable documents(documentsDelegate baseDelegate)
        {
            foreach (ARDocumentEnq.ARDocumentResult row in baseDelegate())
            {
                var foundRow = (ARDocumentEnq.ARDocumentResult)Base.Documents.Cache.Locate(row);
                yield return row;
            }
        }
    }
}

