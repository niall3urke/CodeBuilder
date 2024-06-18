namespace CodeBuilder.Models
{
    public class VariableGroupVariableModel
    {

        public VariableGroupModel? Group { get; set; }

        public VariableModel? Variable { get; set; }

        public Guid VariableId { get; set; }

        public Guid GroupId { get; set; }

    }
}
