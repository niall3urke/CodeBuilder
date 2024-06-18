using CodeBuilder.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Operations.Commands
{
    public sealed class DeleteCommand<T> : ExecutableBase, ICommand<Task<bool>>
        where T : class, IEntity, new()
    {

        // Fields

        private readonly Guid _id;

        // Constructors

        public DeleteCommand(IDbContextFactory<ApplicationDbContext> factory, Guid id) : base(factory)
        {
            _id = id;
        }

        // Methods

        public async Task<bool> Execute()
        {
            using var ctx = await _factory.CreateDbContextAsync();

            T factor;

            try
            {
                factor = new T() { Id = _id };
            }
            catch (Exception ex)
            {
                return false;
            }

            ctx.Set<T>().Attach(factor);
            ctx.Set<T>().Remove(factor);

            try
            {
                return await ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
