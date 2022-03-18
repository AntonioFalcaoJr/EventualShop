using AutoMapper;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.ShoppingCart;
using WebAPI.DataTransferObjects.ShoppingCarts;

namespace WebAPI.MapperProfiles;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        #region Requests --> Models
            CreateMap<Requests.AddCartItem, Models.ShoppingCartItem>();
            CreateMap<Requests.PaymentWithPayPal, Models.PayPal>();
            CreateMap<Requests.PaymentWithCreditCard, Models.CreditCard>();
            CreateMap<Requests.AddAddress, Models.Address>();
        #endregion

        #region Responses --> Outputs
            CreateMap<Responses.ShoppingCart, Outputs.ShoppingCart>();
            CreateMap<Responses.ShoppingCartItem, Outputs.ShoppingCartItem>();
            CreateMap<Responses.ShoppingCartItems, Outputs.ShoppingCartItems>();
        #endregion

        CreateMap<Models.ShoppingCartItem, Outputs.ShoppingCartItem>();


    }
}