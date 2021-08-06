using Application.Abstractions.EventSourcing.Projections.Pagination;

namespace Infrastructure.Abstractions.EventSourcing.Projections.Pagination
{
    public record Paging : IPaging
    {
        private const int UpperLimit = 50;
        private const int DefaultLimit = 10;
        private readonly int _limit;

        public int Limit
        {
            get => _limit > UpperLimit ? UpperLimit : _limit <= 0 ? DefaultLimit : _limit;
            init => _limit = value;
        }

        public int Offset { get; init; }
    }
}