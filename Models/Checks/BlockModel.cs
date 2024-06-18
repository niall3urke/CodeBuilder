using CodeBuilder.Code;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBuilder.Models
{
    public class BlockModel : IEntity, IIndexable
    {

        // Properties

        [ForeignKey("Variables")]
        public Guid? CalculationId { get; set; }

        [ForeignKey("Condition")]
        public Guid? ConditionId { get; set; }

        [ForeignKey("Parent")]
        public Guid? ParentId { get; set; }

        [ForeignKey("Check")]
        public Guid? CheckId { get; set; }


        public VariableModel? Calculation { get; set; }

        public ConditionModel? Condition { get; set; }

        public List<BlockModel> Children { get; set; }

        public BlockModel? Parent { get; set; }

        public CheckModel? Check { get; set; }

        public Guid Id { get; set; }


        public string UnitName => Calculation?.Unit.GetDescription() ?? "";

        public string MathName => Calculation?.Math ?? "";

        public string Name => Calculation?.Name ?? "";

        public string Desc => Calculation?.Desc ?? "";

        public int Index { get; set; }

        // Constructor

        public BlockModel()
        {
            Children = new List<BlockModel>();
        }


    }
}
