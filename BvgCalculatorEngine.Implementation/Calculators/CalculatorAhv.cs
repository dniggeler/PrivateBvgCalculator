using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation.Calculators
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