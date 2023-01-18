using MassTransit;
using Serilog;

namespace WebAPI.PipeObservers;

public class LoggingReceiveObserver : IReceiveObserver
{
    private const string ExchangeKey = "RabbitMQ-ExchangeName";

    public async Task PreReceive(ReceiveContext context)
    {
        await Task.Yield();

        Log.Debug("Receiving message from exchange {Exchange}, Redelivered: {Redelivered}, CorrelationId: {CorrelationId}",
            context.TransportHeaders.Get<string>(ExchangeKey), context.Redelivered, context.GetCorrelationId());
    }

    public async Task PostReceive(ReceiveContext context)
    {
        await Task.Yield();

        Log.Debug("Message was received from exchange {Exchange}, Redelivered: {Redelivered}, CorrelationId: {CorrelationId}",
            context.TransportHeaders.Get<string>(ExchangeKey), context.Redelivered, context.GetCorrelationId());
    }

    public async Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType)
        where T : class
    {
        await Task.Yield();

        Log.Debug("{Message} message from {Namespace} was consumed by {ConsumerType}, Duration: {Duration}s, CorrelationId: {CorrelationId}",
            typeof(T).Name, typeof(T).Namespace, consumerType, duration.TotalSeconds, context.CorrelationId);
    }

    public async Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception)
        where T : class
    {
        await Task.Yield();

        Log.Error("Fault on consuming message {Message} from {Namespace} by {ConsumerType}, Duration: {Duration}s, Error: {Error}, CorrelationId: {CorrelationId}",
            typeof(T).Name, typeof(T).Namespace, consumerType, duration.TotalSeconds, exception.Message, context.CorrelationId);
    }

    public async Task ReceiveFault(ReceiveContext context, Exception exception)
    {
        await Task.Yield();

        Log.Error("Fault on receiving message from exchange {Exchange}, Redelivered: {Redelivered}, Error: {Error}, CorrelationId: {CorrelationId}",
            context.TransportHeaders.Get<string>(ExchangeKey), context.Redelivered, exception.Message, context.GetCorrelationId() ?? new());
    }
}