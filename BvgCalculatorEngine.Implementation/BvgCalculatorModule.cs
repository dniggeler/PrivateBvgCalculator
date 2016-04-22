using BvgCalculatorEngine.Contracts.Calculators;
using Ninject.Modules;

namespace BvgCalculatorEngine.Implementation
{
    public class BvgCalculatorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICalculatorAhv>().To<CalculatorAhv>();
            Bind<ICalcAltersgutschrift>().To<CalculatorAltersgutschrift>();
            Bind<ICalcJahreslohn>().To<CalculatorJahrslohn>();
            Bind<ICalcVersicherterLohn>().To<CalculatorVersicherterLohn>();
            Bind<ICalcKoordinationsabzug>().To<CalculatorKoordinationsabzug>();
            Bind<ICalcMinimumLohn>().To<CalculatorMinimumLohn>();
            Bind<ICalcLohnuntergrenze>().To<CalculatorLohnuntergrenze>();
            Bind<IBvgCalculator>().To<CalculatorBvg>();
        }
    }
}
