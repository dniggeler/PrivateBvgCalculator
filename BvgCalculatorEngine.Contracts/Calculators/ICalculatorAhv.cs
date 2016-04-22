using System.Runtime.Remoting.Messaging;

namespace BvgCalculatorEngine.Contracts.Calculators
{
    public interface ICalculatorAhv
    {
        decimal GetMaxRente(int financialYear);
    }
}