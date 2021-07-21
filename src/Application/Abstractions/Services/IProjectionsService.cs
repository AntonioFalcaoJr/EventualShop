using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IProjectionsService
    {
        Task SaveAsync<TModel>(TModel model, CancellationToken cancellationToken);
        Task UpdateAsync<TModel>(TModel model, CancellationToken cancellationToken);
        Task DeleteOneAsync<TModel>(Expression<Func<TModel, bool>> filter, CancellationToken cancellationToken);
        Task DeleteManyAsync<TModel>(Expression<Func<TModel, bool>> filter, CancellationToken cancellationToken);
    }
}