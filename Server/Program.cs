using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace Pwa;

public class Program
{
  public static void Main(string[] args)
  {
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    ConfigureServices(builder.Services, builder.Configuration);

    WebApplication webApplication = builder.Build();

    ConfigureMiddleware(webApplication);
    ConfigureEndpoints(webApplication);

    webApplication.Run();
  }

  private static void ConfigureEndpoints(WebApplication webApplication)
  {
    webApplication.MapRazorPages();
    webApplication.MapControllers();
    webApplication.MapBlazorHub();
    webApplication.MapFallbackToPage("/_Host");
  }

  private static void ConfigureMiddleware(WebApplication webApplication)
  {
    // Configure the HTTP request pipeline.
    if (webApplication.Environment.IsDevelopment())
    {
      webApplication.UseWebAssemblyDebugging();
    }
    else
    {
      webApplication.UseExceptionHandler("/Error");
      // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      webApplication.UseHsts();
    }

    webApplication.UseHttpsRedirection();

    webApplication.UseBlazorFrameworkFiles();
    webApplication.UseStaticFiles();

    webApplication.UseRouting();
  }

  private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
  {
    services.AddControllersWithViews();
    services.AddRazorPages();
    services.AddServerSideBlazor();
    Pwa.Client.Program.ConfigureServices(services, configuration);
  }
}
