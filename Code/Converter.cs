using CodeBuilder.Models;
using CodeBuilder.Models.Briefs;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Code
{
    public class Converter
    {

        // =============
        // ===== Fields
        // =============

        private static List<VariableModel> _outputs;

        private static List<VariableModel> _inputs;

        // =============
        // ===== Briefs
        // =============

        public static string Brief(BriefModel brief, List<BriefCheckModel> checks)
        {
            string content = Properties.Resources.BriefClassTemplate;

            // Get all outputs

            if (brief == null)
                return "";

            //_outputs = GetPublicVariables(check.Block);

            //_inputs = GetPrivateVariables(_outputs);

            // Get the calculate content

            //content = content.Replace("[Properties]", GetProperties(_outputs));

            content = content.Replace("[Fields]", GetFields(checks));

            content = content.Replace("[Inputs]", GetInputs(checks));

            content = content.Replace("[InitializeFields]", GetInitializeFields(checks));

            //content = content.Replace("[Output]", GetOutput(_outputs));

            //content = content.Replace("[Calculate]", GetCalculate(check));

            //// Simple conversions

            content = content.Replace("[Name]", Name(brief));

            //content = content.Replace("[Title]", check.Name);

            return content;
        }

        private static string GetFields(IEnumerable<BriefCheckModel> checks)
        {
            string content = "";

            foreach (var check in checks)
            {
                string className = Strip(check?.Check?.Name ?? "");

                if (!string.IsNullOrWhiteSpace(className))
                    content += $"private {className} {FirstCharToLower(className)}; {Environment.NewLine}";
            }

            content += Environment.NewLine;

            return content.Length > 1 ? content[..^2] : content;
        }

        private static string GetInputs(IEnumerable<BriefCheckModel> checks)
        {
            string content = "";

            foreach (var check in checks)
            {
                string className = Strip(check?.Check?.Name ?? "");

                if (!string.IsNullOrWhiteSpace(className))
                    content += $"{className} {FirstCharToLower(className)}, ";
            }

            return content.Length > 1 ? content[..^2] : content;
        }

        private static string GetInitializeFields(IEnumerable<BriefCheckModel> checks)
        {
            string content = "";

            foreach (var check in checks)
            {
                string code = Strip(FirstCharToLower(check?.Check?.Name ?? ""));

                if (!string.IsNullOrWhiteSpace(code))
                {
                    content += $"this.{code} = {code}; {Environment.NewLine}";
                }
            }

            content += Environment.NewLine;

            return content;
        }

        private static string FirstCharToLower(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && char.IsUpper(value[0]))
                return value.Length == 1 ? char.ToLower(value[0]).ToString() : char.ToLower(value[0]) + value[1..];

            return value;
        }

        // =============
        // ===== Checks
        // =============

        public static string Check(CheckModel check)
        {
            string content = Properties.Resources.CheckClassTemplate;

            // Get all outputs

            if (check.Block == null)
                return "";

            _outputs = GetPublicVariables(check.Block);

            _inputs = GetPrivateVariables(_outputs);

            // Get the calculate content

            content = content.Replace("[Properties]", GetProperties(_outputs));

            content = content.Replace("[Fields]", GetFields(_inputs));

            content = content.Replace("[Inputs]", GetInputs(_inputs));

            content = content.Replace("[InitializeFields]", GetInitializeFields(_inputs));

            content = content.Replace("[Output]", GetOutput(_outputs));

            content = content.Replace("[Calculate]", GetCalculate(check));

            // Simple conversions

            content = content.Replace("[Name]", Name(check));

            content = content.Replace("[Title]", check.Name);

            return content;
        }

        private static List<VariableModel> GetPrivateVariables(List<VariableModel> outputs)
        {
            var inputs = new List<VariableModel>();

            foreach (var output in outputs)
            {
                foreach (var variable in output.Variables)
                {
                    if (variable.Input == null)
                        continue;

                    if (outputs.Contains(variable.Input))
                        continue;

                    if (inputs.Contains(variable.Input))
                        continue;

                    inputs.Add(variable.Input);
                }
            }

            return inputs;
        }

        // Get: [Fields]

        private static string GetFields(List<VariableModel> inputs)
        {
            string content = "";

            foreach (var variable in inputs)
            {
                content += Field(variable) + Environment.NewLine;
            }

            return content;
        }

        // Get: [Inputs]

        private static string GetInputs(List<VariableModel> inputs)
        {
            string content = "";

            foreach (var variable in inputs)
            {
                content += $"{Type(variable.Storage)} {Strip(variable.Code)}, ";
            }

            return content.Length > 1 ? content[..^2] : content;
        }

        // Get: [InitializeFields]

        private static string GetInitializeFields(IEnumerable<ICodable> inputs)
        {
            string content = "";

            foreach (var variable in inputs)
            {
                string code = Strip(variable.Code);
                content += $"this.{code} = {code}; {Environment.NewLine}";
            }

            return content;
        }

        // Get: [GetOuput]

        private static string GetOutput(List<VariableModel> calculations)
        {
            string content = "";

            foreach (var calculation in calculations)
            {
                content += $"new Row({GetPropertyName(calculation)}),{Environment.NewLine}";
            }

            return content;
        }

        // Get: [Properties] 

        private static string GetProperties(List<VariableModel> calculations)
        {
            string content = "";

            foreach (var calculation in calculations)
            {
                content += $"public ICalculation {GetPropertyName(calculation)};{Environment.NewLine}";
            }

            return content;
        }

        private static List<VariableModel> GetPublicVariables(BlockModel block)
        {
            var calculations = new List<VariableModel>();

            foreach (var child in block.Children)
            {
                if (child.Calculation != null)
                {
                    calculations.Add(child.Calculation);
                }
                calculations.AddRange(GetPublicVariables(child));
            }

            return calculations;
        }

        // Get: [Calculate]

        private static string GetCalculate(CheckModel check)
        {
            if (check.Block == null)
                return "";

            return GetBlockCalculations(check.Block);
        }

        private static string GetBlockCalculations(BlockModel block)
        {
            string content = "";

            foreach (var child in block.Children)
            {
                if (child.Calculation != null)
                {
                    content += GetCalculationInitialization(child.Calculation);
                }

                if (child.Condition == null)
                {
                    content += GetBlockCalculations(child);
                }
                else
                {
                    content += "if (";

                    content += GetBlockCondition(child.Condition);

                    content += ")" + Environment.NewLine;

                    content += "{" + Environment.NewLine;

                    content += GetBlockCalculations(child);

                    content += "}" + Environment.NewLine;
                }
            }

            return content;
        }

        private static string GetBlockCondition(ConditionModel condition)
        {
            string content = "";

            // LHS 

            if (condition.Lhs != null)
            {
                content += $"{GetPropertyName(condition.Lhs)}.Value";
            }
            else if (!string.IsNullOrWhiteSpace(condition.LhsValue))
            {
                content += condition.LhsValue;
            }

            // Operation

            if (condition.OperationType == OperationType.IsEqualTo)
            {
                content += " == ";
            }
            else if (condition.OperationType == OperationType.IsLessThan)
            {
                content += " < ";
            }
            else if (condition.OperationType == OperationType.IsLessThanOrEqualTo)
            {
                content += " <= ";
            }
            else if (condition.OperationType == OperationType.IsGreaterThanOrEqualTo)
            {
                content += " > ";
            }
            else if (condition.OperationType == OperationType.IsGreaterThan)
            {
                content += " > ";
            }
            else if (condition.OperationType == OperationType.Contains)
            {
                content += ".Contains(";
            }

            // RHS

            if (condition.Rhs != null)
            {
                content += $"{GetPropertyName(condition.Rhs)}.Value";
            }
            else if (!string.IsNullOrWhiteSpace(condition.RhsValue))
            {
                content += condition.RhsValue;
            }

            // Check closing for .Contains

            if (condition.OperationType == OperationType.Contains)
            {
                content += ")";
            }

            return content;
        }

        private static string GetCalculationInitialization(VariableModel calculation)
        {
            if (calculation.Variables == null)
            {
                return "";
            }

            var variables = calculation.Variables.Select(x => x.Input).Cast<IVariable>();

            string parameters = "";

            if (variables != null)
            {
                parameters = GetCalculationParameters(variables);
            }

            return $"{GetPropertyName(calculation)} = new {Name(calculation)}({parameters}); {Environment.NewLine}";
        }

        private static string GetCalculationParameters(IEnumerable<IVariable>? variables)
        {
            string parameters = "";

            foreach (var variable in variables)
            {
                if (variable != null)
                {
                    if (_inputs.Contains(variable))
                    {
                        parameters += $"{Strip(variable.Code)}, ";
                    }
                    else
                    {
                        parameters += $"{GetPropertyName(variable)}.Value, ";
                    }
                }
            }

            return parameters.Length > 1 ? parameters[..^2] : parameters;
        }

        private static string GetPropertyName(IVariable variable)
        {
            if (string.IsNullOrWhiteSpace(variable.Code))
                return "";

            string code = Strip(variable.Code);

            return (char.ToUpper(code.First()) + code[1..]).Trim();
        }

        // ==================
        // ===== Calculation
        // ==================

        public static string Calculation(VariableModel model, List<VariableModel> variables)
        {
            string content = Properties.Resources.CalculationClassTemplate;

            // Simple conversions

            content = content.Replace("[Name]", Name(model));

            content = content.Replace("[Code]", $"{model.Code};");

            content = content.Replace("[CodeCalculation]", $"{Strip(model.Code)} = {model.CodeCalculation};");

            content = content.Replace("[Description]", $"\"{model.Desc}\";");

            content = content.Replace("[Result]", $"$\"{{{Strip(model.Code)}}}:0.##\";");

            content = content.Replace("[ResultUnits]", $"@\"\\,\\mathrm{{{model.Unit.GetDescription()}}}\";");

            content = content.Replace("[Clause]", $"\"{model.Reference}\";");

            content = content.Replace("[Units]", $"Units.{model.Unit};");

            content = content.Replace("[Equation]", $"@\"{model.MathCalculation}\";");

            // Build fields

            string fields = "";

            foreach (var variable in variables)
            {
                fields += Field(variable) + Environment.NewLine;
            }

            fields += Field(model) + Environment.NewLine;

            content = content.Replace("[Fields]", fields);

            // Build input parameters

            string parameters = "";

            foreach (var variable in variables)
            {
                parameters += $"{Type(variable.Storage)} {variable.Code}, ";
            }

            if (parameters.Length > 2)
            {
                parameters = parameters[..^2];
            }

            content = content.Replace("[Variables]", parameters);

            // Build field initialization

            string initializeFields = "";

            foreach (var variable in variables)
            {
                initializeFields += $"this.{variable.Code} = {variable.Code}; {Environment.NewLine}";
            }

            content = content.Replace("[InitializeFields]", initializeFields);

            // Build equation property

            if (!string.IsNullOrWhiteSpace(model.MathCalculation))
            {
                string calculation = model.MathCalculation;

                calculation = calculation.Replace(@"\", @"\\");

                calculation = calculation.Replace("{", "{{");

                calculation = calculation.Replace("}", "}}");

                foreach (var variable in variables)
                {
                    if (!string.IsNullOrWhiteSpace(variable.Math))
                    {
                        calculation = calculation.Replace(variable.Math, $"{{{variable.Code}}}:0.##");
                    }
                }

                calculation = $"$\"{calculation}\";";

                content = content.Replace("[MathCalculation]", calculation);
            }

            return content;
        }

        public static string Name(IModelBase model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                return "";

            var words = model.Name.Split(
                new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = char.ToUpper(words[i].First()) + words[i][1..];
            }

            return string.Join("", words);
        }

        public static string Field(IVariable model)
        {
            return $"private {Type(model.Storage)} {Strip(model.Code)};";
        }

        public static string Type(StorageType st)
        {
            if (st == StorageType.Real)
                return "double";

            if (st == StorageType.Integer)
                return "int";

            if (st == StorageType.Boolean)
                return "bool";

            return "string";
        }

        private static string Strip(string text)
        {
            string code = "";

            if (string.IsNullOrWhiteSpace(text))
                return code;

            code = text;
            code = code.Replace("\\", "");
            code = code.Replace(",", "");
            code = code.Replace("{", "");
            code = code.Replace("}", "");
            code = code.Replace(" ", "");

            return code;

        }

    }
}