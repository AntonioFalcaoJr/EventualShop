namespace Infrastructure.Authentication.Abstractions;

public interface IPasswordHasher
{
    string HashPassword(string password);

    bool VerifyHashedPassword(string hashedPassword, string password);
}