using CodeBuilder.Data;
using CodeBuilder.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Operations.Commands
{
    public sealed class CreateCommand<T> : ExecutableBase, ICommand<Task<bool>>
        where T : class
    {

        // Fields

        private readonly T _model;

        // Constructors

        public CreateCommand(IDbContextFactory<ApplicationDbContext> factory, T model) : base(factory)
        {
            _model = model;
        }

        // Methods

        public async Task<bool> Execute()
        {
            try
            {
                using var ctx = await _factory.CreateDbContextAsync();

                await ctx.Set<T>().AddAsync(_model);

                return await ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
