using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using CargaImagensNASA.Data;
using CargaImagensNASA.HttpClients;

[assembly: FunctionsStartup(typeof(CargaImagensNASA.Startup))]
namespace CargaImagensNASA;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddRefitClient<IImagemDiariaAPI>()
            .ConfigureHttpClient(
                c => c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("EndpointNASA")));
        builder.Services.AddScoped<IImagemNASARepository, ImagemNASARepository>();
    }
}