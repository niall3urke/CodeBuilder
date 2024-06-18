using CodeBuilder.Models;

namespace CodeBuilder.Parsers
{

    /// <summary>
    /// Converts a calculation string to MathJax
    /// </summary>
    internal class MathParser
    {

        public static string Parse(string calculation, List<VariableModel> variables)
        {
            // Get rid of items that are purely for formatting MathJax display

            foreach (var symbol in new string[] { "\\left", "\\right" })
            {
                calculation = calculation.Replace(symbol, " ");
            }

            // Replace variable symbols with their code counterpart

            foreach (var variable in variables)
            {
                calculation = calculation.Replace(variable.Math, variable.Code);
            }

            // Add spaces around all operators...

            foreach (var symbol in MathJax.Operators)
            {
                calculation = calculation.Replace(symbol.Code, $" {symbol.Code} ");
            }

            // ... and functional operators

            foreach (var symbol in MathJax.Functional)
            {
                calculation = calculation.Replace(symbol.Code, $" {symbol.Code} ");
            }

            // ... and parentheses

            foreach (var symbol in MathJax.Parentheses)
            {
                calculation = calculation.Replace(symbol.Code, $" {symbol.Code} ");
            }

            // ... and basic operators

            foreach (var symbol in new string[] { "+", "-", "/", "=", "^" })
            {
                calculation = calculation.Replace(symbol, $" {symbol} ");
            }

            // Now split the calculation up into a managable array

            var parts = calculation.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Account for awkward groups like \\vert which have to clear start and end

            parts = PreformatParts(parts);

            // Create nodes and identify relationships

            var root = GetTree(parts);

            // Now we need to pick out special cases

            new MathNext("\\sinh").Check(ref root);

            new MathNext("\\cosh").Check(ref root);

            new MathNext("\\tanh").Check(ref root);

            new MathNext("\\sin").Check(ref root);

            new MathNext("\\cos").Check(ref root);

            new MathNext("\\tan").Check(ref root);

            new MathSqrt().Check(ref root);

            new MathAbs().Check(ref root);

            new MathLog().Check(ref root);

            new MathPow().Check(ref root);

            // Form the calculation string

            string calc = "";

            foreach (var child in root.Children)
            {
                BuildCalculation(child, ref calc);
            }

            CheckFormatOfCalculationString(ref calc);

            return calc;
        }

        private static string[] PreformatParts(string[] parts)
        {
            var list = parts.ToList();

            int absOpenIndex = -1;
            int absShutIndex = -1;

            for (int i = list.Count - 1; i > -1; i--)
            {
                if (list[i] == "\\vert")
                {
                    if (absShutIndex > -1)
                    {
                        absOpenIndex = i;
                    }
                    else
                    {
                        absShutIndex = i;
                    }
                }

                if (absShutIndex > -1 && absOpenIndex > -1)
                {
                    list[absShutIndex] = "}";
                    list[absOpenIndex] = "{";
                    list.Insert(absOpenIndex, "\\vert");

                    absShutIndex = -1;
                    absOpenIndex = -1;
                }

                if (list[i] == "(")
                    list[i] = "{";

                if (list[i] == ")")
                    list[i] = "}";

                if (list[i] == "[")
                    list[i] = "{";

                if (list[i] == "]")
                    list[i] = "}";
            }

            return list.ToArray();
        }

        private static MathNode GetTree(string[] parts)
        {
            var root = new MathNode();

            bool moveUp = false;

            var parent = root;

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "{" && parent.Children.Count > 0)
                {
                    parent = parent.Children.Last();
                }

                else if (parts[i] == "}")
                {
                    moveUp = true;
                }

                parent.Children.Add(new() { Content = parts[i], Parent = parent });

                if (moveUp)
                {
                    moveUp = false;

                    if (parent.Parent != null)
                        parent = parent.Parent;
                }
            }

