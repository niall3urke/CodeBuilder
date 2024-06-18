using CodeBuilder.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Operations.Commands
{
    public class UpdateCommand<T> : ExecutableBase, ICommand<Task<bool>>
        where T : class, IEntity
    {

        // Fields

        private readonly T _model;

        // Constructors

        public UpdateCommand(IDbContextFactory<ApplicationDbContext> factory, T model) : base(factory)
        {
            _model = model;
        }

        // Methods

        public async Task<bool> Execute()
        {
            try
            {
                using var ctx = await _factory.CreateDbContextAsync();
                
                ctx.Set<T>().Update(_model);

                return await ctx.SaveChangesAsync() > 0;
            }

            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return false;
        }
    }
}
