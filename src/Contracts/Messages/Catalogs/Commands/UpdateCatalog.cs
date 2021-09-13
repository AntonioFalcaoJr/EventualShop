using System;

namespace Messages.Catalogs.Commands
{
    public interface UpdateCatalog
    {
        Guid Id { get; }
        string Title { get; }
    }
}