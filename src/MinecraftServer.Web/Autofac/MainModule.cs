using Autofac;
using Microsoft.Extensions.Options;
using MinecraftServer.Web.Clients;
using MinecraftServer.Web.Options;

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
