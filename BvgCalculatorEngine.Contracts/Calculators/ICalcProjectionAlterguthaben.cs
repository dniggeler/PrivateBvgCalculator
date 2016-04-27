using System;
using System.Collections.Generic;

namespace BvgCalculatorEngine.Contracts.Calculators
{
    public interface ICalcProjectionAlterguthaben
    {
        Dictionary<DateTime, decimal> Calculate(BvgPlan plan, BvgCalculationInput input);
    }
}