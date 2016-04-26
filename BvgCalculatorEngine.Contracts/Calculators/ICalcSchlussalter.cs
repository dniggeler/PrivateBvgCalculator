namespace BvgCalculatorEngine.Contracts.Calculators
{
    public interface ICalcSchlussalter
    {
        int Calculate(BvgPlan plan, BvgCalculationInput input);
    }
}