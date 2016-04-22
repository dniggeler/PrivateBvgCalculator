using System.Threading.Tasks;
using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation
{
    public class CalculatorBvg : IBvgCalculator
    {
        private readonly ICalcVersicherterLohn _calcVersicherterLohn;
        private readonly ICalcAltersgutschrift _calcAltersgutschrift;

        public CalculatorBvg(ICalcVersicherterLohn calcVersicherterLohn, ICalcAltersgutschrift calcAltersgutschrift)
        {
            _calcVersicherterLohn = calcVersicherterLohn;
            _calcAltersgutschrift = calcAltersgutschrift;
        }

        public async Task<BvgCalculationResult> CalculateAsync(BvgPlan plan, BvgCalculationInput input)
        {
            BvgCalculationResult result = new BvgCalculationResult();

            result.VersicherterLohn = _calcVersicherterLohn.Calculate(plan, input);
            result.Altersgutschrift = _calcAltersgutschrift.Calculate(plan, input);

            result.Altersrente = 1000m;

            return await Task.FromResult(result);
        }
    }
}
