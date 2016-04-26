using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation.Calculators
{
    public class CalculatorAltersgutschrift : ICalcAltersgutschrift
    {
        private readonly ICalcVersicherterLohn _calcVersicherterLohn;
        private readonly ICalcStaffelung _staffelung;

        public CalculatorAltersgutschrift(ICalcVersicherterLohn calcVersicherterLohn, ICalcStaffelung staffelung)
        {
            _calcVersicherterLohn = calcVersicherterLohn;
            _staffelung = staffelung;
        }

        public decimal Calculate(BvgPlan plan, BvgCalculationInput input)
        {
            decimal factorAltersgutschrift = _staffelung.Calculate(plan, input);

            return _calcVersicherterLohn.Calculate(plan, input)* factorAltersgutschrift;
        }
    }
}