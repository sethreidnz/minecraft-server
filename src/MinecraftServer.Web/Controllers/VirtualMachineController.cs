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
      await _azureRestClient.StartVm();
      return Ok();
    }

    [Route("stop")]
    [HttpPost]
    public async Task<IActionResult> Stop()
    {
      await _azureRestClient.DealocateVm();
      return Ok();
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
