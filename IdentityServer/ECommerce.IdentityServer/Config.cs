﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace ECommerce.IdentityServer
{
    public static class Config
    {

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog") {Scopes = {"catalog_fullpermission"}},
            new ApiResource("resource_photostock") {Scopes = {"photo_stock_fullpermission"}},
            new ApiResource("resource_cart") {Scopes = {"cart_fullpermission"}},
        };
        public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.Email(),
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource() {Name = "roles", DisplayName="Roles", Description="User roles", UserClaims = new[] {"role"}}
        };

        public static IEnumerable<ApiScope> ApiScopes =>  // Scope --> Claim
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission", "Full permission for Catalog Api"),
                new ApiScope("photo_stock_fullpermission", "Full permission for Photo Stock Api"),
                new ApiScope("cart_fullpermission", "Full permission for Cart Api"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClient",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"catalog_fullpermission","photo_stock_fullpermission", IdentityServerConstants.LocalApi.ScopeName}
                },
                    new Client {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClientForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {
                        "cart_fullpermission",
                        IdentityServerConstants.StandardScopes.Email, 
                        IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "roles"
                        },
                        AccessTokenLifetime = 3600, // 1 HOUR
                        RefreshTokenExpiration = TokenExpiration.Absolute,
                        AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds, // 60 DAYS
                        RefreshTokenUsage = TokenUsage.ReUse
                }
            };
    }
}