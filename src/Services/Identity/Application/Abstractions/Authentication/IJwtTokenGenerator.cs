namespace Application.Abstractions.Authentication;

public interface IJwtTokenGenerator
{
     string Generate(Guid userId, string firstName, string lastName, string email);
}