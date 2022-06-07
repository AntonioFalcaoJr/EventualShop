using Microsoft.AspNetCore.Components;
using WebAPP.Abstractions.ViewModels;
using WebAPP.HttpClients;
using WebAPP.Models;

namespace WebAPP.ViewModels;

public class CatalogCardViewModel : ViewModel
{
    private readonly ICatalogHttpClient _httpClient;

    public CatalogCardViewModel() { }

    public CatalogCardViewModel(ICatalogHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private CatalogModel _catalog = new();

    public CatalogModel Catalog
    {
        get => _catalog;
        private set => SetField(ref _catalog, value);
    }

    public async Task ChangeTitleAsync(CancellationToken ct)
        => await _httpClient.ChangeTitleAsync(Catalog.Id, Catalog.Title, ct);

    public async Task ChangeDescriptionAsync(CancellationToken ct)
        => await _httpClient.ChangeDescriptionAsync(Catalog.Id, Catalog.Description, ct);

    public async Task DeleteAsync(EventCallback<Guid> callback, CancellationToken ct)
    {
        var response = await _httpClient.DeleteAsync(Catalog.Id, ct);
        if (response.Success) await callback.InvokeAsync(Catalog.Id);
    }
}