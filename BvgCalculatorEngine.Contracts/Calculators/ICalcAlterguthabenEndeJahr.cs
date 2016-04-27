using System;

namespace BvgCalculatorEngine.Contracts.Calculators
{
    public interface ICalcAlterguthabenEndeJahr
    {
       Tuple<DateTime, decimal> Calculate(BvgPlan plan, BvgCalculationInput input);
    }
}