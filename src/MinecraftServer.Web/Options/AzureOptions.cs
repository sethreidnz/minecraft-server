using System;

namespace MinecraftServer.Web.Options
{
  public class AzureOptions
  {
    public Uri ResourceUri { get; set; }

    public string SubscriptionId { get; set; }

    public string TenantId { get; set; }
  }
}
