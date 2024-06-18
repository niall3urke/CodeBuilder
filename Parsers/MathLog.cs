using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.Parsers
{
    internal class MathLog
    {
        // Fields

        private readonly List<MathNode> _processed;

        // Constructors

        public MathLog()
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

                if (current.Content == "\\log")
                {
                    if (_processed.Contains(current))
                        continue;

                    // TODO parse bases [ ]
                    if (HasNext(nodes, i) && SetBase(ref current, nodes[i + 1]))
                        nodes.RemoveAt(i + 1);

                    if (HasNext(nodes, i - 1) && SetOperand(ref current, nodes[i]))
                        nodes.RemoveAt(i + 1);


                    _processed.Add(current);
                }
            }
        }

        // Methods - private

        private bool HasNext(List<MathNode> nodes, int i) => nodes.Count > i + 1;

        private bool SetBase(ref MathNode current, MathNode next)
        {
            bool removeNext = false;

            if (IsGroup(next))
            {
                // Format: \log_{...}
                var values = new List<MathNode>(next.Children);
                current.Children.AddRange(values);
                next.Children.Clear();
            }
            else
            {
                // Format: \log_x
                next.Content = next.Content.Replace("_", "");
                current.Children.Add(new() { Content = "{", Parent = current });
                current.Children.Add(next);
                current.Children.Add(new() { Content = "}", Parent = current });

                removeNext = true;
            }

            return removeNext;
        }

        private bool SetOperand(ref MathNode current, MathNode next, bool checkChildren = false)
        {
            if (checkChildren && current.Children.Count > 0)
            {
                // If the sqrt operator already has children, it means that the power 
                // was set automatically during the parsing of the calculation string. 
                return false;
            }

            bool removeNext = false;

            if (IsGroup(next))
            {
                // Format: \log {...}
                var values = new List<MathNode>(next.Children);
                current.Children.AddRange(values);
                next.Children.Clear();
            }
            else
            {
                // Format: \log x
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
