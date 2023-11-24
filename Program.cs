using CalculatorApp;

var expression = new BinaryExpression()
{
    Operator = '*',
    Left = new LiteralExpression(2),
    Right = new BinaryExpression()
    {
        Operator = '+',
        Left = new LiteralExpression(3),
        Right = new LiteralExpression(4)
    }
};

var cavab = expression.Calculate();
Console.WriteLine($"cavab = {cavab}");
    
while (false)
{
    var input = Console.ReadLine() ?? "";

    List<string> inputs = new List<string>();
    
    int j = 0;
    int k = -1;

    for (int i = 0; i < input.Length; i++)
    {
        var c = input[i];
        if ((c >= '0' && c <= '9') || c == '.')
        {
            if (k == -1)
            {
                k = i;
            }
            
            if (k != -1 && i == input.Length-1)
            {
                inputs.Add(input[k..(i+1)]);
            }
            
            continue;
        }
        else
        {
            if (k != -1)
            {
                inputs.Add(input[k..i]);
                k = -1;
            }
        }

        if (c == '+' || c == '-' || c == '*' || c == '/' || c == '(' || c == ')')
        {
            inputs.Add(input[i].ToString());
        }
    }

    for (int i = 0; i < inputs.Count; i++)
    {
        if (inputs[i] == "-" && (i == 0 || inputs[i - 1] == "("))
        {
            inputs.Insert(i, "0");
        }
    }

    while (inputs.Contains("("))
    {
        var openParentheses = inputs.LastIndexOf("(");
        var closeParentheses = inputs.IndexOf(")", openParentheses);

        Calculate(inputs, openParentheses + 1, closeParentheses - 1);
        inputs.RemoveAt(openParentheses + 2);
        inputs.RemoveAt(openParentheses);
    }
    
    Console.WriteLine(Calculate(inputs, 0, inputs.Count - 1));
}

static string Calculate(List<string> inputs, int startIndex, int stopIndex)
{
    int m = -1;
    double newValue = 0;
    Dictionary<string, int> strength = new Dictionary<string, int>()
    {
        { ")", 1 },
        { "+", 2 },
        { "-", 2 },
        { "*", 3 },
        { "/", 3 },
        { "(", 4 }
    };
    
    for (int i = startIndex; i <= stopIndex; i++)
    {
        var x = inputs[i];
    
        if (x == "+" || x == "-" || x == "*" || x == "/")
        {
            if (m == -1)
            {
                m = i;
                continue;
            }
            
            if (strength[inputs[i]] <= strength[inputs[m]])
            {
                switch (inputs[m])
                {
                    case "+":
                        newValue = double.Parse(inputs[m - 1]) + double.Parse(inputs[m + 1]);
                        break;
                    case "-":
                        newValue = double.Parse(inputs[m - 1]) - double.Parse(inputs[m + 1]);
                        break;
                    case "*":
                        newValue = double.Parse(inputs[m - 1]) * double.Parse(inputs[m + 1]);
                        break;
                    case "/":
                        newValue = double.Parse(inputs[m - 1]) / double.Parse(inputs[m + 1]);
                        break;
                }
                
                inputs.RemoveAt(m + 1);
                inputs.RemoveAt(m);
                inputs.RemoveAt(m - 1);
                
                inputs.Insert(m - 1, newValue.ToString());
    
                i = startIndex - 1;
                stopIndex -= 2;
                m = -1;
            }
            else
            {
                m = i;
            }
        }
        
        if (i == stopIndex && m != -1)
        {
            switch (inputs[m])
            {
                case "+":
                    newValue = double.Parse(inputs[m - 1]) + double.Parse(inputs[m + 1]);
                    break;
                case "-":
                    newValue = double.Parse(inputs[m - 1]) - double.Parse(inputs[m + 1]);
                    break;
                case "*":
                    newValue = double.Parse(inputs[m - 1]) * double.Parse(inputs[m + 1]);
                    break;
                case "/":
                    newValue = double.Parse(inputs[m - 1]) / double.Parse(inputs[m + 1]);
                    break;
            }
                
            inputs.RemoveAt(m + 1);
            inputs.RemoveAt(m);
            inputs.RemoveAt(m - 1);
                
            inputs.Insert(m - 1, newValue.ToString());

            i = startIndex - 1;
            stopIndex -= 2;
            m = -1;
        }
    }

    return inputs[0];
}