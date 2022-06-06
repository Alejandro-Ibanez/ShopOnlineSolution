using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShopOnline.WEB;
using ShopOnline.WEB.Services;
using ShopOnline.WEB.Services.Contract;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7227") });
builder.Services.AddScoped<IProductService, ProductService>();

await builder.Build().RunAsync();
