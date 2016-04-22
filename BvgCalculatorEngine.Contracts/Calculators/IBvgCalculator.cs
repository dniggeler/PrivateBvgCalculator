using System.Threading.Tasks;

namespace BvgCalculatorEngine.Contracts.Calculators
{
    public interface IBvgCalculator
    {
        Task<BvgCalculationResult> CalculateAsync(BvgPlan plan, BvgCalculationInput input);
    }
}