using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;
using WebAPP.Abstractions.Http;
using WebAPP.HttpClients;

namespace WebAPP.ViewModels;

public class CatalogGridViewModel
{
    private readonly ICatalogHttpClient _httpClient;

    private CatalogGridViewModel() { }

    public CatalogGridViewModel(ICatalogHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<CatalogCardViewModel> Cards { get; private set; }
    public Page Page { get; private set; }

    public async Task FetchDataAsync(CancellationToken ct, int limit = 8, int offset = 0)
    {
        var response = await _httpClient.GetAsync(limit, offset, ct);
        if (response.Success) Load(response.ActionResult);
    }

    public void Add(CatalogCardViewModel card)
        => Cards.Add(card);

    public void Delete(Guid id)
        => Cards.RemoveAll(card => card.Id == id);

    public Task MoveToPageAsync(int page, CancellationToken ct)
        => FetchDataAsync(ct, offset: page - 1);

    public async Task MoveToNextAsync(CancellationToken ct)
    {
        if (Page.HasNext)
            await FetchDataAsync(ct, offset: Page.Current);
    }

    public async Task MoveToPreviewAsync(CancellationToken ct)
    {
        if (Page.HasPrevious)
            await FetchDataAsync(ct, offset: Page.Current - 2);
    }

    private void Load(CatalogGridViewModel model)
    {
        Cards = model.Cards;
        Page = model.Page;
    }

    public static implicit operator CatalogGridViewModel(PagedResult<Projection.Catalog> pagedResult)
        => new()
        {
            Cards = pagedResult.Items.Select(catalog => (CatalogCardViewModel) catalog).ToList(),
            Page = pagedResult.Page
        };
}