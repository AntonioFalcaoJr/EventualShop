using System;
using System.Threading.Tasks;
using MassTransit;
using Serilog;

namespace Infrastructure.DependencyInjection.Observers;

public class LoggingConsumeObserver : IConsumeObserver
{
    public async Task PreConsume<T>(ConsumeContext<T> context)
        where T : class
    {
        var messageType = context.Message.GetType();
        Log.Information("Consuming {Message} message from {Namespace}, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace, context.CorrelationId);
        await Task.CompletedTask;
    }

    public async Task PostConsume<T>(ConsumeContext<T> context)
        where T : class
    {
        var messageType = context.Message.GetType();
        Log.Information("{Message} message from {Namespace} was consumed, ConversationId: {ConversationId}", messageType.Name, messageType.Namespace, context.ConversationId);
        await Task.CompletedTask;
    }

    public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception)
        where T : class
        => Task.CompletedTask;
}