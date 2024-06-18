namespace CodeBuilder.Parsers
{
    public class MathNode
    {
        public string Content { get; set; }

        public int Level { get; set; }

        public List<MathNode> Children { get; set; }

        public MathNode Parent { get; set; }

        public MathNode()
        {
            Children = new List<MathNode>();
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
