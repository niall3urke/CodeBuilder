namespace CodeBuilder.Models
{
    public class StandardModel : IStandardized, IBucket<StandardModel>
    {

        // Properties

        public List<StandardModel> Children { get; set; }

        public StandardType StandardType { get; set; }
        public StandardModel? Standard { get; set; }
        public StateType State { get; set; }

        public DateTime Published { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Guid Id { get; set; }

        public string? Reference { get; set; }
        public string? Link { get; set; }
        public string? Isbn { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Desc { get; set; }

        // Constructors 

        public StandardModel()
        {
            Children = new List<StandardModel>();
            Published = DateTime.Now;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        // Methods

        public string GetName(int limit = 100)
        {
            string name = "";

            if (!string.IsNullOrWhiteSpace(Code))
            {
                name += Code.Trim();
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                name += " - " + Name.Trim();
            }

            if (!string.IsNullOrWhiteSpace(Desc))
            {
                name += " - " + Desc.Trim();
            }

            if (name.Length > limit)
            {
                name = name[..(limit - 3)] + "...";
            }

            return name;
        }

        public string GetDesc(int limit = 100)
        {
            string desc = "";

            if (!string.IsNullOrWhiteSpace(Desc))
            {
                desc = Desc;
            }

            if (desc.Length > 100)
            {
                desc = desc[..97] + "...";
            }

            return desc;
        }

        public string GetReference(int limit = 100)
        {
            string reference = "";

            if (!string.IsNullOrWhiteSpace(Reference))
            {
                reference += Reference.Trim();
            }

            if (reference.Length > limit)
            {
                reference = reference[..(limit - 3)] + "...";
            }

            return reference;
        }

        public string GetStandard(int limit = 100)
        {
            string standard = StandardType.ToString() ?? "";

            if (standard.Length > limit)
            {
                standard = standard[..(limit - 3)] + "...";
            }

            return standard;
        }


    }
}
