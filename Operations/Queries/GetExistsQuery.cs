using CodeBuilder.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodeBuilder.Operations.Queries
{
    public class GetExistsQuery<T> : ExecutableBase, IQuery<Task<bool>> where T : class, IEntity
    {

        // Fields

        private Expression<Func<T, bool>>[] _filters;
        private string[] _includes;
        private Guid? _id;

        // Constructors

        public GetExistsQuery(IDbContextFactory<ApplicationDbContext> factory) : base(factory) { }

        // Methods

        public GetExistsQuery<T> WithFilters(params Expression<Func<T, bool>>[] filters)
        {
            _filters = filters;
            return this;
        }

        public GetExistsQuery<T> WithIncludes(params string[] includes)
        {
            _includes = includes;
            return this;
        }

        public GetExistsQuery<T> WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public async Task<bool> Execute()
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

            if (_id.HasValue)
            {
                query = query.Where(x => x.Id == _id.Value);
            }

            return await query.AnyAsync();
        }


    }
}
