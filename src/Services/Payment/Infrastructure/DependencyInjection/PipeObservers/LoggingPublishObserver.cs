using System;
using System.Threading.Tasks;
using MassTransit;
using Serilog;

namespace Infrastructure.DependencyInjection.PipeObservers;

public class LoggingPublishObserver : IPublishObserver
{
    public async Task PrePublish<T>(PublishContext<T> context)
        where T : class
    {
        var messageType = context.Message.GetType();
        Log.Information("Publishing {Message} event from {Namespace}, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace, context.CorrelationId);
        await Task.CompletedTask;
    }

    public async Task PostPublish<T>(PublishContext<T> context)
        where T : class
    {
        var messageType = context.Message.GetType();
        Log.Information("{MessageType} event, from {Namespace} was published, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace, context.CorrelationId);
        await Task.CompletedTask;
    }

    public async Task PublishFault<T>(PublishContext<T> context, Exception exception)
        where T : class
    {
        var messageType = context.Message.GetType();
        Log.Warning("Fault on publishing message {Message} from {Namespace}, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace, context.CorrelationId);
        await Task.CompletedTask;
    }
}