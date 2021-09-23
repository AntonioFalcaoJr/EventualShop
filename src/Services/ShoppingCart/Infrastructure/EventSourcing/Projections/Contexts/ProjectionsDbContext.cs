using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;
using Infrastructure.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.Projections.Contexts
{
    public class ProjectionsDbContext : MongoDbContext
    {
        public ProjectionsDbContext(IOptionsMonitor<MongoDbOptions> optionsSnapshot)
            : base(optionsSnapshot) { }
    }
}