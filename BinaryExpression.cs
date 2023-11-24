namespace CalculatorApp;

public class BinaryExpression : Expression
{
    public Expression Left { get; set; }
    public Expression Right { get; set; }
    public char Operator { get; set; }
    
    public override double Calculate()
    {
        var left = Left.Calculate();
        var right = Right.Calculate();

        switch (Operator)
        {
            case '+':
                return left + right;
            case '-':
                return left - right;
            case '*':
                return left * right;
            case '/':
                return left / right;
            default:
                throw new Exception();
        }
    }
}