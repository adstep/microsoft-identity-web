﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Microsoft.Identity.Web
{
    /// <summary>
    /// RequireScopeOrAppPermissionOptions.
    /// </summary>
    internal class RequireScopeOrAppPermissionOptions : IPostConfigureOptions<AuthorizationOptions>
    {
        private readonly AuthorizationPolicy _defaultPolicy;

        /// <summary>
        /// Sets the default policy.
        /// </summary>
        public RequireScopeOrAppPermissionOptions()
        {
            _defaultPolicy = new AuthorizationPolicyBuilder()
                .AddRequirements(new ScopeOrAppPermissionAuthorizationRequirement())
                .Build();
        }

        /// <inheritdoc/>
        public void PostConfigure(
            string name,
            AuthorizationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.DefaultPolicy = options.DefaultPolicy is null
                                     ? _defaultPolicy
                                     : AuthorizationPolicy.Combine(options.DefaultPolicy, _defaultPolicy);
        }
    }
}
