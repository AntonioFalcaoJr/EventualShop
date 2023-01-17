namespace Application.Services.DebitCards.Http;

public static class Requests
{
    public record DebitCardAuthorizePayment;

    public record DebitCardCancelPayment;

    public record DebitCardRefundPayment;
}