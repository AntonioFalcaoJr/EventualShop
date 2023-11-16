using Domain.ValueObject;

namespace Application.Abstractions.Gateways;

public interface IEmailGateway : INotificationGateway<Email>;