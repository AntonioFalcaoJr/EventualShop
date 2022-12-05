using Contracts.Abstractions;
using Contracts.Services.Communication.Protobuf;

namespace Contracts.Services.Communication;

public static class Projection
{
    public record EmailSent(Guid Id, Guid UserId, string Email, bool IsDeleted) : IProjection
    {
        public static implicit operator Email(EmailSent emailSent)
            => new()
            {
                Id = emailSent.Id.ToString(),
                UserId = emailSent.UserId.ToString(),
                Email_ = emailSent.Email
            };
    }
}