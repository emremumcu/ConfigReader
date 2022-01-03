﻿using Microsoft.Extensions.Options;

namespace ConfigReader.AppLib
{
    public static class ConfigurationExtension
    {
        public static void AddConfiguration<T>(this IServiceCollection services, IConfiguration configuration, string configurationTag = null) where T : class
        {
            if (string.IsNullOrEmpty(configurationTag))
            {
                configurationTag = typeof(T).Name;
            }

            var instance = Activator.CreateInstance<T>();
            new ConfigureFromConfigurationOptions<T>(configuration.GetSection(configurationTag)).Configure(instance);
            services.AddSingleton(instance);
        }
    }
}