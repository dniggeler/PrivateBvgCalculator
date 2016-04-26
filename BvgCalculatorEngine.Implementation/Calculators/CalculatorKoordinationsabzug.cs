using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation.Calculators
{
    public class CalculatorKoordinationsabzug : ICalcKoordinationsabzug
    {
        private readonly ICalculatorAhv _calcAhv;

        public CalculatorKoordinationsabzug(ICalculatorAhv calcAhv)
        {
            _calcAhv = calcAhv;
        }

        public decimal Calculate(BvgPlan plan, BvgCalculationInput input)
        {
            return 0.875m*_calcAhv.GetMaxRente(input.DateOfEintritt.Year);
        }
    }
}