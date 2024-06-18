namespace CodeBuilder.Models
{
    public class TableDataModel : IEntity
    {

        // Properties

        public TableModel? Table { get; set; }

        public string? Value { get; set; }

        public Guid TableId { get; set; }

        public Guid RowId { get; set; }

        public Guid ColId { get; set; }

        public Guid Id { get; set; }

        // Constructors 

        public TableDataModel() { }

    }
}
