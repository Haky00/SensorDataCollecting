using Blazored.LocalStorage;
using Blazored.LocalStorage.Serialization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor;
using MudBlazor.Services;
using SensorDataCollecting.Client;
using System.Xml.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("SensorDataCollecting.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.ShowCloseIcon = false;
});
builder.Services.AddBlazoredLocalStorage();
builder.Services.Replace(ServiceDescriptor.Scoped<IJsonSerializer, NewtonSoftJsonSerializer>());

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("SensorDataCollecting.ServerAPI"));

Environment.SetEnvironmentVariable("SUPABASE_URL", "https://rhtekizmxsrxzkkmhdvc.supabase.co");
Environment.SetEnvironmentVariable("SUPABASE_KEY", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InJodGVraXpteHNyeHpra21oZHZjIiwicm9sZSI6ImFub24iLCJpYXQiOjE2OTcyMzQ1NjksImV4cCI6MjAxMjgxMDU2OX0.hl_WAQL84C8LpoqbECkC2o472d3pyjbmjW8n0Pmx03I");

await builder.Build().RunAsync();
