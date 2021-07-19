using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Infrastructure.DependencyInjection.Options
{
    public class TransactionOptions
    {
        [Required, EnumDataType(typeof(IsolationLevel))]
        public IsolationLevel IsolationLevel { get; set; }
    }
}