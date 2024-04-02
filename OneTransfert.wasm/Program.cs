using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MudBlazor;
using OneTransfert.wasm.HashCheckService;

namespace OneTransfert.wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddDensenExtensions();
            builder.Services.AddStorages();
            builder.Services.AddSingleton<HashServiceFactory>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build());
            builder.Services.AddHttpClient("OneTransfert.srv", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("OneTransfert.srv") ?? throw new ArgumentException());
            });
            builder.Services.AddMudServices();
            builder.Services.AddMudMarkdownServices();
            await builder.Build().RunAsync();
        }
    }
}
