using System;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.STI.STICustom.Common;

namespace PX.STI.STICustom.DAC.Projection
{
    [Serializable]
    [PXProjection(typeof(Select<STPurchaserContact>), Persistent = false)]
    [PXCacheName(CustomView.PurchaserContact, PXDacType.Catalogue)]
    public class STPurchaserContact : IBqlTable
    {
        #region BAccountID

        public abstract class bAccountID : BqlInt.Field<bAccountID> { }
        protected int? _BAccountID;

        [PXDBInt(IsKey = true)]
        [PXUIField(DisplayName = "BAccount", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(BAccount.bAccountID),
            SubstituteKey = typeof(BAccount.acctCD),
            DescriptionField = typeof(BAccount.acctName),
            DirtyRead = true)]
        public virtual int? BAccountID
        {
            get => _BAccountID;
            set => _BAccountID = value;
        }

        #endregion
        #region Status

        public abstract class status : BqlString.Field<status> { }
        protected string _Status;

        [PXDBString(1, IsFixed = true)]
        [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.Visible)]
        public virtual string Status
        {
            get => _Status;
            set => _Status = value;
        }

        #endregion
        #region Attention

        public abstract class attention : BqlString.Field<attention> { }
        protected string _Attention;

        [PXDBString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Buyer Attention", Visibility = PXUIVisibility.Visible)]
        public virtual string Attention
        {
            get => _Attention;
            set => _Attention = value;
        }

        #endregion
        #region EMail

        public abstract class eMail : BqlString.Field<eMail> { }
        protected string _EMail;

        [PXDBString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Buyer Email", Visibility = PXUIVisibility.Visible)]
        public virtual string EMail
        {
            get => _EMail;
            set => _EMail = value;
        }

        #endregion
    }
}