            return root;
        }

        private static void CheckFormatOfCalculationString(ref string calc)
        {
            // Replace spaces after brackets
            calc = calc.Replace("( ", "(");

            // Replace spaces before brackets
            calc = calc.Replace(" )", ")");

            // Replace duplicate white space
            calc = calc.Replace("  ", " ");

            // Remove whitespace at the start and end of the calculation
            calc = calc.Trim();
        }

        private static void BuildCalculation(MathNode node, ref string calc)
        {
            switch (node.Content)
            {
                case "\\frac":
                    {
                        var groups = GetGroups(node.Children);

                        if (groups.Count == 2)
                        {
                            calc += GetSpace(calc) + "(";

                            var top = groups[0];
                            var btm = groups[1];

                            foreach (var child in top)
                            {
                                BuildCalculation(child, ref calc);
                            }

                            calc += GetSpace(calc) + "/";

                            foreach (var child in btm)
                            {
                                BuildCalculation(child, ref calc);
                            }

                            calc += ")";
                        }

                        break;
                    }

                case "\\times":
                    {
                        calc += GetSpace(calc) + "*";
                        break;
                    }

                case "e":
                    {
                        calc += "Math.E";
                        break;
                    }

                case "\\vert":
                    {
                        var groups = GetGroups(node.Children);
                        if (groups.Count > 0)
                        {
                            calc += GetSpace(calc) + "Math.Abs(";
                            foreach (var child in groups[0])
                            {
                                BuildCalculation(child, ref calc);
                            }
                            calc += ")";
                        }
                        break;
                    }

                case "\\sin":
                    {
                        var groups = GetGroups(node.Children);
                        if (groups.Count > 0)
                        {
                            calc += GetSpace(calc) + "Math.Sin(";
                            foreach (var child in groups[0])
                            {
                                BuildCalculation(child, ref calc);
                            }
                            calc += ")";
                        }
                        break;
                    }

                case "\\cos":
                    {
                        var groups = GetGroups(node.Children);
                        if (groups.Count > 0)
                        {
                            calc += GetSpace(calc) + "Math.Cos(";
                            foreach (var child in groups[0])
                            {
                                BuildCalculation(child, ref calc);
                            }
                            calc += ")";
                        }
                        break;
                    }

                case "\\tan":
                    {
                        var groups = GetGroups(node.Children);
                        if (groups.Count > 0)
                        {
                            calc += GetSpace(calc) + "Math.Tan(";
                            foreach (var child in groups[0])
                            {
                                BuildCalculation(child, ref calc);
                            }
                            calc += ")";
                        }
                        break;
                    }

                case "\\sinh":
                    {
                        var groups = GetGroups(node.Children);
                        if (groups.Count > 0)
                        {
                            calc += GetSpace(calc) + "Math.Sinh(";
                            foreach (var child in groups[0])
                            {
                                BuildCalculation(child, ref calc);
                            }
                            calc += ")";
                        }
                        break;
                    }

                case "\\cosh":
                    {
                        var groups = GetGroups(node.Children);
                        if (groups.Count > 0)
                        {
                            calc += GetSpace(calc) + "Math.Cosh(";
                            foreach (var child in groups[0])
                            {
                                BuildCalculation(child, ref calc);
                            }
                            calc += ")";
                        }
                        break;
                    }

                case "\\tanh":
                    {
                        var groups = GetGroups(node.Children);
                        if (groups.Count > 0)
                        {
                            calc += GetSpace(calc) + "Math.Tanh(";
                            foreach (var child in groups[0])
                            {
                                BuildCalculation(child, ref calc);
                            }
                            calc += ")";
                        }
                        break;
                    }

                case "\\log":
                    {
                        var groups = GetGroups(node.Children);

                        if (groups.Count > 1)
                        {
                            // Operand and base specified
                            calc += GetSpace(calc) + "Math.Log(";

                            foreach (var child in groups[1])
                            {
                                BuildCalculation(child, ref calc);
                            }
                            calc += ", ";
                            foreach (var child in groups[0])
                            {
                                BuildCalculation(child, ref calc);
                            }
                            calc += ")";
                        }

                        else if (groups.Count > 0)
                        {
                            calc += GetSpace(calc) + "Math.Log(";
                            foreach (var child in groups[0])
                            {
                                BuildCalculation(child, ref calc);
                            }
                            calc += ")";
                        }
                        break;
                    }

                case "\\sqrt":
                    {
                        var groups = GetGroups(node.Children);
                        if (groups.Count > 1)
                        {
                            // Power and operand
                        }
                        else
                        {
                            // Square root - operand only
                            calc += GetSpace(calc) + "Math.Sqrt(";
                            foreach (var child in groups[0])
                            {
                                BuildCalculation(child, ref calc);
                            }
                            calc += ")";
                        }
                        break;
                    }

                case "^":
                    {
                        var groups = GetGroups(node.Children);
                        if (groups.Count > 0)
                        {
                            calc += GetSpace(calc) + "Math.Pow(";
                            foreach (var child in groups[0])
                            {
                                BuildCalculation(child, ref calc);
                            }
                            calc += ", ";

                            if (groups.Count > 1)
                            {
                                foreach (var child in groups[1])
                                {
                                    BuildCalculation(child, ref calc);
                                }
                            }
                            calc += ")";
                        }
                        break;
                    }

                default:
                    {
                        calc += GetSpace(calc) + node.Content;
                        break;
                    }

            }

            string GetSpace(string calc) => calc.EndsWith(" ") ? "" : " ";

            List<List<MathNode>> GetGroups(List<MathNode> nodes)
            {
                var groups = new List<List<MathNode>>();

                var group = new List<MathNode>();

                bool createNewGroup = false;

                foreach (var child in nodes)
                {
                    if (child.Content == "{")
                    {
                        createNewGroup = true;
                    }
                    else if (child.Content == "}")
                    {
                        groups.Add(group);
                    }
                    else
                    {
                        if (createNewGroup)
                        {
                            group = new List<MathNode>();
                            createNewGroup = false;
                        }
                        group.Add(child);
                    }
                }
                return groups;
            }

        }


        // ======================
        // ===== Refactored code
        // ======================

        //private void OldSlowApproachToNestedList(string[] parts)
        //{
        //    var nodes = new List<Node>();

        //    bool reduceLevel = false;

        //    int level = 0;

        //    foreach (var part in parts)
        //    {
        //        if (part == "{")
        //        {
        //            level++;
        //        }
        //        else if (part == "}")
        //        {
        //            reduceLevel = true;
        //        }

        //        nodes.Add(new() { Content = part, Level = level });

        //        if (reduceLevel)
        //        {
        //            reduceLevel = false;
        //            level--;
        //        }
        //    }

        //    // Convert a flat list into a nested parent/child list

        //    while (GetLowestLevel(nodes) > 0)
        //    {
        //        int lowestLevel = GetLowestLevel(nodes);

        //        while (lowestLevel == GetLowestLevel(nodes))
        //        {
        //            for (int i = nodes.Count - 1; i > 0; i--)
        //            {
        //                if (nodes[i].Level == lowestLevel)
        //                {
        //                    if (nodes[i - 1].Level == lowestLevel - 1)
        //                    {
        //                        nodes[i - 1].Children.Add(nodes[i]);
        //                        nodes.RemoveAt(i);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    int GetLowestLevel(List<Node> nodes)
        //    {
        //        int lowestLevel = -1;

        //        for (int i = 0; i < nodes.Count - 1; i++)
        //        {
        //            if (nodes[i].Level > lowestLevel)
        //                lowestLevel = nodes[i].Level;
        //        }

        //        return lowestLevel;
        //    }
        //}


    }
}



