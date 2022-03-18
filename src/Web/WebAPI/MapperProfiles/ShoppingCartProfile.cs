using AutoMapper;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.ShoppingCart;
using WebAPI.DataTransferObjects.ShoppingCarts;

namespace WebAPI.MapperProfiles;

public static class ShoppingCartProfile 
{
    public class ShoppingCartRequestsToModelsProfile : Profile
    {
        public ShoppingCartRequestsToModelsProfile()
        {
            CreateMap<Requests.AddCartItem, Models.ShoppingCartItem>();
            CreateMap<Requests.PaymentWithPayPal, Models.PayPal>();
            CreateMap<Requests.PaymentWithCreditCard, Models.CreditCard>();
            CreateMap<Requests.AddAddress, Models.Address>();
        }
    }
    
    public class ShoppingCartResponsesToOutputProfile : Profile
    {
        public ShoppingCartResponsesToOutputProfile()
        {
            CreateMap<Responses.ShoppingCart, Outputs.ShoppingCart>();
            CreateMap<Responses.ShoppingCartItem, Outputs.ShoppingCartItem>();
            CreateMap<Responses.ShoppingCartItems, Outputs.ShoppingCartItems>();
        }
    }

    public class ShoppingCartModelsToOutputProfile : Profile
    {
        public ShoppingCartModelsToOutputProfile()
        {
            CreateMap<Models.ShoppingCartItem, Outputs.ShoppingCartItem>();
        }
    }
}