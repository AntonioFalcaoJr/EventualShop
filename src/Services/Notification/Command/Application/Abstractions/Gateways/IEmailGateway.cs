using Domain.ValueObjects;

namespace Application.Abstractions.Gateways;

public interface IEmailGateway : INotificationGateway<Email>;