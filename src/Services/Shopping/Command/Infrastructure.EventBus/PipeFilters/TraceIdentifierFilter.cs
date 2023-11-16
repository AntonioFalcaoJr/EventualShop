using MassTransit;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EventBus.PipeFilters;

public class TraceIdentifierFilter<T>(IHttpContextAccessor httpContextAccessor) : IFilter<PublishContext<T>>
    where T : class
{
    public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
    {
        if (Guid.TryParse(httpContextAccessor.HttpContext?.TraceIdentifier, out var correlationId))
            context.CorrelationId = correlationId;
        
        return next.Send(context);
    }

    public void Probe(ProbeContext context)
        => context.CreateFilterScope("Trace Identifier");
}