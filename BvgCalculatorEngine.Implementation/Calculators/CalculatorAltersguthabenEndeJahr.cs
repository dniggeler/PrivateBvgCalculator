using System;
using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation.Calculators
{
    public class CalculatorAltersguthabenEndeJahr : ICalcAlterguthabenEndeJahr
    {
        private readonly BvgConstants _constantsBvg;
        private readonly ICalcAltersgutschrift _calcAltersgutschrift;
        private readonly ICalcSchlussalter _calcSchlussalter;
        private readonly ICalcDateOfPensionierung _calcPensionierung;

        public CalculatorAltersguthabenEndeJahr(BvgConstants constantsBvg, ICalcAltersgutschrift calcAltersgutschrift, ICalcSchlussalter calcSchlussalter, 
            ICalcDateOfPensionierung calcPensionierung)
        {
            _constantsBvg = constantsBvg;
            _calcAltersgutschrift = calcAltersgutschrift;
            _calcSchlussalter = calcSchlussalter;
            _calcPensionierung = calcPensionierung;
        }

        public decimal Calculate(BvgPlan plan, BvgCalculationInput input)
        {
            int schlussalter = _calcSchlussalter.Calculate(plan, input);
            DateTime dateOfRetirement = _calcPensionierung.Calculate(plan, input);

            int rechnungsjahr = input.DateOfEintritt.Year;
            int bvgAlter = rechnungsjahr - input.DateOfBirth.Year;

            bool isPensionierungInRechnungsjahr = bvgAlter == schlussalter;

            decimal aghEndeJahr=0;
            if (bvgAlter >= plan.Eintrittsalter)
            {
                decimal schrumpPeriodeRechnungsjahr = (isPensionierungInRechnungsjahr ? dateOfRetirement.Month / 12m : 1m);
                decimal agsRechnungsjahr = _calcAltersgutschrift.Calculate(plan, input) * schrumpPeriodeRechnungsjahr;

                aghEndeJahr = agsRechnungsjahr + input.Altersguthaben * (1m + _constantsBvg.BvgZins * schrumpPeriodeRechnungsjahr);
            }

            return aghEndeJahr;
        }
    }
}
