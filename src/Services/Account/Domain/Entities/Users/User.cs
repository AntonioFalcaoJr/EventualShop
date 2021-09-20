using System;
using Domain.Abstractions.Entities;

namespace Domain.Entities.Users
{
    public class User : Entity<Guid>
    {
        public User(string password, string passwordConfirmation, string name)
        {
            Id = Guid.NewGuid();
            Password = password;
            PasswordConfirmation = passwordConfirmation;
            Name = name;
        }

        public string Password { get; private set; }
        public string PasswordConfirmation { get; private set; }
        public string Name { get; }

        public void ChangePassword(string password, string passwordConfirmation)
        {
            Password = password;
            PasswordConfirmation = passwordConfirmation;
        }

        protected override bool Validate()
            => OnValidate<UserValidator, User>();
    }
}