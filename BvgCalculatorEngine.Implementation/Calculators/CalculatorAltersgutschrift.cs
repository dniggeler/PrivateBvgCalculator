using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation
{
    public class CalculatorAltersgutschrift : ICalcAltersgutschrift
    {
        private readonly ICalcVersicherterLohn _calcVersicherterLohn;

        public CalculatorAltersgutschrift(ICalcVersicherterLohn calcVersicherterLohn)
        {
            _calcVersicherterLohn = calcVersicherterLohn;
        }

        public decimal Calculate(BvgPlan plan, BvgCalculationInput input)
        {
            return _calcVersicherterLohn.Calculate(plan, input)*0.15m;
        }
    }
}