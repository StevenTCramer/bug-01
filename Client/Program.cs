using BlazorState;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Pwa.Client;
using System.Reflection;

namespace Pwa.Client;

public class Program
{
  public static void ConfigureServices(IServiceCollection aServiceCollection, IConfiguration aConfiguration)
  {
    aServiceCollection.AddBlazorState
    (
      (aOptions) =>
      {
    #if ReduxDevToolsEnabled
            aOptions.UseReduxDevTools( options => options.Trace = true);
    #endif
        aOptions.Assemblies =
          new Assembly[]
          {
                  typeof(Program).GetTypeInfo().Assembly,
          };
      }
    );
  }

  public static async Task Main(string[] args)
  {
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");


    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    ConfigureServices(builder.Services, builder.Configuration);
    

    await builder.Build().RunAsync();
  }
}
