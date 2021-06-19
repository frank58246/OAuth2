using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using OAuth.Common;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OAuth.Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Thread.Sleep(10);
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(ProjectSetting.Url.OAuthServer);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"{ProjectSetting.Url.ResourceApi}/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            Console.ReadLine();
        }
    }
}