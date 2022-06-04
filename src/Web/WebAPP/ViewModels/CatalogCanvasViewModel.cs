using Contracts.Services.Catalog;
using Microsoft.AspNetCore.Components;
using WebAPP.HttpClients;

namespace WebAPP.ViewModels;

public class CatalogCanvasViewModel
{
    private readonly ICatalogHttpClient _httpClient;

    public string Title { get; set; }
    public string Description { get; set; }

    public CatalogCanvasViewModel(ICatalogHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(EventCallback<Request.CreateCatalog> callback, CancellationToken ct)
    {
        var request = new Request.CreateCatalog(Guid.NewGuid(), Title, Description);
        var response = await _httpClient.CreateAsync(request, ct);
        if (response.Success) await callback.InvokeAsync(request);
    }
}