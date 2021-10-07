using System;
using System.Threading.Tasks;
using MassTransit;
using Serilog;

namespace Infrastructure.DependencyInjection.Observers
{
    public class LoggingConsumeObserver : IConsumeObserver
    {
        public async Task PreConsume<T>(ConsumeContext<T> context)
            where T : class
        {
            Log.Information("Starting consumption of {MessageType} message", context.Message.GetType().Name);
            await Task.CompletedTask;
        }

        public async Task PostConsume<T>(ConsumeContext<T> context)
            where T : class
        {
            Log.Information("The message {MessageType} was successfully consumed", context.Message.GetType().Name);
            await Task.CompletedTask;
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception)
            where T : class
            => Task.CompletedTask;
    }
}