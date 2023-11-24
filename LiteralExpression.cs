namespace CalculatorApp;

public class LiteralExpression : Expression
{
    public double Value { get; set; }
    public override double Calculate()
    {
        return Value;
    }

    public LiteralExpression(double value)
    {
        Value = value;
    }
}