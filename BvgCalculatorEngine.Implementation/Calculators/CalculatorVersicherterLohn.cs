using System;
using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation
{
    public class CalculatorVersicherterLohn : ICalcVersicherterLohn
    {
        private readonly ICalcJahreslohn _calcJahreslohn;
        private readonly ICalcKoordinationsabzug _calcKoordinationsabzug;
        private readonly ICalcLohnuntergrenze _calcLohnuntergrenze;
        private readonly ICalcMinimumLohn _calcMinimumLohn;

        public CalculatorVersicherterLohn(ICalcJahreslohn calcJahreslohn, ICalcKoordinationsabzug calcKoordinationsabzug, 
            ICalcLohnuntergrenze calcLohnuntergrenze, ICalcMinimumLohn calcMinimumLohn)
        {
            _calcJahreslohn = calcJahreslohn;
            _calcKoordinationsabzug = calcKoordinationsabzug;
            _calcLohnuntergrenze = calcLohnuntergrenze;
            _calcMinimumLohn = calcMinimumLohn;
        }

        public decimal Calculate(BvgPlan plan, BvgCalculationInput input)
        {
            var jahreslohn = _calcJahreslohn.Calculate(plan, input);

            if (jahreslohn <= _calcLohnuntergrenze.Calculate(plan, input))
            {
                return 0m;
            }

            decimal lohn = Math.Max( jahreslohn - _calcKoordinationsabzug.Calculate(plan, input), _calcMinimumLohn.Calculate(plan,input));

            return lohn;
        }
    }
}
