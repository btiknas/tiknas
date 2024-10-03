using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.Security.Claims;
using Tiknas.Security.Encryption;
using Tiknas.SecurityLog;

namespace Tiknas.Security;

public class TiknasSecurityModule : TiknasModule
{
    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        AutoAddClaimsPrincipalContributors(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var applicationName = context.Services.GetApplicationName();
        if (!applicationName.IsNullOrEmpty())
        {
            Configure<TiknasSecurityLogOptions>(options =>
            {
                options.ApplicationName = applicationName!;
            });
        }

        var configuration = context.Services.GetConfiguration();
        context.Services.Configure<TiknasStringEncryptionOptions>(options =>
        {
            var keySize = configuration["StringEncryption:KeySize"];
            if (!keySize.IsNullOrWhiteSpace())
            {
                if (int.TryParse(keySize, out var intValue))
                {
                    options.Keysize = intValue;
                }
            }

            var defaultPassPhrase = configuration["StringEncryption:DefaultPassPhrase"];
            if (!defaultPassPhrase.IsNullOrWhiteSpace())
            {
                options.DefaultPassPhrase = defaultPassPhrase!;
            }

            var initVectorBytes = configuration["StringEncryption:InitVectorBytes"];
            if (!initVectorBytes.IsNullOrWhiteSpace())
            {
                options.InitVectorBytes = Encoding.ASCII.GetBytes(initVectorBytes!);
            }

            var defaultSalt = configuration["StringEncryption:DefaultSalt"];
            if (!defaultSalt.IsNullOrWhiteSpace())
            {
                options.DefaultSalt = Encoding.ASCII.GetBytes(defaultSalt!);
            }
        });
    }


    private static void AutoAddClaimsPrincipalContributors(IServiceCollection services)
    {
        var contributorTypes = new List<Type>();
        var dynamicContributorTypes = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(ITiknasClaimsPrincipalContributor).IsAssignableFrom(context.ImplementationType))
            {
                contributorTypes.Add(context.ImplementationType);
            }

            if (typeof(ITiknasDynamicClaimsPrincipalContributor).IsAssignableFrom(context.ImplementationType))
            {
                dynamicContributorTypes.Add(context.ImplementationType);
            }
        });

        services.Configure<TiknasClaimsPrincipalFactoryOptions>(options =>
        {
            options.Contributors.AddIfNotContains(contributorTypes);
            options.DynamicContributors.AddIfNotContains(dynamicContributorTypes);
        });
    }
}
