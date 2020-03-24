using System.Collections.Generic;

namespace CalculatorLib.CalculationOperators
{
    public interface ICalculationOperator
    {
        Dictionary<string, Operator> Operators { get; set; }
        double DoOperation(double a, double b, Operator @operator);
        Operator GetOperator(string @operator);
    }
}
