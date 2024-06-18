using CodeBuilder.Models;

namespace CodeBuilder.Parsers
{
    public class MathVariableExtractor
    {
        // Methods - public 

        public static bool Extract(string calculation, out List<VariableModel> variables)
        {
            variables = new List<VariableModel>();

            // Get rid of any excess space

            calculation = calculation.Trim();

            // Replace operators with a space

            foreach (var symbol in MathJax.Operators)
            {
                calculation = calculation.Replace(symbol.Code, " ");
            }

            // Replace functional operators with a space

            foreach (var symbol in MathJax.Functional)
            {
                calculation = calculation.Replace(symbol.Code, " ");
            }

            // Replace basic symbols with a space

            var basic = new List<string>
            {
                "+", "-", "/", "=", "^", "\\left", "\\right"
            };

            // Create a couple for all corresponding parentheses

            foreach (var couple in MathJax.ParentheseCouples)
            {
                // Split on back-to-back parentheses i.e. }{, )(

                basic.Add(couple.Left.Code + couple.Right.Code);

                // Split on face-to-face parentheses i.e. {}, ()

                basic.Add(couple.Right.Code + couple.Left.Code);
            }

            // Replace all basic symbols with a space

            foreach (var symbol in basic)
            {
                calculation = calculation.Replace(symbol, " ");
            }

            // Now all we should be left with is potential variables

            var potential = calculation.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Create a collection for the variable (MathJax) symbols

            var symbols = new List<string>();

            foreach (var p in potential)
            {
                //
                // Formatting 
                //

                string symbol = p.Trim();

                // Check for known mathematical constants

                foreach (var constant in new[] { "\\pi", "\\sin", "\\cos", "\\tan", "\\arcsin", "\\arccos", "\\arctan" })
                {
                    symbol = symbol.Replace(constant, "");
                }

                // Ronseal

                RemoveLeadingNumbersAndParentheses(ref symbol);

                // 
                // Validity
                //

                if (symbols.Contains(symbol))
                    continue;

                if (symbol == ",")
                    continue;

                if (double.TryParse(symbol, out double _))
                    continue;

                if (string.IsNullOrWhiteSpace(symbol))
                    continue;

                if (MathJax.Parentheses.Any(x => x.Code == symbol))
                    continue;

                symbols.Add(symbol);
            }

            foreach (var symbol in symbols)
            {
                variables.Add(new VariableModel()
                {
                    Math = symbol,
                    Code = GetSymbolCode(symbol)
                });
            }

            return variables.Count > 0;
        }

        // Methods - private

        private static void RemoveLeadingNumbersAndParentheses(ref string symbol)
        {
            string formatted = new(symbol);

            // Remove leading numbers from variables

            while (formatted.Length > 0 && (char.IsNumber(formatted[0]) || formatted[0] == '.'))
            {
                formatted = formatted[1..];
            }

            // Check parentheses match 

            foreach (var couple in MathJax.ParentheseCouples)
            {
                ReplaceDoubles(couple.Right.Code, ref formatted);

                ReplaceDoubles(couple.Left.Code, ref formatted);

                if (formatted.StartsWith(couple.Left.Code))
                    formatted = formatted[1..];

                if (formatted.StartsWith(couple.Right.Code))
                    formatted = formatted[1..];

                if (formatted.EndsWith(couple.Right.Code) && !formatted.Contains(couple.Left.Code))
                    formatted = formatted[..^1];

                if (formatted.EndsWith(couple.Left.Code) && !formatted.Contains(couple.Right.Code))
                    formatted = formatted[..^1];
            }

            static void ReplaceDoubles(string symbolCode, ref string symbol)
            {
                string doubled = symbolCode + symbolCode;
                do
                {
                    symbol = symbol.Replace(doubled, symbolCode);
                }
                while (symbol.Contains(doubled));
            }

            formatted = formatted.Trim();

            if (formatted != symbol)
            {
                symbol = formatted;
                RemoveLeadingNumbersAndParentheses(ref symbol);
            }
        }

        private static string GetSymbolCode(string symbol)
        {
            string code = symbol;
            code = code.Replace("\\", "");
            code = code.Replace("/", "");
            code = code.Replace("{", "");
            code = code.Replace("}", "");
            code = code.Replace(",", "");
            return code.Trim().ToLower();
        }
    }
}
