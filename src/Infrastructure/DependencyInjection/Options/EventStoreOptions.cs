using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DependencyInjection.Options
{
    public class EventStoreOptions
    {
        [Required, Range(3, 30)] 
        public int SnapshotInterval { get; init; }
    }
}