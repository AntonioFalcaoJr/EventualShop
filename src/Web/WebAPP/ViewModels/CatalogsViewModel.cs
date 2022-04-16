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

    public async Task FetchDataAsync(int limit = 8, int offset = 0)
    {
        var response = await _httpClient.GetAsync(limit, offset, CancellationToken.None);

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
            var response = await _httpClient.CreateAsync(request, CancellationToken.None);

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
            var response = await _httpClient.DeleteAsync(catalogId, CancellationToken.None);

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

    public async Task ChangeTitleAsync(Guid catalogId)
    {
        try
        {
            Requests.ChangeCatalogTitle request = new(Title);
            var response = await _httpClient.ChangeTitleAsync(catalogId, request, CancellationToken.None);

            if (response.Success)
            {
                var catalog = Catalogs.First(catalog => catalog.Id == catalogId);
                var index = Catalogs.IndexOf(catalog);
                Catalogs[index] = catalog with {Id = catalogId, Title = Title};
                Success();
            }
            else Failed(response.Message);
        }
        catch (Exception e)
        {
            Failed(e.Message);
        }
    }

    public async Task ChangeDescriptionAsync(Guid catalogId)
    {
        try
        {
            Requests.ChangeCatalogDescription request = new(Description);
            var response = await _httpClient.ChangeDescriptionAsync(catalogId, request, CancellationToken.None);

            if (response.Success)
            {
                var catalog = Catalogs.First(catalog => catalog.Id == catalogId);
                var index = Catalogs.IndexOf(catalog);
                Catalogs[index] = catalog with {Id = catalogId, Description = Description};
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