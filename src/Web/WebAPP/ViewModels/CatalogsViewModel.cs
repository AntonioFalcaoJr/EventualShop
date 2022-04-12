using BlazorStrap;
using ECommerce.Abstractions.Messages.Queries.Paging;
using ECommerce.Contracts.Catalogs;
using WebAPP.HttpClients;

namespace WebAPP.ViewModels;

public class CatalogsViewModel
{
    private readonly IBlazorStrap _blazorStrap;
    private readonly IECommerceHttpClient _httpClient;

    public List<Projections.Catalog> Catalogs = new();
    public string Description;
    public PageInfo PageInfo = new();

    public string Title;

    public CatalogsViewModel(IECommerceHttpClient httpClient, IBlazorStrap blazorStrap)
    {
        _httpClient = httpClient;
        _blazorStrap = blazorStrap;
    }

    public async Task FetchDataAsync(CancellationToken cancellationToken, int limit = 8, int offset = 0)
    {
        var response = await _httpClient.GetCatalogsAsync(limit, offset, CancellationToken.None);

        if (response.Success)
        {
            Catalogs = response.ActionResult?.Items?.ToList() ?? Catalogs;
            PageInfo = response.ActionResult?.PageInfo ?? PageInfo;
        }
        else Failed(response.Message);
    }

    public async Task CreateAsync()
    {
        try
        {
            Requests.CreateCatalog request = new(Guid.NewGuid(), Title, Description);
            var response = await _httpClient.CreateCatalogAsync(request, CancellationToken.None);

            if (response.Success)
            {
                Title = string.Empty;
                Description = string.Empty;

                Catalogs.Add(new(request.CatalogId, request.Title, request.Description, default, default));

                Success();
            }
            else Failed(response.Message);
        }
        catch (Exception e)
        {
            Failed(e.Message);
        }
    }

    public async Task DeleteAsync(Guid catalogId)
    {
        try
        {
            var response = await _httpClient.DeleteCatalogAsync(catalogId, CancellationToken.None);

            if (response.Success)
            {
                Catalogs.RemoveAll(catalog => catalog.Id == catalogId);
                Success();
            }
            else Failed(response.Message);
        }
        catch (Exception e)
        {
            Failed(e.Message);
        }
    }

    public async Task EditAsync(Guid catalogId)
    {
        try
        {
            Requests.CreateCatalog request = new(catalogId, Title, Description);
            var response = await _httpClient.EditCatalogAsync(request, CancellationToken.None);

            if (response.Success)
            {
                var catalog = Catalogs.First(catalog => catalog.Id == catalogId);
                var index = Catalogs.IndexOf(catalog);
                Catalogs[index] = new Projections.Catalog(catalogId, Title, Description, catalog.IsActive, catalog.IsDeleted);

                Success();
            }
            else Failed(response.Message);
        }
        catch (Exception e)
        {
            Failed(e.Message);
        }
    }

    private void Success()
    {
        _blazorStrap.Toaster.Add("Success", "Catalog created", o =>
        {
            o.Color = BSColor.Success;
            o.CloseAfter = 2000;
            o.Toast = Toast.BottomRight;
        });
    }

    private void Failed(string error = default)
    {
        _blazorStrap.Toaster.Add("Error", error ?? "It was not possible to create a new Catalog", o =>
        {
            o.Color = BSColor.Danger;
            o.CloseAfter = 2000;
            o.Toast = Toast.BottomRight;
        });
    }
}