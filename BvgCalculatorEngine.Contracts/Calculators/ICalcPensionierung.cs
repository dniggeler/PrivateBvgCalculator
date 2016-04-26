using System;

namespace BvgCalculatorEngine.Contracts.Calculators
{
    public interface ICalcDateOfPensionierung
    {
        DateTime Calculate(BvgPlan plan, BvgCalculationInput input);
    }
}