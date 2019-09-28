using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinecraftServer.Web.Models.Azure
{
  public class VirtualMachineStateResponse
  {
    public string ComputerName { get; set; }

    public string OsName { get; set; }

    public string OsVersion { get; set; }

    public Vmagent VmAgent { get; set; }

    public Disk[] Disks { get; set; }

    public string HyperVGeneration { get; set; }

    public Status[] Statuses { get; set; }
  }

  public class Vmagent
  {
    public string VmAgentVersion { get; set; }

    public Status[] Statuses { get; set; }

    public object[] ExtensionHandlers { get; set; }
  }

  public class Status
  {
    public string Code { get; set; }

    public string Level { get; set; }

    public string DisplayStatus { get; set; }

    public string Message { get; set; }

    public DateTime Time { get; set; }
  }

  public class Disk
  {
    public string Name { get; set; }

    public Status[] Statuses { get; set; }
  }
}
