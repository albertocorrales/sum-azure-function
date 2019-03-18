using System;
using System.Net;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SumAzureFunction.Functions
{
    public class SumFunction : FunctionBase<ILogger>, ISumFunction
    {
        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            var req = input as HttpRequest;
            int param1 = Convert.ToInt32(req.Query["param1"]);
            int param2 = Convert.ToInt32(req.Query["param2"]);

            var content = new ContentResult()
            {
                Content = $"{param1}+{param2}={param1 + param2}",
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };

            return (TOutput)(IActionResult)content;
        }
    }
}
