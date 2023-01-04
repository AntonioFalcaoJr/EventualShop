using Asp.Versioning.Builder;
using Contracts.Services.Identity;
using Contracts.Services.Identity.Protobuf;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Identities;

public static class IdentityApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/identities/";

    public static IVersionedEndpointRouteBuilder MapIdentityApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/sign-in", ([AsParameters] Requests.SignIn request)
            => ApplicationApi.GetAsync<IdentityService.IdentityServiceClient, UserDetails>
                (request, (client, cancellationToken) => client.LoginAsync(request, cancellationToken: cancellationToken)));

        group.MapPost("/sign-up", ([AsParameters] Requests.SignUp request)
            => ApplicationApi.SendCommandAsync<Command.RegisterUser>(request));

        group.MapPost("/{userId:guid}/confirm-email", ([AsParameters] Requests.ConfirmEmail request)
            => ApplicationApi.SendCommandAsync<Command.ConfirmEmail>(request));

        group.MapPut("/{userId:guid}/change-email", ([AsParameters] Requests.ChangeEmail request)
            => ApplicationApi.SendCommandAsync<Command.ChangeEmail>(request));

        group.MapPut("/{userId:guid}/change-password", ([AsParameters] Requests.ChangePassword request)
            => ApplicationApi.SendCommandAsync<Command.ChangePassword>(request));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapIdentityApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/sign-in", ([AsParameters] Requests.SignIn request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.LoginAsync(request, cancellationToken: cancellationToken)));

        return builder;
    }
}