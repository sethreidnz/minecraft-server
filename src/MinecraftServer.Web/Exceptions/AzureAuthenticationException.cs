using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinecraftServer.Web.Exceptions
{
  public class AzureAuthenticationException : Exception
  {
    public AzureAuthenticationException()
    {
    }

    public AzureAuthenticationException(string message)
        : base(message)
    {
    }

    public AzureAuthenticationException(string message, Exception inner)
        : base(message, inner)
    {
    }
  }
}
