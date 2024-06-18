using CodeBuilder.Data;
using CodeBuilder.Models.Briefs;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Operations.Commands
{
    public sealed class DeleteBriefCheckCommand : ExecutableBase, ICommand<Task<bool>> 
    {

        // Fields 

        private readonly BriefCheckModel _model;

        // Constructors

        public DeleteBriefCheckCommand(IDbContextFactory<ApplicationDbContext> factory, BriefCheckModel model) : base(factory)
        {
            _model = model;
        }

        // Methods

        public async Task<bool> Execute()
        {
            using var ctx = await _factory.CreateDbContextAsync();

            ctx.Set<BriefCheckModel>().Attach(_model);
            ctx.Set<BriefCheckModel>().Remove(_model);

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
