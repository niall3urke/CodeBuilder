using CodeBuilder.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodeBuilder.Operations.Queries
{
    public class GetCountQuery<T> : ExecutableBase, IQuery<Task<int>> where T : class, IEntity
    {

        // Fields

        private Expression<Func<T, bool>>[] _filters;
        private string[] _includes;


        // Constructors

        public GetCountQuery(IDbContextFactory<ApplicationDbContext> factory) : base(factory){ }

        // Methods

        public GetCountQuery<T> WithFilters(params Expression<Func<T, bool>>[] filters)
        {
            _filters = filters;
            return this;
        }

        public GetCountQuery<T> WithIncludes(params string[] includes)
        {
            _includes = includes;
            return this;
        }

        public async Task<int> Execute()
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

            return await query.CountAsync();
        }


    }
}
