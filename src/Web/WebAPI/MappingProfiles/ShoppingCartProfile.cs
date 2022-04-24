using AutoMapper;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.ShoppingCarts;

namespace WebAPI.MappingProfiles;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<Request.AddShoppingCartItem, Models.ShoppingCartItem>();
        CreateMap<Request.AddPayPal, Models.PayPal>();
        CreateMap<Request.AddCreditCard, Models.CreditCard>();
        CreateMap<Request.AddAddress, Models.Address>();
    }
}