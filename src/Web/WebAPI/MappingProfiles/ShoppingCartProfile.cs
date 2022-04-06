using AutoMapper;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.ShoppingCart;
using WebAPI.DataTransferObjects.ShoppingCarts;

namespace WebAPI.MappingProfiles;

public static class ShoppingCartProfile 
{
    public class ShoppingCartRequestToModelProfile : Profile
    {
        public ShoppingCartRequestToModelProfile()
        {
            CreateMap<Requests.AddShoppingCartItem, Models.ShoppingCartItem>();
            CreateMap<Requests.AddPayPal, Models.PayPal>();
            CreateMap<Requests.AddCreditCard, Models.CreditCard>();
            CreateMap<Requests.AddAddress, Models.Address>();
        }
    }
    
    public class ShoppingCartResponseToOutputProfile : Profile
    {
        public ShoppingCartResponseToOutputProfile()
        {
            CreateMap<Responses.ShoppingCart, Outputs.ShoppingCart>();
            CreateMap<Responses.ShoppingCartItem, Outputs.ShoppingCartItem>();
            CreateMap<Responses.ShoppingCartItems, Outputs.ShoppingCartItems>();
        }
    }

    public class ShoppingCartModelToOutputProfile : Profile
    {
        public ShoppingCartModelToOutputProfile()
        {
            CreateMap<Models.ShoppingCartItem, Outputs.ShoppingCartItem>();
        }
    }
}