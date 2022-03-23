using MassTransit;
using Serilog;

namespace WebAPI.DependencyInjection.PipeObservers;

public class LoggingSendObserver : ISendObserver
{
    public async Task PreSend<T>(SendContext<T> context)
        where T : class
    {
        await Task.Yield();
        var messageType = context.Message.GetType();
        Log.Information("Sending {Message} message from {Namespace}, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace, context.CorrelationId);
    }

    public async Task PostSend<T>(SendContext<T> context)
        where T : class
    {
        await Task.Yield();
        var messageType = context.Message.GetType();
        Log.Information("{MessageType} message from {Namespace} was sent, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace, context.CorrelationId);
    }

    public async Task SendFault<T>(SendContext<T> context, Exception exception)
        where T : class
    {
        await Task.Yield();
        var messageType = context.Message.GetType();
        Log.Error("Fault on sending message {Message} from {Namespace}, Error: {Error}, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace ?? string.Empty, exception.Message, context.CorrelationId ?? new());
    }
}