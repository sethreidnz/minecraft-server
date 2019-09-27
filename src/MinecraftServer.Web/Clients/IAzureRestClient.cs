namespace MinecraftServer.Web.Clients
{
  public interface IAzureRestClient
  {
    bool StartVm();

    bool DealocateVm();

    bool GetVmState();
  }
}
