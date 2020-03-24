using System;
using System.Windows.Forms;
using CalculatorLib;
using CalculatorLib.CalculationOperators;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private CalculatorClient Client { get; }

        public CalculatorForm()
        {
            InitializeComponent();
            this.Client = new CalculatorClient(new CalculationOperator());

            CalculatorClient.CalculatorClientAction labelUpdate = text => label.Text = text;
            this.Client.OnDigit     = labelUpdate;
            this.Client.OnDot       = labelUpdate;
            this.Client.OnOperation = labelUpdate;
            this.Client.OnCE        = labelUpdate;
            this.Client.OnResult    = labelUpdate;
            this.Client.OnError     = labelUpdate;
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            string digit = (string)(sender as Button).Tag;
            this.Client.ActionDigit(digit);
        }
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            string @operator = (string)(sender as Button).Tag;
            this.Client.ActionOperator(@operator);
        }
        private void CEButton_Click(object sender, EventArgs e) => this.Client.ActionCE();
        private void DotButton_Click(object sender, EventArgs e) => this.Client.ActionDot();
        private void ResultButton_Click(object sender, EventArgs e) => this.Client.ActionResult();

        private void CalculatorForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad0: this.Client.ActionDigit("0"); break;
                case Keys.NumPad1: this.Client.ActionDigit("1"); break;
                case Keys.NumPad2: this.Client.ActionDigit("2"); break;
                case Keys.NumPad3: this.Client.ActionDigit("3"); break;
                case Keys.NumPad4: this.Client.ActionDigit("4"); break;
                case Keys.NumPad5: this.Client.ActionDigit("5"); break;
                case Keys.NumPad6: this.Client.ActionDigit("6"); break;
                case Keys.NumPad7: this.Client.ActionDigit("7"); break;
                case Keys.NumPad8: this.Client.ActionDigit("8"); break;
                case Keys.NumPad9: this.Client.ActionDigit("9"); break;

                case Keys.Add: this.Client.ActionOperator("+"); break;
                case Keys.Subtract: this.Client.ActionOperator("-"); break;
                case Keys.Multiply: this.Client.ActionOperator("*"); break;
                case Keys.Divide: this.Client.ActionOperator("/"); break;

                case Keys.Decimal: this.Client.ActionDot(); break;
                case Keys.Enter: this.Client.ActionResult(); break;
                case Keys.Delete: this.Client.ActionCE(); break;

                default: break;
            }
        }
    }
}
