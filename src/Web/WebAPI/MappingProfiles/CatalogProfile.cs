using AutoMapper;
using ECommerce.Contracts.Catalogs;
using WebAPI.DataTransferObjects.Catalogs;

namespace WebAPI.MappingProfiles;

public static class CatalogProfile
{
    public class CatalogResponseToOutputProfile : Profile
    {
        public CatalogResponseToOutputProfile()
        {
            CreateMap<Projections.Catalog, Outputs.Catalog>();
            CreateMap<Responses.Catalogs, Outputs.Catalogs>();

            CreateMap<Projections.CatalogItem, Outputs.CatalogItem>();
            CreateMap<Responses.CatalogItems, Outputs.CatalogItems>();
        }
    }
}