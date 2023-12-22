using System.Net;
using Polly.Timeout;

namespace WebAPP.DependencyInjection.Extensions;

// TODO: It is necessary given the incompetence of the Refit team. Remove when the issue is fixed. 
public class HttpRequestExceptionHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            return await base.SendAsync(request, cancellationToken);
        }
        catch (HttpRequestException exception)
        {
            return new(HttpStatusCode.ServiceUnavailable)
            {
                ReasonPhrase = exception.Message,
                RequestMessage = request
            };
        }
        catch (TimeoutRejectedException exception)
        {
            return new(HttpStatusCode.RequestTimeout)
            {
                ReasonPhrase = exception.Message,
                RequestMessage = request
            };
        }
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            return base.Send(request, cancellationToken);
        }
        catch (HttpRequestException exception)
        {
            return new(HttpStatusCode.ServiceUnavailable)
            {
                ReasonPhrase = exception.Message,
                RequestMessage = request
            };
        }
        catch (TimeoutRejectedException exception)
        {
            return new(HttpStatusCode.RequestTimeout)
            {
                ReasonPhrase = exception.Message,
                RequestMessage = request
            };
        }
    }
}