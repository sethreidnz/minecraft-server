using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MinecraftServer.Web
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var host = WebHost.CreateDefaultBuilder(args)
          .ConfigureServices(services => services.AddAutofac())
          .UseStartup<Startup>()
          .Build();

      host.Run();
    }
  }
}
