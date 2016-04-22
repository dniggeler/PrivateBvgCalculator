using System;
using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation
{
    public class CalculatorJahrslohn : ICalcJahreslohn
    {
        private readonly ICalculatorAhv _calcAhv;

        public CalculatorJahrslohn(ICalculatorAhv calcAhv)
        {
            _calcAhv = calcAhv;
        }

        public decimal Calculate(BvgPlan plan, BvgCalculationInput input)
        {
            return Math.Min(input.Lohn,3.0m *_calcAhv.GetMaxRente(input.DateOfEintritt.Year));
        }
    }
}
