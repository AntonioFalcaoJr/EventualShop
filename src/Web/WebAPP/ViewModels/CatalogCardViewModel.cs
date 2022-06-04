using Contracts.Services.Catalog;
using Microsoft.AspNetCore.Components;
using WebAPP.HttpClients;

namespace WebAPP.ViewModels;

public class CatalogCardViewModel
{
    private readonly ICatalogHttpClient _httpClient;

    public CatalogCardViewModel() { }

    public CatalogCardViewModel(ICatalogHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }


    public async Task ChangeTitleAsync(CancellationToken ct)
        => await _httpClient.ChangeTitleAsync(Id, Title, ct);

    public async Task ChangeDescriptionAsync(CancellationToken ct)
        => await _httpClient.ChangeDescriptionAsync(Id, Description, ct);

    public async Task DeleteAsync(EventCallback<Guid> callback, CancellationToken ct)
    {
        var response = await _httpClient.DeleteAsync(Id, ct);
        if (response.Success) await callback.InvokeAsync(Id);
    }

    public static implicit operator CatalogCardViewModel(Projection.Catalog catalog)
        => new()
        {
            Description = catalog.Description,
            Id = catalog.Id,
            Title = catalog.Title,
            IsActive = catalog.IsActive,
            IsDeleted = catalog.IsDeleted
        };

    public static implicit operator CatalogCardViewModel(Request.CreateCatalog request)
        => new()
        {
            Description = request.Description,
            Id = request.CatalogId,
            Title = request.Title
        };
}