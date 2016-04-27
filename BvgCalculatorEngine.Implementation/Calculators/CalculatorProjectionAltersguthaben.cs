using System;
using System.Collections.Generic;
using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation.Calculators
{
    public class CalculatorProjectionAltersguthaben : ICalcProjectionAlterguthaben
    {
        private readonly ICalcAlterguthabenEndeJahr _calcAlterguthabenEndeJahr;

        public CalculatorProjectionAltersguthaben(ICalcAlterguthabenEndeJahr calcAlterguthabenEndeJahr)
        {
            _calcAlterguthabenEndeJahr = calcAlterguthabenEndeJahr;
        }

        public Dictionary<DateTime, decimal> Calculate(BvgPlan plan, BvgCalculationInput input)
        {
            var dictProjection = new Dictionary<DateTime, decimal>();

            var aghEndeJahr = _calcAlterguthabenEndeJahr.Calculate(plan, input);

            return dictProjection;
        }
    }
}