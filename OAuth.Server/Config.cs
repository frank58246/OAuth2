using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth.Server
{
    public class Config
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            var list = new List<ApiScope>
            {
                new ApiScope("api1", "My API")
            };

            return list;
        }

        public static IEnumerable<Client> GetClients()
        {
            var list = new List<Client>
            {
                 new Client
                 {
                     ClientId = "client",

                     AllowedGrantTypes = GrantTypes.ClientCredentials,

                     ClientSecrets =
                     {
                         new Secret("secret".Sha256())
                     },

                     AllowedScopes = { "api1" }
                 }
            };

            return list;
        }
    }
}