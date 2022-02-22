using System;
using System.Threading.Tasks;
using MassTransit;
using Serilog;

namespace ECommerce.WebAPI.DependencyInjection.PipeObservers;

public class LoggingConsumeObserver : IConsumeObserver
{
    public async Task PreConsume<T>(ConsumeContext<T> context)
        where T : class
    {
        await Task.Yield();
        var messageType = context.Message.GetType();
        Log.Information("Consuming {Message} message from {Namespace}, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace, context.CorrelationId);
    }

    public async Task PostConsume<T>(ConsumeContext<T> context)
        where T : class
    {
        await Task.Yield();
        var messageType = context.Message.GetType();
        Log.Information("{Message} message from {Namespace} was consumed, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace, context.CorrelationId);
    }

    public async Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception)
        where T : class
    {
        await Task.Yield();
        var messageType = context.Message.GetType();
        Log.Warning("Fault on consuming message {Message} from {Namespace}, Error {Error}, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace, exception.Message, context.CorrelationId);
    }
}