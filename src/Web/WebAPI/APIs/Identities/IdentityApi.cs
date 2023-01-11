using Asp.Versioning.Builder;
using Contracts.Services.Identity.Protobuf;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Identities;

public static class IdentityApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/identities/";

    public static IVersionedEndpointRouteBuilder MapIdentityApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/sign-in", ([AsParameters] Queries.SignIn query)
            => ApplicationApi.GetAsync<IdentityService.IdentityServiceClient, UserDetails>
                (query, (client, cancellationToken) => client.LoginAsync(query, cancellationToken: cancellationToken)));

        group.MapPost("/sign-up", ([AsParameters] Commands.SignUp signUp)
            => ApplicationApi.SendCommandAsync(signUp));

        group.MapPost("/{userId:guid}/confirm-email", ([AsParameters] Commands.ConfirmEmail confirmEmail)
            => ApplicationApi.SendCommandAsync(confirmEmail));

        group.MapPut("/{userId:guid}/change-email", ([AsParameters] Commands.ChangeEmail changeEmail)
            => ApplicationApi.SendCommandAsync(changeEmail));

        group.MapPut("/{userId:guid}/change-password", ([AsParameters] Commands.ChangePassword changePassword)
            => ApplicationApi.SendCommandAsync(changePassword));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapIdentityApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/sign-in", ([AsParameters] Queries.SignIn query)
            => ApplicationApi.GetAsync<IdentityService.IdentityServiceClient, UserDetails>
                (query, (client, cancellationToken) => client.LoginAsync(query, cancellationToken: cancellationToken)));

        return builder;
    }
}