using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBuilder.Models.Briefs
{
    public class BriefCheckModel : IEntity, IIndexable
    {
        public BriefModel? Brief { get; set; }

        public CheckModel? Check { get; set; }

        public Guid BriefId { get; set; }

        public Guid CheckId { get; set; }

        public Guid Id { get; set; }

        public string Name => Check?.Name ?? "";

        public string Desc => Check?.Desc ?? "";

        public int Index { get; set; }
    }
}
