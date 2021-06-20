using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using OAuth.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace OAuth.Server
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var list = new List<IdentityResource>
            {
                 new IdentityResources.OpenId(),
                 new IdentityResources.Profile()
            };
            return list;
        }

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
                 },
                  new Client
                  {
                      ClientId = "mvc",
                      ClientSecrets = { new Secret("secret".Sha256()) },

                      AllowedGrantTypes = GrantTypes.Code,

                      // where to redirect to after login
                      RedirectUris = { $"{ProjectSetting.Url.MvcClient}/signin-oidc" },

                      // where to redirect to after logout
                      PostLogoutRedirectUris = { $"{ProjectSetting.Url.MvcClient}/signout-callback-oidc" },

                      AllowedScopes = new List<string>
                      {
                          IdentityServerConstants.StandardScopes.OpenId,
                          IdentityServerConstants.StandardScopes.Profile
                      }
                  }
            };

            return list;
        }

        public static List<TestUser> GetTestUsers()
        {
            var address = new
            {
                street_address = "One Hacker Way",
                locality = "Heidelberg",
                postal_code = 69118,
                country = "Germany"
            };

            var list = new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "alice",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                new TestUser
                {
                    SubjectId = "11",
                    Username = "bob",
                    Password = "bob",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                    }
                }
            };
            return list;
        }
    }
}