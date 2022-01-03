using ConfigReader.AppLib;

var builder = WebApplication.CreateBuilder(args);

var mvcBuilder = builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();

builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

// IOptions Way ------------------------------------------
// IOptions: appSettings.json
builder.Services.Configure<ConfigReader.AppLib.MyAppSettings>(builder.Configuration.GetSection("MyAppSettings"));

// IOptions: data.json
IConfigurationBuilder customBuilder = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath) // Sets the base path of the files to be added to the builder
                .AddJsonFile("data.json", optional: false, reloadOnChange: false);
IConfiguration dataConfig = customBuilder.Build();
builder.Services.Configure<ConfigReader.AppLib.MyCustomSettings>(dataConfig.GetSection("MyCustomSettings"));
// IOptions Way ------------------------------------------


// PRE Binding Way ------------------------------------------
// PRE Binding appSettings.json
builder.Services.AddConfiguration<ConfigReader.AppLib.MyAppSettings>(builder.Configuration, "MyAppSettings");

// PRE Binding data.json
builder.Services.AddConfiguration<ConfigReader.AppLib.MyCustomSettings>(dataConfig, "MyCustomSettings");
// PRE Binding Way ------------------------------------------


// Install-Package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation  
mvcBuilder.AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();



/*
  
// Builder configuration

var builder = new ConfigurationBuilder()
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    .AddJsonFile($"customSettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables(); 
 
// Change the content root, app name, and environment 

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Staging,
    WebRootPath = "customwwwroot"
});


// Add services

builder.Services.AddScoped<ITodoRepository, TodoRepository>();

// Customize IHostBuilder or IWebHostBuilder

builder.Host.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(30));

// Customize IWebHostBuilder

builder.WebHost.UseHttpSys();


// Change the web root

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    // Look for static files in webroot
    WebRootPath = "webroot"
});

// Custom dependency injection (DI) container

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Register services directly with Autofac here. Don't call builder.Populate(), that happens in AutofacServiceProviderFactory.
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new MyApplicationModule()));

// Access additional services

builder.Services.AddSingleton<IService, Service>();

IService service = app.Services.GetRequiredService<IService>();

// Logging
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
        .WriteTo.Console(
#if DEBUG
            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose,
#else
            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
#endif
            outputTemplate: builder.Configuration["Serilog:OutputTemplateConsole"]
        )
        .WriteTo.File(
            path: builder.Configuration["Serilog:LogFileLocation"], retainedFileCountLimit: 7, rollingInterval: RollingInterval.Day, buffered: false,
            outputTemplate: builder.Configuration["Serilog:OutputTemplateFile"]
        );
});


 
 */