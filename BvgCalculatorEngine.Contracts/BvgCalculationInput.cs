using System;

namespace BvgCalculatorEngine.Contracts
{
    public class BvgCalculationInput
    {
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEintritt { get; set; }
        public Geschlecht Geschlecht { get; set; }
        public decimal Lohn { get; set; }
        public decimal Altersguthaben { get; set; }
    }
}