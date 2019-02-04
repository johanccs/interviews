using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Realdigital.Interview.Domain.Contracts;

namespace Realdigital.Interview.Domain.Helpers
{
    public class ConfigBuilder:IConfigBuilder
    {
        private ConfigurationBuilder configurationBuilder;

        public ConfigBuilder()
        {
              configurationBuilder = new ConfigurationBuilder();
        }

        public string GetWebApiAddress()
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "AppConfig.json");
                configurationBuilder.AddJsonFile(path, false);

                var root = configurationBuilder.Build();

                var configString = root.GetSection("WebApiConfig").GetSection("WebConfig").Value;

                return configString;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
