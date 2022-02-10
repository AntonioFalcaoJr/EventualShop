using AutoMapper;
using ECommerce.Contracts.ShoppingCart;
using ECommerce.WebAPI.DataTransferObjects.ShoppingCarts;

namespace ECommerce.WebAPI.MapperProfiles;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<Responses.CartDetails, Outputs.CartDetails>();
    }
}