using CodeBuilder.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodeBuilder.Operations.Queries
{
    public sealed class GetQuery<T> : ExecutableBase, IQuery<Task<T>> where T : class
    {

        // Fields

        private Expression<Func<T, bool>>[] _filters;
        private string[] _includes;
        private Guid? _id;

        // Constructors

        public GetQuery(IDbContextFactory<ApplicationDbContext> factory) : base(factory) { }

        // Methods
        
        public GetQuery<T> WithFilters(params Expression<Func<T, bool>>[] filters)
        {
            _filters = filters;
            return this;
        }

        public GetQuery<T> WithIncludes(params string[] includes)
        {
            _includes = includes;
            return this;
        }

        public GetQuery<T> WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public async Task<T> Execute()
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

            if (_id.HasValue && typeof(T).GetInterface(nameof(IEntity)) != null)
            {
                return await query.FirstOrDefaultAsync(x => (x as IEntity).Id == _id.Value);
            }

            return await query.FirstOrDefaultAsync();
        }




    }
}
