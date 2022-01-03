using ConfigReader.AppLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigReader.Controllers
{
    public class ConfigController : Controller
    {  
        // Simple appSettings.json Readings, no extra steps needed
        public IActionResult SimpleAppSettingsJson([FromServices]IConfiguration config)
        {
            // Reading appSettings.json

            // The first simple way is to use the GetSection method from the IConfiguration interface reading parent/child tags:
            var settingValue1 = config.GetSection("MyAppSettings").GetSection("Setting3").GetSection("SubSetting1").Value;

            // Use the GetValue method and be explicit about what type want to convert it to
            var settingValue2 = config.GetSection("MyAppSettings").GetSection("Setting3").GetValue<bool>("SubSetting1");

            // Write all properties inline and in order, separated by a colon
            var settingValue3 = config.GetValue<bool>("MyAppSettings:Setting3:SubSetting1");

            // Binding
            MyAppSettings settings1 = new MyAppSettings();
            config.GetSection("MyAppSettings").Bind(settings1);

            return Ok("appSettings.json Read Successfully");
        }

        // Simple data.json Readings, no extra steps needed
        public IActionResult SimpleDataJson([FromServices] IWebHostEnvironment env)
        {
            // Parsing data.json into configuration
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath) // Sets the base path of the files to be added to the builder
                .AddJsonFile("data.json", optional: false, reloadOnChange: false);

            IConfiguration config = builder.Build();

            // Reading data.json

            // The first simple way is to use the GetSection method from the IConfiguration interface reading parent/child tags:
            var settingValue1 = config.GetSection("MyCustomSettings").GetSection("Setting3").GetSection("SubSetting1").Value;

            // Use the GetValue method and be explicit about what type want to convert it to
            var settingValue2 = config.GetSection("MyCustomSettings").GetSection("Setting3").GetValue<bool>("SubSetting1");

            // Write all properties inline and in order, separated by a colon
            var settingValue3 = config.GetValue<bool>("MyCustomSettings:Setting3:SubSetting1");

            // Binding
            MyCustomSettings settings1 = new MyCustomSettings();
            config.GetSection("MyCustomSettings").Bind(settings1);

            return Ok("data.json Read Successfully");
        }

        // 
        public IActionResult IOptionsAppSettingsJson([FromServices] IOptions<MyAppSettings> configuration)
        {
            // requires:
            // builder.Services.Configure<ConfigReader.AppLib.MyAppSettings>(builder.Configuration.GetSection("MyAppSettings"));
            // in Program.js
            bool setting1 = configuration.Value.Setting3.SubSetting1;


            return Ok("appSettings.json Read Successfully");
        }

        public IActionResult IOptionsDataJson([FromServices] IOptions<MyCustomSettings> configuration)
        {
            // requires:
            // IConfigurationBuilder customBuilder = new ConfigurationBuilder()
            //    .SetBasePath(builder.Environment.ContentRootPath) // Sets the base path of the files to be added to the builder
            //    .AddJsonFile("data.json", optional: false, reloadOnChange: false);
            // IConfiguration dataConfig = customBuilder.Build();
            // builder.Services.Configure<ConfigReader.AppLib.MyCustomSettings>(dataConfig.GetSection("MyCustomSettings"));
            // in Program.js
            bool setting1 = configuration.Value.Setting3.SubSetting1;


            return Ok("data.json Read Successfully");
        }



        public IActionResult PreBindingAppSettingsJson([FromServices] MyAppSettings appSettings)
        {
            // requires:
            // builder.Services.AddConfiguration<ConfigReader.AppLib.MyAppSettings>(builder.Configuration, "MyAppSettings");
            // in Program.js
            bool setting1 = appSettings.Setting3.SubSetting1;


            return Ok("appSettings.json Read Successfully");
        }

        public IActionResult PreBindingDataJson([FromServices] MyCustomSettings customSettings)
        {
            // requires:
            // builder.Services.AddConfiguration<ConfigReader.AppLib.MyCustomSettings>(dataConfig, "MyCustomSettings");
            // in Program.js
            bool setting1 = customSettings.Setting3.SubSetting1;


            return Ok("data.json Read Successfully");
        }

    }
}



