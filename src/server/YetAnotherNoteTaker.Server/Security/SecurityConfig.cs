using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace YetAnotherNoteTaker.Server.Security
{
    public class SecurityConfig
    {
        public static IEnumerable<Scope> GetScopes(IConfiguration configuration)
        {
            return new List<Scope>
            {
                new Scope
                {
                    Name = configuration.GetSection("AuthServer:ScopeName").Value,
                    Description = configuration.GetSection("AuthServer:ScopeDescription").Value,
                    UserClaims = new List<string>
                    {
                        "role"
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = configuration.GetSection("AuthServer:ClientId").Value,
                    ClientSecrets =
                    {
                        new Secret(configuration.GetSection("AuthServer:ClientSecret").Value.Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { configuration.GetSection("AuthServer:ScopeName").Value },
                    AccessTokenLifetime = (3600 * 24 * 360),
                    IdentityTokenLifetime = (3600 * 24 * 360)
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),

                new IdentityResource("custom", new[] { "status" })
            };
        }

        public static IEnumerable<ApiResource> GetApis(IConfiguration configuration)
        {
            return new List<ApiResource>
            {
                new ApiResource(
                    configuration.GetSection("AuthServer:ScopeName").Value,
                    configuration.GetSection("AuthServer:ScopeDescription").Value)
            };
        }

        public static string GetIssuerUri(IConfiguration configuration)
        {
            return configuration.GetValue<string>("AuthClient:Authority");
        }
    }
}
