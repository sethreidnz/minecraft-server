using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MinecraftServer.Web.Exceptions;
using MinecraftServer.Web.Models.Azure;
using MinecraftServer.Web.Options;
using Newtonsoft.Json;

namespace MinecraftServer.Web.Clients
{
  public class AzureRestClient : IAzureRestClient
  {
    private readonly Uri _bearerTokenUri;
    private readonly Uri _dealocateVmEndpoint;
    private readonly Uri _startVmEndpoint;
    private readonly Uri _vmStateEndpoint;
    private readonly AzureOptions _azureOptions;
    private readonly ServicePrincipleOptions _servicePrincipleOptions;
    private readonly VirtualMachineOptions _virtualMachineOptions;

    public AzureRestClient(
      IOptions<AzureOptions> azureOptions,
      IOptions<ServicePrincipleOptions> servicePrincipleOptions,
      IOptions<VirtualMachineOptions> virtualMachineOptions)
    {
      _azureOptions = azureOptions?.Value;
      _servicePrincipleOptions = servicePrincipleOptions?.Value;
      _virtualMachineOptions = virtualMachineOptions?.Value;
      _bearerTokenUri = new Uri($"https://login.microsoftonline.com/{_azureOptions.TenantId}/oauth2/token");
      _dealocateVmEndpoint = new Uri(_azureOptions.ResourceUri, $"/subscriptions/{_azureOptions.SubscriptionId}/resourceGroups/{_virtualMachineOptions.ResourceGroupName}/providers/Microsoft.Compute/virtualMachines/{_virtualMachineOptions.Name}/deallocate?api-version=2019-03-01");
      _startVmEndpoint = new Uri(_azureOptions.ResourceUri, $"/subscriptions/{_azureOptions.SubscriptionId}/resourceGroups/{_virtualMachineOptions.ResourceGroupName}/providers/Microsoft.Compute/virtualMachines/{_virtualMachineOptions.Name}/start?api-version=2019-03-01");
      _vmStateEndpoint = new Uri(_azureOptions.ResourceUri, $"/subscriptions/{_azureOptions.SubscriptionId}/resourceGroups/{_virtualMachineOptions.ResourceGroupName}/providers/Microsoft.Compute/virtualMachines/{_virtualMachineOptions.Name}/instanceView?api-version=2019-03-01");
    }

    public async Task<BearerTokenResponse> GetBearerToken()
    {
      var requestBody = new Dictionary<string, string>();
      requestBody.Add("grant_type", "client_credentials");
      requestBody.Add("client_id", _servicePrincipleOptions.AppId);
      requestBody.Add("client_secret", _servicePrincipleOptions.ClientSecret);
      requestBody.Add("resource", _azureOptions.ResourceUri.ToString());
      using (var httpClient = new HttpClient())
      using (var req = new HttpRequestMessage(HttpMethod.Post, _bearerTokenUri) { Content = new FormUrlEncodedContent(requestBody) })
      {
        var bearerTokenResponse = await httpClient.SendAsync(req);
        if (!bearerTokenResponse.IsSuccessStatusCode)
        {
          throw new AzureAuthenticationException("Failed to get Azure AD bearer token");
        }

        var bearetTokenResponseAsString = await bearerTokenResponse.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<BearerTokenResponse>(bearetTokenResponseAsString);
      }
    }

    public async Task<bool> DealocateVm()
    {
      var bearerToken = await GetBearerToken();
      var isDeallocated = await VmHasStatus("PowerState/deallocated");
      if (isDeallocated)
      {
        return true;
      }

      using (var httpClient = new HttpClient())
      {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken.AccessToken);
        await httpClient.PostAsync(_dealocateVmEndpoint, null);

        isDeallocated = await VmHasStatus("PowerState/deallocated");
        while (!isDeallocated)
        {
          Thread.Sleep(5000);
          isDeallocated = await VmHasStatus("PowerState/deallocated");
        }

        return true;
      }
    }

    public async Task<VirtualMachineStateResponse> GetVmState()
    {
      var bearerToken = await GetBearerToken();

      using (var httpClient = new HttpClient())
      {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken.AccessToken);
        var response = await httpClient.GetAsync(_vmStateEndpoint);
        var responseAsString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<VirtualMachineStateResponse>(responseAsString);
      }
    }

    public async Task<bool> StartVm()
    {
      var bearerToken = await GetBearerToken();
      var isRunning = await VmHasStatus("PowerState/running");
      if (isRunning)
      {
        return true;
      }

      using (var httpClient = new HttpClient())
      {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken.AccessToken);
        await httpClient.PostAsync(_startVmEndpoint, null);

        isRunning = await VmHasStatus("PowerState/running");
        while (!isRunning)
        {
          Thread.Sleep(5000);
          isRunning = await VmHasStatus("PowerState/running");
        }

        return true;
      }
    }

    public async Task<bool> VmHasStatus(string status)
    {
      var vmStatus = await GetVmState();
      return vmStatus.Statuses.Any(s => s.Code == status);
    }
  }
}
