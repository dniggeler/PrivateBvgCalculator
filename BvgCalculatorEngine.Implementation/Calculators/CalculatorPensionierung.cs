using System;
using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation.Calculators
{
    public class CalculatorDateOfPensionierung : ICalcDateOfPensionierung
    {
        private readonly ICalcSchlussalter _calcSchlussalter;

        public CalculatorDateOfPensionierung(ICalcSchlussalter calcSchlussalter)
        {
            _calcSchlussalter = calcSchlussalter;
        }

        public DateTime Calculate(BvgPlan plan, BvgCalculationInput input)
        {
            int schlussalter = _calcSchlussalter.Calculate(plan, input);

            DateTime dateOfRetirement = new DateTime(input.DateOfBirth.Year, input.DateOfBirth.Month, 1).AddMonths(1).AddYears(schlussalter).AddDays(-1);

            return dateOfRetirement;
        }
    }
}
