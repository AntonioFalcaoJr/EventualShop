namespace Infrastructure.Authentication.Abstractions;

public interface IJwtTokenGenerator
{
     string Generate(Guid userId, string firstName, string lastName, string email);
}