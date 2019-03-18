using Aliencube.AzureFunctions.Extensions.DependencyInjection.Extensions;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Triggers.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SumAzureFunction.Functions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SumAzureFunction
{
    public class SumFunctionHttpTrigger : TriggerBase<ILogger>
    {

        private readonly ISumFunction _function;

        public SumFunctionHttpTrigger(ISumFunction function)
        {
            this._function = function ?? throw new ArgumentNullException(nameof(function));
        }

        [FunctionName("Sum")]
        [OpenApiOperation("Calculator", "sum", Description = "Sum two parameters.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter("param1", In = Microsoft.OpenApi.Models.ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "parameter1", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter("param2", In = Microsoft.OpenApi.Models.ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "parameter2", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseBody(HttpStatusCode.OK, "application/json", typeof(string), Summary = "Sum result")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var result = await this._function
                                   .AddLogger(log)
                                   .InvokeAsync<HttpRequest, IActionResult>(req)
                                   .ConfigureAwait(false);

            return result;
        }
    }
}
