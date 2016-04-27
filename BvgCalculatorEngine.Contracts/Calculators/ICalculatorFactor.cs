namespace BvgCalculatorEngine.Contracts.Calculators
{
    public interface ICalculatorFactor
    {
        decimal Calculate(BvgPlan plan, BvgCalculationInput input);
    }
}