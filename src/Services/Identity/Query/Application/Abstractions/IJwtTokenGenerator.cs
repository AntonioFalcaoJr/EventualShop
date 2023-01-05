using System;

namespace Application.Abstractions;

public interface IJwtTokenGenerator
{
     string Generate(Guid userId, string firstName, string lastName, string email);
}