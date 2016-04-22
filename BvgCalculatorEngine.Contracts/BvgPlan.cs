using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BvgCalculatorEngine.Contracts
{
    public class BvgPlan
    {
        public int Eintrittsalter { get; private set; } = 25;
        public int SchlussalterMann { get; private set; } = 65;
        public int SchlussalterFrau { get; private set; } = 64;
        public decimal FactorInvalidenrente { get; set; } = 1.0m;
        public decimal FactorPartnerrente { get; set; } = 0.6m;
        public decimal FactorWaisenrente { get; set; } = 0.2m;
    }
}
