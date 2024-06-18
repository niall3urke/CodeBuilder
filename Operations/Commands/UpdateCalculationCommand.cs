using CodeBuilder.Data;
using CodeBuilder.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Operations.Commands
{
    public class UpdateCalculationCommand : ExecutableBase, ICommand<Task<bool>>
    {

        // Fields

        private readonly IEnumerable<IVariable> _variables;
        private readonly VariableModel _model;

        // Constructors

        public UpdateCalculationCommand(IDbContextFactory<ApplicationDbContext> factory, VariableModel model, IEnumerable<IVariable> variables) : base(factory)
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

                var calculation = await ctx.Variables
                    .Include("Standard")
                    .FirstOrDefaultAsync(x => x.Id == _model.Id);

                if (calculation == null)
                    return false;

                // Get the variables associated with the _model calculation

                foreach (var variable in _variables.Where(x => x.Id == Guid.Empty))
                {
                    ctx.Add(variable);
                }

                // Get the maps

                //var maps = ctx.CalculationVariables.Where(x => x.CalculationId == calculation.Id);

                //// Add new maps

                //foreach (var variable in _variables)
                //{
                //    if (!maps.Any(x => x.VariableId == variable.Id))
                //    {
                //        ctx.CalculationVariables.Add(new() { CalculationId = calculation.Id, VariableId = variable.Id });
                //    }
                //}

                // Remove old maps

                //foreach (var map in maps)
                //{
                //    if (!_variables.Any(x => x.Id == map.VariableId))
                //    {
                //        ctx.CalculationVariables.Remove(map);
                //    }
                //}

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

                calculation.Standard = standard;

                // Basic values

                calculation.CodeCalculation = _model.CodeCalculation;

                calculation.MathCalculation = _model.MathCalculation;

                calculation.Reference = _model.Reference;

                calculation.Storage = _model.Storage;

                calculation.State = _model.State;

                calculation.Value = _model.Value;

                calculation.Unit = _model.Unit;

                calculation.Name = _model.Name;

                calculation.Desc = _model.Desc;

                calculation.Math = _model.Math;

                calculation.Code = _model.Code;

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
