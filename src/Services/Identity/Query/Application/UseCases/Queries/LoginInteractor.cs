using Application.Abstractions;
using Contracts.Services.Identity;
using Infrastructure.Authentication.Abstractions;

namespace Application.UseCases.Queries;

public class LoginInteractor : IInteractor<Query.Login, Projection.UserDetails>
{
    private readonly IProjectionGateway<Projection.UserDetails> _gateway;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public LoginInteractor(
        IProjectionGateway<Projection.UserDetails> gateway,
        IJwtTokenGenerator tokenGenerator,
        IPasswordHasher passwordHasher)
    {
        _gateway = gateway;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<Projection.UserDetails?> InteractAsync(Query.Login query, CancellationToken cancellationToken)
    {
        // TODO: if email does not exist, return notfound ?
        var userDetails = await _gateway.FindAsync(user => user.Email == query.Email, cancellationToken);

        return userDetails switch
        {
            {Password: var password} when VerifyPassword(password, query.Password) => userDetails with {Token = GenerateToken(userDetails)},
            _ => default
        };
    }

    private bool VerifyPassword(string hashedPassword, string password)
        => _passwordHasher.VerifyHashedPassword(hashedPassword, password);

    private string GenerateToken(Projection.UserDetails userDetails)
        => _tokenGenerator.Generate(
            userDetails.Id,
            userDetails.FirstName,
            userDetails.LastName,
            userDetails.Email);
}