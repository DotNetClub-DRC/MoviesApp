﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "movieClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("sudidav".Sha256())

                    },
                    AllowedScopes = {"movieAPI"}
                },
                new Client
                {
                    ClientId =  "movie_mvc_client",
                    ClientName = "Movie Mvc Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowRememberConsent = true,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:5001/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:5001/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("sudidav".Sha256())

                    },
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("movieAPI", "Movie API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "80dd4646-9471-4631-aefa-9d4bf1c38ef5",
                    Username =  "sudi",
                    Password =  "sudidav",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "sudi"),
                        new Claim(JwtClaimTypes.FamilyName, "sudi")
                    }
                }
            };
    }
}