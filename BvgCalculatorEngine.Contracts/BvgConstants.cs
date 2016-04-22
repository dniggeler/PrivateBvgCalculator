using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BvgCalculatorEngine.Contracts
{
    public sealed class BvgConstants
    {
        public decimal Umws { get; set; } = 0.068m;
        public decimal BvgZins { get; set; } = 0.0125m;
        public decimal MaxAhvRente { get; private set; } = 28200m;
    }
}
