using AutoMapper;
using ECommerce.Contracts.ShoppingCart;

namespace ECommerce.WebAPI.MapperProfiles;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<Responses.CartDetails, DataTransferObjects.ShoppingCarts.Outputs.CartDetails>();
    }
}