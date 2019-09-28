using System.Threading.Tasks;
using MinecraftServer.Web.Models.Azure;

namespace MinecraftServer.Web.Clients
{
  public interface IAzureRestClient
  {
    Task<bool> DealocateVm();

    Task<bool> StartVm();

    Task<VirtualMachineStateResponse> GetVmState();
  }
}
