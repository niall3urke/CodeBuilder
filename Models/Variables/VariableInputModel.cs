namespace CodeBuilder.Models
{
    public class VariableInputModel
    {

        public VariableModel? Calculation { get; set; }

        public VariableModel? Input { get; set; }

        public Guid CalculationId { get; set; }

        public Guid InputId { get; set; }

    }
}
