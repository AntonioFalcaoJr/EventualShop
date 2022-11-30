using Application.Abstractions;
using Contracts.Services.Identity;

namespace Application.UseCases.Queries;

public class LoginInteractor : IInteractor<Query.Login, Projection.UserDetails>
{
    private readonly IProjectionGateway<Projection.UserDetails> _gateway;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginInteractor(
        IProjectionGateway<Projection.UserDetails> gateway,
        IJwtTokenGenerator tokenGenerator)
    {
        _gateway = gateway;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<Projection.UserDetails?> InteractAsync(Query.Login query, CancellationToken cancellationToken)
    {
        var userDetails = await _gateway.FindAsync(user => user.Email == query.Email, cancellationToken);

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