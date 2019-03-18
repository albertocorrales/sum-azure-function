using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Extensions.Logging;

namespace SumAzureFunction.Functions
{
    public interface ISumFunction : IFunction<ILogger>
    {
    }
}