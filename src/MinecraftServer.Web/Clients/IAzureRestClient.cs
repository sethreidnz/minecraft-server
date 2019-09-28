using System.Threading.Tasks;
using MinecraftServer.Web.Models.Azure;

namespace MinecraftServer.Web.Clients
{
  public interface IAzureRestClient
  {
    Task<VirtualMachineStateResponse> DealocateVm();

    Task<VirtualMachineStateResponse> StartVm();

    Task<VirtualMachineStateResponse> GetVmState();
  }
}
