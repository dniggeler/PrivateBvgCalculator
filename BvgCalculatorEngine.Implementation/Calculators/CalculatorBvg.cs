using System.Threading.Tasks;
using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation.Calculators
{
    public class CalculatorBvg : IBvgCalculator
    {
        private readonly ICalcVersicherterLohn _calcVersicherterLohn;
        private readonly ICalcAltersgutschrift _calcAltersgutschrift;
        private readonly ICalcAlterguthabenEndeJahr _calcAlterguthabenEndeJahr;

        public CalculatorBvg(ICalcVersicherterLohn calcVersicherterLohn, ICalcAltersgutschrift calcAltersgutschrift, ICalcAlterguthabenEndeJahr calcAlterguthabenEndeJahr)
        {
            _calcVersicherterLohn = calcVersicherterLohn;
            _calcAltersgutschrift = calcAltersgutschrift;
            _calcAlterguthabenEndeJahr = calcAlterguthabenEndeJahr;
        }

        public async Task<BvgCalculationResult> CalculateAsync(BvgPlan plan, BvgCalculationInput input)
        {
            BvgCalculationResult result = new BvgCalculationResult();

            result.VersicherterLohn = _calcVersicherterLohn.Calculate(plan, input);
            result.Altersgutschrift = _calcAltersgutschrift.Calculate(plan, input);
            result.AlterguthabenEndeJahr = _calcAlterguthabenEndeJahr.Calculate(plan, input).Item2;

            result.Altersrente = 1000m;

            return await Task.FromResult(result);
        }
    }
}
