using Infrastructure.Projections.Abstractions.Contexts;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Projections.Contexts;

public class ProjectionsDbContext : MongoDbContext
{
    public ProjectionsDbContext(IConfiguration configuration)
        : base(configuration) { }
}