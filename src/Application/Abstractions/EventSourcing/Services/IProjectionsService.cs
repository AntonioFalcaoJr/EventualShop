using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.UseCases.Customers.Queries.GetCustomers;

namespace Application.Abstractions.EventSourcing.Services
{
    public interface IProjectionsService
    {
        Task<TModel> FindAsync<TModel>(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken)
            where TModel : Models.Model;

        Task<TModel> GetAsync<TModel, TId>(TId id, CancellationToken cancellationToken)
            where TModel : Models.Model;

        Task<List<TModel>> GetAllAsync<TModel>(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken)
            where TModel : Models.Model;

        Task SaveAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : Models.Model;

        Task SaveManyAsync<TModel>(IEnumerable<TModel> models, CancellationToken cancellationToken)
            where TModel : Models.Model;

        Task UpdateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : Models.Model;

        Task UpdateManyAsync<TModel>(IEnumerable<TModel> models, CancellationToken cancellationToken)
            where TModel : Models.Model;

        Task DeleteAsync<TModel>(Expression<Func<TModel, bool>> filter, CancellationToken cancellationToken)
            where TModel : Models.Model;

        Task DeleteManyAsync<TModel>(Expression<Func<TModel, bool>> filter, CancellationToken cancellationToken)
            where TModel : Models.Model;
    }
}