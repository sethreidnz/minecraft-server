using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinecraftServer.Web.Clients;

namespace MinecraftServer.Web.Controllers
{
  [Route("api/[controller]")]
  public class VirtualMachineController : Controller
  {
    private readonly IAzureRestClient _azureRestClient;

    public VirtualMachineController(IAzureRestClient azureRestClient)
    {
      _azureRestClient = azureRestClient;
    }

    [Route("start")]
    [HttpPost]
    public async Task<IActionResult> Start()
    {
      var vmState = await _azureRestClient.StartVm();
      return Ok(vmState);
    }

    [Route("stop")]
    [HttpPost]
    public async Task<IActionResult> Stop()
    {
      var vmState = await _azureRestClient.DealocateVm();
      return Ok(vmState);
    }

    [Route("details")]
    [HttpGet]
    public async Task<IActionResult> Details()
    {
      var vmState = await _azureRestClient.GetVmState();
      return Ok(vmState);
    }
  }
}
