using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BvgCalculatorEngine.Contracts
{
    public class Sparstaffelung
    {
        private readonly BvgPlan _plan;
        private static Dictionary<int, decimal> _gutschriftssaetze = new Dictionary<int, decimal>
        {
            {25, 0.07m },
            {26, 0.07m },
            {27, 0.07m },
            {28, 0.07m },
            {29, 0.07m },
            {30, 0.07m },
            {31, 0.07m },
            {32, 0.07m },
            {33, 0.07m },
            {34, 0.07m },
            {35, 0.10m },
            {36, 0.10m },
            {37, 0.10m },
            {38, 0.10m },
            {39, 0.10m },
            {40, 0.10m },
            {41, 0.10m },
            {42, 0.10m },
            {43, 0.10m },
            {44, 0.10m },
            {45, 0.15m },
            {46, 0.15m },
            {47, 0.15m },
            {48, 0.15m },
            {49, 0.15m },
            {50, 0.15m },
            {51, 0.15m },
            {52, 0.15m },
            {53, 0.15m },
            {54, 0.15m },
            {55, 0.18m },
            {56, 0.18m },
            {57, 0.18m },
            {58, 0.18m },
            {59, 0.18m },
            {60, 0.18m },
            {61, 0.18m },
            {62, 0.18m },
            {63, 0.18m },
            {64, 0.18m },
            {65, 0.18m },
        };

        public Sparstaffelung(BvgPlan plan)
        {
            _plan = plan;
        }

        public decimal GetGutschriftssatz(int bvgAlter, Geschlecht geschlecht)
        {
            if (bvgAlter < _plan.Eintrittsalter)
            {
                return 0m;
            }

            int schlussalter = 0;
            switch (geschlecht)
            {
                case Geschlecht.Mann:
                {
                    schlussalter = _plan.SchlussalterMann;
                }
                    break;
                case Geschlecht.Frau:
                {
                    schlussalter = _plan.SchlussalterFrau;
                }
                    break;
            }

            if (bvgAlter > schlussalter)
            {
                return 0m;
            }

            decimal gutschriftssatz = 0.0m;
            _gutschriftssaetze.TryGetValue(bvgAlter, out gutschriftssatz);

            return gutschriftssatz;
        }
    }
}
