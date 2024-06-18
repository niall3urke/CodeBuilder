using CodeBuilder.Data;
using CodeBuilder.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Operations.Commands
{
    public sealed class DeleteCalculationVariablesCommand : ExecutableBase, ICommand<Task<bool>>
    {

        // Fields

        private readonly Guid _id;

        // Constructors

        public DeleteCalculationVariablesCommand(IDbContextFactory<ApplicationDbContext> factory, Guid id) : base(factory)
        {
            _id = id;
        }

        // Methods

        public async Task<bool> Execute()
        {
            using var ctx = await _factory.CreateDbContextAsync();

            //var maps = ctx.CalculationVariables.Where(x => x.CalculationId == _id);

            //ctx.CalculationVariables.RemoveRange(maps);

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
