namespace Application.Services.Emails.Http;

public static class Requests
{
    public record CreditCardAuthorizePayment;

    public record CreditCardCancelPayment;

    public record CreditCardRefundPayment;
}