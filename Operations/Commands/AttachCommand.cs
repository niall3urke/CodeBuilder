using CodeBuilder.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Operations.Commands
{
    public class AttachCommand<T> : ExecutableBase, ICommand<Task<bool>>
        where T : class
    {

        // Fields

        private readonly T _model;

        // Constructors

        public AttachCommand(IDbContextFactory<ApplicationDbContext> factory, T model) : base(factory)
        {
            _model = model;
        }

        // Methods

        public async Task<bool> Execute()
        {
            try
            {
                using var ctx = await _factory.CreateDbContextAsync();

                ctx.Set<T>().Attach(_model);

                return await ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
