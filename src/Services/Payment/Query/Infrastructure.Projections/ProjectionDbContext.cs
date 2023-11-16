using Infrastructure.Projections.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Projections;

public class ProjectionDbContext(IConfiguration configuration) : MongoDbContext(configuration);