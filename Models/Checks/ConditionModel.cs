using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBuilder.Models
{
    public class ConditionModel : IEntity
    {

        // Properties

        [ForeignKey("Lhs")]
        public Guid? LhsId { get; set; }

        [ForeignKey("Rhs")]
        public Guid? RhsId { get; set; }

        public ConditionType ConditionType { get; set; }

        public OperationType OperationType { get; set; }

        public ConditionItemType LhsType { get; set; }

        public ConditionItemType RhsType { get; set; }

        public VariableModel? Lhs { get; set; }

        public VariableModel? Rhs { get; set; }

        public string? LhsValue { get; set; }

        public string? RhsValue { get; set; }

        public Guid Id { get; set; }

        // Constructors 

        public ConditionModel() { }

    }
}
