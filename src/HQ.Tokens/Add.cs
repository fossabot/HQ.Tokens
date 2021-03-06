#region LICENSE

// Unless explicitly acquired and licensed from Licensor under another
// license, the contents of this file are subject to the Reciprocal Public
// License ("RPL") Version 1.5, or subsequent versions as allowed by the RPL,
// and You may not copy or use this file in either source code or executable
// form, except in compliance with the terms and conditions of the RPL.
// 
// All software distributed under the RPL is provided strictly on an "AS
// IS" basis, WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, AND
// LICENSOR HEREBY DISCLAIMS ALL SUCH WARRANTIES, INCLUDING WITHOUT
// LIMITATION, ANY WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE, QUIET ENJOYMENT, OR NON-INFRINGEMENT. See the RPL for specific
// language governing rights and limitations under the RPL.

#endregion

using HQ.Tokens.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HQ.Tokens
{
    public static class Add
    {
        public static IServiceCollection AddTokens(this IServiceCollection services, IConfiguration config)
        {
            // for now
            var options = new SecurityOptions();
            config.Bind(options);

            // for later
            services.Configure<SecurityOptions>(config);

            if (options.Tokens.Enabled) services.AddAuthentication(options);

            services.AddAuthorization(x =>
            {
                var builder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                builder.RequireAuthenticatedUser();
                x.DefaultPolicy = builder.Build();
            });

            return services;
        }
    }
}
