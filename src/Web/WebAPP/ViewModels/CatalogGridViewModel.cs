using Contracts.Abstractions.Paging;

namespace WebAPP.ViewModels;

public class CatalogGridViewModel
{
    public List<CatalogCardViewModel> Cards { get; set; }
    public Page Page { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}