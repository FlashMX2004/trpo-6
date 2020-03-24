using System.Linq;
using CalculatorLib.CalculationOperators;

namespace CalculatorLib
{
    public class CalculatorClient
    {
        public const char DOT = ',';
        public const string ERROR_MESSAGE = "Error";

        public ICalculationOperator CalculationOperator { get; }
        public Operator? Operator { get; set; }
        public string Current { get; set; }
        public string Memory { get; set; }
        public bool Error { get; private set; }

        public delegate void CalculatorClientAction(string newText);
        public CalculatorClientAction OnDigit;
        public CalculatorClientAction OnCE;
        public CalculatorClientAction OnDot;
        public CalculatorClientAction OnOperation;
        public CalculatorClientAction OnResult;
        public CalculatorClientAction OnError;

        public CalculatorClient(ICalculationOperator calculationOperator)
        {
            this.CalculationOperator = calculationOperator;
            Clear();
        }

        #region ACTIONS

        public void ActionDigit(string digit)
        {
            if (Error) return;

            Current = Current == "0" ? digit : Current + digit;

            // Raise event
            OnDigit?.Invoke(Current);
        }

        public void ActionDot()
        {
            if (Error) return;

            if (!(Current ??= "0").Contains(DOT)) Current += DOT;

            // Raise event
            OnDot?.Invoke(Current);
        }

        public void ActionOperator(string @operator)
        {
            if (Error) return;

            try
            {
                Calculate();
            }
            catch
            {
                SetError();
                return;
            }

            Operator = CalculationOperator.GetOperator(@operator);

            // Raise event
            OnDot?.Invoke(Memory + @operator);
        }

        public void ActionResult()
        {
            if (Error) return;

            try
            {
                Calculate();
            }
            catch
            {
                SetError();
                return;
            }

            Operator = null;

            // Raise event
            OnResult?.Invoke(Memory);
        }

        public void ActionCE()
        {
            Clear();

            // Raise event
            OnCE?.Invoke(Current);
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Parses string number into double
        /// </summary>
        /// <param name="number">String number</param>
        /// <returns>Double number</returns>
        private double Parse(string number)
        {
            if (number.LastOrDefault() == DOT) 
            {
                number += "0";
            }

            return double.Parse(number);
        }

        /// <summary>
        /// Clears all data
        /// </summary>
        private void Clear()
        {
            Current = "0";
            Memory = null;
            Operator = null;
            Error = false;
        }

        /// <summary>
        /// Calculates number and saves into memory. Clears current number
        /// </summary>
        private void Calculate()
        {
            if (Current != null && Memory != null && Operator != null)
            {
                double a = Parse(Memory);
                double b = Parse(Current);
                Memory = CalculationOperator.DoOperation(a, b, Operator.Value).ToString();
            }
            else if (Current != null)
            {
                Memory = Current;
            }

            Current = null;
        }

        /// <summary>
        /// Sets error on client
        /// </summary>
        private void SetError()
        {
            Error = true;
            OnError?.Invoke(ERROR_MESSAGE);
        }

        #endregion
    }
}
