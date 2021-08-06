using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.Abstractions.UseCases;

namespace Application.Abstractions.EventSourcing.Projections
{
    public interface IProjectionsRepository
    {
        Task<TModel> FindAsync<TModel>(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken)
            where TModel : Model;

        Task<TModel> GetAsync<TModel, TId>(TId id, CancellationToken cancellationToken)
            where TModel : Model;

        Task<IPagedResult<TModel>> GetAllAsync<TModel>(IPaging paging, Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken)
            where TModel : Model;

        Task SaveAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : Model;

        Task SaveManyAsync<TModel>(IEnumerable<TModel> models, CancellationToken cancellationToken)
            where TModel : Model;

        Task UpdateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : Model;

        Task UpdateManyAsync<TModel>(IEnumerable<TModel> models, CancellationToken cancellationToken)
            where TModel : Model;

        Task DeleteAsync<TModel>(Expression<Func<TModel, bool>> filter, CancellationToken cancellationToken)
            where TModel : Model;

        Task DeleteManyAsync<TModel>(Expression<Func<TModel, bool>> filter, CancellationToken cancellationToken)
            where TModel : Model;
    }
}