using CodeBuilder.Data;
using CodeBuilder.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Operations.Queries
{
    public class GetCheckQuery : ExecutableBase, IQuery<Task<CheckModel>>
    {

        // Fields

        private readonly Guid _id;

        // Constructors

        public GetCheckQuery(IDbContextFactory<ApplicationDbContext> factory, Guid id) : base(factory)
        {
            _id = id;
        }

        // Methods

        public async Task<CheckModel> Execute()
        {
            using var ctx = await _factory.CreateDbContextAsync();

            var check = await ctx.Checks
                .Include(x => x.Block)
                .Include(x => x.Standard)
                .FirstOrDefaultAsync(x => x.Id == _id);

            if (check != null)
            {
                await LoadData(check.Block, ctx);
            }

            return check;
        }

        private async Task<BlockModel> LoadData(BlockModel block, ApplicationDbContext ctx)
        {
            await ctx.Entry(block).Reference(x => x.Calculation).LoadAsync();
            await ctx.Entry(block).Reference(x => x.Condition).LoadAsync();
            await ctx.Entry(block).Collection(x => x.Children).LoadAsync();

            if (block.Condition != null)
            {
                await ctx.Entry(block.Condition).Reference(x => x.Lhs).LoadAsync();
                await ctx.Entry(block.Condition).Reference(x => x.Rhs).LoadAsync();
            }

            if (block.Calculation != null)
            {
                await ctx.Entry(block.Calculation).Collection(x => x.Variables).LoadAsync();

                if (block.Calculation.Variables != null)
                {
                    foreach (var cv in block.Calculation.Variables)
                    {
                        await ctx.Entry(cv).Reference(x => x.Input).LoadAsync();
                    }
                }
            }

            foreach (var child in block.Children)
            {
                await LoadData(child, ctx);
            }

            return block;
        }



    }
}
