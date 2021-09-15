using System.Reflection;
using MassTransit.Definition;

namespace Infrastructure.DependencyInjection.Extensions
{
    internal static class EndpointNameFormatterExtensions
    {
        public static string ToKebabCaseString(this MemberInfo member)
            => KebabCaseEndpointNameFormatter.Instance.SanitizeName(member.Name);
    }
}