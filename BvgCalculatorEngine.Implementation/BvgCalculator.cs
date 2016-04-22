using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;

namespace BvgCalculatorEngine.Implementation
{
    public class BvgCalculator : IBvgCalculator
    {
        private readonly BvgConstants _bvgConstants;

        public BvgCalculator(BvgConstants bvgConstants)
        {
            _bvgConstants = bvgConstants;
        }

        public async Task<BvgCalculationResult> CalculateAsync(BvgPlan plan, BvgCalculationInput input)
        {
            DateTime calculationDate = new DateTime(2016,1,1);
            int rechnungsjahr = calculationDate.Year;
            int bvgAlter = rechnungsjahr - input.DateOfBirth.Year;
            int schlussalter =0;
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

            bool isPensionierungInRechnungsjahr = bvgAlter == schlussalter;

            DateTime dateOfRetirement = new DateTime(input.DateOfBirth.Year,input.DateOfBirth.Month,1).AddMonths(1).AddYears(schlussalter).AddDays(-1);

            decimal schrumpfPeriode = dateOfRetirement.Month / 12m;
            decimal schrumpPeriodeRechnungsjahr = (isPensionierungInRechnungsjahr ? schrumpfPeriode : 1m);

            decimal koordinationsabzug = _bvgConstants.MaxAhvRente*0.875m;
            decimal maxVersicherbarerLohn = 3.0m * _bvgConstants.MaxAhvRente;
            decimal versicherterLohn = Math.Min(input.Lohn, maxVersicherbarerLohn) - koordinationsabzug;

            var sparstaffelung = new Sparstaffelung(plan);

            decimal eaghOhneZins = 0m;
            decimal eaghMitZins = 0m;

            var projections = new Dictionary<DateTime,decimal>();

            projections.Add(calculationDate.Date,eaghMitZins+input.Altersguthaben);

            // Aktuelles Jahr
            if (bvgAlter>=plan.Eintrittsalter)
            {
                decimal agsRechnungsjahr = 
                    versicherterLohn * sparstaffelung.GetGutschriftssatz(bvgAlter, input.Geschlecht)* schrumpPeriodeRechnungsjahr;

                eaghOhneZins = 
                    agsRechnungsjahr + input.Altersguthaben * (1m + _bvgConstants.BvgZins* schrumpPeriodeRechnungsjahr);

                eaghMitZins = eaghOhneZins;
                if (isPensionierungInRechnungsjahr)
                {
                    projections.Add(dateOfRetirement.Date, eaghMitZins);
                }
                else
                {
                    projections.Add(new DateTime(calculationDate.AddYears(1).Year, 1, 1).Date, eaghMitZins);
                }
            }

            // Folgejahr ohne Schrumpjahr Pensionierung
            for (int x = Math.Max(bvgAlter+1,plan.Eintrittsalter); x < schlussalter; x++)
            {
                decimal ags = versicherterLohn * sparstaffelung.GetGutschriftssatz(x, input.Geschlecht);

                eaghOhneZins += ags;
                eaghMitZins = eaghMitZins*(1m + _bvgConstants.BvgZins) + ags;

                projections.Add(new DateTime(input.DateOfBirth.Year+x,1,1).AddYears(1).Date, eaghMitZins);
            }

            // Schrumpfjahr
            if(isPensionierungInRechnungsjahr == false)
            {
                
                decimal agsSchlussjahr = versicherterLohn * sparstaffelung.GetGutschriftssatz(schlussalter, input.Geschlecht);
                decimal agsSchrumpfjahr = agsSchlussjahr*schrumpfPeriode;

                eaghOhneZins += agsSchrumpfjahr;
                eaghMitZins = eaghMitZins*(1m + _bvgConstants.BvgZins*schrumpfPeriode) + agsSchrumpfjahr;

                projections.Add(dateOfRetirement, eaghMitZins);
            }


            var result = new BvgCalculationResult()
            {
                EaghMitZins = eaghMitZins,
                EaghOhneZins = eaghOhneZins,
                Altersrente = eaghMitZins * _bvgConstants.Umws,
                Invalidenrente = eaghOhneZins* _bvgConstants.Umws * plan.FactorInvalidenrente,
                Partnerrente = eaghOhneZins * _bvgConstants.Umws * plan.FactorPartnerrente,
                Waisenrente = eaghOhneZins * _bvgConstants.Umws * plan.FactorWaisenrente,
            };

            result.Projections = projections.Select(pair => new ProjectionItem()
            {
                DateOfRechnungsjahr = pair.Key, Altersguthaben = pair.Value,
            })
            .ToArray();

            return await Task.FromResult(result);
        }
    }
}
