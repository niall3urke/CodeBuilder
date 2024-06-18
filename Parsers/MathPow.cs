using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.Parsers
{
    internal class MathPow
    {

        // Fields

        private readonly List<MathNode> _processed;

        // Constructors

        public MathPow()
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

                if (current.Content == "^")
                {
                    if (_processed.Contains(current))
                        continue;

                    if (HasNext(nodes, i) && SetPower(ref current, nodes[i + 1]))
                        nodes.RemoveAt(i + 1);

                    if (HasPrev(nodes, i) && SetOperand(ref current, nodes[i - 1]))
                        nodes.RemoveAt(i - 1);

                    _processed.Add(current);
                }

                Check(ref current);
            }
        }

        // Methods - private

        private bool HasNext(List<MathNode> nodes, int i) => nodes.Count > i + 1;

        private bool HasPrev(List<MathNode> nodes, int i) => nodes.Count > i - 1;

        private bool SetOperand(ref MathNode current, MathNode prev)
        {
            bool removePrev = false;

            if (IsGroup(prev))
            {
                // Format: {...}^
                var values = new List<MathNode>(prev.Children);
                for (int j = 0; j < values.Count; j++)
                {
                    current.Children.Insert(j, values[j]);
                }
                prev.Children.Clear();
            }
            else
            {
                // Format: x^
                current.Children.Insert(0, new() { Content = "{" });
                current.Children.Insert(1, prev);
                current.Children.Insert(2, new() { Content = "}" });
                removePrev = true;
            }

            return removePrev;
        }

        private bool SetPower(ref MathNode current, MathNode next)
        {
            if (current.Children.Count > 0)
            {
                // If the ^ operator already has children, it means that the power was
                // set automatically during the parsing of the calculation string. 
                return false;
            }

            bool removeNext = false;

            if (IsGroup(next))
            {
                // Format: ^{...}
                var values = new List<MathNode>(next.Children);
                current.Children.AddRange(values);
                next.Children.Clear();
            }
            else if (next.Content != "}")
            {
                // Format: ^x
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
