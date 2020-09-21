// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Dwapi.Auth
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("pdapiv1","DWH Palantir Dataset API"),
            };

        public static IEnumerable<Client> Clients(string secret) =>
            new Client[]
            {
                new Client
                {
                    ClientId = "palantir",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret(secret.Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                }
            };

    }
}
