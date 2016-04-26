using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation.Calculators
{
    public class CalculatorLohnuntergrenze : ICalcLohnuntergrenze
    {
        private readonly ICalculatorAhv _calcAhv;

        public CalculatorLohnuntergrenze(ICalculatorAhv calcAhv)
        {
            _calcAhv = calcAhv;
        }

        public decimal Calculate(BvgPlan plan, BvgCalculationInput input)
        {
            return 0.75m*_calcAhv.GetMaxRente(input.DateOfEintritt.Year);
        }
    }
}