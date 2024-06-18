namespace CodeBuilder.Models
{
    public class CheckModel : ICodable
    {

        // Properties 

        public StandardModel? Standard { get; set; }
        public BlockModel? Block { get; set; }
        public StateType State { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Guid Id { get; set; }

        public string? Reference { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Desc { get; set; }

        // Constructors 

        public CheckModel()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        // Methods

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

        public string GetName(int limit = 100)
        {
            string name = "";

            if (!string.IsNullOrWhiteSpace(Name))
            {
                name += Name.Trim();
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
            string standard = "";

            if (Standard != null)
            {
                standard = Standard.StandardType.ToString() ?? "";
            }

            if (standard.Length > limit)
            {
                standard = standard[..(limit - 3)] + "...";
            }

            return standard;
        }


    }
}
