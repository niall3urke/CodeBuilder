using CodeBuilder.Data;
using CodeBuilder.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Operations.Commands
{
    public class UpdateVariableGroupCommand : ExecutableBase, ICommand<Task<bool>>
    {

        // Fields

        private readonly IEnumerable<VariableModel> _variables;
        private readonly VariableGroupModel _model;

        // Constructors

        public UpdateVariableGroupCommand(IDbContextFactory<ApplicationDbContext> factory, VariableGroupModel model, IEnumerable<VariableModel> variables) : base(factory)
        {
            _model = model;
            _variables = variables;
        }

        // Methods

        public async Task<bool> Execute()
        {
            try
            {
                using var ctx = await _factory.CreateDbContextAsync();

                // Get the existing calculation model from the database

                var model = await ctx.VariableGroups
                    .Include("Standard")
                    .Include("Children")
                    .FirstOrDefaultAsync(x => x.Id == _model.Id);

                if (model == null)
                    return false;

                // Get the variables associated with the _model calculation

                foreach (var variable in _variables.Where(x => x.Id == Guid.Empty))
                {
                    ctx.Add(variable);
                }

                // Get the maps

                var maps = ctx.VariableGroupVariables.Where(x => x.GroupId == model.Id);

                // Add new maps

                foreach (var variable in _variables)
                {
                    if (!maps.Any(x => x.VariableId == variable.Id))
                    {
                        ctx.VariableGroupVariables.Add(new() { GroupId = model.Id, VariableId = variable.Id });
                    }
                }

                // Remove old maps

                foreach (var map in maps)
                {
                    if (!_variables.Any(x => x.Id == map.VariableId))
                    {
                        ctx.VariableGroupVariables.Remove(map);
                    }
                }

                // Get the standard associated with the calculation

                StandardModel standard = null;

                if (_model.Standard != null)
                {
                    var dbStandard = await ctx.Standards.FindAsync(_model.Standard.Id);

                    if (dbStandard != null)
                    {
                        standard = dbStandard;
                    }
                }

                // If the calculation variable isn't null, apply the standard

                model.Standard = standard;

                // Save all changes

                return await ctx.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }


    }
}
