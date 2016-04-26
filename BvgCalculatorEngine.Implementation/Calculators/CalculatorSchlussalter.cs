using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation.Calculators
{
    public class CalculatorSchlussalter : ICalcSchlussalter
    {
        public int Calculate(BvgPlan plan, BvgCalculationInput input)
        {
            int schlussalter = 0;
            switch (input.Geschlecht)
            {
                case Geschlecht.Frau:
                    {
                        schlussalter = plan.SchlussalterFrau;
                    }
                    break;
                case Geschlecht.Mann:
                    {
                        schlussalter = plan.SchlussalterMann;
                    }
                    break;
            }

            return schlussalter;
        }
    }
}
