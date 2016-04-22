namespace BvgCalculatorEngine.Contracts.Calculators
{
    class CalculatorAhv : ICalculatorAhv
    {
        private const decimal MaxRente = 28200;

        public decimal GetMaxRente(int financialYear)
        {
            return MaxRente;
        }
    }
}