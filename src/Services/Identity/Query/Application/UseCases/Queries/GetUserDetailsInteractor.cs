using Application.Abstractions;
using Contracts.Boundaries.Identity;

namespace Application.UseCases.Queries;

public class GetUserDetailsInteractor(IProjectionGateway<Projection.UserDetails> projectionGateway,
        IJwtTokenGenerator tokenGenerator)
    : IInteractor<Query.Login, Projection.UserDetails>
{
    public async Task<Projection.UserDetails?> InteractAsync(Query.Login query, CancellationToken cancellationToken)
    {
        var userDetails = await projectionGateway.FindAsync(user => user.Email == query.Email, cancellationToken);

        return userDetails switch
        {
            { Password: var password } when password == query.Password => userDetails with { Token = GenerateToken(userDetails) },
            { Password: var password } when password != query.Password => default,
            _ => default
        };
    }

    private string GenerateToken(Projection.UserDetails userDetails)
        => tokenGenerator.Generate(
            userDetails.Id,
            userDetails.FirstName,
            userDetails.LastName,
            userDetails.Email);
}