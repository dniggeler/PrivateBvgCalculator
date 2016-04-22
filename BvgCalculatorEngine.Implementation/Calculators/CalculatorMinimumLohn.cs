using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation
{
    public class CalculatorMinimumLohn : ICalcMinimumLohn
    {
        private readonly ICalculatorAhv _calcAhv;

        public CalculatorMinimumLohn(ICalculatorAhv calcAhv)
        {
            _calcAhv = calcAhv;
        }

        public decimal Calculate(BvgPlan plan, BvgCalculationInput input)
        {
            return 0.125m * _calcAhv.GetMaxRente(input.DateOfEintritt.Year);
        }
    }
}