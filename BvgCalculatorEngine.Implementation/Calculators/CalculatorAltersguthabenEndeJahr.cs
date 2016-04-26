using System;
using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation.Calculators
{
    public class CalculatorAltersguthabenEndeJahr : ICalcAlterguthabenEndeJahr
    {
        private readonly ICalcAltersgutschrift _calcAltersgutschrift;
        private readonly ICalcSchlussalter _calcSchlussalter;

        public CalculatorAltersguthabenEndeJahr(ICalcAltersgutschrift calcAltersgutschrift, ICalcSchlussalter calcSchlussalter)
        {
            _calcAltersgutschrift = calcAltersgutschrift;
            _calcSchlussalter = calcSchlussalter;
        }

        public decimal Calculate(BvgPlan plan, BvgCalculationInput input)
        {
            int schlussalter = _calcSchlussalter.Calculate(plan, input);

            int rechnungsjahr = input.DateOfEintritt.Year;
            int bvgAlter = rechnungsjahr - input.DateOfBirth.Year;

            bool isPensionierungInRechnungsjahr = bvgAlter == schlussalter;

            DateTime dateOfRetirement = new DateTime(input.DateOfBirth.Year, input.DateOfBirth.Month, 1).AddMonths(1).AddYears(schlussalter).AddDays(-1);

            decimal schrumpfPeriode = dateOfRetirement.Month / 12m;
            decimal schrumpPeriodeRechnungsjahr = (isPensionierungInRechnungsjahr ? schrumpfPeriode : 1m);

            decimal aghEndeJahr = _calcAltersgutschrift.Calculate(plan, input);
            if (bvgAlter >= plan.Eintrittsalter)
            {
                aghEndeJahr *= schrumpPeriodeRechnungsjahr;
            }

            return input.Altersguthaben + aghEndeJahr;
        }
    }
}
