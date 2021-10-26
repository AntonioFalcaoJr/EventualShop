using System.Reflection;
using MassTransit.Definition;
using MassTransit.Topology;

namespace Infrastructure.DependencyInjection.Extensions
{
    internal static class NameFormatterExtensions
    {
        public static string ToKebabCaseString(this MemberInfo member)
            => KebabCaseEndpointNameFormatter.Instance.SanitizeName(member.Name);
    }

    internal class KebabCaseEntityNameFormatter : IEntityNameFormatter
    {
        public string FormatEntityName<T>()
            => typeof(T).ToKebabCaseString();
    }
}