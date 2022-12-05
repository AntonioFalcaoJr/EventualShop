using Contracts.Abstractions;
using Contracts.Services.Communication.Protobuf;

namespace Contracts.Services.Communication;

public static class Projection
{
    public record EmailSent(Guid Id, string Email, bool IsDeleted) : IProjection
    {
        public static implicit operator Email(EmailSent emailSent)
            => new()
            {
                Id = emailSent.Id.ToString(),
                Email_ = emailSent.Email,
            };
    }
}