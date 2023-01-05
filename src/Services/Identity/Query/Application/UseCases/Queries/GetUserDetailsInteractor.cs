using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions;
using Contracts.Services.Identity;

namespace Application.UseCases.Queries;

public class GetUserDetailsInteractor : IInteractor<Query.Login, Projection.UserDetails>
{
    private readonly IProjectionGateway<Projection.UserDetails> _projectionGateway;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public GetUserDetailsInteractor(
        IProjectionGateway<Projection.UserDetails> projectionGateway,
        IJwtTokenGenerator tokenGenerator)
    {
        _projectionGateway = projectionGateway;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<Projection.UserDetails?> InteractAsync(Query.Login query, CancellationToken cancellationToken)
    {
        var userDetails = await _projectionGateway.FindAsync(user => user.Email == query.Email, cancellationToken);

        return userDetails switch
        {
            {Password: var password} when password == query.Password => userDetails with {Token = GenerateToken(userDetails)},
            {Password: var password} when password != query.Password => default,
            _ => default
        };
    }

    private string GenerateToken(Projection.UserDetails userDetails)
        => _tokenGenerator.Generate(
            userDetails.Id,
            userDetails.FirstName,
            userDetails.LastName,
            userDetails.Email);
}