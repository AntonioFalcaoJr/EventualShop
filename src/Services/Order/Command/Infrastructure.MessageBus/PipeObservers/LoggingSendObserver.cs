using MassTransit;
using Serilog;

namespace Infrastructure.MessageBus.PipeObservers;

public class LoggingSendObserver : ISendObserver
{
    public async Task PreSend<T>(SendContext<T> context)
        where T : class
    {
        await Task.Yield();

        Log.Information("Sending {Message} message from {Namespace}, CorrelationId: {CorrelationId}",
            typeof(T).Name, typeof(T).Namespace, context.CorrelationId);
    }

    public async Task PostSend<T>(SendContext<T> context)
        where T : class
    {
        await Task.Yield();

        Log.Debug("{MessageType} message from {Namespace} was sent, CorrelationId: {CorrelationId}",
            typeof(T).Name, typeof(T).Namespace, context.CorrelationId);
    }

    public async Task SendFault<T>(SendContext<T> context, Exception exception)
        where T : class
    {
        await Task.Yield();

        Log.Error("Fault on sending message {Message} from {Namespace}, Error: {Error}, CorrelationId: {CorrelationId}",
            typeof(T).Name, typeof(T).Namespace, exception.Message, context.CorrelationId);
    }
}