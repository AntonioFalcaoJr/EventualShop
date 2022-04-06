namespace WebAPI.DataTransferObjects.Catalogs;

public static class Requests
{
    public record CreateCatalog(string Title, string Description);
}