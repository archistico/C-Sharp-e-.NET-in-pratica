using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using global::App.Web;
using global::App.Web.Services;
using global::App.Web.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<global::App.Web.Root>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new System.Net.Http.HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiBaseAddress"] ?? "http://localhost:5001")
});

builder.Services.AddScoped<CatalogoApiClient>();
builder.Services.AddScoped<ClientiApiClient>();
builder.Services.AddScoped<DocumentiApiClient>();
builder.Services.AddScoped<DashboardApiClient>();
builder.Services.AddScoped<ReportApiClient>();
builder.Services.AddScoped<AppNotificationState>();

await builder.Build().RunAsync();
