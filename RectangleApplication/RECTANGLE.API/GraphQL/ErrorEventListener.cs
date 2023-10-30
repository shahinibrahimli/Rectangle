using System;
using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Execution.Processing;
using HotChocolate.Resolvers;
using Microsoft.Extensions.Logging;

namespace Rectangle.API.GraphQL
{
    public class ErrorEventListener : ExecutionDiagnosticEventListener
    {
        private readonly ILogger<ErrorEventListener> logger;

        public ErrorEventListener(ILogger<ErrorEventListener> logger)
        {
            this.logger = logger;
        }
         
        public override void RequestError(IRequestContext context, Exception exception)
        {
            logger.LogError(exception, "RequestError");
            base.RequestError(context, exception);
        }

        public override void SyntaxError(IRequestContext context, IError error)
        {
            logger.LogError(error.Exception, "SyntaxError");
            logger.LogError(error.Message, "SyntaxError");
            base.SyntaxError(context, error);
        }

        public override void ValidationErrors(IRequestContext context, IReadOnlyList<IError> errors)
        {
            foreach (var error in errors)
            {
                logger.LogError(error.Exception, "ValidationErrors");
                logger.LogError(error.Message, "ValidationErrors");
            }

            base.ValidationErrors(context, errors);
        } 
    }
}
