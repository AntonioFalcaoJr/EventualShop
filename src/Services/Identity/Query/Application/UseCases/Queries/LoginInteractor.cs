using Application.Abstractions.Authentication;
using Application.Abstractions.Projections;
using Application.Abstractions.UseCases;
using Contracts.Services.Identity;

namespace Application.UseCases.Queries;

public class LoginInteractor : IInteractor<Query.Login, Projection.UserDetails>
{
    private readonly IProjectionRepository<Projection.UserDetails> _repository;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginInteractor(
        IProjectionRepository<Projection.UserDetails> repository,
        IJwtTokenGenerator tokenGenerator)
    {
        _repository = repository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<Projection.UserDetails?> InteractAsync(Query.Login query, CancellationToken ct)
    {
        var userDetails = await _repository.FindAsync(user => user.Email == query.Email, ct);

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