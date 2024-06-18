using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.Parsers
{
    internal class MathJax
    {

        // Fields

        private static Symbol[] _parentheses;

        private static Symbol[] _logical;

        private static Symbol[] _operators;

        private static Symbol[] _sets;

        private static Symbol[] _arrows;

        private static Symbol[] _special;

        private static Symbol[] _trigonometric;

        private static Symbol[] _functional;

        private static Symbol[] _greek;

        private static Symbol[] _greekUpper;

        private static Symbol[] _font;

        private static Symbol[] _spaces;

        private static Symbol[] _accents;

        // Properties

        public static Symbol[] Parentheses
        {
            get
            {
                if (_parentheses == null)
                {
                    _parentheses = new Symbol[]
                    {
                        BracketCurvedLeft,
                        BracketCurvedRight,
                        BracketBraceLeft,
                        BracketBraceRight,
                        BracketStraightLeft,
                        BracketStraightRight,
                        BracketAngleLeft,
                        BracketAngleRight,
                        BracketCeilLeft,
                        BracketCeilRight,
                        BracketFloorLeft,
                        BracketFloorRight,
                        Absolute,
                        Norm,
                        ScaleLeft,
                        ScaleRight,
                    };
                }

                return _parentheses;
            }
        }

        public static Symbol[] Logical
        {
            get
            {
                if (_logical == null)
                {
                    _logical = new Symbol[]
                    {
                        LessThan,
                        MoreThan,
                        LessThanOrEqualTo,
                        MoreThanOrEqualTo,
                        NotEqualTo,
                        And,
                        Or,
                        Not,
                        ForAll,
                        Exists,
                        Top,
                        Bottom,
                        Provable,
                        Entails,
                        ApproxEqual,
                        SimilarlyEqual,
                        Similar,
                        Congruent,
                        Equivalent,
                        Order,
                        SubgroupNormal,
                        Therefore,
                    };
                }
                return _logical;
            }
        }

        public static Symbol[] Operators
        {
            get
            {
                if (_operators == null)
                {
                    _operators = new Symbol[]
                    {
                        Times,
                        Divide,
                        Fraction,
                        PlusOrMinus,
                        MinusOrPlus,
                        Dot,
                        Star,
                        Asterisk,
                        ExclusiveOr,
                        Circle,
                        Bullet
                    };
                }

                return _operators;
            }
        }

        public static Symbol[] Functional
        {
            get
            {
                if (_functional == null)
                {
                    _functional = new Symbol[]
                    {
                        SquareRoot,
                        NaturalLog,
                        Log,
                        Sum,
                        Product,
                        Integral,
                        DoubleIntegral,
                        TripleIntegral,
                        Limit,
                        Max,
                        Min,
                        Choose,
                        Binomial,
                    };
                }

                return _functional;
            }
        }

        #region Parentheses

        private static Couple[] _parentheseCouples;
        public static Couple[] ParentheseCouples
        {
            get
            {
                if (_parentheseCouples == null)
                {
                    _parentheseCouples = new Couple[]
                    {

                        new Couple(BracketStraightLeft, BracketStraightRight),
                        new Couple(BracketCurvedLeft, BracketCurvedRight),
                        new Couple(BracketAngleLeft, BracketAngleRight),
                        new Couple(BracketBraceLeft, BracketBraceRight),
                        new Couple(BracketFloorLeft,BracketFloorRight),
                        new Couple(BracketCeilLeft, BracketCeilRight),
                    };
                }

                return _parentheseCouples;
            }
        }

        public static Symbol BracketFloorRight = new("⌋", "\\rfloor");

        public static Symbol BracketAngleRight = new("⟩", "\\rangle");

        public static Symbol BracketCeilRight = new("⌉", "\\rceil");

        public static Symbol BracketStraightRight = new("]", "]");

        public static Symbol BracketCurvedRight = new(")", ")");

        public static Symbol BracketBraceRight = new("}", "}");


        public static Symbol BracketFloorLeft = new("⌊", "\\lfloor");

        public static Symbol BracketAngleLeft = new("⟨", "\\langle");

        public static Symbol BracketCeilLeft = new("⌈", "\\lceil");

        public static Symbol BracketStraightLeft = new("[", "[");

        public static Symbol BracketCurvedLeft = new("(", "(");

        public static Symbol BracketBraceLeft = new("{", "{");


        public static Symbol Absolute = new("|", "\\vert");

        public static Symbol Norm = new("||", "\\Vert");


        public static Symbol ScaleRight = new("Sr", "\\right");

        public static Symbol ScaleLeft = new("Sl", "\\left");

        #endregion

        #region Logical

        public static Symbol LessThan = new("<", "\\lt");

        public static Symbol MoreThan = new(">", "\\gt");

        public static Symbol LessThanOrEqualTo = new("≤", "\\le");

        public static Symbol MoreThanOrEqualTo = new("≥", "\\ge");

        public static Symbol NotEqualTo = new("≠", "\\neq");

        public static Symbol And = new("∧", "\\land");

        public static Symbol Or = new("∨", "\\lor");

        public static Symbol Not = new("¬", "\\lnot");

        public static Symbol ForAll = new("∀", "\\forall");

        public static Symbol Exists = new("∃", "\\exists");

        public static Symbol NotExists = new("∃", "\\nexists");

        public static Symbol Top = new("⊤", "\\top");

        public static Symbol Bottom = new("⊥", "\\bot");

        public static Symbol Provable = new("⊢", "\\vdash");

        public static Symbol Entails = new("⊨", "\\vDash");

        public static Symbol ApproxEqual = new("≈", "\\approx");

        public static Symbol SimilarlyEqual = new("≃", "\\simeq");

        public static Symbol Similar = new("∼", "\\sim");

        public static Symbol Congruent = new("≅", "\\cong");

        public static Symbol Equivalent = new("≡", "\\equiv");

        public static Symbol Order = new("≺", "\\prec");

        public static Symbol SubgroupNormal = new("⊲", "\\lhd");

        public static Symbol Therefore = new("∴", "\\therefore");

        #endregion

        #region Operators

        public static Symbol Times = new("×", "\\times");

        public static Symbol Divide = new("÷", "\\div");

        public static Symbol Fraction = new("/", "\\frac");

        public static Symbol PlusOrMinus = new("±", "\\pm");

        public static Symbol MinusOrPlus = new("∓", "\\mp");

        public static Symbol Dot = new("⋅", "\\cdot");

        public static Symbol Star = new("⋆", "\\star");

        public static Symbol Asterisk = new("∗", "\\ast");

        public static Symbol ExclusiveOr = new("⊕", "\\oplus");

        public static Symbol Circle = new("∘", "\\circ");

        public static Symbol Bullet = new("∙", "\\bullet");

        #endregion

        #region Sets

        public static Symbol Union = new("∪", "\\cup");

        public static Symbol Intersection = new("∩", "\\cap");

        public static Symbol SetMinus = new("∖", "\\setminus");

        public static Symbol StrictSubset = new("⊂", "\\subset");

        public static Symbol Subset = new("⊆", "\\subseteq");

        public static Symbol StrictSuperset = new("⊃", "\\supset");

        public static Symbol Superset = new("⊇", "\\supseteq");

        public static Symbol In = new("∈", "\\in");

        public static Symbol NotIn = new("∉", "\\notin");

        public static Symbol EmptySet = new("∅", "\\emptyset");

        public static Symbol Nothing = new("∅", "\\varnothing");

        #endregion

        #region Arrows

        public static Symbol To = new("→", "\\to");

        public static Symbol ArrowRight = new("→", "\\rightarrow");

        public static Symbol ArrowLeft = new("←", "\\leftarrow");

        public static Symbol ArrowImpliesRight = new("⇒", "\\Rightarrow");

        public static Symbol ArrowImpliesLeft = new("⇐", "\\Leftarrow");

        public static Symbol ArrowImpliesLeftAndRight = new("⇔", "\\Leftrightarrow");

        public static Symbol MapsTo = new("↦", "\\mapsto");

        #endregion

        #region Special

        public static Symbol Infinity = new("∞", "\\infty");

        public static Symbol Gradient = new("∇", "\\nabla");

        public static Symbol Partial = new("∂", "\\partial");

        public static Symbol Dots = new("…", "\\cdots");

        public static Symbol Length = new("ℓ", "\\ell");

        public static Symbol Imaginary = new("I", "\\Im");

        public static Symbol Real = new("R", "\\Re");

        #endregion

        #region Trigonometric

        public static Symbol Sin = new("sin", "\\sin");

        public static Symbol Cos = new("cos", "\\cos");

        public static Symbol Tan = new("tan", "\\tan");

        public static Symbol Cot = new("cot", "\\cot");

        public static Symbol Sec = new("sec", "\\sec");

        public static Symbol Csc = new("csc", "\\csc");

        public static Symbol Arcsin = new("asin", "\\arcsin");

        public static Symbol Arccos = new("acos", "\\arccos");

        public static Symbol Arctab = new("atan", "\\arctan");

        #endregion

        #region Functional

        public static Symbol SquareRoot = new("√", "\\sqrt");

        public static Symbol NaturalLog = new("ln", "\\ln");

        public static Symbol Log = new("log", "\\log");

        public static Symbol Sum = new("∑", "\\sum");

        public static Symbol Product = new("∏", "\\prod");

        public static Symbol Integral = new("∫", "\\int");

        public static Symbol DoubleIntegral = new("∫∫", "\\iint");

        public static Symbol TripleIntegral = new("∫∫∫", "\\iiint");

        public static Symbol Limit = new("lim", "\\lim");

        public static Symbol Max = new("max", "\\max");

        public static Symbol Min = new("min", "\\min");

        public static Symbol Choose = new("Choose", "\\choose");

        public static Symbol Binomial = new("Binomial", "\\binom");

        #endregion

        #region Letters - lowercase

        public static Symbol Alpha = new("α", "\\alpha");

        public static Symbol Beta = new("β", "\\beta");

        public static Symbol Gamma = new("γ", "\\gamma");

        public static Symbol Delta = new("δ", "\\detla");

        public static Symbol Epsilon = new("ϵ", "\\epsilon");

        public static Symbol EpsilonAlt = new("ε", "\\varepsilon");

        public static Symbol Zeta = new("ζ", "\\zeta");

        public static Symbol Eta = new("η", "\\eta");

        public static Symbol Theta = new("θ", "\\theta");

        public static Symbol ThetaAlt = new("ϑ", "\\vartheta");

        public static Symbol Iota = new("ι", "\\iota");

        public static Symbol Kappa = new("κ", "\\lambda");

        public static Symbol Lambda = new("λ", "");

        public static Symbol Mu = new("μ", "\\mu");

        public static Symbol Nu = new("ν", "\\nu");

        public static Symbol Xi = new("ξ", "\\xi");

        public static Symbol Omicron = new("ο", "\\omicron");

        public static Symbol Pi = new("π", "\\pi");

        public static Symbol PiAlt = new("ϖ", "\\varpi");

        public static Symbol Rho = new("ρ", "\\rho");

        public static Symbol RhoAlt = new("ϱ", "\\varrho");

        public static Symbol Sigma = new("σ", "\\sigma");

        public static Symbol SigmaAlt = new("ς", "\\varsigma");

        public static Symbol Tau = new("τ", "\\tau");

        public static Symbol Upsilon = new("υ", "\\upsilon");

        public static Symbol Phi = new("ϕ", "\\phi");

        public static Symbol PhiAlt = new("φ", "\\varphi");

        public static Symbol Chi = new("χ", "\\chi");

        public static Symbol Psi = new("ψ", "\\psi");

        public static Symbol Omega = new("ω", "\\omega");

        #endregion

        #region Letters - uppercase

        public static Symbol GammaUC = new("Γ", "\\Gamma");

        public static Symbol DeltaUC = new("Δ", "\\Delta");

        public static Symbol ThetaUC = new("Θ", "\\Theta");

        public static Symbol LambdaUC = new("Λ", "\\Lambda");

        public static Symbol XiUC = new("Ξ", "\\Xi");

        public static Symbol PiUC = new("Π", "\\Pi");

        public static Symbol SigmaUC = new("Σ", "\\Sigma");

        public static Symbol UpsilonUC = new("Υ", "\\Upsilon");

        public static Symbol PsiUC = new("Ψ", "\\Psi");

        public static Symbol OmegaUC = new("Ω", "\\Omega");

        #endregion

        #region Fonts

        public static Symbol FontBlackBoard = new("Blackboard", "\\mathbb");

        public static Symbol FontText = new("Text", "\\text");

        public static Symbol FontBlackboardBold = new("Blackboard Bold", "Bbb");

        public static Symbol FontBoldFace = new("Boldface", "\\mathbf");

        public static Symbol FontItalics = new("Italics", "\\mathit");

        public static Symbol FontTypewriter = new("Typewriter", "\\mathtt");

        public static Symbol FontRoman = new("Roman", "\\mathrm");

        public static Symbol FontSansSerif = new("Sans Serif", "\\mathsf");

        public static Symbol FontCalligraphy = new("Calligraphy", "\\mathcal");

        public static Symbol FontScript = new("Script", "\\mathscr");

        public static Symbol FontFraktur = new("Fraktur", "\\mathfrak");

        #endregion

        #region Spaces

        public static Symbol SpaceThin = new("Thin Space", "\\,");

        public static Symbol SpaceNormal = new("Normal Space", "\\;");

        public static Symbol SpaceBig = new("Big Space", "\\quad");

        public static Symbol SpaceBigger = new("Bigger Space", "\\qquad");

        #endregion

        #region Accents

        public static Symbol Hat = new("^", "\\hat");

        public static Symbol Overline = new("¯¯¯", "\\overline");

        public static Symbol Vector = new("→", "\\vec");

        public static Symbol Widehat = new("ˆ", "\\widehat");

        public static Symbol Bar = new("¯", "\\bar");

        public static Symbol ArrowOverRight = new("−→", "\\overrightarrow");

        public static Symbol ArrowOverLeftRight = new("←→", "\\overleftrightarrow");

        public static Symbol DotOver = new("˙", "\\dot");

        public static Symbol DoubleDotOver = new("¨", "\\ddot");

        #endregion

        // Records

        internal record Symbol(string Icon, string Code);

        internal record Couple(Symbol Left, Symbol Right);

    }

}
