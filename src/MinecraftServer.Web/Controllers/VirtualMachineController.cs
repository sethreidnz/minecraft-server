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
    public Task<IActionResult> Start()
    {
      throw new NotImplementedException();
    }

    [Route("stop")]
    [HttpPost]
    public Task<IActionResult> Stop()
    {
      throw new NotImplementedException();
    }

    [Route("details")]
    [HttpGet]
    public Task<IActionResult> Details()
    {
      throw new NotImplementedException();
    }
  }
}
