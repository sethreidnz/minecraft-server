using Autofac;
using MinecraftServer.Web.Clients;

namespace MinecraftServer.Web.Autofac
{
  public class MainModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<AzureRestClient>().As<IAzureRestClient>();
    }
  }
}
