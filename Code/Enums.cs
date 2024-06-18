using System.ComponentModel;

public enum ConditionType
{
    [Description("If")]
    If,
    [Description("Else If")]
    ElseIf,
}

public enum ConditionItemType
{
    IsVariable,
    IsNumber,
    IsText,
}

public enum OperationType
{
    [Description("is greater than")]
    IsGreaterThan,
    [Description("is less than")]
    IsLessThan,
    [Description("is equal to")]
    IsEqualTo,
    [Description("is greater than or equal to")]
    IsGreaterThanOrEqualTo,
    [Description("is less than or equal to")]
    IsLessThanOrEqualTo,
    [Description("contains")]
    Contains,
}

public enum TableDimensionType
{
    [Description("Variable Group")]
    VariableGroup,
    [Description("Constant Group")]
    ConstantGroup,
    [Description("Variable")]
    Variable,
    [Description("Constant")]
    Constant
}

public enum StorageType
{
    Text,
    Real,
    Integer,
    Boolean,
    Enum,
}

public enum FunctionType
{
    General,
    Constant,
}

public enum StandardType
{
    [Description("British Standards")]
    BS,
    [Description("Eurocodes")]
    EC,
}

public enum StateType
{
    Current,
    Superseded,
    Widthdrawn,
    Archived
}

public enum OrderBy
{
    Date,
    Name,
    Desc,
    Math, 
    Unit,
    State,
    Standard
}

public enum UnitType
{
    [Description("None")]
    None,
    [Description("kN/mm^2")]
    KiloNewtonsPerMillimeterSquared,
    [Description("m/Ns^2")]
    MetersPerNewtonSecondsSquared,
    [Description("N/mm^2")]
    NewtonsPerMillimeterSquared,
    [Description("kg/m^3")]
    KilogramsPerMeterCubed,
    [Description("mm^2")]
    MillimetersSquared,
    [Description("mm^3")]
    MillimetersCubed,
    [Description("mm^4")]
    MillimetersQuad,
    [Description("m/s")]
    MetersPerSecond,
    [Description("mm")]
    Millimeters,
    [Description("m^3")]
    MetersCubed,
    [Description("m")]
    Meters,
    [Description("Nmm")]
    NewtonMillimeters,
    [Description("kNm")]
    KiloNewtonMeters,
    [Description("kN/m")]
    KiloNewtonsPerMeter,
    [Description("kN")]
    KiloNewtons,
    [Description("N")]
    Newtons,
    [Description("°")]
    Degrees,
    [Description("Hz")]
    Hertz,
}