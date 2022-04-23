using AutoMapper;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.ShoppingCarts;

namespace WebAPI.MappingProfiles;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<Requests.AddShoppingCartItem, Models.ShoppingCartItem>();
        CreateMap<Requests.AddPayPal, Models.PayPal>();
        CreateMap<Requests.AddCreditCard, Models.CreditCard>();
        CreateMap<Requests.AddAddress, Models.Address>();
    }
}