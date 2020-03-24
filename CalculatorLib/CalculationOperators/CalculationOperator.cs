using System;
using System.Collections.Generic;

namespace CalculatorLib.CalculationOperators
{
    public class CalculationOperator : ICalculationOperator
    {
        public Dictionary<string, Operator> Operators { get; set; } = new Dictionary<string, Operator>()
        {
            { "+", Operator.Sum },
            { "-", Operator.Substract },
            { "*", Operator.Multiply },
            { "/", Operator.Divide },
        };

        public Operator GetOperator(string @operator)
        {
            if (Operators.ContainsKey(@operator))
            {
                return Operators[@operator];
            }
            else
            {
                throw new InvalidOperationException("Operator does not existing");
            }
        }

        public double DoOperation(double a, double b, Operator @operator) => @operator switch
        {
            Operator.Sum       => a + b,
            Operator.Substract => a - b,
            Operator.Multiply  => a * b,
            Operator.Divide    => b == 0 ? throw new DivideByZeroException("b is zero") : a / b,
            _                  => throw new InvalidOperationException("Operator does not existing")
        };
    }
}
