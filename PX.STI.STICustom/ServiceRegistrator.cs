using Autofac;
using PX.STI.STICustom.Service;
using PX.STI.STICustom.Service.Interface;

namespace PX.STI.STICustom
{
    public class ServiceRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PurchaseContactService>().As<IPurchaseContactService>();
        }
    }
}
