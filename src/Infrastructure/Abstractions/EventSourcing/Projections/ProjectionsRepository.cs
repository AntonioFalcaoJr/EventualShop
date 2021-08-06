using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.Abstractions.UseCases;
using Infrastructure.Abstractions.EventSourcing.Projections.Pagination;
using Infrastructure.EventSourcing.Customers.Projections.Contexts;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Abstractions.EventSourcing.Projections
{
    public abstract class ProjectionsRepository : IProjectionsRepository
    {
        private readonly IMongoDbContext _context;

        protected ProjectionsRepository(IMongoDbContext context)
        {
            _context = context;
        }

        public virtual Task<TModel> GetAsync<TModel, TId>(TId id, CancellationToken cancellationToken) where TModel : Model
            => FindAsync<TModel>(model => model.Id.Equals(id), cancellationToken);

        public virtual Task<TModel> FindAsync<TModel>(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken) where TModel : Model
            => _context.GetCollection<TModel>().AsQueryable().Where(predicate).FirstOrDefaultAsync(cancellationToken);

        public virtual Task<IPagedResult<TModel>> GetAllAsync<TModel>(IPaging paging, Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken) where TModel : Model
        {
            var queryable = _context.GetCollection<TModel>().AsQueryable().Where(predicate);
            return PagedResult<TModel>.CreateAsync(paging, queryable, cancellationToken);
        }

        public virtual Task SaveAsync<TModel>(TModel model, CancellationToken cancellationToken) where TModel : Model
            => _context.GetCollection<TModel>().InsertOneAsync(model, cancellationToken: cancellationToken);

        public virtual Task SaveManyAsync<TModel>(IEnumerable<TModel> models, CancellationToken cancellationToken) where TModel : Model
            => _context.GetCollection<TModel>().InsertManyAsync(models, cancellationToken: cancellationToken);

        public virtual Task UpdateAsync<TModel>(TModel model, CancellationToken cancellationToken) where TModel : Model
            => _context.GetCollection<TModel>().FindOneAndReplaceAsync<TModel>(default, model, default, cancellationToken);

        public virtual Task UpdateManyAsync<TModel>(IEnumerable<TModel> models, CancellationToken cancellationToken) where TModel : Model
            => throw new NotImplementedException();

        public virtual Task DeleteAsync<TModel>(Expression<Func<TModel, bool>> filter, CancellationToken cancellationToken) where TModel : Model
            => _context.GetCollection<TModel>().DeleteOneAsync(filter, cancellationToken);

        public virtual Task DeleteManyAsync<TModel>(Expression<Func<TModel, bool>> filter, CancellationToken cancellationToken) where TModel : Model
            => _context.GetCollection<TModel>().DeleteManyAsync(filter, cancellationToken);
    }
}