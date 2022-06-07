using Contracts.Services.Catalog;
using Microsoft.AspNetCore.Components;
using WebAPP.Abstractions.ViewModels;
using WebAPP.HttpClients;
using WebAPP.Models;

namespace WebAPP.ViewModels;

public class CatalogCanvasViewModel  : ViewModel
{
    private readonly ICatalogHttpClient _httpClient;
    private CatalogModel _catalog = new();

    public CatalogModel Catalog
    {
        get => _catalog;
        private set => SetField(ref _catalog, value);
    }
    
    public CatalogCanvasViewModel(ICatalogHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(EventCallback<Request.CreateCatalog> callback, CancellationToken ct)
    {
        var response = await _httpClient.CreateAsync(Catalog, ct);
        if (response.Success) await callback.InvokeAsync(Catalog);
        Catalog = new();
    }
}