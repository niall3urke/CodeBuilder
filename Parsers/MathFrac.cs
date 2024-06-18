using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.Parsers
{
    internal class MathFrac : MathNode
    {

        public List<MathNode> Top { get; set; }

        public List<MathNode> Btm { get; set; }

        public MathFrac()
        {
            Top = new List<MathNode>();

            Btm = new List<MathNode>();
        }

    }
}
