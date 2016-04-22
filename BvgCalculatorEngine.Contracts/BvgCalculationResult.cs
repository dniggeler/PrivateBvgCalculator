using System;

namespace BvgCalculatorEngine.Contracts
{
    public class ProjectionItem
    {
        public DateTime DateOfRechnungsjahr { get; set; }
        public decimal Altersguthaben { get; set; }
    }

    public class BvgCalculationResult
    {
        public decimal VersicherterLohn { get; set; }
        public decimal Altersgutschrift { get; set; }
        public decimal EaghMitZins { get; set; }
        public decimal EaghOhneZins { get; set; }
        public decimal Altersrente { get; set; }
        public decimal Invalidenrente { get; set; }
        public decimal Partnerrente { get; set; }
        public decimal Waisenrente { get; set; }
        public ProjectionItem[] Projections;
    }
}
