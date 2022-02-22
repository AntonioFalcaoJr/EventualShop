using AutoMapper;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.ShoppingCart;
using ECommerce.WebAPI.DataTransferObjects.ShoppingCarts;

namespace ECommerce.WebAPI.MapperProfiles;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<Responses.Cart, Dtos.Cart>();
        CreateMap<Responses.CartItemsPagedResult, Outputs.CartItemsPagedResult>();
        
        // TODO - solve this!
        CreateMap<Dtos.Item, Models.Item>().ReverseMap();
        CreateMap<Responses.CartItem, Dtos.Item>();
    }
}