﻿using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Extensions.Logging;

namespace SumAzureFunction.OpenApi
{
    /// <summary>
    /// This provides interfaces to the <see cref="RenderOpeApiDocumentFunction"/> class.
    /// </summary>
    public interface IRenderOpeApiDocumentFunction : IFunction<ILogger>
    {
    }
}