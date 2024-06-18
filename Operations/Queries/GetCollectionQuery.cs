using CodeBuilder.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodeBuilder.Operations.Queries
{
    public class GetCollectionQuery<T> : ExecutableBase, IQuery<Task<IEnumerable<T>>> where T : class
    {

        // Fields

        private Func<IQueryable<T>, IOrderedQueryable<T>> _orderBy;
        private Expression<Func<T, bool>>[] _filters;
        private string[] _includes;
        private int _skip;
        private int _take;

        // Constructors

        public GetCollectionQuery(IDbContextFactory<ApplicationDbContext> factory) : base(factory)
        {
            _take = 10000;
            _skip = 0;
        }

        // Methods

        public GetCollectionQuery<T> WithFilters(params Expression<Func<T, bool>>[] filters)
        {
            _filters = filters;
            return this;
        }

        public GetCollectionQuery<T> WithIncludes(params string[] includes)
        {
            _includes = includes;
            return this;
        }

        public GetCollectionQuery<T> WithOrder(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            _orderBy = orderBy;
            return this;
        }

        public GetCollectionQuery<T> WithSkip(int skip)
        {
            _skip = skip;
            return this;
        }

        public GetCollectionQuery<T> WithTake(int take)
        {
            _take = take;
            return this;
        }

        public async Task<IEnumerable<T>> Execute()
        {
            using var ctx = await _factory.CreateDbContextAsync();

            var query = ctx.Set<T>().AsNoTracking();

            if (_includes != null)
            {
                foreach (string include in _includes)
                {
                    query = query.Include(include);
                }

                query = query.AsSplitQuery();
            }

            if (_filters != null)
            {
                foreach (var filter in _filters)
                {
                    query = query.Where(filter);
                }
            }

            if (_orderBy != null)
            {
                query = _orderBy(query);
            }

            return await query
                .Skip(_skip)
                .Take(_take)
                .ToListAsync();
        }
    }
}
