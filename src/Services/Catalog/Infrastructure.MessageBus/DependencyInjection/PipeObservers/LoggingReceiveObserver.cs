using System;
using System.Threading.Tasks;
using MassTransit;
using Serilog;

namespace Infrastructure.MessageBus.DependencyInjection.PipeObservers;

public class LoggingReceiveObserver : IReceiveObserver
{
    private const string ExchangeKey = "RabbitMQ-ExchangeName";

    public async Task PreReceive(ReceiveContext context)
    {
        await Task.Yield();
        Log.Information("Receiving message from exchange {Exchange}, Redelivered: {Redelivered}, CorrelationId: {CorrelationId}", context.TransportHeaders.Get<string>(ExchangeKey), context.Redelivered, context.GetCorrelationId());
    }

    public async Task PostReceive(ReceiveContext context)
    {
        await Task.Yield();
        Log.Information("Message was received from exchange {Exchange}, Redelivered: {Redelivered}, CorrelationId: {CorrelationId}", context.TransportHeaders.Get<string>(ExchangeKey), context.Redelivered, context.GetCorrelationId());
    }

    public async Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType)
        where T : class
    {
        await Task.Yield();
        var messageType = context.Message.GetType();
        Log.Information("{Message} message from {Namespace} was consumed by {ConsumerType}, Duration: {Duration}s, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace ?? string.Empty, consumerType, duration.TotalSeconds, context.CorrelationId ?? new());
    }

    public async Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception)
        where T : class
    {
        await Task.Yield();
        var messageType = context.Message.GetType();
        Log.Error("Fault on consuming message {Message} from {Namespace} by {ConsumerType}, Duration: {Duration}s, Error: {Error}, CorrelationId: {CorrelationId}", messageType.Name, messageType.Namespace ?? string.Empty, consumerType, duration.TotalSeconds, exception.Message, context.CorrelationId ?? new());
    }

    public async Task ReceiveFault(ReceiveContext context, Exception exception)
    {
        await Task.Yield();
        Log.Error("Fault on receiving message from exchange {Exchange}, Redelivered: {Redelivered}, Error: {Error}, CorrelationId: {CorrelationId}", context.TransportHeaders.Get<string>(ExchangeKey) ?? string.Empty, context.Redelivered, exception.Message, context.GetCorrelationId() ?? new());
    }
}