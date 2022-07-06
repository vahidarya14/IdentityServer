// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        static string scopeName = "api1";
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(scopeName, "My API"),
                new ApiScope( "api2", "My API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client{
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { scopeName }
                }
            };


        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {

            };


 

        public static List<ApiResource> ApiResources =>
         new List<ApiResource>
         {
                new ApiResource
                {
                    Enabled = true,
                    Name = "api_resource_1",
                    DisplayName = "Your user identifier",
                    Description = null,
                    ShowInDiscoveryDocument = true,
                },
                new ApiResource
                {
                    Enabled = true,
                    Name = "api_resource_2",
                    DisplayName = "User profile",
                    Description = "Your user profile information (first name, last name, etc.)",
                    ShowInDiscoveryDocument = true,
                }
         };

    }
}