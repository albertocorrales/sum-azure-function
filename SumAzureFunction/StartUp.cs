using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SumAzureFunction;
using SumAzureFunction.Functions;
using SumAzureFunction.OpenApi;

[assembly: WebJobsStartup(typeof(StartUp))]
namespace SumAzureFunction
{
    /// <summary>
    /// This represents the entity to be invoked during the runtime startup.
    /// </summary>
    public class StartUp : IWebJobsStartup
    {
        /// <summary>
        /// Configures <see cref="IWebJobsBuilder"/> and prepares dependencies.
        /// </summary>
        /// <param name="builder"><see cref="IWebJobsBuilder"/> instance.</param>
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddSingleton<AppSettings>();

            builder.Services.AddTransient<IDocumentHelper, DocumentHelper>();
            builder.Services.AddTransient<IDocument, Document>();
            builder.Services.AddTransient<ISwaggerUI, SwaggerUI>();

            builder.Services.AddTransient<IRenderOpeApiDocumentFunction, RenderOpenApiDocumentFunction>();
            builder.Services.AddTransient<IRenderSwaggerUIFunction, RenderSwaggerUIFunction>();

            builder.Services.AddTransient<ISumFunction, SumFunction>();
        }
    }
}
