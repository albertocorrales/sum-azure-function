using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SumAzureFunction.OpenApi
{
    /// <summary>
    /// This represents the function entity to render Open API document.
    /// </summary>
    public class RenderOpenApiDocumentFunction : FunctionBase<ILogger>, IRenderOpeApiDocumentFunction
    {
        private readonly AppSettings _settings;
        private readonly IDocument _document;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderOpenApiDocumentFunction"/> class.
        /// </summary>
        /// <param name="settings"><see cref="AppSettings"/> instance.</param>
        /// <param name="document"><see cref="IDocument"/> instance.</param>
        public RenderOpenApiDocumentFunction(AppSettings settings, IDocument document)
        {
            this._settings = settings;
            this._document = document;
        }

        /// <inheritdoc />
        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            var request = input as HttpRequest;
            var renderOpenApiDocumentFunctionOptions = options as RenderOpeApiDocumentFunctionOptions;

            var contentType = renderOpenApiDocumentFunctionOptions.Format.GetContentType();
            var result = await this._document
                                   .InitialiseDocument()
                                   .AddMetadata(this._settings.OpenApiInfo)
                                   .AddServer(request, this._settings.HttpSettings.RoutePrefix)
                                   .Build(Assembly.GetExecutingAssembly())
                                   .RenderAsync(renderOpenApiDocumentFunctionOptions.Version, renderOpenApiDocumentFunctionOptions.Format)
                                   .ConfigureAwait(false);

            return (TOutput)(IActionResult)new ContentResult() { Content = result, ContentType = contentType, StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
