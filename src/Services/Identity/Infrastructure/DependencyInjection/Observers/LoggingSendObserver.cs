using System;
using System.Threading.Tasks;
using MassTransit;
using Serilog;

namespace Infrastructure.DependencyInjection.Observers;

public class LoggingSendObserver : ISendObserver
{
    public async Task PreSend<T>(SendContext<T> context)
        where T : class
    {
        var messageType = context.Message.GetType();
        Log.Information("Sending {Message} message from {Namespace}, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace, context.CorrelationId);
        await Task.CompletedTask;
    }

    public async Task PostSend<T>(SendContext<T> context)
        where T : class
    {
        var messageType = context.Message.GetType();
        Log.Information("{MessageType} message from {Namespace} was sent, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace, context.CorrelationId);
        await Task.CompletedTask;
    }

    public async Task SendFault<T>(SendContext<T> context, Exception exception)
        where T : class
    {
        var messageType = context.Message.GetType();
        Log.Warning("Fault on sending message {Message} from {Namespace}, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace, context.CorrelationId);
        await Task.CompletedTask;
    }
}