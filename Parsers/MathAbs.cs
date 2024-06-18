using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.Parsers
{
    internal class MathAbs
    {

        // Fields

        private readonly List<MathNode> _processed;

        // Constructors

        public MathAbs()
        {
            _processed = new List<MathNode>();
        }

        // Methods - public 

        public void Check(ref MathNode node)
        {
            var nodes = node.Children;

            for (int i = 0; i < nodes.Count; i++)
            {
                var current = nodes[i];

                if (current.Content == "\\vert")
                {
                    if (_processed.Contains(current))
                        continue;

                    if (HasNext(nodes, i) && SetOperand(ref current, nodes[i + 1]))
                        nodes.RemoveAt(i + 1);

                    _processed.Add(current);
                }

                Check(ref current);
            }
        }

        // Methods - private

        private bool HasNext(List<MathNode> nodes, int i) => nodes.Count > i + 1;

        private bool SetOperand(ref MathNode current, MathNode next)
        {
            if (current.Children.Count > 0)
            {
                // If the vert operator already has children, it means that the value 
                // was set automatically during the parsing of the calculation string. 
                return false;
            }

            bool removeNext = false;

            if (IsGroup(next))
            {
                // Format: \vert{...}
                var values = new List<MathNode>(next.Children);
                current.Children.AddRange(values);
                next.Children.Clear();
            }
            else
            {
                // Format: \vert x 
                current.Children.Add(new() { Content = "{", Parent = current });
                current.Children.Add(next);
                current.Children.Add(new() { Content = "}", Parent = current });
                removeNext = true;
            }

            return removeNext;
        }

        private static bool IsGroup(MathNode node) => node.Content == "{" && node.Children.Count > 0;

    }
}
